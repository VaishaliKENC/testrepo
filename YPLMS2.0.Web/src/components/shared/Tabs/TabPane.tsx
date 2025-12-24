import React from "react";

type Props = {
  id: string;
  active: boolean;
  children: React.ReactNode;
};

const TabPane: React.FC<Props> = ({ id, active, children }) => {
  return (
    <div
      className={`tab-pane fade ${active ? "show active" : ""}`}
      id={id}
      role="tabpanel"
    >
      {children}
    </div>
  );
};

export default TabPane;
