import { NavLink, useLocation } from "react-router-dom";

const SingleItem = ({
  to,
  pageName,
  iconClass,
  label,
  currentPageName,
}: {
  to: string;
  pageName: string;
  iconClass: string;
  label: string;
  currentPageName:string;
}) => {
  const location = useLocation();
  const isActive = (path: string) => location.pathname === path;

  return (
    <li className={`yp-sidebar-item ${isActive(to) ? "active" : ""}`}>
      <NavLink
        to={to}
        className={
          pageName === currentPageName
            ? "yp-sidebar-link active"
            : "yp-sidebar-link"
        }
      >
        <i className={`yp-sidebar-icon ${iconClass}`}></i>
        <span>{label}</span>
      </NavLink>
    </li>
  );
};
export default SingleItem;
