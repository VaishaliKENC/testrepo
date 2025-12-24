import React from "react";
import TabPane from "./TabPane";
import { TabItem } from "../../../Types/assignmentTypes";

type Props = {
  tabs: TabItem[];
  activeTab: string;
  onTabChange: (tabId: string) => void;
  disableNextTabs?: boolean; 
};

const TabContainer: React.FC<Props> = ({
  tabs,
  activeTab,
  onTabChange,
  disableNextTabs = false, 
}) => {
  const activeTabIndex = tabs.findIndex((tab) => tab.id === activeTab);

  return (
    <>
      <ul className="nav nav-tabs yp-tabs" id="myTab" role="tablist">
        {tabs.map((tab, i) => {
          const isDisabled = disableNextTabs && i > activeTabIndex;

          return (
            <li
              className={`nav-item ${isDisabled ? "disabled" : ""}`}
              role="presentation"
              key={tab.id}
            >
              <button
                disabled={isDisabled}
                className={`nav-link ${activeTab === tab.id ? "active" : ""}`}
                onClick={() => onTabChange(tab.id)}
                id={tab.id}
                type="button"
                role="tab"
              >
                {tab.title}
              </button>
            </li>
          );
        })}
      </ul>

      <div className="tab-content">
        {tabs.map((tab) => (
          <TabPane key={tab.id} id={`${tab.id}Content`} active={activeTab === tab.id}>
            {tab.content}
          </TabPane>
        ))}
      </div>
    </>
  );
};

export default TabContainer;




// const TabContainer: React.FC<Props> = ({ tabs, activeTab, onTabChange }) => {

//   const activeTabIndex = tabs.findIndex((tab) => tab.id === activeTab);

//   return (
//     <>
//       <ul className="nav nav-tabs yp-tabs" id="myTab" role="tablist">
//         {tabs.map((tab, i) => (
//           <li className={`nav-item ${i > activeTabIndex ? "disabled" : ""}`} role="presentation" key={tab.id}>
//             <button
//               disabled={i > activeTabIndex}
//               className={`nav-link ${activeTab === tab.id ? "active" : ""}`}
//               onClick={() => onTabChange(tab.id)}
//               id={tab.id}
//               type="button"
//               role="tab"
//             >
//               {tab.title}
//             </button>
//           </li>
//         ))}
//       </ul>
//       <div className="tab-content">
//         {tabs.map((tab) => (
//           <TabPane key={tab.id} id={`${tab.id}Content`} active={activeTab === tab.id}>
//             {tab.content}
//           </TabPane>
//         ))}
//       </div>
//     </>
//   );
// };

// export default TabContainer;
