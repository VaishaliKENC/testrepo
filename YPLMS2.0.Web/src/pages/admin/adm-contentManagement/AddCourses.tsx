import React, { useEffect, useRef, useState, useCallback } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { AdminPageRoutes } from "../../../utils/Constants/Admin_PageRoutes";
import { ICourseAddCoursePayload, IMainPayload } from '../../../Types/commonTableTypes';
import { useDropzone } from "react-dropzone";
import CustomBreadcrumb from "../../../components/shared/CustomBreadcrumb";
import { RootState } from "../../../redux/store";
import { useAppDispatch, useAppSelector } from '../../../hooks';
import { useSelector } from 'react-redux';
import { fetchCourseType, fetchStandardType, addCourse, updateCourse, fetchSingleCourseData, fetchCourseStructureTree, uploadCourseZip, validateXMLFile, uploadCourseThumbnailSlice } from "../../../redux/Slice/admin/contentSlice";
import { COMMON_FOLDER_TREEVIEW_TYPE } from "../../../utils/Constants/Enums";
import * as bootstrap from 'bootstrap'; // Import Bootstrap
import AlertMessage from '../../../components/shared/AlertMessage';
import { FolderTreeView } from "../../../components/shared";

interface StandardType {
    lookupText: string;
    lookupValue: string;
    //to check git issues
}
interface CourseType {
    lookupText: string;
    lookupValue: string;
}

interface FolderOrFile {
    name: string;
    path: string;
    children?: FolderOrFile[]; // For folders
    size?: number; // For files
    type?: string; // For files
    lastModified?: string; // For files
    isFile?: boolean; // Indicate whether it's a file or folder.
}

const AddCourse: React.FC = () => {
    const navigate = useNavigate();
    const dispatch = useAppDispatch();
    const loggedInUserId = useSelector((state: RootState) => state.auth.id);
    const clientId: any = useSelector((state: RootState) => state.auth.clientId);
    const { loading, error } = useAppSelector((state: RootState) => state.course);
    const { courseId } = useParams<{ courseId: string }>();
    let isAddForm: boolean = courseId ? false : true;
    const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);
    const [treeModalOpen, setTreeModalOpen] = useState<boolean>(false)
    const [courseKeywordsCharCount, setCourseKeywordsCharCount] = useState(0);
    const [courseDescriptionCharCount, setCourseDescriptionCharCount] = useState(0);

    const breadcrumbItems = [
        { iconClass: 'fa-classic fa-solid fa-house', path: AdminPageRoutes.ADMIN_DASHBOARD.FULL_PATH },
        { label: 'Manage Courses', path: AdminPageRoutes.ADMIN_CONTENT_MANAGE_COURSES.FULL_PATH },
        { label: isAddForm ? 'Add Course' : 'Edit Course' },
    ];
    const pageTitle: string = isAddForm ? 'Add Course' : 'Edit Course';
    document.title = pageTitle;

    const [standardTypeList, setStandardTypeList] = useState<StandardType[]>([]);
    const [selectedStandardType, setSelectedStandardType] = useState<string>('');
    const [selectedCourseProtocol, setSelectedCourseProtocol] = useState<'https' | 'http'>('https');
    const [selectedWindowType, setSelectedWindowType] = useState<'New Window' | 'Same Window'>('New Window');
    const [isStandardTypeDisabled, setIsStandardTypeDisabled] = useState<boolean>(false);

    const [courseTypeList, setCourseTypeList] = useState<CourseType[]>([]);
    const [selectedCourseType, setSelectedCourseType] = useState<string>('');

    const [selectedXMLFile, setSelectedXMLFile] = useState<string>('');

    /* For Upload thumbnail */
    const fileUploadThumbnailRef = useRef<HTMLInputElement>(null);
    const [selectedCourseThumbnailUrlPath, setSelectedCourseThumbnailUrlPath] = useState<string>('');
    const [selectedCourseThumbnailUrl, setSelectedCourseThumbnailUrl] = useState<string>('');
    /* END For Upload thumbnail */

    const [savedWindowDimensions, setSavedWindowDimensions] = useState({ width: 1000, height: 800 });


    const [formData, setFormData] = useState({
        id: '',
        courseName: '',
        standardType: '',
        courseAuthoringTool: '',
        manifestOrPoint: 'ManifestPackage',
        manifestPackageFile: null as File | null,
        pointURLFilePath: '', //'Clients/CLIW6hJcmeHlLrN/Courses/COUGiFEsDgc6',
        courseDescription: '',
        courseKeywords: '',
        courseMasteryScore: 0,
        courseWindowType: 'New Window',
        courseSettingsCheckbox: [''],
        courseProtocol: 'https',
        courseDimensionsWidth: 0,
        courseDimensionsHeight: 0,
        allowScrollBar: false,
        allowWindowResizing: false,
        isActive: true,
        thumbnailImgRelativePath: ''
    });
    const handleReset = () => {
        setFormData({
            id: '',
            courseName: '',
            standardType: '',
            courseAuthoringTool: '',
            manifestOrPoint: 'ManifestPackage',
            manifestPackageFile: null as File | null,
            pointURLFilePath: '', //'Clients/CLIW6hJcmeHlLrN/Courses/COUGiFEsDgc6',
            courseDescription: '',
            courseKeywords: '',
            courseMasteryScore: 0,
            courseWindowType: 'New Window',
            courseSettingsCheckbox: [''],
            courseProtocol: 'https',
            courseDimensionsWidth: 0,
            courseDimensionsHeight: 0,
            allowScrollBar: false,
            allowWindowResizing: false,
            isActive: true,
            thumbnailImgRelativePath: ''
        });

        setUploadedFiles([]);
        setSelectedStandardType('');
        setSelectedCourseType('');
        setSelectedCourseProtocol('https');
        setSelectedWindowType('New Window');
        setSelectedManifestOrPoint('ManifestPackage');
        setIsFileUploadDisabled(false);
        setCourseDescriptionCharCount(0);
        setCourseKeywordsCharCount(0);

        setSelectedCourseThumbnailUrl('');
        setSelectedCourseThumbnailUrlPath('');

        setFormErrors({});
        setFormSuccess({});
    };

    useEffect(() => {
        if (courseId) {
            const courseSinglePayload: IMainPayload = {
                id: courseId,
                clientId: clientId
            }

            dispatch(fetchSingleCourseData(courseSinglePayload))
                .then((response: any) => {
                    if (response.meta.requestStatus == "fulfilled") {

                        // const initialCourseSettingsCheckbox = [];
                        // if (response.payload.contentModule.isAssessment) initialCourseSettingsCheckbox.push('Contains Assessment');
                        // if (response.payload.contentModule.isMiddlePage) initialCourseSettingsCheckbox.push('Launch Page Required');
                        // if (response.payload.contentModule.isPrintCertificate) initialCourseSettingsCheckbox.push('Print Certificate');

                        const { isAssessment, isMiddlePage, isPrintCertificate } = response.payload.contentModule;
                        const initialCourseSettingsCheckbox = [
                            isAssessment && 'Contains Assessment',
                            isMiddlePage && 'Launch Page Required',
                            isPrintCertificate && 'Print Certificate'
                        ].filter(Boolean);


                        setFormData({
                            id: response.payload.contentModule.id,
                            courseName: response.payload.contentModule.contentModuleName,
                            standardType: response.payload.contentModule.contentModuleTypeId,
                            courseAuthoringTool: response.payload.contentModule.contentModuleSubTypeId,
                            manifestOrPoint: response.payload.contentModule.contentModuleURL != '' ? 'PointURL' : 'ManifestPackage',
                            manifestPackageFile: null as File | null,
                            pointURLFilePath: response.payload.contentModule.contentModuleURL,
                            courseDescription: response.payload.contentModule.contentModuleDescription,
                            courseKeywords: response.payload.contentModule.contentModuleKeyWords,
                            courseMasteryScore: response.payload.contentModule.masteryScore,
                            courseWindowType: response.payload.contentModule.courseLaunchNewWindow === true ? 'New Window' : 'Same Window',
                            courseSettingsCheckbox: initialCourseSettingsCheckbox,
                            courseProtocol: response.payload.contentModule.protocol,
                            courseDimensionsWidth: response.payload.contentModule.courseWindowWidth,
                            courseDimensionsHeight: response.payload.contentModule.courseWindowHeight,
                            allowScrollBar: response.payload.contentModule.allowScroll,
                            allowWindowResizing: response.payload.contentModule.allowResize,
                            isActive: true,
                            thumbnailImgRelativePath: response.payload.contentModule.thumbnailImgRelativePath
                        });

                        setFormData({
                            id: response.payload.contentModule.id,
                            courseName: response.payload.contentModule.contentModuleName,
                            standardType: response.payload.contentModule.contentModuleTypeId,
                            courseAuthoringTool: response.payload.contentModule.contentModuleSubTypeId,
                            manifestOrPoint: response.payload.contentModule.contentModuleURL != '' ? 'PointURL' : 'ManifestPackage',
                            manifestPackageFile: null as File | null,
                            pointURLFilePath: response.payload.contentModule.contentModuleURL,
                            courseDescription: response.payload.contentModule.contentModuleDescription,
                            courseKeywords: response.payload.contentModule.contentModuleKeyWords,
                            courseMasteryScore: response.payload.contentModule.masteryScore,
                            courseWindowType: response.payload.contentModule.courseLaunchNewWindow === true ? 'New Window' : 'Same Window',
                            courseSettingsCheckbox: initialCourseSettingsCheckbox,
                            courseProtocol: response.payload.contentModule.protocol,
                            courseDimensionsWidth: response.payload.contentModule.courseWindowWidth,
                            courseDimensionsHeight: response.payload.contentModule.courseWindowHeight,
                            allowScrollBar: response.payload.contentModule.allowScroll,
                            allowWindowResizing: response.payload.contentModule.allowResize,
                            isActive: true,
                            thumbnailImgRelativePath: response.payload.contentModule.thumbnailImgRelativePath
                        });

                        setSelectedStandardType(response.payload.contentModule.contentModuleTypeId);
                        setSelectedCourseType(response.payload.contentModule.contentModuleSubTypeId);
                        setSelectedManifestOrPoint(response.payload.contentModule.contentModuleURL != '' ? 'PointURL' : 'ManifestPackage');
                        setSelectedCourseProtocol(response.payload.contentModule.protocol);
                        setSelectedWindowType(response.payload.contentModule.courseLaunchNewWindow === true ? 'New Window' : 'Same Window');

                        setCourseDescriptionCharCount(response.payload.contentModule.contentModuleDescription ? response.payload.contentModule.contentModuleDescription.length : 0);
                        setCourseKeywordsCharCount(response.payload.contentModule.contentModuleKeyWords ? response.payload.contentModule.contentModuleKeyWords.length : 0);

                        setSelectedCourseThumbnailUrl(response.payload.contentModule.thumbnailImgRelativePath);
                        setSelectedCourseThumbnailUrlPath(response.payload.contentModule.contentServerURL + response.payload.contentModule.thumbnailImgRelativePath);

                    } else {
                        setAlert({ type: "error", message: 'Course data fetch failed: ' + response.msg });
                    }
                })
                .catch((error) => {
                    console.error("Course data fetch error:", error);
                    setAlert({ type: "error", message: 'An unexpected error occurred.' });
                });
        }
    }, [courseId]);

    const [formErrors, setFormErrors] = useState<Record<string, string>>({});
    const [formSuccess, setFormSuccess] = useState<Record<string, string>>({});
    const validateField = (name: string, value: string | Array<string> | File | null): string => {
        //setFormSuccess((prev) => ({ ...prev, manifestPackageFile: '', pointURLFilePath: '' }));
        setFormSuccess({});

        if (typeof value === 'string') {
            if (value.trim() && !isValidInput(value)) {
                return "Special characters like < > ' & are not allowed.";
            }
        }

        switch (name) {
            case "courseName":
                if (typeof value === "string" && !value.trim())
                    return "Course name is required";
                break;
            case "standardType":
                if (typeof value === "string" && !value.trim())
                    return "Standard type is required";
                break;
            case "courseAuthoringTool":
                if (typeof value === "string" && !value.trim())
                    return "Course authoring tool is required";
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

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>) => {
        const target = e.target as HTMLInputElement;
        const name = target.name;
        const value = target.value;
        const type = target.type;
        const checked = (target as HTMLInputElement).checked;

        let newValue: any = value;
        // if (type === 'number') {
        //     newValue = value ? Number(value) : ""; 
        // } else if (type === 'checkbox') {
        //     newValue = checked;
        // }

        // Custom validation for courseDimensionsWidth and Height
        if (name === 'courseDimensionsWidth' || name === 'courseDimensionsHeight') {
            // Only allow digits, max 4 characters
            const digitOnlyRegex = /^\d{0,4}$/;

            if (!digitOnlyRegex.test(value)) {
                return; // Reject invalid input like "3-232323..."
            }
            // Convert to number or keep empty string
            newValue = value === '' ? '' : Number(value);
        } else if (name === 'courseMasteryScore') {
            if (value === '' || (/^\d+$/.test(value) && Number(value) <= 100)) {
                newValue = value === '' ? '' : Number(value);
            } else {
                return; // block anything over 100
            }
        } else if (type === 'checkbox') {
            newValue = checked;
        } else if (type === 'number') {
            newValue = value === '' ? '' : Number(value);
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

        // Character count
        if (name === "courseDescription") {
            setCourseDescriptionCharCount(value.length);
        }
        if (name === "courseKeywords") {
            setCourseKeywordsCharCount(value.length);
        }
    };

    const getCoursePayload = (): ICourseAddCoursePayload => {
        return {
            id: formData.id,
            clientId: clientId,
            createdById: loggedInUserId,
            lastModifiedById: loggedInUserId,
            contentModuleName: formData.courseName,
            contentModuleEnglishName: formData.courseName,
            contentModuleTypeId: formData.standardType,
            contentModuleSubTypeId: formData.courseAuthoringTool,
            contentModuleDescription: formData.courseDescription,
            contentModuleKeyWords: formData.courseKeywords,
            contentModuleURL: formData.pointURLFilePath, //formData.manifestOrPoint === 'ManifestPackage' ? formData.manifestPackageFile : formData.pointURLFilePath,
            masteryScore: Number(formData.courseMasteryScore),
            courseLaunchSameWindow: formData.courseWindowType == 'Same Window' ? true : false,
            courseLaunchNewWindow: formData.courseWindowType == 'New Window' ? true : false,
            isAssessment: formData.courseSettingsCheckbox.includes('Contains Assessment'),
            isMiddlePage: formData.courseSettingsCheckbox.includes('Launch Page Required'),
            isPrintCertificate: formData.courseSettingsCheckbox.includes('Print Certificate'),
            protocol: formData.courseProtocol,
            allowScroll: formData.allowScrollBar,
            allowResize: formData.allowWindowResizing,
            courseWindowWidth: Number(formData.courseDimensionsWidth),
            courseWindowHeight: Number(formData.courseDimensionsHeight),
            sendEmailTo: 'test@email.com',
            isActive: true,
            ThumbnailImgRelativePath: selectedCourseThumbnailUrl
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
        if (selectedManifestOrPoint === 'ManifestPackage' && uploadedFiles.length === 0) {
            setFormErrors({
                ...errors,
                manifestPackageFile: 'Please upload ZIP Course Manifest Package file. Before that also ensure all the required fields are filled.',
            });
            hasErrors = true;
        }
        if (selectedManifestOrPoint === 'PointURL' && !formData.pointURLFilePath.trim()) {
            setFormErrors({
                ...errors,
                pointURLFilePath: 'Upload course ZIP file if not uploaded and then browse for course imsmanifest.xml file. OR. Browse for course imsmanifest.xml file in already uploaded courses.',
            });
            hasErrors = true;
        }
        if (hasErrors) {
            return;
        }

        const coursePayLoad: ICourseAddCoursePayload = getCoursePayload();
        console.log('Submitting course:', coursePayLoad);

        if (isAddForm) {
            dispatch(addCourse(coursePayLoad))
                .then((response: any) => {
                    // If signup is successful, navigate to the login
                    if (response.meta.requestStatus == "fulfilled") {
                        setAlert({ type: "success", message: "Course added successfully." });

                        setTimeout(() => {
                            navigate(AdminPageRoutes.ADMIN_CONTENT_MANAGE_COURSES.FULL_PATH);
                        }, 2000);
                    } else {
                        setAlert({ type: "error", message: 'Add course failed: ' + response.msg });
                    }
                })
                .catch((error) => {
                    console.error("Add course error:", error);
                    setAlert({ type: "error", message: 'An unexpected error occurred.' });
                });
        }
        else {
            dispatch(updateCourse(coursePayLoad))
                .then((response: any) => {
                    // If signup is successful, navigate to the login
                    if (response.meta.requestStatus == "fulfilled") {
                        setAlert({ type: "success", message: "Course updated successfully." });
                        setTimeout(() => {
                            navigate(AdminPageRoutes.ADMIN_CONTENT_MANAGE_COURSES.FULL_PATH);
                        }, 2000);
                    } else {
                        setAlert({ type: "error", message: 'Update course failed: ' + response.msg });
                    }
                })
                .catch((error) => {
                    console.error("Update course error:", error);
                    setAlert({ type: "error", message: 'An unexpected error occurred.' });
                });
        }
    };

    //Course Window Settings: New & same window    
    const handleWindowTypeChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const value = event.target.value as 'New Window' | 'Same Window';
        setSelectedWindowType(value);
        // setFormData({
        //     ...formData,
        //     courseWindowType: value
        // });        
        // if (value === 'Same Window') {
        //     // Reset the "Window Settings" values when "Same Window" is selected
        //     setFormData({
        //         ...formData,
        //         allowScrollBar: false,
        //         allowWindowResizing: false,
        //         courseDimensionsWidth: 0,
        //         courseDimensionsHeight: 0,
        //     });
        // }

        setFormData((prevFormData) => {
            const { courseDimensionsWidth, courseDimensionsHeight } = prevFormData;
            const updatedFormData = {
                ...prevFormData,
                courseWindowType: value, // Update courseWindowType in formData
            };

            // if (value === 'Same Window') {
            //     // Reset the "Window Settings" values when "Same Window" is selected
            //     updatedFormData.allowScrollBar = false;
            //     updatedFormData.allowWindowResizing = false;
            //     updatedFormData.courseDimensionsWidth = 0;
            //     updatedFormData.courseDimensionsHeight = 0;
            // } else if (value === 'New Window') {
            //     updatedFormData.courseDimensionsWidth = courseDimensionsWidth || 1000;
            //     updatedFormData.courseDimensionsHeight = courseDimensionsHeight || 800;
            // }
            if (value === 'Same Window') {
                // Save current dimensions before resetting
                setSavedWindowDimensions({
                    width: prevFormData.courseDimensionsWidth > 0 ? prevFormData.courseDimensionsWidth : 1000,
                    height: prevFormData.courseDimensionsHeight > 0 ? prevFormData.courseDimensionsHeight : 800
                });

                updatedFormData.allowScrollBar = false;
                updatedFormData.allowWindowResizing = false;
                updatedFormData.courseDimensionsWidth = 0;
                updatedFormData.courseDimensionsHeight = 0;
            } else if (value === 'New Window') {
                updatedFormData.courseDimensionsWidth = savedWindowDimensions.width > 0 ? savedWindowDimensions.width : 1000;
                updatedFormData.courseDimensionsHeight = savedWindowDimensions.height > 0 ? savedWindowDimensions.height : 800;
            }
            return updatedFormData;
        });
    };

    //Course Protocol: https & http    
    const handleCourseProtocolChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const value = event.target.value as 'https' | 'http';
        setSelectedCourseProtocol(value);
        setFormData({
            ...formData,
            courseProtocol: value
        });
    };

    //Course Settings
    const handleCourseSettingsCheckboxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const { value, checked } = event.target;

        const updatedCheckboxes = checked
            ? [...formData.courseSettingsCheckbox, value]
            : formData.courseSettingsCheckbox.filter((item) => item !== value);

        // Remove empty string value which is initialized in formData or any null value
        const filteredCheckboxes = updatedCheckboxes.filter(function (el) {
            return el != null && el != '';
        });

        setFormData({
            ...formData,
            courseSettingsCheckbox: filteredCheckboxes
        });
    };

    //File upload : Manifest Package or IMS Manifest URL
    const [selectedManifestOrPoint, setSelectedManifestOrPoint] = useState<'ManifestPackage' | 'PointURL'>('ManifestPackage');
    const handleManifestOrPointChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const value = event.target.value as 'ManifestPackage' | 'PointURL';
        setSelectedManifestOrPoint(value);
        setFormData({
            ...formData,
            manifestOrPoint: value
        });
    };

    const goBack = () => {
        navigate(-1); // -1 navigates to the previous page
    };

    /* Get standard type list */
    useEffect(() => {
        if (!clientId) return;

        dispatch(fetchStandardType(clientId)).then((response: any) => {
            if (response.meta.requestStatus == "fulfilled") {
                setStandardTypeList(response.payload.standardTypeList);
            } else {
                setAlert({ type: "error", message: 'Standard type data fetch failed: ' + response.msg });
            }
        })
            .catch((error) => {
                console.error("Standard type data fetch error:", error);
                setAlert({ type: "error", message: 'An unexpected error occurred.' });
            });
    }, [clientId]);
    const handleStandardTypeChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        handleInputChange(event);
        setSelectedStandardType(event.target.value);
    };

    /* Get standard type list */
    useEffect(() => {
        if (!clientId) return;

        dispatch(fetchCourseType(clientId)).then((response: any) => {
            if (response.meta.requestStatus == "fulfilled") {
                setCourseTypeList(response.payload.courseTypeList);
            } else {
                setAlert({ type: "error", message: 'Course type data fetch failed: ' + response.msg });
            }
        })
            .catch((error) => {
                console.error("Course type data fetch error:", error);
                setAlert({ type: "error", message: 'An unexpected error occurred.' });
            });
    }, [clientId]);
    const handleCourseTypeChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        handleInputChange(event);
        setSelectedCourseType(event.target.value);
    };

    /* File Upload */
    const hiddenInputRef = useRef<HTMLInputElement | null>(null);
    const [uploadedFiles, setUploadedFiles] = useState<File[]>([]); // State to store accepted files   
    const [isFileUploadDisabled, setIsFileUploadDisabled] = useState(false);
    const { acceptedFiles, getRootProps, getInputProps } = useDropzone({
        noKeyboard: true,
        accept: {
            "application/zip": [".zip"], // MIME type for ZIP files
        },
        maxFiles: 1,
        disabled: isFileUploadDisabled,
        onDrop: (acceptedFiles, rejectedFiles) => {
            let uploadErrorMessages = "";

            // clear pointURLFilePath
            setFormData({
                ...formData,
                pointURLFilePath: ''
            });

            if (hiddenInputRef.current) {
                const dataTransfer = new DataTransfer();
                acceptedFiles.forEach((v) => {
                    dataTransfer.items.add(v);
                });
                hiddenInputRef.current.files = dataTransfer.files;
            }

            if (rejectedFiles.some(file => file.errors.some(error => error.code === "file-invalid-type"))) {
                uploadErrorMessages = "Only ZIP files are allowed. Kindly upload a ZIP file only.";
            }
            if (rejectedFiles.some(file => file.errors.some(error => error.code === "too-many-files"))) {
                uploadErrorMessages = "You can upload only 1 ZIP file.";
            }
            if (uploadErrorMessages) {
                setFormErrors({
                    ...formErrors,
                    manifestPackageFile: uploadErrorMessages,
                    pointURLFilePath: ''
                });
                setUploadedFiles([]);
                hiddenInputRef.current = null;
                return;
            }

            /* For file upload API call */
            // Validate all fields            
            const errors = validateAllFields();
            if (Object.keys(errors).length > 0) {
                setFormErrors({
                    ...errors,
                    manifestPackageFile: 'Fill the required fields first',
                });
                return;
            }

            setUploadedFiles(acceptedFiles);

            const courseFormData = new FormData();
            courseFormData.append("ClientId", clientId);
            courseFormData.append("ContentModuleTypeId", formData.standardType,);
            courseFormData.append("KeepZipFile", "false");
            courseFormData.append("Isedit", (isAddForm ? 'false' : 'true'));

            courseFormData.append("ID", formData.id);
            courseFormData.append("ContentModuleURL", formData.pointURLFilePath);

            if (hiddenInputRef.current && hiddenInputRef.current.files && hiddenInputRef.current.files.length > 0) {
                courseFormData.append("file", hiddenInputRef.current.files[0]);
            }

            const payload: FormData = courseFormData;

            dispatch(uploadCourseZip(payload))
                .then((response: any) => {
                    console.log(response);
                    if (response.meta.requestStatus === "fulfilled") {
                        setFormSuccess({
                            ...formSuccess,
                            manifestPackageFile: response.payload?.msg,
                        });
                        setFormErrors({});

                        // Set contentModuleURL Path
                        setFormData({
                            ...formData,
                            pointURLFilePath: response.payload.contentModule.contentModuleURL,
                        });

                    } else {
                        const serverMsg =
                            response.payload?.msg ||         // common case if server returned { msg: '...' }
                            response.payload?.title ||       // your previous usage
                            response.error?.msg ||       // fallback to the error message
                            "Upload failed";

                        setFormErrors({
                            ...formErrors,
                            // manifestPackageFile: response.payload.title,
                            manifestPackageFile: serverMsg,
                            pointURLFilePath: '',
                        });
                        setFormSuccess({});

                        setUploadedFiles([]);
                        hiddenInputRef.current = null;
                        setFormData({
                            ...formData,
                            pointURLFilePath: '',
                        });
                    }
                })
                .catch((error) => {
                    setAlert({ type: "error", message: "Error uploading ZIP file." });
                    console.error(error);

                    setUploadedFiles([]);
                    hiddenInputRef.current = null;
                });

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
                                setFormData({
                                    ...formData,
                                    pointURLFilePath: '',
                                });

                                setFormSuccess({});
                                setFormErrors({});
                                hiddenInputRef.current = null;
                            }}><i className="fa-classic fa-regular fa-trash-can fa-fw"></i> </a>
                    </span>
                </div>
            </div>
        </li>
    ));


    // Get Folder Tree Structure from API
    const ypModalLmsManifestPointURLBtnClose = useRef<HTMLButtonElement | null>(null);
    const [courseStructureTreeData, setCourseStructureTreeData] = useState<FolderOrFile[]>([]);
    const ypModalLmsManifestPointURLRef = useRef<HTMLDivElement | null>(null);


    const handleGetFolderStructure = () => {
        setTreeModalOpen(true)
        // Validate all fields            
        const errors = validateAllFields();
        if (Object.keys(errors).length > 0) {
            setFormErrors({
                ...errors,
                pointURLFilePath: 'Fill the required fields first',
            });
            return;
        }

        // programmatically open the modal
        const modalElement = ypModalLmsManifestPointURLRef.current;
        if (modalElement) {
            const modal = new bootstrap.Modal(modalElement);
            modal.show();
        }

        dispatch(fetchCourseStructureTree(clientId))
            .then((response: any) => {
                if (response.meta.requestStatus === 'fulfilled') {
                    setCourseStructureTreeData(response.payload.courses || []);
                } else {
                    setAlert({ type: "error", message: 'Failed to fetch course folder structure: ' + response.payload?.message || 'Unknown error.' });
                }
            })
            .catch((error) => {
                console.error('Error while fetching course folder structure:', error);
                setAlert({ type: "error", message: 'An unexpected error occurred.' });
            });
    };
    const handleFileSelect = (filePath: string) => {
        setSelectedXMLFile(filePath);
        setFormData({ ...formData, pointURLFilePath: filePath });

        // close the popup once file path is received
        if (ypModalLmsManifestPointURLBtnClose.current) {
            ypModalLmsManifestPointURLBtnClose.current.click();
        }

        // Validate XML File API call
        const payload = { clientId: clientId, contentModuleURL: filePath, contentModuleTypeId: formData.standardType };
        dispatch(validateXMLFile(payload))
            .then((response: any) => {
                console.clear();
                console.log(response);
                if (response.meta.requestStatus === "fulfilled") {
                    // disable Satndard Type select dropdown. 
                    // So that the value cannot be changed once XML file is selected 
                    setIsStandardTypeDisabled(true);

                    setFormSuccess({
                        ...formSuccess,
                        pointURLFilePath: response.payload.msg,
                    });
                    setFormErrors({});
                } else {
                    setFormErrors({
                        ...formErrors,
                        manifestPackageFile: '',
                        pointURLFilePath: response.payload.msg,
                    });
                    setFormSuccess({});

                    // clear pointURLFilePath
                    setFormData({
                        ...formData,
                        pointURLFilePath: ''
                    });
                }
            })
            .catch((error) => {
                setAlert({ type: "error", message: "Error validating XML file." });
                console.error(error);
            });
    };

    // Function to clear the courseStructureTreeData
    const clearCourseStructureTreeData = () => {
        setTreeModalOpen(false)
        setCourseStructureTreeData([]);
    };


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

        dispatch(uploadCourseThumbnailSlice(formData))
            .then((response: any) => {
                if (response.meta.requestStatus == "fulfilled") {
                    setSelectedCourseThumbnailUrl(response.payload.descriptionUrl);
                    setSelectedCourseThumbnailUrlPath(response.payload.imageUrl);
                    setAlert({ type: "success", message: response.payload.message });
                } else {
                    setSelectedCourseThumbnailUrl('');
                    setSelectedCourseThumbnailUrlPath('');
                    setAlert({ type: "error", message: 'Upload course thumbnail failed: ' + response.payload.message });
                }
            })
            .catch((error) => {
                console.error("Upload course thumbnail error:", error);
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
            <div className='yp-card' id="yp-card-main-content-section">

                <form encType="multipart/form-data">
                    <div className="row">
                        <div className="col-12 col-md-12 col-lg-4">
                            <div className="form-group">
                                <div className="yp-form-control-wrapper">
                                    <input type="text"
                                        className={`form-control yp-form-control ${formErrors.courseName ? 'is-invalid' : ''}`}
                                        name="courseName"
                                        value={formData.courseName}
                                        // onChange={handleInputChange} 
                                        onChange={(e) => {
                                            const value = e.target.value;
                                            // Prevent leading space
                                            if (formData.courseName === '' && value.startsWith(' ')) return;
                                            handleInputChange(e);
                                        }}
                                        onKeyDown={(e) => {
                                            // Prevent space as first character
                                            if (e.key === ' ' && formData.courseName.length === 0) { e.preventDefault(); }
                                        }}
                                        id=""
                                        placeholder="" />
                                    <label className="form-label"><span className='yp-asteric'>*</span>Course Name</label>
                                    <input type="hidden" name="id" value={formData.id} />
                                </div>
                                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                    {formErrors.courseName && <div className="invalid-feedback px-3">{formErrors.courseName}</div>}
                                </div>
                            </div>
                        </div>
                        <div className="col-12 col-md-12 col-lg-4">
                            <div className="form-group">
                                <div className="yp-form-control-wrapper">
                                    <select className={`form-control yp-form-control ${formErrors.standardType ? 'is-invalid' : ''}`}
                                        name="standardType"
                                        value={selectedStandardType}
                                        onChange={handleStandardTypeChange}
                                        disabled={isStandardTypeDisabled}
                                        id="">
                                        <option value="">Select</option>
                                        {standardTypeList.map((type) => (
                                            <option key={type.lookupText} value={type.lookupValue}>
                                                {type.lookupText}
                                            </option>
                                        ))}
                                    </select>
                                    <label className="form-label"><span className='yp-asteric'>*</span>Standard Type</label>
                                </div>
                                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                    {formErrors.standardType && <div className="invalid-feedback px-3">{formErrors.standardType}</div>}
                                </div>
                            </div>
                        </div>
                        <div className="col-12 col-md-12 col-lg-4">
                            <div className="form-group">
                                <div className="yp-form-control-wrapper">
                                    <select className={`form-control yp-form-control ${formErrors.courseAuthoringTool ? 'is-invalid' : ''}`}
                                        name="courseAuthoringTool"
                                        value={selectedCourseType}
                                        onChange={handleCourseTypeChange}
                                        id="">
                                        <option value="">Select</option>
                                        {courseTypeList.map((type) => (
                                            <option key={type.lookupText} value={type.lookupValue}>
                                                {type.lookupText}
                                            </option>
                                        ))}
                                    </select>
                                    <label className="form-label"><span className='yp-asteric'>*</span>Course Authoring Tool</label>
                                </div>
                                <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                    {formErrors.courseAuthoringTool && <div className="invalid-feedback px-3">{formErrors.courseAuthoringTool}</div>}
                                </div>
                            </div>
                        </div>
                    </div>

                    <div className="row">
                        <div className="col-12 col-md-6 col-lg-4">
                            <div className="form-group">
                                <div className="yp-form-control-wrapper">
                                    <div className={`form-check form-check-box form-check-box-gray form-check-box-lg ${selectedManifestOrPoint === 'ManifestPackage' ? 'selected-card' : ''}`}>                                        <input type="radio" name="manifestOrPoint"
                                        className="form-check-input d-none"
                                        id="manifestOrPoint"
                                        value={'ManifestPackage'}
                                        checked={selectedManifestOrPoint === 'ManifestPackage'}
                                        onChange={handleManifestOrPointChange} />
                                        <label htmlFor="manifestOrPoint" className="w-100 cursor-pointer">
                                            <div className="form-check-label yp-color-dark-purple yp-ml-0-px">
                                                Manifest Package
                                            </div>
                                            <div className="yp-fs-11-400 yp-text-565656 yp-mt-5-px yp-ml-0-px">
                                                Upload your course as a ZIP file—it includes the content and a manifest that helps the LMS run and track it.
                                            </div>
                                        </label>
                                    </div>
                                </div >
                            </div >
                        </div >
                        <div className="col-12 col-md-6 col-lg-4">
                            <div className="form-group">
                                <div className="yp-form-control-wrapper">
                                    <div className={`form-check form-check-box form-check-box-gray form-check-box-lg ${selectedManifestOrPoint === 'PointURL' ? 'selected-card' : ''}`}>
                                        <input type="radio" name="manifestOrPoint"
                                            className="form-check-input d-none"
                                            id="pointURL"
                                            value={'PointURL'}
                                            checked={selectedManifestOrPoint === 'PointURL'}
                                            onChange={handleManifestOrPointChange} />
                                        <label className="form-check-label yp-color-dark-purple yp-ml-0-px" htmlFor="pointURL">
                                            Point IMSmanifest URL
                                            <div className="yp-fs-11-400 yp-text-565656 yp-mt-5-px yp-ml-0-px">
                                                Select the direct URL to the imsmanifest.xml file to load course content from a remote package folders uploaded on the server.
                                            </div>
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    {/* Conditional Rendering of Divs */}
                    {selectedManifestOrPoint === 'ManifestPackage' && (
                        <div className="row">
                            <div className="col-12">
                                <div className="form-group">
                                    <label className="form-label"><span className='yp-asteric'>*</span>Select Course Manifest Package File: (Only Zip File Format)</label>
                                    <div {...getRootProps({ className: isFileUploadDisabled ? 'dropzone yp-dropzone yp-dropzone-disabled' : 'dropzone yp-dropzone' })}>
                                        {/*
                                    Add a hidden file input 
                                    Best to use opacity 0, so that the required validation message will appear on form submission
                                    */}
                                        <input type="file"
                                            name="manifestPackageFile"
                                            className="d-none"
                                            ref={hiddenInputRef} />
                                        <input {...getInputProps()} />
                                        <div className="yp-dropzone-content">
                                            <div className="yp-dropzone-content-icon">
                                                <i className="fa-solid fa-file-zipper yp-color-dark-purple"></i>
                                            </div>
                                            <div className="yp-dropzone-content-info">Drag & drop files or <a className="yp-link yp-link-primary">browse</a></div>
                                        </div>
                                    </div>
                                    <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                        {(formErrors.manifestPackageFile || formErrors.pointURLFilePath) &&
                                            <div className="invalid-feedback px-3">
                                                {formErrors.manifestPackageFile}
                                                {
                                                    formErrors.manifestPackageFile && (
                                                        <><br /></>
                                                    )
                                                }
                                                {formErrors.pointURLFilePath}
                                                {
                                                    formErrors.manifestPackageFile && (
                                                        <><br /></>
                                                    )
                                                }
                                            </div>
                                        }
                                        {formSuccess.manifestPackageFile && <div className="valid-feedback px-3">{formSuccess.manifestPackageFile}</div>}
                                    </div>
                                    <aside>
                                        <ul className="yp-dropzone-file-list">{files}</ul>
                                    </aside>
                                </div>
                            </div>
                        </div>
                    )}
                    {selectedManifestOrPoint === 'PointURL' && (
                        <div className="row" id="yp-pointURLDiv">
                            <div className="col-12">
                                <div className="form-group">
                                    <label className="form-label">Please select course folder (imsmanifest.xml file folder if exist in the course):</label>
                                    <div className="yp-form-control-wrapper yp-form-control-without-label">
                                        <input type="text"
                                            className={`form-control yp-form-control yp-form-control-with-button ${formErrors.pointURLFilePath ? 'is-invalid' : ''}`}
                                            name="pointURLFilePath"
                                            value={formData.pointURLFilePath}
                                            id=""
                                            placeholder=""
                                            readOnly />

                                        <button type="button"
                                            className="btn btn-primary yp-btn-inside-input"
                                            //data-bs-toggle="modal" 
                                            //data-bs-target="#yp-modal-lmsManifestPointURL"
                                            disabled={isFileUploadDisabled ? true : false}
                                            onClick={handleGetFolderStructure}>Browse</button>
                                    </div>
                                    <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                        {formErrors.pointURLFilePath && <div className="invalid-feedback px-3">{formErrors.pointURLFilePath}</div>}
                                        {formSuccess.pointURLFilePath && <div className="valid-feedback px-3">{formSuccess.pointURLFilePath}</div>}
                                    </div>
                                </div>
                            </div>
                        </div>
                    )
                    }


                    <div className="modal fade yp-modal yp-modal-right-side"
                        id="yp-modal-lmsManifestPointURL"
                        data-bs-backdrop="static"
                        data-bs-keyboard="false"
                        tabIndex={-1}
                        aria-labelledby="yp-modal-lmsManifestPointURL"
                        ref={ypModalLmsManifestPointURLRef}
                        aria-hidden="true">
                        <div className="modal-dialog modal-dialog-scrollable modal-fullscreen-sm-down">
                            <div className="modal-content">
                                <div className="modal-header">
                                    <h1 className="modal-title" id="staticBackdropLabel">Please select course folder (imsmanifest.xml file folder if exist in the course)</h1>
                                    <button type="button"
                                        className="btn-close"
                                        data-bs-dismiss="modal"
                                        aria-label="Close"
                                        onClick={clearCourseStructureTreeData}
                                        ref={ypModalLmsManifestPointURLBtnClose}></button>
                                </div>
                                {treeModalOpen && <div className="modal-body p-0">
                                    <FolderTreeView
                                        id={''}
                                        type={COMMON_FOLDER_TREEVIEW_TYPE.ADD_COURSE_FOLDER_TREE}
                                        data={courseStructureTreeData as any}
                                        onSelect={''}
                                        onPageChange={() => { }}
                                        currentPage={1}
                                        totalRecords={0}
                                        pageSize={10}
                                        onFileSelect={handleFileSelect}
                                    />
                                </div>}

                            </div>
                        </div>
                    </div>

                    <div className="row">
                        <div className="col-12 col-md-12 col-lg-6">
                            <div className="form-group yp-form-group-with-note">
                                <div className="yp-form-control-wrapper">
                                    <textarea
                                        className={`form-control yp-form-control`}
                                        name="courseDescription"
                                        value={formData.courseDescription}
                                        onChange={handleInputChange}
                                        onKeyDown={(e) => {
                                            // Prevent space as the first character
                                            if (e.key === ' ' && e.currentTarget.value.length === 0) {
                                                e.preventDefault();
                                            }
                                        }}
                                        maxLength={200}
                                        id=""
                                        placeholder=""></textarea>
                                    <label className="form-label">Course Description</label>
                                </div>
                                <div className='d-flex justify-content-between pt-1 g-2'>
                                    <div className="invalid-feedback px-3">
                                        {
                                            formErrors.courseDescription ? formErrors.courseDescription : ''
                                        }
                                    </div>
                                    <div className="form-text">{courseDescriptionCharCount}/200</div>
                                </div>
                            </div>
                        </div>
                        <div className="col-12 col-md-12 col-lg-6">
                            <div className="form-group yp-form-group-with-note">
                                <div className="yp-form-control-wrapper">
                                    <textarea
                                        className={`form-control yp-form-control`}
                                        name="courseKeywords"
                                        value={formData.courseKeywords}
                                        onChange={handleInputChange}
                                        onKeyDown={(e) => {
                                            // Prevent space as the first character
                                            if (e.key === ' ' && e.currentTarget.value.length === 0) {
                                                e.preventDefault();
                                            }
                                        }}
                                        maxLength={200}
                                        id=""
                                        placeholder=""></textarea>
                                    <label className="form-label">Course Keywords</label>
                                </div>
                                <div className='d-flex justify-content-between pt-1 g-2'>
                                    <div className="invalid-feedback px-3">
                                        {
                                            formErrors.courseKeywords ? formErrors.courseKeywords : ''
                                        }
                                    </div>
                                    <div className="form-text">{courseKeywordsCharCount}/200</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-12 col-md-12 col-lg-4">
                            <div className="form-group">
                                <div className="yp-form-control-wrapper">
                                    <input type="number"
                                        min={0}
                                        max={100}
                                        className={`form-control yp-form-control`}
                                        name="courseMasteryScore"
                                        value={formData.courseMasteryScore}
                                        onChange={handleInputChange}
                                        id="" placeholder=""
                                        onKeyDown={(e) => {
                                            // if (e.key === '-' || e.key === 'e' || e.key === 'E') {
                                            //     e.preventDefault();
                                            // }
                                            if (['-', 'e', 'E', '+'].includes(e.key)) {
                                                e.preventDefault();
                                            }
                                        }} />
                                    <label className="form-label">Course Mastery Score</label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div className="yp-well yp-well-md mb-5">
                        <div className="yp-fs-16 yp-text-dark-purple mb-4">Course Window Settings</div>

                        <div>
                            <div className="form-group">
                                <div className="yp-form-control-wrapper">
                                    <div className="form-check form-check-inline form-check-box">
                                        <input type="radio"
                                            name="courseWindowType"
                                            className="form-check-input"
                                            id="courseWindowTypeNew"
                                            value={'New Window'}
                                            checked={selectedWindowType === 'New Window'}
                                            onChange={handleWindowTypeChange} />
                                        <label className="form-check-label" htmlFor="courseWindowTypeNew">New Window</label>
                                    </div>
                                    <div className="form-check form-check-inline form-check-box">
                                        <input type="radio"
                                            name="courseWindowType"
                                            className="form-check-input"
                                            id="courseWindowTypeSame"
                                            value={'Same Window'}
                                            checked={selectedWindowType === 'Same Window'}
                                            onChange={handleWindowTypeChange} />
                                        <label className="form-check-label" htmlFor="courseWindowTypeSame">Same Window</label>
                                    </div>
                                </div>
                            </div>

                            <div className="row yp-course-window-settings-wrapper flex-wrap flex-lg-nowrap">
                                <div className="col-12 col-lg-auto flex-shrink-0 flex-lg-shrink-1">
                                    <div className="yp-fs-13-500 yp-text-dark-purple">Course Settings:</div>
                                    <div className="mt-3">
                                        <div className="yp-form-control-wrapper">
                                            <div className="form-check">
                                                <input type="checkbox"
                                                    name="courseSettingsCheckbox"
                                                    className="form-check-input"
                                                    id="ContainsAssessmentCheckbox"
                                                    value={'Contains Assessment'}
                                                    checked={formData.courseSettingsCheckbox.includes('Contains Assessment')}
                                                    onChange={handleCourseSettingsCheckboxChange} />
                                                <label className="form-check-label" htmlFor="ContainsAssessmentCheckbox">Contains Assessment</label>
                                            </div>
                                            <div className="form-check">
                                                <input type="checkbox" name="courseSettingsCheckbox"
                                                    className="form-check-input"
                                                    id="LaunchPageRequiredCheckbox"
                                                    value={'Launch Page Required'}
                                                    checked={formData.courseSettingsCheckbox.includes('Launch Page Required')}
                                                    onChange={handleCourseSettingsCheckboxChange} />
                                                <label className="form-check-label" htmlFor="LaunchPageRequiredCheckbox">Launch Page Required</label>
                                            </div>
                                            <div className="form-check">
                                                <input type="checkbox" name="courseSettingsCheckbox"
                                                    className="form-check-input"
                                                    id="PrintCertificateCheckbox"
                                                    value={'Print Certificate'}
                                                    checked={formData.courseSettingsCheckbox.includes('Print Certificate')}
                                                    onChange={handleCourseSettingsCheckboxChange} />
                                                <label className="form-check-label" htmlFor="PrintCertificateCheckbox">Print Certificate</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div className="col-12 col-lg-auto flex-shrink-0 flex-lg-shrink-1">
                                    <div className="yp-fs-13-500 yp-text-dark-purple">Course Protocol:</div>
                                    <div className="mt-3">
                                        <div className="yp-form-control-wrapper">
                                            <div className="form-check form-check-inline">
                                                <input type="radio"
                                                    name="courseProtocol"
                                                    className="form-check-input"
                                                    id="courseProtocolHTTPS"
                                                    value={'https'}
                                                    checked={selectedCourseProtocol === 'https'}
                                                    onChange={handleCourseProtocolChange} />
                                                <label className="form-check-label" htmlFor="courseProtocolHTTPS">Always https</label>
                                            </div>
                                            <div className="form-check form-check-inline">
                                                <input type="radio"
                                                    name="courseProtocol"
                                                    className="form-check-input"
                                                    id="courseProtocolHTTP"
                                                    value={'http'}
                                                    checked={selectedCourseProtocol === 'http'}
                                                    onChange={handleCourseProtocolChange} />
                                                <label className="form-check-label" htmlFor="courseProtocolHTTP">Always http</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div className="col-12 col-lg-auto flex-shrink-0 flex-lg-shrink-1 d-none">
                                    <div className="yp-fs-13-500 yp-text-dark-purple">Window Settings:</div>
                                    <div className="mt-3">
                                        <div className="yp-form-control-wrapper">
                                            <div className="form-check yp-my-10-px">
                                                <input type="checkbox"
                                                    name="allowScrollBar"
                                                    className="form-check-input"
                                                    id="allowScrollBarCheckbox"
                                                    value={'Allow Scroll Bar'}
                                                    checked={formData.allowScrollBar}
                                                    onChange={handleInputChange}
                                                    disabled={selectedWindowType === 'Same Window'} />
                                                <label className="form-check-label" htmlFor="allowScrollBarCheckbox">Allow Scroll Bar</label>
                                            </div>
                                            <div className="form-check yp-my-10-px">
                                                <input type="checkbox"
                                                    name="allowWindowResizing"
                                                    className="form-check-input"
                                                    id="allowWindowResizingCheckbox"
                                                    value={'Allow Window Resizing'}
                                                    checked={formData.allowWindowResizing}
                                                    onChange={handleInputChange}
                                                    disabled={selectedWindowType === 'Same Window'} />
                                                <label className="form-check-label" htmlFor="allowWindowResizingCheckbox">Allow Window Resizing</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div className="col-12 col-lg-auto flex-shrink-0 flex-lg-shrink-1">
                                    <div className="yp-fs-13-500 yp-text-dark-purple">Course Dimensions:</div>
                                    <div className="mt-3">
                                        <div className="form-group">
                                            <div className="yp-form-control-wrapper yp-form-control-with-suffix yp-width-120-px">


                                                <input type="number"
                                                    //   type="text"
                                                    name="courseDimensionsWidth"
                                                    className="form-control yp-form-control"
                                                    value={formData.courseDimensionsWidth}
                                                    onChange={handleInputChange}
                                                    disabled={selectedWindowType === 'Same Window'}
                                                    placeholder=""
                                                // maxLength={4} 
                                                />
                                                <span className="input-group-text">px</span>
                                                <label className="form-label">Width</label>
                                            </div>
                                        </div>
                                        <div className="form-group mb-0">
                                            <div className="yp-form-control-wrapper yp-form-control-with-suffix yp-width-120-px">

                                                <input type="number"
                                                    // type="text"
                                                    name="courseDimensionsHeight"
                                                    className="form-control yp-form-control"
                                                    value={formData.courseDimensionsHeight}
                                                    onChange={handleInputChange}
                                                    disabled={selectedWindowType === 'Same Window'}
                                                    placeholder=""
                                                // maxLength={4}
                                                />
                                                <span className="input-group-text">px</span>
                                                <label className="form-label">Height</label>
                                            </div>
                                        </div>
                                    </div>
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
                                            name="courseThumbnailImage"
                                            className="form-control yp-form-control yp-form-control-with-button"
                                            id="courseThumbnailImage"
                                            placeholder=""
                                            onChange={handleThumbnailImageChange}
                                            ref={fileUploadThumbnailRef} />

                                        <button type="button"
                                            className="btn btn-primary yp-btn-inside-input"
                                            onClick={handleUploadThumbnail}>Browse</button>

                                        <input type="hidden" name="thumbnailImgRelativePath" value={selectedCourseThumbnailUrl} />
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
                                (selectedCourseThumbnailUrl && (selectedCourseThumbnailUrl != null)) && (
                                    <>
                                        <div className="mb-3 mb-md-0">
                                            <label className="form-label">
                                                <span className="yp-text-dark-purple">Thumbnail Image</span>
                                            </label>
                                            <div className="yp-uploaded-thumbnail-img">
                                                <img src={selectedCourseThumbnailUrlPath} alt="" title="Thumbnail Image" />
                                            </div>
                                        </div>
                                    </>
                                )
                            }
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
                </form >

                {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}
            </div >
        </>
    )
}

export default AddCourse;