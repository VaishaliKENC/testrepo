import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { RootState } from "../../../../redux/store";
import { useAppDispatch, useAppSelector } from "../../../../hooks";
import CommonTable from '../../../../components/shared/CommonComponents/CommonTable';
import AlertMessage from "../../../../components/shared/AlertMessage";
import { fetchUsersSelectedSlice, deleteSelectedUserSlice } from "../../../../redux/Slice/admin/oneTimeAssignmentsSlice";
import { User } from "../../../../Types/assignmentTypes";
import { IDeletePayload } from '../../../../Types/commonTableTypes';
import { ACTION_BUTTON_ENUM, COMMON_TABLE_TYPE } from '../../../../utils/Constants/Enums';
import ConfirmationModal from '../../../../components/shared/ConfirmationModal';
import { fetchDefaultAssignmentListSlice } from "../../../../redux/Slice/admin/defaultAssignmentsSlice";
import { MANAGE_ONE_TIME_ASSIGNMENT_USER_SELECTED_TABLE_CONFIG } from "../../../../utils/Constants/tableConfig";

interface Props {
    onSetActiveTab: (tabKey: string) => void;
    selectedBusinessRuleName: string;
    onCancel: () => void;
    activeTabOnAPI: string;
    hasVisitedDefineTab: boolean;
    setHasVisitedDefineTab: (value: boolean) => void;
}

const SelectedUsersContent: React.FC<Props> = ({ onSetActiveTab, selectedBusinessRuleName, hasVisitedDefineTab, setHasVisitedDefineTab }) => {
    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const clientId: any = useSelector((state: RootState) => state.auth.clientId);
    const id: any = useSelector((state: RootState) => state.auth.id);
    const usersList: any = useSelector((state: RootState) => state.oneTimeAssignments.usersSelected);
    const { loading } = useAppSelector((state: RootState) => state.oneTimeAssignments);

    // Selected Users Tab
    const [users, setUsers] = useState<User[]>([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(5);
    const [selectedUsers, setSelectedUsers] = useState<string[]>([]);
    const [appliedFilters, setAppliedFilters] = useState<any>(null);
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);
    const [onConfirmAction, setOnConfirmAction] = useState<() => void>(() => () => { });
    const [showModal, setShowModal] = useState(false);
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

    // Action buttons configuration
    const actionButtonList = [
        { type: "button", className: "btn btn-danger", buttonName: "Delete", actionType: ACTION_BUTTON_ENUM?.DELETE, disabled: users.length === 0 }
    ];

    /** Fetch the initial user list (selected) when the component mounts. */
    // useEffect(() => {
    //     if (!id || !clientId) return;
    //     fetchUserList(1, pageSize, appliedFilters);
    // }, [id, clientId]);

    // useEffect(() => {
    //     if (activeTabOnAPI === 'tabSelectedUsers' && clientId && id) {
    //         fetchUserList(1, pageSize, appliedFilters);
    //     }
    // }, [activeTabOnAPI, clientId, id]);

    /** Update the local users state when usersList changes. */
    useEffect(() => {
        setUsers(usersList?.userList || []);
        setCurrentPage(1);
    }, [usersList]);

    /** Fetch user list with pagination and filters. */
    const fetchUserList = (pageIndex: number, pageSize: number, filters: any) => {
        const params = {
            clientId,
            createdById: id,
            listRange: {
                pageIndex,
                pageSize,
                sortExpression: ""
            }
        };
        dispatch(fetchUsersSelectedSlice(params));
    };

    /** Handle individual user selection via checkbox.*/
    const handleCheckboxChange = (userId: string) => {
        setSelectedUsers((prevSelected) => {
            if (prevSelected.includes(userId)) {
                return prevSelected.filter((id) => id !== userId); // Deselect if already selected
            }
            return [...prevSelected, userId]; // Select if not already selected
        });
        // const updated = selectedUsers.includes(userId)
        //     ? selectedUsers.filter((id) => id !== userId)
        //     : [...selectedUsers, userId];

        // setSelectedUsers(updated);
        // dispatch(setSelectedUserIds(updated));
    };

    /** Handle selecting/deselecting all users. */
    const handleSelectAll = (selectAll: boolean) => {
        if (selectAll) {
            const allUserIds = users.map((user) => user.id);
            setSelectedUsers(allUserIds);
        } else {
            setSelectedUsers([]);
        }
        // const allIds = users.map((user) => user.id);
        // const updated = selectAll
        //     ? Array.from(new Set([...selectedUsers, ...allIds]))
        //     : selectedUsers.filter((id) => !allIds.includes(id));

        // setSelectedUsers(updated);
        // dispatch(setSelectedUserIds(updated));
    };

    /** Handle pagination changes. */
    const handlePageChange = (page: number, newPageSize?: number) => {
        if (newPageSize) setPageSize(newPageSize);
        setCurrentPage(page);

        const params = {
            clientId: clientId,
            createdById: id,
            listRange: {
                pageIndex: page,
                pageSize: newPageSize || pageSize,
                sortExpression: ""
            }
        };
        dispatch(fetchUsersSelectedSlice(params));
    };


    /** Handle Delete button click.  */
    const handleDelete = () => {
        if (selectedUsers.length <= 0) {
            setAlert({ type: "warning", message: "Please select a record.", autoClose: false });
            return;
        }
        const confirmDelete = () => {
            const usersToDelete: IDeletePayload[] = users
                .filter((user) => selectedUsers.includes(user.id))
                .map((user) => ({
                    id: user.id,
                    clientId: clientId,
                    createdById: id
                }));

            onDelete(usersToDelete);
        };
        setModalConfig({
            title: 'Delete User Confirmation',
            message: 'Are you sure you want to delete the selected users?',
            primaryBtnText: 'Delete',
        });
        setOnConfirmAction(() => confirmDelete);
        setShowModal(true);
    };

    const onDelete = (userToDelete: IDeletePayload[]) => {
        dispatch(deleteSelectedUserSlice(userToDelete)).unwrap()
            .then((response) => {

                if (response?.msg) {
                    setModalAlertMessageConfig({
                        type: 'success',
                        message: response.msg
                    });

                    if (response.code === 200) {
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
                    }
                    setCurrentPage(1);
                }
            }).catch((error) => {
                console.error("Error deleting user:", error);
                setModalAlertMessageConfig({
                    type: 'error',
                    message: error?.msg || "Failed to delete user"
                });
            });
    };

    /** Handle user action button clicks (Activate/Deactivate/Delete). */
    const handleActionButtonClick = (actionType: string) => {
        if (actionType === ACTION_BUTTON_ENUM?.DELETE) {
            handleDelete()
        }
    }



    const handleSetActiveTab = (tabKey: string) => {
        onSetActiveTab(tabKey);
    }

    const handleNext = () => {
        // dispatch(fetchDefaultAssignmentListSlice(clientId))
        //     .then((response: any) => {
        //         console.log("response 1111", response)
        //         if (response.meta.requestStatus == "fulfilled") {
        //             handleSetActiveTab("tabDefineProperties");
        //         }
        //     })
        //     .catch((error) => {
        //         console.error("DefaultAssignment error:", error);
        //         setAlert({ type: "error", message: 'An unexpected error occurred.' });
        //     })
  
        if (!hasVisitedDefineTab) {
            dispatch(fetchDefaultAssignmentListSlice(clientId))
                .then((response: any) => {
                    if (response.meta.requestStatus === "fulfilled") {
                        setHasVisitedDefineTab(true); // ✅ mark Define tab as visited
                        handleSetActiveTab("tabDefineProperties");
                    }
                })
                .catch((error) => {
                    console.error("DefaultAssignment error:", error);
                    setAlert({ type: "error", message: 'An unexpected error occurred.' });
                });
        } else {
            handleSetActiveTab("tabDefineProperties"); // ✅ skip API, just navigate
        }
    }

    return (
        <>
            <div className="yp-tab-content-padding">
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

                {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}

                {loading || isSubmitting ? (<div className="yp-loading-overlay"><div className="yp-spinner"><div className="spinner-border"></div></div></div>) : null}

                <div className="yp-box-with-shadow yp-mb-30-px p-0">
                    <div className="yp-custom-table-section">
                        <CommonTable
                            type={COMMON_TABLE_TYPE.MANAGE_ONE_TIME_ASSIGNMENT_USER_SELECTED}
                            tableConfig={MANAGE_ONE_TIME_ASSIGNMENT_USER_SELECTED_TABLE_CONFIG}
                            data={users}
                            onPageChange={handlePageChange}
                            currentPage={currentPage}
                            totalRecords={
                                Array.isArray(usersList?.userList) && usersList.userList.length > 0
                                    ? usersList.userList[0]?.listRange?.totalRows
                                    : 0
                            }
                            pageSize={pageSize}
                            selectedUsers={selectedUsers}
                            handleCheckboxChange={handleCheckboxChange}
                            handleSelectAll={handleSelectAll}
                            actionButtonList={actionButtonList}
                            actionButtonClick={handleActionButtonClick}
                        />
                    </div>
                </div>

                <div className="yp-box-with-shadow">
                    <div className="yp-fs-16 yp-text-dark-purple mb-3">Selected User Group</div>
                    <div className="row">
                        <div className="col-12 col-md-6 col-lg-4">
                            <div className="form-group">
                                {
                                    !selectedBusinessRuleName || selectedBusinessRuleName === '0'
                                        ? (
                                            <div>
                                                No user group selected
                                            </div>
                                        )
                                        : (
                                            <div className="yp-form-control-wrapper">
                                                <select className={`form-control yp-form-control`}
                                                    name="businessRuleSelected"
                                                    value={selectedBusinessRuleName}
                                                    disabled
                                                    id="">
                                                    <option value={selectedBusinessRuleName}>{selectedBusinessRuleName}</option>
                                                </select>
                                                <label className="form-label">Select User Group</label>
                                            </div>
                                        )
                                }
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div className="yp-tab-content-padding mt-5">
                <div className="row">
                    <div className="col-12">
                        <div className="yp-form-button-row">
                            <button className="btn btn-primary" onClick={() => handleNext()} disabled={users.length === 0}>Next</button>
                            <button className="btn btn-secondary"
                                onClick={() => handleSetActiveTab("tabSelectUsers")}>Back</button>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default SelectedUsersContent;