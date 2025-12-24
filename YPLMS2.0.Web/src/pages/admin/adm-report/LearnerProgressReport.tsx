import React, { useEffect, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useSelector } from "react-redux";
import { AdminPageRoutes } from "../../../utils/Constants/Admin_PageRoutes";
import CustomBreadcrumb from "../../../components/shared/CustomBreadcrumb";
import { RootState } from "../../../redux/store";
import { useAppDispatch, useAppSelector } from "../../../hooks";
import AlertMessage from "../../../components/shared/AlertMessage";
import {
    fetchActivitiesSlice,
    fetchActivityTypesSlice,
} from "../../../redux/Slice/admin/oneTimeAssignmentsSlice";
import { GenerateReportPayload, LookupTextValue } from "../../../Types/commonTableTypes";
import CommonTable from "../../../components/shared/CommonComponents/CommonTable";
import {
    ACTIVITY_CONTENT_TYPE_ENUM,
    COMMON_TABLE_TYPE,
} from "../../../utils/Constants/Enums";
import { REPORT_LEARNER_PROGRESS_ACTIVITY_TABLE_CONFIG, REPORT_LEARNER_PROGRESS_TABLE_CONFIG } from "../../../utils/Constants/tableConfig";
import { Activity } from "../../../Types/assignmentTypes";
import { fetchStandardProfileFieldSlice, generateReportSlice, StandardProfileField } from "../../../redux/Slice/admin/reportSlice";
import { Autocomplete } from "../../../components/shared";
import { fetchUsers } from "../../../redux/Slice/admin/userSlice";
import DatePicker from "react-datepicker";
import { registerLocale } from "react-datepicker";
import { enGB } from "date-fns/locale/en-GB";
import AutoAsyncSelect from "../../../components/shared/CommonComponents/AutoAsyncSelect";


const LearnerProgressReport: React.FC = () => {
    const navigate = useNavigate();
    const dispatch = useAppDispatch();

    // Redux state
    const loggedInUserId = useSelector((state: RootState) => state.auth.id);
    const clientId: any = useSelector((state: RootState) => state.auth.clientId);
    const id: any = useSelector((state: RootState) => state.auth.id);
    const loading = useAppSelector((state: RootState) =>
        state.report.loading || state.oneTimeAssignments.loading
    );
    const activityList: any = useSelector(
        (state: RootState) => state.oneTimeAssignments.activities
    );

    const [standardProfileFieldsList, setStandardProfileFields] = useState<any[]>([]);

    // Local states
    const uersReportList: any = useSelector((state: RootState) => state.report.reportUserList);
    const standardProfileList: any = useSelector((state: RootState) => state.report.standardProfileField);
    const loadingReport = useAppSelector((state: RootState) => state.report.loading);

    const [usersReport, setUsersReport] = useState<GenerateReportPayload[]>([]);
    const [alert, setAlert] = useState<{
        type: "success" | "warning" | "error";
        message: string;
        duration?: string;
        autoClose?: boolean;
    } | null>(null);
    registerLocale("en-GB", enGB);

    const [activeAccordionSearch, setActiveAccordionSearch] = useState<string | null>(
        "accordionItemSearch"
    );
    const [activeAccordionAdvancedFilters, setActiveAccordionAdvancedFilters] =
        useState<string | null>("accordionItemAdvancedFilters");

    const [activityTypeList, setActivityTypeList] = useState<LookupTextValue[]>([]);
    const [appliedFilters, setAppliedFilters] = useState<any>(null);

    // Table-related states
    const [activities, setActivities] = useState<Activity[]>([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(5);
    const [selectedActivities, setSelectedActivities] = useState<string[]>([]);
    const [rows, setRows] = useState([{ id: Date.now(), select: "", text: "" }]);

    const [selectedActivityObjects, setSelectedActivityObjects] = useState<
        { activityId: string; clientId: string; createdById: string }[]
    >([]);

    const [includeInactiveUsers, setIncludeInactiveUsers] = useState(false);

    // Table-related states for Leaner Progress Report
    const [currentPageReport, setCurrentPageReport] = useState(1);
    const [pageSizeReport, setPageSizeReport] = useState(5);
    const [reportGenerated, setReportGenerated] = useState(false);

    //   const [learnerUsers, setlearnerUsers] = useState<any[]>([]);
    const [learnerUsers, setLearnerUsers] = useState<{ label: string; value: string }[]>([]);
    const [selectedUser, setSelectedUser] = useState<{ label: string; value: string } | null>(null);


    const initialFormState = {
        activityType: "",
        activityName: "",
        UserName: '',
    };

    const [formData, setFormData] = useState(initialFormState);
    const [formErrors, setFormErrors] = useState<Record<string, string>>({});

    const debounceTimer = useRef<NodeJS.Timeout | null>(null);

    const breadcrumbItems = [
        { iconClass: "fa-classic fa-solid fa-house", path: AdminPageRoutes.ADMIN_DASHBOARD.FULL_PATH },
        { label: "Learner Progress Report" },
    ];

    const pageTitle: string = "Learner Progress Report";
    document.title = pageTitle;

    /* ----------------------------
     * Accordion toggle handlers
     * ---------------------------- */
    const handleAccordionSearch = (accordionId: string) => {
        setActiveAccordionSearch(activeAccordionSearch === accordionId ? null : accordionId);
    };

    const handleAccordionAdvancedFilters = (accordionId: string) => {
        setActiveAccordionAdvancedFilters(
            activeAccordionAdvancedFilters === accordionId ? null : accordionId
        );
    };

    /* ----------------------------
     * Input handling & validation
     * ---------------------------- */
    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));

        const error = validateField(name, value);
        setFormErrors({
            ...formErrors,
            [name]: error,
        });
    };

    const validateField = (name: string, value: string | Array<string> | File | null): string => {
        if (typeof value === "string") {
            if (value.trim() && !isValidInput(value)) {
                return "Special characters like < > ' & are not allowed.";
            }
        }
        setFormErrors({});
        return "";
    };

    const isValidInput = (value: string): boolean => {
        const regx = /^[^'<>&]+$/;
        return regx.test(value);
    };

    /* ----------------------------
     * Data fetching
     * ---------------------------- */
    useEffect(() => {
        if (!clientId || !id) return;
        fetchActivityList(1, pageSize, appliedFilters);
        setCurrentPage(1);
    }, [clientId, id]);

    useEffect(() => {
        if (!standardProfileList?.length) return;
        // setProfileFields(standardProfileList)
        setStandardProfileFields(standardProfileList)
        setCurrentPage(1);
    }, [standardProfileList])


    useEffect(() => {
        setActivities(activityList?.activityAssignmentList || []);
    }, [activityList]);


    useEffect(() => {
        setUsersReport(uersReportList?.data || []);
    }, [uersReportList]);


    const mappedUsersReport = usersReport.map(u => ({
        ...u,
        id: u.UserId, // add id field
    }));


    const fetchActivityList = (pageIndex: number, pageSize: number, filters: any) => {
        if (debounceTimer.current) clearTimeout(debounceTimer.current);

        debounceTimer.current = setTimeout(() => {
            let finalActivityTypeId: number | undefined = 0;
            if (filters?.activityType) {
                finalActivityTypeId = getValueFromEnum(ACTIVITY_CONTENT_TYPE_ENUM, filters?.activityType);
            }

            const params = {
                clientId,
                listRange: {
                    pageIndex,
                    pageSize,
                },
                ActivityName: filters?.activityName,
                ActivityType: finalActivityTypeId,
            };
            dispatch(fetchActivitiesSlice(params));
        }, 300);
    };

    useEffect(() => {
        dispatch(fetchActivityTypesSlice(clientId))
            .then((response: any) => {
                if (response.meta.requestStatus === "fulfilled") {
                    setActivityTypeList(response.payload.assetTypeList);
                } else {
                    setAlert({ type: "error", message: "Activity type data fetch failed: " + response.msg });
                }
            })
            .catch((error) => {
                console.error("Activity type data fetch error:", error);
                setAlert({ type: "error", message: "An unexpected error occurred." });
            });
    }, [clientId]);


    /* ----------------------------
     * Table selection & pagination
     * ---------------------------- */
    const pushActivitiesIntoArrayofObject = (ids: string[]) => {
        const finalSelectedActivities = ids.map((aid) => ({
            activityId: aid,
            clientId: clientId,
            createdById: id,
        }));
        setSelectedActivityObjects(finalSelectedActivities);
    };

    const handleCheckboxChange = (activityId: string) => {
        const updated = selectedActivities.includes(activityId)
            ? selectedActivities.filter((x) => x !== activityId)
            : [...selectedActivities, activityId];

        setSelectedActivities(updated);
        pushActivitiesIntoArrayofObject(updated);
    };

    const handleSelectAll = (selectAll: boolean) => {
        const allIds = activities.map((data) => data.id);
        const updated = selectAll
            ? Array.from(new Set([...selectedActivities, ...allIds]))
            : selectedActivities.filter((id) => !allIds.includes(id));

        setSelectedActivities(updated);
        pushActivitiesIntoArrayofObject(updated);
    };

    const handlePageChangeActivity = (page: number, newPageSize?: number) => {
        if (newPageSize) setPageSize(newPageSize);
        setCurrentPage(page);

        let finalActivityTypeId: number | undefined = 0;
        if (appliedFilters?.activityType) {
            finalActivityTypeId = getValueFromEnum(ACTIVITY_CONTENT_TYPE_ENUM, appliedFilters?.activityType);
        }

        const params = {
            clientId: clientId,
            listRange: {
                pageIndex: page,
                pageSize: newPageSize || pageSize,
            },
            ActivityName: appliedFilters?.activityName,
            ActivityType: finalActivityTypeId,
            isActiveActivity: true,
        };
        dispatch(fetchActivitiesSlice(params));
    };

    const handleFilterSubmit = (filterData: any) => {
        if (!id || !clientId) return;
        // ✅ Validation: check if all filters are empty
        const isAllEmpty =
            (!filterData?.activityType || filterData.activityType.trim() === "") &&
            (!filterData?.activityName || filterData.activityName.trim() === "");

        if (isAllEmpty) {
            setAlert({
                type: "warning",
                message: "Please enter or select at least one filter to perform a search.",
                autoClose: true,
            });
            return;
        }
        setAppliedFilters(filterData);
        setCurrentPage(1);

        let finalActivityTypeId: number | undefined = 0;
        if (filterData?.activityType) {
            finalActivityTypeId = getValueFromEnum(ACTIVITY_CONTENT_TYPE_ENUM, filterData?.activityType);
        }

        const params = {
            clientId: clientId,
            listRange: {
                pageIndex: 1,
                pageSize: pageSize,
            },
            ActivityName: filterData?.activityName,
            ActivityType: finalActivityTypeId,
        } as const;

        dispatch(fetchActivitiesSlice(params))
            .then((response: any) => {
                if (response.meta.requestStatus === "fulfilled") {
                    setAlert({ type: "success", message: "Filter applied successfully." });
                } else {
                    setAlert({
                        type: "error",
                        message: "Failed to apply filter: " + response.payload?.msg || "Unknown error.",
                    });
                }
            })
            .catch((error) => {
                console.error("Error while applying filter:", error);
                setAlert({ type: "error", message: "An unexpected error occurred." });
            });
    };

    const handleClear = () => {
        setFormData(initialFormState);
        setFormErrors({});

        setAppliedFilters(null);
        setCurrentPage(1);

        const params = {
            clientId,
            listRange: {
                pageIndex: 1,
                pageSize,
            },
            ActivityName: "",
            ActivityType: 0,
        };

        dispatch(fetchActivitiesSlice(params));
    };


    /* ----------------------------
     * Utility
     * ---------------------------- */
    function getValueFromEnum<T extends Record<string, any>>(
        enumObj: T,
        key: string
    ): T[keyof T] | undefined {
        if (Object.prototype.hasOwnProperty.call(enumObj, key)) {
            return enumObj[key as keyof T];
        }
        return undefined;
    }

    /* ----------------------------
    * Advanced Filters - Dynamic Fields
    * ---------------------------- */

    // Add a new row
    const handleAddRow = () => {
        setRows([...rows, { id: Date.now(), select: "", text: "" }]);
    };

    // Delete a row
    const handleDeleteRow = (id: number) => {
        setRows(rows.filter((row) => row.id !== id));
    };

    // Handle input/select change
    const handleChange = (id: number, field: string, value: string) => {
        setRows(
            rows.map((row) =>
                row.id === id ? { ...row, [field]: value } : row
            )
        );
    };

    // Inside LearnerProgressReport component
    const [completionStatus, setCompletionStatus] = useState({
        all: false,
        notStarted: false,
        inProgress: false,
        completed: false,
        completedByAdmin: false,
        completedByProxy: false,
        pendingReview: false,
    });

    const handleCompletionToggle = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { value, name, checked } = e.target;

        if (name === "compStatusAll") {
            // Toggle ALL
            setCompletionStatus({
                all: checked,
                notStarted: checked,
                inProgress: checked,
                completed: checked,
                completedByAdmin: checked,
                completedByProxy: checked,
                pendingReview: checked,
            });
        } else {
            // Toggle one
            const updated = { ...completionStatus, [value]: checked };

            // Check if all individual statuses are ON
            const allChecked = Object.entries(updated)
                .filter(([key]) => key !== "all")
                .every(([, v]) => v === true);

            updated.all = allChecked;
            setCompletionStatus(updated);
        }
    };

    useEffect(() => {
        console.log("431")
        if (!clientId) return;
        dispatch(
            fetchStandardProfileFieldSlice({
                clientId,
                pageIndex: 1,
                pageSize: 200,
            })
        )
    }, [clientId]);


    /* ----------------------------
         * login Id auto search
         * ---------------------------- */

    // const handleUserSearch = async (keyword: string) => {
    //     if (!id || !clientId) return;

    //     const params = {
    //         id,
    //         clientId,
    //         listRange: {
    //             pageIndex: 0,
    //             pageSize,
    //             sortExpression: "LastModifiedDate desc",
    //             requestedById: undefined,
    //             //  requestedById: id
    //         },
    //         keyWord: keyword,
    //     };

    //     try {
    //         const response = await dispatch(fetchUsers(params)).unwrap();
    //         console.log("first", response.learnerList);
    //         setlearnerUsers(response.learnerList || []); // ✅ store locally
    //     } catch (error: any) {
    //         console.error("Error fetching users:", error);

    //         // Normalize error message
    //         const errorMessage =
    //             typeof error === "string"
    //                 ? error
    //                 : error?.msg || error?.message || "Failed to fetch users.";

    //         // If you want to show alert:
    //         setAlert({
    //             type: "error",
    //             message: errorMessage,
    //         });
    //     }
    // };

    // const autocompleteItems = learnerUsers.map((u: any) => ({
    //     label: u.name,
    //     value: u.id,
    // }));
    // const selectedUser = formData.UserName
    //     ? autocompleteItems.find(u => u.value === formData.UserName) || null
    //     : null;

    const handleUserSearch = async (keyword: string) => {
        if (!keyword) return;

        const params = {
            id,
            clientId,
            listRange: { pageIndex: 0, pageSize, sortExpression: "LastModifiedDate desc" },
            keyWord: keyword,
        };

        try {
            const response = await dispatch(fetchUsers(params)).unwrap();
            const mapped = (response.learnerList || []).map((u: any) => ({
                label: u.name,
                value: u.id,
            }));
            setLearnerUsers(mapped);
        } catch (err) {
            console.error(err);
        }
    };


    const handleAutocompleteSelect = (option: { label: string; value: string } | null) => {
        setSelectedUser(option);
        setFormData((prev) => ({ ...prev, UserName: option?.value || "" }));
    };


    /* ----------------------------
    * Date Filter
    * ---------------------------- */

    const [dateFilters, setDateFilters] = useState({
        assignmentFrom: null as Date | null,
        assignmentTo: null as Date | null,
        dueFrom: null as Date | null,
        dueTo: null as Date | null,
        expiryFrom: null as Date | null,
        expiryTo: null as Date | null,
        completionFrom: null as Date | null,
        completionTo: null as Date | null,
    });


    const handleDateChange = (field: keyof typeof dateFilters, value: Date | null) => {
        setDateFilters((prev) => ({
            ...prev,
            [field]: value,
        }));
    };

    const isValidDateRange = (from: Date | null, to: Date | null): boolean => {
        if (!from || !to) return true; // allow empty
        return to.getTime() >= from.getTime(); // To must be >= From
    };

    if (
        !isValidDateRange(dateFilters.assignmentFrom, dateFilters.assignmentTo) ||
        !isValidDateRange(dateFilters.dueFrom, dateFilters.dueTo) ||
        !isValidDateRange(dateFilters.expiryFrom, dateFilters.expiryTo) ||
        !isValidDateRange(dateFilters.completionFrom, dateFilters.completionTo)
    ) {
        setAlert({
            type: "error",
            message: "Please ensure each 'To Date' is greater than or equal to its corresponding 'From Date'.",
            autoClose: true,
        });
        return null;
    }
    /* ----------------------------
      * leaner progress report list
      * ---------------------------- */
    const buildReportPayload = (page: number, pageSize: number) => {
        // Build completion status
        let completionStatusValue: string | null = null;

        if (!completionStatus.all) {
            const selectedStatuses = Object.entries(completionStatus)
                .filter(([key, val]) => key !== "all" && val)
                .map(([key]) => {
                    switch (key) {
                        case "notStarted": return "Not Started";
                        case "inProgress": return "InProgress";
                        case "completed": return "Completed";
                        case "completedByAdmin": return "Completed By Admin";
                        case "completedByProxy": return "Completed By Proxy";
                        case "pendingReview": return "PendingReview";
                        default: return "";
                    }
                })
                .filter(Boolean);

            completionStatusValue = selectedStatuses.length > 0 ? selectedStatuses.join(",") : null;
        }

        const standardFields = rows
            .filter(r => r.select && r.text)
            .map(r => `${r.select}##${r.text}`)
            .join('@@');

        return {
            ClientId: clientId,
            PageIndex: page,
            PageSize: pageSize,
            SortExpression: "ActivityName",
            SortDirection: "ASC",
            ActivityName: uniqueActivities.join(","),
            ActivityType: formData.activityType || null,
            CompletionStatus: completionStatusValue,
            UserName: formData.UserName || null,
            AssignmentDateFrom: formatDateForPayload(dateFilters.assignmentFrom),
            AssignmentDateTo: formatDateForPayload(dateFilters.assignmentTo),
            DueDateFrom: formatDateForPayload(dateFilters.dueFrom),
            DueDateTo: formatDateForPayload(dateFilters.dueTo),
            ExpiryDateFrom: formatDateForPayload(dateFilters.expiryFrom),
            ExpiryDateTo: formatDateForPayload(dateFilters.expiryTo),
            CompletionDateFrom: formatDateForPayload(dateFilters.completionFrom),
            CompletionDateTo: formatDateForPayload(dateFilters.completionTo),
            IncludeInactiveUsers: includeInactiveUsers,
            Attempt: "First",
            BusinessRuleId: null,
            StandardFields: standardFields || null,
            CustomFields: null,
            UserId: id
        };
    };

    const handlePageChangeUsersReport = (page: number, newPageSize?: number) => {
        if (newPageSize) setPageSizeReport(newPageSize);
        setCurrentPageReport(page);

        const payload = buildReportPayload(page, newPageSize || pageSizeReport);
        dispatch(generateReportSlice(payload));
    };


    /* ----------------------------
            * generate report
            * ---------------------------- */

    const formatDateForPayload = (date: Date | null): string | null => {
        if (!date) return null;

        const yyyy = date.getFullYear();
        const mm = String(date.getMonth() + 1).padStart(2, '0'); // Months are 0-based
        const dd = String(date.getDate()).padStart(2, '0');
        const hh = String(date.getHours()).padStart(2, '0');
        const mi = String(date.getMinutes()).padStart(2, '0');
        const ss = String(date.getSeconds()).padStart(2, '0');

        return `${yyyy}-${mm}-${dd} ${hh}:${mi}:${ss}`;
    };

    const uniqueActivities = selectedActivities.filter(
        (value, index, self) => self.indexOf(value) === index
    );

    const handleGenerateReport = () => {
        // if (uniqueActivities.length === 0) {
        //     setAlert({ type: "warning", message: "Please select at least one activity." });
        //     return;
        // }

        const payload = buildReportPayload(1, pageSizeReport);
        console.log("Generate Report Payload", payload);
        dispatch(generateReportSlice(payload));
        setReportGenerated(true);
    };


    /* ----------------------------
   * include Inactive Users
   * ---------------------------- */

    const handleToggleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setIncludeInactiveUsers(e.target.checked); // true if checked, false otherwise
    };


    /* ----------------------------
     * Render
     * ---------------------------- */
    return (
        <>
            {/* Loading Overlay */}
            {loading && (
                <div className="yp-loading-overlay">
                    <div className="yp-spinner">
                        <div className="spinner-border" role="status">
                            <span className="visually-hidden">Loading...</span>
                        </div>
                    </div>
                </div>
            )}

            {/* Breadcrumb */}
            <div className="yp-page-title-button-section" id="yp-page-title-breadcrumb-section">
                <div className="yp-page-title-breadcrumb">
                    <div className="yp-page-title">{pageTitle}</div>
                    <CustomBreadcrumb items={breadcrumbItems} />
                </div>
            </div>

            {/* Search Accordion */}
            <div className="accordion yp-accordion" id="accordionSearch">
                <div className="accordion-item">
                    <h2 className="accordion-header">
                        <button
                            className={`accordion-button ${activeAccordionSearch !== "accordionItemSearch" ? "collapsed" : ""
                                }`}
                            type="button"
                            onClick={() => handleAccordionSearch("accordionItemSearch")}
                            aria-expanded={activeAccordionSearch === "accordionItemSearch"}
                            aria-controls="accordionItemSearch" >
                            Search
                        </button>
                    </h2>
                    <div
                        id="accordionItemSearch"
                        className={`accordion-collapse collapse ${activeAccordionSearch === "accordionItemSearch" ? "show" : ""}`}
                        data-bs-parent="#accordionSearch">
                        <div className="accordion-body">
                            {/* Filters */}
                            <div className="row">
                                <div className="col-12 col-md-6 col-lg-6">
                                    <div className="form-group">
                                        <div className="yp-form-control-wrapper">
                                            <select
                                                className={`form-control yp-form-control ${formErrors.activityType ? "is-invalid" : ""
                                                    }`}
                                                name="activityType"
                                                value={formData.activityType}
                                                onChange={handleInputChange}
                                            >
                                                <option value="">Select</option>
                                                {activityTypeList.map((type) => (
                                                    <option key={type.lookupValue} value={type.lookupValue}>
                                                        {type.lookupText}
                                                    </option>
                                                ))}
                                            </select>
                                            <label className="form-label">Activity Type</label>
                                        </div>
                                    </div>
                                </div>

                                <div className="col-12 col-md-6 col-lg-6">
                                    <div className="form-group">
                                        <div className="yp-form-control-wrapper">
                                            <input
                                                type="text"
                                                className={`form-control yp-form-control ${formErrors.activityName ? "is-invalid" : ""}`}
                                                name="activityName"
                                                id="activityName"
                                                value={formData.activityName}
                                                onChange={handleInputChange}
                                                placeholder=""
                                            />
                                            <label htmlFor="activityName" className="form-label">Activity Name</label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            {/* Buttons */}
                            <div className="row">
                                <div className="col-12">
                                    <div className="yp-form-button-row mt-0">
                                        <button
                                            type="button"
                                            className="btn btn-primary"
                                            onClick={() => handleFilterSubmit(formData)}
                                        >
                                            Search Activities
                                        </button>

                                        <a href="#" className="yp-link yp-link-primary yp-link-14-300" onClick={handleClear}>Clear</a>
                                        {/* <button type="reset" className="btn btn-secondary" onClick={handleClear}>Clear</button> */}
                                    </div>
                                </div>
                            </div>

                            {/* Info */}
                            <div className="my-4">
                                <div>
                                    <span className="me-2">
                                        <i className="fa-solid fa-info-circle yp-color-primary"></i>
                                    </span>
                                    <span className="yp-text-13-400 yp-color-565656">
                                        Select one or more activities from the following list.
                                    </span>
                                </div>
                            </div>

                            {/* Table */}
                            <div className="yp-custom-table-section yp-custom-table-section-inside-card">
                                <CommonTable
                                    type={COMMON_TABLE_TYPE.REPORT_LEARNER_ACTIVITY_PROGRESS}
                                    tableConfig={REPORT_LEARNER_PROGRESS_ACTIVITY_TABLE_CONFIG}
                                    filterComponent={<></>}
                                    data={activities}
                                    onPageChange={handlePageChangeActivity}
                                    currentPage={currentPage}
                                    totalRecords={
                                        Array.isArray(activityList?.activityAssignmentList) &&
                                            activityList.activityAssignmentList.length > 0
                                            ? activityList.activityAssignmentList[0]?.listRange?.totalRows
                                            : 0
                                    }
                                    pageSize={pageSize}
                                    selectedUsers={selectedActivities}
                                    handleCheckboxChange={handleCheckboxChange}
                                    handleSelectAll={handleSelectAll}
                                />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            {/* Advanced Filters Accordion */}
            <div className="accordion yp-accordion mb-0" id="accordionAdvancedFilters">
                <div className="accordion-item">
                    <h2 className="accordion-header">
                        <button
                            className={`accordion-button ${activeAccordionAdvancedFilters !== 'accordionItemAdvancedFilters' ? 'collapsed' : ''}`}
                            type="button"
                            onClick={() => handleAccordionAdvancedFilters('accordionItemAdvancedFilters')}
                            aria-expanded={activeAccordionAdvancedFilters === 'accordionItemAdvancedFilters'}
                            aria-controls="accordionItemAdvancedFilters"
                        >
                            Advanced Filters <span className="yp-accordion-header-gray-text">(Optional)</span>
                        </button>
                    </h2>
                    <div id="accordionItemAdvancedFilters"
                        className={`accordion-collapse collapse ${activeAccordionAdvancedFilters === 'accordionItemAdvancedFilters' ? 'show' : ''}`}
                        data-bs-parent="#accordionAdvancedFilters">
                        <div className="accordion-body">
                            <div className="row">
                                <div className="col-12 col-md-6 col-lg-6">
                                    <div className="yp-form-control-with-icon yp-form-control-with-icon-right-side">
                                        <div className="form-group">
                                            {/* <Autocomplete
                                                items={autocompleteItems || []}
                                                handleSelectChange={handleAutocomple}
                                                placeholder="Login ID"
                                                title="Type Login ID..."
                                                onSearch={handleUserSearch}
                                                value={selectedUser}
                                            /> */}
                                            <AutoAsyncSelect
                                                items={learnerUsers}
                                                value={selectedUser}
                                                placeholder="Login ID"
                                                title="Type Login ID..."
                                                onSearch={handleUserSearch}
                                                handleSelectChange={handleAutocompleteSelect}
                                            />
                                        </div>
                                    </div>
                                </div>
                                <div className="col-12 col-md-6 col-lg-6">
                                    <div className="form-group">
                                        <div className="yp-form-control-wrapper">
                                            <select
                                                className={`form-control yp-form-control`}
                                                name=""
                                                id=""
                                                disabled>
                                                <option value="">Select</option>
                                                <option value="1">Option 1</option>
                                                <option value="2">Option 2</option>
                                                <option value="3">Option 3</option>
                                            </select>
                                            <label className="form-label">User Group</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr className="yp-hr yp-hr-30-30 mt-2" />

                            <div className="yp-sub-card">
                                <div className="yp-sub-card-head">
                                    <span className="yp-color-dark-purple">Standard Profile Field Selection</span>
                                </div>
                                <div className="yp-sub-card-body">
                                    {rows.map((row, index) => (
                                        <div className="yp-add-remove-row" key={row.id}>
                                            <div className="row">
                                                <div className="col-12 col-md-3">
                                                    <div className="form-group">
                                                        <div className="yp-form-control-wrapper">
                                                            <select
                                                                className="form-control yp-form-control"
                                                                value={row.select}
                                                                onChange={(e) =>
                                                                    handleChange(row.id, "select", e.target.value)
                                                                }
                                                            >
                                                                <option value="">Select</option>
                                                                {standardProfileFieldsList?.map((field: any) => (
                                                                    <option key={field.fieldName} value={field.fieldName}>
                                                                        {field.fieldName}
                                                                    </option>
                                                                ))}
                                                            </select>
                                                            <label className="form-label">Select Field</label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div className="col-12 col-md-6">
                                                    <div className="form-group">
                                                        <div className="yp-form-control-wrapper">
                                                            <input
                                                                type="text"
                                                                className="form-control yp-form-control"
                                                                value={row.text}
                                                                onChange={(e) =>
                                                                    handleChange(row.id, "text", e.target.value)
                                                                }
                                                            />
                                                            <label className="form-label">Type text here...</label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div className="col-12 col-md-3">
                                                    <div className="yp-add-remove-row-btns">
                                                        <button
                                                            type="button"
                                                            className="btn btn-circle btn-red btn-circle-text-show-for-mobile"
                                                            onClick={() => handleDeleteRow(row.id)}
                                                            disabled={rows.length === 1} // keep at least one row
                                                        >
                                                            <i className="fa-solid fa-trash-can"></i>
                                                            <span className="btn-text">Delete</span>
                                                        </button>

                                                        {index === rows.length - 1 && ( // show Add only on last row
                                                            <button
                                                                type="button"
                                                                className="btn btn-circle btn-green btn-circle-text-show-for-mobile"
                                                                onClick={handleAddRow}
                                                            >
                                                                <i className="fa-solid fa-plus"></i>
                                                                <span className="btn-text">Add More</span>
                                                            </button>
                                                        )}
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    ))}

                                    {/* debug output */}
                                    {/* <pre>{JSON.stringify(rows, null, 2)}</pre> */}

                                </div>
                            </div>
                            <hr className="yp-hr yp-hr-30-30" />

                            <div className="yp-text-14-500 yp-color-primary mb-4">Completion Status:</div>
                            <div className="d-flex flex-wrap column-gap-5 flex-column flex-md-row yp-learner-progress-report-completion-status-section">
                                <div>
                                    <div className="form-group">
                                        <div className="yp-form-control-wrapper">
                                            <div className="form-check form-switch">
                                                <input
                                                    className="form-check-input"
                                                    type="checkbox"
                                                    role="switch"
                                                    name="compStatusAll"
                                                    id="compStatusAll"
                                                    value="all"
                                                    checked={completionStatus.all}
                                                    onChange={handleCompletionToggle} />
                                                <label className="form-check-label" htmlFor="compStatusAll"><span>All</span></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div>
                                    <div className="form-group">
                                        <div className="yp-form-control-wrapper">
                                            <div className="form-check form-switch">
                                                <input
                                                    className="form-check-input"
                                                    type="checkbox"
                                                    role="switch"
                                                    name="compStatus"
                                                    id="compStatusNotStarted"
                                                    value="notStarted"
                                                    checked={completionStatus.notStarted}
                                                    onChange={handleCompletionToggle} />
                                                <label className="form-check-label" htmlFor="compStatusNotStarted"><span>Not Started</span></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="form-group">
                                        <div className="yp-form-control-wrapper">
                                            <div className="form-check form-switch">
                                                <input
                                                    className="form-check-input"
                                                    type="checkbox"
                                                    role="switch"
                                                    name="compStatus"
                                                    id="compStatusInProgress"
                                                    value="inProgress"
                                                    checked={completionStatus.inProgress}
                                                    onChange={handleCompletionToggle} />
                                                <label className="form-check-label" htmlFor="compStatusInProgress"><span>In Progress</span></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="form-group">
                                        <div className="yp-form-control-wrapper">
                                            <div className="form-check form-switch">
                                                <input
                                                    className="form-check-input"
                                                    type="checkbox"
                                                    role="switch"
                                                    name="compStatus"
                                                    id="compStatusCompleted"
                                                    value="completed"
                                                    checked={completionStatus.completed}
                                                    onChange={handleCompletionToggle} />
                                                <label className="form-check-label" htmlFor="compStatusCompleted"><span>Completed</span></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div>
                                    <div className="form-group">
                                        <div className="yp-form-control-wrapper">
                                            <div className="form-check form-switch">
                                                <input
                                                    className="form-check-input"
                                                    type="checkbox"
                                                    role="switch"
                                                    name="compStatus"
                                                    id="compStatusCompletedByAdmin"
                                                    value="completedByAdmin"
                                                    checked={completionStatus.completedByAdmin}
                                                    onChange={handleCompletionToggle} />
                                                <label className="form-check-label" htmlFor="compStatusCompletedByAdmin"><span>Completed By Admin</span></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="form-group">
                                        <div className="yp-form-control-wrapper">
                                            <div className="form-check form-switch">
                                                <input
                                                    className="form-check-input"
                                                    type="checkbox"
                                                    role="switch"
                                                    name="compStatus"
                                                    id="compStatusCompletedByProxy"
                                                    value="completedByProxy"
                                                    checked={completionStatus.completedByProxy}
                                                    onChange={handleCompletionToggle} />
                                                <label className="form-check-label" htmlFor="compStatusCompletedByProxy"><span>Completed By Proxy</span></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="form-group">
                                        <div className="yp-form-control-wrapper">
                                            <div className="form-check form-switch">
                                                <input
                                                    className="form-check-input"
                                                    type="checkbox"
                                                    role="switch"
                                                    name="compStatus"
                                                    id="compStatusPendingReview"
                                                    value="pendingReview"
                                                    checked={completionStatus.pendingReview}
                                                    onChange={handleCompletionToggle} />
                                                <label className="form-check-label" htmlFor="compStatusPendingReview"><span>Pending Review</span></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr className="yp-hr yp-hr-30-30 mt-2" />

                            <div className="yp-text-14-500 yp-color-primary mb-4">Date Filters:</div>
                            <div className="yp-reports-date-filters-rows">
                                <div className="yp-reports-date-filters-row">
                                    <div className="d-flex flex-wrap gap-2 flex-column flex-md-row align-items-md-center">
                                        <div className="yp-width-120-px"><span className="yp-text-13-500 yp-color-primary">Assignment Date:</span></div>
                                        <div className="d-flex flex-wrap gap-4 row-gap-2 flex-column flex-sm-row align-items-sm-center">
                                            <div className="d-flex flex-wrap gap-2 row-gap-1 flex-column flex-sm-row align-items-sm-center">
                                                <div><span className="yp-text-13-400 yp-color-565656">From:</span></div>
                                                <div className="yp-width-175-px">
                                                    <div className="yp-form-control-wrapper">
                                                        <DatePicker
                                                            selected={dateFilters.assignmentFrom}
                                                            onChange={(date) => handleDateChange("assignmentFrom", date)}
                                                            placeholderText="DD/MM/YYYY"
                                                            dateFormat="dd/MM/yyyy"
                                                            locale="en-GB"
                                                            className="form-control yp-form-control yp-form-control-sm"
                                                            showIcon
                                                            toggleCalendarOnIconClick
                                                            icon="fa-solid fa-calendar-days"
                                                            maxDate={dateFilters.assignmentTo || undefined}
                                                        />
                                                    </div>
                                                </div>
                                            </div>
                                            <div className="d-flex flex-wrap gap-2 row-gap-1 flex-column flex-sm-row align-items-sm-center">
                                                <div><span className="yp-text-13-400 yp-color-565656">To:</span></div>
                                                <div className="yp-width-175-px">
                                                    <div className="yp-form-control-wrapper">
                                                        <DatePicker
                                                            selected={dateFilters.assignmentTo}
                                                            onChange={(date) => handleDateChange("assignmentTo", date)}
                                                            placeholderText="DD/MM/YYYY"
                                                            dateFormat="dd/MM/yyyy"
                                                            locale="en-GB"
                                                            className="form-control yp-form-control yp-form-control-sm"
                                                            showIcon
                                                            toggleCalendarOnIconClick
                                                            icon="fa-solid fa-calendar-days"
                                                            minDate={dateFilters.assignmentFrom || undefined}
                                                        />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div className="yp-reports-date-filters-row">
                                    <div className="d-flex flex-wrap gap-2 flex-column flex-md-row align-items-md-center">
                                        <div className="yp-width-120-px"><span className="yp-text-13-500 yp-color-primary">Due Date:</span></div>
                                        <div className="d-flex flex-wrap gap-4 row-gap-2 flex-column flex-sm-row align-items-sm-center">
                                            <div className="d-flex flex-wrap gap-2 row-gap-1 flex-column flex-sm-row align-items-sm-center">
                                                <div><span className="yp-text-13-400 yp-color-565656">From:</span></div>
                                                <div className="yp-width-175-px">
                                                    <div className="yp-form-control-wrapper">

                                                        <DatePicker
                                                            selected={dateFilters.dueFrom}
                                                            onChange={(date) => handleDateChange("dueFrom", date)}
                                                            placeholderText="DD/MM/YYYY"
                                                            dateFormat="dd/MM/yyyy"
                                                            locale="en-GB"
                                                            className="form-control yp-form-control yp-form-control-sm"
                                                            showIcon
                                                            toggleCalendarOnIconClick
                                                            icon="fa-solid fa-calendar-days"
                                                            maxDate={dateFilters.dueTo || undefined}
                                                        />
                                                    </div>
                                                </div>
                                            </div>
                                            <div className="d-flex flex-wrap gap-2 row-gap-1 flex-column flex-sm-row align-items-sm-center">
                                                <div><span className="yp-text-13-400 yp-color-565656">To:</span></div>
                                                <div className="yp-width-175-px">
                                                    <div className="yp-form-control-wrapper">
                                                        <DatePicker
                                                            selected={dateFilters.dueTo}
                                                            onChange={(date) => handleDateChange("dueTo", date)}
                                                            placeholderText="DD/MM/YYYY"
                                                            dateFormat="dd/MM/yyyy"
                                                            locale="en-GB"
                                                            className="form-control yp-form-control yp-form-control-sm"
                                                            showIcon
                                                            toggleCalendarOnIconClick
                                                            icon="fa-solid fa-calendar-days"
                                                            minDate={dateFilters.dueFrom || undefined}
                                                        />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div className="yp-reports-date-filters-row">
                                    <div className="d-flex flex-wrap gap-2 flex-column flex-md-row align-items-md-center">
                                        <div className="yp-width-120-px"><span className="yp-text-13-500 yp-color-primary">Expiry Date:</span></div>
                                        <div className="d-flex flex-wrap gap-4 row-gap-2 flex-column flex-sm-row align-items-sm-center">
                                            <div className="d-flex flex-wrap gap-2 row-gap-1 flex-column flex-sm-row align-items-sm-center">
                                                <div><span className="yp-text-13-400 yp-color-565656">From:</span></div>
                                                <div className="yp-width-175-px">
                                                    <div className="yp-form-control-wrapper">
                                                        <DatePicker
                                                            selected={dateFilters.expiryFrom}
                                                            onChange={(date) => handleDateChange("expiryFrom", date)}
                                                            placeholderText="DD/MM/YYYY"
                                                            dateFormat="dd/MM/yyyy"
                                                            locale="en-GB"
                                                            className="form-control yp-form-control yp-form-control-sm"
                                                            showIcon
                                                            toggleCalendarOnIconClick
                                                            icon="fa-solid fa-calendar-days"
                                                            maxDate={dateFilters.expiryTo || undefined}
                                                        />
                                                    </div>
                                                </div>
                                            </div>
                                            <div className="d-flex flex-wrap gap-2 row-gap-1 flex-column flex-sm-row align-items-sm-center">
                                                <div><span className="yp-text-13-400 yp-color-565656">To:</span></div>
                                                <div className="yp-width-175-px">
                                                    <div className="yp-form-control-wrapper">
                                                        <DatePicker
                                                            selected={dateFilters.expiryTo}
                                                            onChange={(date) => handleDateChange("expiryTo", date)}
                                                            placeholderText="DD/MM/YYYY"
                                                            dateFormat="dd/MM/yyyy"
                                                            locale="en-GB"
                                                            className="form-control yp-form-control yp-form-control-sm"
                                                            showIcon
                                                            toggleCalendarOnIconClick
                                                            icon="fa-solid fa-calendar-days"
                                                            minDate={dateFilters.expiryFrom || undefined}
                                                        />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div className="yp-reports-date-filters-row">
                                    <div className="d-flex flex-wrap gap-2 flex-column flex-md-row align-items-md-center">
                                        <div className="yp-width-120-px"><span className="yp-text-13-500 yp-color-primary">Completion Date:</span></div>
                                        <div className="d-flex flex-wrap gap-4 row-gap-2 flex-column flex-sm-row align-items-sm-center">
                                            <div className="d-flex flex-wrap gap-2 row-gap-1 flex-column flex-sm-row align-items-sm-center">
                                                <div><span className="yp-text-13-400 yp-color-565656">From:</span></div>
                                                <div className="yp-width-175-px">
                                                    <div className="yp-form-control-wrapper">
                                                        <DatePicker
                                                            selected={dateFilters.completionFrom}
                                                            onChange={(date) => handleDateChange("completionFrom", date)}
                                                            placeholderText="DD/MM/YYYY"
                                                            dateFormat="dd/MM/yyyy"
                                                            locale="en-GB"
                                                            className="form-control yp-form-control yp-form-control-sm"
                                                            showIcon
                                                            toggleCalendarOnIconClick
                                                            icon="fa-solid fa-calendar-days"
                                                            maxDate={dateFilters.completionTo || undefined}
                                                        />
                                                    </div>
                                                </div>
                                            </div>
                                            <div className="d-flex flex-wrap gap-2 row-gap-1 flex-column flex-sm-row align-items-sm-center">
                                                <div><span className="yp-text-13-400 yp-color-565656">To:</span></div>
                                                <div className="yp-width-175-px">
                                                    <div className="yp-form-control-wrapper">
                                                        <DatePicker
                                                            selected={dateFilters.completionTo}
                                                            onChange={(date) => handleDateChange("completionTo", date)}
                                                            placeholderText="DD/MM/YYYY"
                                                            dateFormat="dd/MM/yyyy"
                                                            locale="en-GB"
                                                            className="form-control yp-form-control yp-form-control-sm"
                                                            showIcon
                                                            toggleCalendarOnIconClick
                                                            icon="fa-solid fa-calendar-days"
                                                            minDate={dateFilters.completionFrom || undefined}
                                                        />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr className="yp-hr yp-hr-30-30" />

                            <div className="form-group mb-3">
                                <div className="yp-form-control-wrapper">
                                    <div className="form-check form-switch">
                                        <input
                                            className="form-check-input"
                                            type="checkbox"
                                            role="switch"
                                            id="includeInactiveUsers"
                                            checked={includeInactiveUsers}
                                            onChange={handleToggleChange}
                                        />
                                        <label className="form-check-label" htmlFor="includeInactiveUsers"><span>Include inactive users</span></label>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            {/* Generate Report Button */}
            <div className="row my-4">
                <div className="col-12">
                    <div className="yp-form-button-row">
                        <button type="button" className="btn btn-primary" onClick={handleGenerateReport}>
                            Generate Report
                        </button>
                    </div>
                </div>
            </div>

            {/* Common list here */}
            {reportGenerated && !loadingReport && (
                <div className="yp-card p-0 pb-3">
                    <div className="yp-custom-table-section">
                        {mappedUsersReport.length > 0 ? (
                            <CommonTable
                                type={COMMON_TABLE_TYPE.REPORT_LEARNER_PROGRESS}
                                tableConfig={REPORT_LEARNER_PROGRESS_TABLE_CONFIG}
                                data={mappedUsersReport}
                                onPageChange={handlePageChangeUsersReport}
                                currentPage={currentPageReport}
                                pageSize={pageSizeReport}
                                totalRecords={uersReportList?.totalRecords || 0}
                                handleCheckboxChange={handleCheckboxChange}
                                handleSelectAll={handleSelectAll}
                            // getSortedList={getSortedList}
                            />
                        ) : (
                            <p className="text-center py-3">
                                No Learner Progress Report found.
                            </p>
                        )}
                    </div>
                </div>
            )}



            {/* Alerts */}
            {alert && (
                <AlertMessage
                    type={alert.type}
                    message={alert.message}
                    duration={alert.duration}
                    autoClose={alert.autoClose}
                    onClose={() => setAlert(null)}
                />
            )}
        </>
    );
};

export default LearnerProgressReport;
