import React, { useCallback, useEffect, useMemo, useState } from 'react';
import { debounce } from 'lodash';
import { IDeleteLearningPathPayload, LearningPath, User } from '../../../Types/commonTableTypes';
import { useAppDispatch, useAppSelector } from '../../../hooks';
import { useSelector } from 'react-redux';
import { RootState } from '../../../redux/store';
import { useNavigate } from 'react-router-dom';
import { AdminPageRoutes } from '../../../utils/Constants/Admin_PageRoutes';
import { ACTION_BUTTON_ENUM, COMMON_TABLE_TYPE } from '../../../utils/Constants/Enums';
import CommonTable from '../../../components/shared/CommonComponents/CommonTable';
import ConfirmationModal from '../../../components/shared/ConfirmationModal';
import CustomBreadcrumb from '../../../components/shared/CustomBreadcrumb';
import AlertMessage from '../../../components/shared/AlertMessage';
import ManageLearningPathFilter from './LearningPathFilter';
import { activateSelectedLearningPathSlice, copyLearningPath, deactivateSelectedLearningPathSlice, deleteSelectedLearningPathSlice, fetchLearningPath } from '../../../redux/Slice/admin/learningPathSlice';
import { MANAGE_ADMIN_LEARNING_PATH_TABLE_CONFIG } from '../../../utils/Constants/tableConfig';


const ManageLearningPath: React.FC = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const clientId: any = useSelector((state: RootState) => state.auth.clientId);
  const id: any = useSelector((state: RootState) => state.auth.id);
  const learningPathState: any = useSelector((state: RootState) => state.learningPath);
  const { loading } = useAppSelector((state: RootState) => state.learningPath);

  // Local states
  const [learningPaths, setLearningPath] = useState<LearningPath[]>([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [selectedLearningPaths, setSelectedLearningPaths] = useState<string[]>([]);
  const [showModal, setShowModal] = useState(false);
  const [searchMode, setSearchMode] = useState<'keyword' | 'filter' | null>(null);
  const [isFilterVisible, setFilterVisible] = useState(false);
  // Action buttons configuration
  const actionButtonList = [
    { type: "button", className: "btn btn-primary", buttonName: "Activate", actionType: ACTION_BUTTON_ENUM?.ACTIVATE, disabled: learningPaths.length === 0 },
    { type: "button", className: "btn btn-secondary", buttonName: "Deactivate", actionType: ACTION_BUTTON_ENUM?.DEACTIVATE, disabled: learningPaths.length === 0 }
  ];

  const [modalConfig, setModalConfig] = useState({
    title: '',
    message: '',
    primaryBtnText: '',
  });
  const [modalAlertMessageConfig, setModalAlertMessageConfig] = useState<{
    type: "success" | "error";
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
  const [appliedFilters, setAppliedFilters] = useState<any>(null);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [searchKeyword, setSearchKeyword] = useState("");


  const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);

  /** Breadcrumb navigation items*/
  const breadcrumbItems = [
    { iconClass: 'fa-classic fa-solid fa-house', path: AdminPageRoutes.ADMIN_DASHBOARD.FULL_PATH },
    { label: 'Manage Learning Paths' },
  ];
  const pageTitle: string = 'Manage Learning Path';
  document.title = pageTitle;

  /** Fetch user list with pagination and filters. */
  const fetchLearningPathList = (pageIndex: number, pageSize: number, filters: any) => {
    const params = {
      clientId,
      createdByID: null,
      listRange: {
        pageIndex, pageSize, sortexpression: 'LastModifiedDate desc'
      },
      keyWord: "",
      // userCriteria: filters || undefined,
    };
    dispatch(fetchLearningPath(params));
  };

  /** Fetch the initial user list when the component mounts. */
  useEffect(() => {
    if (!id || !clientId) return;
    fetchLearningPathList(1, pageSize, appliedFilters);
  }, [id, clientId]);

  /** Update the local user state when userList changes. */
  useEffect(() => {
    setLearningPath(learningPathState?.learningPaths?.data || []);
  }, [learningPathState]);


  /** Fetch learningPaths fields details validations */
  // useEffect(() => {
  //   const params = {
  //     clientId, createdByID: "USR7cbc556be7f64ac8",
  //     listRange: { pageIndex: 0, pageSize: 0, sortexpression: 'MinLength' },
  //     keyWord: "",
  //   } as const;
  //   dispatch(fetchLearningPath(params));
  // }, [clientId]);



  /** Clear applied filters and reset the user list.  */
  const handleClearFilters = () => {
    setAppliedFilters(null); // Clear applied filters
    setCurrentPage(1); // Reset pagination
    setSearchMode(null); // ✅ Enable keyword search

    const params = {
      clientId,
      // keyWord: null,
      // createdByID: null,
      // LastModifiedById: null,
      // IsUsed: null,
      // IsActive: null,
      listRange: {
        pageIndex: 1,
        pageSize,
        sortexpression: 'LastModifiedDate desc',
      },

    };

    dispatch(fetchLearningPath(params)); // Refetch users with empty filters
  };

  /** Handle individual asset file selection via radio.*/
  const handleSelectChange = (learningPathId: string) => {
    setSelectedLearningPaths([]);
    setSelectedLearningPaths([learningPathId]);
  };

  /** Handle Activate button click.  */
  const handleActivate = () => {
    if (selectedLearningPaths.length !== 1) {
      setAlert({ type: "warning", message: "Please select exactly one inactive record." });
      return;
    }

    const selectedId = selectedLearningPaths[0];
    const selectedPath = learningPaths.find(lp => lp.id === selectedId);

    if (!selectedPath) {
      setAlert({ type: "error", message: "Learning path not found." });
      return;
    }

    if (selectedPath.isActive) {
      setAlert({ type: "warning", message: "Selected learning path is already active." });
      return;
    }

    const confirmActivate = () => {
      const payload = {
        id: selectedId,
        clientId: clientId,
        userId: id,
      };
      onActivate(payload);
    };

    const onActivate = (payload: { id: string; clientId: string; userId: string }) => {
      setIsSubmitting(true);
      dispatch(activateSelectedLearningPathSlice(payload)) // ✅ Pass single object
        .unwrap()
        .then((response) => {
          setModalAlertMessageConfig({
            type: "success",
            message: response?.msg || "Learning path activated successfully!",
          });
          setSelectedLearningPaths([]);
          setCurrentPage(1);
          refreshLearningPathList(1);
        })
        .catch((error) => {
          setModalAlertMessageConfig({
            type: "error",
            message: error?.msg || "Failed to activate learning path.",
          });
        })
        .finally(() => {
          setIsSubmitting(false);
        });
    };

    setModalConfig({
      title: "Activate Learning Path",
      message: "Are you sure you want to activate this learning path?",
      primaryBtnText: "Activate",
    });
    setOnConfirmAction(() => confirmActivate);
    setShowModal(true);
  };

  /** Handle Deactivate button click.  */
  const handleDeactivate = () => {
    if (selectedLearningPaths.length !== 1) {
      setAlert({ type: "warning", message: "Please select exactly one record." });
      return;
    }

    const selectedId = selectedLearningPaths[0];
    const selectedPath = learningPaths.find(lp => lp.id === selectedId);

    if (!selectedPath) {
      setAlert({ type: "error", message: "Learning path not found." });
      return;
    }

    if (!selectedPath.isActive) {
      setAlert({ type: "warning", message: "Selected learning path is already inactive." });
      return;
    }

    const confirmDeactivate = () => {
      const payload = {
        id: selectedId,        // Pass selected learning path ID
        clientId,             //  Pass clientId
        userId: id            //  Pass userId
      };
      onDeactivate(payload);
    };

    setModalConfig({
      title: "Deactivate Learning Path",
      message: "Are you sure you want to deactivate this learning path?",
      primaryBtnText: "Deactivate",
    });
    setOnConfirmAction(() => confirmDeactivate);
    setShowModal(true);
  };

  /** Deactivate selected users. */
  const onDeactivate = (payload: { id: string; clientId: string; userId: string }) => {
    setIsSubmitting(true);
    dispatch(deactivateSelectedLearningPathSlice(payload))
      .unwrap()
      .then((response) => {
        setModalAlertMessageConfig({
          type: "success",
          message: response?.msg || "Learning path deactivated successfully!",
        });
        setSelectedLearningPaths([]);
        setCurrentPage(1);
        refreshLearningPathList(1);
      })
      .catch((error) => {
        setModalAlertMessageConfig({
          type: "error",
          message: error?.msg || "Failed to deactivate learning path.",
        });
      })
      .finally(() => {
        setIsSubmitting(false);
      });
  };

  // Copy Learning Path function
  const handleCopy = (learningPathToCopy: string) => {

    const confirmCopy = () => {
      const params = {
        id: learningPathToCopy,
        clientId: clientId,
        userId: id,
      };
      onCopy(params);
    };

    setModalConfig({
      title: "Copy Learning Path Confirmation",
      message: "Are you sure you want to copy the selected learning path(s)?",
      primaryBtnText: "Copy",
    });

    setOnConfirmAction(() => confirmCopy);
    setShowModal(true);
  };

  // Dispatch the copy action
  const onCopy = (params: { id: string; clientId: string; userId: string }) => {
    dispatch(copyLearningPath(params))
      .unwrap()
      .then((response) => {
        if (response?.msg) {
          setModalAlertMessageConfig({
            type: "success",
            message: response.msg,
          });
        }
        fetchLearningPathList(1, pageSize, appliedFilters);
        setCurrentPage(1);

      })
      .catch((error) => {
        console.error("Error copying learning path:", error);
        setModalAlertMessageConfig({
          type: "error",
          message: error?.msg || "Failed to copy learning path",
        });
      });
  };

  /** Handle Delete button click.  */
  const handleDelete = (learningPathToDelete: string) => {
    const confirmDelete = () => {
      // Find the matching learning path from state to get inUse value
      const matchedPath = learningPathState?.learningPaths?.data?.find(
        (lp: any) => lp.id === learningPathToDelete
      );

      const params: IDeleteLearningPathPayload = {
        id: learningPathToDelete,
        clientId: clientId,
        userId: id,
        inUse: matchedPath?.isUsedT
      };
      onDelete(params);
    };

    setModalConfig({
      title: "Delete Learning Path Confirmation",
      message: "Are you sure you want to delete the selected Learning Path?",
      primaryBtnText: "Delete",
    });

    setOnConfirmAction(() => confirmDelete);
    setShowModal(true);
  };

  // Dispatch the delete action
  const onDelete = (params: IDeleteLearningPathPayload) => {
    setIsSubmitting(true);
    dispatch(deleteSelectedLearningPathSlice(params))  // Pass single object now
      .unwrap()
      .then((response) => {
        if (response?.msg) {
          setModalAlertMessageConfig({
            type: "success",
            message: response.msg
          });
        }
        fetchLearningPathList(1, pageSize, appliedFilters);
        setCurrentPage(1);
      })
      .catch((error) => {
        console.error("Error deleting learning path:", error);
        setModalAlertMessageConfig({
          type: "error",
          message: error?.msg || "Failed to delete learning path"
        });
      })
      .finally(() => {
        setIsSubmitting(false);
      });
  };


  /** Handle learningPaths action button clicks (Activate/Deactivate/Delete). */
  const handleActionButtonClick = (actionType: string) => {
    if (actionType === ACTION_BUTTON_ENUM?.ACTIVATE) {
      handleActivate()
    } else if (actionType === ACTION_BUTTON_ENUM?.DEACTIVATE) {
      handleDeactivate()
    }
  }

  /** Debounced search function */
  const debouncedSearch = useMemo(
    () =>
      debounce((keyword: string) => {
        const trimmedKeyword = keyword.trim();
        setSearchKeyword(trimmedKeyword);
        setSearchMode(trimmedKeyword ? "keyword" : null);

        const updatedPage = 1;
        setCurrentPage(updatedPage);

        const params = {
          clientId,
          createdByID: null,
          listRange: {
            pageIndex: updatedPage,
            pageSize,
            sortexpression: 'LastModifiedDate desc',
          },
          keyWord: trimmedKeyword,
        };

        dispatch(fetchLearningPath(params));
      }, 500),
    [dispatch, id, clientId, pageSize]
  );

  /** Handle search input change */
  const handleSearch = useCallback(
    (keyword: string) => {
      debouncedSearch(keyword);
    },
    [debouncedSearch]
  );

  /** Cleanup debounce on unmount */
  useEffect(() => {
    return () => {
      debouncedSearch.cancel();
    };
  }, [debouncedSearch]);


  /**Navigate to Add User page.   */
  const handleAddLearningPath = () => {
    navigate(AdminPageRoutes.ADMIN_CONTENT_ADD_LEARNING_PATH.FULL_PATH);
  };

  /** Handle pagination changes. */
  const handlePageChange = (page: number, newPageSize?: number) => {
    console.log("handlePageChange", handlePageChange)
    if (newPageSize) setPageSize(newPageSize); // Update page size if provided
    setCurrentPage(page); // Update the current page state
    setSelectedLearningPaths([]);//  Clear selected users when changing page
    const params = {
      clientId: clientId,
      keyWord: searchKeyword || appliedFilters?.curriculumName,
      createdByID: appliedFilters?.createdByName,
      LastModifiedById: appliedFilters?.modifiedByName,
      IsUsed: appliedFilters?.isUsedT ? appliedFilters.isUsedT.toLowerCase() === "yes" : undefined,
      IsActive: appliedFilters?.Active === "true" ? true : appliedFilters?.Active === "false" ? false : undefined,
      listRange: {
        pageIndex: page,
        pageSize: newPageSize || pageSize,
        sortexpression: 'LastModifiedDate desc',
      },
    };
    console.log("params", params)
    dispatch(fetchLearningPath(params));
  };

  /** Refresh learning path list. */
  const refreshLearningPathList = (overridePageIndex?: number) => {
    const params = {
      clientId,
      keyWord: appliedFilters?.curriculumName || null,
      createdByID: appliedFilters?.createdByName || null,
      LastModifiedById: appliedFilters?.modifiedByName || null,
      IsUsed: appliedFilters?.isUsedT ? appliedFilters.isUsedT.toLowerCase() === "yes" : undefined,
      IsActive: appliedFilters?.Active === "true" ? true : appliedFilters?.Active === "false" ? false : undefined,
      listRange: {
        pageIndex: overridePageIndex ?? currentPage,  // Use overridePageIndex if provided
        pageSize,
        sortexpression: 'LastModifiedDate desc',
      },
    };
    dispatch(fetchLearningPath(params));
  };

  /** Handle filter submission.*/
  const handleFilterSubmit = (filterData: any) => {
    console.log("filterData", filterData)
    if (!id || !clientId) return;
    setSearchMode('filter');
    setAppliedFilters(filterData);
    setCurrentPage(1);

    const params = {
      clientId,
      keyWord: filterData?.curriculumName || null,
      createdByID: filterData?.createdByName || null,
      LastModifiedById: filterData?.modifiedByName || null,
      IsUsed: filterData?.isUsedT ? filterData.isUsedT.toLowerCase() === "yes" : undefined,
      IsActive: filterData?.Active === "true" ? true : filterData?.Active === "false" ? false : undefined,

      listRange: {
        pageIndex: 1,
        pageSize: pageSize,
        sortexpression: 'LastModifiedDate desc',
      },
    } as const;

    console.log("params", params)


    dispatch(fetchLearningPath(params))
      .then((response: any) => {
        if (response.meta.requestStatus === 'fulfilled') {

          //     setLearningPath(response.payload.data || []);
          //     setAlert({ type: "success", message: response.payload?.msg });
          //   } else {
          //     setAlert({ type: "error", message: 'Failed to apply filter: ' + response.payload?.status || 'Unknown error.' });
          //   }
          // })
          // .catch((error) => {
          //   console.error('Error while applying filter:', error);
          //   setAlert({ type: "error", message: 'An unexpected error occurred.' });
          // });

          const records = response.payload?.data || [];

          setLearningPath(records);

          if (records.length > 0) {
            setAlert({ type: "success", message: response.payload?.msg || "Filter applied successfully!" });
          } else {
            setAlert({ type: "error", message: "No records found for the applied filter." });
          }
        } else {
          setAlert({
            type: "error",
            message: 'Failed to apply filter: ' + (response.payload?.status || 'Unknown error.')
          });
        }
      })
      .catch((error) => {
        console.error('Error while applying filter:', error);
        setAlert({
          type: "error", message: typeof error === "string"
            ? error
            : error?.msg || error?.message || "An unexpected error occurred."
        });
      });
  }

  const handleSelectChangeFilter = (selectedOption: any) => {
    if (selectedOption && selectedOption !== null) {
      // setSelectedCourse(selectedOption);
      // callApi(selectedOption.value, currentPage, pageSize);
    } else {
      // setSelectedCourse(null);
    }
    setAppliedFilters((prev: any) => ({
      ...prev,
      createdByName: selectedOption?.value || ""
    }));
  };


  return (
    <>
      {/* ✅ Conditionally render AlertMessage */}
      {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}


      {/* Loading spinner overlay */}
      {loading || isSubmitting ? (<div className="yp-loading-overlay"><div className="yp-spinner"><div className="spinner-border"></div></div></div>) : null}

      <div className="yp-page-title-button-section" id="yp-page-title-breadcrumb-section">
        <div className="yp-page-title-breadcrumb">
          <div className="yp-page-title">{pageTitle}</div>
          <CustomBreadcrumb items={breadcrumbItems} />
        </div>
        <div className="yp-page-button">
          <button className="btn btn-primary" onClick={handleAddLearningPath}>
            <i className="fa fa-plus"></i>
            Add New Learning Paths
          </button>
        </div>
      </div>


      {/* Common list here */}
      <div className="yp-card p-0 pb-3" id="yp-card-main-content-section">
        <div className="yp-custom-table-section">
          <CommonTable
            type={COMMON_TABLE_TYPE.MANAGE_ADMIN_LEARNING_PATH}
            tableConfig={MANAGE_ADMIN_LEARNING_PATH_TABLE_CONFIG}
            filterComponent={
              <ManageLearningPathFilter
                onSubmit={handleFilterSubmit}
                onClear={handleClearFilters}
                closeFilter={handleClearFilters}
                handleSelectChange={handleSelectChangeFilter} />}
            onFilterSubmit={handleFilterSubmit}
            data={learningPaths}
            onSearch={handleSearch}
            onPageChange={handlePageChange}
            currentPage={currentPage}
            totalRecords={learningPathState?.learningPaths?.totalRows || 0}
            pageSize={pageSize}
            selectedUsers={selectedLearningPaths}
            handleCheckboxChange={handleSelectChange}
            // handleSelectAll={handleSelectAll}
            actionButtonList={actionButtonList}
            actionButtonClick={handleActionButtonClick}
            searchMode={searchMode} //  Pass down
            setSearchMode={setSearchMode}
            isFilterVisible={isFilterVisible}
            setFilterVisible={setFilterVisible}
            actionDeleteClick={handleDelete}
            actionCopyClick={handleCopy}
          />
        </div>
      </div>

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
  );
};

export default ManageLearningPath;
