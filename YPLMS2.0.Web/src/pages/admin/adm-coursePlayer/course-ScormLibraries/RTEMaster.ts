// RTEMaster.ts
import { ScormValidations } from "./ScormValidations";
import { gArrFunctionsState } from './Master';
import { gArrCurrentStatus, gMasterTemplate } from './Master';
import { gErrorLookup } from './Master';
import { CSCO } from "./Master"
import store, { AppDispatch } from '../../../../redux/store'; 
//import { gDataObj } from "../../types/globalData";
import { stat } from "fs";
import { arrManifestNodes } from "../../../../utils/coursePlayerScormArrays";
import axios from "axios";
import  courseScromApi from '../../../../api/courseScromApi';
import { sendDataToLMS } from "../../../../redux/Slice/coursePlayerScormSlice/courseLaunchSlice";

export const arrSCO: CSCO[] = [];


//console.log("************ : " + gArrFunctionsState["NotInitialized"]);
//console.log("************ Check in ts file : "+processGlobalData.gLearnerId);

const ScormValidationsClassObj = new ScormValidations();

// Globally accessible arrays (consider a better approach like a dedicated state management if the application scales)
//const arrSCO: CSCO[] = [];
//const arrManifestNodes: any[] = []; // Type this appropriately based on your manifest node structure
let sLearnerLanguageId: string = "";
let gTrackScoreSettingFromLMS: string = "";
let gTrackResponseSettingFromLMS: string = "";

const sessionId = store.getState().coursePlayer?.courseLaunch?.contentModuleSession?.sessionId;
console.log("sessionId", sessionId)
export class RTEMaster {
	private currentStatus: { [key: string]: string };
	private functionsState: { [key: string]: string };
	private gDataObj: any;
	// private contentModuleData: any;

	constructor(globalData: any = {}) {
		// Get initial state from Redux store
		this.updateGlobalData();

		// Subscribe to Redux store changes
		store.subscribe(() => {
			this.updateGlobalData();
		});

		console.log("RTEMaster initialized with data:", this.gDataObj);
		console.log(" In RTE ************ : " + gArrFunctionsState["NotInitialized"] + " gArrCurrentStatus : ", gArrCurrentStatus);
		// Initialize the constants in the RTEMaster class
		// debugger;
		// gArrCurrentStatus["Status"] = "NotInitialized";
		this.currentStatus = gArrCurrentStatus;
		this.functionsState = gArrFunctionsState;
	}
	debug: boolean = false;


	private updateGlobalData() {
		const state = store.getState();
		this.gDataObj = state.updatedGlobalData;
		// this.contentModuleData = state.globalData.contentModule;
	}


	fSetError(lErrNo: any, lErrStatus: string = "") {
		//gArrCurrentStatus["LastError"] =  lErrNo;
		//lErrStatus = lErrStatus || "false";
	}

	fResetError() {
		this.fSetError("0");
	}

	fCheckErrorStatus() {
		if (this.fGetNode("LastError") == "0") {
			return "true";
		} else {
			return "false";
		}
	}

	/* LMS function start here */
	LMSIntInitialize(lParam: any): "true" | "false" {
		if (!this.fCheckForEmptyString(lParam)) {
			console.log("exiting Initialize, may not call with parameter value");
			return "false";
		}

		if (!this.fIsFunctionAllowed("LMSInitialize")) {
			console.log("exiting Initialize, function not allowed now");
			return "false";
		}
		this.fResetError();
		this.fModifyCoreEntry();
		this.fCheckAndCreateSCOBlock();
		this.fSetNode("Status", "Initialized"); // Update state

		console.log("LMS Initialized");
		return this.fCheckErrorStatus();
	}

	LMSIntGetValue(lParam: any): any {
		console.log("GetValue: lParam = '" + lParam + "'");
		if (!this.fIsFunctionAllowed("LMSGetValue")) {
			console.log("exiting GetValue, function not allowed now");
			return "";
		}

		this.fResetError();
		const data = this.fGetSetUserData(lParam, "", "Get");
		console.log("finished GetValue, returning data: '" + data + "'");
		return data;
	}

	LMSIntSetValue(lParam: any, lData: any) {
		// debugger;
		console.log("Setvalue: lParam = '" + lParam + "', lData = '" + lData + "'");
		console.log("In LMSIntSetValue : " + this.currentStatus.Status);
		//Below condition is an excpetion to handle Tracking Score based on the LMS level settings

		if (lParam == "cmi.core.score" && gTrackScoreSettingFromLMS == "false") {
			console.log("exiting SetValue, skipping score");
			return "true";
		}

		//Modified the API by Rohit on 30th Oct 2009 for Encora LMS
		//Below condition is an excpetion to handle Tracking Response based on the LMS level settings
		if (lParam == "cmi.interactions" && gTrackResponseSettingFromLMS == "false") {
			console.log("exiting SetValue, skipping interactions");
			return "true";
		}

		if (!this.fIsFunctionAllowed("LMSSetValue")) {
			console.log("exiting SetValue, function not allowed now");
			return "false";
		}

		if (lParam == "cmi.core.lesson_status" && lData == "not attempted") {
			console.log("exiting SetValue, not allowed to set status not attempted");
			this.fSetError("405");
			return "false";
		}

		this.fResetError();

		var compareData = lData;
		if (lParam == "cmi.core.lesson_status") {
			compareData = this.fGetSetUserData(lParam, "", "Get");
		}

		this.fGetSetUserData(lParam, lData, "Set");

		if (compareData != lData) {
			console.log("changed lesson status from '" + compareData + "' to '" + lData + "', going to send data");
			this.fSendRequest("SaveCurrentSco");
		}
		console.log("finished SetValue");
		return this.fCheckErrorStatus();
	}

	LMSIntFinish(lParam: any) {
		console.log("Finish: lParam = '" + lParam + "'");
		debugger;
		if (!this.fCheckForEmptyString(lParam)) {
			console.log("exiting Finish, may not call with parameter value");
			return "false";
		}

		if (!this.fIsFunctionAllowed("LMSFinish")) {
			console.log("exiting Finish, function not allowed now");
			return "false";
		}

		this.fModifySCOParamsBeforeLMSFinish();
		this.fResetError();
		this.LMSIntCommit("");
		this.fSetNode("Status", "Finished");

		const LMSIntFinish = this.fCheckErrorStatus();
		this.fSetLMSFinishReturn(LMSIntFinish);
		console.log("finished Finish");
		return LMSIntFinish;
	}

	LMSIntCommit(lParam: any) {
		console.log("Commit: lParam = '" + lParam + "'");

		if (!this.fCheckForEmptyString(lParam)) {
			console.log("exiting Commit, may not call with parameter value");
			return "false";
		}

		if (!this.fIsFunctionAllowed("LMSCommit")) {
			console.log("exiting Commit, function not allowed now");
			return "false";
		}

		this.fResetError();

		if (!this.fSendRequest("SaveCurrentSco")) {
			this.fSetError("101");
		}
		console.log("finished Commit");
		return this.fCheckErrorStatus();
	}

	LMSIntGetLastError() {

		if (!this.fValidateFinishedState("LMSGetLastError")) {
			return "false";
		}

		return this.fGetNode("LastError");
	}

	LMSIntGetErrorString(lErrNo: any) {

		if (!this.fValidateFinishedState("LMSGetErrorString")) {
			return "false";
		}

		return this.fGetErrorString(lErrNo);
	}

	LMSIntGetDiagnostic(lErrNo: string) {
		if (!this.fValidateFinishedState("LMSGetDiagnostic")) {
			return "false";
		}
		return this.fGetErrorDiagnostic(lErrNo);
	}
	/* LMS function end here */

	fGetErrorDiagnostic(lErrNo: string) {
		if (gErrorLookup[lErrNo]) {
			return gErrorLookup[lErrNo][1];
		}
		return "";
	}

	fGetErrorString(lErrNo: string) {
		if (gErrorLookup[lErrNo]) {
			return gErrorLookup[lErrNo][0];
		}
		return "";
	}

	fValidateFinishedState(lFunctionName: any) {
		if (!this.fIsFunctionAllowed(lFunctionName)) {
			return false;
		}

		if (this.fGetNode("Status") === "Finished") {
			if (this.fGetLMSFinishReturn() == "true") {
				return false;
			}
		}
		return true;
	}

	fGetLMSFinishReturn() {
		return gArrFunctionsState["LMSFinishReturn"];
	}

	fSetLMSFinishReturn(lValue: any) {
		gArrFunctionsState["LMSFinishReturn"] = lValue;
	}

	fModifySCOParamsBeforeLMSFinish() {
		this.fModifyLessonStatus();
		this.fModifyTotalTime();
	}

	//This function will be called before LMSFinish to add session_time
	//to total_time
	fModifyTotalTime() {
		// debugger;
		let sTotalTime = this.fDirectGetDataFormUserDataXML("cmi__core__total_time");
		let sSessionTime = this.fDirectGetDataFormUserDataXML("cmi__core__session_time");

		sTotalTime = this.fAddTime(sSessionTime, sTotalTime);
		if (sTotalTime != "false") {
			this.fDirectSetDataToUserDataXML("cmi__core__total_time", sTotalTime);
		}
	}

	//This function will be called before LMSFinish to correct the lesson_status
	//depending on mastery-score and raw-score if the test is for credit
	fModifyLessonStatus() {
		let sLessonStatus = this.fDirectGetDataFormUserDataXML("cmi__core__lesson_status");

		if (sLessonStatus == "not attempted") {
			sLessonStatus = "completed";
			this.fDirectSetDataToUserDataXML("cmi__core__lesson_status", sLessonStatus);
		}

		if (sLessonStatus == "completed") {
			const sCredit = this.fDirectGetDataFormUserDataXML("cmi__core__credit");
			// const sMasteryScore = this.fDirectGetDataFormUserDataXML("cmi__student_data__mastery_score", sMasteryScore);
			const sMasteryScore = this.fDirectGetDataFormUserDataXML("cmi__student_data__mastery_score");
			if (sCredit != "" && sMasteryScore != "") {
				const sRowScore = this.fDirectGetDataFormUserDataXML("cmi__core__score__raw");
				if (sRowScore != "") {
					if (parseFloat(sRowScore) < parseFloat(sMasteryScore)) {
						this.fDirectSetDataToUserDataXML("cmi__core__lesson_status", "failed");
					}
					else {
						this.fDirectSetDataToUserDataXML("cmi__core__lesson_status", "passed");
					}
				}
			}
		}
	}

	fAddTime(lTime1: string, lTime2: string) {
		const arrTime1 = lTime1.split(":");
		const arrTime2 = lTime2.split(":");

		let extraMinute = 0;
		let extraHour = 0;

		if (arrTime1.length != 3 || arrTime2.length != 3) {
			return "false";
		}

		let fSecond: any = parseFloat(arrTime1[2]) + parseFloat(arrTime2[2]);
		if (fSecond >= 60) {
			fSecond = fSecond - 60;
			extraMinute = 1;
		}

		let fMinute: any = parseInt(arrTime1[1]) + parseInt(arrTime2[1]) + extraMinute;
		if (fMinute >= 60) {
			fMinute = fMinute - 60;
			extraHour = 1;
		}

		let fHour: any = parseInt(arrTime1[0]) + parseInt(arrTime2[0]) + extraHour;

		let fSecondArr = fSecond.toString().split(".");

		if (fSecondArr.length == 1) {
			if (fSecondArr[0].length == 1) {
				fSecond = "0" + fSecondArr[0] + ".00";
			}
			else {
				fSecond = fSecondArr[0] + ".00";
			}
		}
		else {
			if (fSecondArr[0].length == 1) {
				fSecondArr[0] = "0" + fSecondArr[0];
			}
			if (fSecondArr[1].length == 1) {
				fSecondArr[1] = fSecondArr[1] + "0";
			}

			fSecond = fSecondArr[0] + "." + fSecondArr[1];
		}

		fMinute = fMinute.toString();
		if (fMinute.length == 1) {
			fMinute = "0" + fMinute;
		}

		fHour = fHour.toString();
		if (fHour.length == 1) {
			fHour = "0" + fHour;
		}

		if (fSecond.length > 5) {
			fSecond = fSecond.substring(0, 5);
		}
		return fHour + ":" + fMinute + ":" + fSecond;
	}

	fSendRequest(lCase: string): boolean {
		let sURL = `${window.location.origin}/ControlFrame`;// gUserDataURL;

	

		switch (lCase) {
			case "Initialise":
				break;
			case "SaveCurrentSco":
				let sData: any = this.fSCOToXML(this.fGetNode("Identifier"));
				//sData = "<Root><Case>SaveCurrentSco</Case><StudentId>" + this.gDataObj.gStudentId; + "</StudentId><ManifestId>" + this.gDataObj.clientId; + "</ManifestId><UserDataXML>" + sData + "</UserDataXML><ClientId>" + this.gDataObj.gManifestId; + "</ClientId><SessionId>" + this.gDataObj.sessionId; + "</SessionId></Root>";
				// sData = "<Root><Case>SaveCurrentSco</Case><StudentId>" + this.gDataObj.gStudentId + "</StudentId><ManifestId>" + this.gDataObj.gManifestId + "</ManifestId><UserDataXML>" + sData + "</UserDataXML><ClientId>" + this.gDataObj.clientId + "</ClientId><SessionId>" + this.gDataObj.sessionId + "</SessionId></Root>";
				console.log("sData", sData)
				this.fSendDataToLMSNEWServer(sData, sURL);
				break;
			default:
				console.warn('Invalid case:', lCase);
		}

		return true;
	}

	// 	//debugger;
	fSCOToXML(lIdentifier: any, sendToAPI: boolean = false) {
		let oSCO = this.fGetSCO(lIdentifier);
		const sendDataLMSPayloadData = {
			identifier: lIdentifier,
			studentId: this.fGetValueFromSCO(oSCO, "cmi__core__student_id") || "",
			studentName: this.fGetValueFromSCO(oSCO, "cmi__core__student_name") || "",
			lessonLocation: this.fGetValueFromSCO(oSCO, "cmi__core__lesson_location") || "",
			credit: this.fGetValueFromSCO(oSCO, "cmi__core__credit") || "no-credit",
			lessonStatus: this.fGetValueFromSCO(oSCO, "cmi__core__lesson_status") || "unknown",
			entry: this.fGetValueFromSCO(oSCO, "cmi__core__entry") || "",
			rawScore: Number(this.fGetValueFromSCO(oSCO, "cmi__core__score__raw")) || 0,
			minScore: Number(this.fGetValueFromSCO(oSCO, "cmi__core__score__min")) || 0,
			maxScore: Number(this.fGetValueFromSCO(oSCO, "cmi__core__score__max")) || 0,
			exit: this.fGetValueFromSCO(oSCO, "cmi__core__exit") || "",
			sessionTime: this.fGetValueFromSCO(oSCO, "cmi__core__session_time") || "0000:00:00",
			totalTime: this.fGetValueFromSCO(oSCO, "cmi__core__total_time") || "0000:00:00",
			lessonMode: this.fGetValueFromSCO(oSCO, "cmi__core__lesson_mode") || "normal",
			suspendData: this.fGetValueFromSCO(oSCO, "cmi__suspend_data") || "",
			launchData: this.fGetValueFromSCO(oSCO, "cmi__launch_data") || "",
			comments: this.fGetValueFromSCO(oSCO, "cmi__comments") || "",
			commentsFromLms: this.fGetValueFromSCO(oSCO, "cmi__comments_from_lms") || "",
			masteryScore: Number(this.fGetValueFromSCO(oSCO, "cmi__student_data__mastery_score")) || 0,
			maxTimeAllowed: Number(this.fGetValueFromSCO(oSCO, "cmi__student_data__max_time_allowed")) || "0000:00:00",
			timeLimitAction: this.fGetValueFromSCO(oSCO, "cmi__student_data__time_limit_action") || "",
			totalpages: Number(this.fGetValueFromSCO(oSCO, "cmi__core__totalpages")) || 0,
			completedpages: Number(this.fGetValueFromSCO(oSCO, "cmi__core__completedpages")) || 0,
			courseId: this.gDataObj.gManifestId || "",
			clientId: this.gDataObj.clientId || "",
			audio: Boolean(this.fGetValueFromSCO(oSCO, "cmi__student_preference__audio")),
			language: this.fGetValueFromSCO(oSCO, "cmi__student_preference__language") || "en",
			speed: Number(this.fGetValueFromSCO(oSCO, "cmi__student_preference__speed")) || 1,
			text: this.fGetValueFromSCO(oSCO, "cmi__student_preference__text") || "",
			interactionCount: Number(this.fGetValueFromSCO(oSCO, "cmi__interactions__count")) || 0,
			sessionId: sessionId
		};


		console.log("payload sendDataLMSPayloadData", sendDataLMSPayloadData);

		if (sendToAPI) {
			this.fSendDataToLMSNEWServer(JSON.stringify(sendDataLMSPayloadData), 'YOUR_API_ENDPOINT_URL');
		}

		return sendDataLMSPayloadData;
	}


	fGetValueFromSCO(lSCO: any, lParam: any): any {

		if (!lSCO) {
			return "";
		}

		//eval("var sRetVal = lSCO." + lParam);
		let sRetVal = lSCO[lParam];

		if (this.fIsUndefined(sRetVal)) {
			sRetVal = "";
		}

		return sRetVal;
	}

	fGetCount(lSCO: any, lUserNodePath: any) {
		const sPreText = lUserNodePath.substr(0, lUserNodePath.indexOf("___count"));
		let sPostText
		if (lUserNodePath.indexOf("correct_responses") != -1) {
			sPostText = "pattern";
		}
		else {
			sPostText = "id";
		}

		let i = -1;
		let sRetVal;
		for (; ;) {
			//eval("sRetVal = lSCO." + sPreText + "___" + (++i) + "__" + sPostText);
			sRetVal = lSCO[sPreText + "___" + (++i) + "__" + sPostText];
			if (this.fIsUndefined(sRetVal)) {
				break;
			}
		}

		return i;
	}

	fSendDataToLMSNEWServer = async (lData: any, lURL: any) => {

		try {
			const parsedData = typeof lData === 'string' ? JSON.parse(lData) : lData;
			console.log('Sending data to API:', parsedData);
			
			const resultAction = await store.dispatch(sendDataToLMS(parsedData));

			if (sendDataToLMS.fulfilled.match(resultAction)) {
			  console.log('API Response:', resultAction.payload);
			} else {
			  console.error('API Call Error:', resultAction.payload);
			}
		  } catch (error) {
			console.error('Error parsing lData or sending to API:', error);
		  }

		// 	courseScromApi.sendDataToLMS(parsedData)
		// 		.then(response => {
		// 			console.log('API Response:', response.data);
		// 		})
		// 		.catch(error => {
		// 			console.error('API Call Error:', error);
		// 		});
		// } catch (error) {
		// 	console.error('Error parsing lData or sending to API:', error);
		// }
	};


	fGetSetUserData(lParam: string, lData: any, lGetSet: string): string {

		if (lParam.indexOf(".") == -1) {
			this.fSetError("201");
			return "";
		}

		/*
			- Clonning User data xml to achieve the functionality to roll back
			  the changes if required
			- Make any changes to this clone node and if no error ocurred then 
			  copy it back to the user data xml

			set xmlUserDataClone = CreateObject("Microsoft.xmldom")
			xmlUserDataClone.async = false
			call xmlUserDataClone.loadXML(xmlUserData.xml)
		*/

		const arrParams: any[] = lParam.split(".");

		let userData_RunningPath = "";
		let sPreviousObjectivePath = "";              	//to check against userdata xml for objective sequence
		let template_RunningPath = "";                	//to check against template xml
		let currentNodeName = "";
		let bObjective = false;

		for (let i = 0; i < arrParams.length; i++) {
			if (this.fIsNumeric(arrParams[i])) {
				sPreviousObjectivePath = userData_RunningPath.substr(2) + "__" + "_" + (arrParams[i] - 1);
				arrParams[i] = "_" + arrParams[i];
				currentNodeName = "_number";             //to check against template xml
				bObjective = true;
			}
			else {
				currentNodeName = arrParams[i];
				if (this.fIsNumeric(currentNodeName.substr(0, 1))) {
					this.fSetError("201");
					return "";
				}
			}

			template_RunningPath = template_RunningPath + "__" + currentNodeName;     //to check against template xml

			userData_RunningPath = userData_RunningPath + "__" + arrParams[i];  			//to check against userdata xml
		}

		template_RunningPath = template_RunningPath.substr(2);
		userData_RunningPath = userData_RunningPath.substr(2);

		if (lGetSet == "Set") {
			this.fSetNodeValue(template_RunningPath, userData_RunningPath, lData, bObjective, sPreviousObjectivePath);
		}
		else {
			return this.fGetNodeValue(template_RunningPath, userData_RunningPath);
		}

		const bERROR = this.fGetNode("LastError");
		if (bERROR != "0") {
			return "";
		}

		return "";
	}

	fGetNodeValue(lTemplateNodePath: string, lUserNodePath: string) {
		//fGetNodeValue = "false"

		//correct Context
		if (this.fCheckError("201", lTemplateNodePath)) {
			return "";
		}

		//IsImplemented
		if (this.fCheckError("401", lTemplateNodePath)) {
			return "";
		}

		//Can Read
		if (this.fCheckError("404", lTemplateNodePath)) {
			return "";
		}

		let sRetVal = "";

		if (this.fGetLastNodeInContext(lTemplateNodePath) == "_children") {
			//if not XMLLib.fgetvalue(xmlTemplateDoc, lTemplateNodePath, lGetValue) then
			sRetVal = this.fGetValueFromMasterTemplate(lTemplateNodePath + ".Text");

			if (this.fIsUndefined(sRetVal)) {
				this.fSetError("401");
			}
		}
		else {
			if (this.fGetLastNodeInContext(lTemplateNodePath) == "_count") {
				//inscrease the ._count
				this.fUpdateCount(lUserNodePath);

				//call XMLLib.fgetvalue(xmlUserDataClone, lUserNodePath, lGetValue)
				sRetVal = this.fDirectGetDataFormUserDataXML(lUserNodePath);
				if (sRetVal == "") {
					sRetVal = "0";
				}
			}
			else {
				sRetVal = this.fDirectGetDataFormUserDataXML(lUserNodePath);
				if (sRetVal == "") {
					this.fSetError("201");
				}
			}
		}

		this.fSetError("0");
		return sRetVal;
	}

	fSetNodeValue(lTemplateNodePath: any, lUserNodePath: any, lSetValue: any, lbObjective: any, lsPreviousObjectivePath: any): boolean {
		//debugger;
		//incorrect Context
		if (this.fCheckError("201", lTemplateNodePath)) {
			return false;
		}

		//not Implemented
		if (this.fCheckError("401", lTemplateNodePath)) {
			return false;
		}

		//reserved keywords
		if (this.fCheckError("402", lTemplateNodePath)) {
			return false;
		}

		//Can Write
		if (this.fCheckError("403", lTemplateNodePath)) {
			return false;
		}

		//check sequence for objectives, interactions and interactions.objectives
		if (!this.fCheckForObjectiveSequence(lsPreviousObjectivePath, lbObjective)) {
			return false;
		}

		if (!this.fValidateSetValue(lTemplateNodePath, lSetValue)) {
			return false;
		}

		//call XMLLib.fCreateFirstContext(xmlUserDataClone, lUserNodePath, oNode)
		//append to the previously set comments
		if (lTemplateNodePath == "cmi__comments") {
			lSetValue = this.fDirectGetDataFormUserDataXML(lUserNodePath) + lSetValue;
		}

		//call XMLLib.fAddCdataToNode(xmlUserDataClone, oNode, lSetValue)
		this.fDirectSetDataToUserDataXML(lUserNodePath, lSetValue);

		//inscrease the ._count if data for new objective is added to the userdata
		//call fUpdateCount(lTemplateNodePath)

		this.fSetError("0");
		return true;
	}

	fCheckForObjectiveSequence(lPreviousObjectivePath: string, lbObjective: any): boolean {
		if (!lbObjective) {
			return true;
		}

		if (lPreviousObjectivePath.indexOf("_-1") != -1) {
			return true;
		}

		const sRetVal = this.fDirectGetDataFormUserDataXML(lPreviousObjectivePath + "__id");
		if (sRetVal == "") {
			this.fSetError("201");
			return false;
		}
		return true;
	}

	fValidateSetValue(lContext: string, lVal: any) {
		if (!this.fSpChecks(lContext, lVal)) {
			return false;
		}

		const sRetVal = this.fGetValueFromMasterTemplate(lContext + ".ValidationType");
		let sLookupName = "";

		if (sRetVal.toUpperCase() == "CMIVOCABULARY") {
			sLookupName = this.fGetValueFromMasterTemplate(lContext + ".LookupName");
		}

		if (!ScormValidationsClassObj.fExtResValidate(sRetVal, lVal, sLookupName, this.fGetValueFromMasterTemplate(lContext + ".LValue"), this.fGetValueFromMasterTemplate(lContext + ".HValue"))) {
			this.fSetError("405", "true");
			return false;
		}

		return true;
	}

	fSpChecks(lContext: string, lVal: any): boolean {
		switch (lContext) {
			case "cmi__core__score__raw":
			case "cmi__core__score__max":
			case "cmi__core__score__min":
			case "cmi__objectives___number__score__raw":
			case "cmi__objectives___number__score__max":
			case "cmi__objectives___number__score__min":
				if (lVal == "") {
					return true;
				}
				if (ScormValidationsClassObj.fExtResValidate("CMIDECIMAL", lVal, "", "", "")) {
					//value must be 0 to 100
					if (parseFloat(lVal) < 0 || parseFloat(lVal) > 100) {
						this.fSetError("405", "true");
						return false;
					}
				}
				break;
		}
		return true;
	}

	fGetLastNodeInContext(lXPath: string): any {
		const ArrNodes = lXPath.split("__");
		return ArrNodes[ArrNodes.length - 1];
	}

	fGetValueFromMasterTemplate(lParam: any): any {
		try {
			// Use bracket notation for dynamic property access
			const sRetVal = gMasterTemplate[lParam];
			return this.fIsUndefined(sRetVal) ? "" : sRetVal;
		} catch (error) {
			// Handle potential errors like invalid property access
			console.error("Error accessing property:", lParam, error);
			return ""; // Or a specific error value if needed
		}
	}

	fUpdateCount(lUserNodePath: string) {
		//cmi__objectives___count										n.id
		//cmi__interactions___count									n.id
		//cmi__interactions__n__objectives___count				n.id
		//cmi__interactions__n__correct_responses___count		n.pattern

		const sPreText = lUserNodePath.substr(0, lUserNodePath.indexOf("___count"));
		let sPostText;
		if (lUserNodePath.indexOf("correct_responses") != -1) {
			sPostText = "pattern";
		}
		else {
			sPostText = "id";
		}

		let i = -1;
		for (; ;) {
			if (!this.fNodeExistsInCurrentSco(sPreText + "___" + (++i) + "__" + sPostText)) {
				break;
			}
		}

		this.fDirectSetDataToUserDataXML(lUserNodePath, i.toString());
	}

	fNodeExistsInCurrentSco(lParam: string): boolean {
		const oSCO = this.fGetCurrentSCO();
		if (oSCO) {
			return lParam in oSCO;
		}
		return false;
	}

	fCheckError(lCase: string, lContext: any) {
		//eval("var oTemplateNode = gMasterTemplate." + lContext);
		const oTemplateNode = gMasterTemplate[lContext];

		let sErrorNumber = "0";
		const sLastNode = this.fGetLastNodeInContext(lContext);

		switch (lCase) {
			case "201":											//correct Context
				if (!this.fIsCorrectContext(lContext)) {
					sErrorNumber = "201";

					if (sLastNode == "_children") {
						sErrorNumber = "202";
					}

					if (sLastNode == "_count") {
						sErrorNumber = "203";
					}
				}
				break;

			case "202":
				break;

			case "203":
				if (sLastNode == "_count") {
					sErrorNumber = "203";
				}
				break;

			case "401":           							//IsImplemented
				if (!this.fIsImplemented(lContext)) {
					sErrorNumber = "401";
				}
				break;

			case "404":           							//Can Read
				if (!this.fCanRead(oTemplateNode)) {
					sErrorNumber = "404";
				}
				break;

			case "402":
				if (sLastNode == "_children" || sLastNode == "_count" || sLastNode == "_version") {
					sErrorNumber = "402";
				}
				break;

			case "403":
				if (!this.fCanWrite(oTemplateNode)) {
					sErrorNumber = "403";
				}
				break;
		}

		if (sErrorNumber == "0") {
			return false;
		}
		else {
			this.fSetError(sErrorNumber);
			return true;
		}
	}

	//verifies the context passed to the function is implemented or not
	fIsImplemented(lContext: string): boolean {
		const sRetVal = this.fGetValueFromMasterTemplate(lContext + ".Implemented");
		return (sRetVal != "false");
	}

	fCanWrite(lNode: any): boolean {
		var sRetVal = lNode.Mode;

		if (sRetVal == "Write" || sRetVal == "Both") {
			return true;
		}

		return false;
	}

	fCanRead(lNode: any): boolean {
		var sRetVal = lNode.Mode;
		if (sRetVal == "Read" || sRetVal == "Both") {
			return true;
		}

		return false;
	}

	fIsCorrectContext(lContext: any): any {
		//eval("var oTemplateNode = gMasterTemplate." + lContext);
		const oTemplateNode = gMasterTemplate[lContext];
		return (!this.fIsUndefined(oTemplateNode));
	}

	fIsFunctionAllowed(lFunctionName: string): boolean {
		const sRetVal = gArrCurrentStatus["Status"];
		//if (!sRetVal || sRetVal === "") {
		if (!sRetVal) {
			return false;
		}
		const sFunctions = gArrFunctionsState[sRetVal];
		if (sFunctions.indexOf(lFunctionName) !== -1) {
			return true;
		}

		if (sRetVal === "NotInitialized") {
			this.fSetError("301");
		} else {
			this.fSetError("101");
		}

		return false;
	}

	fCheckForEmptyString(lString: string) {
		if (lString !== "") {
			this.fSetError("201"); // Invalid argument error
			return false;
		}
		return true;
	}

	//This function will be called from LMSInitialize to set the core.entry
	//depending on core.exit
	fModifyCoreEntry() {
		const sExitVal = this.fDirectGetDataFormUserDataXML("cmi__exit");

		if (sExitVal === "suspend") {
			this.fDirectSetDataToUserDataXML("cmi__entry", "resume");
		} else {
			this.fDirectSetDataToUserDataXML("cmi__entry", "");
		}
	}

	fDirectGetDataFormUserDataXML(lParam: string): string {
		let sRetVal: string | undefined;
		const oSCO = this.fGetCurrentSCO();
		if (oSCO) {
			sRetVal = oSCO[lParam];
			if (this.fIsUndefined(sRetVal)) {
				sRetVal = "";
			}
		}
		return sRetVal || "";
	}

	fDirectSetDataToUserDataXML(lParam: string, lData: any) {
		const oSCO = this.fGetCurrentSCO();
		if (oSCO) {
			oSCO[lParam] = lData;
		}
	}

	fCheckAndCreateSCOBlock() {
		//const state = store.getState();  
		//this.gDataObj = state.updatedGlobalData;

		const sIdentifier = this.fGetNode("Identifier");
		let oSCO = this.fGetCurrentSCO();

		//this.updateGlobalData(); // Fetch latest Redux state
		console.log("Current gDataObj:", this.gDataObj);
		if (!oSCO) {
		
			oSCO = new CSCO();
			oSCO.identifier = sIdentifier;
			if (sIdentifier != "") {
				//set Learner id
				oSCO.cmi__core__student_id = this.gDataObj.gStudentId;// gStudentId;

				//set Learner Name
				oSCO.cmi__core__student_name = this.gDataObj.gLearnerName;// gLearnerName;

				//set Manager Email 
				oSCO.cmi__core__manageremail = this.gDataObj.gManagerEmail;// gManagerEmail;

				//set Total pages 
				oSCO.cmi__core__totalpages = this.gDataObj.gTotalNoOfPages;// gTotalNoOfPages;

				//set Total pages 
				oSCO.cmi__core__completedpages = this.gDataObj.gNoOfCompletedPages;// gNoOfCompletedPages;

				//set Student Email 
				oSCO.cmi__core__studentemail = this.gDataObj.gStudentEmail;// gStudentEmail;
				oSCO.cmi__student_preference__language = sLearnerLanguageId;
			}
			arrSCO.push(oSCO); // Use push to add to the array

			//addCSCO(oSCO);
			console.log("arrSCO from RTEMASTER : ", arrSCO);
			//set data from Manifest
			this.fSetDataFromManifest("cmi__launch_data", "datafromlms");
			this.fSetDataFromManifest("cmi__student_data__mastery_score", "masteryscore");
			this.fSetDataFromManifest("cmi__student_data__max_time_allowed", "adlcp:maxtimeallowed");
			this.fSetDataFromManifest("cmi__student_data__time_limit_action", "adlcp:timelimitaction");
		}
	}

	fSetDataFromManifest(lXPath: string, lParam: string) {
		const retVal = this.fGetDataFromManifest(lParam);
		if (retVal !== "") {
			this.fDirectSetDataToUserDataXML(lXPath, retVal);
		}
	}

	fGetDataFromManifest(lParam: string): string {
		const sNodeId = this.fGetNode("Identifier");
		const oNode = this.fGetItemNodeObject(sNodeId);
		return this.fGetValue(oNode, lParam);
	}

	fGetItemNodeObject(lIdentifier: string): any | null { //  Type any needs to be replaced with proper Manifest Node Type.
		const arrNodes = arrManifestNodes; //NTrees["COOLjsTree"].Nodes;
		for (let i = 0; i < arrNodes.length; i++) {
			if (arrNodes[i].identifier === lIdentifier) {
				return arrNodes[i];
			}
		}
		return null;
	}

	fGetValue(lNode: any, lParam: string): string { //  Type any needs to be replaced with proper Manifest Node Type.
		if (lNode && lParam in lNode) {
			return lNode[lParam];
		} else {
			return "";
		}
	}

	fGetCurrentSCO(): CSCO | null {
		const sIdentifier = this.fGetNode("Identifier");
		return this.fGetSCO(sIdentifier);
	}

	fGetSCO(lIdentifier: string): CSCO | null {
		for (let i = 0; i < arrSCO.length; i++) {
			if (arrSCO[i].identifier === lIdentifier) {
				return arrSCO[i];
			}
		}
		return null;
	}

	fDirectAddDataToUserDataXML(lParam: string, lData: any) {
		const oSCO = this.fGetCurrentSCO();
		if (oSCO) {
			oSCO[lParam] = lData;
		}
	}

	fGetNode(lParam: string): string {
		console.log("GET node : ", lParam);
		//return this.currentStatus[lParam];
		console.log("fGetNode Identifier :  ",gArrCurrentStatus[lParam]);
		return gArrCurrentStatus[lParam];
	}

	fSetNode(lParam: string, lVal: any) {
		
		this.currentStatus[lParam] = lVal;
		gArrCurrentStatus[lParam] = lVal;

		if(lParam == "Status"){
			if(gArrCurrentStatus[lParam] == "Initialized")
			{
				this.currentStatus[lParam] = lVal;
				gArrCurrentStatus[lParam] = lVal;
			}
		}
		console.log("Setting node : ", gArrCurrentStatus);
		
		return true;
	}


	fGoToLaunchSco(sSingleScoLaunch: string) {
		let sBookmark = sSingleScoLaunch;

		if (sBookmark != "") {
			var oNode = this.fGetItemNodeObject(sBookmark);
			var lUrl = this.fGetValue(oNode, "href");
			var lIdentifier = this.fGetValue(oNode, "identifier");
			this.fOpenScoAsset(this.gDataObj.gContentPath + lUrl, lIdentifier, "");
			//fOpenScoAsset(gContentPath + lUrl, lIdentifier);
		}
	}
	fIsUndefined(val: any): boolean {
		return typeof val === 'undefined';
	}
	fIsNumeric(lValue: any): boolean {
		if ((/^[0-9 ]+$/.test(lValue))) {
			return true;
		}
		return false;
	}



	// Function to open SCO asset
	fOpenScoAsset(lUrl: string, lNodeId: string, lTypeOfNode: string): void {
		let gURL: string | undefined;
		this.fSetNode("Identifier", lNodeId);

		if (lTypeOfNode === "asset") {
			// fCheckAndCreateSCOBlockForAsset();
		} else {
			this.fSetNode("Status", "NotInitialized");
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




}

