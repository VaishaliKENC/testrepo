import React from "react";
import { NavLink, useLocation } from "react-router-dom";
import "./Sidebar.css";
import SingleItem from "./SingleItem";
import DropdownItem from "./DropDownItem";
import { adminSidebar, learnerSidebar } from "../../../utils/sidebarMock";
import { useAppSelector } from "../../../hooks";

const Sidebar: React.FC = () => {
  const location = useLocation();

  const segments = location.pathname.split("/").filter((item) => item !== "");
  const [segment1, segment2, segment3] = segments;
  let getModuleName = segment1 || "";
  let getSubModuleName = segment2 || segment1 || "";
  let getPageName = segment3 || segment2 || segment1 || "";
  const userTypeId = useAppSelector((state) => state.auth.userTypeId);
  const sideBarData =  userTypeId === "Admin" ? adminSidebar : learnerSidebar
  return (
    <div className="yp-sidebar">
      <ul className="yp-sidebar-list">
        {sideBarData?.map((item: any) => (
          <>
            {item?.path && item?.subModule?.length < 1 ? ( //for just single links
              <SingleItem
                to={item?.path}
                pageName={item?.pageName}
                iconClass={item?.iconClass}
                label={item?.label}
                currentPageName={getPageName}
              />
            ) : (
              <li
                className={`yp-sidebar-item dropdown ${
                  getModuleName === item?.moduleName ? "active" : ""
                }`}
              >
                <NavLink
                  to={item.path}
                  className={`yp-sidebar-link dropdown-toggle ${
                    getModuleName === item?.moduleName ? "active show" : ""
                  }`}
                  role="button"
                  data-bs-toggle="dropdown"
                  aria-expanded={
                    getModuleName === item?.moduleName ? "true" : "false"
                  }
                  data-bs-auto-close="true"
                >
                  <i className={item?.iconClass}></i>
                  <span>{item.label}</span>
                </NavLink>
                {item?.subModule?.length > 0 && (
                  <ul className={`dropdown-menu`}>
                    {item?.subModule?.map(
                      (
                        indv: any //submodules
                      ) => (
                        <DropdownItem
                          key={indv?.label}
                          to={indv?.path}
                          label={indv?.label}
                          subModuleName={indv?.subModuleName}
                          currentSubModuleName={getSubModuleName}
                        />
                      )
                    )}
                  </ul>
                )}
              </li>
            )}
          </>
        ))}
      </ul>
    </div>
  );
};

export default Sidebar;
