import { FolderOrFile } from "./FolderTreeView";

const FolderNode: React.FC<{
  item: FolderOrFile;
  openFolders: { [key: string]: boolean };
  toggleFolderCollapse: (item: Record<string, any>) => void;
  onSelect: (item: FolderOrFile | null) => void;
  selectedFolderName: string | undefined;
}> = ({ item, openFolders, toggleFolderCollapse, onSelect, selectedFolderName }) => {
  const isFolder = item.children !== undefined && !item.isFile && item.isOpen;
  const isCurrentSelected = selectedFolderName === item.name;

  return (
    <li className={isCurrentSelected ? "active" : ""}>
      {item.isFile === false && ( // Only render if it's a folder
        <div className="flex items-center gap-2">
          {item.hasChildren ? ( // Check for subfolders
            <span
              onClick={() => {
                toggleFolderCollapse(item);
              }}
              className="yp-tree-plusMinus-icon"
            >
              {/* onClick only if subfolders exist */}
              {item.isOpen ? (
                <i className="zmdi zmdi-minus-circle-outline"></i>
              ) : (
                <i className="zmdi zmdi-plus-circle-o"></i>
              )}
             
            </span>
          ) : (
            // Empty span if no subfolders
            <span className="yp-tree-plusMinus-icon yp-tree-plusMinus-icon-empty"></span>
          )}

          <span className="yp-tree-folder-icon">
            {openFolders[item.path] || isCurrentSelected ? (
              <i className="fa-classic fa-solid fa-folder-open fa-fw"></i>
            ) : (
              <i className="fa-classic fa-solid fa-folder fa-fw"></i>
            )}
          </span>
          <span
            onClick={() => {
              onSelect(item);
            }}
            className="yp-tree-folderName"
          >
            {item.name}
            {/* <span className="yp-tree-folderName-count">({isFileCount})</span> */}
          </span>
        </div>
      )}
      {isFolder && (
        // Render children (subfolders) ONLY if a folder
        <ul className="yp-tree-sub-main-ul">
          {item.children
            ?.filter((child) => !child.isFile)
            .map(
              (
                child // Filter out files!
              ) => (
                <FolderNode
                  key={child.path}
                  item={child}
                  openFolders={openFolders}
                  toggleFolderCollapse={toggleFolderCollapse}
                  onSelect={onSelect}
                  selectedFolderName={selectedFolderName}
                />
              )
            )}
        </ul>
      )}
    </li>
  );
};
export default FolderNode;