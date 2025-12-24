import React, { useEffect, useState } from 'react';
import { LookupTextValue } from '../../../../Types/commonTableTypes';
import AlertMessage from '../../../../components/shared/AlertMessage';

export interface IActivityFilterProps {
    onSubmit: (filterData: any) => void;
    onClear: () => void;
    closeFilter: () => void;
    activityTypeList: any[];
    categoryList: any[];
    subCategoryList: any[];
    onCategoryChange: (categoryId: string) => void;
    resetFilterForm?: boolean;
}

const ActivityFilter: React.FC<IActivityFilterProps> = ({
    onSubmit,
    onClear,
    closeFilter,
    activityTypeList,
    categoryList,
    subCategoryList,
    onCategoryChange,
    resetFilterForm
}) => {

    // Initial state for the form fields
    const initialFormState = {
        activityType: '',
        categoryId: '',
        subCategoryId: '',
        activityName: '',
    };

    // Form state for user details
    const [formData, setFormData] = useState(initialFormState);
    const [formErrors, setFormErrors] = useState<Record<string, string>>({});

    const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);

    /** After sumbit data, reset the form*/
    useEffect(() => {
        if (resetFilterForm) {
            setFormData(initialFormState);
        }
    }, [resetFilterForm]);


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

    // useEffect(() => {
    //     setFormData(prev => ({
    //       ...prev,
    //       categoryId: selectedCategory
    //     }));
    // }, [selectedCategory]);

    return (
        <div>
            {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}
            <form>
                <div className="row">
                    <div className="col-12 col-md-12 col-lg-4">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <select className={`form-control yp-form-control ${formErrors.activityType ? 'is-invalid' : ''}`}
                                    name="activityType"
                                    value={formData.activityType}
                                    onChange={(e) => {
                                        const value = e.target.value;
                                        handleInputChange(e);
                                    }}
                                    id="">
                                    <option value="">Select</option>
                                    {activityTypeList.map((type) => (
                                        <option key={type.lookupValue} value={type.lookupValue}>
                                            {type.lookupText}
                                        </option>
                                    ))}
                                </select>
                                <label className="form-label">Activity Type</label>
                            </div>
                            <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                {formErrors.activityType && <div className="invalid-feedback px-3">{formErrors.activityType}</div>}
                            </div>
                        </div>
                    </div>

                    <div className="col-12 col-md-12 col-lg-4">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <select className={`form-control yp-form-control ${formErrors.categoryId ? 'is-invalid' : ''}`}
                                    name="categoryId"
                                    value={formData.categoryId}
                                    onChange={(e) => {
                                        const value = e.target.value;
                                        handleInputChange(e);
                                        onCategoryChange(value);
                                    }}
                                    id="" disabled>
                                    <option value="">Select</option>
                                    {categoryList.map((type) => (
                                        <option key={type.id} value={type.id}>
                                            {type.categoryName}
                                        </option>
                                    ))}
                                </select>
                                <label className="form-label">Category</label>
                            </div>
                            <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                {formErrors.categoryId && <div className="invalid-feedback px-3">{formErrors.categoryId}</div>}
                            </div>
                        </div>
                    </div>

                    <div className="col-12 col-md-12 col-lg-4">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <select className={`form-control yp-form-control ${formErrors.subCategoryId ? 'is-invalid' : ''}`}
                                    name="subCategoryId"
                                    value={formData.subCategoryId}
                                    onChange={(e) => {
                                        const value = e.target.value;
                                        handleInputChange(e);
                                    }}
                                    id="" disabled>
                                    <option value="">Select</option>
                                    {subCategoryList.map((type) => (
                                        <option key={type.id} value={type.id}>
                                            {type.subCategoryName}
                                        </option>
                                    ))}
                                </select>
                                <label className="form-label">Sub Category</label>
                            </div>
                            <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                {formErrors.subCategoryId && <div className="invalid-feedback px-3">{formErrors.subCategoryId}</div>}
                            </div>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-12 col-md-12 col-lg-4">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <input type="text" className="form-control yp-form-control" name="activityName" value={formData.activityName}
                                    onChange={(e) => {
                                        const value = e.target.value;
                                        // Prevent leading space
                                        if (formData.activityName === '' && value.startsWith(' ')) return;
                                        handleInputChange(e);
                                    }}
                                    onKeyDown={(e) => {
                                        // Prevent space as first character
                                        if (e.key === ' ' && formData.activityName.length === 0) { e.preventDefault(); }
                                    }}
                                    id="" placeholder="" required />
                                <label className="form-label">Activity Name</label>
                            </div>
                            <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                {formErrors.activityName && <div className="invalid-feedback px-3">{formErrors.activityName}</div>}
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

export default ActivityFilter