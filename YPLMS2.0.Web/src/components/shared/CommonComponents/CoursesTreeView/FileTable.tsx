import { useEffect, useState } from "react";
import { useAppDispatch } from "../../../../hooks";
import { FetchCourseListByFile } from "../../../../redux/Slice/admin/contentSlice";
import { COMMON_TABLE_TYPE } from "../../../../utils/Constants/Enums";
import CommonTable from "../CommonTable";
import { useSelector } from "react-redux";
import { FolderOrFile } from "./FolderTreeView";
import { RootState } from "../../../../redux/store";
import { MANAGE_TREE_COURSES_FILES_TABLE_CONFIG } from "../../../../utils/Constants/tableConfig";

const FileTable: React.FC<{
  selectedFolderName: string | undefined;
  onFileSelect: (filePath: string) => void;
  selectedFolder: Record<string, any>;
}> = ({ selectedFolderName, onFileSelect, selectedFolder }) => {
 
 const fileList: any = useSelector(
    (state: RootState) => state.course?.courseTreeFileList
  );
  // Added onFileSelect prop
  const [selectedFilePath, setSelectedFilePath] = useState<string>(""); // State to track selected file
  const dispatch = useAppDispatch();
  const clientId: any = useSelector((state: RootState) => state.auth.clientId);
  const userId: any = useSelector((state: RootState) => state.auth.id);
  const [pageSize, setPageSize] = useState(5);
  const [currentPage, setCurrentPage] = useState(1);
  const [searchKeyword, setSearchKeyword] = useState<string>("");
  const handleFileSelect = (filePath: string) => {
    setSelectedFilePath(filePath);
  };
  const handleFileSave = (filePath: string) => {
    onFileSelect(filePath); // Call the callback with the selected file path
  };
  const modifyFileList = (fileList: FolderOrFile[]) => {
    return fileList.map((item: any) => ({ ...item, id: item.path }));
  };

  useEffect(() => {
    let saveCancelFooter = document.getElementById("yp-custom-table-sticky-footer");
    let fileTableDivWrapper = document.getElementById("yp-tree-fileTableDivWrapper");
    if (saveCancelFooter && fileTableDivWrapper) {
      let tableResponsive = fileTableDivWrapper.querySelector(".table-responsive");
      if (tableResponsive) {
        tableResponsive.appendChild(saveCancelFooter);
      }
    }
  }, [selectedFilePath]);

  const handlePageChange = (page: number, newPageSize?: number) => {
    if (newPageSize) setPageSize(newPageSize); // Update page size if provided
    setCurrentPage(page); // Update the current page state
    setSelectedFilePath("");
    const params = {
      id: selectedFolderName || "",
      clientId: clientId,
      createdById: userId,
      currentUserId: userId,
      listRange: {
        pageIndex: page,
        pageSize: newPageSize || pageSize,
        totalRows: 0,
        sortExpression: "string",
        requestedById: "string",
        keyWord: "string",
      },
      fileURL: `Courses/${selectedFolder.path.split("Courses/")[1]}`,
      keyword: searchKeyword,
    };
    dispatch(FetchCourseListByFile(params));
  };
  const handleSearch = (keyword: string) => {
    const trimmedKeyword = keyword.trim();

    setSearchKeyword(trimmedKeyword);
    // Always reset current page to 1 before calling fetch
    const updatedPage = 1;
    setCurrentPage(updatedPage);

    const params = {
      id: selectedFolderName || "",
      clientId: clientId,

      createdById: userId,
      currentUserId: userId,
      listRange: {
        pageIndex: 1,
        pageSize: pageSize,
        totalRows: 0,
        sortExpression: "string",
        requestedById: "string",
        keyWord: "string",
      },
      fileURL: `Courses/${selectedFolder.path.split("Courses/")[1]}`,
      keyword: trimmedKeyword,
    };
    dispatch(FetchCourseListByFile(params));
  };
  return (
    <div>
      {/* <div
        className="yp-table-top-search-section"
        style={{ paddingTop: "0px" }}
      >
        <div className="yp-ttss-right-section">
          <div className="d-flex flex-grow-1 flex-wrap align-items-center gap-3">
            <div className="yp-table-title">
              File List for "{selectedFolderName}"
            </div>
          </div> */}
      {/* <div className="d-flex flex-wrap gap-3">
                    <div className="d-inline-flex flex-wrap">
                        <div className="yp-form-control-with-icon">
                            <div className="form-group">
                                <div className="yp-form-control-wrapper">
                                    <input type="text" name=""
                                    placeholder="Search here..."
                                    className="form-control yp-form-control yp-form-control-sm" 
                                    onKeyDown={(e) => { if (e.key === ' ') e.preventDefault(); }}/>
                                    <span className="yp-form-control-icon"><i className="fa fa-search"></i></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="yp-page-show-records">
                        <div className="yp-inline-label-input">
                            <label className="form-label">Show</label>
                            <select name="" className="form-control yp-form-control yp-form-control-sm">
                                <option value="5">5</option>
                                <option value="10">10</option>
                                <option value="15">15</option>
                            </select>
                            <label className="form-label">Records</label>
                        </div>
                    </div>
                </div> */}
      {/* </div>
      </div> */}
      {/* className="table-responsive" */}
      
        <CommonTable
          type={COMMON_TABLE_TYPE.MANAGE_TREE_COURSES_FILES}
          tableConfig={MANAGE_TREE_COURSES_FILES_TABLE_CONFIG}
          data={modifyFileList(fileList.courses || [])}
          onSearch={handleSearch}
          currentPage={currentPage}
          totalRecords={fileList?.totalRows}
          pageSize={pageSize}
          onPageChange={handlePageChange}
          handleSelectAll={() => {}}
          handleCheckboxChange={handleFileSelect}
          selectedUsers={[selectedFilePath]}
          tableTitle={selectedFolderName}
          searchValue=""
        />
        {selectedFilePath && (
          <div id="yp-custom-table-sticky-footer">
            <div className="d-flex flex-wrap align-items-center justify-content-end gap-3">
              <button
                type="button"
                className="btn btn-primary"
                onClick={() => handleFileSave(selectedFilePath)}
              >
                Save
              </button>
              <button
                type="button"
                className="btn btn-secondary"
                onClick={() => handleFileSelect("")}
              >
                Cancel
              </button>
            </div>
          </div>
        )}
      {/* <div className="yp-custom-pagination d-flex justify-content-between align-items-center flex-wrap">
            <div className="yp-custom-pagination__showing">Showing 0 of 0 entries</div>      
            <ul className="pagination">
                <li className={`page-item page-item-prev}`}>
                <a className="page-link"><i className="fa fa-angle-left"></i></a>
                </li>
                <li className={`page-item`}><a className="page-link">1</a></li>
                <li className={`page-item`}><a className="page-link">2</a></li>
                <li className={`page-item`}><a className="page-link">3</a></li>
                <li className={`page-item page-item-next`}>
                <a className="page-link">
                    <i className="fa fa-angle-right"></i>
                </a>
                </li>
            </ul>
        </div> */}
    </div>
  );
};
export default FileTable;
