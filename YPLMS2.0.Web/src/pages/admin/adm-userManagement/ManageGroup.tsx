import React, { useEffect, useState } from 'react';
import { Group, IActivateDeactivatePayload, IDeletePayload, User } from '../../../Types/commonTableTypes';
import { useAppDispatch, useAppSelector } from '../../../hooks';
import { deactivateGroupRule, deleteGroupRule, fetchGroups } from '../../../redux/Slice/admin/userSlice';
import { useSelector } from 'react-redux';
import { RootState } from '../../../redux/store';
import { useNavigate } from 'react-router-dom';
import { AdminPageRoutes } from '../../../utils/Constants/Admin_PageRoutes';
import { ACTION_BUTTON_ENUM, COMMON_TABLE_TYPE } from '../../../utils/Constants/Enums';
import CommonTable from '../../../components/shared/CommonComponents/CommonTable';
import ConfirmationModal from '../../../components/shared/ConfirmationModal';
import CustomBreadcrumb from '../../../components/shared/CustomBreadcrumb';
import AlertMessage from '../../../components/shared/AlertMessage';
import { MANAGE_GROUP_TABLE_CONFIG } from '../../../utils/Constants/tableConfig';



const ManageGroup: React.FC = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const clientId: any = useSelector((state: RootState) => state.auth.clientId);
  const id: any = useSelector((state: RootState) => state.auth.id);
  const groupList: any = useSelector((state: RootState) => state.user.groups);
  const { loading } = useAppSelector((state: RootState) => state.user);

  //Local states
  const [groups, setGroups] = useState<Group[]>([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [selectedUsers, setSelectedUsers] = useState<string[]>([]);
  const [showModal, setShowModal] = useState(false);
  // Action buttons configuration
const actionButtonList = [
  { type: "button", className: "btn btn-primary", buttonName: "Activate", actionType: ACTION_BUTTON_ENUM?.ACTIVATE },
  { type: "button", className: "btn btn-secondary", buttonName: "Deactivate", actionType: ACTION_BUTTON_ENUM?.DEACTIVATE, disabled: groups.length === 0  },
  { type: "button", className: "btn btn-danger", buttonName: "Delete", actionType: ACTION_BUTTON_ENUM?.DELETE, disabled: groups.length === 0  }
];
  const [modalConfig, setModalConfig] = useState({
      title: '',
      message: '',
      primaryBtnText: '',
  });
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
  const [onConfirmAction, setOnConfirmAction] = useState<() => void>(() => () => { });
  const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);

  /** Breadcrumb navigation items*/
  const breadcrumbItems = [
    { iconClass: 'fa-classic fa-solid fa-house', path: AdminPageRoutes.ADMIN_DASHBOARD.FULL_PATH },
    { label: 'Manage Groups' },
  ];
  const pageTitle: string = 'Manage Groups';
  document.title = pageTitle;


  /** Fetch the initial groupList  when the component mounts. */
  useEffect(() => {
    if (!id || !clientId) return;
    fetchGroupList(1, pageSize);
  }, [id, clientId]);

  /** Update the local user state when groupList changes. */
  useEffect(() => {
    setGroups(groupList?.groupRuleList || []);
  }, [groupList]);

  /** Fetch group list with pagination and filters. */
  const fetchGroupList = (pageIndex: number, pageSize: number) => {
    const params = {
      id, clientId,
      listRange: { pageIndex, pageSize, sortExpression: 'LastModifiedDate desc', requestedById: id },
      ruleName: "",
    };
    dispatch(fetchGroups(params));
  };

  /** Handle individual group selection via checkbox.*/
  const handleCheckboxChange = (userId: string) => {
    setSelectedUsers((prevSelected) => {
      if (prevSelected.includes(userId)) {
        return prevSelected.filter((id) => id !== userId);
      }
      return [...prevSelected, userId];
    });
  };

  /** Handle selecting/deselecting all groups. */
  const handleSelectAll = (selectAll: boolean) => {
    if (selectAll) {
      const allUserIds = groups.map((user) => user.id);
      setSelectedUsers(allUserIds);
    } else {
      setSelectedUsers([]);
    }
  };


  /** 
    * Deactivate selected users. 
     */
  const onDeactivate = (usersToDeactivate: IActivateDeactivatePayload[]) => {
    dispatch(deactivateGroupRule(usersToDeactivate)).then(() => {
      setSelectedUsers([]);
      setCurrentPage(1);
      refreshGroupList(1);
      setModalAlertMessageConfig({
        type: 'success', 
        message: 'Groups deactivated successfully!'
      });
    }).catch((error) => {
      console.error("Error deactivating groups:", error);
      setModalAlertMessageConfig({
        type: 'error', 
        message: "Error deactivating groups: "+ error
      });
    });
  };

  /** Handle Deactivate button click.  */
  const handleDeactivate = () => {
    if (selectedUsers.length <= 0) { 
      setAlert({ type: "warning", message: "Please select a record." });
      return; 
    }

    const selectedUsersWithActiveStatus = groups.filter(user => selectedUsers.includes(user.id) && user.isActive);
    if (selectedUsers.length !== selectedUsersWithActiveStatus.length) {
      setAlert({ type: "warning", message: "Please select only active record(s)." });
      return;
    }

    const confirmDeactivate = () => {
      const usersToDeactivate: IActivateDeactivatePayload[] = groups
        .filter((user) => selectedUsers.includes(user.id))
        .map((user) => ({
          id: user.id,
          clientId: clientId,
          isActive: false,
          lastModifiedById: "USR7cbc556be7f64ac8",
          // lastModifiedById: user.id,

        }));
      onDeactivate(usersToDeactivate);      
    };

    setModalConfig({
      title: 'Deactivate Groups Confirmation',
      message: 'Are you sure you want to deactivate the selected groups?',
      primaryBtnText: 'Deactivate',
    });
    setOnConfirmAction(() => confirmDeactivate);
    setShowModal(true);
  };

  /**  Delete selected group.*/
  const onDelete = (groupToDelete: IDeletePayload[]) => {
    dispatch(deleteGroupRule(groupToDelete)).unwrap()
      .then((response) => {
        if (response?.msg) {
          setModalAlertMessageConfig({
            type: 'success', 
            message: response.msg
          });
        }
        setSelectedUsers([]);
        setCurrentPage(1);
        refreshGroupList(1);
      }).catch((error) => {
        console.error("Error deleting Group:", error);
        setModalAlertMessageConfig({
          type: 'error', 
          message: error?.message || "Failed to delete Group"
        });
      });
  };

  /** Handle Delete button click.  */
  const handleDelete = () => {
    if (selectedUsers.length <= 0) { 
      setAlert({ type: "warning", message: "Please select a record.", autoClose: false });
      return; 
    }
    const confirmDelete = () => {
      const groupToDelete: IDeletePayload[] = groups
        .filter((user) => selectedUsers.includes(user.id))
        .map((user) => ({
          id: user.id,
          clientId: clientId,
          // lastModifiedById:  user.id,
          lastModifiedById: "USR7cbc556be7f64ac8",
        }));

      onDelete(groupToDelete);
    };
    setModalConfig({
      title: 'Delete Groups Confirmation',
      message: 'Are you sure you want to delete the selected Groups?',
      primaryBtnText: 'Delete',
    });
    setOnConfirmAction(() => confirmDelete);
    setShowModal(true);
  };


  /** Handle user action button clicks (Deactivate/ delete). */
  const handleActionButtonClick = (actionType: string) => {
    if (actionType === ACTION_BUTTON_ENUM?.DEACTIVATE) {
      handleDeactivate()
    } else if (actionType === ACTION_BUTTON_ENUM?.DELETE) {
      handleDelete()
    }
  }

  /** Handle user search. */
  const handleSearch = (ruleName: string) => {
    const params = {
      id, clientId,
      listRange: { pageIndex: currentPage, pageSize, sortExpression: 'LastModifiedDate desc', requestedById: id },
      ruleName: ruleName
    };
    dispatch(fetchGroups(params));
    setCurrentPage(1);
  };

  /**Navigate to Add Group page.   */
  const handleCreateGroup = () => {
    navigate(AdminPageRoutes.ADMIN_ADD_USER_GROUP.FULL_PATH);
  };

  /** Handle pagination changes. */
  const handlePageChange = (page: number, newPageSize?: number) => {
    if (newPageSize) setPageSize(newPageSize); // Update page size if provided
    setCurrentPage(page); // Update the current page state
    setSelectedUsers([]);//  Clear selected users when changing page
    const params = {
      id: id,
      clientId: clientId,
      listRange: {
        pageIndex: page,
        pageSize: newPageSize || pageSize,
        sortExpression: 'LastModifiedDate desc',
        requestedById: id,
      },
      ruleName: "",
    };
    dispatch(fetchGroups(params));
  };

  /** Refresh group list. */
  const refreshGroupList = (overridePageIndex?: number) => {
    const params = {
      id, clientId,
      listRange: {
        pageIndex: overridePageIndex ?? currentPage,
        pageSize,
        sortExpression: 'LastModifiedDate desc',
        requestedById: id
      },
      ruleName: ""
    };
    dispatch(fetchGroups(params));
  };


  return (
    <>
      {/* Loading spinner overlay */}
      {loading && <div className="yp-loading-overlay"><div className="yp-spinner"><div className="spinner-border"></div></div></div>}

      {/* ✅ Conditionally render AlertMessage */}
      {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}

      <div className="yp-page-title-button-section" id="yp-page-title-breadcrumb-section">
        <div className="yp-page-title-breadcrumb">
          <div className="yp-page-title">{pageTitle}</div>
          <CustomBreadcrumb items={breadcrumbItems} />
        </div>
        <div className="yp-page-button">
          <button className="btn btn-primary" onClick={handleCreateGroup}>
            <i className="fa fa-plus"></i>
            Create New Group
          </button>
        </div>
      </div>

      {/* Common list here */}
      <div className="yp-card p-0 pb-3" id="yp-card-main-content-section">
        <div className="yp-custom-table-section">
          <CommonTable
            type={COMMON_TABLE_TYPE.MANAGE_GROUP}
             tableConfig={MANAGE_GROUP_TABLE_CONFIG}
            data={groups}
            onSearch={handleSearch}
            onPageChange={handlePageChange}
            currentPage={currentPage}
            totalRecords={groupList?.totalRows}
            pageSize={pageSize}
            selectedUsers={selectedUsers}
            handleCheckboxChange={handleCheckboxChange}
            handleSelectAll={handleSelectAll}
            actionButtonList={actionButtonList}
            actionButtonClick={handleActionButtonClick}
          />
        </div>
      </div>

      {/* Confirmation Modal */}
      <ConfirmationModal
        show={showModal}
        onConfirm={onConfirmAction}
        onCancel={() => {
          setShowModal(false);
          setModalAlertMessageConfig({ type: 'success', message: '', duration: '4000', autoClose: true });
        }
      }
      modalConfig={modalConfig}
      modalAlertMessageConfig={modalAlertMessageConfig}
      />

    </>
  );
};

export default ManageGroup;
