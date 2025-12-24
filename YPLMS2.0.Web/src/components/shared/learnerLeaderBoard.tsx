import { Link } from "react-router-dom";
import learnerLeaderboardBg from "../../assets/images/learner-leaderboard-bg.png";
import rankGold from "../../assets/images/rank-gold.png";
import rankSilver from "../../assets/images/rank-silver.png";
import rankBronze from "../../assets/images/rank-bronze.png";
import { LearnerPageRoutes } from "../../utils/Constants/Learner_PageRoutes";
import { useEffect, useState } from "react";
import {
  getLeaderBoardCourseList,
  getLearnerLeaderBoardRankList,
} from "../../redux/Slice/leaderBoard/leaderBoard.requests";
import { RootState } from "../../redux/store";
import { useAppDispatch, useAppSelector } from "../../hooks";
import {
  getRankHolders,
  mapLeaderBoardCourseList,
} from "../../utils/commonHelpers";
import Autocomplete from "./CommonComponents/Autocomplete";

const LearnerLeaderBoard = () => {
  const dispatch = useAppDispatch();
  const clientId: any = useAppSelector(
    (state: RootState) => state.auth.clientId
  );
  const userId: any = useAppSelector((state: RootState) => state.auth.id);
  const rankList = useAppSelector(
    (state: RootState) => state?.leaderBoard?.learnerCourseLeaders
  );
  const [courseLeaders, setCourseLeaders] = useState<any[]>([]);
  const [selectedCourse, setSelectedCourse] = useState(null);
  const courseList = useAppSelector((state: RootState) =>
    mapLeaderBoardCourseList(state?.leaderBoard?.courseList)
  );
  useEffect(() => {
    if (selectedCourse !== null) setCourseLeaders(rankList);
  }, [rankList]);

  useEffect(() => {
    dispatch(getLeaderBoardCourseList({ clientId, userId }));
  }, []);

  const handleSelectChange = (selectedOption: any) => {
    if (selectedOption && selectedOption !== null) {
      setSelectedCourse(selectedOption);
      dispatch(
        getLearnerLeaderBoardRankList({
          clientId,
          systemId: userId,
          activityId: selectedOption.value,
          rows: 3,
        })
      );
    } else {
      setSelectedCourse(null);
      setCourseLeaders([]);
    }
  };

  const filterMyRank = () => {
    return courseLeaders.filter((rank: any) => {
      return rank.systemUserGUID === userId;
    });
  };

  return (
    <div
      className="yp-learner-dashboard-leaderboard-wrapper"
      style={{ backgroundImage: `url(${learnerLeaderboardBg})` }}
    >
      {/* <img src={learnerLeaderboardBg} alt="learner-leaderboard-bg" className="yp-learner-leaderboard-bg" /> */}
      <div className="yp-page-title-button-section">
        <div className="yp-page-title-breadcrumb">
          <div className="yp-page-title">
            Leaderboard
            {courseLeaders.length > 0 && (
              <span className="yp-learner-myrank-badge">
                My Rank: <strong>{filterMyRank()?.[0]?.rank}</strong>
              </span>
            )}
          </div>
        </div>
        {selectedCourse !== null && (
          <div className="yp-page-button">
            <Link
              to={`${
                LearnerPageRoutes.LEADER_BOARD.FULL_PATH
              }?activityId=${encodeURIComponent(
                JSON.stringify(selectedCourse)
              )}`}
              className="btn btn-sm btn-primary"
            >
              {" "}
              View
            </Link>
          </div>
        )}
      </div>

      <div className="yp-leaderboard-search-box">
        <div className="yp-form-control-with-icon yp-form-control-with-icon-right-side">
          <div className="form-group mb-0">
              <Autocomplete
                items={courseList}
                handleSelectChange={handleSelectChange}
                placeholder="Type Course Name..."
                title="Type Course Name..." 
              />
              {/* <LeaderBoardAutocomplete
                options={courseList}
                handleSelectChange={handleSelectChange}
                placeholder="Type Course Name..."
              /> */}
          </div>
        </div>
      </div>

      <div className="yp-leaderboard-result-section">
        {selectedCourse === null && courseLeaders.length === 0 && (
          <p className="">
            "Please select a course
            <br /> to view the top 3 ranked users.”
          </p>
        )}

        {selectedCourse != null && courseLeaders.length > 0 && (
          <div className="yp-leaderboard-winners-box">
            {/* {getRankHolders(courseLeaders, "2nd").length > 0 && ( */}
            <div
              className={
                getRankHolders(courseLeaders, "2nd").length > 0
                  ? "yp-leaderboard-winner"
                  : "yp-leaderboard-winner yp-leaderboard-winner-disabled"
              }
              title={
                getRankHolders(courseLeaders, "2nd").length > 0
                  ? getRankHolders(courseLeaders, "2nd")?.[0]?.fullName
                  : "No data available"
              }
            >
              <img src={rankSilver} alt="2nd rank" />
              <div
                title={
                  getRankHolders(courseLeaders, "2nd").length > 0
                    ? getRankHolders(courseLeaders, "2nd")?.[0]?.fullName
                    : ""
                }
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
                  ? getRankHolders(courseLeaders, "1st")?.[0]?.fullName
                  : "No data available"
              }
            >
              <img src={rankGold} alt="1st rank" />
              <div
                title={
                  getRankHolders(courseLeaders, "1st").length > 0
                    ? getRankHolders(courseLeaders, "1st")?.[0]?.fullName
                    : ""
                }
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
                  ? getRankHolders(courseLeaders, "3rd")?.[0]?.fullName
                  : "No data available"
              }
            >
              <img src={rankBronze} alt="3rd rank" />
              <div
                title={
                  getRankHolders(courseLeaders, "3rd").length > 0
                    ? getRankHolders(courseLeaders, "3rd")?.[0]?.fullName
                    : ""
                }
              >
                {getRankHolders(courseLeaders, "3rd").length > 0
                  ? getRankHolders(courseLeaders, "3rd")?.[0]?.fullName
                  : "NA"}
              </div>
            </div>
            {/* )} */}
          </div>
        )}
      </div>
    </div>
  );
};
export default LearnerLeaderBoard;