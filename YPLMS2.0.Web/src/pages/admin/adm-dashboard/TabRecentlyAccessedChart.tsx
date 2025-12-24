import React, { useEffect, useRef, useState } from "react";
import { Tab } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { AppDispatch, RootState } from "../../../redux/store";
import { AssignmentItem } from "../../../redux/Slice/admin/adminDashboardSlice";

interface TabRecentlyAccessedChartProps {
  RecentDataAssignment: AssignmentItem[];
  activeTab: string;
}

const TabRecentlyAccessedChart: React.FC<TabRecentlyAccessedChartProps> = ({ RecentDataAssignment, activeTab }) => {

  const [animate, setAnimate] = useState(false);

  // ✅ Flatten array if nested
  const dataArray = Array.isArray(RecentDataAssignment[0])
    ? RecentDataAssignment[0]
    : RecentDataAssignment || [];

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

  /* For Chart  */
  // const data = [400, 360, 380, 300, 390];
  // const maxValue = Math.ceil(Math.max(...data) / 50) * 50; // rounded to next 50 => 400
  // const tickInterval = 50;
  // const xTicks = Array.from({ length: maxValue / tickInterval + 1 }, (_, i) => i * tickInterval);

  // const tickInterval = 2;
  // const maxValue = 20;
  // const xTicks = Array.from(
  //   { length: maxValue / tickInterval + 1 },
  //   (_, i) => i * tickInterval
  // );

  /* END For Chart  */


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


            {/* <div className="yp-admin-chart-box">
            {dataArray.length > 0 ? (
              dataArray.map((item) => {
                const percentage = (item.noOfCompletion / maxValue) * 100; // ✅ relative to x-axis scale

                return (
                  <div key={item.id} className="yp-admin-chart-row">
                    <div className="yp-admin-chart-col-title">
                      <p>{item.activityName}</p>
                    </div>
                    <div className="yp-admin-chart-col-progress">
                      <div
                        className="yp-admin-chart-total-progress"
                        style={{ width: animate ? "100%" : "0%" }}
                      >
                        <div
                          className="yp-admin-chart-actual-progress"
                          style={{ width: animate ? `${percentage}%` : "0%" }}
                        >
                          <div className="yp-admin-chart-progress-count">
                            <span>{item.noOfCompletion}</span>
                          </div>
                        </div>
                        <div className="yp-admin-chart-total-progress-count">
                          {item.noOfAssignement}
                        </div>
                      </div>
                    </div>
                  </div>
                );
              })
            ) : (
              <p>No recent assignments found</p>
            )}
          </div> */}
            <div className="yp-admin-chart-box">
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


            {/* <div className="yp-admin-chart-box">
            <div className="yp-admin-chart-row">
              <div className="yp-admin-chart-col-title">
                <p title="Advanced Evaluation">Advanced Evaluation</p>
              </div>
              <div className="yp-admin-chart-col-progress">
                <div className="yp-admin-chart-total-progress" style={{ width: animate ? `100%` : '0%' }}>
                  <div className="yp-admin-chart-actual-progress" style={{ width: animate ? '47.5%' : '0%' }}>
                    <div className="yp-admin-chart-progress-count">
                      <span>190</span>
                    </div>
                  </div>
                  <div className="yp-admin-chart-total-progress-count">400</div>
                </div>
              </div>
            </div>
            <div className="yp-admin-chart-row">
              <div className="yp-admin-chart-col-title">
                <p title="Assessment Insights and some text">Assessment Insights and some text</p>
              </div>
              <div className="yp-admin-chart-col-progress">
                <div className="yp-admin-chart-total-progress" style={{ width: animate ? `90%` : '0%' }}>
                  <div className="yp-admin-chart-actual-progress" style={{ width: animate ? '78%' : '0%' }}>
                    <div className="yp-admin-chart-progress-count">
                      <span>280</span>
                    </div>
                  </div>
                  <div className="yp-admin-chart-total-progress-count">360</div>
                </div>
              </div>
            </div>
            <div className="yp-admin-chart-row">
              <div className="yp-admin-chart-col-title">
                <p title="Essential Summary and some long title">Essential Summary and some long title</p>
              </div>
              <div className="yp-admin-chart-col-progress">
                <div className="yp-admin-chart-total-progress" style={{ width: animate ? '95%' : '0%' }}>
                  <div className="yp-admin-chart-actual-progress" style={{ width: animate ? '63%' : '0%' }}>
                    <div className="yp-admin-chart-progress-count">
                      <span>240</span>
                    </div>
                  </div>
                  <div className="yp-admin-chart-total-progress-count">380</div>
                </div>
              </div>
            </div>
            <div className="yp-admin-chart-row">
              <div className="yp-admin-chart-col-title">
                <p title="Evaluation Overview and some long title">Evaluation Overview and some long title</p>
              </div>
              <div className="yp-admin-chart-col-progress">
                <div className="yp-admin-chart-total-progress" style={{ width: animate ? '75%' : '0%' }}>
                  <div className="yp-admin-chart-actual-progress" style={{ width: animate ? '53%' : '0%' }}>
                    <div className="yp-admin-chart-progress-count">
                      <span>160</span>
                    </div>
                  </div>
                  <div className="yp-admin-chart-total-progress-count">300</div>
                </div>
              </div>
            </div>
            <div className="yp-admin-chart-row">
              <div className="yp-admin-chart-col-title">
                <p title="Assessment Findings and some long title">Assessment Findings and some long title</p>
              </div>
              <div className="yp-admin-chart-col-progress">
                <div className="yp-admin-chart-total-progress" style={{ width: animate ? '97%' : '0%' }}>
                  <div className="yp-admin-chart-actual-progress" style={{ width: animate ? '90%' : '0%' }}>
                    <div className="yp-admin-chart-progress-count">
                      <span>350</span>
                    </div>
                  </div>
                  <div className="yp-admin-chart-total-progress-count">390</div>
                </div>
              </div>
            </div>
          </div> */}

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

export default TabRecentlyAccessedChart;