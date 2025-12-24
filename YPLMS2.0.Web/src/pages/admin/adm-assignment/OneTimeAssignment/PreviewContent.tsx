import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { format, parseISO } from 'date-fns';
import { RootState } from "../../../../redux/store";
import { useNavigate, useParams } from 'react-router-dom';
import CommonTable from '../../../../components/shared/CommonComponents/CommonTable';
import { ACTIVITY_CONTENT_TYPE_ENUM, COMMON_TABLE_TYPE } from "../../../../utils/Constants/Enums";
import { User } from "../../../../Types/authTypes";
import { Activity, ISaveOnetimeAssignment } from "../../../../Types/assignmentTypes";
import { AdminPageRoutes } from "../../../../utils/Constants/Admin_PageRoutes";
import { SaveOnetimeAssignment } from "../../../../redux/Slice/admin/oneTimeAssignmentsSlice";
import { useAppDispatch, useAppSelector } from "../../../../hooks";
import AlertMessage from "../../../../components/shared/AlertMessage";
import { MANAGE_ONE_TIME_ASSIGNMENT_PREVIEW_CONTENT_SELECTED_ACTIVITY_TABLE_CONFIG, MANAGE_ONE_TIME_ASSIGNMENT_PREVIEW_CONTENT_SELECTED_USER_TABLE_CONFIG } from "../../../../utils/Constants/tableConfig";


interface Props {
    onSetActiveTab: (tabKey: string) => void;
    onCancel: () => void;
    selectedBusinessRuleName: string;
    onResetAfterSubmit: () => void; 
}

const PreviewContent: React.FC<Props> = (
    {
        onSetActiveTab,
        onCancel,onResetAfterSubmit, selectedBusinessRuleName
    }) => {
    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const clientId: any = useSelector((state: RootState) => state.auth.clientId);
    const id: any = useSelector((state: RootState) => state.auth.id);
    const [users, setUsers] = useState<User[]>([]);
    const [activities, setActivities] = useState<Activity[]>([]);
    const selectedActivitiyList: any = useSelector((state: RootState) => state.oneTimeAssignments.activitySelected);
    const usersList: any = useSelector((state: RootState) => state.oneTimeAssignments.usersSelected);
    const definePropertiesList: any = useSelector((state: RootState) => state.oneTimeAssignments.assignmentDefineProperties);
    const { loading } = useAppSelector((state: RootState) => state.oneTimeAssignments);

    const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);

    // console.log("selectedActivitiy", selectedActivitiyList)
    // console.log("usersList", usersList)
    // console.log("definePropertiesList", definePropertiesList)

    const formatToMMDDYYYYTime = (dateStr: string): string => {
        if (!dateStr || dateStr === "0001-01-01T00:00:00") return "";

        const date = new Date(dateStr);
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');
        const year = date.getFullYear();

        return `${month}/${day}/${year} 00:00:00`;
    };

    const handleSetActiveTab = (tabKey: string) => {
        onSetActiveTab(tabKey);
    }

    useEffect(() => {
        setActivities(selectedActivitiyList?.activityList || []);
    }, [selectedActivitiyList]);

    useEffect(() => {
        setUsers(usersList?.userList || []);
    }, [usersList]);

     // Match selected activity type key in enum and return that key's value
    function getValueFromEnum<T extends Record<string, any>>(enumObj: T, key: string): T[keyof T] | undefined {
        if (Object.prototype.hasOwnProperty.call(enumObj, key)) {
            return enumObj[key as keyof T];
        }
        return undefined;
    }

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        const isMailChecked = definePropertiesList?.IsMailChecked ?? false;

        const payload: ISaveOnetimeAssignment = {
            clientId: clientId,


              activities: activities.map((activity) => ({
                id: activity.id || "",
                name: activity.activityName || "",
                type: getValueFromEnum(ACTIVITY_CONTENT_TYPE_ENUM, activity?.activityType),
               //  type: activity.activityType || "",
                isSelected: true
            })),
            learners: (users as any[]).map((user) => ({
                id: user.systemUserGUID || "",
                isSelected: true,
                registrationDate: ""
            })),

            AssignmentDateText: formatToMMDDYYYYTime(definePropertiesList?.AssignmentDateText ?? ""),
            DueDateText: formatToMMDDYYYYTime(definePropertiesList?.DueDateText ?? ""),
            ExpiryDateText: formatToMMDDYYYYTime(definePropertiesList?.ExpiryDateText ?? ""),

            LastModifiedById: id ?? "",
            CurrentUserID: id ?? "",

            IsReassignmentChecked: definePropertiesList?.IsReassignmentChecked ?? false,
            BusinessRuleId: definePropertiesList?.BusinessRuleId ?? 0,
            IsMailChecked: isMailChecked,
            IsAutoMail: isMailChecked,
            IsDirectSendMail: isMailChecked,

            IsAssignmentBasedOnHireDate: definePropertiesList?.IsAssignmentBasedOnHireDate ?? false,
            IsAssignmentBasedOnCreationDate: definePropertiesList?.IsAssignmentBasedOnCreationDate ?? false,
            AssignAfterDaysOf: definePropertiesList?.AssignAfterDaysOf || 0,

            IsNoDueDate: definePropertiesList?.IsNoDueDate ?? false,
            IsDueBasedOnAssignDate: definePropertiesList?.IsDueBasedOnAssignDate ?? false,
            IsDueBasedOnHireDate: definePropertiesList?.IsDueBasedOnHireDate ?? false,
            IsDueBasedOnCreationDate: definePropertiesList?.IsDueBasedOnCreationDate ?? false,
            IsDueBasedOnStartDate: definePropertiesList?.IsDueBasedOnStartDate ?? false,
            DueAfterDaysOf: definePropertiesList?.DueAfterDaysOf || 0,

            IsNoExpiryDate: definePropertiesList?.IsNoExpiryDate ?? false,
            IsExpiryBasedOnAssignDate: definePropertiesList?.IsExpiryBasedOnAssignDate ?? false,
            IsExpiryBasedOnStartDate: definePropertiesList?.IsExpiryBasedOnStartDate ?? false,
            IsExpiryBasedOnDueDate: definePropertiesList?.IsExpiryBasedOnDueDate ?? false,
            ExpireAfterDaysOf: definePropertiesList?.ExpireAfterDaysOf || 0,

        }
        // console.log("payload", payload)

        dispatch(SaveOnetimeAssignment(payload))
            .then((response: any) => {

                if (response.meta.requestStatus == "fulfilled") {
                    setAlert({ type: "success", message: response?.payload.msg });
                    setTimeout(() => {
                         onResetAfterSubmit();
                         handleSetActiveTab("tabSelectActivity");
                    }, 2000);
                } else {
                    setAlert({ type: "error", message: response?.payload.msg });
                }
            })
            .catch((error) => {
                console.error("Add oneTime Assignment error:", error);
                setAlert({ type: "error", message: 'An unexpected error occurred.' });
            });

    }
    return (
        <>
            {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}

            {/* Loading spinner overlay */}
            {loading && <div className="yp-loading-overlay"><div className="yp-spinner"><div className="spinner-border"></div></div></div>}

            
                <div className="d-flex flex-column flex-wrap">
                    <div className="d-flex flex-row flex-wrap align-items-center column-gap-3 mb-3 yp-pl-30-px yp-pr-30-px">
                        <div className="d-flex flex-grow-1 flex-shrink-0">
                            <div className="yp-fs-14-500 yp-text-dark-purple">Selected Activities ({selectedActivitiyList?.totalRows ?? 0})</div>
                        </div>
                        <div className="d-flex">
                            <a
                                className="yp-link-icon yp-link-icon-primary yp-link-icon-in-box"
                                onClick={() => handleSetActiveTab("tabSelectActivity")}
                                role="button"
                                style={{ cursor: "pointer" }}
                            >
                                <svg xmlns="http://www.w3.org/2000/svg" width="15" height="15" viewBox="0 0 15 15" fill="none">
                                    <path
                                        d="M1.56663 8.57143H2.42672C2.55867 8.57143 2.68522 8.62787 2.77853 8.72834C2.87183 8.8288 2.92425 8.96506 2.92425 9.10714V12.8571L13.2675 12.9262V1.9644L8.53208 1.89536C8.40013 1.89536 8.27358 1.83892 8.18027 1.73845C8.08697 1.63799 8.03455 1.50173 8.03455 1.35965V0.535714C8.03455 0.393634 8.08697 0.257373 8.18027 0.156907C8.27358 0.0564414 8.40013 0 8.53208 0H13.5074C13.9033 0 14.2829 0.169324 14.5628 0.470721C14.8427 0.772119 15 1.1809 15 1.60714V13.3929C15 13.8191 14.8427 14.2279 14.5628 14.5293C14.2829 14.8307 13.9033 15 13.5074 15H2.5617C2.16584 15 1.78619 14.8307 1.50627 14.5293C1.22636 14.2279 1.0691 13.8191 1.0691 13.3929V9.10714C1.0691 8.96506 1.12152 8.8288 1.21483 8.72834C1.30813 8.62787 1.43468 8.57143 1.56663 8.57143ZM0.746298 0H4.72655C5.39107 0 5.72317 0.802735 5.25518 1.30798L4.14413 2.44784L9.94247 7.96051C10.012 8.03517 10.0673 8.11156 10.1049 8.20923C10.1426 8.30691 10.162 8.39709 10.162 8.50285C10.162 8.60861 10.1426 8.69879 10.1049 8.79646C10.0673 8.89414 10.012 8.97053 9.94247 9.04518L9.23753 9.76712C9.1682 9.84204 9.08581 9.89322 8.9951 9.93378C8.90438 9.97434 8.80713 9.99232 8.7089 9.99232C8.61068 9.99232 8.51342 9.97434 8.42271 9.93378C8.33199 9.89322 8.24961 9.84204 8.18027 9.76712L2.38567 4.25317L1.27493 5.39143C0.80849 5.89366 0 5.56896 0 4.8491V0.765647C0 0.552527 0.0786276 0.374952 0.218585 0.224253C0.358543 0.073554 0.548368 0 0.746298 0Z"
                                        fill="#8B6CAB"
                                    />
                                </svg>
                            </a>
                        </div>
                    </div>
                    <div className="yp-mb-40-px p-0">
                        <div className="yp-custom-table-section yp-table-first-column-padding-left">
                            <CommonTable
                                type={COMMON_TABLE_TYPE.MANAGE_ONE_TIME_ASSIGNMENT_PREVIEW_CONTENT_SELECTED_ACTIVITY}
                                 tableConfig={MANAGE_ONE_TIME_ASSIGNMENT_PREVIEW_CONTENT_SELECTED_ACTIVITY_TABLE_CONFIG}

                                data={activities.slice(0, 5)}
                                onPageChange={() => { }}
                                currentPage={1}
                                totalRecords={activities.length}
                                pageSize={5}
                                isHidePagination={true}
                                isHideHeader={true}
                            />
                        </div>
                    </div>
                </div>

                <div className="d-flex flex-column flex-wrap">
                    <div className="d-flex flex-row flex-wrap align-items-center column-gap-3 mb-3  yp-pl-30-px yp-pr-30-px">
                        <div className="d-flex flex-grow-1 flex-shrink-0">
                            <div className="yp-fs-14-500 yp-text-dark-purple">Selected Users ({usersList?.totalRows ?? 0})</div>
                        </div>
                        <div className="d-flex">
                            <a
                                className="yp-link-icon yp-link-icon-primary yp-link-icon-in-box"
                                onClick={() => handleSetActiveTab("tabSelectUsers")}
                                role="button"
                                style={{ cursor: "pointer" }}
                            >
                                <svg xmlns="http://www.w3.org/2000/svg" width="15" height="15" viewBox="0 0 15 15" fill="none">
                                    <path
                                        d="M1.56663 8.57143H2.42672C2.55867 8.57143 2.68522 8.62787 2.77853 8.72834C2.87183 8.8288 2.92425 8.96506 2.92425 9.10714V12.8571L13.2675 12.9262V1.9644L8.53208 1.89536C8.40013 1.89536 8.27358 1.83892 8.18027 1.73845C8.08697 1.63799 8.03455 1.50173 8.03455 1.35965V0.535714C8.03455 0.393634 8.08697 0.257373 8.18027 0.156907C8.27358 0.0564414 8.40013 0 8.53208 0H13.5074C13.9033 0 14.2829 0.169324 14.5628 0.470721C14.8427 0.772119 15 1.1809 15 1.60714V13.3929C15 13.8191 14.8427 14.2279 14.5628 14.5293C14.2829 14.8307 13.9033 15 13.5074 15H2.5617C2.16584 15 1.78619 14.8307 1.50627 14.5293C1.22636 14.2279 1.0691 13.8191 1.0691 13.3929V9.10714C1.0691 8.96506 1.12152 8.8288 1.21483 8.72834C1.30813 8.62787 1.43468 8.57143 1.56663 8.57143ZM0.746298 0H4.72655C5.39107 0 5.72317 0.802735 5.25518 1.30798L4.14413 2.44784L9.94247 7.96051C10.012 8.03517 10.0673 8.11156 10.1049 8.20923C10.1426 8.30691 10.162 8.39709 10.162 8.50285C10.162 8.60861 10.1426 8.69879 10.1049 8.79646C10.0673 8.89414 10.012 8.97053 9.94247 9.04518L9.23753 9.76712C9.1682 9.84204 9.08581 9.89322 8.9951 9.93378C8.90438 9.97434 8.80713 9.99232 8.7089 9.99232C8.61068 9.99232 8.51342 9.97434 8.42271 9.93378C8.33199 9.89322 8.24961 9.84204 8.18027 9.76712L2.38567 4.25317L1.27493 5.39143C0.80849 5.89366 0 5.56896 0 4.8491V0.765647C0 0.552527 0.0786276 0.374952 0.218585 0.224253C0.358543 0.073554 0.548368 0 0.746298 0Z"
                                        fill="#8B6CAB"
                                    />
                                </svg>
                            </a>
                        </div>
                    </div>
                    <div className="yp-mb-40-px p-0">
                        <div className="yp-custom-table-section yp-table-first-column-padding-left">
                            <CommonTable
                                type={COMMON_TABLE_TYPE.MANAGE_ONE_TIME_ASSIGNMENT_PREVIEW_CONTENT_SELECTED_USER}
                                tableConfig={MANAGE_ONE_TIME_ASSIGNMENT_PREVIEW_CONTENT_SELECTED_USER_TABLE_CONFIG}
                                data={users.slice(0, 5)}
                                onPageChange={() => { }}
                                currentPage={1}
                                totalRecords={users.length}
                                pageSize={5}
                                isHidePagination={true}
                                isHideHeader={true}
                            />
                        </div>
                    </div>
                </div>

            <div className="yp-tab-content-padding">
                <div className="d-flex flex-row flex-wrap align-items-center column-gap-3 mb-3">
                    <div className="d-flex flex-grow-1 flex-shrink-0">
                        <div className="yp-fs-14-500 yp-text-dark-purple">Selected User Group</div>
                    </div>
                    <div className="d-flex">
                        <a
                            className="yp-link-icon yp-link-icon-primary yp-link-icon-in-box"
                            onClick={() => handleSetActiveTab("tabSelectUsers")}
                            role="button"
                            style={{ cursor: "pointer" }}
                        >
                            <svg xmlns="http://www.w3.org/2000/svg" width="15" height="15" viewBox="0 0 15 15" fill="none">
                                <path
                                    d="M1.56663 8.57143H2.42672C2.55867 8.57143 2.68522 8.62787 2.77853 8.72834C2.87183 8.8288 2.92425 8.96506 2.92425 9.10714V12.8571L13.2675 12.9262V1.9644L8.53208 1.89536C8.40013 1.89536 8.27358 1.83892 8.18027 1.73845C8.08697 1.63799 8.03455 1.50173 8.03455 1.35965V0.535714C8.03455 0.393634 8.08697 0.257373 8.18027 0.156907C8.27358 0.0564414 8.40013 0 8.53208 0H13.5074C13.9033 0 14.2829 0.169324 14.5628 0.470721C14.8427 0.772119 15 1.1809 15 1.60714V13.3929C15 13.8191 14.8427 14.2279 14.5628 14.5293C14.2829 14.8307 13.9033 15 13.5074 15H2.5617C2.16584 15 1.78619 14.8307 1.50627 14.5293C1.22636 14.2279 1.0691 13.8191 1.0691 13.3929V9.10714C1.0691 8.96506 1.12152 8.8288 1.21483 8.72834C1.30813 8.62787 1.43468 8.57143 1.56663 8.57143ZM0.746298 0H4.72655C5.39107 0 5.72317 0.802735 5.25518 1.30798L4.14413 2.44784L9.94247 7.96051C10.012 8.03517 10.0673 8.11156 10.1049 8.20923C10.1426 8.30691 10.162 8.39709 10.162 8.50285C10.162 8.60861 10.1426 8.69879 10.1049 8.79646C10.0673 8.89414 10.012 8.97053 9.94247 9.04518L9.23753 9.76712C9.1682 9.84204 9.08581 9.89322 8.9951 9.93378C8.90438 9.97434 8.80713 9.99232 8.7089 9.99232C8.61068 9.99232 8.51342 9.97434 8.42271 9.93378C8.33199 9.89322 8.24961 9.84204 8.18027 9.76712L2.38567 4.25317L1.27493 5.39143C0.80849 5.89366 0 5.56896 0 4.8491V0.765647C0 0.552527 0.0786276 0.374952 0.218585 0.224253C0.358543 0.073554 0.548368 0 0.746298 0Z"
                                    fill="#8B6CAB"
                                />
                            </svg>
                        </a>
                    </div>
                </div>
                <div className="d-flex flex-column flex-wrap">
                    {
                        !selectedBusinessRuleName || selectedBusinessRuleName === '0'
                            ? (
                                <div className="yp-fs-13-400 yp-color-565656">
                                    No user group selected
                                </div>
                            )
                            : (
                                <div className="row">
                                    <div className="col-12 col-md-6 col-lg-4">
                                        <div className="form-group mb-0">                                
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
                                        </div>
                                        {/* <div className="form-group mb-0">
                                            <div className="yp-form-control-wrapper">
                                                <select
                                                    className="form-control yp-form-control"
                                                    name="businessRuleSelected"
                                                    value={selectedBusinessRuleName}
                                                    disabled
                                                >
                                                    {selectedBusinessRuleName === '0' ? (
                                                        <option value="0" disabled>
                                                            No user group selected
                                                        </option>
                                                    ) : (
                                                        <option value={selectedBusinessRuleName}>{selectedBusinessRuleName}</option>
                                                    )}
                                                </select>
                                                <label className="form-label">Select User Group</label>
                                            </div>
                                        </div> */}

                                    </div>
                                </div>
                            )
                }
                </div>
                <hr className="yp-hr yp-hr-30-0" />

                <div className="d-flex flex-row flex-wrap align-items-center column-gap-3 mb-4">
                    <div className="d-flex flex-grow-1 flex-shrink-0">
                        <div className="yp-fs-14-500 yp-text-dark-purple">Defined Properties</div>
                    </div>
                    <div className="d-flex">
                        <a
                            className="yp-link-icon yp-link-icon-primary yp-link-icon-in-box"
                            onClick={() => handleSetActiveTab("tabDefineProperties")}
                            role="button"
                            style={{ cursor: "pointer" }}
                        >
                            <svg xmlns="http://www.w3.org/2000/svg" width="15" height="15" viewBox="0 0 15 15" fill="none">
                                <path
                                    d="M1.56663 8.57143H2.42672C2.55867 8.57143 2.68522 8.62787 2.77853 8.72834C2.87183 8.8288 2.92425 8.96506 2.92425 9.10714V12.8571L13.2675 12.9262V1.9644L8.53208 1.89536C8.40013 1.89536 8.27358 1.83892 8.18027 1.73845C8.08697 1.63799 8.03455 1.50173 8.03455 1.35965V0.535714C8.03455 0.393634 8.08697 0.257373 8.18027 0.156907C8.27358 0.0564414 8.40013 0 8.53208 0H13.5074C13.9033 0 14.2829 0.169324 14.5628 0.470721C14.8427 0.772119 15 1.1809 15 1.60714V13.3929C15 13.8191 14.8427 14.2279 14.5628 14.5293C14.2829 14.8307 13.9033 15 13.5074 15H2.5617C2.16584 15 1.78619 14.8307 1.50627 14.5293C1.22636 14.2279 1.0691 13.8191 1.0691 13.3929V9.10714C1.0691 8.96506 1.12152 8.8288 1.21483 8.72834C1.30813 8.62787 1.43468 8.57143 1.56663 8.57143ZM0.746298 0H4.72655C5.39107 0 5.72317 0.802735 5.25518 1.30798L4.14413 2.44784L9.94247 7.96051C10.012 8.03517 10.0673 8.11156 10.1049 8.20923C10.1426 8.30691 10.162 8.39709 10.162 8.50285C10.162 8.60861 10.1426 8.69879 10.1049 8.79646C10.0673 8.89414 10.012 8.97053 9.94247 9.04518L9.23753 9.76712C9.1682 9.84204 9.08581 9.89322 8.9951 9.93378C8.90438 9.97434 8.80713 9.99232 8.7089 9.99232C8.61068 9.99232 8.51342 9.97434 8.42271 9.93378C8.33199 9.89322 8.24961 9.84204 8.18027 9.76712L2.38567 4.25317L1.27493 5.39143C0.80849 5.89366 0 5.56896 0 4.8491V0.765647C0 0.552527 0.0786276 0.374952 0.218585 0.224253C0.358543 0.073554 0.548368 0 0.746298 0Z"
                                    fill="#8B6CAB"
                                />
                            </svg>
                        </a>
                    </div>
                </div>

                <div className="d-flex flex-column flex-wrap">
                    <div className="d-flex mb-3">
                        <div className="yp-fs-13-500 yp-text-dark-purple">Assignment Date:</div>
                    </div>
                    {/* Absolute Date */}
                    {definePropertiesList?.AssignmentDateText && (
                        <div className="d-flex flex-wrap">
                            <span className="yp-fs-13-400 yp-color-565656 me-1">Absolute Date:</span>
                            <span className="yp-fs-13-500 yp-color-565656">
                                {format(parseISO(definePropertiesList.AssignmentDateText), 'dd/MM/yyyy')}
                            </span>
                        </div>
                    )}

                    {/* Relative Date */}
                    {(definePropertiesList?.AssignAfterDaysOf || definePropertiesList?.IsAssignmentBasedOnCreationDate || definePropertiesList?.IsAssignmentBasedOnHireDate) && (
                        <div className="d-flex flex-wrap">
                            <span className="yp-fs-13-400 yp-color-565656 me-1">Relative Date:</span>
                            <span className="yp-fs-13-500 yp-color-565656">
                                {definePropertiesList?.AssignAfterDaysOf || 0} Days After{' '}
                                {definePropertiesList?.IsAssignmentBasedOnCreationDate
                                    ? 'Users Record Creation Date'
                                    : definePropertiesList?.IsAssignmentBasedOnHireDate
                                        ? 'Hire Date'
                                        : ''}
                            </span>
                        </div>
                    )}
                </div>
                <hr className="yp-hr yp-hr-30-0" />

                <div className="d-flex flex-column flex-wrap">
                    <div className="d-flex mb-3">
                        <div className="yp-fs-13-500 yp-text-dark-purple">Due Date:</div>
                    </div>
                    {/* Absolute Date */}
                    {definePropertiesList?.DueDateText ? (
                        <div className="d-flex flex-wrap">
                            <span className="yp-fs-13-400 yp-color-565656 me-1">Absolute Date:</span>
                            <span className="yp-fs-13-500 yp-color-565656">
                                {format(parseISO(definePropertiesList.DueDateText), 'dd/MM/yyyy')}
                            </span>
                        </div>
                    ) : null}
                    {/* Relative Date */}
                    {definePropertiesList?.DueAfterDaysOf > 0 && (
                        <div className="d-flex flex-wrap">
                            <span className="yp-fs-13-400 yp-color-565656 me-1">Relative Date:</span>
                            <span className="yp-fs-13-500 yp-color-565656">
                                {definePropertiesList.DueAfterDaysOf} Days{' '}
                                {definePropertiesList.IsDueBasedOnAssignDate
                                    ? 'After Assignment Date'
                                    : definePropertiesList.IsDueBasedOnStartDate
                                        ? 'After Assignment Start Date'
                                        : definePropertiesList.IsDueBasedOnHireDate
                                            ? 'After Hire Date'
                                            : definePropertiesList.IsDueBasedOnCreationDate
                                                ? 'After Users Record Creation Date<'
                                                : definePropertiesList.IsDueBasedOnExpiryDate
                                                    ? 'Before Expiry Date'
                                                    : ''}
                            </span>
                        </div>
                    )}
                    {/* No Due Date */}
                    {definePropertiesList?.IsNoDueDate && (
                        <div className="d-flex flex-wrap">
                            <span className="yp-fs-13-500 yp-color-565656">No Due Date</span>
                        </div>
                    )}
                </div>
                <hr className="yp-hr yp-hr-30-0" />

                <div className="d-flex flex-column flex-wrap">
                    <div className="d-flex mb-3">
                        <div className="yp-fs-13-500 yp-text-dark-purple">Expiry Date:</div>
                    </div>
                    {/* Absolute Expiry Date */}
                    {definePropertiesList?.ExpiryDateText && (
                        <div className="d-flex flex-wrap">
                            <span className="yp-fs-13-400 yp-color-565656 me-1">Absolute Date:</span>
                            <span className="yp-fs-13-500 yp-color-565656">
                                {format(parseISO(definePropertiesList.ExpiryDateText), 'dd/MM/yyyy')}
                            </span>
                        </div>
                    )}
                    {/* Relative Expiry Date */}
                    {definePropertiesList?.ExpireAfterDaysOf && Number(definePropertiesList.ExpireAfterDaysOf) > 0 && (
                        <div className="d-flex flex-wrap">
                            <span className="yp-fs-13-400 yp-color-565656 me-1">Relative Date:</span>
                            <span className="yp-fs-13-500 yp-color-565656">
                                {definePropertiesList.ExpireAfterDaysOf} Days After {
                                    definePropertiesList.IsExpiryBasedOnAssignDate
                                        ? 'Assignment Date'
                                        : definePropertiesList.IsExpiryBasedOnDueDate
                                            ? 'Assignment Due Date'
                                            : definePropertiesList.IsExpiryBasedOnStartDate
                                                ? 'Assignment Start Date'
                                                : ''
                                }
                            </span>
                        </div>
                    )}

                    {/* No Expiry Date */}
                    {definePropertiesList?.IsNoExpiryDate && (
                        <div className="d-flex flex-wrap">
                            <span className="yp-fs-13-500 yp-color-565656">No Expiry Date</span>
                        </div>
                    )}
                </div>
                <hr className="yp-hr yp-hr-30-0" />

                <div className="d-flex flex-column flex-wrap mb-3">
                    <div className="d-flex flex-wrap">
                        <span className="yp-fs-13-400 yp-color-565656 me-1">Reassign selected activities to selected users:</span>
                        <span className="yp-fs-13-500 yp-color-565656"> {definePropertiesList?.IsReassignmentChecked ? "Yes" : "No"}</span>
                    </div>
                </div>
                <div className="d-flex flex-column flex-wrap">
                    <div className="d-flex flex-wrap">
                        <span className="yp-fs-13-400 yp-color-565656 me-1">Email notification:</span>
                        <span className="yp-fs-13-500 yp-color-565656">{definePropertiesList?.IsMailChecked ? "Yes" : "No"}</span>
                    </div>
                </div>
                <hr className="yp-hr yp-hr-30-0" />
                <div className="yp-spacer"></div>
                
                <div className="row">
                    <div className="col-12">
                        <div className="yp-form-button-row mt-3">
                            <button className="btn btn-primary" onClick={handleSubmit}>Save</button>
                            <button className="btn btn-secondary" onClick={() => handleSetActiveTab("tabDefineProperties")}>Back</button>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
};

export default PreviewContent;