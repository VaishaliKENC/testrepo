import { useState } from "react";
import FolderNode from "./FolderNode";
import { FolderOrFile } from "./FolderTreeView";
import { useAppDispatch } from "../../../../hooks";
import { useSelector } from "react-redux";
import { RootState } from "../../../../redux/store";
import { fetchCourseSubFolders } from "../../../../redux/Slice/admin/contentSlice";

const FolderTree: React.FC<{
  folders: FolderOrFile[];
  onSelect: (folder: FolderOrFile | null) => void;
  selectedFolderName?: string;
  setFolders: React.Dispatch<React.SetStateAction<FolderOrFile[]>>;
  treeHeight?: any;
}> = ({ folders, onSelect, selectedFolderName, setFolders, treeHeight }) => {
  const [openFolders, setOpenFolders] = useState<{ [key: string]: boolean }>({});
  const dispatch = useAppDispatch();
  const clientId: any = useSelector((state: RootState) => state.auth.clientId);

  // Expand the root folder(s) by default
  //   useEffect(() => {
  //     const initialOpenFolders: { [key: string]: boolean } = {};
  //     folders.forEach((folder) => {
  //       if (!folder.isFile && folder.children && folder.children.length > 0) {
  //         initialOpenFolders[folder.path] = true;
  //       }
  //     });
  //     setOpenFolders(initialOpenFolders);
  //   }, [folders]);
  const openFolderSubfolders = (
    nodes: FolderOrFile[],
    targetPath: string,
    children: FolderOrFile[]
  ): FolderOrFile[] => {
    return nodes.map((node) => {
      if (node.path === targetPath) {
        return {
          ...node,
          children,
          isOpen: true,
        };
      } else if (node.children) {
        return {
          ...node,
          children: openFolderSubfolders(node.children, targetPath, children),
        };
      }
      return { ...node };
    });
  };
  const hideOpenFolders = (prevFolder: FolderOrFile[], path: string): FolderOrFile[] => {
    return prevFolder.map((node: any) => {
      if (node.path === path) {
        return {
          ...node,
          isOpen: false,
        };
      } else if (node.children) {
        return {
          ...node,
          children: hideOpenFolders(node.children, path),
        };
      }
      return { ...node };
    });
  };
  const toggleFolderCollapse = (item: Record<string, any>) => {
    setOpenFolders((prev) => ({ ...prev, [item.path]: !prev[item.path] }));
    //close if folder open and clicked
    if (item.isOpen) {
      setFolders((prevFolder) => hideOpenFolders(prevFolder, item.path));
    } else {
      //fetch subfolders and open 
      dispatch(
        fetchCourseSubFolders({
          clientId,
          filePath: item.path.split("Courses/")[1] || "",
        })
      )
        .then((response: any) => {
          if (response.meta.requestStatus === "fulfilled") {
            setFolders((prevFolders) => {
              return openFolderSubfolders(
                prevFolders,
                item.path,
                response.payload.courses
              );
            });
            // setCourseStructureTreeData(response.payload.courses || []);
          } else {
            // setAlert({ type: "error", message: 'Failed to fetch course folder structure: ' + response.payload?.message || 'Unknown error.' });
          }
        })
        .catch((error) => {
          console.error("Error while fetching course folder structure:", error);
        });
    }
  };

  return (
    <ul className="yp-tree-main-ul" style={{ 'maxHeight': treeHeight + 'px' }}>
      {folders
        .filter((item) => !item.isFile)
        .map(
          (
            item // Filter out files!
          ) => (
            <FolderNode
              key={item.path}
              item={item}
              openFolders={openFolders}
              toggleFolderCollapse={toggleFolderCollapse}
              onSelect={onSelect}
              selectedFolderName={selectedFolderName}
            />
          )
        )}
    </ul>
  );
};
export default FolderTree;