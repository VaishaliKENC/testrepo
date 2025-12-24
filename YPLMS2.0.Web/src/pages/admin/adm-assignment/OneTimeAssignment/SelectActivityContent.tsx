import React, { useCallback, useEffect, useMemo, useRef, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { RootState } from "../../../../redux/store";
import { useAppDispatch, useAppSelector } from "../../../../hooks";
import CommonTable from '../../../../components/shared/CommonComponents/CommonTable';
import AlertMessage from "../../../../components/shared/AlertMessage";
import {
    fetchActivitiesSlice,
    fetchCategoriesSlice,
    fetchSubCategoriesSlice,
    fetchActivityTypesSlice,
    saveSelectedActivitiesSlice
} from "../../../../redux/Slice/admin/oneTimeAssignmentsSlice";
import { Activity, selectActivityContentProps } from "../../../../Types/assignmentTypes";
import {
    LookupTextValue,
    ICategoryPayload,
    ISubCategoryPayload
} from "../../../../Types/commonTableTypes";
import ActivityFilter from "./ActivityFilter";
import { COMMON_TABLE_TYPE } from '../../../../utils/Constants/Enums';
import { ACTIVITY_CONTENT_TYPE_ENUM } from '../../../../utils/Constants/Enums';
import { MANAGE_ONE_TIME_ASSIGNMENT_ACTIVITY_TABLE_CONFIG } from "../../../../utils/Constants/tableConfig";

import { debounce } from "lodash";

interface Props {
    onSelectedActivitiesChange: (selected: string[]) => void;
    onSetActiveTab: (tabKey: string) => void;
    onCancel: () => void;
    activeTabOnAPI: string;
    resetForm?: boolean;
    onResetComplete?: () => void;
}

const SelectActivityContent: React.FC<Props> = (
    {
        onSelectedActivitiesChange,
        onSetActiveTab,
        onCancel,
        activeTabOnAPI, resetForm, onResetComplete

    }) => {
    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const clientId: any = useSelector((state: RootState) => state.auth.clientId);
    const id: any = useSelector((state: RootState) => state.auth.id);
    const activityList: any = useSelector((state: RootState) => state.oneTimeAssignments.activities);
    const categoriesList: any = useSelector((state: RootState) => state.oneTimeAssignments.categories);
    const subCategoriesList: any = useSelector((state: RootState) => state.oneTimeAssignments.subCategories);
    const activityTypesList: any = useSelector((state: RootState) => state.oneTimeAssignments.activityTypes);

    const { loading } = useAppSelector((state: RootState) => state.oneTimeAssignments);

    // Select Activity Tab
    const [activities, setActivities] = useState<Activity[]>([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(5);
    const [selectedActivities, setSelectedActivities] = useState<string[]>([]);
    const [searchMode, setSearchMode] = useState<"filter" | "keyword" | null>(null);
    const [isFilterVisible, setFilterVisible] = useState(false);
    const [modalAlertMessageConfig, setModalAlertMessageConfig] = useState<{
        type: "success" | "error";
        message: string;
        duration?: string;
        autoClose?: boolean;
    }>({
        type: 'success',
        message: '',
        duration: '',
        autoClose: false
    });

    const [appliedFilters, setAppliedFilters] = useState<any>(null);
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [searchKeyword, setSearchKeyword] = useState<string>('');
    const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);


    const [categoryList, setCategoryList] = useState<LookupTextValue[]>([]);
    const [selectedCategory, setSelectedCategory] = useState<string>('');
    const [subCategoryList, setSubCategoryList] = useState<LookupTextValue[]>([]);
    const [activityTypeList, setActivityTypeList] = useState<LookupTextValue[]>([]);


    //  Add debounce timer ref
    const debounceTimer = useRef<NodeJS.Timeout | null>(null);
    /** Fetch the initial activity list when the component mounts. */

    useEffect(() => {
        if (!clientId || !id) return;
        if (activeTabOnAPI === 'tabSelectActivity') {
            fetchActivityList(1, pageSize, appliedFilters);
            setCurrentPage(1);
        }
    }, [activeTabOnAPI, clientId, id]);

    /** Update the local activity state when activityList changes. */
    useEffect(() => {
        setActivities(activityList?.activityAssignmentList || []);
    }, [activityList]);

    useEffect(() => {
        setSubCategoryList(subCategoriesList?.activityAssignmentList || []);
    }, [subCategoriesList]);

    useEffect(() => {
        if (resetForm) {
            setSelectedActivities([]);
            onSelectedActivitiesChange([]);
            setSelectedActivityObjects([]);

            // Add these lines to reset filters
            setAppliedFilters(null);
            setSearchKeyword('');
            setSearchMode(null);
            setCurrentPage(1);

            fetchActivityList(1, pageSize, null); // reload unfiltered data

            onResetComplete?.();
        }
    }, [resetForm]);

    /** Fetch activity list with pagination and filters. */
    const fetchActivityList = (pageIndex: number, pageSize: number, filters: any) => {

        if (debounceTimer.current) clearTimeout(debounceTimer.current);

        debounceTimer.current = setTimeout(() => {

            let finalActivityTypeId: number | undefined = 0;
            if (filters?.activityType) {
                finalActivityTypeId = getValueFromEnum(ACTIVITY_CONTENT_TYPE_ENUM, filters?.activityType)
            }

            const params = {
                clientId,
                listRange: {
                    pageIndex,
                    pageSize,
                },
                ActivityName: "",
                ActivityType: finalActivityTypeId,
                categoryId: filters?.categoryId,
                subCategoryId: filters?.subCategoryId,
                isActiveActivity: true,
            };
            dispatch(fetchActivitiesSlice(params));
        }, 300); // ✅ 300ms debounce
    };

    /** Clear applied filters and reset the user list.  */
    const handleClearFilters = () => {
        setAppliedFilters(null);
        setCurrentPage(1);
        setSearchMode(null);

        const params = {
            clientId,
            listRange: {
                pageIndex: 1,
                pageSize,
            },
            ActivityName: "",
            ActivityType: 0,
            categoryId: "",
            subCategoryId: "",
            isActiveActivity: true,
        };

        dispatch(fetchActivitiesSlice(params));
    };

    /** Handle individual activity selection via checkbox.*/
    const handleCheckboxChange = (acitivityId: string) => {

        const updated = selectedActivities.includes(acitivityId)
            ? selectedActivities.filter((x) => x !== acitivityId)
            : [...selectedActivities, acitivityId];

        setSelectedActivities(updated);
        onSelectedActivitiesChange(updated);
        pushActivitiesIntoArrayofObject(updated);
    };

    /** Handle selecting/deselecting all activities. */
    const handleSelectAll = (selectAll: boolean) => {
        const allIds = activities.map((data) => data.id);
        const updated = selectAll
            ? Array.from(new Set([...selectedActivities, ...allIds]))
            : selectedActivities.filter((id) => !allIds.includes(id));

        setSelectedActivities(updated);
        onSelectedActivitiesChange(updated);
        pushActivitiesIntoArrayofObject(updated);
    };

    /** Handle activity search. */
    // const handleSearch = (keyword: string) => {
    //     const trimmedKeyword = keyword.trim();

    //     setSearchKeyword(trimmedKeyword);
    //     setSearchMode(trimmedKeyword ? 'keyword' : null);

    //     // Always reset current page to 1 before calling fetch
    //     const updatedPage = 1;
    //     setCurrentPage(updatedPage);

    //     const params = {
    //         clientId,
    //         listRange: {
    //             pageIndex: updatedPage,
    //             pageSize,
    //         },
    //         ActivityName: trimmedKeyword,
    //         isActiveActivity: true,
    //     };
    //     dispatch(fetchActivitiesSlice(params));
    // };

    const debouncedSearch = useMemo(
        () =>
            debounce((keyword: string) => {
                const trimmedKeyword = keyword.trim();

                setSearchKeyword(trimmedKeyword);
                setSearchMode(trimmedKeyword ? "keyword" : null);

                // Always reset current page to 1 before calling fetch
                const updatedPage = 1;
                setCurrentPage(updatedPage);

                const params = {
                    clientId,
                    listRange: {
                        pageIndex: updatedPage,
                        pageSize,
                    },
                    ActivityName: trimmedKeyword,
                    isActiveActivity: true,
                };

                dispatch(fetchActivitiesSlice(params));
            }, 500), //  Debounce delay: 500ms
        [dispatch, clientId, pageSize]
    );

    // UseCallback to wrap handleSearch
    const handleSearch = useCallback(
        (keyword: string) => {
            debouncedSearch(keyword);
        },
        [debouncedSearch]
    );

    //  Cleanup to prevent memory leaks on unmount
    useEffect(() => {
        return () => {
            debouncedSearch.cancel();
        };
    }, [debouncedSearch]);

    /** Handle pagination changes. */
    const handlePageChange = (page: number, newPageSize?: number) => {
        if (newPageSize) setPageSize(newPageSize);
        setCurrentPage(page);
        //setSelectedActivities([]); commented as no need to clear selected records for activity tab data

        let finalActivityTypeId: number | undefined = 0;
        if (appliedFilters?.activityType) {
            finalActivityTypeId = getValueFromEnum(ACTIVITY_CONTENT_TYPE_ENUM, appliedFilters?.activityType)
        }

        const params = {
            clientId: clientId,
            listRange: {
                pageIndex: page,
                pageSize: newPageSize || pageSize,
            },
            ActivityName: searchKeyword || appliedFilters?.activityName,
            ActivityType: finalActivityTypeId,
            categoryId: appliedFilters?.categoryId,
            subCategoryId: appliedFilters?.subCategoryId,
            isActiveActivity: true,
        };
        dispatch(fetchActivitiesSlice(params));
    };


    /** Handle filter submission.*/
    const handleFilterSubmit = (filterData: any) => {
        if (!id || !clientId) return;
        setSearchMode('filter');
        setAppliedFilters(filterData);
        setCurrentPage(1);

        let finalActivityTypeId: number | undefined = 0;
        if (filterData?.activityType) {
            finalActivityTypeId = getValueFromEnum(ACTIVITY_CONTENT_TYPE_ENUM, filterData?.activityType)
        }

        const params = {
            clientId: clientId,
            listRange: {
                pageIndex: 1,
                pageSize: pageSize,
            },
            ActivityName: filterData?.activityName,
            ActivityType: finalActivityTypeId,
            categoryId: filterData?.categoryId,
            subCategoryId: filterData?.subCategoryId,
            // activityCriteria: {
            //   activityType: finalActivityTypeId,
            //   categoryId: filterData?.categoryId,
            //   subCategoryId: filterData?.subCategoryId,
            //   activityName: filterData?.activityName,
            // },      
            isActiveActivity: true,
        } as const;

        dispatch(fetchActivitiesSlice(params))
            .then((response: any) => {
                if (response.meta.requestStatus === 'fulfilled') {
                    //setActivities(response.payload.activityAssignmentList || []);
                    setAlert({ type: "success", message: "Filter applied successfully." });
                } else {
                    setAlert({ type: "error", message: 'Failed to apply filter: ' + response.payload?.msg || 'Unknown error.' });
                }
            })
            .catch((error) => {
                console.error('Error while applying filter:', error);
                setAlert({ type: "error", message: 'An unexpected error occurred.' });
            });
    }

    // Match selected activity type key in enum and return that key's value
    function getValueFromEnum<T extends Record<string, any>>(enumObj: T, key: string): T[keyof T] | undefined {
        if (Object.prototype.hasOwnProperty.call(enumObj, key)) {
            return enumObj[key as keyof T];
        }
        return undefined;
    }

    /****************************/
    useEffect(() => {
        // if (!clientId) return;
        if (activeTabOnAPI !== 'tabSelectActivity' || !clientId) return;

        const handler = setTimeout(() => {
            const params: ICategoryPayload = {
                clientId: clientId,
                listRange: {
                    pageIndex: 0,
                    pageSize: 0,
                    totalRows: 0,
                    sortExpression: 'CategoryName asc',
                    requestedById: ''
                },
                IsActiveActivity: true
            };

            dispatch(fetchCategoriesSlice(params)).then((response: any) => {
                if (response.meta.requestStatus == "fulfilled") {
                    setCategoryList(response.payload.activityAssignmentList);
                } else {
                    setAlert({ type: "error", message: 'Categories data fetch failed: ' + response.msg });
                }
            })
                .catch((error) => {
                    console.error("Categories data fetch error:", error);
                    setAlert({ type: "error", message: 'An unexpected error occurred.' });
                });

        }, 500); //  delay (500ms)

        return () => clearTimeout(handler); //  Cleanup to avoid multiple calls
    }, [activeTabOnAPI, clientId]);

    useEffect(() => {
        // if (!clientId) return;
        if (activeTabOnAPI !== 'tabSelectActivity' || !clientId) return;


        dispatch(fetchActivityTypesSlice(clientId)).then((response: any) => {
            if (response.meta.requestStatus == "fulfilled") {
                setActivityTypeList(response.payload.assetTypeList);
            } else {
                setAlert({ type: "error", message: 'Activity type data fetch failed: ' + response.msg });
            }
        })
            .catch((error) => {
                console.error("Activity type data fetch error:", error);
                setAlert({ type: "error", message: 'An unexpected error occurred.' });
            });
    }, [activeTabOnAPI, clientId]);

    const handleCategoryChange = (categoryId: string) => {
        setSelectedCategory(categoryId);

        const params: ISubCategoryPayload = {
            clientId: clientId,
            Id: categoryId,
            IsActiveActivity: true,
        }
        dispatch(fetchSubCategoriesSlice(params)).then((response: any) => {
            if (response.meta.requestStatus == "fulfilled") {
                setSubCategoryList(response.payload.activityAssignmentList);
            } else {
                setAlert({ type: "error", message: 'Sub Categories data fetch failed: ' + response.msg });
            }
        })
            .catch((error) => {
                console.error("Sub Categories data fetch error:", error);
                setAlert({ type: "error", message: 'An unexpected error occurred.' });
            });
    };
    //Clear search text and reset search mode
    const clearSearch = () => {
        setSearchKeyword('');
        setSearchMode(null);
    };

    const handleCancel = () => {
        setSelectedActivities([]);
        onCancel();
        clearSearch(); // clear only search text
    }

    const handleSetActiveTab = (tabKey: string) => {
        onSetActiveTab(tabKey);
    }


    const [selectedActivityObjects, setSelectedActivityObjects] = useState<
        { activityId: string; clientId: string; createdById: string }[]
    >([]);
    const pushActivitiesIntoArrayofObject = (ids: string[]) => {
        const finalSelectedActivities = ids.map((aid) => ({
            activityId: aid,
            clientId: clientId,
            createdById: id,
        }));

        setSelectedActivityObjects(finalSelectedActivities);
    };

    const handleNext = () => {
        if (selectedActivityObjects.length > 0) {
            dispatch(saveSelectedActivitiesSlice(selectedActivityObjects))
                .then((response: any) => {
                    // If signup is successful, navigate to the login
                    if (response.meta.requestStatus == "fulfilled") {
                        //setAlert({ type: "success", message: "Selected activities saved successfully." });
                        clearSearch(); //  clear only search text
                        handleSetActiveTab("tabSelectUsers");
                    } else {
                        setAlert({ type: "error", message: 'Saving activities failed: ' + response.msg });
                    }
                })
                .catch((error) => {
                    console.error("Saving activities error:", error);
                    setAlert({ type: "error", message: 'An unexpected error occurred.' });
                });
        }
    }

    return (
        <>
            {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}

            {loading || isSubmitting ? (<div className="yp-loading-overlay"><div className="yp-spinner"><div className="spinner-border"></div></div></div>) : null}

            <div className="yp-custom-table-section">
                <CommonTable
                    type={COMMON_TABLE_TYPE.MANAGE_ONE_TIME_ASSIGNMENT_ACTIVITY}
                    tableConfig={MANAGE_ONE_TIME_ASSIGNMENT_ACTIVITY_TABLE_CONFIG}
                    filterComponent={
                        <ActivityFilter
                            onSubmit={handleFilterSubmit}
                            onClear={handleClearFilters}
                            closeFilter={handleClearFilters}
                            activityTypeList={activityTypeList}
                            categoryList={categoryList}
                            subCategoryList={subCategoryList}
                            onCategoryChange={handleCategoryChange}
                            resetFilterForm={resetForm}
                        />
                    }
                    onFilterSubmit={handleFilterSubmit}
                    data={activities}
                    onSearch={handleSearch}
                    onPageChange={handlePageChange}
                    currentPage={currentPage}
                    totalRecords={
                        Array.isArray(activityList?.activityAssignmentList) && activityList.activityAssignmentList.length > 0
                            ? activityList.activityAssignmentList[0]?.listRange?.totalRows
                            : 0
                    }
                    pageSize={pageSize}
                    selectedUsers={selectedActivities}
                    handleCheckboxChange={handleCheckboxChange}
                    handleSelectAll={handleSelectAll}
                    searchMode={searchMode}
                    setSearchMode={setSearchMode}
                    isFilterVisible={isFilterVisible}
                    setFilterVisible={setFilterVisible}
                    searchValue={searchKeyword}
                />
            </div>
            <div className="yp-tab-content-padding">
                <div className="row">
                    <div className="col-12">
                        <div className="yp-form-button-row">
                            <button className="btn btn-primary"
                                onClick={() => handleNext()}
                                disabled={selectedActivities.length === 0}>
                                Next
                            </button>
                            <button type="reset" className="btn btn-secondary"
                                onClick={() => handleCancel()}
                                disabled={selectedActivities.length === 0}>Cancel</button>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default SelectActivityContent;
