import React, { useCallback, useEffect, useMemo, useRef, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";

const AddModulesActivitiesContent: React.FC = () => {

    return(
        <>
            <div className="yp-tab-content-padding">
                <div className="row">
                    <div className="col-12 col-md-6 col-lg-4">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                {/* ${selectedManifestOrPoint === 'ManifestPackage' ? 'selected-card' : ''} */}
                                <div className={`form-check form-check-box form-check-box-white form-check-box-lg`}>                                       
                                    <input type="radio" name="modulesOrActivities"
                                    className="form-check-input"
                                    id="addActivities"
                                    value={'activities'} />
                                    <label htmlFor="addActivities" className="form-check-label w-100 cursor-pointer">
                                        <div className="yp-color-dark-purple yp-ml-0-px">
                                            Add Activities
                                        </div>
                                        <div className="yp-fs-11-400 yp-text-565656 yp-mt-5-px yp-ml-0-px">
                                            Lorem Ipsum is simply dummy text the print and typesetting industry.
                                        </div>
                                    </label>
                                </div>
                            </div >
                        </div >
                    </div >
                    <div className="col-12 col-md-6 col-lg-4">
                        <div className="form-group">
                            <div className="yp-form-control-wrapper">
                                {/* ${selectedManifestOrPoint === 'PointURL' ? 'selected-card' : ''} */}
                                <div className={`form-check form-check-box form-check-box-white form-check-box-lg`}>
                                    <input type="radio" name="modulesOrActivities"
                                        className="form-check-input"
                                        id="addModules"
                                        value={'modules'} />
                                    <label htmlFor="addModules" className="form-check-label w-100 cursor-pointer">
                                        <div className="yp-color-dark-purple yp-ml-0-px">
                                            Point IMSmanifest URL
                                        </div>
                                        <div className="yp-fs-11-400 yp-text-565656 yp-mt-5-px yp-ml-0-px">
                                            Lorem Ipsum is simply dummy text the print and typesetting industry.
                                        </div>
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div className="row">
                    <div className="col-12">
                        <div className="yp-form-button-row">
                            <button type="button" className="btn btn-secondary">Previous</button>
                            <button type="button" className="btn btn-primary">
                                Next
                            </button>                            
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}

export default AddModulesActivitiesContent;