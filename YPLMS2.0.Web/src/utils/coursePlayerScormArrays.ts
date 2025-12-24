// src/utils/scormArrays.ts
// export class ManifestNode {
//     constructor(public params: string[]) {}
//   }
import { CSCO } from "../../src/pages/admin/adm-coursePlayer/course-ScormLibraries/Master";

  // export class CSCO {
  //   constructor(public params: string[]) {}
  // }

  export class ManifestNode {
    identifier: string;
    title: string;
    winTarget: string;
    prerequisites: string;
    datafromlms: string;
    masteryscore: string;
    maxtimeallowed: string;
    timelimitaction: string;
    identifierref: string;
    scormtype: string;
    type: string;
    base: string;
    href: string;
  
    constructor(lArr: string[]) {
      this.identifier = lArr[0];
      this.title = lArr[1];
      this.winTarget = lArr[2];
      this.prerequisites = lArr[3];
      this.datafromlms = lArr[4];
      this.masteryscore = lArr[5];
      this.maxtimeallowed = lArr[6];
      this.timelimitaction = lArr[7];
      this.identifierref = lArr[8];
      this.scormtype = lArr[9];
      this.type = lArr[10];
      this.base = lArr[11];
      this.href = lArr[12];
    }
  }
  
  // Arrays to store data
  export let arrManifestNodes: ManifestNode[] = [];
  //export const arrManifestNodes: ManifestNode[] = [];
  //export const arrSCO: CSCO[] = [];


  /**
   * Add a new ManifestNode to arrManifestNodes
   */
  export const addManifestNode = (params: string[]) => {
     const identifier = params[0];
      if (!arrManifestNodes.find(node => node.identifier === identifier)) {
        arrManifestNodes.push(new ManifestNode(params));
      }
    // if(arrManifestNodes.length == 0)
    // {
    //   arrManifestNodes.push(new ManifestNode(params));
    //   console.log("arrManifestNodes", arrManifestNodes);
    // }

  };
  
  /**
   * Add a new CSCO to arrSCO
   */
  // export const addCSCO = (params: string[]) => {
  //   arrSCO.push(new CSCO(params));
  //   console.log("***********arrSCO",arrSCO);
  // };
  
  /**
   * Clear all data in arrays (optional, for cleanup)
   */
  export const clearScormData = () => {
    arrManifestNodes.length = 0;
    //arrSCO.length = 0;
  };


export const getScoStatus = (status: string): string => {
  switch (status.toLowerCase()) {
    case "completed":
    case "passed":
      return "Completed";
    case "incomplete":
    case "browsed":
    case "in progress":
      return "In Progress";
    default:
      return "Not Started";
  }
};

// Object to store lesson status keyed by lesson identifier
export const lessonStatuses: Record<string, string> = {};

// Simulated tracking writer (replace with your backend API or storage logic)
export const writeScoTracking = (tracking: any): void => {
  console.log("Writing SCO tracking:", tracking);
};
