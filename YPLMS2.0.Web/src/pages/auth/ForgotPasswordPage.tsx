import React, { useState, useEffect } from 'react';
import { useAppDispatch, useAppSelector } from '../../hooks';
import logo from '../../assets/images/enc_logo 1.png';
import { Link, useNavigate } from 'react-router-dom';
import { PageRoutes } from '../../utils/Constants/Auth_PageRoutes';
import { RootState } from '../../redux/store';
import { forgotPassword, clearError, fetchClientIdByUrlSlice } from '../../redux/Slice/authSlice';
import AlertMessage from '../../components/shared/AlertMessage';
import { useSelector } from 'react-redux';


const ForgotPasswordPage: React.FC = () => {
    const dispatch = useAppDispatch();
    const { loading, error } = useAppSelector((state: RootState) => state.auth);
    const navigate = useNavigate();
    // const [ClientId, setClientID] = useState('CLIYPRevamp_New');
    // const [ClientId, setClientID] = useState('CLIYPRevamp_QA');
    const { clientId } = useSelector((state: RootState) => state.auth);
    const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);
    const [UserNameAlias, setLoginId] = useState('');

    const [formData, setFormData] = useState({
        UserNameAlias: ''
    });

    const handleReset = () => {
        setFormData({
            UserNameAlias: ''
        });
    };

    const [formErrors, setFormErrors] = useState<Record<string, string>>({});


    useEffect(() => {
        document.title = 'Forgot Password';
        document.body.removeAttribute("id");

        try {
            const apiBaseUrl = process.env.REACT_APP_API_BASE_URL;
            const url = new URL(apiBaseUrl || "");
            const clientAccessURL = url.hostname;

            dispatch(fetchClientIdByUrlSlice({ clientAccessURL }))
                .then((response: any) => {
                    if (response.meta.requestStatus === 'fulfilled') {
                        // setClientID(response.payload.clientId); // or ClientId based on API
                    } else {
                        setAlert({ type: "error", message: `Failed to fetch client ID: ${response.payload}` });
                    }
                })
                .catch(() => {
                    setAlert({ type: "error", message: "Error fetching client ID by URL" });
                });

        } catch (err) {
            console.error("Invalid REACT_APP_API_BASE_URL", err);
            setAlert({ type: "error", message: "Invalid API base URL in environment config." });
        }
    }, []);

    // Common validation function
    const validateField = (name: string, value: string): string => {
        if (typeof value === 'string') {
            if (value.trim() && !isValidInput(value)) {
                return "Special characters like < > ' & are not allowed.";
            }
        }

        if (name === "UserNameAlias" && !value.trim()) {
            return "Login ID or Email is required";
        }
        // switch (name) {
        //     case "UserNameAlias":
        //         if (!value.trim()) return "Login ID is required";
        //         break;
        //     default:
        //         break;
        // }
        return ""; // No error
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


    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const target = e.target as HTMLInputElement | HTMLSelectElement;
        const { name, value, type } = e.target as HTMLInputElement;

        setFormData({
            ...formData,
            [name]: value,
        });

        // Validate the field
        const error = validateField(name, value);

        // Update form errors
        setFormErrors({
            ...formErrors,
            [name]: error,
        });
    };
    // ✅ Check if the value is email
    const isValidEmail = (value: string): boolean => {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(value);
    };


    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        // Validate all fields
        const errors = validateAllFields();
        if (Object.keys(errors).length > 0) {
            setFormErrors(errors);
            return;
        }

        const inputValue = formData.UserNameAlias.trim();
        const payload: any = { ClientId: clientId };

        // Dynamically assign either EmailID or UserNameAlias
        if (isValidEmail(inputValue)) {
            payload.EmailID = inputValue;
        } else {
            payload.UserNameAlias = inputValue;
        }
        // console.log("Final Payload Before Dispatch:", payload);

        dispatch(forgotPassword(payload))
            .then((response: any) => {
                if (response.meta.requestStatus === "fulfilled") {
                    setAlert({ type: "success", message: "Password has been sent to your email." });
                    setTimeout(() => {
                        navigate(PageRoutes.LOGIN.FULL_PATH);
                    }, 2000);
                } else {
                    setAlert({
                        type: "error",
                        message: response?.payload?.msg || "Login ID or Email does not exist. Please try again."
                    });
                }
            })
            .catch((error) => {
                console.error("Forgot password error:", error);
                setAlert({ type: "error", message: "An unexpected error occurred. Please try again." });
            });
    };


    // Clear error on component mount or when `loginId` changes
    useEffect(() => {
        dispatch(clearError());
    }, [dispatch, UserNameAlias]);

    return (
        <div className='container-fluid yp-loginSignupPage'>
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

            <div className='yp-loginSignupPage-header'>
                <a href={`${PageRoutes.LOGIN.FULL_PATH}`}>
                    <img src={logo} alt='' title='' className='yp-loginSignupPage-header-logo' />
                </a>
            </div>

            <form noValidate className='yp-form'>
                <div className='yp-card yp-border-radius-50'>
                    <div className="row">
                        <div className="col-md-6">
                            <div className='yp-signupFormWrapper'>
                                <div className='yp-card-heading-row'>
                                    <h4 className='yp-text-40-500 yp-text-primary'>Forgot Password</h4>
                                </div>
                            </div>
                        </div>
                        <div className="col-md-6">
                            <div className='yp-signupFormWrapper'>
                                <div className='yp-card-heading-row d-flex justify-content-end align-items-center gap-2'>
                                    <h4 className='yp-text-28-500'>
                                        <Link to={PageRoutes.LOGIN.FULL_PATH} className="yp-no-underline yp-link">Back</Link>
                                    </h4>
                                    <i className="fa fa-arrow-right yp-text-primary mb-1 yp-fs-25" aria-hidden="true"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className='row'>
                        <div className='col-12 col-sm-10 col-md-8 col-lg-6'>
                            <div className='yp-signupFormWrapper form-floating yp-form-row'>
                                <p className='yp-text-20-300'> Please enter your Login ID or Email Address to reset Password</p>
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-12 col-md-6 col-lg-6">
                            <div className='yp-signupFormWrapper'>
                                <div className="form-group yp-form-group-with-note">
                                    <div className="yp-form-control-wrapper">
                                        <input type="text"
                                            className={`form-control yp-form-control ${formErrors.UserNameAlias ? 'is-invalid' : ''}`}
                                            name="UserNameAlias"
                                            value={formData.UserNameAlias}
                                            // onChange={(e) => { handleInputChange(e); setLoginId(e.target.value);  }}
                                            onChange={(e) => {
                                                const value = e.target.value;
                                                // Prevent leading space on paste or change
                                                if (formData.UserNameAlias === '' && value.startsWith(' ')) return;
                                                handleInputChange(e);
                                                setLoginId(value);
                                            }}
                                            onKeyDown={(e) => {
                                                // Prevent space if it's the first character
                                                if (e.key === ' ' && formData.UserNameAlias.length === 0) {
                                                    e.preventDefault();
                                                }
                                            }}
                                            id="" placeholder=""
                                            required />
                                        <label className="form-label"><span className='yp-asteric'>*</span>Login ID & Email Address</label>
                                    </div>
                                    <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                        <div className="invalid-feedback px-3">
                                            {formErrors.UserNameAlias}
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-lg-12">
                            <div className='yp-signupFormWrapper'>
                                <div className='yp-form-button-row'>
                                    {/* <button onClick={handleSubmit} type='submit' className='btn btn-primary yp-btn-lg'>
                                        Email My Password
                                    </button> */}
                                    <button type='submit' onClick={handleSubmit} className='btn btn-primary yp-btn-forgot-signup' disabled={loading}>
                                        Email My Password
                                    </button>

                                    {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form >

            <div className='row'>
                <div className='col-12'>
                    <ul className='yp-footer-links'>
                        <li><a href='#'>About Us</a></li>
                        <li><a href='#'>Policy Privacy</a></li>
                        <li><a href='#'>Terms of Use</a></li>
                        <li><a href='#'>Purchase Policy</a></li>
                    </ul>
                </div>
            </div>

            <div className='yp-bgGraphic-bottom-right'>
                <span className='yp-circle-br yp-circle-br-1'></span>
                <span className='yp-circle-br yp-circle-br-2'></span>
            </div>

            <div className='yp-bgGraphic-top-left'>
                <span className='yp-circle-tl yp-circle-tl-1'></span>
                <span className='yp-circle-tl yp-circle-tl-2'></span>
                <span className='yp-circle-tl yp-circle-tl-3'></span>
            </div>
        </div >
    );
};


export default ForgotPasswordPage