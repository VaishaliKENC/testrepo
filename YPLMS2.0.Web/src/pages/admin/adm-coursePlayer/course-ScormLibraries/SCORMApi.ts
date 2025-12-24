// scormApi.ts
import { LMSRTEWrapperJS } from "../course-ScormPlayer/LMSRTEWrapperJS";
import CoursePage from '../course-ScormPlayer/CoursePage';

export class SCORMApi {
  
  private _errorCode: number = 0; // Internal numeric error code
  private LMSRTEWrapperJSObj = new LMSRTEWrapperJS();


  InitializeGData(gDataObj: any) {
    console.log("scorm InitializeGData "+gDataObj.gLearnerId);
  }

  LMSInitialize(str: string): any {
    console.log("Initialize is working" )
    return this.LMSRTEWrapperJSObj.LMSInitialize(str);
  }

  LMSGetValue(lParam: string): any {
    return this.LMSRTEWrapperJSObj.LMSGetValue(lParam);
  }

  LMSSetValue(lParam: string, lSetValue: any): any {
    return this.LMSRTEWrapperJSObj.LMSSetValue(lParam, lSetValue);
  }

  LMSCommit(lParam: string): any {
    return this.LMSRTEWrapperJSObj.LMSCommit(lParam);
  }

  LMSFinish(lParam: string): any {
    let retLMSFinish;
    try {
      retLMSFinish = this.LMSRTEWrapperJSObj.LMSFinish(lParam);
    } catch (e) {
      alert(`Error finishing SCORM session: ${e}`);
    }

    if (!this.isSingleLaunchSco()) {
      this.exitServerFrame();
      setTimeout(() => this.closeTop(), 5000);
    } else {
      setTimeout(() => this.closeTop(), 5000);
    }

    return retLMSFinish;
  }


  LMSGetLastError(): string { // Correct return type: string
    return this._errorCode.toString();
}

  LMSGetErrorString(lErrNo: number): string {
    return this.LMSRTEWrapperJSObj.LMSGetErrorString(lErrNo) || "Unknown error";
  }

  LMSGetDiagnostic(lErrNo: number): string {
    return this.LMSRTEWrapperJSObj.LMSGetDiagnostic(lErrNo) || "No diagnostic available";
  }

  private closeTop(): void {
    //window.top.close();
  }

  private exitServerFrame(): void {
    console.log("Exiting server frame...");
  }

  private isSingleLaunchSco(): boolean {
    return false; // Replace with actual condition if available
  }
}
