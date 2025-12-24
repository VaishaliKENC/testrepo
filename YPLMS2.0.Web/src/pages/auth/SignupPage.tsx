import React, { useState, useEffect } from 'react';
import { useAppDispatch, useAppSelector } from '../../hooks';
import logo from '../../assets/images/enc_logo 1.png';
import { Link, useNavigate } from 'react-router-dom';
import { PageRoutes } from '../../utils/Constants/Auth_PageRoutes';
import { AdminPageRoutes } from '../../utils/Constants/Admin_PageRoutes';
import { RootState } from '../../redux/store';
import { checkavailablity, fetchClientConfigByClientIdSlice, fetchClientIdByUrlSlice, signup, signupAdminApprove } from '../../redux/Slice/authSlice';
import AlertMessage from '../../components/shared/AlertMessage';
import { useSelector } from 'react-redux';

let debounceTimer: any;

const SignupPage: React.FC = () => {
  const dispatch = useAppDispatch();
  const { loading, error } = useAppSelector((state: RootState) => state.auth);
  const navigate = useNavigate();
  // const [ClientId, setClientID] = useState('CLIYPRevamp_New');
  // const [ClientId, setClientID] = useState('CLIYPRevamp_QA');
  const { clientId } = useSelector((state: RootState) => state.auth);
  const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);
  const [UserNameAlias, setLoginID] = useState('');
  const [isSelfRegistration, setIsSelfRegistration] = useState(false);
  const [isSelfRegistrationAdminApproval, setIsSelfRegistrationAdminApproval] = useState(false);
  const [isPassCodeBased, setIsPassCodeBased] = useState(false);
  const [isEmailDomainBased, setIsEmailDomainBased] = useState(false);
  const [formData, setFormData] = useState({
    FirstName: '',
    LastName: '',
    UserNameAlias: '',
    EmailID: '',
    ClientId: clientId,
    country: '',
    organization: '',
    department: '',
    role: ''
  });

  const handleReset = () => {
    setFormData({
      FirstName: '',
      LastName: '',
      UserNameAlias: '',
      EmailID: '',
      ClientId: clientId,
      country: '',
      organization: '',
      department: '',
      role: ''
    });
  };


  const [formErrors, setFormErrors] = useState<Record<string, string>>({});
  const [formSuccess, setFormSuccess] = useState<Record<string, string>>({});

  useEffect(() => {
    document.title = 'Signup';
    document.body.removeAttribute("id");

    try {
      const apiBaseUrl = process.env.REACT_APP_API_BASE_URL;
      const url = new URL(apiBaseUrl || "");
      const clientAccessURL = url.hostname;

      dispatch(fetchClientIdByUrlSlice({ clientAccessURL }))
        .then((response: any) => {
          if (response.meta.requestStatus === 'fulfilled') {
            // setClientID(response.payload.clientId); // or ClientId based on API   
            
            if(clientId) {
              dispatch(fetchClientConfigByClientIdSlice(clientId))
                .then((response: any) => {
                  if (response.meta.requestStatus === 'fulfilled') {
                    setIsSelfRegistration(response.payload.client.isSelfRegistration);
                    setIsSelfRegistrationAdminApproval(response.payload.client.isSelfRegistrationAdminApproval);
                  }
                })
              .catch(() => {
                setAlert({ type: "error", message: "Error fetching client ID by URL" });
              });
            }
            
            
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
    setFormSuccess((prev) => ({ ...prev, UserNameAlias: '' }));

    if (typeof value === 'string') {
      if (value.trim() && !isValidInput(value)) {
        return "Special characters like < > ' & are not allowed.";
      }
    }

    switch (name) {
      case "FirstName":
        if (!value.trim()) return "First name is required";
        break;
      case "LastName":
        if (!value.trim()) return "Last name is required";
        break;
      case "UserNameAlias":
        if (!value.trim()) return "Login ID is required";
        break;
      case "EmailID":
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!value.trim()) return "Email is required";
        if (value && !emailRegex.test(value)) return "Invalid email format";
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
    const checked = (target as HTMLInputElement).checked; // Safely access 'checked' for inputs of type checkbox

    setFormData({
      ...formData,
      [name]: target.type === 'checkbox' ? checked : value,
    });

    // Validate the field
    const error = validateField(name, type === "checkbox" ? String(checked) : value);

    // Update form errors
    setFormErrors({
      ...formErrors,
      [name]: error,
    });

    if (name === "UserNameAlias") {
      clearTimeout(debounceTimer);
      debounceTimer = setTimeout(() => {
        if (formData.UserNameAlias != "") {
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
    if (formErrors.UserNameAlias) {
      return;
    }

    if(isSelfRegistration) {
    //isSelfRegistration means through self registration flow
      let FirstName = formData.FirstName;
      let LastName = formData.LastName;
      let EmailID = formData.EmailID;
      let UserNameAlias = formData.UserNameAlias;
      let ClientId = formData.ClientId;


      dispatch(signup({ FirstName, LastName, EmailID, UserNameAlias, ClientId: clientId }))
        .then((response: any) => {
          // If signup is successful, navigate to the login
          if (response.meta.requestStatus == "fulfilled") {
            setAlert({ type: "success", message: "User added successfully." });
            setTimeout(() => {
              navigate(PageRoutes.LOGIN.FULL_PATH);
            }, 2000);
          } else {
            setAlert({ type: "error", message: 'Signup failed: ' + response.payload });
          }
        })
        .catch((error) => {
          console.error("signup error:", error);
          setAlert({ type: "error", message: 'An unexpected error occurred.' });
        });
    }

    if(isSelfRegistrationAdminApproval) {
        // isSelfRegistrationAdminApproval means through admin approval flow
        let firstName = formData.FirstName;
        let lastName = formData.LastName;
        let emailId = formData.EmailID;
        let loginId = formData.UserNameAlias;
        let clientId = formData.ClientId;
        let country = formData.country;
        let organization = formData.organization;
        let department = formData.department;
        let role = formData.role;
        let systemuserGUID = '';

        dispatch(signupAdminApprove({ firstName, lastName, emailId, loginId, clientId: clientId, country, organization, department, role, systemuserGUID}))
        .then((response: any) => {
          // If signup is successful, navigate to the login
          if (response.meta.requestStatus == "fulfilled") {
          setAlert({ type: "success", message: "Your account has been created and is awaiting admin approval." });
          setTimeout(() => {
            navigate(PageRoutes.LOGIN.FULL_PATH);
          }, 2000);
          } else {
          setAlert({ type: "error", message: 'Signup failed: ' + response.payload });
          }
        })
        .catch((error) => {
          console.error("signup error:", error);
          setAlert({ type: "error", message: 'An unexpected error occurred.' });
        });
    }

    console.log("firstName", formData.FirstName);
    console.log("lastName", formData.LastName);
    console.log("email", formData.EmailID);
    console.log("loginId", formData.UserNameAlias);
    // Handle form submission logic here
  };

  const Checkavailablity = (typedValue: string) => {
    if (!typedValue) {
      setFormErrors((prev) => ({ ...prev, UserNameAlias: 'Login ID is required' }));
      return;
    }
    if (typedValue.trim() && !isValidInput(typedValue)) {
      setFormErrors((prev) => ({ ...prev, UserNameAlias: "Special characters like < > ' & are not allowed." }));
      return;
    }

    setAlert(null);
    setFormErrors((prev) => ({ ...prev, UserNameAlias: '' })); // Clear any existing errors for this field
    dispatch(checkavailablity({ UserNameAlias: typedValue, ClientId: clientId }))
      .then((response: any) => {
        // console.log(response);
        if (response.meta.requestStatus === 'fulfilled') {
          setFormSuccess((prev) => ({ ...prev, UserNameAlias: response.payload?.msg }));
          // setFormErrors((prev) => ({ ...prev, UserNameAlias: 'Username already exists. Please enter another Username.' }));
          setFormErrors((prev) => ({ ...prev, UserNameAlias: '' }));
        } else {
          //response.payload?.msg
          setFormErrors((prev) => ({ ...prev, UserNameAlias: response.payload?.msg }));
          // setFormSuccess((prev) => ({ ...prev, UserNameAlias: 'Username is available.' }));
          setFormSuccess((prev) => ({ ...prev, UserNameAlias: '' }));
        }
      })
      .catch((error) => {
        console.error('Error checking availability:', error);
        setFormErrors((prev) => ({ ...prev, UserNameAlias: 'An unexpected error occurred while checking availability.' }));
      });
  }

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
                  <h4 className='yp-text-40-500 yp-text-primary'>Signup</h4>
                </div>
              </div>
            </div>
            <div className="col-md-6">
              <div className='yp-signupFormWrapper'>
                <div className='yp-card-heading-row d-flex justify-content-end align-items-center gap-2'>
                  <h4 className='yp-text-28-500  '>
                    <Link to={PageRoutes.LOGIN.FULL_PATH} className="yp-no-underline yp-link">Login</Link>
                  </h4>
                  <i className="fa fa-arrow-right yp-text-primary mb-1 yp-fs-25" aria-hidden="true"></i>
                </div>
              </div>
            </div>
          </div>


          <div className="row">
            <div className="col-12 col-md-6 col-lg-6">
              <div className="form-group">
                <div className="yp-form-control-wrapper">
                  <input type="text"
                    className={`form-control yp-form-control ${formErrors.FirstName ? 'is-invalid' : ''}`}
                    name="FirstName"
                    value={formData.FirstName}
                    // onChange={handleInputChange}
                    onChange={(e) => {
                      const value = e.target.value;
                      // Prevent leading space
                      if (formData.FirstName === '' && value.startsWith(' ')) return;
                      handleInputChange(e);
                    }}
                    onKeyDown={(e) => {
                      // Prevent space as first character
                      if (e.key === ' ' && formData.FirstName.length === 0) { e.preventDefault(); }
                    }}
                    id="" placeholder="" />
                  <label className="form-label"><span className='yp-asteric'>*</span>First Name</label>
                </div>
                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                  {formErrors.FirstName && <div className="invalid-feedback px-3">{formErrors.FirstName}</div>}
                </div>
              </div>
            </div>
            <div className="col-12 col-md-6 col-lg-6">
              <div className="form-group">
                <div className="yp-form-control-wrapper">
                  <input type="text"
                    className={`form-control yp-form-control ${formErrors.LastName ? 'is-invalid' : ''}`}
                    name="LastName"
                    value={formData.LastName}
                    // onChange={handleInputChange}
                    onChange={(e) => {
                      const value = e.target.value;
                      // Prevent leading space
                      if (formData.LastName === '' && value.startsWith(' ')) return;
                      handleInputChange(e);
                    }}
                    onKeyDown={(e) => {
                      // Prevent space as first character
                      if (e.key === ' ' && formData.LastName.length === 0) { e.preventDefault(); }
                    }}
                    id="" placeholder="" />
                  <label className="form-label"><span className='yp-asteric'>*</span>Last Name</label>
                </div>
                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                  {formErrors.LastName && <div className="invalid-feedback px-3">{formErrors.LastName}</div>}
                </div>
              </div>
            </div>
          </div>
          <div className="row">
            <div className="col-12 col-md-6 col-lg-6">
              <div className="form-group yp-form-group-with-note">
                <div className="yp-form-control-wrapper">
                  <input type="text"
                    className={`form-control yp-form-control ${formErrors.UserNameAlias ? 'is-invalid' : ''}`}
                    name="UserNameAlias"
                    value={formData.UserNameAlias}
                    // onChange={(e) => { handleInputChange(e); setLoginID(e.target.value); }}
                    onChange={(e) => {
                      const value = e.target.value;
                      // Prevent leading space on paste or change
                      if (formData.UserNameAlias === '' && value.startsWith(' ')) return;
                      handleInputChange(e);
                      setLoginID(value);
                    }}
                    onKeyDown={(e) => {
                      // Prevent space if it's the first character
                      if (e.key === ' ' && formData.UserNameAlias.length === 0) {
                        e.preventDefault();
                      }
                    }}
                    id="" placeholder="" />
                  <label className="form-label"><span className='yp-asteric'>*</span>Login ID</label>
                </div>
                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                  {formErrors.UserNameAlias && <div className="invalid-feedback px-3">{formErrors.UserNameAlias}</div>}
                  {formSuccess.UserNameAlias && <div className="valid-feedback px-3">{formSuccess.UserNameAlias}</div>}
                </div>

              </div>
            </div>
            <div className="col-12 col-md-6 col-lg-6">
              <div className="form-group">
                <div className="yp-form-control-wrapper">
                  <input type="email"
                    className={`form-control yp-form-control ${formErrors.EmailID ? 'is-invalid' : ''}`}
                    name="EmailID"
                    value={formData.EmailID}
                    // onChange={handleInputChange}
                    onChange={(e) => {
                      const value = e.target.value;
                      // Prevent leading space
                      if (formData.EmailID === '' && value.startsWith(' ')) return;
                      handleInputChange(e);
                    }}
                    onKeyDown={(e) => {
                      // Prevent space as first character
                      if (e.key === ' ' && formData.EmailID.length === 0) { e.preventDefault(); }
                    }}
                    id="" placeholder="" />
                  <label className="form-label"><span className='yp-asteric'>*</span>Email Address</label>
                </div>
                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                  {formErrors.EmailID && <div className="invalid-feedback px-3">{formErrors.EmailID}</div>}
                </div>
              </div>
            </div>
          </div>
          <div className="row">
            <div className="col-12 col-md-6 col-lg-6">
              <div className="form-group">
              <div className="yp-form-control-wrapper">
                <select
                className={`form-control yp-form-control`}
                name="country"
                value={formData.country}
                onChange={handleInputChange}
                id=""
                >
                  <option value={''} selected>Select</option>
                  <option value={'India'}>India</option>
                  <option value={'US'}>US</option>
                  <option value={'Japan'}>Japan</option>
                </select>
                <label className="form-label">Country</label>
              </div>
              </div>
            </div>
            <div className="col-12 col-md-6 col-lg-6">
              <div className="form-group">
              <div className="yp-form-control-wrapper">
                <input type="text"
                className={`form-control yp-form-control`}
                name="organization"
                value={formData.organization}
                // onChange={handleInputChange}
                onChange={(e) => {
                  const value = e.target.value;
                  // Prevent leading space
                  if (formData.organization === '' && value.startsWith(' ')) return;
                  handleInputChange(e);
                }}
                onKeyDown={(e) => {
                  // Prevent space as first character
                  if (e.key === ' ' && formData.organization.length === 0) { e.preventDefault(); }
                }}
                id="" placeholder="" />
                <label className="form-label">Organization</label>
              </div>
              </div>
            </div>
            </div>

            <div className="row">
            <div className="col-12 col-md-6 col-lg-6">
              <div className="form-group">
              <div className="yp-form-control-wrapper">
                <input type="text"
                className={`form-control yp-form-control`}
                name="department"
                value={formData.department}
                // onChange={handleInputChange}
                onChange={(e) => {
                  const value = e.target.value;
                  // Prevent leading space
                  if (formData.department === '' && value.startsWith(' ')) return;
                  handleInputChange(e);
                }}
                onKeyDown={(e) => {
                  // Prevent space as first character
                  if (e.key === ' ' && formData.department.length === 0) { e.preventDefault(); }
                }}
                id="" placeholder="" />
                <label className="form-label">Department</label>
              </div>
              </div>
            </div>
            <div className="col-12 col-md-6 col-lg-6">
              <div className="form-group">
              <div className="yp-form-control-wrapper">
                <input type="text"
                className={`form-control yp-form-control`}
                name="role"
                value={formData.role}
                // onChange={handleInputChange}
                onChange={(e) => {
                  const value = e.target.value;
                  // Prevent leading space
                  if (formData.role === '' && value.startsWith(' ')) return;
                  handleInputChange(e);
                }}
                onKeyDown={(e) => {
                  // Prevent space as first character
                  if (e.key === ' ' && formData.role.length === 0) { e.preventDefault(); }
                }}
                id="" placeholder="" />
                <label className="form-label">Role</label>
              </div>
              </div>
            </div>
            </div>
          <div className="row">
            <div className="col-lg-12">
              <div className='yp-form-button-row'>
                <button
                  onClick={handleSubmit}
                  type='submit'
                  className='btn btn-primary yp-btn-forgot-signup yp-width-200-px'
                  disabled={loading}
                >
                  Signup
                </button>
              </div>
            </div>
          </div>

          {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}

        </div>
      </form>

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

export default SignupPage;
