import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { RootState } from "../../../redux/store";
import { useAppDispatch, useAppSelector } from "../../../hooks";
import { AdminPageRoutes } from "../../../utils/Constants/Admin_PageRoutes";
import CustomBreadcrumb from "../../../components/shared/CustomBreadcrumb";
import AlertMessage from '../../../components/shared/AlertMessage';
import DatePicker from 'react-datepicker';
import { fetchDefaultAssignmentListSlice, saveDefaultAssignmentSlice } from "../../../redux/Slice/admin/defaultAssignmentsSlice";

const DefaultAssignmentDates: React.FC = () => {
    const navigate = useNavigate();
    const dispatch = useAppDispatch();
    const clientId: any = useSelector((state: RootState) => state.auth.clientId);
    const id: any = useSelector((state: RootState) => state.auth.id);
    const { loading, error } = useAppSelector((state: RootState) => state.course);
    const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);

    const breadcrumbItems = [
        { iconClass: 'fa-classic fa-solid fa-house', path: AdminPageRoutes.ADMIN_DASHBOARD.FULL_PATH },
        { label: 'Default Assignment Dates' },
    ];
    const pageTitle: string = 'Default Assignment Dates';
    document.title = pageTitle;

    // Based on provided values
    const [isDefaultDateEnabled, setIsDefaultDateEnabled] = useState<boolean>(true);
    const [chkIsForDynamic, setChkIsForDynamic] = useState(false);

    const [txtDefaultAssignmnetDays, setTxtDefaultAssignmnetDays] = useState<string>("");
    const [txtAssignmentDays, setTxtAssignmentDays] = useState<string>("");
    const [ddlAssignmentDate, setDdlAssignmentDate] = useState<string>("");
    const [defaultAssignmentDateType, setDefaultAssignmentDateType] = useState<string>('');

    const [txtDefaultDueDays, setTxtDefaultDueDays] = useState<string>("");
    const [txtDueDays, setTxtDueDays] = useState<string>("");
    const [ddlDueDate, setDdlDueDate] = useState<string>("");
    const [defaultDueDateType, setDefaultDueDateType] = useState<string>('');

    const [txtDefaultExpDays, setTxtDefaultExpDays] = useState<string>("");
    const [txtExprDays, setTxtExprDays] = useState<string>("");
    const [ddlExprDate, setDdlExprDate] = useState<string>("");
    const [defaultExpiryDateType, setDefaultExpiryDateType] = useState<string>('');

    // Page load default assignment value set on fields
    useEffect(() => {
        dispatch(fetchDefaultAssignmentListSlice(clientId))
            .then((response: any) => {
                console.log("response", response)
                if (response.meta.requestStatus === "fulfilled") {
                    const data = response.payload;
                    console.log("Default assignment response:", data);

                    if (!data) return;

                    // ✅ Main toggles
                    setIsDefaultDateEnabled(data.chkAssignmentDates ?? true);
                    // setChkIsForDynamic(data.chkIsForDynamic ?? false);
                    setChkIsForDynamic(data.chkIsForDynamic === true);

                    // ✅ Default Assignment Date
                    if (data.rbAbsoluteDate) {
                        setDefaultAssignmentDateType("absolute");
                        setTxtDefaultAssignmnetDays(data.txtDefaultAssignmnetDays || "");
                        setTxtAssignmentDays("");
                        setDdlAssignmentDate("");
                    } else {
                        setDefaultAssignmentDateType("relative");
                        setTxtAssignmentDays(data.txtAssignmentDays || "");
                        setDdlAssignmentDate(data.ddlAssignmentDate || "");
                        setTxtDefaultAssignmnetDays("");
                    }

                    // ✅ Default Due Date
                    if (data.rbAbsoluteDueDate) {
                        setDefaultDueDateType("absolute");
                        setTxtDefaultDueDays(data.txtDefaultDueDays || "");
                        setTxtDueDays("");
                        setDdlDueDate("");
                    } else if (data.rbRelativeDueDate) {
                        setDefaultDueDateType("relative");
                        setTxtDueDays(data.txtDueDays || "");
                        setDdlDueDate(data.ddlDueDate || "");
                        setTxtDefaultDueDays("");
                    } else if (data.rbNoDueDate) {
                        setDefaultDueDateType("none");
                        setTxtDefaultDueDays("");
                        setTxtDueDays("");
                        setDdlDueDate("");
                    }

                    // ✅ Default Expiry Date
                    if (data.rbAbsoluteExpiryDate) {
                        setDefaultExpiryDateType("absolute");
                        setTxtDefaultExpDays(data.txtDefaultExpDays || "");
                        setTxtExprDays("");
                        setDdlExprDate("");
                    } else if (data.rbRelativeExpiryDate) {
                        setDefaultExpiryDateType("relative");
                        setTxtExprDays(data.txtExprDays || "");
                        setDdlExprDate(data.ddlExprDate || "");
                        setTxtDefaultExpDays("");
                    } else if (data.rbNoExpiryDate) {
                        setDefaultExpiryDateType("none");
                        setTxtDefaultExpDays("");
                        setTxtExprDays("");
                        setDdlExprDate("");
                    }
                }
            })
            .catch((error) => {
                console.error("DefaultAssignment error:", error);
                setAlert({ type: "error", message: 'An unexpected error occurred.' });
            });
    }, [clientId, dispatch]);


    const handleAssignmentDateChange = (type: string) => {
        setDefaultAssignmentDateType(type);
        if (type === 'absolute') {
            setTxtAssignmentDays("");
            setDdlAssignmentDate("");
        } else {
            setTxtDefaultAssignmnetDays("");
        }
    };

    const handleDueDateChange = (type: string) => {
        setDefaultDueDateType(type);
        if (type === 'absolute') {
            setTxtDueDays("");
            setDdlDueDate("");
        } else if (type === 'relative') {
            setTxtDefaultDueDays("");
        } else {
            setTxtDefaultDueDays("");
            setTxtDueDays("");
            setDdlDueDate("");
        }
    };

    const handleExpiryDateChange = (type: string) => {
        setDefaultExpiryDateType(type);
        if (type === 'absolute') {
            setTxtExprDays("");
            setDdlExprDate("");
        } else if (type === 'relative') {
            setTxtDefaultExpDays("");
        } else {
            setTxtDefaultExpDays("");
            setTxtExprDays("");
            setDdlExprDate("");
        }
    };

    const handleSave = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault(); // Prevent page reload

        if (isDefaultDateEnabled) {
            // ✅ Assignment Date validation
            if (defaultAssignmentDateType !== 'absolute' && defaultAssignmentDateType !== 'relative') {
                setAlert({ type: "warning", message: "Set Assignment Date: Specify Absolute Date or Specify Relative Date." });
                return;
            }

            if (defaultAssignmentDateType === 'absolute' && (!txtDefaultAssignmnetDays || parseInt(txtDefaultAssignmnetDays) <= 0)) {
                setAlert({ type: "warning", message: "Please enter a valid Absolute Assignment Date (days > 0)." });
                return;
            }

            if (defaultAssignmentDateType === 'relative') {
                if (!txtAssignmentDays || parseInt(txtAssignmentDays) <= 0) {
                    setAlert({ type: "warning", message: "Please enter a valid number of days after for Assignment." });
                    return;
                }
                if (!ddlAssignmentDate || ddlAssignmentDate === "0") {
                    setAlert({ type: "warning", message: "Please select a valid base date for Relative Assignment." });
                    return;
                }
            }

            // ✅ Due Date validation
            if (
                defaultDueDateType !== 'absolute' &&
                defaultDueDateType !== 'relative' &&
                defaultDueDateType !== 'none'
            ) {
                setAlert({ type: "warning", message: "Set Due Date: Specify Absolute, Relative, or No Due Date." });
                return;
            }

            if (defaultDueDateType === 'absolute' && (!txtDefaultDueDays || parseInt(txtDefaultDueDays) <= 0)) {
                setAlert({ type: "warning", message: "Please enter a valid Absolute Due Date (days > 0)." });
                return;
            }

            if (defaultDueDateType === 'relative') {
                if (!txtDueDays || parseInt(txtDueDays) <= 0) {
                    setAlert({ type: "warning", message: "Please enter a valid number of days after for Due Date." });
                    return;
                }
                if (!ddlDueDate || ddlDueDate === "0") {
                    setAlert({ type: "warning", message: "Please select a valid base date for Relative Due Date." });
                    return;
                }
            }

            // ✅ Expiry Date validation
            if (
                defaultExpiryDateType !== 'absolute' &&
                defaultExpiryDateType !== 'relative' &&
                defaultExpiryDateType !== 'none'
            ) {
                setAlert({ type: "warning", message: "Set Expiry Date: Specify Absolute, Relative, or No Expiry Date." });
                return;
            }

            if (defaultExpiryDateType === 'absolute' && (!txtDefaultExpDays || parseInt(txtDefaultExpDays) <= 0)) {
                setAlert({ type: "warning", message: "Please enter a valid Absolute Expiry Date (days > 0)." });
                return;
            }

            if (defaultExpiryDateType === 'relative') {
                if (!txtExprDays || parseInt(txtExprDays) <= 0) {
                    setAlert({ type: "warning", message: "Please enter a valid number of days after for Expiry Date." });
                    return;
                }
                if (!ddlExprDate || ddlExprDate === "0") {
                    setAlert({ type: "warning", message: "Please select a valid base date for Relative Expiry Date." });
                    return;
                }
            }

        }
        const params = {
            pageIsValid: true,
            clientId,
            currentUrl: window.location.href,
            createdById: id,
            chkAssignmentDates: isDefaultDateEnabled,

            // Assignment Date Options
            rbAbsoluteDate: defaultAssignmentDateType === 'absolute',
            txtDefaultAssignmnetDays: txtDefaultAssignmnetDays || "0",
            txtAssignmentDays: txtAssignmentDays || "0",
            ddlAssignmentDate: ddlAssignmentDate || "0",

            // Due Date Options
            rbAbsoluteDueDate: defaultDueDateType === 'absolute',
            txtDefaultDueDays: txtDefaultDueDays || "0",
            rbRelativeDueDate: defaultDueDateType === 'relative',
            txtDueDays: txtDueDays || "0",
            ddlDueDate: ddlDueDate || "0",
            rbNoDueDate: defaultDueDateType === 'none',

            // Expiry Date Options
            rbAbsoluteExpiryDate: defaultExpiryDateType === 'absolute',
            txtDefaultExpDays: txtDefaultExpDays || "0",
            rbRelativeExpiryDate: defaultExpiryDateType === 'relative',
            txtExprDays: txtExprDays || "0",
            ddlExprDate: ddlExprDate || "0",
            rbNoExpiryDate: defaultExpiryDateType === 'none',

            chkIsForDynamic
        };

        // console.log("params", params);
        dispatch(saveDefaultAssignmentSlice(params))
            .then((response: any) => {
                if (response.meta.requestStatus == "fulfilled") {
                    setAlert({ type: "success", message: response.payload.msg });
                } else {
                    setAlert({ type: "warning", message: response.payload.msg });
                }
            })
            .catch((error) => {
                console.error("DefaultAssignment error:", error);
                setAlert({ type: "error", message: 'An unexpected error occurred.' });
            })
    };

    const handleClear = (e: React.MouseEvent<HTMLAnchorElement>) => {
        e.preventDefault();

        // setIsDefaultDateEnabled(true);
        // setChkIsForDynamic(true);

        // Assignment
        setDefaultAssignmentDateType('');
        setTxtDefaultAssignmnetDays('');
        setTxtAssignmentDays('');
        setDdlAssignmentDate('');

        // Due
        setDefaultDueDateType('');
        setTxtDefaultDueDays('');
        setTxtDueDays('');
        setDdlDueDate('');

        // Expiry
        setDefaultExpiryDateType('');
        setTxtDefaultExpDays('');
        setTxtExprDays('');
        setDdlExprDate('');

        // Clear any alert
        setAlert(null);
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

            {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}

            <div className="yp-page-title-button-section" id="yp-page-title-breadcrumb-section">
                <div className="yp-page-title-breadcrumb">
                    <div className="yp-page-title">{pageTitle}</div>
                    <CustomBreadcrumb items={breadcrumbItems} />
                </div>
            </div>
            <div className='yp-card' id="yp-card-main-content-section">
                <form>
                    <div className="row">
                        <div className="col-12">
                            <div className="form-group mb-0">
                                <div className="yp-form-control-wrapper">
                                    <div className="form-check form-switch">
                                        <input
                                            className="form-check-input"
                                            type="checkbox"
                                            role="switch"
                                            name="checkSetDefaultOtaDateChecked"
                                            id="checkSetDefaultOtaDateChecked"
                                            value=""
                                            checked={isDefaultDateEnabled}
                                            onChange={(e) => setIsDefaultDateEnabled(e.target.checked)}
                                        />
                                        <label className="form-check-label yp-text-primary yp-fs-13-400" htmlFor="checkSetDefaultOtaDateChecked">
                                            <span>Set Default One Time Assignment Dates</span>
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr className="yp-hr yp-hr-30-0" />
                    <fieldset disabled={!isDefaultDateEnabled}>
                        <div className="yp-fs-13-500 yp-text-dark-purple mb-3"><span className='yp-asteric'>*</span> Default Assignment Date:</div>
                        <div className="row">
                            <div className="col-12 col-md-12 col-lg-3">
                                <div className="form-check form-check-inline">
                                    <input type="radio" name="radioSetDefaultAssignmentDate"
                                        className="form-check-input"
                                        id="radioDefaultAssignmentAbsoluteDate"
                                        value={'absolute'}
                                        checked={defaultAssignmentDateType === 'absolute'}
                                        onChange={() => handleAssignmentDateChange('absolute')} />
                                    <label className="form-check-label yp-color-dark-purple"
                                        htmlFor="radioDefaultAssignmentAbsoluteDate">Specify Absolute Date</label>
                                </div>
                                {defaultAssignmentDateType === 'absolute' && (
                                    <div className="mt-1 mb-3 mb-lg-0">
                                        <div className="yp-days-after-wrapper">
                                            <label className="form-label pe-0">Current Date + </label>
                                            <div className="yp-width-60-px">
                                                <input type="number"
                                                    name="inputDefaultAssignmentAbsoluteDaysPlus"
                                                    className={`form-control yp-form-control yp-form-control-sm`}
                                                    placeholder=""
                                                    value={txtDefaultAssignmnetDays}
                                                    onChange={(e) => {
                                                        const value = e.target.value;
                                                        if (value === "") {
                                                            setTxtDefaultAssignmnetDays(""); // allow empty state
                                                        } else if (/^\d+$/.test(value)) {
                                                            setTxtDefaultAssignmnetDays(String(value)); // allow only digits
                                                        }
                                                    }}
                                                // onChange={(e) => setTxtDefaultAssignmnetDays(e.target.value)}
                                                />
                                            </div>
                                            <label className="form-label">Days</label>
                                        </div>
                                    </div>
                                )}
                            </div>
                            <div className={`col-12 ${defaultAssignmentDateType === 'relative' ? 'col-md-12 col-lg-auto' : 'col-md-12 col-lg-3'}`}>
                                <div className="form-check form-check-inline">
                                    <input type="radio" name="radioSetDefaultAssignmentDate"
                                        className="form-check-input"
                                        id="radioDefaultAssignmentRelativeDate"
                                        value={'relative'}
                                        checked={defaultAssignmentDateType === 'relative'}
                                        onChange={() => handleAssignmentDateChange('relative')} />
                                    <label className="form-check-label yp-color-dark-purple"
                                        htmlFor="radioDefaultAssignmentRelativeDate">Specify Relative Date</label>
                                </div>
                                {defaultAssignmentDateType === 'relative' && (
                                    <div className="mt-1 mb-3 mb-lg-0">
                                        <div className="yp-days-after-wrapper">
                                            <div className="yp-width-60-px">
                                                <input type="number"
                                                    name="inputDefaultAssignmentAbsoluteDaysAfter"
                                                    className={`form-control yp-form-control yp-form-control-sm`}
                                                    placeholder=""
                                                    value={txtAssignmentDays}
                                                    onChange={(e) => {
                                                        const value = e.target.value;
                                                        if (value === "") {
                                                            setTxtAssignmentDays(""); // allow empty state
                                                        } else if (/^\d+$/.test(value)) {
                                                            setTxtAssignmentDays(String(value)); // allow only digits
                                                        }
                                                    }}
                                                // onChange={(e) => setTxtDefaultAssignmnetDays(e.target.value)}
                                                />
                                            </div>
                                            <label className="form-label">Days After</label>
                                            <div className="yp-width-218-px">
                                                <select name="selectDefaultAssignmentAbsoluteDaysAfterOption"
                                                    className="form-control yp-form-control yp-form-control-sm" value={ddlAssignmentDate} onChange={(e) => setDdlAssignmentDate(e.target.value)}>
                                                    <option>Select</option>
                                                    <option value="Hire Date">Hire Date</option>
                                                    <option value="User's Record Creation Date">User's Record Creation Date</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                )}
                            </div>
                        </div>
                        <hr className="yp-hr yp-hr-30-0" />

                        <div className="yp-fs-13-500 yp-text-dark-purple mb-3"><span className='yp-asteric'>*</span> Default Due Date:</div>
                        <div className="row">
                            <div className="col-12 col-md-12 col-lg-3">
                                <div className="form-check form-check-inline">
                                    <input type="radio" name="radioSetDefaultExpiryAlertDate"
                                        className="form-check-input"
                                        id="radioDefaultExpiryAlertAbsoluteDate"
                                        value={'absolute'}
                                        checked={defaultDueDateType === 'absolute'}
                                        onChange={() => handleDueDateChange('absolute')} />
                                    <label className="form-check-label yp-color-dark-purple"
                                        htmlFor="radioDefaultExpiryAlertAbsoluteDate">Specify Absolute Date</label>
                                </div>
                                {defaultDueDateType === 'absolute' && (
                                    <div className="mt-1 mb-3 mb-lg-0">
                                        <div className="yp-days-after-wrapper">
                                            <label className="form-label pe-0">Current Date + </label>
                                            <div className="yp-width-60-px">
                                                <input type="number"
                                                    name="inputDefaultExpiryAlertAbsoluteDaysPlus"
                                                    className={`form-control yp-form-control yp-form-control-sm`}
                                                    placeholder="" value={txtDefaultDueDays} onChange={(e) => setTxtDefaultDueDays(e.target.value)}
                                                />
                                            </div>
                                            <label className="form-label">Days</label>
                                        </div>
                                    </div>
                                )}
                            </div>
                            <div className={`col-12 ${defaultDueDateType === 'relative' ? 'col-md-12 col-lg-auto' : 'col-md-12 col-lg-3'}`}>
                                <div className="form-check form-check-inline">
                                    <input type="radio" name="radioSetDefaultExpiryAlertDate"
                                        className="form-check-input"
                                        id="radioDefaultExpiryAlertRelativeDate"
                                        value={'relative'}
                                        checked={defaultDueDateType === 'relative'}
                                        onChange={() => handleDueDateChange('relative')} />
                                    <label className="form-check-label yp-color-dark-purple"
                                        htmlFor="radioDefaultExpiryAlertRelativeDate">Specify Relative Date</label>
                                </div>
                                {defaultDueDateType === 'relative' && (
                                    <div className="mt-1 mb-3 mb-lg-0">
                                        <div className="yp-days-after-wrapper">
                                            <div className="yp-width-60-px">
                                                <input type="number"
                                                    name="inputDefaultExpiryAlertAbsoluteDaysAfter"
                                                    className={`form-control yp-form-control yp-form-control-sm`}
                                                    placeholder=""
                                                    value={txtDueDays} onChange={(e) => setTxtDueDays(e.target.value)}
                                                />
                                            </div>
                                            <label className="form-label">Days After</label>
                                            <div className="yp-width-218-px">
                                                <select name="selectDefaultExpiryAlertAbsoluteDaysAfterOption"
                                                    className="form-control yp-form-control yp-form-control-sm" value={ddlDueDate} onChange={(e) => setDdlDueDate(e.target.value)}>
                                                    <option>Select</option>
                                                    <option value="Hire Date">Hire Date</option>
                                                    <option value="User's Record Creation Date">User's Record Creation Date</option>
                                                    <option value="Assignment Date"> Assignment Date</option>
                                                    <option value="Assignment Start Date"> Assignment Start Date</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                )}
                            </div>
                            <div className="col-12 col-md-12 col-lg-3">
                                <div className="form-check form-check-inline">
                                    <input type="radio" name="radioSetDefaultExpiryAlertDate"
                                        className="form-check-input"
                                        id="radioDefaultExpiryAlertNoDueDate"
                                        value={'none'}
                                        checked={defaultDueDateType === 'none'}
                                        onChange={() => handleDueDateChange('none')} />
                                    <label className="form-check-label yp-color-dark-purple"
                                        htmlFor="radioDefaultExpiryAlertNoDueDate">No Due Date</label>
                                </div>
                            </div>
                        </div>
                        <hr className="yp-hr yp-hr-30-0" />


                        <div className="yp-fs-13-500 yp-text-dark-purple mb-3"><span className='yp-asteric'>*</span> Default Expiry Date:</div>
                        <div className="row">
                            <div className="col-12 col-md-12 col-lg-3">
                                <div className="form-check form-check-inline">
                                    <input type="radio" name="radioSetDefaultExpiryDate"
                                        className="form-check-input"
                                        id="radioDefaultExpiryAbsoluteDate"
                                        value={'absolute'}
                                        checked={defaultExpiryDateType === 'absolute'}
                                        onChange={() => handleExpiryDateChange('absolute')} />
                                    <label className="form-check-label yp-color-dark-purple"
                                        htmlFor="radioDefaultExpiryAbsoluteDate">Specify Absolute Date</label>
                                </div>
                                {defaultExpiryDateType === 'absolute' && (
                                    <div className="mt-1 mb-3 mb-lg-0">
                                        <div className="yp-days-after-wrapper">
                                            <label className="form-label pe-0">Current Date + </label>
                                            <div className="yp-width-60-px">
                                                <input type="number"
                                                    name="inputDefaultExpiryAbsoluteDaysPlus"
                                                    className={`form-control yp-form-control yp-form-control-sm`}
                                                    placeholder=""
                                                    value={txtDefaultExpDays} onChange={(e) => setTxtDefaultExpDays(e.target.value)}
                                                />
                                            </div>
                                            <label className="form-label">Days</label>
                                        </div>
                                    </div>
                                )}
                            </div>
                            <div className={`col-12 ${defaultExpiryDateType === 'relative' ? 'col-md-12 col-lg-auto' : 'col-md-12 col-lg-3'}`}>
                                <div className="form-check form-check-inline">
                                    <input type="radio" name="radioSetDefaultExpiryDate"
                                        className="form-check-input"
                                        id="radioDefaultExpiryRelativeDate"
                                        value={'relative'}
                                        checked={defaultExpiryDateType === 'relative'}
                                        onChange={() => handleExpiryDateChange('relative')} />
                                    <label className="form-check-label yp-color-dark-purple"
                                        htmlFor="radioDefaultExpiryRelativeDate">Specify Relative Date</label>
                                </div>
                                {defaultExpiryDateType === 'relative' && (
                                    <div className="mt-1 mb-3 mb-lg-0">
                                        <div className="yp-days-after-wrapper">
                                            <div className="yp-width-60-px">
                                                <input type="number"
                                                    name="inputDefaultExpiryAbsoluteDaysAfter"
                                                    className={`form-control yp-form-control yp-form-control-sm`}
                                                    placeholder=""
                                                    value={txtExprDays} onChange={(e) => setTxtExprDays(e.target.value)}
                                                />
                                            </div>
                                            <label className="form-label">Days After</label>
                                            <div className="yp-width-218-px">
                                                <select name="selectDefaultExpiryAbsoluteDaysAfterOption"
                                                    className="form-control yp-form-control yp-form-control-sm" value={ddlExprDate} onChange={(e) => setDdlExprDate(e.target.value)} >
                                                    <option>Select</option>
                                                    <option value="Assignment Date">Assignment Date</option>
                                                    <option value="Assignment Due Date">Assignment Due Date</option>
                                                    <option value="Assignment Start Date"> Assignment Start Date</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                )}
                            </div>
                            <div className="col-12 col-md-12 col-lg-3">
                                <div className="form-check form-check-inline">
                                    <input type="radio" name="radioSetDefaultExpiryDate"
                                        className="form-check-input"
                                        id="radioDefaultExpiryNoDueDate"
                                        value={'none'}
                                        checked={defaultExpiryDateType === 'none'}
                                        onChange={() => handleExpiryDateChange('none')} />
                                    <label className="form-check-label yp-color-dark-purple"
                                        htmlFor="radioDefaultExpiryNoDueDate">No Expiry Date</label>
                                </div>
                            </div>
                        </div>
                        <hr className="yp-hr yp-hr-30-0" />

                        <div className="row">
                            <div className="col-12">
                                <div className="form-group mb-0">
                                    <div className="yp-form-control-wrapper">
                                        <div className="form-check form-switch">
                                            <input className="form-check-input"
                                                type="checkbox"
                                                role="switch"
                                                name="checkUseTheSettingsOnDynamicAssignmentPage"
                                                id="checkUseTheSettingsOnDynamicAssignmentPage"
                                                value=""
                                                checked={chkIsForDynamic} onChange={(e) => setChkIsForDynamic(e.target.checked)} />
                                            <label className="form-check-label yp-text-primary yp-fs-13-400" htmlFor="checkUseTheSettingsOnDynamicAssignmentPage"><span>Use these settings on Dynamic assignment page</span></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <hr className="yp-hr yp-hr-30-0" />
                    </fieldset>
                    <div className="row">
                        <div className="col-12">
                            <div className="yp-form-button-row">
                                <button className="btn btn-primary" onClick={handleSave} >Save</button>
                                {/* <button className="btn btn-secondary">Cancel</button> */}
                                <a href='#' className="yp-link yp-link-primary yp-link-14-300" onClick={handleClear}>Clear</a>
                            </div>
                        </div>
                    </div>

                </form>
            </div>
        </>
    );
};

export default DefaultAssignmentDates;
