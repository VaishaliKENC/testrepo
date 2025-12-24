import React, { useEffect, useState } from "react";
import CustomBreadcrumb from "../../../components/shared/CustomBreadcrumb";
import { AdminPageRoutes } from "../../../utils/Constants/Admin_PageRoutes";
import RowEditableCommonTable from "../../../components/shared/CommonComponents/RowEditableCommonTable";
import { useAppDispatch, useAppSelector } from "../../../hooks";
import { RootState } from "../../../redux/store";
import ConfirmationModal from "../../../components/shared/ConfirmationModal";
import { User } from "../../../Types/authTypes";
import { configureProfileData, editConfigureProfileData, importDefinationSendMail } from "../../../redux/Slice/admin/userSlice";
import { useNavigate } from "react-router-dom";
import { useSelector } from "react-redux";
import AlertMessage from '../../../components/shared/AlertMessage';

/**
 * React functional component for configuring profile definitions.
 * Displays user profile configurations and allows editing.
 */
const ConfigureProfileDefinition: React.FC = () => {

    const dispatch = useAppDispatch();
    const [users, setUsers] = useState<any>([]);
    const { loading, error } = useAppSelector((state: RootState) => state.user);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(5);
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
    const [onConfirmAction, setOnConfirmAction] = useState<() => void>(() => () => { });

    const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);

    const clientId: any = useSelector((state: RootState) => state.auth.clientId);
    const id: any = useSelector((state: RootState) => state.auth.id);
    const configureUserList: any = useSelector((state: RootState) => state.user.profileUser);
    const navigate = useNavigate();

    /** Breadcrumb navigation items*/
    const breadcrumbItems = [
        { iconClass: 'fa-classic fa-solid fa-house', path: AdminPageRoutes.ADMIN_DASHBOARD.FULL_PATH },
        { label: 'Configure Profile Definition' },
    ];
    const pageTitle: string = 'Configure Profile Definition';
    document.title = pageTitle;

    const [formData, setFormData] = useState({
        emailId: '',
    });

    /**
       * Fetches profile configuration data when `id` or `clientId` changes.
       */
    useEffect(() => {
        if (!id || !clientId) return;
        const params = {
            id: id,
            clientId: clientId,
            listRange: {
                pageIndex: 0,
                pageSize: 0,
                sortExpression: 'MinLength',
            }
        } as const;

        dispatch(configureProfileData(params));
    }, [id, clientId]);


    /**
   * Updates the `users` state when `configureUserList` changes.
   * If the list is empty, resets `users` state to an empty array.
   */
    useEffect(() => {
        if (!configureUserList?.importDefinationList?.length) {
            setUsers([])
            return
        };
        setUsers(configureUserList?.importDefinationList)
    }, [configureUserList])

    // const handleSearch = (keyword: string) => {
    //     const params = {
    //         id, clientId,
    //         listRange: { pageIndex: currentPage, pageSize, sortExpression: 'LastModifiedDate desc' },
    //         keyWord: keyword
    //     };
    //     dispatch(configureProfileData(params));
    // };

    // const handlePageChange = (page: number, newPageSize?: number) => {
    //     if (newPageSize) setPageSize(newPageSize); // Update page size if provided
    //     setCurrentPage(page); // Update the current page state

    //     const params = {
    //         id: id,
    //         clientId: clientId,
    //         listRange: {
    //             pageIndex: page,
    //             pageSize: newPageSize || pageSize,
    //             sortExpression: 'DateCreated desc',
    //             requestedById: "USR7cbc556be7f64ac8",
    //         },
    //         keyWord: ""
    //     };
    //     dispatch(configureProfileData(params));
    // };

    /**
     * Handles the edit action for a user profile configuration.
     * Filters the data to keep only relevant fields before dispatching the edit action.
     * After a successful edit, fetches the updated list of profile configurations.
     * @param {any} data - The data to be updated.
     */
    const handleEditClick = async (data: any) => {
        const keysToKeep = [
            "id",
            "clientId",
            "fieldName",
            "minLength",
            "maxLength",
            "maxLengthInDB",
            "fieldValueType",
            "fieldTypes",
            "allowBlank",
            "fieldErrorLevel",
            "include",
            "isMandatory",
            "isDefault",
            "defaultValue",
            "importAction",
            "fieldDataType",
            "errorLevelType"
        ];

        // Filter the object
        const filtered: any = Object.fromEntries(
            Object.entries(data).filter(([key]) => keysToKeep.includes(key))
        );

        // Check validation: If isDefault is true and defaultValue is empty, show alert and return
        if (filtered.isDefault === true && !filtered.defaultValue) {
            setAlert({ type: "warning", message: "Please enter Default Value." });
            return;
        }

        try {
            // Dispatch editConfigureProfileData and wait for its success
            await dispatch(editConfigureProfileData(filtered)).unwrap();

            // Dispatch configureProfileData after successful edit
            const params = {
                id: id,
                clientId: clientId,
                listRange: {
                    pageIndex: 0,
                    pageSize: 0,
                    sortExpression: 'MinLength',
                },
            };
            dispatch(configureProfileData(params));

        } catch (error) {
            console.error("Error in editConfigureProfileData:", error);
        }
    }

    const [showSendEmailSection, setShowSendEmailSection] = useState(false);
    const handleShowHideEmailSection = (param:boolean) => {
        setShowSendEmailSection(param);
    }

    const [formErrors, setFormErrors] = useState<Record<string, string>>({});
    const validateField = (name: string, value: string): string => {
        if (typeof value === 'string') {
            if (value.trim() && !isValidInput(value)) {
                return "Special characters like < > ' & are not allowed.";
            }
        }
        switch (name) {
            case "emailId":
                const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                if (typeof value === "string" && !value.trim())
                    return "Email ID is required";
                if (value && !emailRegex.test(value))
                    return "Invalid email format";
                break;
            default:
                break;
        }
        return "";
    };

    const isValidInput = (value: string): boolean => {
        const regx = /^[^'<>&]+$/; // Used in YP
        return regx.test(value);
    };

    // Validate all fields function
    const validateAllFields = (): Record<string, string> => {
        const errors: Record<string, string> = {};

        Object.keys(formData).forEach((key) => {
            const error = validateField(key, (formData as any)[key]);
            if (error) errors[key] = error;
        });

        return errors;
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const target = e.target as HTMLInputElement;
        const { name, value, type } = e.target as HTMLInputElement;

        let newValue: any = value;
        setFormData({
            ...formData,
            [name]: newValue,
        });

        // Validate the field
        const error = validateField(name, value);
        // Update form errors
        setFormErrors({
            ...formErrors,
            [name]: error,
        });
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        // Validate all fields
        const errors = validateAllFields();

        if (Object.keys(errors).length > 0) {
            setFormErrors(errors); // Set errors to display
            return; // Stop form submission
        }

        dispatch(importDefinationSendMail({ clientId: clientId, currentUserId: id, emailId: formData.emailId }))
        .then(async (response: any) => { // <-- make this async
            if (response.meta.requestStatus === "fulfilled") {
                const params = {
                    id: id,
                    clientId: clientId,
                    listRange: {
                        pageIndex: 0,
                        pageSize: 0,
                        sortExpression: 'MinLength',
                    },
                };
                await dispatch(configureProfileData(params)); 

                setAlert({ type: "success", message: "The email has been sent successfully." });                    
                setFormData({
                    emailId: '',
                });
                setTimeout(() => {
                    navigate(`/redirect?to=${encodeURIComponent(AdminPageRoutes.ADMIN_CONFIGURE_PROFILE_DEFINITION.FULL_PATH)}`);
                }, 2000);
            } else {
                setAlert({ type: "error", message: 'Email sent failed: ' + response.msg });
            }
        })
        .catch((error) => {
            console.error("Email sent error:", error);
            setAlert({ type: "error", message: 'An unexpected error occurred.' });
        });
    };


    return (
        <>
            {/* Loading spinner overlay */}
            {loading && <div className="yp-loading-overlay"><div className="yp-spinner"><div className="spinner-border"></div></div></div>}

            <div className="yp-page-title-button-section" id="yp-page-title-breadcrumb-section">
                <div className="yp-page-title-breadcrumb">
                    <div className="yp-page-title">{pageTitle}</div>
                    <CustomBreadcrumb items={breadcrumbItems} />
                </div>
                <div className="yp-page-button">
                    {
                        !showSendEmailSection && (
                            <>
                                <button type="button" 
                                    className="btn btn-primary"
                                    onClick={() => {
                                        handleShowHideEmailSection(true);
                                    }}>
                                    <i className="fa-solid fa-envelope"></i> Email
                                </button>
                            </>
                        )
                    }
                    
                    <div className={`yp-config-profile-top-email-section ${showSendEmailSection ? "open" : ""}`}>
                        <div className="d-flex flex-wrap justify-content-start align-items-start gap-2">
                            <div className="yp-config-profile-top-email-section-label">
                                <span className="yp-fs-13-400 yp-text-565656">                                    
                                    Send Email Profile Definition File to:
                                </span>
                            </div>
                            <div className="yp-config-profile-top-email-section-input yp-width-315-px">
                                <div className="form-group mb-0">
                                    <div className="yp-form-control-wrapper">
                                        <input type="email"
                                            name="emailId"
                                            id=""
                                            value={formData.emailId}
                                            // onChange={handleInputChange}
                                            onChange={(e) => {
                                                const value = e.target.value;
                                                // Prevent leading space
                                                if (formData.emailId === '' && value.startsWith(' ')) return;
                                                handleInputChange(e);
                                            }}
                                            onKeyDown={(e) => {
                                                // Prevent space as first character
                                                if (e.key === ' ' && formData.emailId.length === 0) { e.preventDefault(); }
                                            }}
                                            className="form-control yp-form-control yp-form-control-with-button"
                                            //placeholder="Please enter Email Address" 
                                            placeholder=""
                                            />
                                            <label className="form-label">Please enter Email Address</label>

                                        <button type="button" className="btn btn-primary yp-btn-inside-input yp-btn-inside-input-right" onClick={handleSubmit}>Send</button>
                                    </div>
                                    <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                        {formErrors.emailId && <div className="invalid-feedback">{formErrors.emailId}</div>}
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                    </div>
                </div>
            </div>

            <div className='yp-card p-0 pb-4' id="yp-card-main-content-section">
                <RowEditableCommonTable
                    usersProfile={users}
                    onSaveClick={handleEditClick}
                // onSearch={handleSearch}
                // onPageChange={handlePageChange}
                // currentPage={currentPage}
                // totalRecords={configureUserList?.totalRows}
                // pageSize={pageSize}
                />

                {/* Email input section */}
                {/* <div className="mt-4 mx-3">
                    <div className="yp-well">
                        
                    </div>
                </div> */}

                {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}

                {/* Confirmation Modal */}
                <ConfirmationModal
                    show={showModal}
                    onConfirm={onConfirmAction}
                    onCancel={() => setShowModal(false)}
                    modalConfig={modalConfig}
                />
            </div>

        </>
    );
};

export default ConfigureProfileDefinition;