import React, { useCallback, useEffect, useMemo, useRef, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";

const SettingsContent: React.FC = () => {

    return(
        <>
            <div className="yp-tab-content-padding">

                <div className="row">
                    <div className="col-12">
                        <div className="yp-form-button-row">
                            <button type="button" className="btn btn-secondary">Previous</button>
                            <button type="button" className="btn btn-primary">
                                Submit
                            </button>                            
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}

export default SettingsContent;