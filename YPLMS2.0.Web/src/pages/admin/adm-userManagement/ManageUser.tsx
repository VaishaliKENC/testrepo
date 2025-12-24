import React, { useCallback, useEffect, useMemo, useState } from "react";
import { debounce } from "lodash";
import {
  IActivateDeactivatePayload,
  User,
} from "../../../Types/commonTableTypes";
import { useAppDispatch, useAppSelector } from "../../../hooks";
import {
  activateDeactivateUser,
  addUserGetclientFields,
  fetchUsers,
} from "../../../redux/Slice/admin/userSlice";
import { useSelector } from "react-redux";
import { RootState } from "../../../redux/store";
import { useNavigate } from "react-router-dom";
import { AdminPageRoutes } from "../../../utils/Constants/Admin_PageRoutes";
import {
  ACTION_BUTTON_ENUM,
  COMMON_TABLE_TYPE,
} from "../../../utils/Constants/Enums";
import ManageUserFilter from "./ManageUserFilter";
import CommonTable from "../../../components/shared/CommonComponents/CommonTable";
import ConfirmationModal from "../../../components/shared/ConfirmationModal";
import CustomBreadcrumb from "../../../components/shared/CustomBreadcrumb";
import AlertMessage from "../../../components/shared/AlertMessage";
import { MANAGE_USER_TABLE_CONFIG } from "../../../utils/Constants/tableConfig";

const ManageUser: React.FC = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const clientId: any = useSelector((state: RootState) => state.auth.clientId);
  const id: any = useSelector((state: RootState) => state.auth.id);
  const userList: any = useSelector((state: RootState) => state.user.users);
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
      buttonName: "Activate",
      actionType: ACTION_BUTTON_ENUM?.ACTIVATE,
      disabled: users.length === 0,
    },
    {
      type: "button",
      className: "btn btn-secondary",
      buttonName: "Deactivate",
      actionType: ACTION_BUTTON_ENUM?.DEACTIVATE,
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
    { label: "Manage Users" },
  ];
  const pageTitle: string = "Manage Users";
  document.title = pageTitle;

  /** Fetch users fields details validations */
  useEffect(() => {
    const params = {
      clientId,
      listRange: { pageIndex: 0, pageSize: 0, sortExpression: "MinLength" },
    } as const;
    dispatch(addUserGetclientFields(params));
  }, [clientId]);

  /** Fetch the initial user list when the component mounts. */
  useEffect(() => {
    if (!id || !clientId) return;
    fetchUserList(1, pageSize, appliedFilters);
  }, [id, clientId]);

  /** Update the local user state when userList changes. */
  useEffect(() => {
    setUsers(userList?.learnerList || []);
  }, [userList]);

  /** Fetch user list with pagination and filters. */
  const fetchUserList = (pageIndex: number, pageSize: number, filters: any) => {
    const params = {
      id,
      clientId,
      listRange: {
        pageIndex,
        pageSize,
        sortExpression: "LastModifiedDate desc",
        requestedById: undefined,
        //  requestedById: id
      },
      keyWord: "",
      userCriteria: filters || undefined,
    };
    dispatch(fetchUsers(params));
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

    dispatch(fetchUsers(params)); // Refetch users with empty filters
  };

  /** Handle individual user selection via checkbox.*/
  const handleCheckboxChange = (userId: string) => {
    setSelectedUsers((prevSelected) => {
      let newSelected;
      if (prevSelected.includes(userId)) {
        newSelected = prevSelected.filter((id) => id !== userId);
      } else {
        newSelected = [...prevSelected, userId];
      }
      return newSelected;
    });
  };

  /** Handle selecting/deselecting all users. */
  const handleSelectAll = (selectAll: boolean) => {
    if (selectAll) {
      const allUserIds = users.map((user) => user.id);
      setSelectedUsers(allUserIds);
    } else {
      setSelectedUsers([]);
    }
  };

  /** Handle activate button click.  */
  const handleActivate = () => {
    if (selectedUsers.length <= 0) {
      setAlert({ type: "warning", message: "Please select a record." });
      return;
    }

    const selectedUsersWithInactiveStatus = users.filter(
      (user) => selectedUsers.includes(user.id) && !user.isActive
    );
    // If any selected user is already active, show an error message
    if (selectedUsers.length !== selectedUsersWithInactiveStatus.length) {
      setAlert({
        type: "warning",
        message: "Please select only inactive record(s).",
      });
      return;
    }
    const confirmActivate = () => {
      const usersToActivate: IActivateDeactivatePayload[] = users
        .filter((user) => selectedUsers.includes(user.id))
        .map((user) => ({
          id: user.id,
          clientId: clientId,
          lastModifiedById: user.id,
          isActive: true,
        }));

      onActivate(usersToActivate);
    };

    /** Activate selected courses.   */
    const onActivate = (usersToActivate: IActivateDeactivatePayload[]) => {
      setIsSubmitting(true); //  Start loader
      dispatch(activateDeactivateUser(usersToActivate))
        .then(() => {
          setSelectedUsers([]);
          setCurrentPage(1);
          refreshUserList(1);
          setModalAlertMessageConfig({
            type: "success",
            message: "Users activated successfully!",
          });
        })
        .catch((error) => {
          setModalAlertMessageConfig({
            type: "error",
            message: "Error activating users.",
          });
        })
        .finally(() => {
          setIsSubmitting(false); //  Stop loader
        });
    };
    setModalConfig({
      title: "Activate User Confirmation",
      message: "Are you sure you want to activate the selected users?",
      primaryBtnText: "Activate",
    });
    setOnConfirmAction(() => confirmActivate);
    setShowModal(true);
  };

  /** Handle Deactivate button click.  */
  const handleDeactivate = () => {
    if (selectedUsers.length <= 0) {
      setAlert({ type: "warning", message: "Please select a record." });
      return;
    }

    const selectedUsersWithActiveStatus = users.filter(
      (user) => selectedUsers.includes(user.id) && user.isActive
    );
    // If any selected user is already inactive, show an error message
    if (selectedUsers.length !== selectedUsersWithActiveStatus.length) {
      setAlert({
        type: "warning",
        message: "Please select only active record(s).",
      });
      return;
    }

    const confirmDeactivate = () => {
      const usersToDeactivate: IActivateDeactivatePayload[] = users
        .filter((user) => selectedUsers.includes(user.id))
        .map((user) => ({
          id: user.id,
          clientId: clientId,
          lastModifiedById: user.id,
          isActive: false,
        }));
      onDeactivate(usersToDeactivate);
    };

    setModalConfig({
      title: "Deactivate User Confirmation",
      message: "Are you sure you want to deactivate the selected users?",
      primaryBtnText: "Deactivate",
    });
    setOnConfirmAction(() => confirmDeactivate);
    setShowModal(true);
  };

  /**
   * Deactivate selected users.
   */
  const onDeactivate = (usersToDeactivate: IActivateDeactivatePayload[]) => {
    setIsSubmitting(true); //  Start loader
    dispatch(activateDeactivateUser(usersToDeactivate))
      .then(() => {
        setSelectedUsers([]);
        setCurrentPage(1);
        refreshUserList(1);
        setModalAlertMessageConfig({
          type: "success",
          message: "Users deactivated successfully!",
        });
      })
      .catch((error) => {
        setModalAlertMessageConfig({
          type: "error",
          message: "Error deactivating users.",
        });
      })
      .finally(() => {
        setIsSubmitting(false); //  Stop loader
      });
  };

  /** Handle user action button clicks (Activate/Deactivate). */
  const handleActionButtonClick = (actionType: string) => {
    if (actionType === ACTION_BUTTON_ENUM?.ACTIVATE) {
      handleActivate();
    } else if (actionType === ACTION_BUTTON_ENUM?.DEACTIVATE) {
      handleDeactivate();
    }
  };

  /** Handle user search. */
  // const handleSearch = (keyword: string) => {
  //   const trimmedKeyword = keyword.trim();

  //   setSearchKeyword(trimmedKeyword);
  //   setSearchMode(trimmedKeyword ? 'keyword' : null);

  //   // Always reset current page to 1 before calling fetch
  //   const updatedPage = 1;
  //   setCurrentPage(updatedPage);

  //   const params = {
  //     id,
  //     clientId,
  //     listRange: {
  //       pageIndex: updatedPage,
  //       pageSize,
  //       sortExpression: 'LastModifiedDate desc',
  //     },
  //     keyWord: trimmedKeyword,
  //   };

  //   dispatch(fetchUsers(params));
  // };

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
        };

        dispatch(fetchUsers(params));
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

  /**Navigate to Add User page.   */
  const handleAddUser = () => {
    navigate(AdminPageRoutes.ADMIN_ADD_USER.FULL_PATH);
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
            FirstName: appliedFilters?.FirstName,
            LastName: appliedFilters?.LastName,
            LoginId: appliedFilters?.LoginId,
            Email: appliedFilters?.Email,
            Active:
              appliedFilters?.Active === "true"
                ? true
                : appliedFilters?.Active === "false"
                ? false
                : undefined,
          }
        : undefined,
    };
    dispatch(fetchUsers(params));
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
            FirstName: appliedFilters?.FirstName,
            LastName: appliedFilters?.LastName,
            LoginId: appliedFilters?.LoginId,
            Email: appliedFilters?.Email,
            Active:
              appliedFilters?.Active === "true"
                ? true
                : appliedFilters?.Active === "false"
                ? false
                : undefined,
          }
        : undefined,
    };
    dispatch(fetchUsers(params));
  };

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
        FirstName: filterData?.FirstName,
        LastName: filterData?.LastName,
        LoginId: filterData?.LoginId,
        Email: filterData?.Email,
        Active:
          filterData?.Active === "true"
            ? true
            : filterData?.Active === "false"
            ? false
            : undefined,
      },
    } as const;

    dispatch(fetchUsers(params))
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
  const getSortedList = (sortExpression: string) => {
    const params = {
      id,
      clientId,
      listRange: {
        pageIndex: 1,
        pageSize,
        sortExpression: sortExpression,
      },
      keyWord: searchKeyword,
    };

    dispatch(fetchUsers(params));
  };
  return (
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
          <button className="btn btn-primary" onClick={handleAddUser}>
            <i className="fa fa-plus"></i>
            Add User
          </button>
        </div>
      </div>

      {/* Common list here */}
      <div className="yp-card p-0 pb-3" id="yp-card-main-content-section">
        <div className="yp-custom-table-section">
          <CommonTable
            type={COMMON_TABLE_TYPE.MANAGE_USER}
            tableConfig={MANAGE_USER_TABLE_CONFIG}
            filterComponent={
              <ManageUserFilter
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
            handleCheckboxChange={handleCheckboxChange}
            handleSelectAll={handleSelectAll}
            actionButtonList={actionButtonList}
            actionButtonClick={handleActionButtonClick}
            searchMode={searchMode} //  Pass down
            setSearchMode={setSearchMode}
            isFilterVisible={isFilterVisible}
            setFilterVisible={setFilterVisible}
            getSortedList={getSortedList}
          />
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
  );
};

export default ManageUser;
