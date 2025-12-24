import { Navigate, Outlet } from "react-router-dom";
import { useSelector } from "react-redux";
import { selectAuth } from "../redux/Slice/authSlice";

const ProtectedRoutes = () => {
  const { isAuthenticated } = useSelector(selectAuth);

  return isAuthenticated ? <Outlet /> : <Navigate to="/login" replace />;
};

export default ProtectedRoutes;
