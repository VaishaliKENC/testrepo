import React, { useEffect, useRef, useState } from "react";
import { CommonTableProps } from "../../../Types/commonTableTypes";
import { Link } from "react-router-dom";
import {
  ACTIVITY_TYPES,
  COMMON_TABLE_TYPE,
} from "../../../utils/Constants/Enums";
import { AdminPageRoutes } from "../../../utils/Constants/Admin_PageRoutes";
import { ACTIVITY_CONTENT_TYPE_ENUM } from "../../../utils/Constants/Enums";
import { getAssetType } from "../../../utils/commonHelpers";
import { setAssetTracking } from "../../../redux/Slice/tracking/assetTracking.requests";
import { useAppDispatch, useAppSelector } from "../../../hooks";
import { RootState } from "../../../redux/store";
import { handleAssetPreviewTracking } from "../../../utils/commonUtils";
import Autocomplete from "./Autocomplete";
import Collapse from "bootstrap/js/dist/collapse";

const CommonTable = <
  T extends {
    id: string;
    assetFolderId?: string;
    contentServerURL?: string;
    relativePath?: string;
    assetName?: string;
    isDownload?: boolean;
    assetFileType?: string;
    userStatus?: string;
  }
>(
  props: CommonTableProps<T>
) => {
  const {
    type,
    filterComponent,
    onFilterSubmit,
    data,
    onSearch,
    onPageChange,
    currentPage,
    totalRecords,
    pageSize,
    selectedUsers,
    handleCheckboxChange,
    handleSelectAll,
    actionButtonList,
    actionButtonClick,
    onCourseLaunch,
    isFilterVisible,
    setFilterVisible,
    searchMode,
    setSearchMode,
    actionDeleteClick,
    actionCopyClick,
    isHidePagination = false,
    isHideHeader = false,
    tableTitle,
    handleSelectChange,
    defaultAutcompleteValue,
    autocompleteData,
    tableConfig,
    getSortedList,
    onClearFilter,
    handleOpenDetailsPopup,
  } = props;
  // const [searchInput, setSearchInput] = useState<string>('');
  const [searchInput, setSearchInput] = useState<string>(
    props.searchValue || ""
  );
  const [currentSort, setCurrentSort] = useState<{
    key: string;
    sort: string;
  } | null>(null);

  const [formErrors, setFormErrors] = useState<Record<string, string>>({});
  const searchInputRef = useRef<HTMLInputElement | null>(null);
  const dispatch = useAppDispatch();
  const clientId: any = useAppSelector(
    (state: RootState) => state.auth.clientId
  );
  const userId: any = useAppSelector((state: RootState) => state.auth.id);
  const contentServerURL: any = useAppSelector(
    (state: RootState) => state.auth.serverUrl
  );
  // Function to toggle the visibility
  const toggleFilterVisibility = () => {
    setFilterVisible((prevState: boolean) => !prevState);
  };

  // Handle "select all" checkbox state
  const selectedUsersSafe = selectedUsers ?? [];
  const isAllSelected =
    data.length > 0 &&
    data.every((item: any) => selectedUsersSafe.includes(item.id));
  const isIndeterminate =
    data.length > 0 &&
    data.some((item: any) => selectedUsersSafe.includes(item.id)) &&
    !isAllSelected;

  // Handle page size change
  const handlePageSizeChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const newPageSize = parseInt(e.target.value, 10);

    if (currentSort !== null) {
      onPageChange(1, newPageSize, currentSort?.key + " " + currentSort?.sort);
    } else onPageChange(1, newPageSize);
  };

  //oneTime text will clear clicking next
  useEffect(() => {
    if (props.searchValue !== undefined) {
      setSearchInput(props.searchValue);
    }
  }, [props.searchValue]);

  // Pagination logic
  const totalPages = Math.ceil(totalRecords / pageSize);
  const pageNumbers = [];
  const pagesToShow = 5;

  for (let i = 1; i <= totalPages; i++) {
    if (
      i === 1 ||
      i === totalPages ||
      (i >= currentPage - 2 && i <= currentPage + 2)
    ) {
      pageNumbers.push(i);
    }
  }

  const handlePageClick = (page: number) => {
    if (page !== currentPage) {
      if (currentSort !== null) {
        onPageChange(
          page,
          pageSize,
          currentSort?.key + " " + currentSort?.sort
        );
      } else onPageChange(page, pageSize);
    }
  };

  const isAssetLibraryTable = () =>
    type === COMMON_TABLE_TYPE.MANAGE_ASSET_LIBRARY;
  const isActivityTable = () =>
    type === COMMON_TABLE_TYPE.REPORT_LEARNER_ACTIVITY_PROGRESS;
  const isUserReportTable = () =>
    type === COMMON_TABLE_TYPE.REPORT_LEARNER_PROGRESS;
  const isActivityTypeDisplayTable = () =>
    type === COMMON_TABLE_TYPE.MANAGE_ONE_TIME_ASSIGNMENT_ACTIVITY ||
    type === COMMON_TABLE_TYPE.MANAGE_ONE_TIME_ASSIGNMENT_PREVIEW_CONTENT_SELECTED_ACTIVITY ||
    type === COMMON_TABLE_TYPE.REPORT_LEARNER_ACTIVITY_PROGRESS ||
    type === COMMON_TABLE_TYPE.REPORT_LEARNER_PROGRESS;

  const isOneTimeAssignmentSelectedUserTable = () =>
    type === COMMON_TABLE_TYPE.MANAGE_ONE_TIME_ASSIGNMENT_USER_SELECTED;
  const isCourseTreeFileListTable = () =>
    type === COMMON_TABLE_TYPE.MANAGE_TREE_COURSES_FILES;

  const isAdminLeaderboardUsers = () =>
    type === COMMON_TABLE_TYPE.MANAGE_ADMIN_LEADERBOARD_USERS;

  const isPendingApprovalsTable = () =>
    type === COMMON_TABLE_TYPE.PENDING_APPROVALS;

  const renderCell = (item: any, column: any) => {
    if (column.isBadge) {
      return (
        <span
          className={`badge ${item.isActive ? "badge-success" : "badge-danger"
            }`}
        >
          {item.isActive ? "Active" : "Inactive"}
        </span>
      );
    }

    if (column.isUsed) {
      return (
        <span
          className={`badge ${item.isUsed ? "badge-success" : "badge-danger"}`}
        >
          {item.isUsed ? "Yes" : "No"}
        </span>
      );
    }

    if (column.isApprovedPendingReject) {
      return (
        <span
          className={`badge ${item.userStatus === "Approved" ? "badge-success" : item.userStatus === "Pending" ? "badge-warning" : "badge-danger"}`}
        >
          {item.userStatus}
        </span>
      );
    }

    // if ( (column.key === "dateCreated" ||  column.key === "assignmentDate" || column.key === "startDate" || column.key === "registrationDate") &&  item[column.key] ) {
    //   const date = new Date(item[column.key]);
    //   const formattedDate = `${(date.getMonth() + 1)
    //     .toString()
    //     .padStart(2, "0")}/${date
    //       .getDate()
    //       .toString()
    //       .padStart(2, "0")}/${date.getFullYear()}`;
    //   return formattedDate;
    // }

    if ((column.key === "dateCreated" || column.key === "registrationDate") && item[column.key]) {
      const date = new Date(item[column.key]);
      const formattedDate = `${(date.getMonth() + 1)
        .toString()
        .padStart(2, "0")}/${date
          .getDate()
          .toString()
          .padStart(2, "0")}/${date.getFullYear()}`;
      return formattedDate;
    }


    if ((column.key === "assignmentDate" || column.key === "startDate") && item[column.key]) {
      const date = new Date(item[column.key]);
      const formattedDate = `${(date.getDate()).toString().padStart(2, "0")}/${(date.getMonth() + 1).toString().padStart(2, "0")}/${date.getFullYear()}`;
      return formattedDate;
    }




    if (column.key === "size" && item[column.key]) {
      return `${item[column.key]}KB`;
    }

    if (isActivityTypeDisplayTable() && column.key === "activityType") {
      const key = getKeyFromEnumValue(ACTIVITY_CONTENT_TYPE_ENUM, Number(item[column.key]));
      return key || item[column.key] || "-";
    }
    return item[column.key] || "-";
  };

  // Validation

  const handleOnSearch = (val: string) => {
    setSearchInput(val);
    if (val.trim() && !isValidInput(val)) {
      setFormErrors({
        ...formErrors,
        tableSearchInput: "Special characters like < > ' & are not allowed.",
      });
      return;
    }
    if (currentSort !== null)
      onSearch?.(val, currentSort?.key + " " + currentSort?.sort);
    else onSearch?.(val);
    setFormErrors({});
  };
  const isValidInput = (value: string): boolean => {
    const regx = /^[^'<>&]+$/; // Used in YP
    return regx.test(value);
  };

  function getKeyFromEnumValue<T extends Record<string, any>>(
    enumObj: T,
    value: T[keyof T]
  ): string | undefined {
    if (value == null) return undefined;
    for (const key in enumObj) {
      if (Object.prototype.hasOwnProperty.call(enumObj, key)) {
        if (enumObj[key] == value) {
          return key;
        }
      }
    }
    return undefined;
  }
  const handleLinkClick = (
    event: React.MouseEvent<HTMLAnchorElement, MouseEvent>,
    item: Record<string, any>,
    assetType: any
  ) => {
    event.preventDefault();
    const isMp4 = item?.assetFileName?.split(".")?.pop();

    const payload = {
      clientId,
      currentUserID: userId,
      activityId: item.id,
      activityType: ACTIVITY_TYPES.ASSET,
      isForAdminPreview: true,
      progress: 100,
    };

    dispatch(setAssetTracking(payload)).then((trackingData) => {
      const watchedInMins =
        trackingData?.payload?.asset?.trackingData?.watchedInMins ?? 0;
      const previewPayload = {
        contentServerURL,
        isDownload: item.isDownload,
        assetName: item.assetName,
        assetFileName: item.assetFileName,
        assetFileType: assetType,
        relativePath: item.relativePath,
        isMp4: isMp4 === "mp4" ? "true" : "false",
        id: item.id,
        clientId,
        userId,
        watchedInMins,
      };
      handleAssetPreviewTracking(previewPayload);
    });
  };

  const assetsViewer = (item: any) => {
    return (
      <Link
        to="#"
        onClick={(event) =>
          handleLinkClick(
            event,
            item,
            getAssetType(item?.assetFileType ?? " ") //receiving different format from BE
          )
        }
        className="yp-link-icon yp-link-icon-primary"
        target="_blank"
      // rel="noopener noreferrer"   //PREVENT TAB EVENT LISTENERES LIKE BEFOREUNLOAD TRY NOT TO USE
      >
        <i className="fa fa-eye"></i>
      </Link>
    );
  };
  const handleAutocomple = (option: any) => {
    if (handleSelectChange) {
      handleSelectChange(option);
    }
  };

  // Autoclose filter dropdown when click outside or body
  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      const collapseElement = document.getElementById("collapseExample");
      const toggleButton = document.querySelector(
        ".yp-advanced-search-link a.yp-link"
      ) as HTMLElement | null;

      const target = event.target as HTMLElement;

      if (!collapseElement || !toggleButton) return;

      if (collapseElement.classList.contains("show")) {
        if (
          !collapseElement.contains(event.target as Node) &&
          !toggleButton.contains(event.target as Node) &&
          !target.closest(".react-datepicker-popper")
        ) {
          const bsCollapse = new Collapse(collapseElement, { toggle: true });
          bsCollapse.hide();

          setSearchMode?.(null);
          setFilterVisible(false);
        }
      }
    };

    document.addEventListener("click", handleClickOutside);
    return () => document.removeEventListener("click", handleClickOutside);
  }, []);

  const handleSorting = (key: string, order: string) => {
    setCurrentSort({ key: key, sort: order });
    getSortedList?.(key + " " + order);
  };
  const handleOnFilterClear = () => {
    if (currentSort !== null) {
      onClearFilter?.(currentSort?.key + " " + currentSort?.sort);
    }
  };
  return (
    <>
      {!isHideHeader ? (
        <div
          className="yp-table-top-search-section"
          id={`yp-table-top-search-section-${type}`}
        >
          <div className="yp-ttss-right-section">
            <div className="yp-ttssrs-search-section">
              {isAssetLibraryTable() && (
                <div className="d-inline-flex">
                  <div className="yp-table-title yp-mt-10">Asset List</div>
                </div>
              )}
              {isActivityTable() && (
                <div className="d-inline-flex">
                  <div className="yp-table-title yp-mt-10">Activity List</div>
                </div>
              )}
              {isUserReportTable() && (
                <div className="d-inline-flex">
                  <div className="yp-table-title yp-mt-10">Learner Progress Report - User List</div>
                </div>
              )}
              {isOneTimeAssignmentSelectedUserTable() && (
                <div className="d-inline-flex">
                  <div className="yp-fs-16 yp-text-dark-purple yp-mt-10">
                    User List
                  </div>
                </div>
              )}
              {isCourseTreeFileListTable() && (
                <div className="d-inline-flex">
                  <div className="yp-table-title yp-mt-10">
                    File List for "{tableTitle}"
                  </div>
                </div>
              )}

              {
                //!isAssetLibraryTable() && (
                tableConfig.showSearchSectionForTheseTables && (
                  <>
                    {isAdminLeaderboardUsers() ? (
                      <div className="yp-form-control-with-icon yp-form-control-with-icon-right-side">
                        <div className="form-group">
                          <Autocomplete
                            items={autocompleteData || []}
                            handleSelectChange={handleAutocomple}
                            placeholder="Type Course Name..."
                            title="Type Course Name..."
                            defaultValue={defaultAutcompleteValue}
                          />
                        </div>
                      </div>
                    ) : (
                      // <LeaderBoardAutocomplete
                      //   options={(autocompleteData || [])}
                      //   handleSelectChange={handleAutocomple}
                      //   placeholder="Type Course Name..."
                      //   defaultValue={defaultAutcompleteValue}
                      // />

                      <div className="yp-form-control-with-icon-wrapper">
                        <div className="yp-form-control-with-icon">
                          <div className="form-group">
                            <div className="yp-form-control-wrapper">
                              {/* <input type="text" name="tableSearchInput"
                        placeholder={searchPlaceholder}
                        value={searchInput} 
                        className="form-control yp-form-control yp-form-control-sm"
                        onChange={(e) => {
                          setSearchInput(e.target.value);  // Update the input value
                          handleOnSearch(e.target.value);  // Validate + Search trigger
                        }}
                        onKeyDown={(e) => { if (e.key === ' ') e.preventDefault(); }}
                        disabled={type === COMMON_TABLE_TYPE.MANAGE_USER && searchMode === 'filter'} //  Only disable for Manage Users
                      /> */}

                              <input
                                type="text"
                                name="tableSearchInput"
                                //placeholder={tableConfig.searchPlaceholder}
                                placeholder=""
                                title={
                                  `Search: ` + tableConfig.searchPlaceholder
                                }
                                value={searchInput}
                                className="form-control yp-form-control"
                                onChange={(e) => {
                                  let value = e.target.value;
                                  // Disallow leading space
                                  if (value.length === 1 && value === " ") {
                                    return;
                                  }
                                  setSearchInput(value);
                                  // Only trigger search if the trimmed value has content
                                  // if (value.trim() !== '') {
                                  //   handleOnSearch(value);
                                  // }
                                  handleOnSearch(value.trim());
                                }}
                                onKeyDown={(e) => {
                                  // Prevent leading space
                                  if (
                                    e.key === " " &&
                                    searchInput.length === 0
                                  ) {
                                    e.preventDefault();
                                  }
                                }}
                                disabled={searchMode === "filter"} // Only disable for Manage Users
                              />
                              <label className="form-label">
                                {tableConfig.searchPlaceholder}
                              </label>

                              <span className="yp-form-control-icon">
                                <i className="fa fa-search"></i>
                              </span>
                            </div>
                            <div className="d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2">
                              {formErrors.tableSearchInput && (
                                <div className="invalid-feedback px-3">
                                  {formErrors.tableSearchInput}
                                </div>
                              )}
                            </div>
                          </div>
                        </div>

                        {tableConfig.showFilterSectionForTheseTables && ( // Show filter section only for "Manage User"
                          <div className="d-inline-flex yp-advanced-search-link">
                            {/* <a className="yp-link yp-link-primary" onClick={toggleFilterVisibility} data-bs-toggle="collapse" href="#collapseExample" role="button" aria-expanded={isFilterVisible} aria-controls="collapseExample" data-bs-auto-close="outside">Search Filters</a> */}
                            <a
                              className={`yp-link yp-link-primary ${searchMode === "keyword" ? "disabled" : ""
                                }`} // ✅ Disable if keyword search active
                              onClick={(e) => {
                                e.preventDefault();
                                if (searchMode !== "keyword") {
                                  setFilterVisible((prev: boolean) => {
                                    const next = !prev;
                                    if (next) {
                                      setSearchMode?.("filter"); // ✅ Works now
                                      // 👉 Clear the search input using the ref
                                      setSearchInput(""); // This should reset the search input value
                                      setFormErrors({}); // Clear form errors
                                    } else {
                                      setSearchMode?.(null);
                                    }
                                    return next;
                                  });
                                }
                              }}
                              href="#collapseExample"
                              role="button"
                              aria-expanded={isFilterVisible}
                              aria-disabled={searchMode === "keyword"}
                            >
                              Search Filters
                            </a>
                          </div>
                        )}
                        {tableConfig.showFilterSectionForTheseTables && (
                          <div
                            className={`collapse yp-advanced-search-card ${isFilterVisible ? "show" : ""
                              }`}
                            id="collapseExample"
                          >
                            <div className="card card-body">
                              {React.cloneElement(filterComponent, {
                                onSubmit: (filterData: any) => {
                                  if (onFilterSubmit) {
                                    if (currentSort !== null) {
                                      onFilterSubmit(
                                        filterData,
                                        currentSort.key + " " + currentSort.sort
                                      );
                                    } else onFilterSubmit(filterData); // Notify parent
                                  }

                                  setFilterVisible(false); // Hide the filter dialog
                                },
                                onClear: onClearFilter,
                                closeFilter: () => setFilterVisible(false)
                              })}
                            </div>
                          </div>
                        )}
                      </div>
                    )}
                  </>
                )
              }
            </div>
            <div className="yp-ttssrs-buttons-section">
              {!isHidePagination ? (
                <>
                  <div className="yp-ttssrs-buttons">
                    {actionButtonList?.map((button: any, index) => (
                      <button
                        key={button?.actionType || index}
                        type={button?.type}
                        className={button?.className}
                        onClick={() => actionButtonClick?.(button?.actionType)}
                        disabled={button.disabled}
                      >
                        {button?.buttonName}
                      </button>
                    ))}
                  </div>

                  {/* <div className="yp-ttssrs-buttons">
                    {tableConfig.showReportDownloadIconForTheseTables && (
                      <a
                        className="yp-link-icon yp-link-icon-primary"
                        // onClick={() => onReportXLS(item.id)}
                        style={{ cursor: "pointer" }}
                      >
                        <i className="fa-solid fa-file-excel fa-lg"></i>
                      </a>
                    )}
                  </div> */}
                  <div className="yp-page-show-records">
                    <div className="yp-inline-label-input">
                      <label className="form-label">Show</label>
                      <select
                        name=""
                        className="form-control yp-form-control yp-form-control-sm"
                        onChange={handlePageSizeChange}
                        value={pageSize}
                      >
                        <option value="5">5</option>
                        <option value="10">10</option>
                        <option value="15">15</option>
                      </select>
                      <label className="form-label">Records</label>
                    </div>
                  </div>
                </>
              ) : (
                <></>
              )}
            </div>
          </div>
        </div>
      ) : (
        <></>
      )}
      <div className="table-responsive" id={`yp-table-${type}`}>
        <table className="table yp-custom-table" border={0}>
          <thead>
            <tr>
              {(tableConfig.showCheckboxForTheseTables ||
                tableConfig.showRadioForTheseTables) && (
                  <th className="yp-custom-table__checkbox-column">
                    {tableConfig.showCheckboxForTheseTables ? (
                      <input
                        type="checkbox"
                        className="form-check-input"
                        checked={
                          data.length > 0 &&
                          data.every((item) =>
                            selectedUsersSafe.includes(item.id)
                          )
                        }
                        onChange={() =>
                          handleSelectAll(
                            !data.every((item) =>
                              selectedUsersSafe.includes(item.id)
                            )
                          )
                        }
                      />
                    ) : (
                      ""
                    )}
                  </th>
                )}

              {tableConfig.columns.map((col) => (
                <th key={col.key}>
                  <div className="yp-table-head-th-content">
                    <div className="yp-table-head-th-label">{col.label}</div>
                    {(tableConfig.sortConfig || []).includes(col.key) && (
                      <div className="yp-table-head-th-sorting"
                        onClick={() => handleSorting(col.key, currentSort?.sort === "asc" ? "desc" : "asc")}>
                        <svg
                          xmlns="http://www.w3.org/2000/svg"
                          width="6"
                          height="5"
                          viewBox="0 0 6 5"
                          fill="none"
                          className={`svg-sort-up ${currentSort?.key === col.key &&
                            currentSort?.sort === "asc"
                            ? ""
                            : "yp-sort-disabled"
                            }`}
                          // onClick={() => handleSorting(col.key, "asc")}
                        >
                          <path
                            d="M2.57622 0.244045C2.8105 -0.0813484 3.19096 -0.0813484 3.42524 0.244045L5.82424 3.57608C5.99667 3.81557 6.04727 4.1722 5.95356 4.48458C5.85985 4.79695 5.64244 5 5.39879 5H0.600793C0.359019 5 0.139736 4.79695 0.0460246 4.48458C-0.0476863 4.1722 0.0047918 3.81557 0.175346 3.57608L2.57435 0.244045H2.57622Z"
                            fill="#B5B5B5"
                          />
                        </svg>
                        <svg
                          xmlns="http://www.w3.org/2000/svg"
                          width="6"
                          height="5"
                          viewBox="0 0 6 5"
                          fill="none"
                          className={`svg-sort-down ${currentSort?.key === col.key &&
                            currentSort?.sort === "desc"
                            ? ""
                            : "yp-sort-disabled"
                            }`}
                          // onClick={() => handleSorting(col.key, "desc")}
                        >
                          <path
                            d="M2.57622 4.75595C2.8105 5.08135 3.19096 5.08135 3.42524 4.75595L5.82424 1.42392C5.99667 1.18443 6.04727 0.827802 5.95356 0.515424C5.85985 0.203046 5.64244 1.11054e-07 5.39879 1.11054e-07H0.600793C0.359019 1.11054e-07 0.139736 0.203046 0.0460246 0.515424C-0.0476863 0.827802 0.0047918 1.18443 0.175346 1.42392L2.57435 4.75595H2.57622Z"
                            fill="#B5B5B5"
                          />
                        </svg>
                      </div>
                    )}
                  </div>
                </th>
              ))}
              {tableConfig.showActionColumnForTheseTables && (
                <th className="text-center yp-table-action-column">Action</th>
              )}
            </tr>
          </thead>
          <tbody>
            {data.length > 0 ? (
              data.map((item) => (
                <tr key={item.id}>
                  {tableConfig.showCheckboxForTheseTables && (
                    <td className="yp-custom-table__checkbox-column">
                      <input
                        type="checkbox"
                        className="form-check-input"
                        checked={selectedUsersSafe.includes(item.id)}
                        onChange={() => handleCheckboxChange?.(item.id)}
                      />
                    </td>
                  )}
                  {tableConfig.showRadioForTheseTables && (
                    <td className="yp-custom-table__checkbox-column">
                      <input
                        type="radio"
                        className="form-check-input"
                        value={item.id}
                        checked={selectedUsersSafe.includes(item.id)}
                        onChange={() => handleCheckboxChange?.(item.id)}

                        // disabled logic for Pending Approvals Table
                        disabled={
                          isPendingApprovalsTable() &&
                          (item.userStatus === "Approved" || item.userStatus === "Rejected")
                        }
                      />
                    </td>
                  )}

                  {tableConfig.columns.map((col) => (
                    <td key={col.key}>{renderCell(item, col)}</td>
                  ))}

                  {tableConfig.showActionColumnForTheseTables && (
                    <td className="text-center yp-table-action-column">
                      {/* Learning Path → View Icon */}
                      {type ===
                        COMMON_TABLE_TYPE.MANAGE_ADMIN_LEARNING_PATH && (
                          <Link
                            to={`${AdminPageRoutes.ADMIN_MANAGE_LEARNING_PATH.FULL_PATH}/viewLearningPath/${item.id}`}
                            className="yp-link-icon yp-link-icon-primary"
                            title="View Learning Path"
                          >
                            <i className="fa fa-eye yp-text-15"></i>
                          </Link>
                        )}

                      {/* Pending Approvals → View Icon */}
                      {type ===
                        COMMON_TABLE_TYPE.PENDING_APPROVALS && (
                          <Link
                            to="#"
                            className="yp-link-icon yp-link-icon-primary"
                            title="View User Details"
                            onClick={(e) => {
                              e.preventDefault(); // prevent navigation since using "#"
                              handleOpenDetailsPopup?.(item.id);
                            }}
                          >
                            <i className="fa fa-eye yp-text-15"></i>
                          </Link>
                        )}

                      {/* Edit Icon */}
                      {tableConfig.showEditIconForTheseTables && (
                        <Link
                          to={
                            type === COMMON_TABLE_TYPE.MANAGE_USER
                              ? `${AdminPageRoutes.ADMIN_MANAGE_USER.FULL_PATH}/editUser/${item.id}`
                              : type === COMMON_TABLE_TYPE.MANAGE_COURSES
                                ? `${AdminPageRoutes.ADMIN_CONTENT_MANAGE_COURSES.FULL_PATH}/contentEditCourse/${item.id}`
                                : type === COMMON_TABLE_TYPE.MANAGE_ASSET_LIBRARY
                                  ? `${AdminPageRoutes.ADMIN_CONTENT_MANAGE_ASSET_LIBRARY.FULL_PATH}/contentEditAsset/${item.assetFolderId}/${item.id}`
                                  : type ===
                                    COMMON_TABLE_TYPE.MANAGE_ADMIN_LEARNING_PATH
                                    ? `${AdminPageRoutes.ADMIN_MANAGE_LEARNING_PATH.FULL_PATH}/editLearningPath/${item.id}`
                                    : `/groupManagement/editGroup/${item.id}`
                          }
                          className="yp-link-icon yp-link-icon-primary"
                        >
                          <i className="fa-regular fa-pen-to-square"></i>
                        </Link>
                      )}

                      {/* Course Launch Icon */}
                      {type === COMMON_TABLE_TYPE.MANAGE_COURSES && (
                        <a
                          className="yp-link-icon yp-link-icon-primary"
                          onClick={() => onCourseLaunch(item.id)}
                          style={{ cursor: "pointer" }}
                        >
                          <i className="fa fa-paper-plane ms-2"></i>
                        </a>
                      )}

                      {/* Asset Viewer */}
                      {type === COMMON_TABLE_TYPE.MANAGE_ASSET_LIBRARY && (
                        <>{assetsViewer(item)}</>
                      )}

                      {/* Learning Path → File Icon */}
                      {type ===
                        COMMON_TABLE_TYPE.MANAGE_ADMIN_LEARNING_PATH && (
                          <a
                            href="#"
                            className="yp-link-icon yp-link-icon-primary"
                            title="Copy Files"
                            onClick={(e) => {
                              e.preventDefault();
                              if (actionCopyClick) actionCopyClick(item.id);
                            }}
                          >
                            <i className="fa-regular fa-copy"></i>
                          </a>
                        )}

                      {/* Learning Path → Delete Icon */}
                      {type ===
                        COMMON_TABLE_TYPE.MANAGE_ADMIN_LEARNING_PATH && (
                          <a
                            href="#"
                            className="yp-link-icon yp-link-icon-primary"
                            onClick={(e) => {
                              e.preventDefault();
                              if (actionDeleteClick) actionDeleteClick(item.id);
                            }}
                            title="Delete Learning Path"
                          >
                            <i className="fa-solid fa-trash-can"></i>
                          </a>
                        )}
                    </td>
                  )}
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan={tableConfig.columns.length + 2}>No data found</td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
      {!isHidePagination ? (
        <div className="yp-custom-pagination d-flex justify-content-between align-items-center flex-wrap"
          id={`yp-custom-pagination-${type}`}>
          <div className="yp-custom-pagination__showing">
            {totalRecords > 0 ? (
              <>
                Showing {(currentPage - 1) * pageSize + 1} to{" "}
                {Math.min(currentPage * pageSize, totalRecords)} of{" "}
                {totalRecords} entries
              </>
            ) : (
              <>Showing 0 of 0 entries</>
            )}
          </div>
          {/* pagination */}
          {totalRecords > 0 ? (
            <ul className="pagination">
              <li
                className={`page-item page-item-prev ${currentPage === 1 ? "disabled" : ""
                  }`}
              >
                <a
                  className="page-link"
                  onClick={() => onPageChange(currentPage - 1, pageSize)}
                >
                  <i className="fa fa-angle-left"></i>
                </a>
              </li>
              {pageNumbers.map((page) => (
                <li
                  key={page}
                  className={`page-item ${currentPage === page ? "active" : ""
                    }`}
                >
                  <a
                    className="page-link"
                    onClick={() => handlePageClick(page)}
                  >
                    {page}
                  </a>
                </li>
              ))}
              <li
                className={`page-item page-item-next ${currentPage === totalPages ? "disabled" : ""
                  }`}
              >
                <a
                  className="page-link"
                  onClick={() => onPageChange(currentPage + 1, pageSize)}
                >
                  <i className="fa fa-angle-right"></i>
                </a>
              </li>
            </ul>
          ) : (
            <></>
          )}
        </div>
      ) : (
        <></>
      )}
    </>
  );
};

export default CommonTable;
