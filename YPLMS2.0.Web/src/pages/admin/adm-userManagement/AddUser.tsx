import React, { useEffect, useRef, useState } from 'react';
import CustomBreadcrumb from '../../../components/shared/CustomBreadcrumb';
import { AdminPageRoutes } from '../../../utils/Constants/Admin_PageRoutes';
import { IUserAddUserPayload, IMainPayload } from '../../../Types/commonTableTypes';
import { addUserGetclientFields, addUsers, fetchSingleUserData, updateUser } from '../../../redux/Slice/admin/userSlice';
import { checkavailablity } from '../../../redux/Slice/authSlice';
import { useAppDispatch, useAppSelector } from '../../../hooks';
import { useNavigate, useParams } from 'react-router-dom';
import { RootState } from '../../../redux/store';
import { useSelector } from 'react-redux';
import { isGetFieldMaxLength, isFieldAllowBlanks, isFieldIncluded, isGetFieldErrorMessage, isGetFieldLengthErrorMessage, isGetDefaultValue } from '../../../utils/fieldUtils';
import { FIELDS_ENUM } from '../../../utils/Constants/Enums';
import AlertMessage from '../../../components/shared/AlertMessage';

let debounceTimer: any;

const AddUser: React.FC = () => {
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    // const [ClientId, setClientID] = useState('CLIYPRevamp_New');
    // const [ClientId, setClientID] = useState('CLIYPRevamp_QA');
    const [UserNameAlias, setLoginID] = useState('');
    const [pageSize, setPageSize] = useState(5)
    const clientId: any = useSelector((state: RootState) => state.auth.clientId);

    const { fields } = useSelector((state: RootState) => state.user);
    const { userId } = useParams<{ userId: string }>();
    const { loading } = useAppSelector((state: RootState) => state.user);

    let isAddForm: boolean = userId ? false : true;
    const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);
    const isResettingRef = useRef(false);

    const breadcrumbItems = [
        { iconClass: 'fa-classic fa-solid fa-house', path: AdminPageRoutes.ADMIN_DASHBOARD.FULL_PATH },
        { label: 'Manage Users', path: AdminPageRoutes.ADMIN_MANAGE_USER.FULL_PATH },
        { label: isAddForm ? 'Add Users' : 'Edit Users' },
    ];
    const pageTitle: string = isAddForm ? 'Add Users' : 'Edit Users';
    document.title = pageTitle;

    const [formData, setFormData] = useState({
        id: '',
        firstName: '',
        lastName: '',
        emailID: '',
        userNameAlias: '',
        userNameAliasHidden: '',
        userPassword: '',
        confirmPassword: '',
        defaultLanguageId: 'en-US',
        isActive: '',
        managerEmailId: '',
        isSendEmail: false,
        //emailOption: '',
        //sendDirectEmail: false,
    });

    useEffect(() => {
        if (isResettingRef.current) return;

        if (!userId && fields.length > 0) {
            setFormData(prevData => ({
                ...prevData,
                id: '',
                firstName: isGetDefaultValue(fields, FIELDS_ENUM.FIRST_NAME),
                lastName: isGetDefaultValue(fields, FIELDS_ENUM.LAST_NAME),
                emailID: isGetDefaultValue(fields, FIELDS_ENUM.EMAIL_ID),
                userNameAlias: isGetDefaultValue(fields, FIELDS_ENUM.LOGIN_ID),
                userNameAliasHidden: isGetDefaultValue(fields, "userNameAlias"),
                userPassword: isGetDefaultValue(fields, FIELDS_ENUM.USER_PASSWORD),
                confirmPassword: isGetDefaultValue(fields, FIELDS_ENUM.USER_PASSWORD),
                defaultLanguageId: isGetDefaultValue(fields, FIELDS_ENUM.DEFAULT_LANGUAGE_ID) || 'en-US',
                isActive: isGetDefaultValue(fields, FIELDS_ENUM.IS_ACTIVE),
                managerEmailId: isGetDefaultValue(fields, FIELDS_ENUM.MANAGER_EMAIL_ID),
                isSendEmail: false,
            }));
        }
    }, [fields]); // Runs when fields are updated

    const handleReset = () => {
        isResettingRef.current = true;
        setFormData({
            id: '',
            firstName: isGetDefaultValue(fields, FIELDS_ENUM.FIRST_NAME),
            lastName: isGetDefaultValue(fields, FIELDS_ENUM.LAST_NAME),
            emailID: isGetDefaultValue(fields, FIELDS_ENUM.EMAIL_ID),
            userNameAlias: isGetDefaultValue(fields, FIELDS_ENUM.LOGIN_ID),
            userNameAliasHidden: isGetDefaultValue(fields, "userNameAlias"),
            userPassword: isGetDefaultValue(fields, FIELDS_ENUM.USER_PASSWORD),
            confirmPassword: isGetDefaultValue(fields, FIELDS_ENUM.USER_PASSWORD),
            defaultLanguageId: isGetDefaultValue(fields, FIELDS_ENUM.DEFAULT_LANGUAGE_ID) || 'en-US',
            isActive: isGetDefaultValue(fields, FIELDS_ENUM.IS_ACTIVE),
            managerEmailId: isGetDefaultValue(fields, FIELDS_ENUM.MANAGER_EMAIL_ID),
            isSendEmail: false,
        });

        setFormErrors({});
        setFormSuccess({});

        // Clear guard after React finishes updating state
        setTimeout(() => {
            isResettingRef.current = false;
        }, 100);
    };


    // const handleReset = () => {
    //     setFormData({
    //         id: '',
    //         firstName: '',
    //         lastName: '',
    //         emailID: '',
    //         userNameAlias: '',
    //         userNameAliasHidden: '',
    //         userPassword: '',
    //         confirmPassword: '',
    //         defaultLanguageId: 'en-US',
    //         isActive: '',
    //         managerEmailId: '',
    //         isSendEmail: false,
    //         //emailOption: '',
    //         //sendDirectEmail: false,
    //     });
    // };

    useEffect(() => {
        const params = {
            clientId, listRange: { pageIndex: 0, pageSize: 0, sortExpression: 'MinLength' }
        } as const;
        dispatch(addUserGetclientFields(params));
    }, [clientId]);

    useEffect(() => {
        if (userId) {
            const userSinglePayload: IMainPayload = {
                id: userId,
                clientId: clientId
            }

            dispatch(fetchSingleUserData(userSinglePayload))
                .then((response: any) => {
                    // If signup is successful, navigate to the login
                    if (response.meta.requestStatus == "fulfilled") {
                        // console.log("Fetched User Data:", response.payload.learner);
                        // setFormData({
                        //     id: response.payload.learner.id,
                        //     firstName: response.payload.learner.firstName,
                        //     lastName: response.payload.learner.lastName,
                        //     emailID: response.payload.learner.emailID,
                        //     userNameAlias: response.payload.learner.userNameAlias,
                        //     userNameAliasHidden: response.payload.learner.userNameAlias,
                        //     userPassword: response.payload.learner.userPassword,
                        //     confirmPassword: response.payload.learner.userPassword,
                        //     defaultLanguageId: response.payload.learner.defaultLanguageId,
                        //     isActive: response.payload.learner.isActive.toString(),
                        //     managerEmailId: response.payload.learner.managerEmailId,
                        //     isSendEmail: response.payload.learner.isSendEmail,
                        // });

                        setFormData({
                            id: response.payload.learner.id || '',
                            firstName: response.payload.learner.firstName || isGetDefaultValue(fields, FIELDS_ENUM.FIRST_NAME),
                            lastName: response.payload.learner.lastName || isGetDefaultValue(fields, FIELDS_ENUM.LAST_NAME),
                            emailID: response.payload.learner.emailID || isGetDefaultValue(fields, FIELDS_ENUM.EMAIL_ID),
                            userNameAlias: response.payload.learner.userNameAlias || isGetDefaultValue(fields, FIELDS_ENUM.LOGIN_ID),
                            userNameAliasHidden: response.payload.learner.userNameAlias || isGetDefaultValue(fields, "userNameAlias"),
                            userPassword: response.payload.learner.userPassword || isGetDefaultValue(fields, FIELDS_ENUM.USER_PASSWORD),
                            confirmPassword: response.payload.learner.userPassword || isGetDefaultValue(fields, FIELDS_ENUM.USER_PASSWORD),
                            defaultLanguageId: response.payload.learner.defaultLanguageId || isGetDefaultValue(fields, FIELDS_ENUM.DEFAULT_LANGUAGE_ID) || 'en-US',
                            isActive: response.payload.learner.isActive !== null ? response.payload.learner.isActive.toString() : isGetDefaultValue(fields, FIELDS_ENUM.IS_ACTIVE),
                            managerEmailId: response.payload.learner.managerEmailId || isGetDefaultValue(fields, FIELDS_ENUM.MANAGER_EMAIL_ID),
                            isSendEmail: response.payload.learner.isSendEmail !== undefined ? response.payload.learner.isSendEmail : false,
                        });

                        // console.log("Updated formData:", formData);

                    } else {
                        setAlert({ type: "error", message: 'User data fetch failed: ' + response.msg });
                    }
                })
                .catch((error) => {
                    console.error("User data fetch error:", error);
                    setAlert({ type: "error", message: 'An unexpected error occurred.' });
                });
        }
    }, [userId]);

    const [formErrors, setFormErrors] = useState<Record<string, string>>({});
    const [formSuccess, setFormSuccess] = useState<Record<string, string>>({});


    // Common validation function
    const validateField = (name: string, value: string): string => {
         if (isResettingRef.current) return "";
        if (!fields.length) { return ""; }

        const errorMessage = isGetFieldErrorMessage(fields, name);
        const lengthErrorMessage = isGetFieldLengthErrorMessage(fields, name);
        const maxLength = isGetFieldMaxLength(fields, name);
        //setFormSuccess((prev) => ({ ...prev, userNameAlias: '' }));
        setFormSuccess({});
        // const fieldData = fields.find((field: any) => field.fieldName.toLowerCase() === name.toLowerCase());

        if (typeof value === 'string') {
            if (value.trim() && !isValidInput(value)) {
                return "Special characters like < > ' & are not allowed.";
            }
        }

        switch (name) {
            case "firstName":
                if (isFieldIncluded(fields, FIELDS_ENUM.FIRST_NAME) && !isFieldAllowBlanks(fields, FIELDS_ENUM.FIRST_NAME) && !value.trim()) return errorMessage || "First name is required";
                if (value.length > maxLength) return lengthErrorMessage || `Maximum ${maxLength} characters allowed`;
                break;
            case "lastName":
                if (isFieldIncluded(fields, FIELDS_ENUM.LAST_NAME) && !isFieldAllowBlanks(fields, FIELDS_ENUM.LAST_NAME) && !value.trim()) {
                    return errorMessage || "Last name is required";
                }
                if (value.length > maxLength) return lengthErrorMessage || `Maximum ${maxLength} characters allowed`;
                break;
            case "emailID":
                if (isFieldIncluded(fields, FIELDS_ENUM.EMAIL_ID)) {
                    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                    if (!isFieldAllowBlanks(fields, FIELDS_ENUM.EMAIL_ID) && !value.trim()) {
                        return errorMessage || "Email is required";
                    }
                    if (value.trim() && !emailRegex.test(value.trim())) {
                        // if (value && !emailRegex.test(value)) {
                        return "Invalid email format";
                    }
                }
                break;
            case "userNameAlias":
                if (isFieldIncluded(fields, FIELDS_ENUM.LOGIN_ID) && !isFieldAllowBlanks(fields, FIELDS_ENUM.LOGIN_ID) && !value.trim()) {
                    return errorMessage || "Login ID is required";
                }
                break;
            case "userPassword":
                if (isFieldIncluded(fields, FIELDS_ENUM.USER_PASSWORD) && !isFieldAllowBlanks(fields, FIELDS_ENUM.USER_PASSWORD) && !value.trim()) {
                    return errorMessage || "Password is required";
                }
                break;
            case "confirmPassword":
                if (isFieldIncluded(fields, FIELDS_ENUM.USER_PASSWORD) && !isFieldAllowBlanks(fields, FIELDS_ENUM.USER_PASSWORD)) {
                    if (!value.trim()) return "Please enter Confirm Password";
                    if (formData.userPassword && formData.userPassword !== value) return "Passwords do not match";
                }
                break;
            case "managerEmailId":
                if (isFieldIncluded(fields, FIELDS_ENUM.MANAGER_EMAIL_ID)) {
                    const managerEmailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                    if (!isFieldAllowBlanks(fields, FIELDS_ENUM.MANAGER_EMAIL_ID) && !value.trim()) {
                        return errorMessage || "Manager email is required";
                    }
                    if (value && !managerEmailRegex.test(value)) {
                        return "Invalid manager email format";
                    }
                }
                break;
            case "isActive":
                if (isFieldIncluded(fields, FIELDS_ENUM.IS_ACTIVE) && !isFieldAllowBlanks(fields, FIELDS_ENUM.IS_ACTIVE) && !value.trim()) {
                    return errorMessage || "Is Active is required";
                }
                break;
            case "defaultLanguageId":
                if (isFieldIncluded(fields, FIELDS_ENUM.DEFAULT_LANGUAGE_ID) && !isFieldAllowBlanks(fields, FIELDS_ENUM.DEFAULT_LANGUAGE_ID) && !value.trim()) {
                    return errorMessage || "Default Language Id is required";
                }
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

    const validateFieldOnChange = (name: string, value: string): string => {
        if (!fields.length) { return ""; }

        const lengthErrorMessage = isGetFieldLengthErrorMessage(fields, name);

        switch (name) {
            case "firstName":
                if (!value) return "";
                if (value.length < isGetFieldMaxLength(fields, FIELDS_ENUM.FIRST_NAME)) return "";
                if (value.length > isGetFieldMaxLength(fields, FIELDS_ENUM.FIRST_NAME)) return lengthErrorMessage || `Maximum ${isGetFieldMaxLength(fields, FIELDS_ENUM.FIRST_NAME)} characters allowed`;
                break;
            case "lastName":
                if (!value) return "";
                if (value.length < isGetFieldMaxLength(fields, FIELDS_ENUM.LAST_NAME)) return "";
                if (value.length > isGetFieldMaxLength(fields, FIELDS_ENUM.LAST_NAME)) return lengthErrorMessage || `Maximum ${isGetFieldMaxLength(fields, FIELDS_ENUM.LAST_NAME)} characters allowed`;
                break;
            case "userNameAlias":
                if (!value) return "";
                if (value.length < isGetFieldMaxLength(fields, FIELDS_ENUM.LOGIN_ID)) return "";
                if (value.length > isGetFieldMaxLength(fields, FIELDS_ENUM.LOGIN_ID)) return lengthErrorMessage || `Maximum ${isGetFieldMaxLength(fields, FIELDS_ENUM.LOGIN_ID)} characters allowed`;
                break;
            default:
                break;
        }
        return "";
    };

    // const validateField = (name: string, value: string): string => {
    //     switch (name) {
    //         case "firstName":
    //             if (!value.trim()) return "First name is required";
    //             break;
    //         case "lastName":
    //             if (!value.trim()) return "Last name is required";
    //             break;
    //         case "emailID":
    //             // if (!value.trim()) return "Email is required";
    //             const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    //             if (value && !emailRegex.test(value)) return "Invalid email format";
    //             break;
    //         case "userNameAlias":
    //             if (!value.trim()) return "Login ID is required";
    //             break;
    //         case "userPassword":
    //             if (!value.trim()) return "Password is required";
    //             break;
    //         case "confirmPassword":
    //             if (!value.trim()) return "Confirm Password is required";
    //             if (formData.userPassword !== value) return "Passwords do not match";
    //             break;
    //         case "managerEmailId":
    //             const managerEmailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    //             if (value && !managerEmailRegex.test(value)) return "Invalid manager email format";
    //             break;
    //         default:
    //             break;
    //     }
    //     return ""; // No error
    // };


    // Validate all fields function
    const validateAllFields = (): Record<string, string> => {
        const errors: Record<string, string> = {};

        Object.keys(formData).forEach((key) => {
            const error = validateField(key, (formData as any)[key]);
            if (error) errors[key] = error;
        });

        return errors;
    };

    const validateAllFieldsOnChange = (): Record<string, string> => {
        const errors: Record<string, string> = {};

        Object.keys(formData).forEach((key) => {
            const error = validateFieldOnChange(key, (formData as any)[key]);
            if (error) errors[key] = error;
        });

        return errors;
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
         if (isResettingRef.current) return;
        const target = e.target as HTMLInputElement | HTMLSelectElement;
        const { name, value, type } = e.target as HTMLInputElement;
        const checked = (target as HTMLInputElement).checked; // Safely access 'checked' for inputs of type checkbox

        setFormData({
            ...formData,
            [name]: target.type === 'checkbox' ? checked : value,
        });

        // Run both validations
        const liveError = validateFieldOnChange(name, value); // Live validation
        const finalError = validateField(name, value.trim()); // Full validation

        setFormErrors(prevErrors => ({
            ...prevErrors,
            [name]: liveError || finalError, // Prioritize live error, else final error
        }));


        // Validate the field
        /** Temp commented */
        // const error = validateField(name, type === "checkbox" ? String(checked) : value);

        // // Update form errors
        // setFormErrors({
        //     ...formErrors,
        //     [name]: error,
        // });
        /** Temp commented */

        if (name === "userNameAlias") {
            clearTimeout(debounceTimer);
            debounceTimer = setTimeout(() => {
                if (formData.userNameAlias != "" && (e.target.value != formData.userNameAliasHidden)) {
                    Checkavailablity(e.target.value);
                }
            }, 1000);
        }
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        // Validate all fields
        const errors = validateAllFields();

        if (Object.keys(errors).length > 0) {
            setFormErrors(errors); // Set errors to display
            return; // Stop form submission
        }
        if (formErrors.userNameAlias) {
            return;
        }

        let trueOrFalse: boolean;
        if (formData.isSendEmail) {
            trueOrFalse = true;
        } else {
            trueOrFalse = false;
        }

        // console.log("formData", formData)

        const userPayload: IUserAddUserPayload = {
            id: formData.id,
            clientId: clientId,
            firstName: formData.firstName,
            lastName: formData.lastName,
            emailID: formData.emailID,
            userNameAlias: formData.userNameAlias,
            userNameAliasHidden: formData.userNameAliasHidden,
            defaultLanguageId: formData.defaultLanguageId,
            userPassword: formData.userPassword,
            confirmPassword: formData.confirmPassword,
            isActive: formData?.isActive === "true" ? true : formData?.isActive === "false" ? false : undefined,
            managerEmailId: formData.managerEmailId,
            isSendEmail: trueOrFalse,
            isAutoEmail: trueOrFalse,
            isDirectSendMail: trueOrFalse,
            FlagAddUserPage: true,
            //emailOption: formData.emailOption,
            //sendDirectEmail: formData.sendDirectEmail,
        };

        // console.log('Submitting user:', userPayload);
        //return;
        if (isAddForm) {
            dispatch(addUsers(userPayload))
                .then((response: any) => {
                    // If signup is successful, navigate to the login
                    if (response.meta.requestStatus == "fulfilled") {
                        //setAlert({ type: "success", message: "User added successfully." });
                        setAlert({ type: "success", message: response?.payload.msg });
                        setTimeout(() => {
                            navigate(AdminPageRoutes.ADMIN_MANAGE_USER.FULL_PATH);
                        }, 2000);
                    } else {
                        //setAlert({ type: "error", message: 'Add user failed: ' + response.msg });
                        setAlert({ type: "error", message: response?.payload.msg });
                    }
                })
                .catch((error) => {
                    console.error("Add user error:", error);
                    setAlert({ type: "error", message: 'An unexpected error occurred.' });
                });
        }
        else {
            dispatch(updateUser(userPayload))
                .then((response: any) => {
                    // If signup is successful, navigate to the login
                    if (response.meta.requestStatus == "fulfilled") {
                        //setAlert({ type: "success", message: "User updated successfully." });
                        setAlert({ type: "success", message: response?.payload.msg });
                        setTimeout(() => {
                            navigate(AdminPageRoutes.ADMIN_MANAGE_USER.FULL_PATH);
                        }, 2000);
                    } else {
                        //setAlert({ type: "error", message: 'Update user failed: ' + response.msg });
                        setAlert({ type: "error", message: response?.payload.msg });
                    }
                })
                .catch((error) => {
                    console.error("Update user error:", error);
                    setAlert({ type: "error", message: 'An unexpected error occurred.' });
                });
        }
    };

    const Checkavailablity = (typedValue: string) => {
        if (!typedValue) {
            setFormErrors((prev) => ({ ...prev, userNameAlias: 'Login ID is required' }));
            return;
        }
        if (typedValue.trim() && !isValidInput(typedValue)) {
            setFormErrors((prev) => ({ ...prev, userNameAlias: "Special characters like < > ' & are not allowed." }));
            return;
        }

        setFormErrors({});
        setFormSuccess({});
        setFormErrors((prev) => ({ ...prev, userNameAlias: '' })); // Clear any existing errors for this field
        dispatch(checkavailablity({ UserNameAlias: typedValue, ClientId: clientId }))
            .then((response: any) => {
                // console.log(response);
                if (response.meta.requestStatus === 'fulfilled') {
                    // setFormErrors((prev) => ({ ...prev, userNameAlias: 'Username already exists. Please enter another Username.' }));
                    setFormSuccess((prev) => ({ ...prev, userNameAlias: response.payload?.msg }));
                    setFormErrors((prev) => ({ ...prev, userNameAlias: '' }));
                } else {
                    //response.payload?.msg
                    // setFormSuccess((prev) => ({ ...prev, userNameAlias: 'Username is available.' }));
                    setFormErrors((prev) => ({ ...prev, userNameAlias: response.payload?.msg }));
                    setFormSuccess((prev) => ({ ...prev, userNameAlias: '' }));
                }
            })
            .catch((error) => {
                console.error('Error checking availability:', error);
                setFormErrors((prev) => ({ ...prev, userNameAlias: 'An unexpected error occurred while checking availability.' }));
            });
    }

    const goBack = () => {
        navigate(-1); // -1 navigates to the previous page
    };


    return (
        <>
            {/* Loading Overlay */}
            {loading && (
                <div className='yp-loading-overlay'>
                    <div className='yp-spinner'>
                        <div className='spinner-border' role='status'>
                            <span className='visually-hidden'>Loading...</span>
                        </div>
                    </div>
                </div>
            )}

            <div className="yp-page-title-button-section" id="yp-page-title-breadcrumb-section">
                <div className="yp-page-title-breadcrumb">
                    <div className="yp-page-title">{pageTitle}</div>
                    <CustomBreadcrumb items={breadcrumbItems} />
                </div>
            </div>
            <div className='yp-card' id="yp-card-main-content-section">
                <form>
                    <div className="row row-cols-auto g-3">

                        <div className={`col ${isFieldIncluded(fields, FIELDS_ENUM.FIRST_NAME) ? 'col-12 col-md-12 col-lg-4' : 'd-none'}`}>
                            <div className="form-group">
                                <div className="yp-form-control-wrapper">
                                    <input type="text"
                                        className={`form-control yp-form-control ${formErrors.firstName ? 'is-invalid' : ''}`}
                                        name="firstName"
                                        value={formData.firstName ?? ""}
                                        // onChange={handleInputChange}
                                        onChange={(e) => {
                                            const value = e.target.value;
                                            // Prevent leading space
                                            if (formData.firstName === '' && value.startsWith(' ')) return;
                                            handleInputChange(e);
                                        }}
                                        onKeyDown={(e) => {
                                            // Prevent space as first character
                                            if (e.key === ' ' && formData.firstName.length === 0) { e.preventDefault(); }
                                        }}
                                        id="" placeholder="" />
                                    <label className="form-label">
                                        {!isFieldAllowBlanks(fields, FIELDS_ENUM.FIRST_NAME) && <span className="yp-asteric">*</span>}
                                        First Name
                                    </label>
                                    <input type="hidden" name="id" value={formData.id} />
                                </div>
                                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                    {formErrors.firstName && <div className="invalid-feedback px-3">{formErrors.firstName}</div>}
                                </div>
                            </div>
                        </div>
                        <div className={`col ${isFieldIncluded(fields, FIELDS_ENUM.LAST_NAME) ? 'col-12 col-md-12 col-lg-4' : 'd-none'}`}>
                            <div className="form-group">
                                <div className="yp-form-control-wrapper">
                                    <input type="text"
                                        className={`form-control yp-form-control ${formErrors.lastName ? 'is-invalid' : ''}`}
                                        name="lastName"
                                        value={formData.lastName ?? ""}
                                        // onChange={handleInputChange}
                                        onChange={(e) => {
                                            const value = e.target.value;
                                            // Prevent leading space
                                            if (formData.lastName === '' && value.startsWith(' ')) return;
                                            handleInputChange(e);
                                        }}
                                        onKeyDown={(e) => {
                                            // Prevent space as first character
                                            if (e.key === ' ' && formData.lastName.length === 0) { e.preventDefault(); }
                                        }}
                                        id="" placeholder="" />
                                    <label className="form-label">
                                        {!isFieldAllowBlanks(fields, FIELDS_ENUM.LAST_NAME) && <span className="yp-asteric">*</span>}
                                        Last Name
                                    </label>
                                </div>
                                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                    {formErrors.lastName && <div className="invalid-feedback px-3">{formErrors.lastName}</div>}
                                </div>
                            </div>
                        </div>
                        <div className={`col ${isFieldIncluded(fields, FIELDS_ENUM.EMAIL_ID) ? 'col-12 col-md-12 col-lg-4' : 'd-none'}`}>
                            <div className="form-group">
                                <div className="yp-form-control-wrapper">
                                    <input type="email"
                                        className={`form-control yp-form-control ${formErrors.emailID ? 'is-invalid' : ''}`}
                                        name="emailID"
                                        value={formData.emailID ?? ""}
                                        // onChange={handleInputChange}
                                        onChange={(e) => {
                                            const value = e.target.value;
                                            // Prevent leading space
                                            if (formData.emailID === '' && value.startsWith(' ')) return;
                                            handleInputChange(e);
                                        }}
                                        onKeyDown={(e) => {
                                            // Prevent space as first character
                                            if (e.key === ' ' && formData.emailID.length === 0) { e.preventDefault(); }
                                        }}
                                        id="" placeholder="" />
                                    <label className="form-label">
                                        {!isFieldAllowBlanks(fields, FIELDS_ENUM.EMAIL_ID) && <span className="yp-asteric">*</span>}
                                        Email Address
                                    </label>
                                </div>
                                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                    {formErrors.emailID && <div className="invalid-feedback px-3">{formErrors.emailID}</div>}
                                </div>
                            </div>
                        </div>
                        <div className={`col ${isFieldIncluded(fields, FIELDS_ENUM.LOGIN_ID) ? 'col-12 col-md-12 col-lg-4' : 'd-none'}`}>
                            <div className="form-group yp-form-group-with-note">
                                <div className="yp-form-control-wrapper">
                                    <input type="text"
                                        className={`form-control yp-form-control ${formErrors.userNameAlias ? 'is-invalid' : ''}`}
                                        name="userNameAlias"
                                        value={formData.userNameAlias ?? ""}
                                        // onChange={(e) => { handleInputChange(e); setLoginID(e.target.value); }}
                                        onChange={(e) => {
                                            const value = e.target.value;
                                            // Prevent leading space on paste or change
                                            if (formData.userNameAlias === '' && value.startsWith(' ')) return;
                                            handleInputChange(e);
                                            setLoginID(value);
                                        }}
                                        onKeyDown={(e) => {
                                            // Prevent space if it's the first character
                                            if (e.key === ' ' && formData.userNameAlias.length === 0) {
                                                e.preventDefault();
                                            }
                                        }}
                                        //disabled={!isAddForm}
                                        id="" placeholder="" />
                                    <label className="form-label">
                                        {!isFieldAllowBlanks(fields, FIELDS_ENUM.LOGIN_ID) && <span className="yp-asteric">*</span>}
                                        Login ID</label>

                                    <input type="hidden" name="userNameAliasHidden" value={formData.userNameAliasHidden} />
                                </div>
                                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                    {formErrors.userNameAlias && <div className="invalid-feedback px-3">{formErrors.userNameAlias}</div>}
                                    {formSuccess.userNameAlias && <div className="valid-feedback  px-3">{formSuccess.userNameAlias}</div>}
                                    {/* <div className='flex-shrink-0'>
                                        <a href={(!UserNameAlias || loading) ? undefined : '#'}
                                        onClick={Checkavailablity}
                                        className={`yp-link yp-link-primary yp-fs-11 px-3 ${(!UserNameAlias || loading) ? 'disabled' : ''}`}>Check Availability</a>
                                    </div> */}
                                </div>
                            </div>
                        </div>
                        <div className={`col ${isFieldIncluded(fields, FIELDS_ENUM.USER_PASSWORD) ? 'col-12 col-md-12 col-lg-4' : 'd-none'}`}>
                            <div className="form-group yp-form-group-with-note">
                                <div className="yp-form-control-wrapper">
                                    <input type="password"
                                        className={`form-control yp-form-control ${formErrors.userPassword ? 'is-invalid' : ''}`}
                                        name="userPassword"
                                        value={formData.userPassword ?? ""}
                                        // onChange={handleInputChange}
                                        onChange={(e) => {
                                            const value = e.target.value;
                                            // Prevent leading space
                                            if (formData.userPassword === '' && value.startsWith(' ')) return;
                                            handleInputChange(e);
                                        }}
                                        onKeyDown={(e) => {
                                            // Prevent space as first character
                                            if (e.key === ' ' && formData.userPassword.length === 0) { e.preventDefault(); }
                                        }}
                                        id="" placeholder="" />
                                    <label className="form-label">
                                        {!isFieldAllowBlanks(fields, FIELDS_ENUM.USER_PASSWORD) && <span className="yp-asteric">*</span>}
                                        Password</label>
                                </div>
                                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                    {formErrors.userPassword && <div className="invalid-feedback px-3">{formErrors.userPassword}</div>}
                                </div>
                            </div>
                        </div>

                        <div className={`col ${isFieldIncluded(fields, FIELDS_ENUM.USER_PASSWORD) ? 'col-12 col-md-12 col-lg-4' : 'd-none'}`}>
                            <div className="form-group">
                                <div className="yp-form-control-wrapper">
                                    <input type="password"
                                        className={`form-control yp-form-control ${formErrors.confirmPassword ? 'is-invalid' : ''}`}
                                        name="confirmPassword"
                                        value={formData.confirmPassword ?? ""}
                                        // onChange={handleInputChange}
                                        onChange={(e) => {
                                            const value = e.target.value;
                                            // Prevent leading space
                                            if (formData.confirmPassword === '' && value.startsWith(' ')) return;
                                            handleInputChange(e);
                                        }}
                                        onKeyDown={(e) => {
                                            // Prevent space as first character
                                            if (e.key === ' ' && formData.confirmPassword.length === 0) { e.preventDefault(); }
                                        }}
                                        id="" placeholder="" />
                                    <label className="form-label">
                                        {!isFieldAllowBlanks(fields, FIELDS_ENUM.USER_PASSWORD) && <span className="yp-asteric">*</span>}
                                        Confirm Password</label>
                                </div>
                                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                    {formErrors.confirmPassword && <div className="invalid-feedback px-3">{formErrors.confirmPassword}</div>}
                                </div>
                            </div>
                        </div>
                        <div className={`col ${isFieldIncluded(fields, FIELDS_ENUM.IS_ACTIVE) ? 'col-12 col-md-12 col-lg-4' : 'd-none'}`}>
                            <div className="form-group">
                                <div className="yp-form-control-wrapper">
                                    <select
                                        name="isActive"
                                        className={`form-control yp-form-control ${formErrors.isActive ? 'is-invalid' : ''}`}
                                        value={formData.isActive ?? ""}
                                        onChange={handleInputChange}
                                        id="">
                                        <option value="" selected>Select</option>
                                        <option value="true">Active</option>
                                        <option value="false">Inactive</option>
                                    </select>
                                    <label className="form-label">
                                        {!isFieldAllowBlanks(fields, FIELDS_ENUM.IS_ACTIVE) && <span className="yp-asteric">*</span>}
                                        Is Active?</label>
                                </div>
                                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                    {formErrors.isActive && <div className="invalid-feedback px-3">{formErrors.isActive}</div>}
                                </div>
                            </div>
                        </div>
                        <div className={`col ${isFieldIncluded(fields, FIELDS_ENUM.DEFAULT_LANGUAGE_ID) ? 'col-12 col-md-12 col-lg-4' : 'd-none'}`}>
                            <div className="form-group">
                                <div className="yp-form-control-wrapper">
                                    <select
                                        //    className="form-control yp-form-control"
                                        name="defaultLanguageId"
                                        value={formData.defaultLanguageId ?? ""}
                                        className={`form-control yp-form-control ${formErrors.defaultLanguageId ? 'is-invalid' : ''}`}
                                        // onChange={handleInputChange}
                                        onChange={(e) => {
                                            const value = e.target.value;
                                            // Prevent leading space
                                            if (formData.defaultLanguageId === '' && value.startsWith(' ')) return;
                                            handleInputChange(e);
                                        }}
                                        onKeyDown={(e) => {
                                            // Prevent space as first character
                                            if (e.key === ' ' && formData.defaultLanguageId.length === 0) { e.preventDefault(); }
                                        }}
                                        id="" disabled>
                                        <option>Select</option>
                                        <option value={'en-US'} selected>English (US)</option>
                                    </select>
                                    <label className="form-label">
                                        {!isFieldAllowBlanks(fields, FIELDS_ENUM.DEFAULT_LANGUAGE_ID) && <span className="yp-asteric">*</span>}
                                        Preferred Language</label>
                                </div>
                                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                    {formErrors.defaultLanguageId && <div className="invalid-feedback px-3">{formErrors.defaultLanguageId}</div>}
                                </div>
                            </div>
                        </div>
                        <div className={`col ${isFieldIncluded(fields, FIELDS_ENUM.MANAGER_EMAIL_ID) ? 'col-12 col-md-12 col-lg-4' : 'd-none'}`}>
                            <div className="form-group">
                                <div className="yp-form-control-wrapper">
                                    <input type="email"
                                        className={`form-control yp-form-control ${formErrors.managerEmailId ? 'is-invalid' : ''}`}
                                        name="managerEmailId"
                                        value={formData.managerEmailId ?? ""}
                                        // onChange={handleInputChange}
                                        onChange={(e) => {
                                            const value = e.target.value;
                                            // Prevent leading space
                                            if (formData.managerEmailId === '' && value.startsWith(' ')) return;
                                            handleInputChange(e);
                                        }}
                                        onKeyDown={(e) => {
                                            // Prevent space as first character
                                            if (e.key === ' ' && formData.managerEmailId.length === 0) { e.preventDefault(); }
                                        }}
                                        id="" placeholder="" />
                                    <label className="form-label">
                                        {!isFieldAllowBlanks(fields, FIELDS_ENUM.MANAGER_EMAIL_ID) && <span className="yp-asteric">*</span>}
                                        Manager Email Address</label>
                                </div>
                                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                    {formErrors.managerEmailId && <div className="invalid-feedback px-3">{formErrors.managerEmailId}</div>}
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-12">
                            <div className="yp-toggle-card">
                                <div className="yp-toggle-card-head">
                                    <div className="yp-form-control-wrapper">
                                        <div className="form-check form-switch">
                                            <input className="form-check-input" type="checkbox" role="switch"
                                                name="isSendEmail"
                                                id="isSendEmailToggleRadio"
                                                value={formData.isSendEmail.toString()}
                                                checked={formData.isSendEmail} // Bind `checked` for checkboxes
                                                onChange={handleInputChange}
                                                data-bs-toggle="collapse" data-bs-target="#collapseIsSendEmailSection" aria-expanded="false" aria-controls="collapseIsSendEmailSection" />
                                            <label className="form-check-label yp-text-primary yp-fs-16" htmlFor="isSendEmailToggleRadio"><span>Send Email</span></label>
                                        </div>
                                    </div>
                                </div>
                                {/* <div className="yp-toggle-card-body collapse" id="collapseIsSendEmailSection">
                                    <div className="row">
                                        <div className="col-12">
                                            <div className="form-group">
                                                <div className="yp-form-control-wrapper">
                                                    <div className="form-check form-check-inline">
                                                        <input className="form-check-input" name="emailAutoScheduleOption" type="radio" id="autoEmail" value="autoEmail" />
                                                        <label className="form-check-label" htmlFor="autoEmail">Auto Email</label>
                                                    </div>
                                                    <div className="form-check form-check-inline">
                                                        <input className="form-check-input" name="emailAutoScheduleOption" type="radio" id="scheduleEmail" value="scheduleEmail" />
                                                        <label className="form-check-label" htmlFor="scheduleEmail">Schedule Email</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="row">
                                        <div className="col-12">
                                            <div className="form-group">
                                                <div className="yp-form-control-wrapper">
                                                    <div className="form-check form-switch">
                                                        <input className="form-check-input" type="checkbox" role="switch" id="sendDirectToEmailToUser" />
                                                        <label className="form-check-label" htmlFor="sendDirectToEmailToUser"><span>Send direct email to user</span></label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div> */}
                            </div>
                        </div>
                    </div>

                    <div className="row">
                        <div className="col-12">
                            <div className="yp-form-button-row">
                                <button type="button" className="btn btn-primary" onClick={handleSubmit}>Submit</button>
                                <button type="reset" className="btn btn-secondary" onClick={goBack}>Cancel</button>
                                {isAddForm &&
                                    <a href='#' className="yp-link yp-link-primary yp-link-14-300" onClick={(e) => { e.preventDefault(); handleReset(); }}>Clear</a>
                                }
                            </div>
                        </div>
                    </div>
                </form>

                {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}

            </div>
        </>
    )
}

export default AddUser;
