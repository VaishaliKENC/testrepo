import React, { ChangeEvent, useEffect, useMemo, useState } from "react";
import { RootState } from "../../redux/store";
import { useAppDispatch, useAppSelector } from "../../hooks";
import CardFallback from "../../assets/images/card_fallback.png";
import { handleDebounce, validateInput } from "../../utils/commonUtils";
import { AssignmentCard } from "../../components/shared";
import { getCompletedAssignment } from "../../redux/Slice/learner/completedAssignment/completedAssignment.requests";
import CustomPagination from "../../components/shared/CommonComponents/CustomPagination";
import CustomLoader from "../../components/shared/CommonComponents/CustomLoader";
import { LearnerPageRoutes } from "../../utils/Constants/Learner_PageRoutes";
import CustomBreadcrumb from "../../components/shared/CustomBreadcrumb";
import { getDashboardData } from "../../redux/Slice/learner/dashboard/dashboard.requests";

const CompletedAssignmentPage: React.FC = () => {
  document.title = 'Completed Assignment';
  const clientId: any = useAppSelector(
    (state: RootState) => state.auth.clientId
  );
  const userId: any = useAppSelector((state: RootState) => state.auth.id);
  const dispatch = useAppDispatch();

  const { completedAssignment: assignData, loading: assignLoader } =
    useAppSelector((state: RootState) => state.completedAssignment);

  const pageSize = 9;
  const [currentPage, setCurrentPage] = useState(1);
  const [searchTerm, setSearchTerm] = useState<string>("");

  const totalRecords = useAppSelector(
    (state: RootState) => state.learnerDashboard.completedCourses
  );
  // const totalRecords = useAppSelector(
  //   (state: RootState) => state.learnerDashboard.completedCourses
  // );
  const breadcrumbItems = [
    {
      iconClass: "fa-classic fa-solid fa-house",
      path: LearnerPageRoutes.LEARNER_DASHBOARD.FULL_PATH,
    },
    { label: "Completed Assignments" },
  ];

  useEffect(() => {
    dispatch(getDashboardData({ clientId: clientId, userId: userId })); //get total completed courses number for pagination
  }, []);

  useEffect(() => {
    dispatch(
      getCompletedAssignment({
        pageIndex: currentPage,
        pageSize,
        sortExpression: null,
        clientId: clientId,
        userId: userId,
      })
    );
  }, [dispatch, clientId, userId, currentPage]);

  const cardData = assignData?.map((card: Record<string, any>) => {
    return {
      id: card.ID,
      imageSrc: card?.ThumbnailImgRelativePath
        ? process.env.REACT_APP_CONTENT_SERVER_URL +
        card.ThumbnailImgRelativePath
        : CardFallback,
      title: card?.ActivityName,
      courseType: card.ActivityType,
      assignedDate: validateInput(card.AssignmentDateSet),
      showProgress: true,
      progressBar: Math.round(card?.Progress ?? 0),
      description: card?.ActivityDescription,
      activityStatus: card?.ActivityStatus,
      completionDate: validateInput(card.DateOfCompletion),
      viewResult: false,                     // card?.IsPrintCertificate,
    };
  });

  const onPageChange = (page: number, pagesize: number) => {
    setCurrentPage(page);
  };

  const handlePageClick = (page: number) => {
    if (page !== currentPage) {
      onPageChange(page, pageSize);
    }
  };
  const handleSearch = (event: ChangeEvent<HTMLInputElement>) => {
    setSearchTerm(event?.target?.value);
    debouncedSearch(event?.target?.value);
  };
  const searchAssignment = (value: string) => {
    dispatch(
      getCompletedAssignment({
        pageIndex: currentPage,
        pageSize,
        sortExpression: null,
        clientId: clientId,
        userId: userId,
        keyWord: value,
      })
    );
  };

  const debouncedSearch = useMemo(
    () => handleDebounce(searchAssignment, 1000),
    []
  );
  return (
    <>
      {assignLoader && <CustomLoader />}
      <div
        className="yp-page-title-button-section"
        id="yp-page-title-breadcrumb-section"
      >
        <div className="yp-page-title-breadcrumb">
          <div className="yp-page-title">
            Completed Assignment ({cardData.length ?? 0})
          </div>
          <CustomBreadcrumb items={breadcrumbItems} />
        </div>
        <div className="yp-page-button">
          <div className="yp-width-335-px">
            <div className="yp-form-control-with-icon">
              <div className="form-group mb-0">
                <div className="yp-form-control-wrapper">
                  <input
                    type="text"
                    name="tableSearchInput"
                    //placeholder="Assignment Name..."
                    placeholder=""
                    className="form-control yp-form-control"
                    value={searchTerm}
                    onChange={(event) => handleSearch(event)}
                  />
                  <label className="form-label">Assignment Name</label>
                  <span className="yp-form-control-icon">
                    <i className="fa fa-search"></i>
                  </span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div className="yp-card" id="yp-card-main-content-section">
        <div className="yp-learner-assgn-boxes">
          <div className="row row-gap-4">
            {/* {cardData.map((card, index) => (
              <AssignmentCard key={card.title} data={card} id={index} />
            ))} */}

            {cardData && cardData.length > 0 ? (
              cardData.map((card, index) => (
                <AssignmentCard key={card.title || index} data={card} id={index} />
              ))
            ) : (
              <div className="col-12 text-center py-5">
                <p className="mb-0">No assignments found.</p>
              </div>
            )}

          </div>
        </div>

        {cardData && cardData.length > 0 && (
          <CustomPagination
            totalRecords={totalRecords}
            currentPage={currentPage}
            onPageChange={onPageChange}
            pageSize={pageSize}
            handlePageClick={handlePageClick}
          />
        )}
      </div>
    </>
  );
};

export default CompletedAssignmentPage;
