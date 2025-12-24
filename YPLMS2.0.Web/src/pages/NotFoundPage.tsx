import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { RootState } from '../redux/store';
import { selectAuth } from '../redux/Slice/authSlice';
import { PageRoutes } from '../utils/Constants/Auth_PageRoutes'

const NotFoundPage: React.FC = () => {
  const { isAuthenticated } = useSelector((state: RootState) => selectAuth(state));
  const navigate = useNavigate();

  // If session is expired (user is not authenticated), redirect to login
  useEffect(() => {
    if (!isAuthenticated) {
      navigate(PageRoutes.LOGIN.FULL_PATH);
    }
  }, [isAuthenticated, navigate]);

  // If inside an iframe, suppress rendering entirely
  const isInIframe = window.self !== window.top;
  if (isInIframe) return null;


  return (
    <div style={{ textAlign: "center", marginTop: "50px" }}>
      <h1>404 - Page Not Found</h1>
      <p>The page you are looking for does not exist.</p>
      <button
        onClick={() => navigate(PageRoutes.LOGIN.FULL_PATH)}
        style={{
          padding: "10px 20px",
          fontSize: "16px",
          cursor: "pointer",
          borderRadius: "5px",
          border: "none",
          backgroundColor: "#007bff",
          color: "white",
          marginTop: "20px",
        }}
      >
        Go Back Login Page
      </button>
    </div>
  );
};

export default NotFoundPage;
