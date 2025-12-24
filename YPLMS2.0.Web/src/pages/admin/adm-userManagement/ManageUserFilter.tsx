import React, { useState } from 'react';
import AlertMessage from '../../../components/shared/AlertMessage';

export interface IManageUserFilterProps {
    onSubmit: (filterData: any) => void;
    onClear: () => void;
    closeFilter: () => void;
}

const ManageUserFilter: React.FC<IManageUserFilterProps> = ({
    onSubmit,
    onClear,
    closeFilter
}) => {

    // Initial state for the form fields
    const initialFormState = {
        FirstName: '',
        LastName: '',
        LoginId: '',
        Email: '',
        Active: '',
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
        setFormData(initialFormState);
        onClear();
        setFormErrors({});
        closeFilter();
    };

    return (
        <div>
            {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}
            <form>
                <div className="row">
                    <div className="col-12 col-md-12 col-lg-4">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <input type="text" className="form-control yp-form-control" value={formData.FirstName}
                                    // onChange={handleInputChange} 
                                    onChange={(e) => {
                                        const value = e.target.value;
                                        // Prevent leading space
                                        if (formData.FirstName === '' && value.startsWith(' ')) return;
                                        handleInputChange(e);
                                    }}
                                    onKeyDown={(e) => {
                                        // Prevent space as first character
                                        if (e.key === ' ' && formData.FirstName.length === 0) { e.preventDefault(); }
                                    }}
                                    name="FirstName" id="" placeholder="" required />
                                <label className="form-label">First Name</label>
                            </div>
                            <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                {formErrors.FirstName && <div className="invalid-feedback px-3">{formErrors.FirstName}</div>}
                            </div>
                        </div>
                    </div>
                    <div className="col-12 col-md-12 col-lg-4">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <input type="text" className="form-control yp-form-control" value={formData.LastName}
                                    // onChange={handleInputChange} 
                                    onChange={(e) => {
                                        const value = e.target.value;
                                        // Prevent leading space
                                        if (formData.LastName === '' && value.startsWith(' ')) return;
                                        handleInputChange(e);
                                    }}
                                    onKeyDown={(e) => {
                                        // Prevent space as first character
                                        if (e.key === ' ' && formData.LastName.length === 0) { e.preventDefault(); }
                                    }}
                                    name="LastName" id="" placeholder="" required />
                                <label className="form-label">Last Name</label>
                            </div>
                            <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                {formErrors.LastName && <div className="invalid-feedback px-3">{formErrors.LastName}</div>}
                            </div>
                        </div>
                    </div>
                    <div className="col-12 col-md-12 col-lg-4">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <input type="text" className="form-control yp-form-control" name="LoginId" id="" placeholder="" value={formData.LoginId}
                                    // onChange={handleInputChange}
                                    onChange={(e) => {
                                        const value = e.target.value;
                                        // Prevent leading space
                                        if (formData.LoginId === '' && value.startsWith(' ')) return;
                                        handleInputChange(e);
                                    }}
                                    onKeyDown={(e) => {
                                        // Prevent space as first character
                                        if (e.key === ' ' && formData.LoginId.length === 0) { e.preventDefault(); }
                                    }}
                                    required
                                />
                                <label className="form-label">Login ID</label>
                            </div>
                            <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                {formErrors.LoginId && <div className="invalid-feedback px-3">{formErrors.LoginId}</div>}
                            </div>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-12 col-md-12 col-lg-4">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <input type="text" className="form-control yp-form-control" name="Email" value={formData.Email}
                                    onChange={(e) => {
                                        const value = e.target.value;
                                        // Prevent leading space
                                        if (formData.Email === '' && value.startsWith(' ')) return;
                                        handleInputChange(e);
                                    }}
                                    onKeyDown={(e) => {
                                        // Prevent space as first character
                                        if (e.key === ' ' && formData.Email.length === 0) { e.preventDefault(); }
                                    }}
                                    id="" placeholder="" required />
                                <label className="form-label">Email Address</label>
                            </div>
                            <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                {formErrors.Email && <div className="invalid-feedback px-3">{formErrors.Email}</div>}
                            </div>
                        </div>
                    </div>
                    <div className="col-12 col-md-12 col-lg-4">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <select className="form-control yp-form-control" value={formData.Active} onChange={handleInputChange} name="Active" id="">
                                    <option value="">Select</option>
                                    <option value="true">Active</option>
                                    <option value="false">Inactive</option>
                                </select>
                                <label className="form-label">Is Active?</label>
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

export default ManageUserFilter