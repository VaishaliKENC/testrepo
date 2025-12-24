import React, { useEffect, useState } from 'react';
import { ManageRowEditableUserProps } from '../../../Types/commonTableTypes';
import { Link } from 'react-router-dom';

const RowEditableCommonTable: React.FC<ManageRowEditableUserProps> = ({
  usersProfile,
  onSaveClick
  // onSearch,
  // onPageChange,
  // currentPage,
  // totalRecords,
  // pageSize,
}) => {

  const [editableRow, setEditableRow] = useState<number | null>(null); // Track editable row
  const [editedValues, setEditedValues] = useState<any>({}); // Store edited values

  // // Handle page size change
  // const handlePageSizeChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
  //   const newPageSize = parseInt(e.target.value, 10);
  //   onPageChange(1, newPageSize);
  // };

  // // Pagination logic
  // const totalPages = Math.ceil(totalRecords / pageSize);
  // const pageNumbers = [];
  // const pagesToShow = 5;

  // for (let i = 1; i <= totalPages; i++) {
  //   if (i === 1 || i === totalPages || (i >= currentPage - 2 && i <= currentPage + 2)) {
  //     pageNumbers.push(i);
  //   }
  // }

  // const handlePageClick = (page: number) => {
  //   if (page !== currentPage) {
  //     onPageChange(page, pageSize);
  //   }
  // };


  // Handles the click event to enable row editing for a specific user// Handle edit button click
  const handleEditClick = (id: number) => {
    setEditableRow(id);
    const user = usersProfile.find((user: any) => user.id === id);
    setEditedValues(user); // Initialize with existing values
  };

  // Handles input changes and updates the editedValues state
  const handleInputChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>,
    field: string
  ) => {
    const { value } = e.target;

    setEditedValues((prev: any) => {
      let parsedValue: any;

      // Convert specific fields to boolean if needed
      if (field === 'allowBlank' || field === 'include' || field === 'isDefault') {
        parsedValue = value === 'true';
      } else {
        parsedValue = value;
      }
      // return {
      //   ...prev,
      //   [field]: parsedValue,
      // };

      const updatedValues = {
        ...prev,
        [field]: parsedValue,
      };

      // Check if "isDefault" is true and "defaultValue" is empty
      // if (field === 'isDefault' && parsedValue === true && !updatedValues.defaultValue) {
      //   alert('Please enter Default Value.');
      // }

      return updatedValues;
    });
  };

  // Handles the cancel button click, resets the editable row state
  const handleCancelClick = () => {
    setEditableRow(null);
    setEditedValues({});
  };

  return (
    <div className="yp-custom-table-section">
      {/* <div className="yp-table-top-search-section">
            <div className="yp-ttss-right-section">
              <div className="d-flex flex-grow-1 flex-wrap align-items-center gap-3">
                <div className="yp-form-control-with-icon">
                  <div className="form-group">
                    <div className="yp-form-control-wrapper">
                      <input type="text" name=""
                        placeholder=""
                        className="form-control yp-form-control yp-form-control-sm"
                        onChange={(e) => onSearch(e.target.value)} />
                      <span className="yp-form-control-icon"><i className="fa fa-search"></i></span>
                      <label>Type...</label>
                    </div>
                  </div>
                </div>
              </div>
              <div className="d-flex flex-wrap gap-3">
                <div className="yp-page-show-records">
                  <div className="yp-inline-label-input">
                    <label className="form-label">Show</label>
                    <select name="" className="form-control yp-form-control yp-form-control-sm" onChange={handlePageSizeChange} value={pageSize}>
                      <option value="5">5</option>
                      <option value="10">10</option>
                      <option value="15">15</option>
                    </select>
                    <label className="form-label">Records</label>
                  </div>
                </div>
              </div>
            </div>
          </div> */}

      <div className="table-responsive" id="yp-configure-profile-table">
        <table className="table yp-custom-table" border={0}>
          <thead>
            <tr>
              <th scope="col">Field Type</th>
              <th scope="col">Min <br />Length</th>
              <th scope="col">Max <br />Length</th>
              <th scope="col">Field Data <br />Type</th>
              <th scope="col">Allow <br />Blanks</th>
              <th scope="col">Use Default <br />Value</th>
              <th scope="col">Default <br />Value</th>
              <th scope="col">Criticality</th>
              <th scope="col">Include</th>
              <th scope="col" className='text-center yp-table-action-column'>Action</th>
            </tr>
          </thead>
          <tbody>
            {usersProfile?.map((user: any) => {
              const isEditing = editableRow === user.id;
              const isLoginID = user.fieldName === 'LoginId';
              const isUserPassword = user.fieldName === 'UserPassword';

              return (
                <tr key={user.id} className={isEditing ? 'yp-table-row-active' : ''}>
                  <td>{user.fieldName}</td>
                  <td> {isEditing ? (
                    <div className='yp-width-60-px'>
                      <input
                        type="number"
                        min={1}
                        value={editedValues.minLength || ''}
                        // onChange={(e) => handleInputChange(e, 'minLength')}
                        onChange={(e) => {
                          const val = e.target.value;

                          // Block a single "0"
                          if (val === '0') return;

                          handleInputChange(e, 'minLength');
                        }}
                        className='form-control yp-form-control yp-form-control-sm'
                        onKeyDown={(e) => {
                          if (e.key === '-' || e.key === 'e' || e.key === 'E') {
                            e.preventDefault();
                          }
                        }}
                      />
                    </div>
                  ) : (
                    user.minLength
                  )}
                  </td>
                  <td> {isEditing ? (
                    <div className='yp-width-60-px'>
                      <input
                        type="number"
                        min={1}
                        value={editedValues.maxLength || ''}
                        // onChange={(e) => handleInputChange(e, 'maxLength')}
                         onChange={(e) => {
                          const val = e.target.value;

                          // Block a single "0"
                          if (val === '0') return;

                          handleInputChange(e, 'maxLength');
                        }}
                        className='form-control yp-form-control yp-form-control-sm'
                        onKeyDown={(e) => {
                          if (e.key === '-' || e.key === 'e' || e.key === 'E') {
                            e.preventDefault();
                          }
                        }}
                      />
                    </div>
                  ) : (
                    user.maxLength
                  )}</td>
                  <td>{user.fieldDataType}</td>
                  <td>
                    {isEditing ? (
                      <div className='yp-width-70-px'>
                        <select
                          disabled={isLoginID || isUserPassword}
                          value={editedValues.allowBlank?.toString() || ''}
                          onChange={(e) => handleInputChange(e, 'allowBlank')}
                          className='form-control yp-form-control yp-form-control-sm'
                        >
                          <option value="true">Yes</option>
                          <option value="false">No</option>
                        </select>
                      </div>
                    ) : (
                      user.allowBlank ? 'Yes' : 'No'
                    )}
                  </td>

                  <td>
                    {isEditing ? (
                      <div className='yp-width-70-px'>
                        <select
                          disabled={isLoginID || isUserPassword}
                          value={editedValues.isDefault?.toString() || ''}
                          onChange={(e) => handleInputChange(e, 'isDefault')}
                          className='form-control yp-form-control yp-form-control-sm'
                        >
                          <option value="true">Yes</option>
                          <option value="false">No</option>
                        </select>
                      </div>
                    ) : (
                      user.isDefault ? 'Yes' : 'No'
                    )}
                  </td>
                  <td> {isEditing ? (
                    <div className='yp-width-100-px'>
                      <input
                        disabled={isLoginID || isUserPassword}
                        type="text"
                        value={editedValues.defaultValue || ''}
                        // onChange={(e) => handleInputChange(e, 'defaultValue')}
                        onChange={(e) => {
                          const value = e.target.value;

                          // Prevent setting value that starts with a space
                          if (value.length === 1 && value === ' ') return;

                          handleInputChange(e, 'defaultValue');
                        }}
                        onKeyDown={(e) => {
                          // Prevent space as the first character
                          if (e.key === ' ' && e.currentTarget.value.length === 0) {
                            e.preventDefault();
                          }
                        }}
                        className='form-control yp-form-control yp-form-control-sm'
                      />
                    </div>
                  ) : (
                    user.defaultValue
                  )}</td>

                  <td>
                    {isEditing ? (
                      <div className='yp-width-100-px'>
                        <select
                          disabled={isLoginID}
                          value={editedValues.errorLevelType || ''}
                          onChange={(e) => handleInputChange(e, 'errorLevelType')}
                          className='form-control yp-form-control yp-form-control-sm'
                        >
                          <option value="Fatal">Fatal</option>
                          <option value="Warning">Warning</option>
                        </select>
                      </div>
                    ) : (
                      user.errorLevelType
                    )}
                  </td>
                  <td>
                    {isEditing ? (
                      <div className='yp-width-70-px'>
                        <select
                          disabled={isLoginID}
                          value={editedValues.include?.toString() || ''}
                          onChange={(e) => handleInputChange(e, 'include')}
                          className='form-control yp-form-control yp-form-control-sm'
                        >
                          <option value="true">Yes</option>
                          <option value="false">No</option>
                        </select>
                      </div>
                    ) : (
                      <span className={`badge ${user.include ? 'badge-success' : 'badge-danger'}`}>
                        {user.include ? 'Yes' : 'No'}
                      </span>
                    )}
                  </td>

                  <td className="text-center yp-table-action-column">
                    {isEditing ? (
                      <>
                        <div className='d-flex gap-3'>
                          <a className="yp-link-icon yp-link-icon-green" onClick={() => { onSaveClick(editedValues); setEditableRow(null); }}>
                            <i className="fa fa-check"></i> {/* Save icon */}
                          </a>
                          <a className=" yp-link-icon yp-link-icon-red" onClick={handleCancelClick}>
                            <i className="fa fa-times"></i> {/* Cancel icon */}
                          </a>
                        </div>
                      </>
                    ) : (
                      <a className="yp-link-icon yp-link-icon-primary" onClick={() => handleEditClick(user.id)}>
                        <i className="fa fa-pen-to-square"></i> {/* Edit icon */}
                      </a>
                    )}
                  </td>

                </tr>
              );
            })}
          </tbody>

        </table>
      </div>
    </div>
  );
};

export default RowEditableCommonTable;
