// globals.d.ts
interface Window {
    API: {
      LMSInitialize(str: string): any;
      LMSGetValue(lParam: string): any;
      LMSSetValue(lParam: string, lSetValue: any): any;
      LMSCommit(lParam: string): any;
      LMSFinish(lParam: string): any;
      LMSGetLastError(): string;
      LMSGetErrorString(lErrNo: number): string;
      LMSGetDiagnostic(lErrNo: number): string;
    };
  }
  
  // // globals.d.ts
  // export interface API {
  //     LMSInitialize(str: string): any;
  //     LMSGetValue(lParam: string): any;
  //     LMSSetValue(lParam: string, lSetValue: any): any;
  //     LMSCommit(lParam: string): any;
  //     LMSFinish(lParam: string): any;
  //     LMSGetLastError(): string;
  //     LMSGetErrorString(lErrNo: number): string;
  //     LMSGetDiagnostic(lErrNo: number): string;
  //   }
