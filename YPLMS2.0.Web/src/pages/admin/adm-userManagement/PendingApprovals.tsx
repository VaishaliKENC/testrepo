import React, { useCallback, useEffect, useMemo, useRef, useState } from "react";
import { debounce } from "lodash";
import {
  IApproveRejectPayload,
  User,
} from "../../../Types/commonTableTypes";
import { useAppDispatch, useAppSelector } from "../../../hooks";
import {
  approveRejectUserSlice,
  fetchUsersPendingApprovalsSlice,
  fetchPendingApprovalsUserDetailsSlice
} from "../../../redux/Slice/admin/userSlice";
import { useSelector } from "react-redux";
import { RootState } from "../../../redux/store";
import { useNavigate } from "react-router-dom";
import { AdminPageRoutes } from "../../../utils/Constants/Admin_PageRoutes";
import {
  ACTION_BUTTON_ENUM,
  COMMON_TABLE_TYPE,
} from "../../../utils/Constants/Enums";
import PendingApprovalsFilter from "./PendingApprovalsFilter";
import CommonTable from "../../../components/shared/CommonComponents/CommonTable";
import ConfirmationModal from "../../../components/shared/ConfirmationModal";
import CustomBreadcrumb from "../../../components/shared/CustomBreadcrumb";
import AlertMessage from "../../../components/shared/AlertMessage";
import * as bootstrap from 'bootstrap';
import { PENDING_APPROVALS_TABLE_CONFIG } from "../../../utils/Constants/tableConfig";
import { AnyARecord } from "dns";

const PendingApprovals: React.FC = () => {
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const clientId: any = useSelector((state: RootState) => state.auth.clientId);
    const id: any = useSelector((state: RootState) => state.auth.id);
    const userList: any = useSelector((state: RootState) => state.user.usersPendingApprovals);
    const { loading } = useAppSelector((state: RootState) => state.user);


    // Local states
    const [users, setUsers] = useState<User[]>([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(5);
    const [selectedUsers, setSelectedUsers] = useState<string[]>([]);
    const [showModal, setShowModal] = useState(false);
    const [searchMode, setSearchMode] = useState<"keyword" | "filter" | null>(
        null
    );

    const [isFilterVisible, setFilterVisible] = useState(false);
    // Action buttons configuration
    const actionButtonList = [
        {
            type: "button",
            className: "btn btn-primary",
            buttonName: "Approve",
            actionType: ACTION_BUTTON_ENUM?.APPROVE,
            disabled: users.length === 0,
        },
        {
            type: "button",
            className: "btn btn-secondary",
            buttonName: "Reject",
            actionType: ACTION_BUTTON_ENUM?.REJECT,
            disabled: users.length === 0,
        },
    ];

    const [modalConfig, setModalConfig] = useState({
        title: "",
        message: "",
        primaryBtnText: "",
    });
    const [modalAlertMessageConfig, setModalAlertMessageConfig] = useState<{
        type: "success" | "error";
        message: string;
        duration?: string;
        autoClose?: boolean;
    }>({
        type: "success",
        message: "",
        duration: "",
        autoClose: false,
    });
    const [onConfirmAction, setOnConfirmAction] = useState<() => void>(
        () => () => {}
    );

    const [appliedFilters, setAppliedFilters] = useState<any>(null);
    const [isSubmitting, setIsSubmitting] = useState(false);
    // const [searchKeyword, setSearchKeyword] = useState<string>('');
    const [searchKeyword, setSearchKeyword] = useState("");
    
    const [alert, setAlert] = useState<{
        type: "success" | "warning" | "error";
        message: string;
        duration?: string;
        autoClose?: boolean;
    } | null>(null);

    /** Breadcrumb navigation items*/
    const breadcrumbItems = [
        {
            iconClass: "fa-classic fa-solid fa-house",
            path: AdminPageRoutes.ADMIN_DASHBOARD.FULL_PATH,
        },
        { label: "Pending Approvals" },
    ];
    const pageTitle: string = "Pending Approvals";
    document.title = pageTitle;

    /** Fetch the initial pending approval user list when the component mounts. */
    useEffect(() => {
        if (!id || !clientId) return;
        fetchUsersListPendingApprovals(1, pageSize, appliedFilters);
    }, [id, clientId]);

    /** Update the local user state when userList list changes. */
    useEffect(() => {
        setUsers(userList?.learnerList || []);
    }, [userList]);

    /** Fetch user list with pagination and filters. */
    const fetchUsersListPendingApprovals = (pageIndex: number, pageSize: number, filters: any) => {
        const params = {
            id,
            clientId,
            listRange: {
                pageIndex,
                pageSize,
                sortExpression: "LastModifiedDate desc",
                requestedById: undefined,
            },
            keyWord: "",
            userCriteria: filters || {},
        };
        dispatch(fetchUsersPendingApprovalsSlice(params));
    };

    /** Clear applied filters and reset the user list.  */
    const handleClearFilters = (
        sortExpression: string = "LastModifiedDate desc"
    ) => {
        setAppliedFilters(null); // Clear applied filters
        setCurrentPage(1); // Reset pagination
        setSearchMode(null); // ✅ Enable keyword search
        const params = {
            id,
            clientId,
            listRange: {
                pageIndex: 1,
                pageSize,
                sortExpression: sortExpression,
                // requestedById: id
                requestedById: undefined,
            },
            keyWord: "",
            userCriteria: {}, // ✅ Clear criteria
        };

        dispatch(fetchUsersPendingApprovalsSlice(params)); // Refetch users with empty filters
    };

    /** Handle individual asset file selection via radio.*/
    const handleSelectChange = (data: string) => {
        setSelectedUsers([]);
        setSelectedUsers([data]);
    };

    /** Handle Activate button click.  */
    const handleApprove = () => {
        if (selectedUsers.length !== 1) {
            setAlert({ type: "warning", message: "Please select exactly one record." });
            return;
        }

        const selectedId = selectedUsers[0];
        const selectedUser = users.find(lp => lp.id === selectedId);

        if (!selectedUser) {
            setAlert({ type: "error", message: "User not found." });
            return;
        }

        if (selectedUser.userStatus != "Pending") {
            setAlert({ type: "warning", message: "Please select only pending record." });
            return;
        }

        const confirmApprove = () => {
            const payload = {
                id: selectedUser.id,
                signUpId: selectedUser.signUpId,
                clientId: clientId,
                lastModifiedBy: id,
                userStatus: "Approved",
            };
            onApprove(payload);
        };

        /** Approve selected users.   */
        const onApprove = (usersToApprove: IApproveRejectPayload) => {
          setIsSubmitting(true); //  Start loader
          dispatch(approveRejectUserSlice(usersToApprove))
            .then(() => {
              setSelectedUsers([]);
              setCurrentPage(1);
              refreshUserList(1);
              setModalAlertMessageConfig({
                type: "success",
                message: "User approved successfully!",
              });
            })
            .catch((error) => {
              setModalAlertMessageConfig({
                type: "error",
                message: "Error approving user.",
              });
            })
            .finally(() => {
              setIsSubmitting(false); //  Stop loader
            });
        };
        setModalConfig({
          title: "Approve User Confirmation",
          message: "Are you sure you want to approve the selected users?",
          primaryBtnText: "Approve",
        });
        setOnConfirmAction(() => confirmApprove);
        setShowModal(true);
    };

    /** Handle Reject button click.  */
    const handleReject = () => {
        if (selectedUsers.length !== 1) {
            setAlert({ type: "warning", message: "Please select exactly one record." });
            return;
        }

        const selectedId = selectedUsers[0];
        const selectedUser = users.find(lp => lp.id === selectedId);

        if (!selectedUser) {
            setAlert({ type: "error", message: "User not found." });
            return;
        }

        if (selectedUser.userStatus != "Pending") {
            setAlert({ type: "warning", message: "Please select only pending record." });
            return;
        }

        const confirmReject = () => {
            const payload = {
                id: selectedUser.id,
                signUpId: selectedUser.signUpId,
                clientId: clientId,
                lastModifiedBy: id,
                userStatus: "Rejected",
            };
            onReject(payload);
        };       
    

        /**
        * Reject selected users.
        */
        const onReject = (usersToReject: IApproveRejectPayload) => {
            setIsSubmitting(true); //  Start loader
            dispatch(approveRejectUserSlice(usersToReject))
            .then(() => {
                setSelectedUsers([]);
                setCurrentPage(1);
                refreshUserList(1);
                setModalAlertMessageConfig({
                    type: "success",
                    message: "User rejected successfully!",
                });
            })
            .catch((error) => {
                setModalAlertMessageConfig({
                    type: "error",
                    message: "Error rejecting user.",
                });
            })
            .finally(() => {
                setIsSubmitting(false); //  Stop loader
            });
        };

        setModalConfig({
            title: "Reject User Confirmation",
            message: "Are you sure you want to reject this user?",
            primaryBtnText: "Reject",
        });
        setOnConfirmAction(() => confirmReject);
        setShowModal(true);
    };



    /** Handle user action button clicks (Approve/Reject). */
    const handleActionButtonClick = (actionType: string) => {
        if (actionType === ACTION_BUTTON_ENUM?.APPROVE) {
          handleApprove();
        } else if (actionType === ACTION_BUTTON_ENUM?.REJECT) {
          handleReject();
        }
    };

    /** Debounced search function */
    const debouncedSearch = useMemo(
        () =>
          debounce((keyword: string, sortExpression: string) => {
            const trimmedKeyword = keyword.trim();
            setSearchKeyword(trimmedKeyword);
            setSearchMode(trimmedKeyword ? "keyword" : null);
    
            const updatedPage = 1;
            setCurrentPage(updatedPage);
    
            const params = {
              id,
              clientId,
              listRange: {
                pageIndex: updatedPage,
                pageSize,
                sortExpression: sortExpression,
              },
              keyWord: trimmedKeyword,
              userCriteria: {},
            };
    
            dispatch(fetchUsersPendingApprovalsSlice(params));
          }, 500),
        [dispatch, id, clientId, pageSize]
    );

    /** Handle search input change */
    const handleSearch = useCallback(
        (keyword: string, sortExpression: string = "LastModifiedDate desc") => {
          debouncedSearch(keyword, sortExpression);
        },
        [debouncedSearch]
    );
    
    /** Cleanup debounce on unmount */
    useEffect(() => {
        return () => {
          debouncedSearch.cancel();
        };
    }, [debouncedSearch]);

    /** Handle filter submission.*/
    const handleFilterSubmit = (
        filterData: any,
        sortExpression: string = "LastModifiedDate desc"
      ) => {
        if (!id || !clientId) return;
        setSearchMode("filter"); // ✅ Disable keyword search
        setAppliedFilters(filterData);
        setCurrentPage(1);
    
        const params = {
          id: id,
          clientId: clientId,
          listRange: {
            pageIndex: 1,
            pageSize: pageSize,
            sortExpression: sortExpression,
            // requestedById: id
          },
          keyWord: "",
          userCriteria: {
            firstName: filterData?.firstName,
            lastName: filterData?.lastName,
            email: filterData?.email,
            userStatus: filterData?.userStatus,       
            country: filterData?.country, 
            registrationDateFrom: filterData?.registrationDateFrom || null,
            registrationDateTo: filterData?.registrationDateTo || null,
          },
        } as const;
    
        dispatch(fetchUsersPendingApprovalsSlice(params))
          .then((response: any) => {
            if (response.meta.requestStatus === "fulfilled") {
              setUsers(response.payload.learnerList || []);
              setAlert({
                type: "success",
                message: "Filter applied successfully.",
              });
            } else {
              setAlert({
                type: "error",
                message:
                  "Failed to apply filter: " + response.payload?.status ||
                  "Unknown error.",
              });
            }
          })
          .catch((error) => {
            console.error("Error while applying filter:", error);
            setAlert({ type: "error", message: "An unexpected error occurred." });
          });
    };

    /** Handle pagination changes. */
    const handlePageChange = (
        page: number,
        newPageSize?: number,
        sortExpression = "LastModifiedDate desc"
      ) => {
        if (newPageSize) setPageSize(newPageSize); // Update page size if provided
        setCurrentPage(page); // Update the current page state
        setSelectedUsers([]); //  Clear selected users when changing page
        const params = {
          id: id,
          clientId: clientId,
          listRange: {
            pageIndex: page,
            pageSize: newPageSize || pageSize,
            sortExpression: sortExpression,
            // requestedById: id,
            requestedById: undefined,
          },
          // keyWord: "",
          keyWord: searchKeyword,
          userCriteria: appliedFilters
            ? {
                // Use stored filters
                firstName: appliedFilters?.firstName,
                lastName: appliedFilters?.lastName,
                email: appliedFilters?.email,
                userStatus: appliedFilters?.userStatus,       
                country: appliedFilters?.country, 
                registrationDateFrom: appliedFilters?.registrationDateFrom || null,
                registrationDateTo: appliedFilters?.registrationDateTo || null,
              }
            : {},
        };
        dispatch(fetchUsersPendingApprovalsSlice(params));
    };

    /** Refresh user list. */
    const refreshUserList = (overridePageIndex?: number) => {
        const params = {
          id,
          clientId,
          listRange: {
            pageIndex: overridePageIndex ?? currentPage, // Use overridePageIndex if provided
            pageSize,
            sortExpression: "LastModifiedDate desc",
            // requestedById: "USR7cbc556be7f64ac8"
          },
          keyWord: "",
          userCriteria: appliedFilters
            ? {
                firstName: appliedFilters?.firstName,
                lastName: appliedFilters?.lastName,
                email: appliedFilters?.email,
                userStatus: appliedFilters?.userStatus,       
                country: appliedFilters?.country, 
                registrationDateFrom: appliedFilters?.registrationDateFrom || null,
                registrationDateTo: appliedFilters?.registrationDateTo || null,
              }
            : {},
        };
        dispatch(fetchUsersPendingApprovalsSlice(params));
    };

    const getSortedList = (sortExpression: string) => {
        setCurrentPage(1);
        const params = {
          id,
          clientId,
          listRange: {
            pageIndex: 1,
            pageSize,
            sortExpression: sortExpression,
          },
          keyWord: searchKeyword,
          userCriteria: {},
        };
    
        dispatch(fetchUsersPendingApprovalsSlice(params));
    };

    /* Open User details popup on view icon click */
    const ypModalUserDetailsRef = useRef<HTMLDivElement | null>(null);
    const ypModalUserDetailsRefBtnClose = useRef<HTMLButtonElement | null>(null);
    const [userDetailsData, setUserDetailsData] = useState<any>([]);
    const [userDetailsModalOpen, setUserDetailsModalOpen] = useState<boolean>(false);
    const handleOpenDetailsPopupClick = (data: any) => {
        if(data) {
            dispatch(fetchPendingApprovalsUserDetailsSlice({clientId: clientId, signUpId: data}))
                .then((response: any) => {
                    if (response.meta.requestStatus == "fulfilled") {
                        
                        setUserDetailsData(response.payload.learnerList[0]);
                        setUserDetailsModalOpen(true);

                        const modalElement = ypModalUserDetailsRef.current;
                        if (modalElement) {
                            const modal = new bootstrap.Modal(modalElement);
                            modal.show();
                        }
                        
                    } else {
                        setAlert({ type: "error", message: 'User details fetch failed: ' + response.msg });
                    }
                })
                .catch((error) => {
                    console.error("User details fetch error:", error);
                    setAlert({ type: "error", message: 'An unexpected error occurred.' });
                });
        }
    };

    // Function to clear the courseStructureTreeData
    const clearUserDetailsData = () => {
        setUserDetailsModalOpen(false);
        setUserDetailsData([]);
    };

    const formatDateForPayload = (date: Date | string | null): string | null => {
        if (!date) return null;

        const d = typeof date === "string" ? new Date(date) : date;

        if (isNaN(d.getTime())) return null; // invalid date check

        const yyyy = d.getFullYear();
        const mm = String(d.getMonth() + 1).padStart(2, '0');
        const dd = String(d.getDate()).padStart(2, '0');

        return `${dd}/${mm}/${yyyy}`;
    };

    return(
        <>

            {/* ✅ Conditionally render AlertMessage */}
            {alert && (
                <AlertMessage
                type={alert.type}
                message={alert.message}
                duration={alert.duration}
                autoClose={alert.autoClose}
                onClose={() => setAlert(null)}
                />
            )}

            {/* Loading spinner overlay */}
            {loading || isSubmitting ? (
                <div className="yp-loading-overlay">
                <div className="yp-spinner">
                    <div className="spinner-border"></div>
                </div>
                </div>
            ) : null}


            <div
                className="yp-page-title-button-section"
                id="yp-page-title-breadcrumb-section"
            >
                <div className="yp-page-title-breadcrumb">
                    <div className="yp-page-title">{pageTitle}</div>
                    <CustomBreadcrumb items={breadcrumbItems} />
                </div>
                <div className="yp-page-button">
                    
                </div>
            </div>

            {/* Common list here */}
            <div className="yp-card p-0 pb-3" id="yp-card-main-content-section">
                <div className="yp-custom-table-section">
                <CommonTable
                    type={COMMON_TABLE_TYPE.PENDING_APPROVALS}
                    tableConfig={PENDING_APPROVALS_TABLE_CONFIG}
                    filterComponent={
                        <PendingApprovalsFilter
                            onSubmit={handleFilterSubmit}
                            onClear={handleClearFilters}
                            closeFilter={handleClearFilters}
                        />
                    }
                    onFilterSubmit={handleFilterSubmit}
                    onClearFilter={handleClearFilters}     //send sort exxpression while clearing
                    data={users}
                    onSearch={handleSearch}
                    onPageChange={handlePageChange}
                    currentPage={currentPage}
                    totalRecords={userList?.totalRows}
                    pageSize={pageSize}
                    selectedUsers={selectedUsers}
                    handleCheckboxChange={handleSelectChange}
                    //handleSelectAll={handleSelectAll}
                    actionButtonList={actionButtonList}
                    actionButtonClick={handleActionButtonClick}
                    searchMode={searchMode} //  Pass down
                    setSearchMode={setSearchMode}
                    isFilterVisible={isFilterVisible}
                    setFilterVisible={setFilterVisible}
                    getSortedList={getSortedList}
                    handleOpenDetailsPopup={handleOpenDetailsPopupClick}
                />
                </div>
            </div>

            <div className="modal fade yp-modal yp-modal-right-side"
                        id="yp-modal-userDetails"
                        data-bs-backdrop="static"
                        data-bs-keyboard="false"
                        tabIndex={-1}
                        aria-labelledby="yp-modal-userDetails"
                        ref={ypModalUserDetailsRef}
                        aria-hidden="true">
                        <div className="modal-dialog modal-md modal-dialog-scrollable modal-fullscreen-sm-down">
                            <div className="modal-content">
                                <div className="modal-header">
                                    <h1 className="modal-title" id="staticBackdropLabel">User Details</h1>
                                    <button type="button"
                                        className="btn-close"
                                        data-bs-dismiss="modal"
                                        aria-label="Close"
                                        onClick={clearUserDetailsData}
                                        ref={ypModalUserDetailsRefBtnClose}></button>
                                </div>
                                <div className="modal-body">
                                    {
                                        userDetailsData && (
                                            <div className="yp-label-value-wrapper yp-label-value-fixed-width yp-label-value-fixed-width-125">
                                                <div className="yp-label-value-row">
                                                    <div className="yp-lv-label">First Name</div>
                                                    <div className="yp-lv-label-colon">:</div>
                                                    <div className="yp-lv-value">{userDetailsData?.firstName}</div>
                                                </div>
                                                <div className="yp-label-value-row">
                                                    <div className="yp-lv-label">Last Name</div>
                                                    <div className="yp-lv-label-colon">:</div>
                                                    <div className="yp-lv-value">{userDetailsData?.lastName}</div>
                                                </div>
                                                <div className="yp-label-value-row">
                                                    <div className="yp-lv-label">Login ID</div>
                                                    <div className="yp-lv-label-colon">:</div>
                                                    <div className="yp-lv-value">{userDetailsData?.loginId}</div>
                                                </div>
                                                <div className="yp-label-value-row">
                                                    <div className="yp-lv-label">Email Address</div>
                                                    <div className="yp-lv-label-colon">:</div>
                                                    <div className="yp-lv-value">{userDetailsData?.emailId}</div>
                                                </div>
                                                <div className="yp-label-value-row">
                                                    <div className="yp-lv-label">Status</div>
                                                    <div className="yp-lv-label-colon">:</div>
                                                    <div className="yp-lv-value">
                                                        <span
                                                            className={`badge ${userDetailsData?.userStatus === "Approved" ? "badge-success" : userDetailsData?.userStatus === "Pending" ? "badge-warning" : "badge-danger"}`}
                                                        >
                                                            {userDetailsData?.userStatus}
                                                        </span>
                                                    </div>
                                                </div>
                                                <div className="yp-label-value-row">
                                                    <div className="yp-lv-label">Country</div>
                                                    <div className="yp-lv-label-colon">:</div>
                                                    <div className="yp-lv-value">{userDetailsData?.country}</div>
                                                </div>
                                                <div className="yp-label-value-row">
                                                    <div className="yp-lv-label">Registration Date</div>
                                                    <div className="yp-lv-label-colon">:</div>
                                                    <div className="yp-lv-value">{userDetailsData?.registrationDate ? formatDateForPayload(userDetailsData?.registrationDate) : ''}</div>
                                                </div>
                                                <div className="yp-label-value-row">
                                                    <div className="yp-lv-label">Organization</div>
                                                    <div className="yp-lv-label-colon">:</div>
                                                    <div className="yp-lv-value">{userDetailsData?.organization}</div>
                                                </div>
                                                <div className="yp-label-value-row">
                                                    <div className="yp-lv-label">Department</div>
                                                    <div className="yp-lv-label-colon">:</div>
                                                    <div className="yp-lv-value">{userDetailsData?.department}</div>
                                                </div>
                                                <div className="yp-label-value-row">
                                                    <div className="yp-lv-label">Role</div>
                                                    <div className="yp-lv-label-colon">:</div>
                                                    <div className="yp-lv-value">{userDetailsData?.role}</div>
                                                </div>

                                                {
                                                    userDetailsData?.userStatus === "Rejected" && (
                                                        <>
                                                            <div className="yp-label-value-row">
                                                                <div className="yp-lv-label">Rejected By</div>
                                                                <div className="yp-lv-label-colon">:</div>
                                                                <div className="yp-lv-value">{userDetailsData?.lastModifiedByName}</div>
                                                            </div>
                                                            <div className="yp-label-value-row">
                                                                <div className="yp-lv-label">Rejected Date</div>
                                                                <div className="yp-lv-label-colon">:</div>
                                                                <div className="yp-lv-value">{userDetailsData?.lastModifiedDate ? formatDateForPayload(userDetailsData?.lastModifiedDate) : ''}</div>
                                                            </div>
                                                        </>
                                                    )
                                                }
                                                {
                                                    userDetailsData?.userStatus === "Approved" && (
                                                        <>
                                                            <div className="yp-label-value-row">
                                                                <div className="yp-lv-label">Approved By</div>
                                                                <div className="yp-lv-label-colon">:</div>
                                                                <div className="yp-lv-value">{userDetailsData?.lastModifiedByName}</div>
                                                            </div>
                                                            <div className="yp-label-value-row">
                                                                <div className="yp-lv-label">Approved Date</div>
                                                                <div className="yp-lv-label-colon">:</div>
                                                                <div className="yp-lv-value">{userDetailsData?.lastModifiedDate ? formatDateForPayload(userDetailsData?.lastModifiedDate) : ''}</div>
                                                            </div>
                                                        </>
                                                    )
                                                }
                                            </div>
                                        )
                                    }
                                </div>                                
                            </div>
                        </div>
            </div>

            {/* Confirmation Modal */}
            <ConfirmationModal
                show={showModal}
                onConfirm={onConfirmAction}
                onCancel={() => {
                setShowModal(false);
                setModalAlertMessageConfig({
                    type: "success",
                    message: "",
                    duration: "4000",
                    autoClose: true,
                });
                }}
                modalConfig={modalConfig}
                modalAlertMessageConfig={modalAlertMessageConfig}
            />

        </>
    )
};

export default PendingApprovals;