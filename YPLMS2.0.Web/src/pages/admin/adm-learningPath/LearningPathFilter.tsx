import React, { useState } from 'react';
import AlertMessage from '../../../components/shared/AlertMessage';
import { Autocomplete } from '../../../components/shared';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from '../../../redux/store';
import { fetchAdminUsers } from '../../../redux/Slice/admin/learningPathSlice';
import { useAppDispatch } from '../../../hooks';
import { ILearningPathFilterPayload } from '../../../Types/learningPathTypes';
import AutoAsyncSelect from '../../../components/shared/CommonComponents/AutoAsyncSelect';

export interface IManageLearningPathFilterProps {
    onSubmit: (filterData: any) => void;
    onClear: () => void;
    closeFilter: () => void;
    handleSelectChange?: (selectedOption: Record<string, any>) => void;
}

const LearningPathFilter: React.FC<IManageLearningPathFilterProps> = ({
    onSubmit,
    onClear,
    closeFilter,
    handleSelectChange
}) => {

    // Initial state for the form fields
    const initialFormState = {
        curriculumName: '',
        createdByName: '',
        modifiedByName: '',
        isUsedT: '',
        Active: '',
    };

    // Form state for user details
    const [formData, setFormData] = useState(initialFormState);
    const [formErrors, setFormErrors] = useState<Record<string, string>>({});
    const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);

    const dispatch = useAppDispatch();
    const clientId = useSelector((state: RootState) => state.auth.clientId);
    const id = useSelector((state: RootState) => state.auth.id);
    const [pageSize, setPageSize] = useState(0);
    const [adminUsers, setAdminUsers] = useState<any[]>([]);
    // const [selectedUser, setSelectedUser] = useState<{ label: string; value: string } | null>(null);



    // handle search
    const handleUserSearch = async (keyword: string) => {
        if (!id || !clientId) return;

        const params: ILearningPathFilterPayload = {
            id,
            clientId,
            listRange: {
                pageIndex: 0,
                pageSize,
                sortExpression: "LastModifiedDate desc",
            },
            keyWord: keyword,
        };
        console.log("params", params)

        // const response = await dispatch(fetchAdminUsers(params)).unwrap();
        // setAdminUsers(response.adminList || []);  

        try {
            const response = await dispatch(fetchAdminUsers(params)).unwrap();
            console.log("first", response.adminList);
            setAdminUsers(response.adminList || []); // ✅ store locally
        } catch (error: any) {
            console.error("Error fetching admin users:", error);

            // Normalize error message
            const errorMessage =
                typeof error === "string"
                    ? error
                    : error?.msg || error?.message || "Failed to fetch admin users.";

            // If you want to show alert:
            setAlert({
                type: "error",
                message: errorMessage,
            });
        }
    };

    const autocompleteItems = adminUsers.map((u: any) => ({
        label: u.name,
        value: u.id,
    }));

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


    // const handleAutocomple = (option: any) => {
    //     setFormData((prev) => ({
    //         ...prev,
    //         createdByName: option?.value || ""
    //     }));
    //     setSelectedUser(option || null);
    // };


    const handleAutoCompleteSelect = (option: { label: string; value: string } | null) => {
        setFormData((prev) => ({
            ...prev,
            createdByName: option?.value || ""
        }));
    };

    const selectedUser = formData.createdByName
        ? autocompleteItems.find(u => u.value === formData.createdByName) || null
        : null;




    return (
        <div>
            {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}
            <form>
                <div className="row">
                    <div className="col-12 col-md-12 col-lg-12">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <input type="text" className="form-control yp-form-control" value={formData.curriculumName}
                                    // onChange={handleInputChange} 
                                    onChange={(e) => {
                                        const value = e.target.value;
                                        // Prevent leading space
                                        if (formData.curriculumName === '' && value.startsWith(' ')) return;
                                        handleInputChange(e);
                                    }}
                                    onKeyDown={(e) => {
                                        // Prevent space as first character
                                        if (e.key === ' ' && formData.curriculumName.length === 0) { e.preventDefault(); }
                                    }}
                                    name="curriculumName" id="" placeholder="" required />
                                <label className="form-label">Learning Path Name</label>
                            </div>
                            <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                {formErrors.curriculumName && <div className="invalid-feedback px-3">{formErrors.curriculumName}</div>}
                            </div>
                        </div>
                    </div>
                    {/* <div className="col-12 col-md-12 col-lg-4">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <input type="text" className="form-control yp-form-control" name="modifiedByName" id="" placeholder="" value={formData.modifiedByName}
                                    // onChange={handleInputChange}
                                    onChange={(e) => {
                                        const value = e.target.value;
                                        // Prevent leading space
                                        if (formData.modifiedByName === '' && value.startsWith(' ')) return;
                                        handleInputChange(e);
                                    }}
                                    onKeyDown={(e) => {
                                        // Prevent space as first character
                                        if (e.key === ' ' && formData.modifiedByName.length === 0) { e.preventDefault(); }
                                    }}
                                    required
                                />
                                <label className="form-label">Modified By</label>
                            </div>
                            <div className='d-flex justify-content-start flex-wrap justify-content-sm-between flex-sm-nowrap g-2'>
                                {formErrors.modifiedByName && <div className="invalid-feedback px-3">{formErrors.modifiedByName}</div>}
                            </div>
                        </div>
                    </div> */}

                </div>
                <div className="row">
                    <div className="col-12 col-md-12 col-lg-6">
                        <div className="yp-form-control-with-icon yp-form-control-with-icon-right-side">
                            <div className="form-group">
                                {/* <Autocomplete
                                    items={autocompleteItems || []}
                                    handleSelectChange={handleAutocomple}
                                    placeholder="Created by"
                                    title="Type Created by Name..."
                                    // onSearch={handleUserSearch}
                                    value={selectedUser}
                                /> */}
                                <AutoAsyncSelect
                                    items={autocompleteItems}
                                    value={selectedUser}
                                    placeholder="Created by"
                                    title="Type Created by Name..."
                                    onSearch={handleUserSearch}
                                    handleSelectChange={handleAutoCompleteSelect}
                                />
                            </div>
                        </div>
                    </div>
                    <div className="col-12 col-md-12 col-lg-3">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <select className="form-control yp-form-control" value={formData.isUsedT} onChange={handleInputChange} name="isUsedT" id="">
                                    <option value="">Select</option>
                                    <option value="Yes">Yes</option>
                                    <option value="No">No</option>
                                </select>
                                <label className="form-label">Assigned</label>
                            </div>
                        </div>
                    </div>
                    <div className="col-12 col-md-12 col-lg-3">
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

export default LearningPathFilter