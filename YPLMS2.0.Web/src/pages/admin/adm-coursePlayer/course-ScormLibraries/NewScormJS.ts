import { SCORMApi } from "./SCORMApi";
import { RTEMaster } from './RTEMaster';

import store  from '../../../../redux/store'; // Adjust the path based on your file structure
import { gDataObj, GlobalData } from '../../../../Types/coursePlayerScormType/globalDataType';

// Access the Redux state
const state = store.getState();

// Access a specific slice of the state
const globalData = state.globalData;

const rtemaster = new RTEMaster(globalData);
  // Initialize variables
  let API = new SCORMApi();
  let gStudentId: string =gDataObj.gStudentId;
  let gManifestId: string = gDataObj.gManifestId;
  let gContentPath: string = gDataObj.gContentPath;
  const gUserDataURL = `ControlFrame.aspx?StudentId=${gStudentId}&ManifestId=${gManifestId}`;
  
  // Variables
  let gURL: string | undefined;
  
  // Function to open SCO asset
   function fOpenScoAsset(lUrl: string, lNodeId: string, lTypeOfNode: string): void {
    rtemaster.fSetNode("Identifier", lNodeId);
  
    if (lTypeOfNode === "asset") {
     // fCheckAndCreateSCOBlockForAsset();
    } else {
        rtemaster.fSetNode("Status", "NotInitialized");
    }
  
    gURL = lUrl;
    // parent.ContentSrvFrame.location.href = "LMSNewScormDisplaySCO.htm";
  
    // if (!parent.IsSingleLaunchSco()) {
    //   parent.document.getElementById("frmsetLaunch").rows = "0,0,0,*";
    // } else if (window.parent.name === "course") {
    //   parent.document.getElementById("frmsetLaunch").rows = "0,0,0,*";
    // } else {
    //   parent.document.getElementById("frmsetLaunch").rows = "50,0,0,*";
    // }
  }
  