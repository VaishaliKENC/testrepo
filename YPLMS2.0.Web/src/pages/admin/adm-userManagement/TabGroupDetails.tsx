import React, { useCallback, useEffect, useMemo, useRef, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";

const TabGroupDetails: React.FC = () => {

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
                                    name=""
                                    id=""
                                    placeholder="" />
                                <label className="form-label"><span className='yp-asteric'>*</span>User Group Name</label>
                                {/* <input type="hidden" name="id" value={formData.id} /> */}
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
                                    name=""
                                    maxLength={200}
                                    id=""
                                    placeholder=""></textarea>
                                <label className="form-label">Add a brief description to explain this group's purpose</label>
                            </div>
                        </div>
                    </div>
                </div>
                
            </div>
        </>
    )
}

export default TabGroupDetails;