import { Collapse } from "bootstrap";
import { useRef, useEffect } from "react";
import { CertificateIcon, DueClockIcon } from "../../../assets/icons";
import { formatDate } from "../../../utils/commonUtils";
import CircularProgress from "../CircularProgress/CircularProgress";
import { AssignmentCardProps } from "./AssignmentCard.type";

const AssignmentCard = ({
  data,
  id,
  handleAssignments,
}: {
  data: AssignmentCardProps;
  id: number;
  handleAssignments?: () => void;
}) => {
  const {
    imageSrc,
    title,
    courseType,
    assignedDate,
    expiryDate = "",
    expiringSoon,
    expiryAlertDate,
    completionDate = "",
    dueDate = "",
    isDueDate = false,
    description = "",
    progressBar,
    activityStatus = "",
    showProgress = false,
    viewResult = false,
  } = data;

  /* To auto close description overlay when clicked outside */
  const overlayRef = useRef<HTMLDivElement>(null);
  let collapseInstance: Collapse | null = null;

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (
        overlayRef.current &&
        !overlayRef.current.contains(event.target as Node)
      ) {
        // Only collapse if it’s visible
        if (overlayRef.current.classList.contains("show")) {
          collapseInstance?.hide();
        }
      }
    };

    // Initialize collapse manually
    if (overlayRef.current) {
      collapseInstance = new Collapse(overlayRef.current, {
        toggle: false,
      });
    }

    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
      collapseInstance?.dispose();
    };
  }, []);
  /* END To auto close description overlay when clicked outside */

  return (
    <div className="col-lg-4 col-md-6 col-sm-6">
      <div className="yp-learner-assgn-box">
        <div className="yp-learner-assgn-box-img-wrapper">
          <img src={imageSrc} className="object-fit-cover" alt="" />
          {/* add class active on this div if user adds as favorite */}
          {/* <div className="yp-card-fav-btn-wrapper"> 
            <a href="#" className="yp-link">
              <i className="fa-regular fa-heart"></i>
            </a>
            
           if user adds as favorite 
            <a href="#" className="yp-link">
              <i className="fa fa-heart"></i>
            </a>
          </div>  */}
        </div>
        <div className="yp-learner-assgn-box-content-wrapper">
          <div
            className="yp-learner-assgn-box-title"
            style={{cursor:'pointer'}}
            title={title}
            onClick={() => {
                handleAssignments?.();
            }}
          >
            {title}
          </div>
          {description && (
            <>
              <div className="yp-learner-assgn-box-desc">
                <span>{description}</span>
                <a
                  href="#"
                  className="yp-link ms-2"
                  data-bs-toggle="collapse"
                  data-bs-target={`#${id}`}
                  aria-controls={`${id}`}
                  aria-expanded="false"
                >
                  more
                </a>
              </div>
              <div
                className="yp-learner-assgn-box-desc-overlay collapse"
                id={`${id}`}
                ref={overlayRef}
              >
                <div className="yp-label-value-wrapper">
                  <div className="yp-label-value-row">
                    <div className="yp-learner-assgn-box-title">{title}</div>
                    <div className="yp-lv-value">{description}</div>
                    <div>
                      <a
                        href="#"
                        className="yp-link"
                        data-bs-toggle="collapse"
                        data-bs-target={`#${id}`}
                        aria-controls={`${id}`}
                        aria-expanded="true"
                      >
                        Less
                      </a>
                    </div>
                  </div>
                </div>
              </div>
            </>
          )}
          <div className="yp-learner-assgn-box-info-progress-wrapper">
            <div className="yp-learner-assgn-box-inside-info-wrapper">
              <div className="yp-label-value-wrapper yp-label-value-inline">
                <div className="yp-label-value-row">
                  <div className="yp-lv-label">Type:</div>
                  <div className="yp-lv-value">{courseType}</div>
                </div>
                {assignedDate && (
                  <div className="yp-label-value-row">
                    <div className="yp-lv-label">Assigned Date:</div>
                    <div className="yp-lv-value">
                      {formatDate(assignedDate)}
                    </div>
                  </div>
                )}
                {/* {expiryAlertDate && (
                  <div className="yp-label-value-row">
                    <div className="yp-lv-label">Expiry Alert Date:</div>
                    <div className="yp-lv-value">
                      {expiringSoon ? (
                        <span className="yp-color-danger">
                          {formatDate(expiryAlertDate)}
                          <i className="zmdi zmdi-time-interval"></i>
                        </span>
                      ) : (
                        <div className="yp-lv-value">
                          {" "}
                          {formatDate(expiryAlertDate)}
                        </div>
                      )}
                    </div>
                  </div>
                )} */}
                {dueDate && (
                  <div className="yp-label-value-row">
                    <div className="yp-lv-label">Due Date:</div>
                    <div
                      className={`yp-lv-value`}
                    >
                      <span className={`${
                        isDueDate
                          ? "yp-color-warning" //CHANGE COLOR HERE
                          : expiringSoon
                          ? "yp-color-danger"
                          : ""
                      }`}>{formatDate(dueDate)}</span>
                    </div>
                  </div>
                )}
                {isDueDate && ( //ADD ICON FOR CLOCK
                  <div className="yp-label-value-row mt-0">
                    <div className="yp-lv-value">
                      <span className="yp-color-warning">
                        <DueClockIcon />
                        <span className="ms-1">Nearing Due Date</span>
                      </span>
                    </div>
                  </div>
                )}
                {expiryDate && (
                  <div className="yp-label-value-row">
                    <div className="yp-lv-label">Expiry Date:</div>
                    <div className="yp-lv-value">{formatDate(expiryDate)}</div>
                  </div>
                )}
                {expiringSoon && ( //ADD ICON FOR DANGER
                  <div className="yp-label-value-row">
                    <div className="yp-lv-value">
                      <span className="yp-color-danger">
                        <i className="fa fa-warning yp-text-14"></i>
                        <span className="ms-1">Activity Expiring Soon</span>
                      </span>
                    </div>
                  </div>
                )}
                {completionDate && (
                  <div className="yp-label-value-row">
                    <div className="yp-lv-label">Completion Date:</div>
                    <div className="yp-lv-value">
                      <div className="yp-lv-value">
                        {" "}
                        {formatDate(completionDate)}
                      </div>
                    </div>
                  </div>
                )}
                {/* {(expiringSoon || isDueDate) && (
                  <div className="yp-label-value-row">
                    <div className="yp-lv-label">
                      <span className="yp-color-danger">
                        {isDueDate
                          ? "Nearing Due Date"
                          : "Activity Expiring Soon"}
                      </span>
                    </div>
                  </div>
                )} */}
              </div>
            </div>

            {showProgress && (
              <div className="yp-learner-assgn-box-inside-progress-wrapper">
                <CircularProgress progress={progressBar ?? 0} />
                <div className="yp-circle-progress-status">
                  {activityStatus}
                </div>
              </div>
            )}
          </div>
          {viewResult && (
            <div className="yp-learner-assgn-box-button-row">
              <a href="#" className="btn btn-primary">
                View Result
              </a>
              <a href="#" className="yp-link">
                <CertificateIcon />
              </a>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};
export default AssignmentCard;
