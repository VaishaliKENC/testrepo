import React, {
  useState,
  useEffect,
  useMemo,
  ChangeEvent,
  useCallback,
  useRef,
} from "react";
import { CommonFolderTreeViewProps } from "../../../../Types/commonTableTypes";
import { useAppDispatch } from "../../../../hooks";
import { useSelector } from "react-redux";
import  { RootState } from "../../../../redux/store";
import { FetchCourseListByFile } from "../../../../redux/Slice/admin/contentSlice";
import FileTable from "./FileTable";
import FolderTree from "./FolderTree";
import { debounce } from "lodash";
export interface FolderOrFile {
  id?: string;
  name: string;
  path: string;
  children?: FolderOrFile[];
  size?: number;
  type?: string;
  lastModified?: string;
  isFile?: boolean;
  hasChildren?: boolean;
  isOpen: boolean;
}

const FolderTreeView = <
  T extends {
    id: string;
    isFile: boolean;
    lastModified: string;
    type: string;
    size: number;
    children: any;
    path: string;
    name: string;
    filteredTreeData?: any;
    hasChildren?: boolean;
    isOpen: boolean;
  }
>(
  props: CommonFolderTreeViewProps<T>
) => {
  const { data, onFileSelect } = props;

  const dispatch = useAppDispatch();
  const clientId: any = useSelector((state: RootState) => state.auth.clientId);
  const userId: any = useSelector((state: RootState) => state.auth.id);
  const [folders, setFolders] = useState<FolderOrFile[]>([
    {
      id: "Courses",
      name: "Courses",
      path: "Courses",
      children: [],
      hasChildren: true,
      size: 0,
      type: "",
      lastModified: "",
      isFile: false,
      isOpen: true,
    },
  ]);
  const [selectedFolder, setSelectedFolder] = useState<FolderOrFile | null>(
    null
  );
  const [selectedFolderName, setSelectedFolderName] = useState<
    string | undefined
  >(undefined);
  const [folderSearchKeyword, setFolderSearchKeyword] = useState<string>("");
  const [isLoading, setIsLoading] = useState(false); // Add loading state

  const transformTreeData = () => {
    if (!Array.isArray(data)) return [];
    return data.map((item) => ({
      id: item.id,
      name: item.name,
      path: item.path,
      children: item.children,
      hasChildren: item.hasChildren,
      size: item.size,
      type: item.type,
      lastModified: item.lastModified,
      isFile: item.isFile,
      isOpen: false,
    }));
  };
  useEffect(() => {
    const updatedRoot = {
      ...folders[0],
      children: transformTreeData() || [],
    };
    setFolders([updatedRoot]);
    // Automatically select the root folder if it exists
    if (transformTreeData().length > 0) {
      setSelectedFolder(folders[0]);
      setSelectedFolderName("Courses");
    }
  }, [data]);

  const handleFileSelect = (filePath: string) => {
    if (onFileSelect) onFileSelect(filePath);
  };
  const fetchCourseList = (folder: any) => {
    const params = {
      id: folder.name,
      clientId: clientId,
      createdById: userId,
      currentUserId: userId,
      listRange: {
        pageIndex: 1,
        pageSize: 5,
        totalRows: 0,
        sortExpression: "string",
        requestedById: "string",
        keyWord: "string",
      },
      fileURL: `Courses/${folder.path.split("Courses/")[1]}`,
      keyword: "",
    };
    dispatch(FetchCourseListByFile(params));
  };
  useEffect(() => {
    if (selectedFolder !== null) {
      const params = {
        id: selectedFolder?.name,
        clientId: clientId,
        createdById: userId,
        currentUserId: userId,
        listRange: {
          pageIndex: 1,
          pageSize: 5,
          totalRows: 0,
          sortExpression: "string",
          requestedById: "string",
          keyWord: "string",
        },
        fileURL: `Courses/${selectedFolder?.path.split("Courses/")[1]}`,
        keyword: "",
      };
      dispatch(FetchCourseListByFile(params));
    }
  }, [selectedFolder, selectedFolderName]);

  const handleCoursesFolderSearch = (event: ChangeEvent<HTMLInputElement>) => {
    setFolderSearchKeyword(event.target.value);
    debouncedSearch(event.target.value);
  };
  const searchFolder = useCallback(
    (searchKeyword: string) => {
      const root = folders[0];
      if (searchKeyword !== "") {
        const filteredOutput = transformTreeData()?.filter((item) =>
          item.name.toLowerCase().includes(searchKeyword.toLowerCase())
        );
        const updatedRoot = {
          ...root,
          children: filteredOutput || [],
        };
        setFolders([updatedRoot]);
      } else {
        const updatedRoot = {
          ...root,
          children: transformTreeData() || [],
        };
        setFolders([updatedRoot]);
      }
    },
    [folders, data, setFolders]
  );

  const debouncedSearch = useMemo(
    () => debounce(searchFolder, 500),
    [searchFolder]
  );
  useEffect(() => {
    return () => {
      debouncedSearch.cancel();
    };
  }, [debouncedSearch]);


  // Set modal content box height
  const setMainContentBoxHeight = () => {
    let windowHt = window.innerHeight;
    let finalHt;
    let mainCardSubSectionHt = document.getElementById("yp-tree-sidebar-table-wrapper");
    
    if (document.body.classList.contains('modal-open')) {
      if(mainCardSubSectionHt) {
        let modalHeaderHt = document.getElementsByClassName("modal-header")[0]?.clientHeight;
        finalHt = windowHt - modalHeaderHt;
        mainCardSubSectionHt.style.minHeight = `${finalHt}px`;
        console.log(finalHt);
      }
    }
  }
  useEffect(() => {
    setTimeout(() => {    
      setMainContentBoxHeight();
    }, 500);
  }, []);

  // handleToggleTreeSidebar
  const treeSidebarTableRef = useRef<HTMLDivElement | null>(null);
  const [isSidebarCollapsed, setIsSidebarCollapsed] = useState(false);
  const handleToggleTreeSidebar = () => {
      setIsSidebarCollapsed(!isSidebarCollapsed);
      if (treeSidebarTableRef.current) {
          if (isSidebarCollapsed) {
              treeSidebarTableRef.current.classList.remove('yp-tree-main-ul-sidebar-collapsed');
          } else {
              treeSidebarTableRef.current.classList.add('yp-tree-main-ul-sidebar-collapsed');
          }
      }
  };
  useEffect(() => {
      // Add initial class if needed on component mount
      if (treeSidebarTableRef.current && isSidebarCollapsed) {
          treeSidebarTableRef.current.classList.add('yp-tree-main-ul-sidebar-collapsed');
      }

      // Call setTreeListHeightFunction function
      //debouncedSetTreeListHeightFunction();
  }, [isSidebarCollapsed]);
  // END handleToggleTreeSidebar

  // Start Set tree structure scrollbar height 
  const treeSidebarMainWrapperRef = useRef<HTMLDivElement | null>(null);
  const treeSidebarSearchWrapperRef = useRef<HTMLDivElement | null>(null);
  const treeSidebarListWrapperRef = useRef<HTMLDivElement | null>(null);
  const [treeListHeight, setTreeListHeight] = useState<number>(0);

  // Debounced version of setTreeListHeightFunction
  const debouncedSetTreeListHeightFunction = useCallback(
      debounce(() => {
          if (treeSidebarMainWrapperRef.current && treeSidebarSearchWrapperRef.current && treeSidebarListWrapperRef.current) {
              const listWrapperStyles = window.getComputedStyle(treeSidebarListWrapperRef.current);
              const padding = parseFloat(listWrapperStyles.paddingTop) + parseFloat(listWrapperStyles.paddingBottom);

              const finalScrollHeight = treeSidebarMainWrapperRef.current?.getBoundingClientRect().height
                  - (treeSidebarSearchWrapperRef.current?.getBoundingClientRect().height
                    + padding);
              setTreeListHeight(finalScrollHeight);
          }
      }, 150),
      []
  );

  useEffect(() => {
      const sidebarWrapper = treeSidebarTableRef.current;
      if (!sidebarWrapper) return;

      const resizeObserver = new ResizeObserver((entries) => {
          for (let entry of entries) {
              if (entry.target === sidebarWrapper) {
                  debouncedSetTreeListHeightFunction(); // Call the debounced function
              }
          }
      });

      resizeObserver.observe(sidebarWrapper);

      return () => {
          resizeObserver.disconnect();
          debouncedSetTreeListHeightFunction.cancel(); // Cancel any pending calls
      };
  }, [debouncedSetTreeListHeightFunction]);
  // END Set tree structure scrollbar height

  return (
    <div id="yp-modal-tree-fileTable-wrapper">
      {/* {isLoading && <p>Loading file structure...</p>}  */}
      {/* Display loading message */}
      <div className="yp-tree-sidebar-table-wrapper" id="yp-tree-sidebar-table-wrapper" ref={treeSidebarTableRef}>
        <div className="yp-tree-main-ul-sidebar" ref={treeSidebarMainWrapperRef}>
          <div className="yp-tree-mus-toggle-button"
              onClick={handleToggleTreeSidebar}
              title="Show/Hide Course Sidebar">
              <i className={`fa ${isSidebarCollapsed ? 'fa-folder-tree' : 'fa-folder-tree'}`}></i>
          </div>
          <div className="yp-tree-main-search-wrapper" ref={treeSidebarSearchWrapperRef}>
            <div className="yp-form-control-with-icon">
              <div className="form-group mb-0">
                <div className="yp-form-control-wrapper">
                    <input
                      type="text"
                      name="tableSearchInput"
                      //placeholder={"Search Folder Name here..."}
                      placeholder=""
                      title={`Search: Courses Folder`}
                      value={folderSearchKeyword}
                      className="form-control yp-form-control"
                      onChange={handleCoursesFolderSearch}
                    />
                    <label className="form-label">{"Search Folder Name here..."}</label>
                    <span className="yp-form-control-icon">
                      <i className="fa fa-search"></i>
                    </span>
                </div>
              </div>
            </div>
          </div>
          <div className="yp-tree-main-ul-wrapper pt-0" ref={treeSidebarListWrapperRef}>
            {folders.length === 0 && <span>No Folders found</span>}
            <FolderTree
              folders={folders}
              onSelect={(folder) => {
                setSelectedFolder(folder);
                setSelectedFolderName(folder?.name);
                fetchCourseList(folder);
              }}
              selectedFolderName={selectedFolderName}
              setFolders={setFolders}
              treeHeight={treeListHeight}
            />
          </div>
        </div>
        <div id="yp-tree-fileTableDivWrapper" className="yp-custom-table-section yp-custom-table-section-inside-card">
          {!isLoading && folders.length > 0 && (
            <div className="flex flex-grow">
              {selectedFolder && (
                <FileTable
                
                  selectedFolderName={selectedFolderName}
                  onFileSelect={handleFileSelect}
                
                  selectedFolder={selectedFolder}
                />
              )}
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default FolderTreeView;
