import { useLocation } from "react-router-dom";
import CustomBreadcrumb from "../../components/shared/CustomBreadcrumb";
import { useAppDispatch, useAppSelector } from "../../hooks";
import { RootState } from "../../redux/store";
import { AdminPageRoutes } from "../../utils/Constants/Admin_PageRoutes";
import { useEffect, useState } from "react";
import {
  getAdminLeaderBoardRankList,
  getLeaderBoardCourseList,
} from "../../redux/Slice/leaderBoard/leaderBoard.requests";
import CommonTable from "../../components/shared/CommonComponents/CommonTable";
import { ACTION_BUTTON_ENUM, COMMON_TABLE_TYPE } from "../../utils/Constants/Enums";
import { formatDate } from "../../utils/commonUtils";
import { mapLeaderBoardCourseList } from "../../utils/commonHelpers";
import ExcelJS from "exceljs";
import { saveAs } from "file-saver";
import { MANAGE_ADMIN_LEADERBOARD_USERS_TABLE_CONFIG } from "../../utils/Constants/tableConfig";

const AdminLeaderBoardUsers = () => {
  const pageTitle: string = "Leaderboard";
  document.title = pageTitle;
  /** Breadcrumb navigation items*/
  const breadcrumbItems = [
    {
      iconClass: "fa-classic fa-solid fa-house",
      path: AdminPageRoutes.ADMIN_DASHBOARD.FULL_PATH,
    },
    { label: "Leaderboard" },
  ];

  const dispatch = useAppDispatch();
  const searchParams = new URLSearchParams(useLocation()?.search);
  const decodedString = decodeURIComponent(
    searchParams?.get?.("activityId") ?? ""
  );
  const receivedObject = decodedString ? JSON.parse?.(decodedString) : "";
  const clientId: any = useAppSelector(
    (state: RootState) => state.auth.clientId
  );
  const userId: any = useAppSelector((state: RootState) => state.auth.id);
  // const userId: any = 'USRd762415f329b4537';

  const { loading } = useAppSelector((state: RootState) => state.leaderBoard);
  const { rankList, totalRows } = useAppSelector(
    (state: RootState) => state?.leaderBoard?.adminCourseLeaders
  );
  const courseList = useAppSelector((state: RootState) =>
    mapLeaderBoardCourseList(state?.leaderBoard?.courseList)
  );
  const [selectedCourse, setSelectedCourse] = useState(receivedObject ?? null);


  const [pageSize, setPageSize] = useState(10);
  const [currentPage, setCurrentPage] = useState(1);
  const actionButtonList = [
    { type: "button", className: "btn btn-primary", buttonName: "Download", actionType: ACTION_BUTTON_ENUM?.ACTIVATE, disabled: rankList.length === 0 },
  ];
  useEffect(() => {
    dispatch(
      getLeaderBoardCourseList({ clientId })
    );
  }, []);

  useEffect(() => {
    if (selectedCourse && selectedCourse !== null)
      callApi(selectedCourse.value, 1, pageSize);
  }, []);

  const handlePageChange = (page: number, newPageSize?: number) => {
    if (newPageSize) {
      setPageSize(newPageSize);
    }
    setCurrentPage(page);
    callApi(selectedCourse.value, page, newPageSize || pageSize);
  };
  const modifyCourse = (leaders: any[]) => {
    return leaders.map((rank: any) => ({
      ...rank,
      dateOfCompletion: formatDate(rank.dateOfCompletion),
      score: parseInt(rank.score),
    }));
  };

  const handleSelectChange = (selectedOption: any) => {
    if (selectedOption && selectedOption !== null ) {
      setSelectedCourse(selectedOption);
      callApi(selectedOption.value, currentPage, pageSize);
    } else {
      setSelectedCourse(null);
    }
  };

  const callApi = (courseId: string, pageIndex: number, pageSize: number) => {
    dispatch(
      getAdminLeaderBoardRankList({
        clientId,
        activityId: courseId,
        pageIndex: pageIndex,
        pageSize: pageSize,
      })
    );
  };


  const handleActionButtonClick = async () => {
    try {
      if (!selectedCourse?.value) {
        alert("Please select a course first!");
        return;
      }

      // ✅ Fetch data from API
      const response = await dispatch(
        getAdminLeaderBoardRankList({
          clientId,
          activityId: selectedCourse.value,
        })
      ).unwrap();

      console.log("API Full Response:", response);

      // ✅ Extract leaderboard data
      const rankList = response?.assetTypeList || [];
      console.log("Extracted rankList:", rankList);

      if (!Array.isArray(rankList) || rankList.length === 0) {
        alert("No data available for download.");
        return;
      }

      // ✅ Define headers & matching API fields
      const headers = ["Rank", "LoginId", "Full Name", "Score", "CompletionDate"];
      const fieldMapping = ["rank", "loginID", "fullName", "score", "dateOfCompletion"];

      // ✅ Create workbook & worksheet
      const workbook = new ExcelJS.Workbook();
      const worksheet = workbook.addWorksheet("Leaderboard Report");

      // ✅ Add custom headers
      worksheet.addRow(headers);

      // ✅ Style header row
      const headerRow = worksheet.getRow(1);
      headerRow.eachCell((cell: ExcelJS.Cell) => {
        cell.font = { bold: true, color: { argb: "FFFFFFFF" } };
        cell.fill = {
          type: "pattern",
          pattern: "solid",
          fgColor: { argb: "1F8EC8" }, // Header background color
        };
        cell.alignment = { horizontal: "center" };
      });

      // ✅ Add only selected fields data based on fieldMapping
      rankList.forEach((row: Record<string, any>) => {
        const rowData = fieldMapping.map((field) => {
          if (field === "dateOfCompletion" && row[field]) {
            const date = new Date(row[field]);
            return `${date.getDate().toString().padStart(2, "0")}-${(date.getMonth() + 1)
              .toString()
              .padStart(2, "0")}-${date.getFullYear()}`;
          }
          return row[field] ?? "";
        });
        worksheet.addRow(rowData);
      });

      // ✅ Auto-adjust column widths
      worksheet.columns.forEach((column: Partial<ExcelJS.Column>, index: number) => {
        if (column) {
          const maxLength = Math.max(
            headers[index].length,
            ...rankList.map((row: any) =>
              row[fieldMapping[index]] ? row[fieldMapping[index]].toString().length : 0
            )
          );
          column.width = Math.min(maxLength + 2, 50);
        }
      });

      // ✅ Generate Excel & download
      const buffer = await workbook.xlsx.writeBuffer();
      const blob = new Blob([buffer], {
        type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      });

      saveAs(blob, "Leaderboard_Report.xlsx");
    } catch (error) {
      console.error("Error downloading Excel report:", error);
    }
  };


  return (
    <>
      {/* Loading spinner overlay */}
      {loading && (
        <div className="yp-loading-overlay">
          <div className="yp-spinner">
            <div className="spinner-border"></div>
          </div>
        </div>
      )}

      <div
        className="yp-page-title-button-section"
        id="yp-page-title-breadcrumb-section"
      >
        <div className="yp-page-title-breadcrumb">
          <div className="yp-page-title">{pageTitle}</div>
          <CustomBreadcrumb items={breadcrumbItems} />
        </div>
      </div>

      {/* Common list here */}

      <div className="yp-card p-0 pb-3" id="yp-card-main-content-section">
        <div className="yp-custom-table-section">
          <CommonTable
            type={COMMON_TABLE_TYPE.MANAGE_ADMIN_LEADERBOARD_USERS}
             tableConfig={MANAGE_ADMIN_LEADERBOARD_USERS_TABLE_CONFIG}
            data={
              selectedCourse && selectedCourse != null
                ? modifyCourse(rankList || [])
                : []
            }
            currentPage={currentPage} //currentPage
            totalRecords={totalRows} // fileList?.totalRows
            pageSize={pageSize} //pageSize
            onPageChange={handlePageChange} //handlePageChange
            handleSelectChange={handleSelectChange}
            onSearch={() => { }}
            searchValue=""
            defaultAutcompleteValue={selectedCourse}
            autocompleteData={courseList}
            actionButtonList={actionButtonList}
            actionButtonClick={handleActionButtonClick}
          />
        </div>
      </div>
    </>
  );
};

export default AdminLeaderBoardUsers;