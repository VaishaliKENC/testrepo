import React, { useCallback, useEffect, useMemo, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { RootState } from "../../../../redux/store";
import { useAppDispatch, useAppSelector } from "../../../../hooks";
import CommonTable from '../../../../components/shared/CommonComponents/CommonTable';
import AlertMessage from "../../../../components/shared/AlertMessage";
import { fetchUsersSlice, fetchBusinessRuleSlice, saveSelectedUsersSlice, fetchUsersBusinessRuleSlice, fetchUsersSelectedSlice } from "../../../../redux/Slice/admin/oneTimeAssignmentsSlice";
import { User } from "../../../../Types/assignmentTypes";
import { LookupTextValue } from "../../../../Types/commonTableTypes";
import { COMMON_TABLE_TYPE } from '../../../../utils/Constants/Enums';
import { MANAGE_ONE_TIME_ASSIGNMENT_USER_BUSINESS_RULE_TABLE_CONFIG, MANAGE_ONE_TIME_ASSIGNMENT_USER_TABLE_CONFIG } from "../../../../utils/Constants/tableConfig";

import { debounce } from "lodash";

interface Props {
    onSelectedUsersChange: (selected: string[]) => void;
    onSelectedBusinessRuleChange: (name: string, value: string) => void;
    onSetActiveTab: (tabKey: string) => void;
    onCancel: () => void;
    activeTabOnAPI: string;
}

const SelectUsersContent: React.FC<Props> = ({ onSelectedUsersChange, onSelectedBusinessRuleChange, onSetActiveTab, onCancel, activeTabOnAPI }) => {
    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const clientId: any = useSelector((state: RootState) => state.auth.clientId);
    const id: any = useSelector((state: RootState) => state.auth.id);
    const usersList: any = useSelector((state: RootState) => state.oneTimeAssignments.users);
    const { loading } = useAppSelector((state: RootState) => state.oneTimeAssignments);
    const selectedUsersList: any = useSelector((state: RootState) => state.oneTimeAssignments.usersSelected);
    const hasUsersInRedux = Array.isArray(selectedUsersList?.userList) && selectedUsersList.userList.length > 0;

    //radioSearchOrSelectUserGroup
    const [selectedUserSearchOrUserGroup, setSelectedUserSearchOrUserGroup] = useState<string>('SearchUser');

    // Select Users Tab
    const [users, setUsers] = useState<User[]>([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(5);
    const [selectedUsers, setSelectedUsers] = useState<string[]>([]);
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


    /** Fetch the initial user list when the component mounts. */
    // useEffect(() => {
    //     if (!id || !clientId) return;
    //     fetchUserList(1, pageSize, appliedFilters);
    // }, [id, clientId]);

    useEffect(() => {
        if (activeTabOnAPI === 'tabSelectUsers' && id && clientId) {
            fetchUserList(1, pageSize, appliedFilters);
            setCurrentPage(1);
        }
    }, [activeTabOnAPI, id, clientId]);

    useEffect(() => {
        // console.log("selectedUsers", selectedUsers)
        // console.log("selectedUsersList", selectedUsersList)

        // Extract existing GUIDs from userList
        // Step 1: Extract valid systemUserGUIDs from
        if (selectedUsersList?.length == 0) {
            setSelectedUsers([]);
        }

        if (selectedUsersList && selectedUsersList?.userList?.length) {
            const validGUIDs: any = new Set(selectedUsersList?.userList?.map((user: any) => user.systemUserGUID));

            // Step 2: Keep only those selectedUsers that exist in userList
            const filteredExisting: any[] = selectedUsers.filter(guid => validGUIDs.has(guid));

            // console.log("filteredExisting", filteredExisting);

            setSelectedUsers(filteredExisting);
        }


        // const filteredSelectedUsers = selectedUsersList.length && selectedUsersList?.userList?.filter((user: any) =>
        //     selectedUsers.includes(user.systemUserGUID)
        // );

        // console.log("filteredSelectedUsers", filteredSelectedUsers)
    }, [selectedUsersList])

    /** Update the local users state when usersList changes. */
    useEffect(() => {
        setUsers(usersList?.learnerList || []);
    }, [usersList]);

    /** Fetch user list with pagination and filters. */
    const fetchUserList = (pageIndex: number, pageSize: number, filters: any) => {
        const params = {
            clientId,
            listRange: {
                pageIndex,
                pageSize,
            },
            keyWord: '',
            isActive: true
        };
        dispatch(fetchUsersSlice(params));
    };

    // const handleClearFilters = () => {
    //     setAppliedFilters(null);
    //     setCurrentPage(1);
    //     setSearchMode(null);

    //     const params = {
    //         clientId,
    //         listRange: {
    //             pageIndex: 1,
    //             pageSize,
    //         },
    //         keyWord: '',
    //         isActive: true
    //     };

    //     dispatch(fetchUsersSlice(params));
    // };

    /** Handle individual user selection via checkbox.*/
    const handleCheckboxChange = (userId: string) => {
        const updated = selectedUsers.includes(userId)
            ? selectedUsers.filter((x) => x !== userId)
            : [...selectedUsers, userId];

        setSelectedUsers(updated);
        onSelectedUsersChange(updated);
        pushUsersIntoArrayofObject(updated);
    };

    /** Handle selecting/deselecting all users. */
    const handleSelectAll = (selectAll: boolean) => {
        const allIds = users.map((data) => data.id);
        const updated = selectAll
            ? Array.from(new Set([...selectedUsers, ...allIds]))
            : selectedUsers.filter((id) => !allIds.includes(id));

        setSelectedUsers(updated);
        onSelectedUsersChange(updated);
        pushUsersIntoArrayofObject(updated);
    };


    /** Handle user search. */
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
    //         keyWord: trimmedKeyword,
    //         isActive: true
    //     };
    //     dispatch(fetchUsersSlice(params));
    // };

    //  Create a debounced search function
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
                    keyWord: trimmedKeyword,
                    isActive: true,
                };

                dispatch(fetchUsersSlice(params));
            }, 500), //  Debounce delay: 500ms
        [dispatch, clientId, pageSize]
    );

    //  Wrap handleSearch with useCallback
    const handleSearch = useCallback(
        (keyword: string) => {
            debouncedSearch(keyword);
        },
        [debouncedSearch]
    );

    // Cleanup to avoid memory leaks
    useEffect(() => {
        return () => {
            debouncedSearch.cancel();
        };
    }, [debouncedSearch]);

    /** Handle pagination changes. */
    const handlePageChange = (page: number, newPageSize?: number) => {
        if (newPageSize) setPageSize(newPageSize);
        setCurrentPage(page);
        console.log("page", page)

        const params = {
            clientId: clientId,
            listRange: {
                pageIndex: page,
                pageSize: newPageSize || pageSize,
            },
            keyWord: searchKeyword,
            isActive: true
        };
        dispatch(fetchUsersSlice(params));
    };

    // Business Rule
    /** Fetch business rule list */
    const [businessRuleList, setBusinessRuleList] = useState<any>([]);
    const [selectedBusinessRule, setSelectedBusinessRule] = useState<string>('');
    useEffect(() => {
        // if(!clientId) return;   
        if (activeTabOnAPI !== 'tabSelectUsers' || !clientId) return;


        const params = {
            clientId: clientId
        }

        dispatch(fetchBusinessRuleSlice(params)).then((response: any) => {
            if (response.meta.requestStatus == "fulfilled") {
                setBusinessRuleList(response.payload.activityAssignmentList);
            } else {
                setAlert({ type: "error", message: 'Business rule data fetch failed: ' + response.msg });
            }
        })
            .catch((error) => {
                console.error("Business rule data fetch error:", error);
                setAlert({ type: "error", message: 'An unexpected error occurred.' });
            });
    }, [activeTabOnAPI, clientId]);

    const handleBusinessRuleChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        if (event.target.options[event.target.selectedIndex].text != 'select') {
            setSelectedBusinessRule(event.target.value);
            onSelectedBusinessRuleChange(event.target.options[event.target.selectedIndex].text, event.target.value);
        }
        else {
            setSelectedBusinessRule('');
            onSelectedBusinessRuleChange('', '');
        }
    };

    const handleSetActiveTab = (tabKey: string) => {
        onSetActiveTab(tabKey);
    }

    const [selectedUsersObjects, setSelectedUsersObjects] = useState<
        { systemUserGUID: string; clientId: string; createdById: string }[]
    >([]);
    const pushUsersIntoArrayofObject = (ids: string[]) => {
        const finalSelectedUsers = ids.map((uid) => ({
            systemUserGUID: uid,
            clientId: clientId,
            createdById: id,
        }));

        setSelectedUsersObjects(finalSelectedUsers);
    };

    // Sync Redux user removal to local state
    useEffect(() => {
        if (!hasUsersInRedux) {
            setSelectedUsers([]);
            setSelectedUsersObjects([]);
        }
    }, [hasUsersInRedux]);


    // const handleNext = () => {
    //     console.log("selectedUsersObjects", selectedUsersObjects)

    //     if (selectedUsersObjects.length > 0) {
    //         dispatch(saveSelectedUsersSlice(selectedUsersObjects))
    //             .then((response: any) => {
    //                 if (response.meta.requestStatus == "fulfilled") {
    //                     //setAlert({ type: "success", message: "Selected users saved successfully." }); 
    //                     const params = {
    //                         clientId,
    //                         createdById: id,
    //                         listRange: {
    //                             pageIndex: 1,
    //                             pageSize,
    //                             sortExpression: ""
    //                         }
    //                     };
    //                     dispatch(fetchUsersSelectedSlice(params));
    //                 } else {
    //                     setAlert({ type: "error", message: 'Saving users failed: ' + response.msg });
    //                 }
    //             })
    //             .catch((error) => {
    //                 console.error("Saving users error:", error);
    //                 setAlert({ type: "error", message: 'An unexpected error occurred.' });
    //             });
    //     }    handleSetActiveTab("tabSelectedUsers");
    // }

    // Users List (business rule) 

    const handleNext = () => {
        if (selectedUsers.length > 0) {
            const selectedUsersObjects = selectedUsers.map((uid) => ({
                systemUserGUID: uid,
                clientId: clientId,
                createdById: id,
            }));

            console.log("Saving selected users:", selectedUsersObjects);

            dispatch(saveSelectedUsersSlice(selectedUsersObjects))
                .then((response: any) => {
                    if (response.meta.requestStatus === "fulfilled") {
                        clearSearch(); //  clear only search text
                        const params = {
                            clientId,
                            createdById: id,
                            listRange: {
                                pageIndex: 1,
                                pageSize,
                                sortExpression: ""
                            }
                        };
                        dispatch(fetchUsersSelectedSlice(params));
                    } else {
                        setAlert({ type: "error", message: 'Saving users failed: ' + response.msg });
                    }
                })
                .catch((error) => {
                    console.error("Saving users error:", error);
                    setAlert({ type: "error", message: 'An unexpected error occurred.' });
                });
        } else {
            // If nothing selected, clear the saved list
            dispatch(saveSelectedUsersSlice([]));
        }

        handleSetActiveTab("tabSelectedUsers");
    };

    //Clear search text and reset search mode
    const clearSearch = () => {
        setSearchKeyword('');
        setSearchMode(null);
    };

    const handleBack = () => {
        // setSelectedUsers([]);
        onCancel();
        clearSearch(); // clear only search text
        handleSetActiveTab("tabSelectActivity")
    }




    const usersListBusinessRule: any = useSelector((state: RootState) => state.oneTimeAssignments.usersBusinessRule);
    const [usersBusinessRule, setUsersBusinessRule] = useState<User[]>([]);
    const [currentPageBusinessRule, setCurrentPageBusinessRule] = useState(1);
    const [pageSizeBusinessRule, setPageSizeBusinessRule] = useState(5);
    const [searchModeBusinessRule, setSearchModeBusinessRule] = useState<"filter" | "keyword" | null>(null);
    const [isFilterVisibleBusinessRule, setFilterVisibleBusinessRule] = useState(false);
    const [appliedFiltersBusinessRule, setAppliedFiltersBusinessRule] = useState<any>(null);
    const [searchKeywordBusinessRule, setSearchKeywordBusinessRule] = useState<string>('');

    /** Fetch the initial user list (business rule) when the business rule is changed in dropdown. */
    useEffect(() => {
        if (!id || !clientId || !selectedBusinessRule) return;
        fetchUserListBusinessRule(1, pageSizeBusinessRule, appliedFiltersBusinessRule);
    }, [selectedBusinessRule]);

    /** Update the local users (business rule) state when usersList changes. */
    useEffect(() => {
        setUsersBusinessRule(usersListBusinessRule?.userList || []);
    }, [usersListBusinessRule]);

    /** Fetch user list (business rule) with pagination and filters. */
    const fetchUserListBusinessRule = (pageIndex: number, pageSize: number, filters: any) => {
        const params = {
            clientId,
            RuleId: selectedBusinessRule,
            listRange: {
                pageIndex,
                pageSize,
            },
            keyWord: '',
            isActive: true
        };
        dispatch(fetchUsersBusinessRuleSlice(params));
    };

    /** Handle pagination changes. */
    const handlePageChangeBusinessRule = (page: number, newPageSize?: number) => {
        if (newPageSize) setPageSizeBusinessRule(newPageSize);
        setCurrentPageBusinessRule(page);

        const params = {
            clientId: clientId,
            RuleId: selectedBusinessRule,
            listRange: {
                pageIndex: page,
                pageSize: newPageSize || pageSizeBusinessRule,
            },
            keyWord: searchKeywordBusinessRule,
            isActive: true
        };
        dispatch(fetchUsersBusinessRuleSlice(params));
    };

    return (
        <>
            {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}

            {loading || isSubmitting ? (<div className="yp-loading-overlay"><div className="yp-spinner"><div className="spinner-border"></div></div></div>) : null}

            <div className="yp-tab-content-padding">
                <div className="row">
                    <div className="col-12 col-md-6 col-lg-4">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                {/* <div className="form-check form-check-box form-check-box-gray form-check-box-lg"> */}
                                <div
                                    className={`form-check form-check-box form-check-box-gray form-check-box-lg ${selectedUserSearchOrUserGroup === "SearchUser" ? "active" : ""
                                        }`}
                                    onClick={() => setSelectedUserSearchOrUserGroup("SearchUser")}
                                    style={{ cursor: "pointer" }} // Optional: show pointer on hover
                                >
                                    <input type="radio" name="radioSearchOrSelectUserGroup"
                                        className="form-check-input d-none"
                                        id="radioSearchUser"
                                        value={'SearchUser'}
                                        checked={selectedUserSearchOrUserGroup === "SearchUser"}
                                        // onChange={(event) => setSelectedUserSearchOrUserGroup(event.target.value)}
                                        readOnly />
                                    <label className="form-check-label yp-color-dark-purple yp-ml-0-px" htmlFor="radioSearchUser">Search User</label>
                                    <div className="yp-fs-11-400 yp-text-565656 yp-mt-5-px yp-ml-0-px">
                                        Search user from the user's list to whom the activities are to be assigned.
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="col-12 col-md-6 col-lg-4">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                {/* <div className="form-check form-check-box form-check-box-gray form-check-box-lg"> */}
                                <div
                                    className={`form-check form-check-box form-check-box-gray form-check-box-lg ${selectedUserSearchOrUserGroup === "SelectUserGroup" ? "active" : ""
                                        }`}
                                    onClick={() => setSelectedUserSearchOrUserGroup("SelectUserGroup")}
                                    style={{ cursor: "pointer" }}
                                >
                                    <input type="radio" name="radioSearchOrSelectUserGroup"
                                        className="form-check-input d-none"
                                        id="radioSelectUserGroup"
                                        value={'SelectUserGroup'}
                                        checked={selectedUserSearchOrUserGroup === "SelectUserGroup"}
                                        // onChange={(event) => setSelectedUserSearchOrUserGroup(event.target.value)}
                                        readOnly />
                                    <label className="form-check-label yp-color-dark-purple yp-ml-0-px" htmlFor="radioSelectUserGroup">Select User Group</label>
                                    <div className="yp-fs-11-400 yp-text-565656 yp-mt-5-px yp-ml-0-px">
                                        Select a Business Rule which contains a set of user.
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            {
                selectedUserSearchOrUserGroup === "SearchUser" && (
                    <div className="yp-custom-table-section">
                        <CommonTable
                            type={COMMON_TABLE_TYPE.MANAGE_ONE_TIME_ASSIGNMENT_USER}
                            tableConfig={MANAGE_ONE_TIME_ASSIGNMENT_USER_TABLE_CONFIG}
                            data={users}
                            onSearch={handleSearch}
                            onPageChange={handlePageChange}
                            currentPage={currentPage}
                            totalRecords={
                                Array.isArray(usersList?.learnerList) && usersList.learnerList.length > 0
                                    ? usersList.learnerList[0]?.listRange?.totalRows
                                    : 0
                            }
                            pageSize={pageSize}
                            selectedUsers={selectedUsers}
                            handleCheckboxChange={handleCheckboxChange}
                            handleSelectAll={handleSelectAll}
                            searchMode={searchMode}
                            setSearchMode={setSearchMode}
                            isFilterVisible={isFilterVisible}
                            setFilterVisible={setFilterVisible}
                            searchValue={searchKeyword}
                        />
                    </div>
                )
            }

            {
                selectedUserSearchOrUserGroup === "SelectUserGroup" && (
                    <>
                        <div className="yp-tab-content-padding">
                            <div className="row">
                                <div className="col-12 col-md-6 col-lg-4">
                                    <div className="form-group">
                                        <div className="yp-form-control-wrapper">
                                            <select disabled className={`form-control yp-form-control`}
                                                name="businessRule"
                                                value={selectedBusinessRule}
                                                onChange={handleBusinessRuleChange}
                                                id="">
                                                <option value="">Select</option>
                                                {businessRuleList.map((data: any) => (
                                                    <option key={data.id} value={data.id}>
                                                        {data.ruleName}
                                                    </option>
                                                ))}
                                            </select>
                                            <label className="form-label">Select User Group</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        {
                            selectedBusinessRule && (
                                <div className="yp-custom-table-section">
                                    <CommonTable
                                        type={COMMON_TABLE_TYPE.MANAGE_ONE_TIME_ASSIGNMENT_USER_BUSINESS_RULE}
                                       tableConfig={MANAGE_ONE_TIME_ASSIGNMENT_USER_BUSINESS_RULE_TABLE_CONFIG} 
                                        data={usersBusinessRule}
                                        // onSearch={handleSearch}
                                        onPageChange={handlePageChangeBusinessRule}
                                        currentPage={currentPageBusinessRule}
                                        totalRecords={
                                            Array.isArray(usersListBusinessRule?.userList) && usersListBusinessRule.userList.length > 0
                                                ? usersListBusinessRule.userList[0]?.listRange?.totalRows
                                                : 0
                                        }
                                        pageSize={pageSizeBusinessRule}
                                    // selectedUsers={selectedUsers}
                                    // handleCheckboxChange={handleCheckboxChange}
                                    // handleSelectAll={handleSelectAll}
                                    // searchMode={searchModeBusinessRule}
                                    // setSearchMode={setSearchModeBusinessRule}
                                    // isFilterVisible={isFilterVisibleBusinessRule}
                                    // setFilterVisible={setFilterVisibleBusinessRule}
                                    />


                                </div>

                            )

                        }
                    </>
                )
            }
            <div className="yp-tab-content-padding">
                <div className="row">
                    <div className="col-12">
                        <div className="yp-form-button-row">
                            <button className="btn btn-primary"
                                onClick={() => handleNext()}
                                disabled={
                                    selectedUsersObjects.length === 0 &&
                                    !hasUsersInRedux &&
                                    selectedBusinessRule === ''
                                }
                            // disabled={selectedUsersObjects.length === 0 && selectedBusinessRule === ''}
                            >
                                Next</button>
                            <button className="btn btn-secondary"
                                onClick={() => handleBack()}>Back</button>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default SelectUsersContent;
