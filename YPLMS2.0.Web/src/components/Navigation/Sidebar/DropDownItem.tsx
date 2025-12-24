import { NavLink } from "react-router-dom";

const DropdownItem = ({
  to,
  subModuleName,
  label,
  currentSubModuleName,
}: {
  to: string;
  subModuleName: string;
  label: string;
  currentSubModuleName: string;
}) => {
  return (
    <li>
      <NavLink
        to={to}
        className={`dropdown-item ${
          subModuleName === currentSubModuleName ? "active" : ""
        }`}
      >
        <i className="zmdi zmdi-circle-o"></i> {label}
      </NavLink>
    </li>
  );
};

export default DropdownItem;
