import React, { useEffect, useRef, useState } from "react";
import { AssignmentItem } from "../../../redux/Slice/admin/adminDashboardSlice";

interface TabTabTopAssignedChartProps {
  TopDataAssignment: AssignmentItem[];
  activeTab: string;
}

const TabTopAssignedChart: React.FC<TabTabTopAssignedChartProps> = ({ TopDataAssignment, activeTab }) => {

  const [animate, setAnimate] = useState(false);
  //const [activeTab, setActiveTab] = useState<string>("tabTopAssigned");

  // ✅ Flatten array if nested
  const dataArray = Array.isArray(TopDataAssignment[0])
    ? TopDataAssignment[0]
    : TopDataAssignment || [];

  // console.log("dataArray", dataArray)

  const highestValue =
    dataArray.length > 0
      ? Math.max(...dataArray.map((item) => item.noOfAssignement || 0))
      : 0;

  // ✅ Decide tick gap (2, 5, or 10 based on value range)
  const tickInterval = highestValue <= 10 ? 2 : highestValue <= 50 ? 5 : 10;

  // ✅ Round up maxValue to next tick
  const maxValue = Math.ceil(highestValue / tickInterval) * tickInterval;

  const xTicks = Array.from(
    { length: maxValue / tickInterval + 1 },
    (_, i) => i * tickInterval
  );

  useEffect(() => {
    const timeout = setTimeout(() => setAnimate(true), 1000); // delay ensures transition
    setAnimate(false);    
    return () => clearTimeout(timeout);
  }, [activeTab]);

  return (
    <>
    {dataArray.length === 0 ? (
        <div className="yp-admin-chart-box-no-data">
          <p className="text-center py-3">No activities found</p>
        </div>
      ) : (
      <div className="yp-admin-chart-box-wrapper">
        <div className="yp-admin-chart-box-label-wrapper">
          <div className="yp-admin-chart-legends">
            <div className="yp-admin-chart-legend">
              <div className="yp-admin-chart-legend-color yp-bg-color-B49ECA"></div>
              <div className="yp-admin-chart-legend-text">No. of Completion</div>
            </div>
            <div className="yp-admin-chart-legend">
              <div className="yp-admin-chart-legend-color yp-bg-color-EBE9ED"></div>
              <div className="yp-admin-chart-legend-text">No. of Assignment</div>
            </div>
          </div>

          <div className="yp-admin-chart-box">
            {/* {dataArray.length > 0 ? ( */}
              {dataArray.map((item) => {
                // Outer bar width → scale with noOfAssignement relative to X-axis maxValue
                const outerWidth = (item.noOfAssignement / maxValue) * 100;

                // Inner bar width → completion percentage inside that bar
                const innerWidth =
                  (item.noOfCompletion / (item.noOfAssignement || 1)) * 100;

                return (
                  <div key={item.id} className={`yp-admin-chart-row ${animate ? "" : "no-transition"}`}>
                    <div className="yp-admin-chart-col-title" title={item.activityName}>
                      <p>{item.activityName}</p>
                    </div>

                    <div className="yp-admin-chart-col-progress" 
                      title={`${item.activityName}\nNo. of Assignments: ${item.noOfAssignement}\nNo. of Completion: ${item.noOfCompletion}`}>
                      <div
                        className="yp-admin-chart-total-progress"
                        style={{ width: animate ? `${outerWidth}%` : "0%" }}
                      >
                        <div
                          className="yp-admin-chart-actual-progress"
                          style={{ width: animate ? `${innerWidth}%` : "0%" }}
                        >
                          <div className="yp-admin-chart-progress-count">
                            <span>{item.noOfCompletion}</span>
                          </div>
                        </div>

                        {/* Total Assignments Label */}
                        <div className="yp-admin-chart-total-progress-count">
                          {item.noOfAssignement}
                        </div>
                      </div>
                    </div>
                  </div>
                );
              })}
          </div>

          <div className="yp-admin-chart-x-axis">
            {xTicks.map((tick) => (
              <div key={tick} className="yp-admin-chart-x-tick">
                <span>{tick}</span>
              </div>
            ))}
          </div>

          <div className="yp-admin-chart-xy-labels">
            <div className="yp-admin-chart-xy-label yp-admin-chart-x-label">No. of Users</div>
            <div className="yp-admin-chart-xy-label yp-admin-chart-y-label">Assignments/Activities</div>
          </div>
        </div>
      </div>
       )}
    </>
  )
};

export default TabTopAssignedChart;




