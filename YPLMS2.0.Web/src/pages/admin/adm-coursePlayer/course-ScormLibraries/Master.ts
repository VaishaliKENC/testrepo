// Master.ts

/***************************************************************************************
*											Master Template													*
***************************************************************************************/

class CAttributes {
    ValidationType: string;
    Mode: string;
    Required: string;
    LookupName: string;
    LValue: string;
    HValue: string;
    Implemented: string;
    Text: string;

    constructor(lValidationType: string, lMode: string, lRequired: string, lLookup: string, lLValue: string, lHValue: string, lImplemented: string, lText: string) {
        this.ValidationType = lValidationType;
        this.Mode = lMode;
        this.Required = lRequired;
        this.LookupName = lLookup;
        this.LValue = lLValue;
        this.HValue = lHValue;
        this.Implemented = lImplemented;
        this.Text = lText;
    }
}

class CMasterTemplate {
    cmi__core___children:any;
    cmi__core__student_id:any;
    cmi__core__student_name:any;
    cmi__core__manageremail:any;
    cmi__core__studentemail:any;
    cmi__core__lesson_location:any;
    cmi__core__credit:any;
    cmi__core__lesson_status:any;
    cmi__core__entry:any;
    //this.cmi__core__score
    cmi__core__score___children:any;
    cmi__core__score__raw:any;
    cmi__core__score__max:any;
    cmi__core__score__min:any;
    cmi__core__total_time:any;
    cmi__core__lesson_mode:any;
    cmi__core__exit:any;
    cmi__core__session_time:any;
    cmi__suspend_data:any;
    cmi__launch_data:any;
    cmi__comments:any;
    cmi__comments_from_lms:any;

    //this.cmi__objectives
    cmi__objectives___children:any;
    cmi__objectives___count:any;

    //this.cmi__objectives___number
    cmi__objectives___number__id:any;

    //this.cmi__objectives___number__score
    cmi__objectives___number__score___children:any;
    cmi__objectives___number__score__raw:any;
    cmi__objectives___number__score__max:any;
    cmi__objectives___number__score__min:any;
    cmi__objectives___number__status:any;

    //this.cmi__student_data
    cmi__student_data___children:any;
    cmi__student_data__mastery_score:any;
    cmi__student_data__max_time_allowed:any;
    cmi__student_data__time_limit_action:any;

    //this.cmi__student_preference
    cmi__student_preference___children:any;
    cmi__student_preference__audio:any;
    cmi__student_preference__language:any;
    cmi__student_preference__speed:any;
    cmi__student_preference__text:any;

    //this.cmi__interactions
    cmi__interactions___children:any;
    cmi__interactions___count:any;

    //this.cmi__interactions___number
    cmi__interactions___number__id:any;

    //this.cmi__interactions___number__objectives
    cmi__interactions___number__objectives___count:any;

    //this.cmi__interactions___number__objectives___number
    cmi__interactions___number__objectives___number__id:any;

    cmi__interactions___number__time:any;
    cmi__interactions___number__type:any;

    //this.cmi__interactions___number__correct_responses
    cmi__interactions___number__correct_responses___count:any;

    //this.cmi__interactions___number__correct_responses___number
    cmi__interactions___number__correct_responses___number__pattern:any;

    cmi__interactions___number__weighting:any;
    cmi__interactions___number__student_response:any;
    cmi__interactions___number__result:any;
    cmi__interactions___number__latency:any;
    cmi__core__totalpages:any;
    cmi__core__completedpages:any;

    constructor() {
        this.cmi__core___children = new CAttributes("CMIString255","Read","true","","","","","student_id,student_name,manageremail,studentemail,lesson_location,credit,lesson_status,entry,score,total_time,exit,session_time");
		this.cmi__core__student_id = new CAttributes("CMIIdentifier","Read","true","","","","","");
		this.cmi__core__student_name = new CAttributes("CMIString255","Read","true","","","","","");
		this.cmi__core__manageremail = new CAttributes("CMIString255","Read","true","","","","","");
		this.cmi__core__studentemail = new CAttributes("CMIString255","Read","true","","","","","");
		this.cmi__core__lesson_location = new CAttributes("CMIString255","Both","true","","","","","");
		this.cmi__core__credit = new CAttributes("CMIVocabulary","Read","true","Credit","","","","");
		this.cmi__core__lesson_status = new CAttributes("CMIVocabulary","Both","true","J_Status","","","","");
		this.cmi__core__entry = new CAttributes("CMIVocabulary","Read","true","Entry","","","","");
		
        //this.cmi__core__score
		this.cmi__core__score___children = new CAttributes("CMIString255","Read","true","","","","","raw,min,max");
		this.cmi__core__score__raw = new CAttributes("CMIDecimal","Both","true","","","","","");
		this.cmi__core__score__max = new CAttributes("CMIDecimal","Both","false","","","","","");
		this.cmi__core__score__min = new CAttributes("CMIDecimal","Both","false","","","","","");
		this.cmi__core__total_time = new CAttributes("CMITimespan","Read","true","","","","","");
		this.cmi__core__lesson_mode = new CAttributes("CMIVocabulary","Read","false","Lesson_Mode","","","","");
		this.cmi__core__exit = new CAttributes("CMIVocabulary","Write","true","Exit","","","","");
		this.cmi__core__session_time = new CAttributes("CMITimespan","Write","true","","","","","");
		this.cmi__suspend_data = new CAttributes("CMIString4096","Both","true","","","","","");
		this.cmi__launch_data = new CAttributes("CMIString4096","Read","true","","","","","");
		this.cmi__comments = new CAttributes("CMIString4096","Both","false","","","","","");
		this.cmi__comments_from_lms = new CAttributes("CMIString4096","Read","false","","","","","");
		
        //this.cmi__objectives
		this.cmi__objectives___children = new CAttributes("CMIString255","Read","false","","","","","id,score,status");
		this.cmi__objectives___count = new CAttributes("CMIInteger","Read","false","","","","","");
		
		//this.cmi__objectives___number
		this.cmi__objectives___number__id = new CAttributes("CMIIdentifier","Both","false","","","","","");
		
		//this.cmi__objectives___number__score
		this.cmi__objectives___number__score___children = new CAttributes("CMIString255","Read","false","","","","","raw,min,max");
		this.cmi__objectives___number__score__raw = new CAttributes("CMIDecimal","Both","false","","","","","");
		this.cmi__objectives___number__score__max = new CAttributes("CMIDecimal","Both","false","","","","","");
		this.cmi__objectives___number__score__min = new CAttributes("CMIDecimal","Both","false","","","","","");
		this.cmi__objectives___number__status = new CAttributes("CMIVocabulary","Both","false","J_Status","","","","");
		
		//this.cmi__student_data
		this.cmi__student_data___children = new CAttributes("CMIString255","Read","false","","","","","mastery_score,time_limit_action,max_time_allowed");
		this.cmi__student_data__mastery_score = new CAttributes("CMIDecimal","Read","false","","","","","");
		this.cmi__student_data__max_time_allowed = new CAttributes("CMITimespan","Read","false","","","","","");
		this.cmi__student_data__time_limit_action = new CAttributes("CMIVocabulary","Read","false","Time_Limit_Action","","","","");
		
        //this.cmi__student_preference
		this.cmi__student_preference___children = new CAttributes("CMIString255","Read","false","","","","","audio,language, speed,text");
		this.cmi__student_preference__audio = new CAttributes("CMISInteger","Both","false","","-1","100","","");
		this.cmi__student_preference__language = new CAttributes("CMIString255","Both","false","","","","","");
		this.cmi__student_preference__speed = new CAttributes("CMISInteger","Both","false","","-100","100","","");
		this.cmi__student_preference__text = new CAttributes("CMISInteger","Both","false","","-1","1","","");
		
		//this.cmi__interactions
		this.cmi__interactions___children = new CAttributes("CMIString255","Read","false","","","","","id,objectives,time,type,correct_responses,weighting,student_response,result,latency");
		this.cmi__interactions___count = new CAttributes("CMIInteger","Read","false","","","","","");
		
		//this.cmi__interactions___number
		this.cmi__interactions___number__id = new CAttributes("CMIIdentifier","Write","false","","","","","");
		
		//this.cmi__interactions___number__objectives
		this.cmi__interactions___number__objectives___count = new CAttributes("CMIInteger","Read","false","","","","","");
		
		//this.cmi__interactions___number__objectives___number
		this.cmi__interactions___number__objectives___number__id = new CAttributes("CMIIdentifier","Write","false","","","","","");
		
		this.cmi__interactions___number__time = new CAttributes("CMITime","Write","false","","","","","");
		this.cmi__interactions___number__type = new CAttributes("CMIVocabulary","Write","false","Interaction","","","","");
		
		//this.cmi__interactions___number__correct_responses
		this.cmi__interactions___number__correct_responses___count = new CAttributes("CMIInteger","Read","false","","","","","");
		
		//this.cmi__interactions___number__correct_responses___number
		this.cmi__interactions___number__correct_responses___number__pattern = new CAttributes("CMIFeedback","Write","false","","","","","");
		
		this.cmi__interactions___number__weighting = new CAttributes("CMIDecimal","Write","false","","","","","");
		this.cmi__interactions___number__student_response = new CAttributes("CMIFeedback","Write","false","","","","","");
		this.cmi__interactions___number__result = new CAttributes("CMIVocabulary","Write","false","Result","","","","");
		this.cmi__interactions___number__latency = new CAttributes("CMITimespan", "Write", "false", "", "", "", "", "");
		this.cmi__core__totalpages = new CAttributes("CMIDecimal", "Both", "false", "", "", "", "", "");
		this.cmi__core__completedpages = new CAttributes("CMIDecimal", "Both", "false", "", "", "", "", "");
	
    }
}

export const gMasterTemplate : any = new CMasterTemplate();



/***************************************************************************************
*											Initial Userdat SCO												*
***************************************************************************************/

export class CSCO {
    [key: string]: string; // Index signature
    identifier: string = "";
    cmi__core__student_id: string = "";
    cmi__core__student_name: string = "";
    cmi__core__manageremail: string = "";
    cmi__core__studentemail: string = "";
    cmi__core__lesson_location: string = "";
    cmi__core__credit: string = "credit";
    cmi__core__lesson_status: string = "not attempted";
    cmi__core__entry: string = "ab-initio";
    cmi__core__score__raw: string = "";
    cmi__core__score__max: string = "";
    cmi__core__score__min: string = "";
    cmi__core__total_time: string = "0000:00:00.00";
    cmi__core__lesson_mode: string = "normal";
    cmi__core__exit: string = "";
    cmi__core__session_time: string = "0000:00:00.00";
    cmi__suspend_data: string = "";
    cmi__launch_data: string = "";
    cmi__comments: string = "";
    cmi__comments_from_lms: string = "";
    cmi__objectives___count: string = "0";
    cmi__student_data__mastery_score: string = "";
    cmi__student_data__max_time_allowed: string = "";
    cmi__student_data__time_limit_action: string = "";
    cmi__student_preference__language: string = "en-us";//sLearnerLanguageId; // Make sure sLearnerLanguageId is defined elsewhere
    cmi__core__totalpages: string = "";
    cmi__core__completedpages: string = "";

    constructor(lArr: string[] = []) {
        
        if(lArr.length >0)
		{
			this.identifier = lArr[0];
			this.cmi__core__student_id = lArr[1];
			this.cmi__core__student_name = lArr[2];
			this.cmi__core__manageremail = lArr[3];
			this.cmi__core__lesson_location = lArr[5];
			this.cmi__core__credit = lArr[6];
			this.cmi__core__lesson_status = lArr[7];
			this.cmi__core__entry = lArr[8];
			this.cmi__core__score__raw = lArr[9];
			this.cmi__core__score__max = lArr[10];
			this.cmi__core__score__min = lArr[11];
			this.cmi__core__total_time = lArr[12];
			this.cmi__core__lesson_mode = lArr[13];
			this.cmi__core__exit = lArr[14];
			this.cmi__core__session_time = lArr[15];
			
			this.cmi__suspend_data = lArr[16];
			this.cmi__launch_data = lArr[17];
			this.cmi__comments = lArr[18];
			this.cmi__comments_from_lms = lArr[19];
			this.cmi__objectives___count = lArr[20];
			this.cmi__core__totalpages = lArr[25];
			this.cmi__core__completedpages = lArr[26];

			var a2 = lArr[19].split("#&bv&#");
		    
			for (var i=0;i<a2.length;i++){
			    var a3 = a2[i].split(",");
			    
			    eval("this.cmi__objectives___" + i + "__id = a3[0]");
				eval("this.cmi__objectives___" + i + "__score__raw = a3[1]");
				eval("this.cmi__objectives___" + i + "__score__max = a3[2]");
				eval("this.cmi__objectives___" + i + "__score__min = a3[3]");
				eval("this.cmi__objectives___" + i + "__status = a3[4]");
			}
			this.cmi__student_data__mastery_score = lArr[20];
			this.cmi__student_data__max_time_allowed = lArr[21];
			this.cmi__student_data__time_limit_action = lArr[22];
			this.cmi__student_preference__language = lArr[23];
		}
		else
		{
			this.cmi__core__student_id="";
			this.cmi__core__student_name = "";
			this.cmi__core__manageremail = "";
			this.cmi__core__lesson_location="";
			this.cmi__core__credit = "credit";
			this.cmi__core__lesson_status = "not attempted";
			this.cmi__core__entry = "ab-initio";
			this.cmi__core__score__raw="";
			this.cmi__core__score__max="";
			this.cmi__core__score__min="";
			this.cmi__core__total_time = "0000:00:00.00";
			this.cmi__core__lesson_mode = "normal";
			this.cmi__core__exit="";
			this.cmi__core__session_time="0000:00:00.00";
			this.cmi__suspend_data="";
			this.cmi__launch_data="";
			this.cmi__comments="";
			this.cmi__comments_from_lms="";
			this.cmi__objectives___count="0";
			this.cmi__student_data__mastery_score="";
			this.cmi__student_data__max_time_allowed="";
			this.cmi__student_data__time_limit_action="";
			this.cmi__student_preference__language = "en-us";//sLearnerLanguageId;
			this.cmi__core__totalpages = "";
			this.cmi__core__completedpages = "";
		}
    }

}


/***************************************************************************************
*											SCORM Functions States											*
***************************************************************************************/


export const gArrFunctionsState: { [key: string]: string } = {};  // Use a more appropriate type for the object
gArrFunctionsState["NotInitialized"] = "LMSInitialize LMSGetLastError LMSGetErrorString LMSGetDiagnostic";
gArrFunctionsState["Initialized"] = "LMSGetValue LMSSetValue LMSGetLastError LMSGetErrorString LMSGetDiagnostic LMSCommit LMSFinish";
gArrFunctionsState["Finished"] = "LMSGetLastError LMSGetErrorString LMSGetDiagnostic";
gArrFunctionsState["LMSFinishReturn"] = "true";
// ... (rest of the gArrFunctionsState assignments)



/***************************************************************************************
*											Current Status														*
***************************************************************************************/

export const gArrCurrentStatus: { [key: string]: string } = {
    Identifier: "",
    SectionIdentifier: "",
    PagePosition: "",
    SectionPosition: "",
    LastError: "0",
    Status: "NotInitialized",
    Default: "",
    InitializedNodeId: "",
    LastInitializedNodeId: "",
    LastVisitedNodeId: "",
  };
/***************************************************************************************
*											Scorm CMIVOCABULARY Lookup										*
***************************************************************************************/

const gScormLookups: { [key: string]: string } = {}; // Corrected type annotation
gScormLookups["Credit__credit"] = "credit";
gScormLookups["Credit__no-credit"] = "no-credit";
gScormLookups["Credit__Default"] = "credit";

gScormLookups["Lesson_Mode__browse"] = "browse";
gScormLookups["Lesson_Mode__Default"] = "normal";
gScormLookups["Lesson_Mode__normal"] = "normal";
gScormLookups["Lesson_Mode__review"] = "review";

gScormLookups["J_Status__passed"] = "passed";	
gScormLookups["J_Status__completed"] = "completed";	
gScormLookups["J_Status__failed"] = "failed";	
gScormLookups["J_Status__incomplete"] = "incomplete";	
gScormLookups["J_Status__browsed"] = "browsed";	
gScormLookups["J_Status__not attempted"] = "not attempted";	

gScormLookups["Time_Limit_Action__exit,message"] = "exit,message";	
gScormLookups["Time_Limit_Action__exit,no message"] = "exit,no message";	
gScormLookups["Time_Limit_Action__continue,message"] = "continue,message";	
gScormLookups["Time_Limit_Action__continue,no message"] = "continue,no message";	

gScormLookups["Exit__time-out"] = "time-out";	
gScormLookups["Exit__suspend"] = "suspend";	
gScormLookups["Exit__Default"] = "suspend";	
gScormLookups["Exit__logout"] = "logout";	
gScormLookups["Exit__"] = "";	

gScormLookups["Entry__ab-initio"] = "ab-initio";	
gScormLookups["Entry__suspend"] = "suspend";	
gScormLookups["Entry__"] = "";	

gScormLookups["Result__correct"] = "correct";
gScormLookups["Result__wrong"] = "wrong";
gScormLookups["Result__unanticipated"] = "unanticipated";
gScormLookups["Result__neutral"] = "neutral";
gScormLookups["Result__CheckFor"] = "CMIDecimal";

gScormLookups["Interaction__true-false"] = "true-false";
gScormLookups["Interaction__choice"] = "choice";
gScormLookups["Interaction__fill-in"] = "fill-in";
gScormLookups["Interaction__matching"] = "matching";
gScormLookups["Interaction__performance"] = "performance";
gScormLookups["Interaction__sequencing"] = "sequencing";
gScormLookups["Interaction__likert"] = "likert";
gScormLookups["Interaction__numeric"] = "numeric";
// ... (all gScormLookups key-value pairs remain the same)

/***************************************************************************************
*											SCORM Error Lookup												*
***************************************************************************************/


export const gErrorLookup: { [key: string]: string[] } = {};
gErrorLookup["0"] = new Array("No error", "");
gErrorLookup["101"] = new Array("General Exception", "");
gErrorLookup["201"] = new Array("Invalid argument error", "");
gErrorLookup["202"] = new Array("Element cannot have children", "");
gErrorLookup["203"] = new Array("Element not an array - Cannot have count", "");
gErrorLookup["301"] = new Array("Not initialized", "");
gErrorLookup["401"] = new Array("Not implemented error", "");
gErrorLookup["402"] = new Array("Invalid Set Value, element is a keyword", "");
gErrorLookup["403"] = new Array("Element is read only", "");
gErrorLookup["404"] = new Array("Element is write only", "");
gErrorLookup["405"] = new Array("Incorrect Data Type", "");

// ... (rest of the gErrorLookup assignments, using array literals)

export const scoInstance = new CSCO();

