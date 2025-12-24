import React, { useState } from 'react';
import AlertMessage from '../../../components/shared/AlertMessage';
import DatePicker from "react-datepicker";
import { registerLocale } from "react-datepicker";
import { enGB } from "date-fns/locale/en-GB";

export interface IPendingApprovalsFilterProps {
    onSubmit: (filterData: any) => void;
    onClear: () => void;
    closeFilter: () => void;
}

const PendingApprovalsFilter: React.FC<IPendingApprovalsFilterProps> = ({
    onSubmit,
    onClear,
    closeFilter
}) => {

    // Initial state for the form fields
    const initialFormState = {
        firstName: "",
        lastName: "",
        email: "",
        userStatus: "",       
        country: "", 
        registrationDateFrom: "",
        registrationDateTo: "",
    };

    // Form state for user details
    const [formData, setFormData] = useState(initialFormState);
    const [formErrors, setFormErrors] = useState<Record<string, string>>({});
    const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);


    /** Handles changes in input fields and updates the state.*/
    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));

        // Validate the field
        const error = validateField(name, value);

        // Update form errors
        setFormErrors({
            ...formErrors,
            [name]: error,
        });
    };
    const validateField = (name: string, value: string | Array<string> | File | null): string => {
        if (typeof value === 'string') {
            if (value.trim() && !isValidInput(value)) {
                return "Special characters like < > ' & are not allowed.";
            }
        }
        setFormErrors({});
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

    /** Handles the form submission.*/
    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        // console.log('Form submitted:', formData);

        // Check if all fields are empty
        const isAllEmpty = Object.values(formData).every(value => value.trim() === '');

        if (isAllEmpty) {
            setAlert({ type: "warning", message: "Please enter or select at least one filter to perform a search." });
            return;
        }

                
        const from = formData.registrationDateFrom;
        const to = formData.registrationDateTo;
        if (from && !to) {
            setAlert({
                type: "error",
                message: "Please select Registration Date To"
            });
            return;
        }
        if (to && !from) {
            setAlert({
                type: "error",
                message: "Please select Registration Date To"
            });
            return;
        }


        let hasErrors = false;
        const errors = validateAllFields();
        if (Object.keys(errors).length > 0) {
            setFormErrors(errors);
            hasErrors = true;
        }
        if (hasErrors) {
            return;
        }
        onSubmit(formData);
        closeFilter();
    };

    /** Resets the form fields to their initial state
       * Calls the onClear prop to notify the parent component.
    */
    const handleClear = () => {
        setDatePickers({
            registrationDateFrom: null,
            registrationDateTo: null,
        });

        setFormData(initialFormState);
        onClear();
        setFormErrors({});
        closeFilter();
    };

    /* Date Pickers */
    const [datePickers, setDatePickers] = useState({
        registrationDateFrom: null as Date | null,
        registrationDateTo: null as Date | null,
    });

    const handleDateChange = (field: keyof typeof datePickers, value: Date | null) => {
        setDatePickers((prev) => {
            let updated = { ...prev, [field]: value };

            // If "From" is selected and "To" is empty → copy same date
            if (field === "registrationDateFrom" && value && !prev.registrationDateTo) {
                updated.registrationDateTo = value;

                // Also update formData for "To" field
                setFormData((prevForm) => ({
                    ...prevForm,
                    registrationDateTo: formatDateForPayload(value), // always string
                }));
            }
            if (field === "registrationDateTo" && value && !prev.registrationDateFrom) {
                updated.registrationDateFrom = value;

                // Also update formData for "To" field
                setFormData((prevForm) => ({
                    ...prevForm,
                    registrationDateFrom: formatDateForPayload(value), // always string
                }));
            }

            // If "From" is cleared → also clear "To"
            if (field === "registrationDateFrom" && !value) {
                updated.registrationDateTo = null;
                setFormData((prevForm) => ({
                    ...prevForm,
                    registrationDateFrom: "",
                    registrationDateTo: "",
                }));
                return updated;
            }

            return updated;
        });

        // Always update formData for the selected field
        setFormData((prev) => ({
            ...prev,
            [field]: value ? formatDateForPayload(value) : "",
        }));
    };

    const isValidDateRange = (from: Date | null, to: Date | null): boolean => {
        if (!from || !to) return true; // allow empty
        return to.getTime() >= from.getTime(); // To must be >= From
    };

    if (
        !isValidDateRange(datePickers.registrationDateFrom, datePickers.registrationDateTo)
    ) {
        setAlert({
            type: "error",
            message: "Please ensure each 'To Date' is greater than or equal to its corresponding 'From Date'.",
            autoClose: true,
        });
        return null;
    }

    const formatDateForPayload = (date: Date | null): string => {
        if (!date) return "";

        const yyyy = date.getFullYear();
        const mm = String(date.getMonth() + 1).padStart(2, '0'); // Months are 0-based
        const dd = String(date.getDate()).padStart(2, '0');

        return `${mm}/${dd}/${yyyy}`;
    };

    return (
        <div>
            {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}
            <form>
                <div className="row">
                    <div className="col-12 col-md-12 col-lg-6">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <input type="text" className="form-control yp-form-control" value={formData.firstName}
                                    // onChange={handleInputChange} 
                                    onChange={(e) => {
                                        const value = e.target.value;
                                        // Prevent leading space
                                        if (formData.firstName === '' && value.startsWith(' ')) return;
                                        handleInputChange(e);
                                    }}
                                    onKeyDown={(e) => {
                                        // Prevent space as first character
                                        if (e.key === ' ' && formData.firstName.length === 0) { e.preventDefault(); }
                                    }}
                                    name="firstName" id="" placeholder="" required />
                                <label className="form-label">First Name</label>
                            </div>
                            <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                {formErrors.firstName && <div className="invalid-feedback px-3">{formErrors.firstName}</div>}
                            </div>
                        </div>
                    </div>
                    <div className="col-12 col-md-12 col-lg-6">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <input type="text" className="form-control yp-form-control" value={formData.lastName}
                                    // onChange={handleInputChange} 
                                    onChange={(e) => {
                                        const value = e.target.value;
                                        // Prevent leading space
                                        if (formData.lastName === '' && value.startsWith(' ')) return;
                                        handleInputChange(e);
                                    }}
                                    onKeyDown={(e) => {
                                        // Prevent space as first character
                                        if (e.key === ' ' && formData.lastName.length === 0) { e.preventDefault(); }
                                    }}
                                    name="lastName" id="" placeholder="" required />
                                <label className="form-label">Last Name</label>
                            </div>
                            <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                {formErrors.lastName && <div className="invalid-feedback px-3">{formErrors.lastName}</div>}
                            </div>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-12 col-md-12 col-lg-6">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <input type="text" className="form-control yp-form-control" name="email" value={formData.email}
                                    onChange={(e) => {
                                        const value = e.target.value;
                                        // Prevent leading space
                                        if (formData.email === '' && value.startsWith(' ')) return;
                                        handleInputChange(e);
                                    }}
                                    onKeyDown={(e) => {
                                        // Prevent space as first character
                                        if (e.key === ' ' && formData.email.length === 0) { e.preventDefault(); }
                                    }}
                                    id="" placeholder="" required />
                                <label className="form-label">Email Address</label>
                            </div>
                            <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                {formErrors.email && <div className="invalid-feedback px-3">{formErrors.email}</div>}
                            </div>
                        </div>
                    </div>

                    <div className="col-12 col-md-12 col-lg-6">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <select className="form-control yp-form-control" value={formData.userStatus} onChange={handleInputChange} name="userStatus" id="">
                                    <option value="">Select</option>
                                    <option value="Pending">Pending</option>
                                    <option value="Approved">Approved</option>
                                    <option value="Rejected">Rejected</option>
                                </select>
                                <label className="form-label">Status</label>
                            </div>
                        </div>
                    </div>

                    {/* <div className="col-12 col-md-12 col-lg-6">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <input type="text" className="form-control yp-form-control" name="loginId" id="" placeholder="" value={formData.loginId}
                                    // onChange={handleInputChange}
                                    onChange={(e) => {
                                        const value = e.target.value;
                                        // Prevent leading space
                                        if (formData.loginId === '' && value.startsWith(' ')) return;
                                        handleInputChange(e);
                                    }}
                                    onKeyDown={(e) => {
                                        // Prevent space as first character
                                        if (e.key === ' ' && formData.loginId.length === 0) { e.preventDefault(); }
                                    }}
                                    required
                                />
                                <label className="form-label">Login ID</label>
                            </div>
                            <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                {formErrors.loginId && <div className="invalid-feedback px-3">{formErrors.loginId}</div>}
                            </div>
                        </div>
                    </div> */}
                </div>
                <div className="row">
                    <div className="col-12 col-md-12 col-lg-6">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <select className="form-control yp-form-control" value={formData.country} onChange={handleInputChange} name="country" id="">
                                    <option value="">Select</option>
                                    <option value="India">India</option>
                                    <option value="US">US</option>
                                    <option value="Japan">Japan</option>
                                </select>
                                <label className="form-label">Country</label>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div className="row">
                    <div className="col-12">
                        <div className="mb-2">
                            <span className="yp-text-13-500 yp-text-primary">Registration Date:</span>
                        </div>
                    </div>
                    <div className="col-12 col-md-12 col-lg-6">
                        <div className="form-group">
                            <div className="d-flex flex-wrap gap-2 row-gap-1 flex-column flex-sm-row align-items-sm-center">
                                <div><span className="yp-text-13-400 yp-color-565656">From:</span></div>
                                <div className="yp-datepicker-grow">
                                    <div className="yp-form-control-wrapper">
                                            <DatePicker
                                                selected={datePickers.registrationDateFrom}
                                                onChange={(date) => handleDateChange("registrationDateFrom", date)}
                                                placeholderText="DD/MM/YYYY"
                                                dateFormat="dd/MM/yyyy"
                                                locale="en-GB"
                                                className="form-control yp-form-control yp-form-control-sm"
                                                showIcon
                                                toggleCalendarOnIconClick
                                                icon="fa-solid fa-calendar-days"
                                                maxDate={datePickers.registrationDateTo || undefined}
                                            />
                                    </div>
                                </div>
                            </div>                            
                        </div>
                    </div>
                    <div className="col-12 col-md-12 col-lg-6">
                        <div className="form-group">
                            <div className="d-flex flex-wrap gap-2 row-gap-1 flex-column flex-sm-row align-items-sm-center">
                                <div><span className="yp-text-13-400 yp-color-565656">To:</span></div>
                                <div className="yp-datepicker-grow">
                                    <div className="yp-form-control-wrapper">
                                            <DatePicker
                                                selected={datePickers.registrationDateTo}
                                                onChange={(date) => handleDateChange("registrationDateTo", date)}
                                                placeholderText="DD/MM/YYYY"
                                                dateFormat="dd/MM/yyyy"
                                                locale="en-GB"
                                                className="form-control yp-form-control yp-form-control-sm"
                                                showIcon
                                                toggleCalendarOnIconClick
                                                icon="fa-solid fa-calendar-days"
                                                minDate={datePickers.registrationDateFrom || undefined}
                                            />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                

                <div className="row">
                    <div className="col-12">
                        <div className="yp-form-button-row">
                            <button type="button" className="btn btn-primary" onClick={handleSubmit}>Submit</button>
                            <button type="reset" className="btn btn-secondary" onClick={handleClear}>Clear</button>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    )

}

export default PendingApprovalsFilter