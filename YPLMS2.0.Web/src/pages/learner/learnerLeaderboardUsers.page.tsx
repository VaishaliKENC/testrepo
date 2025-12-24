import React, { useEffect, useState } from "react";
import {
  getLearnerLeaderBoardRankList,
} from "../../redux/Slice/leaderBoard/leaderBoard.requests";
import { useAppDispatch, useAppSelector } from "../../hooks";
import { RootState } from "../../redux/store";
import { useLocation } from "react-router-dom";
import { mapLeaderBoardCourseList } from "../../utils/commonHelpers";
import Autocomplete from "../../components/shared/CommonComponents/Autocomplete";

const LearnerLeaderboardUsersPage: React.FC = () => {
  const dispatch = useAppDispatch();
  const searchParams = new URLSearchParams(useLocation().search);
  const rankList: any = useAppSelector(
    (state: RootState) => state.leaderBoard.learnerCourseLeaders
  );
  const decodedString = decodeURIComponent(
    searchParams?.get?.("activityId") ?? ""
  );
  const receivedObject = JSON.parse(decodedString);
  const courseList = useAppSelector((state: RootState) =>
    mapLeaderBoardCourseList(state?.leaderBoard?.courseList)
  );
  const userId: any = useAppSelector((state: RootState) => state.auth.id);
  const clientId: any = useAppSelector(
    (state: RootState) => state.auth.clientId
  );
  const [selectedCourse, setSelectedCourse] = useState(receivedObject);
  const [courseLeader, setCourseLeader] = useState(rankList || []);

  useEffect(() => {
    if (selectedCourse !== null) {
      setCourseLeader(rankList);
    }
  }, [rankList]);

  useEffect(() => {
    callApi(selectedCourse?.value);
  }, []);

  const handleSelectChange = (selectedOption: any) => {
    if (selectedOption && selectedOption !== null) {
      setSelectedCourse(selectedOption);
      callApi(selectedOption?.value);
    } else {
      setCourseLeader([]);
      setSelectedCourse(selectedOption);
    }
  };
  const callApi = (courseId: string) => {
    dispatch(
      getLearnerLeaderBoardRankList({
        clientId,
        systemId: userId,
        activityId: courseId,
      })
    );
  };
  return (
    <>
      <div
        className="yp-page-title-button-section"
        id="yp-page-title-breadcrumb-section"
      >
        <div className="yp-page-title-breadcrumb">
          <div className="yp-page-title">Leaderboard</div>
        </div>
        <div className="yp-page-button">
          <div className="yp-width-450-px">
            <div className="yp-form-control-with-icon yp-form-control-with-icon-right-sides">
              <div className="form-group mb-0">
                   <Autocomplete
                      items={courseList}
                      handleSelectChange={handleSelectChange}
                      placeholder="Enter Course Name..."
                      title="Enter Course Name..." 
                      defaultValue={receivedObject}
                    />
                  {/* <LeaderBoardAutocomplete
                    options={courseList}
                    placeholder="Enter Course Name..."
                    handleSelectChange={handleSelectChange}
                    defaultValue={receivedObject}
                  /> */}

              </div>
            </div>
          </div>
        </div>
      </div>

      <div className="yp-card" id="yp-card-main-content-section">
        <div className="yp-card-box-shadow yp-card-rounded">
          <div className="yp-table-learner-leaderboard">
            <div className="table-responsive">
              <table className="table yp-custom-table" border={0}>
                <thead>
                  <tr>
                    <th>Rank</th>
                    <th>Full Name</th>
                    <th>Score</th>
                  </tr>
                </thead>
                <tbody>
                  {courseLeader?.map((rank: any, index: number) => (
                    <tr
                      className={
                        rank.systemUserGUID === userId
                          ? "yp-leaderboard-trow-highlighted"
                          : ""
                      }
                      key={rank.fullName}
                    >
                      <td>{rank.rank}</td>
                      <td>
                        {rank.fullName}{" "}
                        { rank.systemUserGUID === userId ? "(You)" : ""}
                      </td>
                      <td>{parseInt(rank?.score)}</td>
                    </tr>
                  ))}
                 {courseLeader.length===0 &&  <tr>
                   <td colSpan={3}>No data found</td>
                  </tr>
                  } 
                  {/* <tr>
                    <td>1st</td>
                    <td>Sophia Lee</td>
                    <td>98</td>
                  </tr>
                  <tr>
                    <td>2nd</td>
                    <td>John Smith</td>
                    <td>90</td>
                  </tr>
                  <tr>
                    <td>3rd</td>
                    <td>Michael Wilson</td>
                    <td>88</td>
                  </tr>
                  <tr>
                    <td>4th</td>
                    <td>Mia Wang</td>
                    <td>85</td>
                  </tr>
                  <tr>
                    <td>5th</td>
                    <td>Noah Johnson</td>
                    <td>84</td>
                  </tr>
                  <tr>
                    <td>6th</td>
                    <td>Jack Wilshere</td>
                    <td>80</td>
                  </tr>
                  <tr>
                    <td>7th</td>
                    <td>Marcus Rashford</td>
                    <td>78</td>
                  </tr>
                  <tr>
                    <td>8th</td>
                    <td>Dan James</td>
                    <td>75</td>
                  </tr>
                  <tr>
                    <td>9th</td>
                    <td>Bryan Mbeumo</td>
                    <td>70</td>
                  </tr> */}
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default LearnerLeaderboardUsersPage;