import React, { useEffect, useState } from 'react';
import { useAppDispatch, useAppSelector } from '../../hooks';
import { fetchClientIdByUrlSlice, login } from '../../redux/Slice/authSlice';
import { RootState } from '../../redux/store';
import { Link, useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { Form, Button, Spinner, Alert } from 'react-bootstrap';

import logo from '../../assets/images/enc_logo 1.png';
import loginBg from '../../assets/images/login_graphics.png';
import { SecureEncrypt } from '../../components/shared/DecodeUtils';
import { PageRoutes } from '../../utils/Constants/Auth_PageRoutes';
import { AdminPageRoutes } from '../../utils/Constants/Admin_PageRoutes';
import { LearnerPageRoutes } from '../../utils/Constants/Learner_PageRoutes';
import AlertMessage from '../../components/shared/AlertMessage';
import ConfirmationModal from '../../components/shared/ConfirmationModal';

const LoginPage: React.FC = () => {
  const dispatch = useAppDispatch();
  const { loading, error } = useAppSelector((state: RootState) => state.auth);
  const navigate = useNavigate();
  // const [ClientId, setClientID] = useState('CLIYPRevamp_New');
  // const [ClientId, setClientID] = useState('CLIYPRevamp_QA');
  const { clientId } = useSelector((state: RootState) => state.auth);
  // console.log("clientId", clientId)

  const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);
  const [userNameAlias, setLoginID] = useState('');

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
  const [localClientId, setLocalClientId] = useState<string | null>(null);


  const [formData, setFormData] = useState({
    userNameAlias: '',
    userPassword: ''
  });

  const handleReset = () => {
    setFormData({
      userNameAlias: '',
      userPassword: ''
    });
  };

  const [formErrors, setFormErrors] = useState<Record<string, string>>({});

  useEffect(() => {
    document.title = 'Login';
    document.body.removeAttribute("id");
    if (clientId) { return; }

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

    switch (name) {
      case "userNameAlias":
        if (!value.trim()) return "Login ID is required";
        break;
      case "userPassword":
        if (!value.trim()) return "Password is required";
        break;
      default:
        break;
    }
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

  /* login page handle function */
  //   const handleSubmit = (e: React.FormEvent, forceLoginValue = false) => {
  //     e.preventDefault();

  //     // Validate all fields
  //     const errors = validateAllFields();
  //     if (Object.keys(errors).length > 0) {
  //       setFormErrors(errors);
  //       return;
  //     }

  //     // Encrypt user credentials before sending
  //     //  const encryptedLoginID = SecureEncrypt(formData.userNameAlias);
  //     const encryptedPassword = SecureEncrypt(formData.userPassword);
  //     let userNameAlias = formData.userNameAlias;

  //     dispatch(login({
  //       payload: { userNameAlias, userPassword: encryptedPassword, ClientId: clientId },
  //       forceLogin: forceLoginValue
  //     })
  //     ).unwrap()
  //       .then((response: any) => {


  //      //  Handle 409 from success response
  //     if (response?.statusCode === 409) {
  //         setModalConfig({
  //           title: "Force Login",
  //           message: response?.msg || "You are already logged in on another device. Do you want to force login?",
  //           primaryBtnText: "Yes, Force Login"
  //         });

  //         setModalAlertMessageConfig({
  //           type: "error",
  //           message: "",
  //           duration: "",
  //           autoClose: false
  //         });

  //         setOnConfirmAction(() => () => {
  //           setShowModal(false);
  //           // 🔄 Retry login with forceLogin = true
  //           handleSubmit(e, true);
  //         });

  //         setShowModal(true);
  //         return;
  //       }
  //         //  Normal success handling
  //         if (response.meta.requestStatus === "fulfilled") {

  //           const roleIds = response.payload.learner.userAdminRole.map((role: any) => role.roleId);
  //           setAlert({ type: "success", message: response.payload?.msg });

  //           // Determine redirection based on user roles
  //           setTimeout(() => {
  //             if (roleIds.includes("ROL0005")) {
  //               navigate(AdminPageRoutes.ADMIN_DASHBOARD.FULL_PATH);
  //             } else if (roleIds.includes("ROL0001")) {
  //               navigate(LearnerPageRoutes.LEARNER_DASHBOARD.FULL_PATH);
  //             } else {
  //               console.log("Unknown role, redirecting to Login");
  //               navigate(PageRoutes.LOGIN.FULL_PATH);
  //             }
  //           }, 2000);
  //         } else {
  //           // setAlert({ type: "error", message: "Login failed: " + response.msg });
  //           setAlert({ type: "error", message: `Login failed: ${response.payload}` });
  //         }
  //       })
  //       .catch((error: any) => {
  //       setAlert({
  //         type: 'error',
  //         message: error?.message || 'An unexpected error occurred.'
  //       });
  //     });
  // };

  useEffect(() => {
    if (clientId) {
      setLocalClientId(clientId);
    }
  }, [clientId]);

  const handleSubmit = (e?: React.FormEvent, forceLoginValue = false) => {
    if (e) e.preventDefault();

    //  Validate form fields
    const errors = validateAllFields();
    if (Object.keys(errors).length > 0) {
      setFormErrors(errors);
      return;
    }

    const encryptedPassword = SecureEncrypt(formData.userPassword);
    let userNameAlias = formData.userNameAlias;

    dispatch(
      login({
        payload: {
          userNameAlias,
          userPassword: encryptedPassword,
          ClientId: localClientId || clientId,
          isActive: true
        },
        forceLogin: forceLoginValue
      })
    )
      .unwrap()
      .then((response: any) => {
        //  Handle 409 - Already logged in elsewhere
        if (response?.statusCode === 409) {
          setModalConfig({
            title: "Force Login",
            message:
              response?.msg ||
              "You are already logged in on another device. Do you want to force login?",
            primaryBtnText: "Yes, Force Login"
          });

          setModalAlertMessageConfig({
            type: "error",
            message: "",
            duration: "",
            autoClose: false
          });

          setOnConfirmAction(() => () => {
            setShowModal(false);
            // Retry login with forceLogin = true (no event passed)
            handleSubmit(undefined, true);
          });

          setShowModal(true);
          return;
        }

        // Handle normal successful login
        if (response?.status === "SUCCESS") {
          const roleIds =
            response?.learner?.userAdminRole?.map((role: any) => role.roleId) || [];

          setAlert({ type: "success", message: response?.msg || "Login Successfully" });

          setTimeout(() => {
            if (roleIds.includes("ROL0005")) {
              navigate(AdminPageRoutes.ADMIN_DASHBOARD.FULL_PATH);
            } else if (roleIds.includes("ROL0001")) {
              navigate(LearnerPageRoutes.LEARNER_DASHBOARD.FULL_PATH);
            } else {
              console.warn("Unknown role, redirecting to Login");
              navigate(PageRoutes.LOGIN.FULL_PATH);
            }
          }, 2000);
        } else {
          setAlert({
            type: "error",
            message: `Login failed: ${response?.msg || "Unknown error"}`
          });
        }
      })
      .catch((error: any) => {
        setAlert({
          type: "error",
          message: error?.message || "An unexpected error occurred."
        });
      });
  };


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
        <img src={logo} alt='' title='' className='yp-loginSignupPage-header-logo' />
      </div>

      <ConfirmationModal
        show={showModal}
        onConfirm={onConfirmAction}
        onCancel={() => {
          setShowModal(false);
          setModalAlertMessageConfig({
            type: 'success',
            message: '',
            duration: '',
            autoClose: true
          });
        }}
        modalConfig={modalConfig}
        modalAlertMessageConfig={modalAlertMessageConfig}
      />
      <div className='yp-card yp-border-radius-50'>
        <div className='row'>
          <div className='col-lg-6'>
            <div className='yp-loginFormWrapper m-auto'>
              <div className='yp-card-heading-row text-center'>
                <h4 className='yp-text-40-500 yp-text-primary'>Login</h4>
                <hr className='yp-hr-primary' />
              </div>
              <form noValidate className='yp-form'>
                <div className="row">
                  <div className="col-12 col-md-12 col-lg-12">
                    <div className="form-group yp-form-group-with-note">
                      <div className="yp-form-control-wrapper">
                        <input type="text"
                          className={`form-control yp-form-control ${formErrors.userNameAlias ? 'is-invalid' : ''}`}
                          name="userNameAlias"
                          value={formData.userNameAlias}
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
                          id="" placeholder="" />
                        <label className="form-label"><span className='yp-asteric'>*</span>Login ID</label>
                      </div>
                      <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                        <div className="invalid-feedback px-3">
                          {formErrors.userNameAlias}
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div className="row">
                  <div className="col-12 col-md-12 col-lg-12">
                    <div className="form-group">
                      <div className="yp-form-control-wrapper">
                        <input type="password"
                          className={`form-control yp-form-control ${formErrors.userPassword ? 'is-invalid' : ''}`}
                          name="userPassword"
                          value={formData.userPassword}
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
                        <label className="form-label"><span className='yp-asteric'>*</span>Password</label>
                      </div>
                      <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                        {formErrors.userPassword && <div className="invalid-feedback px-3">{formErrors.userPassword}</div>}
                      </div>
                    </div>
                  </div>
                </div>
                <div className="row">
                  <div className="col-12 col-md-12 col-lg-12">
                    <div className='yp-form-button-row'>
                      <button onClick={handleSubmit} type='submit' className='btn btn-primary yp-btn-lg yp-width-full' disabled={loading}>
                        Login
                      </button>
                    </div>
                  </div>
                </div>
              </form>

              <div className='text-center my-4'>
                <Link to={PageRoutes.FORGOT_PASSWORD.FULL_PATH} className='yp-link yp-link-16 yp-link-primary yp-no-underline'>
                  Forgot Password?
                </Link>
              </div>

              {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}

              <div className='text-center mt-3'>
                <Link to={PageRoutes.SIGNUP.FULL_PATH} className='yp-link yp-link-20 yp-link-primary yp-no-underline'>
                  Sign up
                </Link>
              </div>
            </div>
          </div>
        </div>

        <div className='yp-loginBg d-none d-lg-block'>
          <span className='yp-loginBgImg' style={{ "backgroundImage": `url(${loginBg})` }}></span>
          <span className='yp-circle-inside yp-circle-inside-1'></span>
          <span className='yp-circle-inside yp-circle-inside-2'></span>
          <span className='yp-circle-inside yp-circle-inside-3'></span>
        </div>
      </div>

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
    </div>


  );
};

export default LoginPage;
