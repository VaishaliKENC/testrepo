import { Link } from "react-router-dom";
import leaderboardDefault from "../../../assets/images/admin-leaderboard-default.png";
import rankGold from "../../../assets/images/rank-gold.png";
import rankSilver from "../../../assets/images/rank-silver.png";
import rankBronze from "../../../assets/images/rank-bronze.png";
import { RootState } from "../../../redux/store";
import { useAppDispatch, useAppSelector } from "../../../hooks";
import { useEffect, useState } from "react";
import {
  getRankHolders,
  mapLeaderBoardCourseList,
} from "../../../utils/commonHelpers";
import {
  getLeaderBoardCourseList,
  getAdminLeaderBoardRankList,
} from "../../../redux/Slice/leaderBoard/leaderBoard.requests";
import { AdminPageRoutes } from "../../../utils/Constants/Admin_PageRoutes";
import { Autocomplete } from "../../../components/shared";

const AdminLeaderBoard = () => {
  const dispatch = useAppDispatch();
  const clientId: any = useAppSelector(
    (state: RootState) => state.auth.clientId
  );
  const { rankList, totalRows } = useAppSelector(
    (state: RootState) => state?.leaderBoard?.adminCourseLeaders
  );
  const [courseLeaders, setCourseLeaders] = useState(rankList);
  const [selectedCourse, setSelectedCourse] = useState(null);
  const courseList = useAppSelector((state: RootState) =>
    mapLeaderBoardCourseList(state?.leaderBoard?.courseList)
  );
  useEffect(() => {
    if (selectedCourse !== null) setCourseLeaders(rankList || []);
  }, [rankList]);

  useEffect(() => {
    dispatch(getLeaderBoardCourseList({ clientId }));
  }, []);
  const handleSelectChange = (selectedOption: any) => {

    if (selectedOption && selectedOption !== null) {
      setSelectedCourse(selectedOption);
      dispatch(
        getAdminLeaderBoardRankList({
          clientId,
          activityId: selectedOption.value,
        })
      );
    } else {
      setSelectedCourse(null);
      setCourseLeaders([]);
    }
  };
  return (
    <div className="yp-dashboard-cards-wrapper">
      <div className="yp-text-16-400 yp-color-482F58 mb-3">Leaderboard</div>
      <div className="yp-card yp-admin-dashboard-leaderboard-card">
        <div className="yp-admin-dashboard-leaderboard-box">
          <div className="yp-leaderboard-search-box">
            <div className="yp-form-control-with-icon yp-form-control-with-icon-right-side">
                  <div className="form-group mb-0">
                    <Autocomplete
                      items={courseList}
                      handleSelectChange={handleSelectChange}
                      placeholder="Type Course Name..."
                       title="Type Course Name..." 
                    />
                  </div>

                  {/* <LeaderBoardAutocomplete
                    options={courseList}
                    handleSelectChange={handleSelectChange}
                    placeholder="Type Course Name..."
                  /> */}
            </div>
          </div>

          <div className="yp-leaderboard-result-section">
            {selectedCourse === null && (
              <div className="yp-leaderboard-result-default">
                <img src={leaderboardDefault} alt="" title="" />
                <p>"Please select a course to view the top 3 ranked users.”</p>
              </div>
            )}

            {selectedCourse !== null && courseLeaders.length > 0 && (
              <div className="yp-leaderboard-winners-box ">
                {/* {getRankHolders(courseLeaders, "2nd").length > 0 && ( */}
                <div
                  className={
                    getRankHolders(courseLeaders, "2nd").length > 0
                      ? "yp-leaderboard-winner"
                      : "yp-leaderboard-winner yp-leaderboard-winner-disabled"
                  }
                  title={
                    getRankHolders(courseLeaders, "2nd").length > 0
                      ? ""
                      : "No data available"
                  }
                >
                  <img src={rankSilver} alt="" title="" />
                  <div className="yp-leaderboard-winner-score">
                    Score:{" "}
                    <strong>
                      {getRankHolders(courseLeaders, "2nd").length > 0
                        ? parseInt(
                            getRankHolders(courseLeaders, "2nd")?.[0]?.score
                          )
                        : "NA"}
                    </strong>
                  </div>
                  <div
                    className="yp-leaderboard-winner-name"
                    title={getRankHolders(courseLeaders, "2nd")?.[0]?.fullName}
                  >
                    {getRankHolders(courseLeaders, "2nd").length > 0
                      ? getRankHolders(courseLeaders, "2nd")?.[0]?.fullName
                      : "NA"}
                  </div>
                </div>
                {/* )} */}
                {/* {getRankHolders(courseLeaders, "1st").length > 0 && ( */}
                <div
                  className={
                    getRankHolders(courseLeaders, "1st").length > 0
                      ? "yp-leaderboard-winner"
                      : "yp-leaderboard-winner yp-leaderboard-winner-disabled"
                  }
                  title={
                    getRankHolders(courseLeaders, "1st").length > 0
                      ? ""
                      : "No data available"
                  }
                >
                  <img src={rankGold} alt="" title="" />
                  <div className="yp-leaderboard-winner-score">
                    Score:{" "}
                    <strong>
                      {getRankHolders(courseLeaders, "1st").length > 0
                        ? parseInt(
                            getRankHolders(courseLeaders, "1st")?.[0]?.score
                          )
                        : "NA"}
                    </strong>
                  </div>
                  <div
                    className="yp-leaderboard-winner-name"
                    title={getRankHolders(courseLeaders, "1st")?.[0]?.fullName}
                  >
                    {getRankHolders(courseLeaders, "1st").length > 0
                      ? getRankHolders(courseLeaders, "1st")?.[0]?.fullName
                      : "NA"}
                  </div>
                </div>
                {/* )} */}
                {/* {getRankHolders(courseLeaders, "3rd").length > 0 && ( */}
                <div
                  className={
                    getRankHolders(courseLeaders, "3rd").length > 0
                      ? "yp-leaderboard-winner"
                      : "yp-leaderboard-winner yp-leaderboard-winner-disabled"
                  }
                  title={
                    getRankHolders(courseLeaders, "3rd").length > 0
                      ? ""
                      : "No data available"
                  }
                >
                  <img src={rankBronze} alt="" title="" />
                  <div className="yp-leaderboard-winner-score">
                    Score:{" "}
                    <strong>
                      {getRankHolders(courseLeaders, "3rd").length > 0
                        ? parseInt(
                            getRankHolders(courseLeaders, "3rd")?.[0]?.score
                          )
                        : "NA"}
                    </strong>
                  </div>
                  <div
                    className="yp-leaderboard-winner-name"
                    title={getRankHolders(courseLeaders, "3rd")?.[0]?.fullName}
                  >
                    {getRankHolders(courseLeaders, "3rd").length > 0
                      ? getRankHolders(courseLeaders, "3rd")?.[0]?.fullName
                      : "NA"}
                  </div>
                </div>
                {/* )} */}
              </div>
            )}

            {selectedCourse !== null && courseLeaders.length > 0 && (
              <Link
                to={`${
                  AdminPageRoutes.ADMIN_LEADER_BOARD.FULL_PATH
                }?activityId=${encodeURIComponent(
                  JSON.stringify(selectedCourse)
                )}`}
                className="btn btn-primary"
              >
                View Complete Leaderboard
              </Link>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};
export default AdminLeaderBoard;