import { RTEMaster } from '../course-ScormLibraries/RTEMaster';
import store, { AppDispatch } from "../../../../redux/store";

import { GlobalData } from '../../../../Types/coursePlayerScormType/globalDataType';


// class clAPIAdapter {
//     LMSInitialize: (str: string) => string;
//     LMSGetValue: (lParam: string) => string;
//     LMSSetValue: (lParam: string, lSetValue: string) => string;
//     LMSCommit: (lParam: string) => string;
//     LMSFinish: (lParam: string) => string;
//     LMSGetLastError: () => number;
//     LMSGetErrorString: (lErrNo: string) => string;
//     LMSGetDiagnostic: (lErrNo: string) => string;

//     constructor() {
//         this.LMSInitialize = LMSInitialize;
//         this.LMSGetValue = LMSGetValue;
//         this.LMSSetValue = LMSSetValue;
//         this.LMSCommit = LMSCommit;
//         this.LMSFinish = LMSFinish;
//         this.LMSGetLastError = LMSGetLastError;
//         this.LMSGetErrorString = LMSGetErrorString;
//         this.LMSGetDiagnostic = LMSGetDiagnostic;
//     }
// }

const state = store.getState();
const RTEMasterClassObj = new RTEMaster(state.updatedGlobalData);
//const APIAdapter = new clAPIAdapter();

export class LMSRTEWrapperJS{
    LMSInitialize(str:string){
    
        return RTEMasterClassObj.LMSIntInitialize(str);
    }
    LMSGetValue(lParam:string){
      
        return RTEMasterClassObj.LMSIntGetValue(lParam);
    }
    
    LMSSetValue(lParam:string, lSetValue:string){
        return RTEMasterClassObj.LMSIntSetValue(lParam, lSetValue);
    }

    closetop() {
        //top.close();
    }

    LMSFinish(lParam:string) {
        let gTimerId:any;
        let retLMSFinish:any;
        try {
            retLMSFinish = RTEMasterClassObj.LMSIntFinish(lParam);
        }
        //catch(e) { parent.alert(e); }
        catch (e) {
            try {
              if (window.top && typeof window.top.alert === 'function') {
                window.top.alert(e);
              } else {
                window.alert("Error: " + e);
              }
            } catch (err) {
              console.error("Error with top alert:", err);
            }
          }
        // if (!parent.IsSingleLaunchSco()) {
        //     parent.ExitServerFrame.fnExitContentPlayer(true);
        //     gTimerId = window.setTimeout("closetop()", 5000);
        // }
        // else {
        //     //top.close();
            
        //     gTimerId = window.setTimeout("closetop()", 5000);  //Solution for ipad closing window issue
        // }
        return retLMSFinish;
    }

    LMSCommit(lParam:string) {
        return RTEMasterClassObj.LMSIntCommit(lParam);
    }
    
    LMSGetLastError(){
        var retVal= parseInt(RTEMasterClassObj.LMSIntGetLastError(),10);
        if(isNaN(retVal))
            return 0;
        else
            return retVal;
    }
    
    LMSGetErrorString(lErrNo:any){
        return RTEMasterClassObj.LMSIntGetErrorString(lErrNo);
    }
    
    LMSGetDiagnostic(lErrNo:any){
        return RTEMasterClassObj.LMSIntGetDiagnostic(lErrNo);
    }
}
