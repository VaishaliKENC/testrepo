import React, { useEffect, useState, useRef, useCallback, useMemo } from 'react';
import { useAppDispatch, useAppSelector } from '../../../hooks';
import { useSelector } from 'react-redux';
import { RootState } from '../../../redux/store';
import { useNavigate } from 'react-router-dom';
import { AdminPageRoutes } from '../../../utils/Constants/Admin_PageRoutes';
import { ACTION_BUTTON_ENUM, COMMON_TABLE_TYPE } from '../../../utils/Constants/Enums';
import { IDeletePayload, IAssetLibraryAddEditAssetFolderPayload, IAssetLibraryAssetListPayload, IActivateDeactivatePayload, IAssetLibraryAssetListActivateDeactivatePayload } from '../../../Types/commonTableTypes';
import { debounce } from 'lodash';
import { fetchAssetFilesListSlice, activateDeactivateAssetFilesSlice, fetchAssetFoldersSlice, addAssetFolderSlice, editAssetFolderSlice, deleteAssetFolderSlice, deleteAssetFileSlice } from '../../../redux/Slice/admin/assetLibrarySlice';
import CommonTable from '../../../components/shared/CommonComponents/CommonTable';
import ConfirmationModal from '../../../components/shared/ConfirmationModal';
import CustomBreadcrumb from '../../../components/shared/CustomBreadcrumb';
import AlertMessage from '../../../components/shared/AlertMessage';
import AlertMessageInsideModal from '../../../components/shared/AlertMessageInsideModal';
import { MANAGE_ASSET_LIBRARY_TABLE_CONFIG } from '../../../utils/Constants/tableConfig';

import * as bootstrap from 'bootstrap';

interface FolderNode {
    title: string;
    unitId: string;
    children: FolderNode[];
    [key: string]: any;
}

interface FolderTreeProps {
    data: FolderNode[];
}

const ManageAssetLibrary: React.FC = () => {
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const currentUserId: any = useSelector((state: RootState) => state.auth.id);
    const clientId: any = useSelector((state: RootState) => state.auth.clientId);
    const { loading, error } = useAppSelector((state: RootState) => state.assetLibrary);
    const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);

    const [showModal, setShowModal] = useState(false);
    const [modalConfig, setModalConfig] = useState({
        title: '',
        message: '',
        primaryBtnText: '',
    });
    const [modalAlertMessageConfig, setModalAlertMessageConfig] = useState<{
        type: "success" | "error" | "info";
        message: string;
        duration?: string;
        autoClose?: boolean;
    }>({
        type: 'success',
        message: '',
        duration: '',
        autoClose: false
    });
    const [onConfirmAction, setOnConfirmAction] = useState<() => void>(() => () => { });

    //const [assetFolderId, setassetFolderId] = useState<string>('');


    // 
    const [folderTreeData, setFolderTreeData] = useState<FolderNode[]>([]);
    const [selectedFolderId, setSelectedFolderId] = useState<string | null>(null);
    const [selectedFolderName, setSelectedFolderName] = useState<string | null>(null);
    const [selectedFolderParentId, setSelectedFolderParentId] = useState<string | null>(null);
    const [selectedFolderDescription, setSelectedFolderDescription] = useState<string | null>(null);
    const [folderExpandedMap, setFolderExpandedMap] = useState<{ [key: string]: boolean }>({}); //**
    const [searchQueryForFolderTree, setSearchQueryForFolderTree] = useState<string>("");
    const [searchKeyword, setSearchKeyword] = useState<string>('');

    let isAddForm = useRef<boolean>(true);
    const addEditFolderModalTitle: string = isAddForm.current ? 'Add Folder' : 'Edit Folder';

    // Grid List, pagination and actions
    const assetFilesList: any = useSelector((state: RootState) => state.assetLibrary.assetFiles);
    const [assetFiles, setAssetFiles] = useState<IAssetLibraryAssetListPayload[]>([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(5);
    const [appliedFilters, setAppliedFilters] = useState<any>(null);
    const [selectedAssetFiles, setSelectedAssetFiles] = useState<string[]>([]);
    // const [onConfirmAction, setOnConfirmAction] = useState<() => void>(() => () => { });

    // Action buttons configuration
    const actionButtonList = [
        { type: "button", className: "btn btn-secondary", buttonName: "Deactivate", actionType: ACTION_BUTTON_ENUM?.DEACTIVATE, disabled: assetFiles.length === 0 },
        { type: "button", className: "btn btn-danger", buttonName: "Delete", actionType: ACTION_BUTTON_ENUM?.DELETE, disabled: assetFiles.length === 0 }
    ];

    useEffect(() => {
        setSelectedAssetFiles([]);
        if (!selectedFolderId) {
            setModalAlertMessageConfig({
                type: 'info',
                message: 'Please select asset folder first',
                autoClose: false
            });
            return;
        }
        setCurrentPage(1);
        fetchAssetFilesList(1, pageSize, appliedFilters);
        setModalAlertMessageConfig({
            type: 'success',
            message: '',
            duration: '',
            autoClose: false
        });
    }, [selectedFolderId]);
    useEffect(() => {
        setAssetFiles(assetFilesList?.assetList || []);
    }, [assetFilesList]);
    const fetchAssetFilesList = (pageIndex: number, pageSize: number, filters: any) => {
        const params = {
            currentUserId, clientId,
            assetFolderId: selectedFolderId,
            listRange: {
                pageIndex, pageSize,
                sortExpression: 'LastModifiedDate desc',
            },
            keyWord: "",
            userCriteria: filters || undefined,
        };
        dispatch(fetchAssetFilesListSlice(params));
    };
    //asset Preview Closed & reload the page
    useEffect(() => {
        const handleMessage = (event: MessageEvent) => {
            if (event.data === "assetPreviewClosed") {
                window.location.reload(); // Reload parent when popup sends message
            }
        };

        window.addEventListener("message", handleMessage);
        return () => window.removeEventListener("message", handleMessage);
    }, []);

    /** Handle user action button clicks (Activate/Deactivate). */
    const handleActionButtonClick = (actionType: string) => {
        if (actionType === ACTION_BUTTON_ENUM?.ACTIVATE) {
            // no action            
        } else if (actionType === ACTION_BUTTON_ENUM?.DEACTIVATE) {
            handleDeactivateAssetFile()
        } else if (actionType === ACTION_BUTTON_ENUM?.DELETE) {
            handleDeleteAssetFile()
        }
    }

    // Delete selected courses
    const onDeleteAssetFile = (params: IDeletePayload) => {
        dispatch(deleteAssetFileSlice(params)).unwrap()
            .then((response) => {
                if (response?.msg) {
                    setModalAlertMessageConfig({
                        type: 'success',
                        message: response.msg
                    });
                }
                setSelectedAssetFiles([]);
                setCurrentPage(1);
                refreshAssetFilesList(1);
            }).catch((error) => {
                console.error("Error deleting asset file:", error);
                setModalAlertMessageConfig({
                    type: 'error',
                    message: error?.message || "Failed to delete asset file"
                });
            });
    };

    /** Handle Delete button click.  */
    const handleDeleteAssetFile = () => {
        if (selectedAssetFiles.length <= 0) {
            setAlert({ type: "warning", message: "Please select a record.", autoClose: true });
            return;
        }
        const confirmDeleteAssetFile = () => {
            const data = assetFiles.find((data) => selectedAssetFiles.includes(data.id));
            if (data) {
                const recordsToDelete: IDeletePayload = {
                    id: data.id,
                    clientId: clientId
                };
                onDeleteAssetFile(recordsToDelete);
            }
        };
        setModalConfig({
            title: 'Delete Asset Confirmation',
            message: 'Are you sure you want to delete the selected asset?',
            primaryBtnText: 'Delete',
        });
        setOnConfirmAction(() => confirmDeleteAssetFile);
        setShowModal(true);
    };

    /** Handle Deactivate button click.  */
    const handleDeactivateAssetFile = () => {
        if (selectedAssetFiles.length <= 0) {
            setAlert({ type: "warning", message: "Please select a record." }); return;
        }

        const selectedRecordWithActiveStatus = assetFiles.filter(data => selectedAssetFiles.includes(data.id) && data.isActive);
        // If any selected record is already inactive, show an error message
        if (selectedAssetFiles.length !== selectedRecordWithActiveStatus.length) {
            setAlert({ type: "warning", message: "Please select only active record(s)." });
            return;
        }

        const confirmDeactivateAssetFile = () => {
            const data = assetFiles.find((data) => selectedAssetFiles.includes(data.id));
            const recordsToDeactivate: IAssetLibraryAssetListActivateDeactivatePayload | undefined = data
                ? {
                    id: data.id,
                    clientId: clientId,
                    currentUserId: currentUserId,
                    assetName: data.assetName,
                    assetFolderId: data.assetFolderId,
                    assetFileType: data.assetFileType,
                    assetDescription: data.assetDescription,
                    assetFileName: data.assetFileName,
                    isActive: true,
                }
                : undefined;

            // console.log("recordsToDeactivate", recordsToDeactivate)
            if (recordsToDeactivate) {
                onDeactivateAssetFile(recordsToDeactivate);
            }
        };

        setModalConfig({
            title: 'Deactivate Asset File Confirmation',
            message: 'Are you sure you want to deactivate the selected asset file?',
            primaryBtnText: 'Deactivate',
        });
        setOnConfirmAction(() => confirmDeactivateAssetFile);
        setShowModal(true);
    };

    // Deactivate selected record
    const onDeactivateAssetFile = (recordsToDeactivate: IAssetLibraryAssetListActivateDeactivatePayload) => {
        dispatch(activateDeactivateAssetFilesSlice(recordsToDeactivate)).then(() => {
            setSelectedAssetFiles([]);
            setCurrentPage(1);
            refreshAssetFilesList(1);
            setModalAlertMessageConfig({
                type: 'success',
                message: 'Asset files deactivated successfully!'
            });
        }).catch((error) => {
            setModalAlertMessageConfig({
                type: 'error',
                message: 'Error deactivating asset files.'
            });
        });
    };

    /** Handle user search. */
    // const handleSearch = (keyword: string) => {
    //     const trimmedKeyword = keyword.trim();

    //     setSearchKeyword(trimmedKeyword);
    //     // Always reset current page to 1 before calling fetch
    //     const updatedPage = 1;
    //     setCurrentPage(updatedPage);


    //     const params = {
    //         assetFolderId: selectedFolderId,
    //         currentUserId, clientId,
    //         listRange: { pageIndex: updatedPage, pageSize, sortExpression: 'LastModifiedDate desc' },
    //         keyWord: trimmedKeyword,
    //     };
    //     dispatch(fetchAssetFilesListSlice(params));
    // };

    const debouncedSearch = useMemo(
        () =>
            debounce((keyword: string) => {
                const trimmedKeyword = keyword.trim();
                setSearchKeyword(trimmedKeyword);

                // Always reset current page to 1 before calling fetch
                const updatedPage = 1;
                setCurrentPage(updatedPage);

                const params = {
                    assetFolderId: selectedFolderId,
                    currentUserId,
                    clientId,
                    listRange: {
                        pageIndex: updatedPage,
                        pageSize,
                        sortExpression: "LastModifiedDate desc",
                    },
                    keyWord: trimmedKeyword,
                };

                dispatch(fetchAssetFilesListSlice(params));
            }, 500),
        [dispatch, selectedFolderId, currentUserId, clientId, pageSize]
    );

    //  New handleSearch function
    const handleSearch = useCallback(
        (keyword: string) => {
            debouncedSearch(keyword);
        },
        [debouncedSearch]
    );

    //  Cleanup debounce on unmount to prevent memory leaks
    useEffect(() => {
        return () => {
            debouncedSearch.cancel();
        };
    }, [debouncedSearch]);


    /** Handle pagination changes. */
    const handlePageChange = (page: number, newPageSize?: number) => {
        if (newPageSize) setPageSize(newPageSize); // Update page size if provided
        setCurrentPage(page); // Update the current page state
        setSelectedAssetFiles([]);
        const params = {
            assetFolderId: selectedFolderId,
            currentUserId,
            clientId: clientId,
            listRange: {
                pageIndex: page,
                pageSize: newPageSize || pageSize,
                sortExpression: 'LastModifiedDate desc',
            },
            keyWord: searchKeyword
        };
        dispatch(fetchAssetFilesListSlice(params));
    };

    /** Refresh asset file list. */
    const refreshAssetFilesList = (overridePageIndex?: number) => {
        const params = {
            assetFolderId: selectedFolderId,
            currentUserId, clientId,
            listRange: {
                pageIndex: overridePageIndex ?? currentPage,  // Use overridePageIndex if provided
                pageSize,
                sortExpression: 'LastModifiedDate desc',
            },
            keyWord: ""
        };
        dispatch(fetchAssetFilesListSlice(params));
    };

    /** Handle individual asset file selection via checkbox.*/
    const handleCheckboxChange = (assetFileId: string) => {
        setSelectedAssetFiles([]);
        setSelectedAssetFiles([assetFileId]);
    };

    /** Handle selecting/deselecting all users. */
    const handleSelectAll = (selectAll: boolean) => {
        if (selectAll) {
            const allAssetFileIds = assetFiles.map((data) => data.id);
            setSelectedAssetFiles(allAssetFileIds);
        } else {
            setSelectedAssetFiles([]);
        }
    };

    // Breadcrumb navigation items
    const breadcrumbItems = [
        { iconClass: 'fa-classic fa-solid fa-house', path: AdminPageRoutes.ADMIN_DASHBOARD.FULL_PATH },
        { label: 'Manage Asset Library' },
    ];
    const pageTitle: string = 'Manage Asset Library';
    document.title = pageTitle;

    // START Add Edit Delete Asset Folder
    const [formData, setFormData] = useState({
        id: '',
        assetFolderName: '',
        assetFolderDescription: '',
    });
    const handleReset = () => {
        setFormData({
            id: '',
            assetFolderName: '',
            assetFolderDescription: '',
        });
    };

    const [formErrors, setFormErrors] = useState<Record<string, string>>({});
    const [formSuccess, setFormSuccess] = useState<Record<string, string>>({});
    const validateField = (name: string, value: string | null): string => {
        if (typeof value === 'string') {
            if (value.trim() && !isValidInput(value)) {
                return "Special characters like < > ' & are not allowed.";
            }
        }

        switch (name) {
            case "parentFolderId":
                if (typeof value === "string" && !value.trim())
                    return "Please select any folder from the Asset Folder list";
                break;
            case "assetFolderName":
                if (typeof value === "string" && !value.trim())
                    return "Asset folder name is required";
                break;
            default:
                break;
        }
        return ""; // No error
    };

    const isValidInput = (value: string): boolean => {
        const regx = /^[^'<>&]+$/; // Used in YP
        return regx.test(value);
    };

    // Validate all fields function
    const validateAllFields = (): Record<string, string> => {
        const errors: Record<string, string> = {};

        Object.keys(formData).forEach((key) => {
            const error = validateField(key, (formData as any)[key]);
            if (error) errors[key] = error;
        });
        return errors;
    };
    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const target = e.target as HTMLInputElement;
        const name = target.name;
        const value = target.value;

        let newValue: any = value;

        setFormData({
            ...formData,
            [name]: newValue,
        });

        // Validate the field
        const error = validateField(name, value);

        // Update form errors
        setFormErrors({
            ...formErrors,
            [name]: error,
        });
    };

    // START Open Add Edit Folder Modal    
    const ypModalAddEditAssetFolderRef = useRef<HTMLDivElement | null>(null);
    const ypModalAddEditAssetFolderBtnClose = useRef<HTMLButtonElement | null>(null);

    // programmatically open the modal
    const modalElement = ypModalAddEditAssetFolderRef.current;
    let finalParentFolderId: any = "";
    const openAddAssetFolderModal = () => {
        isAddForm.current = true;

        //  Allow adding folder even if "Assets" is selected or root
         const parentId = "A00000";
         const parentName = "Assets";
        // const parentId = selectedFolderId || "A00000"; 
        // const parentName = selectedFolderName || "Assets";

        // if (selectedFolderId) {
        if (modalElement) {
            const modal = new bootstrap.Modal(modalElement);
            modal.show();

            setFormData({
                id: "",
                assetFolderName: "",
                assetFolderDescription: "",
            });
            finalParentFolderId = parentId;
            setSelectedFolderId(parentId);
            setSelectedFolderName(parentName);
            setSelectedFolderParentId("");
            setSelectedFolderDescription("");
            // finalParentFolderId = selectedFolderId;
            // }
        }
        // else {
        //     setAlert({ type: "warning", message: "Please Select folder first" });
        // }
    };
    const openEditAssetFolderModal = () => {
        isAddForm.current = false;
        if (selectedFolderId) {
            if (modalElement) {
                const modal = new bootstrap.Modal(modalElement);
                modal.show();

                setFormData({
                    id: selectedFolderId,
                    assetFolderName: selectedFolderName || "",
                    assetFolderDescription: selectedFolderDescription || "",
                });
                finalParentFolderId = selectedFolderParentId;
            }
        }
        else {
            setAlert({ type: "warning", message: "Please Select folder first" });
        }
    };

    // Function to clear the Add Asset Folder form data
    const clearAssetFolderData = () => {

    };
    // END Open Add Edit Folder Modal

    const getAddEditAssetFolderPayload = (): IAssetLibraryAddEditAssetFolderPayload => {
        return {
            id: formData.id,
            clientId: clientId,
            currentUserId: currentUserId,
            assetFolderName: formData.assetFolderName,
            parentFolderId: isAddForm.current ? selectedFolderId : selectedFolderParentId,
            assetFolderDescription: formData.assetFolderDescription,
        };
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        // Validate all fields
        let hasErrors = false;
        const errors = validateAllFields();
        if (Object.keys(errors).length > 0) {
            setFormErrors(errors);
            hasErrors = true;
        }
        if (hasErrors) {
            return;
        }

        const assetFolderPayLoad: IAssetLibraryAddEditAssetFolderPayload = getAddEditAssetFolderPayload();
        if (isAddForm.current) {
            dispatch(addAssetFolderSlice(assetFolderPayLoad))
                .then((response: any) => {
                    if (response.meta.requestStatus == "fulfilled") {
                        setModalAlertMessageConfig({
                            type: 'success',
                            message: "Asset folder added successfully."
                        });

                        setTimeout(() => {
                            if (ypModalAddEditAssetFolderBtnClose.current) {
                                ypModalAddEditAssetFolderBtnClose.current.click();
                            }
                            navigate(`/redirect?to=${encodeURIComponent(AdminPageRoutes.ADMIN_CONTENT_MANAGE_ASSET_LIBRARY.FULL_PATH)}`);
                        }, modalAlertMessageConfig.duration ? parseInt(modalAlertMessageConfig.duration) : 4000);
                    } else {
                        setModalAlertMessageConfig({
                            type: 'error',
                            message: 'Add asset folder failed: ' + response.payload.msg
                        });
                    }
                })
                .catch((error) => {
                    console.error("Add asset folder error:", error);
                    setModalAlertMessageConfig({
                        type: 'error',
                        message: 'An unexpected error occurred.'
                    });
                });
        } else {
            dispatch(editAssetFolderSlice(assetFolderPayLoad))
                .then((response: any) => {
                    if (response.meta.requestStatus == "fulfilled") {
                        setModalAlertMessageConfig({
                            type: 'success',
                            message: "Asset folder updated successfully."
                        });

                        setTimeout(() => {
                            if (ypModalAddEditAssetFolderBtnClose.current) {
                                ypModalAddEditAssetFolderBtnClose.current.click();
                            }
                            navigate(`/redirect?to=${encodeURIComponent(AdminPageRoutes.ADMIN_CONTENT_MANAGE_ASSET_LIBRARY.FULL_PATH)}`);
                        }, modalAlertMessageConfig.duration ? parseInt(modalAlertMessageConfig.duration) : 4000);
                    } else {
                        setModalAlertMessageConfig({
                            type: 'error',
                            message: 'Update asset folder failed: ' + response.payload.msg
                        });
                    }
                })
                .catch((error) => {
                    console.error("Update Asset folder error:", error);
                    setModalAlertMessageConfig({
                        type: 'error',
                        message: 'An unexpected error occurred.'
                    });
                });
        }
    };

    // Handle asset folder Delete
    const handleDeleteAssetFolder = () => {
        if (!selectedFolderId) {
            setAlert({ type: "warning", message: "Please Select folder first", autoClose: true });
            return;
        }
        else {
            const confirmDelete = () => {
                const assetFolderToDelete: IDeletePayload = {
                    id: selectedFolderId,
                    clientId: clientId,
                    lastModifiedById: currentUserId
                };

                onDelete(assetFolderToDelete);
            };
            setModalConfig({
                title: 'Delete Asset Folder Confirmation',
                message: 'Are you sure you want to delete the selected asset folder?',
                primaryBtnText: 'Delete',
            });
            setOnConfirmAction(() => confirmDelete);
            setShowModal(true);
        }
    }
    const onDelete = (assetFolderToDelete: IDeletePayload) => {
        dispatch(deleteAssetFolderSlice(assetFolderToDelete)).unwrap()
            .then((response) => {
                if (response?.msg) {
                    setModalAlertMessageConfig({
                        type: 'success',
                        message: response.msg
                    });

                    setTimeout(() => {
                        navigate(`/redirect?to=${encodeURIComponent(AdminPageRoutes.ADMIN_CONTENT_MANAGE_ASSET_LIBRARY.FULL_PATH)}`);
                    }, modalAlertMessageConfig.duration ? parseInt(modalAlertMessageConfig.duration) : 4000);
                }
            }).catch((error) => {
                console.error("Error deleting asset folder:", error);
                setModalAlertMessageConfig({
                    type: 'error',
                    message: 'Delete asset folder failed: ' + error?.msg
                });
            });
    };
    // END Add Edit Delete Asset Folder

    // START Tree Folder Data and its populated files API Calls and logic
    // Fetch the initial folders and sub folders and populate tree when the component mounts
    useEffect(() => {
        if (!currentUserId || !clientId) return;
        fetchAssetFoldersList();
    }, [currentUserId, clientId]);

    const fetchAssetFoldersList = () => {
        const params = {
            clientId: clientId, currentUserId: currentUserId
        };
        dispatch(fetchAssetFoldersSlice(params))
            .then((response: any) => {
                if (response.meta.requestStatus === 'fulfilled') {
                    // bind data and populate folder tree structure                   
                    setFolderTreeData(response.payload.assetFolderTree);

                    //**Expand only root
                    if (response.payload.assetFolderTree.length > 0) {
                        setFolderExpandedMap({ [response.payload.assetFolderTree[0].unitId]: true });
                    }

                } else {
                    setAlert({ type: "error", message: 'Failed to fetch asset folder data: ' + response.payload?.msg || 'Unknown error.' });
                }
            })
            .catch((error) => {
                console.error('Error while fetching asset folder data:', error);
                setAlert({ type: "error", message: 'An unexpected error occurred.' });
            });
    };

    // Function to filter folder tree based on search query
    const filterTree = (nodes: FolderNode[], query: string): FolderNode[] => {
        if (!query) return nodes;

        let result: FolderNode[] = [];

        nodes.forEach((node) => {
            const isMatch = node.title.toLowerCase().includes(query.toLowerCase());

            // Recursively filter children
            const filteredChildren = filterTree(node.children, query);

            if (isMatch) {
                // If this node matches, include it with all its original children
                result.push({ ...node });
            } else if (filteredChildren.length > 0) {
                // If children match, include THIS node (not rootNode!) with only the matching children
                result.push({ ...node, children: filteredChildren });
            }
        });

        return result;
    };
    const filteredTreeData = filterTree(folderTreeData, searchQueryForFolderTree);


    useEffect(() => {
        if (searchQueryForFolderTree.trim()) {
            const expandedIds = collectExpandedUnitIds(folderTreeData, searchQueryForFolderTree);

            setFolderExpandedMap((prev) => {
                const updated = { ...prev };
                expandedIds.forEach(id => {
                    updated[id] = true;
                });
                return updated;
            });
        }

        if (!searchQueryForFolderTree.trim()) {
            // Reset to root only
            if (folderTreeData.length > 0) {
                setFolderExpandedMap({ [folderTreeData[0].unitId]: true });
            }
        }
    }, [searchQueryForFolderTree, folderTreeData]);
    // END Tree Data API Calls and logic


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
        debouncedSetTreeListHeightFunction();
    }, [isSidebarCollapsed]);
    // END handleToggleTreeSidebar

    // Start Set tree structure scrollbar height 
    const treeSidebarMainWrapperRef = useRef<HTMLDivElement | null>(null);
    const treeSidebarSearchWrapperRef = useRef<HTMLDivElement | null>(null);
    const treeSidebarActionsWrapperRef = useRef<HTMLDivElement | null>(null);
    const treeSidebarListWrapperRef = useRef<HTMLDivElement | null>(null);
    const [treeListHeight, setTreeListHeight] = useState<number>(0);

    // Debounced version of setTreeListHeightFunction
    const debouncedSetTreeListHeightFunction = useCallback(
        debounce(() => {
            if (treeSidebarMainWrapperRef.current && treeSidebarSearchWrapperRef.current && treeSidebarActionsWrapperRef.current && treeSidebarListWrapperRef.current) {
                const listWrapperStyles = window.getComputedStyle(treeSidebarListWrapperRef.current);
                const padding = parseFloat(listWrapperStyles.paddingTop) + parseFloat(listWrapperStyles.paddingBottom);

                const finalScrollHeight = treeSidebarMainWrapperRef.current?.getBoundingClientRect().height
                    - (treeSidebarSearchWrapperRef.current?.getBoundingClientRect().height
                        + treeSidebarActionsWrapperRef.current?.getBoundingClientRect().height
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


    /* Navigate to Add User page. */
    const handleAddAsset = () => {
        if (!selectedFolderId) {
            setAlert({ type: "warning", message: "Please Select folder first" });
            return;
        }
        navigate(`${AdminPageRoutes.ADMIN_CONTENT_MANAGE_ASSET_LIBRARY.FULL_PATH}/contentAddAsset/${selectedFolderId}`);
    };

    return (
        <>
            {/* Loading spinner overlay */}
            {loading && <div className="yp-loading-overlay"><div className="yp-spinner"><div className="spinner-border"></div></div></div>}

            <div className="yp-page-title-button-section" id="yp-page-title-breadcrumb-section">
                <div className="yp-page-title-breadcrumb">
                    <div className="yp-page-title">{pageTitle}</div>
                    <CustomBreadcrumb items={breadcrumbItems} />
                </div>
                <div className="yp-page-button">
                    <button className="btn btn-primary" onClick={handleAddAsset}>
                        <i className="fa fa-plus"></i>
                        Add New Asset
                    </button>
                </div>
            </div>

            <div className="yp-card p-0" id="yp-card-main-content-section" style={{ minHeight: '100%' }}>
                <div className="yp-tree-sidebar-table-wrapper" id="yp-tree-sidebar-table-wrapper" ref={treeSidebarTableRef}>
                    <div className="yp-tree-main-ul-sidebar" ref={treeSidebarMainWrapperRef}>
                        <div className="yp-tree-mus-toggle-button"
                            onClick={handleToggleTreeSidebar}
                            title="Show/Hide Asset Sidebar">
                            <i className={`fa ${isSidebarCollapsed ? 'fa-folder-tree' : 'fa-folder-tree'}`}></i>
                        </div>
                        <div className="yp-tree-main-search-wrapper" ref={treeSidebarSearchWrapperRef}>
                            <div className="yp-form-control-with-icon">
                                <div className="form-group mb-0">
                                    <div className="yp-form-control-wrapper">
                                        <input type="text"
                                            name=""
                                            //placeholder="Search folder name here..."
                                            placeholder=""
                                            className="form-control yp-form-control"
                                            value={searchQueryForFolderTree}
                                            // onChange={(e) => setSearchQueryForFolderTree(e.target.value)}
                                            onChange={(e) => {
                                                const value = e.target.value;
                                                // Prevent setting value that starts with a space
                                                if (searchQueryForFolderTree === '' && value.startsWith(' ')) return;

                                                setSearchQueryForFolderTree(value);
                                            }}
                                            onKeyDown={(e) => {
                                                // Prevent typing space as first character
                                                if (e.key === ' ' && searchQueryForFolderTree.length === 0) {
                                                    e.preventDefault();
                                                }
                                            }}
                                        />
                                        <label className="form-label">Search folder name here...</label>
                                        <span className="yp-form-control-icon"><i className="fa fa-search"></i></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="yp-tree-main-actions-wrapper" ref={treeSidebarActionsWrapperRef}>
                            <div className="yp-text-12-500 yp-color-565656 mb-2">Asset Folder Actions:</div>
                            <div className="d-flex flex-wrap gap-2">
                                <button type="button" className="btn btn-primary btn-sm" onClick={openAddAssetFolderModal}>Add</button>
                                <button type="button" className="btn btn-primary btn-sm" onClick={openEditAssetFolderModal}>Edit</button>
                                <button type="button" className="btn btn-danger btn-sm" onClick={handleDeleteAssetFolder}>Delete</button>
                            </div>
                        </div>
                        <div className="yp-tree-main-ul-wrapper" ref={treeSidebarListWrapperRef}>


                            <ul className="yp-tree-main-ul" style={{ 'maxHeight': treeListHeight + 'px' }}>
                                {filteredTreeData.length > 0 ? (
                                    filteredTreeData.map((folder) => (
                                        <FolderNodeItem
                                            key={folder.unitId}
                                            node={folder}
                                            selectedFolderId={selectedFolderId}
                                            setSelectedFolderId={setSelectedFolderId}
                                            selectedFolderName={selectedFolderName}
                                            setSelectedFolderName={setSelectedFolderName}
                                            selectedFolderParentId={selectedFolderParentId}
                                            setSelectedFolderParentId={setSelectedFolderParentId}
                                            selectedFolderDescription={selectedFolderDescription}
                                            setSelectedFolderDescription={setSelectedFolderDescription}
                                            folderExpandedMap={folderExpandedMap}
                                            setFolderExpandedMap={setFolderExpandedMap}
                                        />
                                    ))
                                ) : (
                                    <li>No matching folders found</li>
                                )}
                            </ul>
                        </div>
                    </div>


                    {/* Common list here */}
                    <div className="yp-custom-table-section yp-custom-table-section-inside-card">
                        {
                            selectedFolderId ?
                                <CommonTable
                                    type={COMMON_TABLE_TYPE.MANAGE_ASSET_LIBRARY}
                                    tableConfig={MANAGE_ASSET_LIBRARY_TABLE_CONFIG}
                                    data={assetFiles}
                                    onSearch={handleSearch}
                                    onPageChange={handlePageChange}
                                    currentPage={currentPage}
                                    totalRecords={assetFilesList?.totalRows}
                                    pageSize={pageSize}
                                    selectedUsers={selectedAssetFiles}
                                    handleCheckboxChange={handleCheckboxChange}
                                    handleSelectAll={handleSelectAll}
                                    actionButtonList={actionButtonList}
                                    actionButtonClick={handleActionButtonClick}
                                />
                                :
                                modalAlertMessageConfig.type && modalAlertMessageConfig.message &&
                                <AlertMessageInsideModal type={modalAlertMessageConfig.type} message={modalAlertMessageConfig.message} duration={modalAlertMessageConfig.duration} autoClose={modalAlertMessageConfig.autoClose} />
                        }

                    </div>
                </div>
            </div>


            <div className="modal fade yp-modal yp-modal-right-side"
                id="yp-modal-addEditAssetFolder"
                data-bs-backdrop="static"
                data-bs-keyboard="false"
                tabIndex={-1}
                aria-labelledby="yp-modal-addEditAssetFolder"
                ref={ypModalAddEditAssetFolderRef}>
                <div className="modal-dialog modal-md modal-dialog-scrollable modal-fullscreen-sm-down">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h1 className="modal-title" id="staticBackdropLabel">{addEditFolderModalTitle}</h1>
                            <button type="button"
                                className="btn-close"
                                data-bs-dismiss="modal"
                                aria-label="Close"
                                onClick={clearAssetFolderData}
                                ref={ypModalAddEditAssetFolderBtnClose}></button>
                        </div>
                        <div className="modal-body">
                            <div className="yp-modal-sub-heading">
                                Destination Folder : <strong>{selectedFolderName}</strong>
                            </div>
                            {!modalAlertMessageConfig.message &&
                                <form>
                                    <div className="row">
                                        <div className="col-12 col-md-12 col-lg-12">
                                            <div className="form-group">
                                                <div className="yp-form-control-wrapper">
                                                    <input type="text"
                                                        className={`form-control yp-form-control ${formErrors.assetFolderName ? 'is-invalid' : ''}`}
                                                        name="assetFolderName"
                                                        value={formData.assetFolderName}
                                                        // onChange={handleInputChange}
                                                        onChange={(e) => {
                                                            const value = e.target.value;
                                                            // Prevent leading space
                                                            if (formData.assetFolderName === '' && value.startsWith(' ')) return;
                                                            handleInputChange(e);
                                                        }}
                                                        onKeyDown={(e) => {
                                                            // Prevent space as first character
                                                            if (e.key === ' ' && formData.assetFolderName.length === 0) { e.preventDefault(); }
                                                        }}
                                                        id=""
                                                        placeholder="" />
                                                    <label className="form-label"><span className='yp-asteric'>*</span>Folder Name</label>
                                                    <input type="hidden" name="id" value={formData.id} />
                                                </div>
                                                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                                    {formErrors.assetFolderName && <div className="invalid-feedback px-3">{formErrors.assetFolderName}</div>}
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="row">
                                        <div className="col-12 col-md-12 col-lg-12">
                                            <div className="form-group">
                                                <div className="yp-form-control-wrapper">
                                                    <textarea
                                                        className={`form-control yp-form-control`}
                                                        name="assetFolderDescription"
                                                        value={formData.assetFolderDescription}
                                                        onChange={handleInputChange}
                                                        onKeyDown={(e) => {
                                                            // Prevent leading space
                                                            if (e.key === ' ' && e.currentTarget.value.length === 0) {
                                                                e.preventDefault();
                                                            }
                                                        }}
                                                        id=""
                                                        maxLength={256}
                                                        placeholder=""></textarea>
                                                    <label className="form-label">Description</label>
                                                </div>
                                                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                                    {formErrors.assetFolderDescription && <div className="invalid-feedback px-3">{formErrors.assetFolderDescription}</div>}
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="row">
                                        <div className="col-12">
                                            <div className="yp-form-button-row">
                                                <button type="button" className="btn btn-primary" onClick={handleSubmit}>Save</button>
                                                <button type="reset" className="btn btn-secondary" onClick={clearAssetFolderData} data-bs-dismiss="modal">Cancel</button>
                                            </div>
                                        </div>
                                    </div>
                                </form>
                            }

                            {modalAlertMessageConfig.type && modalAlertMessageConfig.message &&
                                <AlertMessageInsideModal type={modalAlertMessageConfig.type} message={modalAlertMessageConfig.message} duration={modalAlertMessageConfig.duration} autoClose={modalAlertMessageConfig.autoClose} />
                            }
                        </div>
                    </div>
                </div>
            </div>

            {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}

            {/* Confirmation Modal */}
            <ConfirmationModal
                show={showModal}
                onConfirm={onConfirmAction}
                onCancel={() => {
                    setShowModal(false);
                    setModalAlertMessageConfig({ type: 'success', message: '', duration: '4000', autoClose: true });
                }
                }
                modalConfig={modalConfig}
                modalAlertMessageConfig={modalAlertMessageConfig}
            />
        </>
    )
};

const FolderNodeItem: React.FC<{
    node: FolderNode;
    selectedFolderId: string | null;
    setSelectedFolderId: React.Dispatch<React.SetStateAction<string | null>>;
    selectedFolderName: string | null;
    setSelectedFolderName: React.Dispatch<React.SetStateAction<string | null>>;
    selectedFolderParentId: string | null;
    setSelectedFolderParentId: React.Dispatch<React.SetStateAction<string | null>>;
    selectedFolderDescription: string | null;
    setSelectedFolderDescription: React.Dispatch<React.SetStateAction<string | null>>;
    folderExpandedMap: { [key: string]: boolean };
    setFolderExpandedMap: React.Dispatch<React.SetStateAction<{ [key: string]: boolean }>>;
}> = ({ node, selectedFolderId, setSelectedFolderId, selectedFolderName, setSelectedFolderName, selectedFolderParentId, setSelectedFolderParentId, selectedFolderDescription, setSelectedFolderDescription, folderExpandedMap, setFolderExpandedMap }) => {

    const isFolderSelected = selectedFolderId === node.unitId;
    const folderExpanded = folderExpandedMap[node.unitId] || false;

    const toggleExpanded = () => {
        setFolderExpandedMap((prev) => ({
            ...prev,
            [node.unitId]: !folderExpanded
        }));
    };

    const handleFolderSelectClick = (id: string, title: string, parentId: string, description: string) => {
        // Disable click for the root "Assets" folder
        if (title === "Assets" || id === "A00000") return;

        if (selectedFolderId === id) {
            setSelectedFolderId(null);
            setSelectedFolderName(null);
            setSelectedFolderParentId(null);
            setSelectedFolderDescription(null);
        } else {
            setSelectedFolderId(id);
            setSelectedFolderName(title);

            setSelectedFolderParentId(parentId);
            setSelectedFolderDescription(description);


            setFolderExpandedMap((prev) => ({
                ...prev,
                [node.unitId]: true //** Expand on select
            }));
        }
    };

    return (
        <li className={isFolderSelected ? 'active' : ''}>
            <div className="flex items-center gap-2">
                {node.children.length > 0 ? (
                    <span className="yp-tree-plusMinus-icon" onClick={toggleExpanded}>
                        <i className={folderExpanded ? "zmdi zmdi-minus-circle-outline" : "zmdi zmdi-plus-circle-o"}></i>
                    </span>
                ) : (
                    <span className="yp-tree-plusMinus-icon yp-tree-plusMinus-icon-empty"></span>
                )}

                <span className="yp-tree-folder-icon">
                    <i className={isFolderSelected ? "fa-classic fa-solid fa-folder-open fa-fw" : "fa-classic fa-solid fa-folder fa-fw"}></i>
                </span>

                <span className="yp-tree-folderName" onClick={() => handleFolderSelectClick(node.unitId, node.title, node.parentId, node.folderDescription)}
                    style={node.title === "Assets" || node.unitId === "A00000" ? { pointerEvents: "none" } : {}}>
                    {node.title} <span className="yp-tree-folderName-count"></span>
                </span>
            </div>

            {folderExpanded && node.children.length > 0 && (
                <ul className="yp-tree-sub-main-ul">
                    {node.children.map((child) => (
                        <FolderNodeItem
                            key={child.unitId}
                            node={child}
                            selectedFolderId={selectedFolderId}
                            setSelectedFolderId={setSelectedFolderId}
                            selectedFolderName={selectedFolderName}
                            setSelectedFolderName={setSelectedFolderName}
                            selectedFolderParentId={selectedFolderParentId}
                            setSelectedFolderParentId={setSelectedFolderParentId}
                            setSelectedFolderDescription={setSelectedFolderDescription}
                            selectedFolderDescription={selectedFolderDescription}
                            folderExpandedMap={folderExpandedMap}
                            setFolderExpandedMap={setFolderExpandedMap}
                        />
                    ))}
                </ul>
            )}
        </li>
    );
};

const collectExpandedUnitIds = (nodes: FolderNode[], query: string, ancestors: string[] = []): string[] => {
    let expandedIds: string[] = [];

    nodes.forEach((node) => {
        const isMatch = node.title.toLowerCase().includes(query.toLowerCase());
        const currentPath = [...ancestors, node.unitId];

        if (isMatch) {
            // Expand all ancestors (exclude self)
            expandedIds.push(...ancestors);
        }

        const childExpandedIds = collectExpandedUnitIds(node.children, query, currentPath);
        expandedIds.push(...childExpandedIds);
    });

    return Array.from(new Set(expandedIds)); // remove duplicates
};

export default ManageAssetLibrary;