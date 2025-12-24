import React, { useEffect, useRef, useState, useCallback } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { AdminPageRoutes } from "../../../utils/Constants/Admin_PageRoutes";
import { IMainPayload } from '../../../Types/commonTableTypes';
import { useDropzone } from "react-dropzone";
import CustomBreadcrumb from "../../../components/shared/CustomBreadcrumb";
import { RootState } from "../../../redux/store";
import { useAppDispatch, useAppSelector } from '../../../hooks';
import { useSelector } from 'react-redux';
import { addAssetSlice, editAssetSlice, fetchAssetTypeSlice, fetchSingleAssetDataSlice, uploadAssetThumbnailSlice } from '../../../redux/Slice/admin/assetLibrarySlice';
import AlertMessage from '../../../components/shared/AlertMessage';
import FileUploadImage from '../../../assets/images/file_upload_icon.png';

interface AssetType {
    lookupText: string;
    lookupValue: string;
}

const AddAsset: React.FC = () => {
    const navigate = useNavigate();
    const dispatch = useAppDispatch();
    const loggedInUserId = useSelector((state: RootState) => state.auth.id);
    const clientId: any = useSelector((state: RootState) => state.auth.clientId);
    const { loading, error } = useAppSelector((state: RootState) => state.assetLibrary);
    const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);
    const { assetId } = useParams<{ assetId: string }>();
    const { assetFolderId } = useParams<{ assetFolderId: string }>();
    let isAddForm: boolean = assetId ? false : true;

    const [assetDescriptionCharCount, setAssetDescriptionCharCount] = useState(0);

    const breadcrumbItems = [
        { iconClass: 'fa-classic fa-solid fa-house', path: AdminPageRoutes.ADMIN_DASHBOARD.FULL_PATH },
        { label: 'Manage Asset Library', path: AdminPageRoutes.ADMIN_CONTENT_MANAGE_ASSET_LIBRARY.FULL_PATH },
        { label: isAddForm ? 'Add Asset' : 'Edit Asset' },
    ];
    const pageTitle: string = isAddForm ? 'Add Asset' : 'Edit Asset';
    document.title = pageTitle;

    const [assetTypeList, setAssetTypeList] = useState<AssetType[]>([]);
    const [selectedAssetType, setSelectedAssetType] = useState<string>('');
    const [selectedAssetFileName, setSelectedAssetFileName] = useState<string>('');
    const [uploadedAssetFilePath, setUploadedAssetFilePath] = useState<string>('');
    
    /* For Upload thumbnail */
    const fileUploadThumbnailRef = useRef<HTMLInputElement>(null);
    const [selectedAssetThumbnailUrlPath, setSelectedAssetThumbnailUrlPath] = useState<string>('');
    const [selectedAssetThumbnailUrl, setSelectedAssetThumbnailUrl] = useState<string>('');
    /* END For Upload thumbnail */

    const [formData, setFormData] = useState({
        id: '',
        assetName: '',
        assetDescription: '',
        assetType: '',
        assetFile: null as File | null,
        assetFileName: '',
        allowPrintCertificate: false,
        thumbnailImgRelativePath: '',
        allowAssetDownload:false
    });
    const handleReset = () => {
        setFormData({
            id: '',
            assetName: '',
            assetDescription: '',
            assetType: '',
            assetFile: null as File | null,
            assetFileName: '',
            allowPrintCertificate: false,
            thumbnailImgRelativePath: '',
            allowAssetDownload:false
        });

        setSelectedAssetType('');
        setSelectedAssetFileName('');
        setUploadedAssetFilePath('');
        setUploadedFiles([]);
        setAssetDescriptionCharCount(0);
        setSelectedAssetThumbnailUrl('');
        setSelectedAssetThumbnailUrlPath('');
    };

    useEffect(() => {
        if (assetId) {
            const assetSinglePayload: IMainPayload = {
                clientId: clientId,
                id: assetId
            }

            dispatch(fetchSingleAssetDataSlice(assetSinglePayload))
                .then((response: any) => {
                    if (response.meta.requestStatus == "fulfilled") {
                        setFormData({
                            id: response.payload.asset.id,
                            assetName: response.payload.asset.assetName,
                            assetDescription: response.payload.asset.assetDescription,
                            assetType: response.payload.asset.assetFileType,
                            assetFile: null as File | null,
                            assetFileName: response.payload.asset.assetFileName,
                            allowPrintCertificate: response.payload.asset.isPrintCertificate,
                            thumbnailImgRelativePath: response.payload.asset.thumbnailImgRelativePath,
                            allowAssetDownload: response.payload.asset.isDownload,
                        });
                        setSelectedAssetType(response.payload.asset.assetFileType);
                        setSelectedAssetFileName(response.payload.asset.assetFileName);
                        setAssetDescriptionCharCount(response.payload.asset.assetDescription.length);
                        setUploadedAssetFilePath(response.payload.asset.contentServerURL + response.payload.asset.relativePath);
                        setSelectedAssetThumbnailUrl(response.payload.asset.thumbnailImgRelativePath);
                        setSelectedAssetThumbnailUrlPath(response.payload.asset.contentServerURL + response.payload.asset.thumbnailImgRelativePath);
                    }
                    else {
                        setAlert({ type: "error", message: 'Asset data fetch failed: ' + response.msg });
                    }
                })
                .catch((error) => {
                    console.error("Asset data fetch error:", error);
                    setAlert({ type: "error", message: 'An unexpected error occurred.' });
                });
        }
    }, [assetId]);

    const [formErrors, setFormErrors] = useState<Record<string, string>>({});
    const [formSuccess, setFormSuccess] = useState<Record<string, string>>({});
    const validateField = (name: string, value: string | Array<string> | File | null): string => {
        setFormSuccess({});

        if (typeof value === 'string') {
            if (value.trim() && !isValidInput(value)) {
                return "Special characters like < > ' & are not allowed.";
            }
        }

        switch (name) {
            case "assetName":
                if (typeof value === "string" && !value.trim())
                    return "Asset name is required";
                break;
            case "assetType":
                if (typeof value === "string" && !value.trim())
                    return "Asset type is required";
                break;
            default:
                break;
        }
        return "";
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

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>) => {
        const target = e.target as HTMLInputElement;
        const name = target.name;
        const value = target.value;
        const type = target.type;
        const checked = (target as HTMLInputElement).checked;

        let newValue: any = value;
        if (type === 'number') {
            newValue = value ? Number(value) : "";
        } else if (type === 'checkbox') {
            newValue = checked;
        }

        if (name === "assetDescription") {
            setAssetDescriptionCharCount(value.length);
        }

        setFormData({
            ...formData,
            [name]: newValue,
        });

        // Validate the field
        const error = validateField(name, type === "checkbox" ? String(checked) : value);

        // Update form errors
        setFormErrors({
            ...formErrors,
            [name]: error,
        });
    };
    const getAssetPayload = (): any => {
        const frmData = new FormData();
        frmData.append("ID", formData.id);
        frmData.append("ClientId", clientId);
        frmData.append("CreatedById", loggedInUserId || '');
        frmData.append("LastModifiedById", loggedInUserId || '');
        frmData.append("AssetName", formData.assetName);
        frmData.append("AssetDescription", formData.assetDescription);
        frmData.append("AssetFileType", formData.assetType);
        frmData.append("AssetFolderId", assetFolderId || '');
        frmData.append("IsPrintCertificate", formData.allowPrintCertificate.toString());
        frmData.append("IsDownload", formData.allowAssetDownload.toString());
        frmData.append("ThumbnailImgRelativePath", selectedAssetThumbnailUrl);

        frmData.append("isEdit", (isAddForm ? 'false' : 'true'));

        if (hiddenInputRef.current && hiddenInputRef.current.files && hiddenInputRef.current.files.length > 0) {
            frmData.append("file", hiddenInputRef.current.files[0]);
        }
        frmData.append("AssetFileName", selectedAssetFileName);

        const payload: FormData = frmData;
        return payload;
    }

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        // Validate all fields
        let hasErrors = false;
        const errors = validateAllFields();
        if (Object.keys(errors).length > 0) {
            setFormErrors(errors);
            hasErrors = true;
        }

        if (selectedAssetFileName === '') {
            setFormErrors({
                ...errors,
                assetFile: 'Please upload asset file.',
            });
            hasErrors = true;
        }
        if (hasErrors) {
            return;
        }

        const assetPayload: FormData = getAssetPayload();
        if (isAddForm) {
            dispatch(addAssetSlice(assetPayload))
                .then((response: any) => {
                    if (response.meta.requestStatus == "fulfilled") {
                        console.log("response", response.payload.msg)
                        // setAlert({ type: "success", message: "Asset added successfully." });
                        setAlert({ type: "success", message: response.payload.msg });
                        setTimeout(() => {
                            navigate(AdminPageRoutes.ADMIN_CONTENT_MANAGE_ASSET_LIBRARY.FULL_PATH);
                        }, 2000);
                    } else {
                          console.log("error", error)
                        setAlert({ type: "error", message: 'Add asset failed: ' + response.msg });
                    }
                })
                .catch((error) => {
                    console.error("Add asset error:", error);
                    setAlert({ type: "error", message: 'An unexpected error occurred.' });
                });
        }
        else {
            dispatch(editAssetSlice(assetPayload))
                .then((response: any) => {
                    if (response.meta.requestStatus == "fulfilled") {
                        setAlert({ type: "success", message: "Asset updated successfully." });
                        setTimeout(() => {
                            navigate(AdminPageRoutes.ADMIN_CONTENT_MANAGE_ASSET_LIBRARY.FULL_PATH);
                        }, 2000);
                    } else {
                        setAlert({ type: "error", message: 'Update asset failed: ' + response.msg });
                    }
                })
                .catch((error) => {
                    console.error("Update asset error:", error);
                    setAlert({ type: "error", message: 'An unexpected error occurred.' });
                });
        }
    };

    const goBack = () => {
        navigate(-1); // -1 navigates to the previous page
    };

    /* Get asset type list */
    useEffect(() => {
        if (!clientId) return;
        dispatch(fetchAssetTypeSlice(clientId)).then((response: any) => {
            if (response.meta.requestStatus == "fulfilled") {
                setAssetTypeList(response.payload.assetTypeList);
            } else {
                setAlert({ type: "error", message: 'Asset type data fetch failed: ' + response.msg });
            }
        })
            .catch((error) => {
                console.error("Asset type data fetch error:", error);
                setAlert({ type: "error", message: 'An unexpected error occurred.' });
            });
    }, [clientId]);
    const handleAssetTypeChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        // clear previously uploaded file
        setSelectedAssetFileName('');
        setUploadedAssetFilePath('');
        setUploadedFiles([]);
        setFormData({
            ...formData,
            assetFileName: '',
        });

        handleInputChange(event);
        setSelectedAssetType(event.target.value);
    };
    const acceptFileConfig: Record<string, { [mime: string]: string[] }> = {
        PDF: {
            "application/pdf": [".pdf"],
        },
        WRD: {
            "application/msword": [".doc"],
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document": [".docx"],
        },
        PPT: {
            "application/vnd.ms-powerpoint": [".ppt"],
            "application/vnd.openxmlformats-officedocument.presentationml.presentation": [".pptx"],
        },
        IMG: {
            "image/jpeg": [".jpg", ".jpeg"],
            "image/bmp": [".bmp"],
            "image/gif": [".gif"],
        },
        AUDIO: {
            "audio/mpeg": [".mp3"],
            "audio/wav": [".wav"],
            "audio/x-ms-wma": [".wma"],
        },
        VIDEO: {
            "video/x-msvideo": [".avi"],
            "video/mpeg": [".mpeg"],
            "video/mp4": [".mp4"],
            "video/x-ms-wmv": [".wmv"],
            "video/x-flv": [".flv"],
        },
        XL: {
            "application/vnd.ms-excel": [".xls"],
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet": [".xlsx"],
        },
    };
    const selectedType = selectedAssetType ? acceptFileConfig[selectedAssetType] : null;
    const dropzoneAccept: { [mime: string]: string[] } = {};
    if (selectedType) {
        for (const [mime, extensions] of Object.entries(selectedType)) {
            dropzoneAccept[mime] = extensions;
        }
    }

    /* File Upload */
    const hiddenInputRef = useRef<HTMLInputElement | null>(null);
    const [uploadedFiles, setUploadedFiles] = useState<File[]>([]); // State to store accepted files   
    const [isFileUploadDisabled, setIsFileUploadDisabled] = useState(false);

    const { acceptedFiles, getRootProps, getInputProps } = useDropzone({
        noKeyboard: true,
        accept: selectedType ? dropzoneAccept : {},
        maxFiles: 1,
        disabled: isFileUploadDisabled,
        onDrop: (acceptedFiles, rejectedFiles) => {
            let uploadErrorMessages = "";

            if (hiddenInputRef.current) {
                const dataTransfer = new DataTransfer();
                acceptedFiles.forEach((v) => {
                    dataTransfer.items.add(v);
                });
                hiddenInputRef.current.files = dataTransfer.files;
            }

            if (selectedAssetType === '') {
                uploadErrorMessages = "Select asset type first.";
            }
            if (rejectedFiles.some(file => file.errors.some(error => error.code === "file-invalid-type"))) {
                uploadErrorMessages = "Selected asset file type does not match with uploading file. File uploading failed.";
            }
            if (rejectedFiles.some(file => file.errors.some(error => error.code === "too-many-files"))) {
                uploadErrorMessages = "You can upload only 1 file.";
            }
            if (uploadErrorMessages) {
                setFormErrors({
                    ...formErrors,
                    assetFile: uploadErrorMessages
                });
                setUploadedFiles([]);
                setSelectedAssetFileName('');
                setUploadedAssetFilePath('');
                setFormData({
                    ...formData,
                    assetFileName: '',
                });
                return;
            }

            /* For file upload API call */
            // Validate all fields            
            const errors = validateAllFields();
            if (Object.keys(errors).length > 0) {
                setFormErrors({
                    ...errors,
                    assetFile: 'Fill the required fields first',
                });
                return;
            }
            setUploadedFiles(acceptedFiles);
            setSelectedAssetFileName(acceptedFiles[0].name);

            // clear errors and success validation
            setFormErrors({});
            setFormSuccess({});
        }
    });

    const files = uploadedFiles.map(file => (
        <li key={file.name}>
            <div className="d-flex align-items-center gap-2">
                <div>
                    <span className="yp-dz-file-list-file-icon">
                        {file.type === 'application/zip' && (
                            <i className="fa-classic fa-solid fa-file-zipper fa-fw"></i>
                        )}
                    </span>
                </div>
                <div className="flex-wrap flex-grow-1">
                    <span className="yp-dz-file-list-file-name">{file.name}&nbsp;&nbsp;<span>({file.size} bytes)</span></span>
                </div>
                <div>
                    <span className="yp-dz-file-list-action-icon">
                        <a href="#"
                            className="yp-link-icon yp-link-icon-red"
                            onClick={(e) => {
                                e.preventDefault();
                                setUploadedFiles([]);
                                setSelectedAssetFileName('');
                                setUploadedAssetFilePath('');
                                setFormData({
                                    ...formData,
                                    assetFileName: '',
                                });
                                setFormSuccess({});
                                setFormErrors({});
                            }}><i className="fa-classic fa-regular fa-trash-can fa-fw"></i> </a>
                    </span>
                </div>
            </div>
        </li>
    ));

    /* Upload thumbnail */
    const handleUploadThumbnail = () => {
        if (fileUploadThumbnailRef.current) {
            fileUploadThumbnailRef.current.click();
        }
    };
    const handleThumbnailImageChange = async (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0];
        if (!file) return;

        const validTypes = ['image/jpeg', 'image/png'];
        const maxSizeInBytes = 1 * 1024 * 1024; // 1MB

        if (!validTypes.includes(file.type)) {
            setAlert({ type: "error", message: 'Only JPG and PNG files are allowed.' });
            clearUploadThumbnailInput();
            return;
        }

        if (file.size > maxSizeInBytes) {
            setAlert({ type: "error", message: 'File size must be less than 1MB.' });
            clearUploadThumbnailInput();
            return;
        }
        
        // Create FormData
        const formData = new FormData();
        formData.append("ClientId", clientId);
        formData.append('File', file);

        dispatch(uploadAssetThumbnailSlice(formData))
            .then((response: any) => {
                if (response.meta.requestStatus == "fulfilled") {
                    setSelectedAssetThumbnailUrl(response.payload.descriptionUrl);
                    setSelectedAssetThumbnailUrlPath(response.payload.imageUrl);
                    setAlert({ type: "success", message: response.payload.message });
                } else {
                    setSelectedAssetThumbnailUrl('');
                    setSelectedAssetThumbnailUrlPath('');
                    setAlert({ type: "error", message: 'Upload asset thumbnail failed: ' + response.payload.message });
                }
            })
            .catch((error) => {
                console.error("Upload asset thumbnail error:", error);
                setAlert({ type: "error", message: 'An unexpected error occurred.' });
            });
    };    
    const clearUploadThumbnailInput = () => {
        if (fileUploadThumbnailRef.current) {
            fileUploadThumbnailRef.current.value = '';
        }
    };

    return (
        <>

            {/* Loading Overlay */}
            {loading && (
                <div className='yp-loading-overlay'>
                    <div className='yp-spinner'>
                        <div className='spinner-border' role='status'>
                            <span className='visually-hidden'>Loading...</span>
                        </div>
                    </div>
                </div>
            )}

            <div className="yp-page-title-button-section" id="yp-page-title-breadcrumb-section">
                <div className="yp-page-title-breadcrumb">
                    <div className="yp-page-title">{pageTitle}</div>
                    <CustomBreadcrumb items={breadcrumbItems} />
                </div>
            </div>

            <div className='yp-card' id="yp-card-main-content-wrapper">
                <form encType="multipart/form-data">
                    <div className="row">
                        <div className="col-12 col-md-12 col-lg-12">
                            <div className="form-group">
                                <div className="yp-form-control-wrapper">
                                    <input type="text"
                                        className={`form-control yp-form-control  ${formErrors.assetName ? 'is-invalid' : ''}`}
                                        name="assetName"
                                        value={formData.assetName}
                                        // onChange={handleInputChange} 
                                        onChange={(e) => {
                                            const value = e.target.value;
                                            // Prevent leading space
                                            if (formData.assetName === '' && value.startsWith(' ')) return;
                                            handleInputChange(e);
                                        }}
                                        onKeyDown={(e) => {
                                            // Prevent space as first character
                                            if (e.key === ' ' && formData.assetName.length === 0) { e.preventDefault(); }
                                        }}
                                        id=""
                                        placeholder="" />
                                    <label className="form-label"><span className='yp-asteric'>*</span>Asset Name</label>
                                    <input type="hidden" name="id" value={formData.id} />
                                </div>
                                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                    {formErrors.assetName && <div className="invalid-feedback px-3">{formErrors.assetName}</div>}
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
                                        name="assetDescription"
                                        value={formData.assetDescription}
                                        onChange={handleInputChange}
                                        maxLength={200}
                                        onKeyDown={(e) => {
                                            // Prevent leading space
                                            if (e.key === ' ' && e.currentTarget.value.length === 0) {
                                                e.preventDefault();
                                            }
                                        }}
                                        id=""
                                        placeholder=""></textarea>
                                    <label className="form-label">Description</label>
                                </div>
                                <div className='d-flex justify-content-between pt-1 g-2'>
                                    <div className="invalid-feedback px-3">
                                        {
                                            formErrors.assetDescription ? formErrors.assetDescription : ''
                                        }
                                    </div>
                                    <div className="form-text">{assetDescriptionCharCount}/200</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-12 col-md-12 col-lg-6">
                            <div className="form-group">
                                <label className="form-label">Upload Thumbnail</label> 
                                <div className="yp-form-control-wrapper yp-form-control-without-label">
                                    <div className="yp-form-control-file-upload">                                       
                                        <input type="file"
                                            name="assetThumbnailImage" 
                                            className="form-control yp-form-control yp-form-control-with-button"
                                            id="assetThumbnailImage"  
                                            placeholder="" 
                                            onChange={handleThumbnailImageChange}
                                            ref={fileUploadThumbnailRef} />
                                        
                                        <button type="button" 
                                            className="btn btn-primary yp-btn-inside-input"
                                            onClick={handleUploadThumbnail}>Browse</button>

                                        <input type="hidden" name="thumbnailImgRelativePath" value={selectedAssetThumbnailUrl} />
                                    </div> 
                                </div>                               
                                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                    <div className="form-text">
                                        <div>Image Dimensions (PX): <strong>350W X 200H</strong></div>
                                        <div>File Format: <strong>jpg, png</strong></div>
                                        <div>File Size: <strong>1MB</strong></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="col-12 col-md-12 col-lg-6">
                            {
                                (selectedAssetThumbnailUrl && (selectedAssetThumbnailUrl != null)) && (
                                    <>
                                        <div className="mb-3 mb-md-0">
                                            <label className="form-label">
                                                <span className="yp-text-dark-purple">Thumbnail Image</span>
                                            </label>
                                            <div className="yp-uploaded-thumbnail-img">                                                
                                                <img src={selectedAssetThumbnailUrlPath} alt="" title="Thumbnail Image" />
                                            </div>
                                        </div>
                                    </>
                                )
                            }
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-12 col-md-12 col-lg-6">
                            <div className="yp-text-16-400 yp-color-dark-purple mt-3 mb-3">Upload Asset</div>
                            <div className="form-group">
                                <div className="yp-form-control-wrapper">
                                    <select className={`form-control yp-form-control ${formErrors.assetType ? 'is-invalid' : ''}`}
                                        name="assetType"
                                        value={selectedAssetType}
                                        onChange={handleAssetTypeChange}
                                        id="">
                                        <option value="">Select</option>
                                        {assetTypeList.map((type) => (
                                            <option key={type.lookupText} value={type.lookupValue}>
                                                {type.lookupText}
                                            </option>
                                        ))}
                                    </select>
                                    <label className="form-label"><span className='yp-asteric'>*</span>Select Asset Type</label>
                                </div>
                                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                    {formErrors.assetType && <div className="invalid-feedback px-3">{formErrors.assetType}</div>}
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    {
                        (selectedAssetType === 'PDF') 
                        && (
                            <div className="row">
                                <div className="col-12">
                                    <div className="form-group">
                                        <div className="yp-form-control-wrapper">
                                            <div className="form-check form-switch">
                                                <input type="checkbox"
                                                    className="form-check-input"
                                                    role="switch"
                                                    name="allowAssetDownload"
                                                    id="allowAssetDownload"
                                                    value={formData.allowAssetDownload.toString()}
                                                      checked={formData.allowAssetDownload}
                                                    onChange={handleInputChange} />
                                                <label className="form-check-label yp-text-primary yp-text-13-400" htmlFor="allowAssetDownload"><span>Allow learners to download</span></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        )
                    }

                    <div className="row">
                        <div className="col-12">
                            <div className="form-group">
                                <label className="form-label"><span className='yp-asteric'>*</span>Select or Drag & Drop Asset File</label>
                                <div {...getRootProps({ className: isFileUploadDisabled ? 'dropzone yp-dropzone yp-dropzone-disabled' : 'dropzone yp-dropzone' })}>
                                    {/*
                                    Add a hidden file input 
                                    Best to use opacity 0, so that the required validation message will appear on form submission
                                    */}
                                    <input type="file"
                                        name="assetFile"
                                        className="d-none"
                                        ref={hiddenInputRef} />
                                    <input {...getInputProps()} />
                                    <div className="yp-dropzone-content">
                                        <div className="yp-dropzone-content-icon">
                                            <img src={FileUploadImage} alt="File Upload" />
                                        </div>
                                        <div className="yp-dropzone-content-info">Drag & drop files or <a className="yp-link yp-link-primary">browse</a></div>
                                    </div>
                                </div>
                                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                    {(formErrors.assetFile) && <div className="invalid-feedback px-3">{formErrors.assetFile}</div>}
                                    {formSuccess.assetFile && <div className="valid-feedback px-3">{formSuccess.assetFile}</div>}
                                </div>
                                <aside>
                                    <ul className="yp-dropzone-file-list">{files}</ul>
                                </aside>
                                {
                                    (!isAddForm && uploadedAssetFilePath && files.length === 0) && (
                                        <a href={uploadedAssetFilePath} className="yp-link yp-link-primary" target="_blank" rel="noopener noreferrer">{selectedAssetFileName}</a>
                                    )
                                }
                                <input type="hidden" name="assetFileName" className="d-none" value={selectedAssetFileName} />
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-12">
                            <div className="form-group">
                                <div className="yp-form-control-wrapper">
                                    <div className="form-check form-switch">
                                        <input type="checkbox"
                                            className="form-check-input"
                                            role="switch"
                                            name="allowPrintCertificate"
                                            id="allowPrintCertificate"
                                            value={formData.allowPrintCertificate.toString()}
                                            checked={formData.allowPrintCertificate}
                                            onChange={handleInputChange} />
                                        <label className="form-check-label yp-text-primary yp-text-13-400" htmlFor="allowPrintCertificate"><span>Allow learners to print certificate of completion</span></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-12">
                            <div className="yp-form-button-row">
                                <button type="button" className="btn btn-primary" onClick={handleSubmit}>Submit</button>
                                <button type="reset" className="btn btn-secondary" onClick={goBack}>Cancel</button>
                                {isAddForm &&
                                    <a href='#' className="yp-link yp-link-primary yp-link-14-300" onClick={(e) => { e.preventDefault(); handleReset(); }}>Clear</a>
                                }
                            </div>
                        </div>
                    </div>
                </form>

                {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}
            </div>

        </>
    )
}

export default AddAsset;