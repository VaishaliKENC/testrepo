// DefinePropertiesContent.tsx
import React, { useEffect, useState } from 'react';
import DatePicker, { registerLocale } from "react-datepicker";
import { useAppDispatch } from '../../../../hooks';
import { fetchSelectedActivitySlice, fetchUsersSelectedSlice, setAssignmentProperties } from '../../../../redux/Slice/admin/oneTimeAssignmentsSlice';
import AlertMessage from '../../../../components/shared/AlertMessage';
import { useSelector } from 'react-redux';
import { RootState } from '../../../../redux/store';
import { enGB } from "date-fns/locale/en-GB";
interface Props {
    onCancel: () => void;
    selectedBusinessRuleValue: string;
    onSetActiveTab: (tabKey: string) => void;
    resetForm?: boolean;
    onResetComplete?: () => void;
    defaultAssignmentData?: any;
}

const DefinePropertiesContent: React.FC<Props> = ({ onCancel, onSetActiveTab, selectedBusinessRuleValue, resetForm, onResetComplete, defaultAssignmentData }) => {
    const dispatch = useAppDispatch();
    const id: any = useSelector((state: RootState) => state.auth.id);
    const clientId: any = useSelector((state: RootState) => state.auth.clientId);

    const [assignmentDateType, setAssignmentDateType] = useState<string>('');
    const [dueDateType, setDueDateType] = useState<string>('');
    const [expiryDateType, setExpiryDateType] = useState<string>('');

    const [assignmentAbsoluteDate, setAssignmentAbsoluteDate] = useState<Date | null>(null);
    const [dueAbsoluteDate, setDueAbsoluteDate] = useState<Date | null>(null);
    const [expiryAbsoluteDate, setExpiryAbsoluteDate] = useState<Date | null>(null);

    const [isReassignmentChecked, setIsReassignmentChecked] = useState<boolean>(false);
    const [isMailChecked, setIsMailChecked] = useState<boolean>(false);

    const [assignmentRelativeBase, setAssignmentRelativeBase] = useState<string>("");
    const [dueRelativeBase, setdueRelativeBase] = useState<string>("");
    const [expiryRelativeBase, setExpiryRelativeBase] = useState<string>("");

    const [assignAfterDaysOf, setAssignAfterDaysOf] = useState<number | "">("");
    const [dueAfterDaysOf, setDueAfterDaysOf] = useState<number | "">("");
    const [expireAfterDaysOf, setExpireAfterDaysOf] = useState<number | "">("");


    const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);

    registerLocale("en-GB", enGB);
    // Get date from default Assignment List
    const getDateFromToday = (daysToAdd: string | null): Date | null => {
        if (!daysToAdd) return null;
        const parsed = parseInt(daysToAdd);
        if (isNaN(parsed)) return null;

        const date = new Date();
        date.setDate(date.getDate() + parsed);
        return date;
    };

    // Fetch & Set defaultAssignment page data on this
    useEffect(() => {

        if (!defaultAssignmentData) return;

        if (defaultAssignmentData.chkAssignmentDates !== true) {
            resetAllAssignmentFields();
            return;
        }
        // if (!defaultAssignmentData || !defaultAssignmentData.chkAssignmentDates) return;

        // ✅ Assignment Absolute Date
        if (defaultAssignmentData.rbAbsoluteDate && defaultAssignmentData.txtDefaultAssignmnetDays) {
            const date = getDateFromToday(defaultAssignmentData.txtDefaultAssignmnetDays);
            if (date) {
                setAssignmentDateType('absolute');
                setAssignmentAbsoluteDate(date);
            }
        }
        // ✅ Due Absolute Date
        if (defaultAssignmentData.rbAbsoluteDueDate && defaultAssignmentData.txtDefaultDueDays) {
            const date = getDateFromToday(defaultAssignmentData.txtDefaultDueDays);
            if (date) {
                setDueDateType('absolute');
                setDueAbsoluteDate(date);
            }
        }

        // ✅ Expiry Absolute Date
        if (defaultAssignmentData.rbAbsoluteExpiryDate && defaultAssignmentData.txtDefaultExpDays) {
            const date = getDateFromToday(defaultAssignmentData.txtDefaultExpDays);
            if (date) {
                setExpiryDateType('absolute');
                setExpiryAbsoluteDate(date);
            }
        }

        //  No Due Date
        if (defaultAssignmentData.rbNoDueDate) {
            setDueDateType('none');
        }

        // No Expiry Date
        if (defaultAssignmentData.rbNoExpiryDate) {
            setExpiryDateType('none');
        }


        // Relative Dates
        if (defaultAssignmentData.txtAssignmentDays && defaultAssignmentData.ddlAssignmentDate) {
            setAssignmentDateType('relative');
            setAssignAfterDaysOf(parseInt(defaultAssignmentData.txtAssignmentDays));

            if (defaultAssignmentData.ddlAssignmentDate === "Hire Date") {
                setAssignmentRelativeBase("IsAssignmentBasedOnHireDate");
            } else if (defaultAssignmentData.ddlAssignmentDate === "User's Record Creation Date") {
                setAssignmentRelativeBase("IsAssignmentBasedOnCreationDate");
            }
        }

        if (defaultAssignmentData.txtDueDays && defaultAssignmentData.ddlDueDate) {
            setDueDateType('relative');
            setDueAfterDaysOf(parseInt(defaultAssignmentData.txtDueDays));

            if (defaultAssignmentData.ddlDueDate === "Hire Date") {
                setdueRelativeBase("IsDueBasedOnHireDate");
            } else if (defaultAssignmentData.ddlDueDate === "User's Record Creation Date") {
                setdueRelativeBase("IsDueBasedOnCreationDate");
            } else if (defaultAssignmentData.ddlDueDate === "Assignment Date") {
                setdueRelativeBase("IsDueBasedOnAssignDate");
            } else if (defaultAssignmentData.ddlDueDate === "Assignment Start Date") {
                setdueRelativeBase("IsDueBasedOnStartDate");
            }
        }

        if (defaultAssignmentData.txtExprDays && defaultAssignmentData.ddlExprDate) {
            setExpiryDateType('relative');
            setExpireAfterDaysOf(parseInt(defaultAssignmentData.txtExprDays));

            if (defaultAssignmentData.ddlExprDate === "Assignment Date") {
                setExpiryRelativeBase("IsExpiryBasedOnAssignDate");
            } else if (defaultAssignmentData.ddlExprDate === "Assignment Due Date") {
                setExpiryRelativeBase("IsExpiryBasedOnDueDate");
            } else if (defaultAssignmentData.ddlExprDate === "Assignment Start Date") {
                setExpiryRelativeBase("IsExpiryBasedOnStartDate");
            }
        }
    }, [defaultAssignmentData]);

    // Reset all assignment, due, expiry dates
    const resetAllAssignmentFields = () => {
        setAssignmentDateType('');
        setDueDateType('');
        setExpiryDateType('');

        setAssignmentAbsoluteDate(null);
        setDueAbsoluteDate(null);
        setExpiryAbsoluteDate(null);

        setAssignmentRelativeBase('');
        setdueRelativeBase('');
        setExpiryRelativeBase('');

        setAssignAfterDaysOf('');
        setDueAfterDaysOf('');
        setExpireAfterDaysOf('');

        setIsReassignmentChecked(false);
        setIsMailChecked(false);
    };


    useEffect(() => {
        if (resetForm) {
            resetAllAssignmentFields();
            onResetComplete?.();
        }
    }, [resetForm]);

    const handleAssignmentDateTypeChange = (type: 'absolute' | 'relative' | 'none') => {
        setAssignmentDateType(type);
        if (type !== 'absolute') { setAssignmentAbsoluteDate(null); }
        if (type !== 'relative') { setAssignAfterDaysOf(""); setAssignmentRelativeBase(""); }
    };

    const handleDueDateTypeChange = (type: string) => {
        setDueDateType(type);
        if (type !== 'absolute') { setDueAbsoluteDate(null); }
        if (type !== 'relative') { setDueAfterDaysOf(""); setdueRelativeBase(""); }
        if (type === 'none') { setDueAbsoluteDate(null); setDueAfterDaysOf(""); setdueRelativeBase(""); }
    };

    const handleExpiryDateTypeChange = (type: string) => {
        setExpiryDateType(type);
        if (type !== 'absolute') { setExpiryAbsoluteDate(null); }
        if (type !== 'relative') { setExpireAfterDaysOf(""); setExpiryRelativeBase(""); }
        if (type === 'none') { setExpiryAbsoluteDate(null); setExpireAfterDaysOf(""); setExpiryRelativeBase(""); }
    };

    const handleSetActiveTab = (tabKey: string) => {
        onSetActiveTab(tabKey);
    }

    const handleSaveAndNext = (pageIndex?: number, pageSize?: number) => {

        // ✅ Ensure Assignment Date Type is selected
        if (assignmentDateType !== 'absolute' && assignmentDateType !== 'relative') {
            setAlert({ type: "warning", message: "Set Assignment Date: Specify Absolute Date or Specify Relative Date." });
            return;
        }

        // ✅ Assignment Date validation
        if (assignmentDateType === 'absolute' && !assignmentAbsoluteDate) {
            setAlert({ type: "warning", message: "Please select an Assignment Date." });
            return;
        }

        if (assignmentDateType === 'relative' && (assignAfterDaysOf === null || assignAfterDaysOf === "" || assignAfterDaysOf < 0)) {
            setAlert({ type: "warning", message: "Please enter a valid number of days after the selected base for Assignment." });
            return;
        }

        // ✅ Ensure Due Date Type is selected (include 'none')
        if (dueDateType !== 'absolute' && dueDateType !== 'relative' && dueDateType !== 'none') {
            setAlert({ type: "warning", message: "Set Due Date: Specify Absolute Date, Relative Date, or No Due Date." });
            return;
        }

        // ✅ Due Date validation
        if (dueDateType === 'absolute') {
            if (!dueAbsoluteDate) {
                setAlert({ type: "warning", message: "Please select a Due Date." });
                return;
            }

            //  If Due Date < Assignment Date → Show Alert
            if (assignmentAbsoluteDate && dueAbsoluteDate < assignmentAbsoluteDate) {
                setAlert({ type: "warning", message: "Due Date cannot be earlier than Assignment Date." });
                return;
            }
        }

        if (dueDateType === 'relative' && (dueAfterDaysOf === null || dueAfterDaysOf === "" || dueAfterDaysOf < 0)) {
            setAlert({ type: "warning", message: "Please enter valid 'Days After' for Due Date." });
            return;
        }

        // ✅ Ensure Expiry Date Type is selected (include 'none')
        if (expiryDateType !== 'absolute' && expiryDateType !== 'relative' && expiryDateType !== 'none') {
            setAlert({ type: "warning", message: "Set Expiry Date: Specify Absolute Date, Relative Date, or No Expiry Date." });
            return;
        }

        // ✅ Expiry Date validation
        if (expiryDateType === 'absolute') {
            if (!expiryAbsoluteDate) {
                setAlert({ type: "warning", message: "Please select an Expiry Date." });
                return;
            }

            //  If Expiry Date < Due Date → Show Alert
            if (dueAbsoluteDate && expiryAbsoluteDate < dueAbsoluteDate) {
                setAlert({ type: "warning", message: "Expiry Date cannot be earlier than Due Date." });
                return;
            }

            //  Expiry Date cannot be earlier than Assignment Date
            if (assignmentAbsoluteDate && expiryAbsoluteDate < assignmentAbsoluteDate) {
                setAlert({ type: "warning", message: "Expiry Date cannot be earlier than Assignment Date." });
                return;
            }
        }

        if (expiryDateType === 'relative' && (expireAfterDaysOf === null || expireAfterDaysOf === "" || expireAfterDaysOf < 0)) {
            setAlert({ type: "warning", message: "Please enter valid 'Days After' for Expiry Date." });
            return;
        }


        const payload = {

            AssignmentDateText: assignmentAbsoluteDate
                ? `${assignmentAbsoluteDate.getFullYear()}-${String(assignmentAbsoluteDate.getMonth() + 1).padStart(2, '0')}-${String(assignmentAbsoluteDate.getDate()).padStart(2, '0')}`
                : '',

            DueDateText: dueAbsoluteDate
                ? `${dueAbsoluteDate.getFullYear()}-${String(dueAbsoluteDate.getMonth() + 1).padStart(2, '0')}-${String(dueAbsoluteDate.getDate()).padStart(2, '0')}`
                : '',

            ExpiryDateText: expiryAbsoluteDate
                ? `${expiryAbsoluteDate.getFullYear()}-${String(expiryAbsoluteDate.getMonth() + 1).padStart(2, '0')}-${String(expiryAbsoluteDate.getDate()).padStart(2, '0')}`
                : '',

            IsAssignmentBasedOnHireDate:
                assignmentDateType === 'relative' && assignmentRelativeBase === 'IsAssignmentBasedOnHireDate',
            IsAssignmentBasedOnCreationDate:
                assignmentDateType === 'relative' && assignmentRelativeBase === 'IsAssignmentBasedOnCreationDate',

            IsNoDueDate: dueDateType === 'none',
            IsDueBasedOnAssignDate: dueDateType === 'relative' && dueRelativeBase === 'IsDueBasedOnAssignDate',
            IsDueBasedOnHireDate: dueDateType === 'relative' && dueRelativeBase === 'IsDueBasedOnHireDate',
            IsDueBasedOnCreationDate: dueDateType === 'relative' && dueRelativeBase === 'IsDueBasedOnCreationDate',
            IsDueBasedOnStartDate: dueDateType === 'relative' && dueRelativeBase === 'IsDueBasedOnStartDate',


            IsNoExpiryDate: expiryDateType === 'none',
            IsExpiryBasedOnDueDate: expiryDateType === 'relative' && expiryRelativeBase === 'IsExpiryBasedOnDueDate',
            IsExpiryBasedOnAssignDate: expiryDateType === 'relative' && expiryRelativeBase === 'IsExpiryBasedOnAssignDate',
            IsExpiryBasedOnStartDate: expiryDateType === 'relative' && expiryRelativeBase === 'IsExpiryBasedOnStartDate',

            AssignAfterDaysOf: assignAfterDaysOf,
            DueAfterDaysOf: dueAfterDaysOf,
            ExpireAfterDaysOf: expireAfterDaysOf,

            IsReassignmentChecked: isReassignmentChecked,
            IsMailChecked: isMailChecked,

            IsAutoMail: false,
            IsDirectSendMail: false,

            BusinessRuleId: selectedBusinessRuleValue,
            LastModifiedById: id,
            CurrentUserID: id,
        };

        dispatch(setAssignmentProperties(payload));
        console.log("payload", payload)

        const params = {
            clientId,
            createdById: id,
            listRange: {
                pageIndex,
                pageSize,
                sortExpression: "",
            }
        } as any;
        dispatch(fetchSelectedActivitySlice(params));
        dispatch(fetchUsersSelectedSlice(params));
        console.log("params", params)
        onSetActiveTab('tabPreview');
    };


    return (
        <>
            {/* ✅ Conditionally render AlertMessage */}
            {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}

            <div className="yp-tab-content-padding">
                <div className="yp-fs-13-500 yp-text-dark-purple mb-3"><span className='yp-asteric'>*</span> Set Assignment Date:</div>
                {/* Assignment Date */}
                <div className="row">
                    <div className="col-12 col-md-12 col-lg-3">
                        <div className="form-check form-check-inline">
                            <input type="radio" name="radioSetAssignmentDate"
                                className="form-check-input"
                                id="radioAssignmentAbsoluteDate"
                                value="assignmentAbsoluteDate"
                                checked={assignmentDateType === 'absolute'}
                                onChange={() => handleAssignmentDateTypeChange('absolute')} />
                            <label className="form-check-label yp-color-dark-purple"
                                htmlFor="radioAssignmentAbsoluteDate">Specify Absolute Date</label>
                        </div>
                        {assignmentDateType === 'absolute' && (
                            <div className="mt-1 mb-3 mb-lg-0">
                                <div className="yp-width-175-px">
                                    <DatePicker
                                        name="inputAssignmentAbsoluteDate"
                                        className={`form-control yp-form-control yp-form-control-sm`}
                                        selected={assignmentAbsoluteDate}
                                        onChange={(date: Date | null) => setAssignmentAbsoluteDate(date)}
                                        placeholderText="DD/MM/YYYY"
                                        dateFormat="dd/MM/yyyy"
                                        locale="en-GB"
                                        showIcon
                                        toggleCalendarOnIconClick
                                        icon="fa-solid fa-calendar-days"
                                    />
                                </div>
                            </div>
                        )}
                    </div>
                    <div className={`col-12 ${assignmentDateType === 'relative' ? 'col-md-12 col-lg-auto' : 'col-md-12 col-lg-3'}`}>
                        <div className="form-check form-check-inline">
                            <input type="radio" name="radioSetAssignmentDate"
                                className="form-check-input"
                                id="radioAssignmentRelativeDate"
                                value="assignmentRelativeDate"
                                checked={assignmentDateType === 'relative'}
                                onChange={() => handleAssignmentDateTypeChange('relative')} />
                            <label className="form-check-label yp-color-dark-purple"
                                htmlFor="radioAssignmentRelativeDate">Specify Relative Date</label>
                        </div>
                        {assignmentDateType === 'relative' && (
                            <div className="mt-1 mb-3 mb-lg-0">
                                <div className="yp-days-after-wrapper">
                                    <div className="yp-width-60-px">
                                        <input type="number"
                                            name="inputAssignmentAbsoluteDaysAfter"
                                            value={assignAfterDaysOf}
                                            // onChange={(e) => setAssignAfterDaysOf(Number(e.target.value))}
                                            onChange={(e) => {
                                                const value = e.target.value;
                                                if (value === "") {
                                                    setAssignAfterDaysOf(""); // allow empty state
                                                } else if (/^\d+$/.test(value)) {
                                                    setAssignAfterDaysOf(Number(value)); // allow only digits
                                                }
                                            }}
                                            className={`form-control yp-form-control yp-form-control-sm`}
                                            placeholder=""
                                        />
                                    </div>
                                    <label className="form-label">Days After</label>
                                    <div className="yp-width-218-px">
                                        <select name="selectAssignmentAbsoluteDaysAfterOption"
                                            className="form-control yp-form-control yp-form-control-sm"
                                            value={assignmentRelativeBase}
                                            onChange={(e) => setAssignmentRelativeBase(e.target.value)}
                                        >
                                            <option>Select</option>
                                            <option value="IsAssignmentBasedOnHireDate">Hire Date</option>
                                            <option value="IsAssignmentBasedOnCreationDate">User's Record Creation Date</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        )}
                    </div>
                </div>
                <hr className="yp-hr yp-hr-20-0-30" />

                {/* Due Date */}
                <div className="yp-fs-13-500 yp-text-dark-purple mb-3"><span className='yp-asteric'>*</span> Set Due Date:</div>
                <div className="row">
                    <div className="col-12 col-md-12 col-lg-3">
                        <div className="form-check form-check-inline">
                            <input type="radio" name="radioSetDueDate"
                                className="form-check-input"
                                id="radioDueAbsoluteDate"
                                value={'dueAbsoluteDate'}
                                checked={dueDateType === 'absolute'}
                                onChange={() => handleDueDateTypeChange('absolute')} />
                            <label className="form-check-label yp-color-dark-purple"
                                htmlFor="radioDueAbsoluteDate">Specify Absolute Date</label>
                        </div>
                        {dueDateType === 'absolute' && (
                            <div className="mt-1 mb-3 mb-lg-0">
                                <div className="yp-width-175-px">
                                    <DatePicker
                                        name="inputDueAbsoluteDate"
                                        className={`form-control yp-form-control yp-form-control-sm`}
                                        selected={dueAbsoluteDate}
                                        onChange={(date: Date | null) => setDueAbsoluteDate(date)}
                                        placeholderText="DD/MM/YYYY"
                                        dateFormat="dd/MM/yyyy"
                                        locale="en-GB"
                                        minDate={assignmentAbsoluteDate || undefined}
                                        showIcon
                                        toggleCalendarOnIconClick
                                        icon="fa-solid fa-calendar-days"
                                    />
                                </div>
                            </div>
                        )}
                    </div>
                    <div className={`col-12 ${dueDateType === 'relative' ? 'col-md-12 col-lg-auto' : 'col-md-12 col-lg-3'}`}>
                        <div className="form-check form-check-inline">
                            <input type="radio" name="radioSetDueDate"
                                className="form-check-input"
                                id="radioDueRelativeDate"
                                checked={dueDateType === 'relative'}
                                onChange={() => handleDueDateTypeChange('relative')}
                            />
                            <label className="form-check-label yp-color-dark-purple"
                                htmlFor="radioDueRelativeDate">Specify Relative Date</label>
                        </div>
                        {dueDateType === 'relative' && (
                            <div className="mt-1 mb-3 mb-lg-0">
                                <div className="yp-days-after-wrapper">
                                    <div className="yp-width-60-px">
                                        <input type="number"
                                            name="inputDueAbsoluteDaysAfter"
                                            className={`form-control yp-form-control yp-form-control-sm`}
                                            placeholder=""
                                            value={dueAfterDaysOf}
                                            // onChange={(e) => setDueAfterDaysOf(Number(e.target.value))}
                                            onChange={(e) => {
                                                const value = e.target.value;
                                                if (value === "") {
                                                    setDueAfterDaysOf(""); // allow empty state
                                                } else if (/^\d+$/.test(value)) {
                                                    setDueAfterDaysOf(Number(value)); // allow only digits
                                                }
                                            }}
                                        />
                                    </div>
                                    <label className="form-label">Days After</label>
                                    <div className="yp-width-218-px">
                                        <select name="selectDueAbsoluteDaysAfterOption"
                                            className="form-control yp-form-control yp-form-control-sm"
                                            value={dueRelativeBase}
                                            onChange={(e) => setdueRelativeBase(e.target.value)}>
                                            <option>Select</option>
                                            <option value="IsDueBasedOnHireDate">Hire Date</option>
                                            <option value="IsDueBasedOnCreationDate">User's Record Creation Date</option>
                                            <option value="IsDueBasedOnAssignDate"> Assignment Date</option>
                                            <option value="IsDueBasedOnStartDate"> Assignment Start Date</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        )}
                    </div>
                    <div className="col-12 col-md-12 col-lg-3">
                        <div className="form-check form-check-inline">
                            <input type="radio" name="radioSetDueDate"
                                className="form-check-input"
                                id="radioDueNoDueDate"
                                value={'dueNoDueDate'}
                                checked={dueDateType === 'none'}
                                onChange={() => handleDueDateTypeChange('none')} />
                            <label className="form-check-label yp-color-dark-purple"
                                htmlFor="radioDueNoDueDate">No Due Date</label>
                        </div>
                    </div>
                </div>
                <hr className="yp-hr yp-hr-20-0-30" />

                {/* Expiry Date */}
                <div className="yp-fs-13-500 yp-text-dark-purple mb-3"><span className='yp-asteric'>*</span> Set Expiry Date:</div>
                <div className="row">
                    <div className="col-12 col-md-12 col-lg-3">
                        <div className="form-check form-check-inline">
                            <input type="radio" name="radioSetExpiryDate"
                                className="form-check-input"
                                id="radioExpiryAbsoluteDate"
                                value={'expiryAbsoluteDate'}
                                checked={expiryDateType === 'absolute'}
                                onChange={() => handleExpiryDateTypeChange('absolute')} />
                            <label className="form-check-label yp-color-dark-purple"
                                htmlFor="radioExpiryAbsoluteDate">Specify Absolute Date</label>
                        </div>
                        {expiryDateType === 'absolute' && (
                            <div className="mt-1 mb-3 mb-lg-0">
                                <div className="yp-width-175-px">
                                    <DatePicker
                                        name="inputExpiryAbsoluteDate"
                                        className={`form-control yp-form-control yp-form-control-sm`}
                                        selected={expiryAbsoluteDate}
                                        onChange={(date: Date | null) => setExpiryAbsoluteDate(date)}
                                        placeholderText="DD/MM/YYYY"
                                        dateFormat="dd/MM/yyyy"
                                        locale="en-GB"
                                        minDate={
                                            dueAbsoluteDate
                                                ? dueAbsoluteDate
                                                : assignmentAbsoluteDate
                                                    ? assignmentAbsoluteDate
                                                    : undefined
                                        }
                                        // minDate={dueAbsoluteDate || assignmentAbsoluteDate || undefined}
                                        showIcon
                                        toggleCalendarOnIconClick
                                        icon="fa-solid fa-calendar-days"
                                    />
                                </div>
                            </div>
                        )}
                    </div>
                    <div className={`col-12 ${expiryDateType === 'relative' ? 'col-md-12 col-lg-auto' : 'col-md-12 col-lg-3'}`}>
                        <div className="form-check form-check-inline">
                            <input type="radio" name="radioSetExpiryDate"
                                className="form-check-input"
                                id="radioExpiryRelativeDate"
                                value={'expiryRelativeDate'}
                                checked={expiryDateType === 'relative'}
                                onChange={() => handleExpiryDateTypeChange('relative')} />
                            <label className="form-check-label yp-color-dark-purple"
                                htmlFor="radioExpiryRelativeDate">Specify Relative Date</label>
                        </div>
                        {expiryDateType === 'relative' && (
                            <div className="mt-1 mb-3 mb-lg-0">
                                <div className="yp-days-after-wrapper">
                                    <div className="yp-width-60-px">
                                        <input type="number"
                                            name="inputExpiryAbsoluteDaysAfter"
                                            className={`form-control yp-form-control yp-form-control-sm`}
                                            placeholder=""
                                            value={expireAfterDaysOf}
                                            // onChange={(e) => setExpireAfterDaysOf(Number(e.target.value))}
                                            onChange={(e) => {
                                                const value = e.target.value;
                                                if (value === "") {
                                                    setExpireAfterDaysOf(""); // allow empty state
                                                } else if (/^\d+$/.test(value)) {
                                                    setExpireAfterDaysOf(Number(value)); // allow only digits
                                                }
                                            }}
                                        />
                                    </div>
                                    <label className="form-label">Days After</label>
                                    <div className="yp-width-218-px">
                                        <select name="selectExpiryAbsoluteDaysAfterOption"
                                            className="form-control yp-form-control yp-form-control-sm"
                                            value={expiryRelativeBase}
                                            onChange={(e) => setExpiryRelativeBase(e.target.value)}>
                                            <option>Select</option>
                                            <option value="IsExpiryBasedOnAssignDate">Assignment Date</option>
                                            <option value="IsExpiryBasedOnDueDate">Assignment Due Date</option>
                                            <option value="IsExpiryBasedOnStartDate"> Assignment Start Date</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        )}
                    </div>
                    <div className="col-12 col-md-12 col-lg-3">
                        <div className="form-check form-check-inline">
                            <input type="radio" name="radioSetExpiryDate"
                                className="form-check-input"
                                id="radioExpiryNoExpiryDate"
                                value={'expiryNoExpiryDate'}
                                checked={expiryDateType === 'none'}
                                onChange={() => handleExpiryDateTypeChange('none')} />
                            <label className="form-check-label yp-color-dark-purple"
                                htmlFor="radioExpiryNoExpiryDate">No Expiry Date</label>
                        </div>
                    </div>
                </div>
                <hr className="yp-hr yp-hr-20-0-30" />

                <div className="row">
                    <div className="col-12">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <div className="form-check form-switch">
                                    <input className="form-check-input"
                                        type="checkbox"
                                        role="switch"
                                        name="IsReassignmentChecked"
                                        id="IsReassignmentChecked"
                                        checked={isReassignmentChecked}
                                        onChange={(e) => setIsReassignmentChecked(e.target.checked)}
                                        value="" />
                                    <label className="form-check-label yp-text-primary yp-fs-13-400" htmlFor="IsReassignmentChecked"><span>Reassign selected activity to selected users</span></label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div className="row mt-3">
                    <div className="col-12">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <div className="form-check form-switch">
                                    <input className="form-check-input"
                                        type="checkbox"
                                        role="switch"
                                        name="IsMailChecked"
                                        id="IsMailChecked"
                                        onChange={(e) => setIsMailChecked(e.target.checked)}
                                        value="" />
                                    <label className="form-check-label yp-text-primary yp-fs-13-400" htmlFor="IsMailChecked"><span>Email Notification</span></label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <hr className="yp-hr yp-hr-20-0-30" />

                <div className="row">
                    <div className="col-12">
                        <div className="yp-form-button-row">
                            <button className="btn btn-primary" onClick={() => handleSaveAndNext()}>Next</button>
                            <button className="btn btn-secondary"
                                onClick={() => handleSetActiveTab("tabSelectedUsers")}>Back</button>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default DefinePropertiesContent;