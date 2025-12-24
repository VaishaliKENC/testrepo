import React, { useEffect, useState } from "react";
import "./Navbar.css";
import loginUser from '../../../assets/images/LoginUser_pic.png';
import encLogo from '../../../assets/images/enc_logo 1.png';
import { Link, useNavigate } from "react-router-dom";
import { useSelector } from "react-redux";
import { RootState } from "../../../redux/store";
import { fetchClientIdByUrlSlice, logoutUser, selectAuth, switchUserType } from "../../../redux/Slice/authSlice";
import { fetchPendingForApprovalsCountSlice } from "../../../redux/Slice/admin/userSlice";
import { useAppDispatch, useAppSelector } from "../../../hooks";
import { PageRoutes } from "../../../utils/Constants/Auth_PageRoutes";
import { LearnerPageRoutes } from "../../../utils/Constants/Learner_PageRoutes";
import { AdminPageRoutes } from "../../../utils/Constants/Admin_PageRoutes";
import iconPendingApproval from "../../../assets/images/pending-approvals.png";

const Navbar: React.FC = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const [roleIds, setRoleIds] = useState<string[]>([]);
  const { user, token } = useSelector((state: RootState) => selectAuth(state));
  const userTypeId = useAppSelector((state) => state.auth.userTypeId);

  const clientId: any = useSelector((state: RootState) => state.auth.clientId);
  const pendingApprovalsCount: any = useSelector((state: RootState) => state.user.pendingForApprovalsCount);
  const pendingApprovalsList: any = useSelector((state: RootState) => state.user.usersPendingApprovals);
  // Local states
  const [pendingApprovalsUserCount, setPendingApprovalsUserCount] = useState(0);

  // Extract role IDs from the user object (assuming it's stored in Redux)
  // const roleIds: string[] = useSelector(
  //   (state: RootState) => state.auth.user?.learner?.userAdminRole?.map((role) => role.roleId) || []
  // );


  // Effect → Sync role IDs from Redux / localStorage
  useEffect(() => {
    const fetchedRoleIds = user?.learner?.userAdminRole?.map((role) => role.roleId) || [];
    if (fetchedRoleIds.length > 0) {
      setRoleIds(fetchedRoleIds);
      localStorage.setItem("roleIds", JSON.stringify(fetchedRoleIds));
    } else {
      const storedRoleIds = JSON.parse(localStorage.getItem("roleIds") || "[]");
      setRoleIds(storedRoleIds);
    }
  }, [user]);


  // const logout = () => {
  //   if (token) {
  //     dispatch(logoutUser({ token }));
  //   }

  //   localStorage.removeItem("roleIds");
  //   localStorage.removeItem("userTypeId");
  //   localStorage.removeItem("token");
  //   localStorage.removeItem("user");
  // };

  // const logout = async () => {
  //   if (!token) return;

  //   try {
  //     await dispatch(logoutUser({ token })).unwrap(); // Wait for API success

  //   } catch (error) {
  //     console.error("Logout API failed:", error);
  //   }

  //   localStorage.removeItem("roleIds");
  //   localStorage.removeItem("userTypeId");
  //   localStorage.removeItem("token");
  //   localStorage.removeItem("user");

  //   // Navigate after API call finishes
  //   navigate(PageRoutes.LOGIN.FULL_PATH);
  // };


  const logout = () => {
    try {
      const apiBaseUrl = process.env.REACT_APP_API_BASE_URL;
      if (!apiBaseUrl) {
        console.error("REACT_APP_API_BASE_URL is not set");
        return;
      }

      const { hostname: clientAccessURL } = new URL(apiBaseUrl);

      const doRedirect = () => {
        localStorage.clear();
        navigate(PageRoutes.LOGIN.FULL_PATH);
      };

      if (!token) {
        // If no token, just fetch clientId and redirect
        dispatch(fetchClientIdByUrlSlice({ clientAccessURL }))
          .finally(doRedirect);
        return;
      }

      // If token exists, logout first, then fetch clientId, then redirect
      dispatch(logoutUser({ token }))
        .unwrap()
        .catch((err) => {
          console.error("Logout API failed:", err);
        })
        .finally(() => {
          dispatch(fetchClientIdByUrlSlice({ clientAccessURL }))
            .catch((err) => {
              console.error("Fetch client ID failed:", err);
            })
            .finally(doRedirect);
        });

    } catch (err) {
      console.error("Error in logout:", err);
      localStorage.clear();
      navigate(PageRoutes.LOGIN.FULL_PATH);
    }
  };

  const handleSwitch = () => {
    if (userTypeId === "Admin") {
      dispatch(switchUserType("Learner")); //  Update Redux + Session
      navigate(LearnerPageRoutes.LEARNER_DASHBOARD.FULL_PATH); //  Navigate to learner dashboard
    } else {
      dispatch(switchUserType("Admin"));
      navigate(AdminPageRoutes.ADMIN_DASHBOARD.FULL_PATH); //  Navigate to admin dashboard
    }
  };

  /* Pending Approvals Count  */
  useEffect(() => {
      if (!clientId) return;
      fetchPendingApprovalsCount();
  }, [clientId, pendingApprovalsList]);

  const fetchPendingApprovalsCount = () => {
      const params = {
        clientId
      };
      dispatch(fetchPendingForApprovalsCountSlice(params));

      dispatch(fetchPendingForApprovalsCountSlice(params))
        .then((response: any) => {
          if (response.meta.requestStatus === 'fulfilled') {
            setPendingApprovalsUserCount(response.payload.pendingCount);
          }
        })
        .catch(() => {
          console.log("Error fetching pending approvals count api");
        });
  };
  const handleGotoPage = (page: string) => {
    navigate(page);
  };

  return (
    <nav className="yp-navbar-container">
      <div className="yp-navbar-left-wrapper">
        <img src={encLogo} className="yp-navbar-logo" alt="YPLMS Logo" />
      </div>
      <div className="yp-navbar-right-wrapper">
        <div className="yp-navbar-menus">
          <div className="yp-navbar-menu yp-navbar-menu-icon-with-notification">
            <a href="#" 
              className="yp-link"
              onClick={(e) => {
                e.preventDefault(); 
                handleGotoPage(AdminPageRoutes.ADMIN_PENDING_APPROVALS.FULL_PATH);
              }}>
              <span className="yp-navbar-menu-icon">
                <span className="yp-navbar-menu-icon-with-notification-wrapper">
                  <img src={iconPendingApproval} alt="Pending Approvals" />
                  <span className="yp-navbar-menu-notification-count">{pendingApprovalsUserCount}</span>
                </span>
              </span>
              <span className="yp-navbar-menu-text">
                Pending Approvals
              </span>
            </a>
          </div>

          {/* <div className="yp-navbar-menu-switchto">
            {
              userTypeId === "Admin" && (
                <a href="#" className="yp-link"  onClick={handleSwitch}>
                  <span>Switch to</span><br />
                  Learner <i className="fa fa-right-left" aria-hidden="true"></i>
                </a>
              )
            }
            {
              userTypeId === "Learner" && (
                <a href="#" className="yp-link"  onClick={handleSwitch}>
                  <span>Switch to</span><br />
                  Admin <i className="fa fa-right-left" aria-hidden="true"></i>
                </a>
              )
            }
          </div>  */}

          <div className="yp-navbar-menu-switchto">
            {/* Show only if the user has BOTH roles  */}
            {roleIds.includes("ROL0001") && roleIds.includes("ROL0005") && (
              <>
                {/* Show switch to Learner only if current is Admin */}
                {userTypeId === "Admin" && (
                  <a href="#" className="yp-link"
                    onClick={(e) => {
                      e.preventDefault(); // Prevents the "#" issue
                      handleSwitch();
                    }}
                  >
                    <span>Switch to</span><br />
                    Learner <i className="fa fa-right-left" aria-hidden="true"></i>
                  </a>
                )}

                {/* Show switch to Admin only if current is Learner */}
                {userTypeId === "Learner" && (
                  <a href="#" className="yp-link"
                    onClick={(e) => {
                      e.preventDefault(); // Prevents the "#" issue
                      handleSwitch();
                    }}
                  >
                    <span>Switch to</span><br />
                    Admin <i className="fa fa-right-left" aria-hidden="true"></i>
                  </a>
                )}
              </>
            )}
          </div>

          {/* <div className="yp-navbar-menu-notification yp-navbar-menu-icon-with-notification">
            <a href="#">
              <span className="yp-navbar-menu-icon-with-notification-wrapper">
                <i className="fa fa-bell"></i>
                <span className="yp-navbar-menu-notification-count">19</span>
              </span>
            </a>
          </div> */}

          <div className="yp-navbar-menu-profile">
            <button type="button"
              className="dropdown-toggle"
              data-bs-toggle="dropdown"
              aria-expanded="false">
              <img src={loginUser} alt='Profile' title='' />
            </button>
            <ul className="dropdown-menu yp-dropdown-menu dropdown-menu-end">
              {/* <Link to="/login" className="dropdown-item" onClick={logout}>
                Logout
              </Link> */}
              <li className="dropdown-item" onClick={logout}>
                Logout
              </li>
            </ul>
          </div>
        </div>
      </div>
    </nav>

  );
};

export default Navbar;

