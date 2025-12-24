import React, { useCallback, useEffect, useMemo, useRef, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";

const BasicDetailsContent: React.FC = () => {

    /* Keywords input control */
    const [keywords, setKeywords] = useState<string[]>([]);
    const [inputValue, setInputValue] = useState("");

    const handleKeyDown = (e: any) => {
        if (e.key === "Enter" && inputValue.trim() !== "") {
        e.preventDefault();
        if (!keywords.includes(inputValue.trim())) {
            setKeywords([...keywords, inputValue.trim()]);
        }
        setInputValue("");
        }
    };

    const removeKeyword = (keyword: string) => {
        setKeywords(keywords.filter((k) => k !== keyword));
    };
    /* END Keywords input control */

    return(
        <>
            <div className="yp-tab-content-padding">
                <div className="row">
                    <div className="col-12 col-md-12 col-lg-12">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <input type="text"
                                    className={`form-control yp-form-control`}
                                    name="courseName"
                                    id=""
                                    placeholder="" />
                                <label className="form-label"><span className='yp-asteric'>*</span>Learning Path Name</label>
                                {/* <input type="hidden" name="id" value={formData.id} /> */}
                            </div>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-12 col-md-12 col-lg-12">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <div className="yp-keyword-input-wrapper">
                                    <div className="yp-keyword-container yp-form-control">
                                        {keywords.map((keyword, index) => (
                                        <div className="yp-keyword-chip" key={index}>
                                            <span>{keyword}</span>
                                            <button onClick={() => removeKeyword(keyword)}>×</button>
                                        </div>
                                        ))}
                                        <input
                                            type="text"
                                            className={`form-control yp-form-control`}
                                            value={inputValue}
                                            placeholder={keywords.length === 0 ? "Type keyword and press enter" : ""}
                                            onChange={(e) => setInputValue(e.target.value)}
                                            onKeyDown={handleKeyDown}
                                        />
                                        <label className="form-label">Keywords</label>
                                    </div>
                                </div>
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
                                    name="courseDescription"
                                    maxLength={200}
                                    id=""
                                    placeholder=""></textarea>
                                <label className="form-label">Description</label>
                            </div>
                        </div>
                    </div>
                </div>

                <div className="row">
                    <div className="col-12 col-md-12 col-lg-6">
                        <div className="form-group">
                            <label className="form-label"><span className="yp-text-14-400 yp-text-dark-purple">Upload Thumbnail</span></label>
                            <div className="yp-form-control-wrapper yp-form-control-without-label">
                                <div className="yp-form-control-file-upload">
                                    <input type="file"
                                        name="lpThumbnailImage"
                                        className="form-control yp-form-control yp-form-control-with-button"
                                        id="lpThumbnailImage"
                                        placeholder=""
                                        // onChange={handleThumbnailImageChange}
                                        // ref={fileUploadThumbnailRef} 
                                    />

                                    <button type="button"
                                        className="btn btn-primary yp-btn-inside-input"
                                        //onClick={handleUploadThumbnail}
                                    >Browse</button>

                                    <input type="hidden" 
                                        name="thumbnailImgRelativePath" 
                                        //value={selectedCourseThumbnailUrl} 
                                    />
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
                    {/* <div className="col-12 col-md-12 col-lg-6">
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
                    </div> */}
                </div>
                
                <div className="row">
                    <div className="col-12 col-md-12 col-lg-12">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                <div className="form-check form-switch">
                                    <input className="form-check-input" type="checkbox" role="switch"
                                        name="isEnforceSequencing"
                                        id="isEnforceSequencing"
                                        //value={formData.isSendEmail.toString()}
                                        //checked={formData.isSendEmail} // Bind `checked` for checkboxes
                                        //onChange={handleInputChange}
                                    />
                                    <label className="form-check-label yp-text-primary" htmlFor="isEnforceSequencing"><span>Enforce Sequencing</span></label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div className="row">
                    <div className="col-12">
                        <div className="yp-form-button-row">
                            <button className="btn btn-primary">
                                Next
                            </button>
                            <button type="reset" className="btn btn-secondary">Cancel</button>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}

export default BasicDetailsContent;