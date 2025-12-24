
	var oXML;
	var readyflg = 0;
	var FlashPlayerErrorMessage1;
	var FlashPlayerErrorMessage2;
	var FlashPlayerErrorMessage3;
	var FlashPlayerErrorMessage4;
	var FlashPlayerErrorMessage5;
	var FlashPlayerErrorMessage6;

	//var gIsMultilingual;
	function loadXMLfile(xmlname)
	{
		
	}	
	function getLaunchpage(){
		//alert(thisMovie('index'))
		thisMovie('index').SetVariable("gReadyState", readyflg);
		var gMultiflg = oXML.find("index_page").find("lang_txt").text();
		var audioflg  = oXML.find("index_page").find("audio_flg").text();
		var coursetitle = oXML.find("index_page").find("coursetitle").text();
		var audionotes_txt = oXML.find("index_page").find("audionotes_txt").text();
		var nonaudionotes_txt = oXML.find("index_page").find("nonaudionotes_txt").text();
		var stop_txt = oXML.find("index_page").find("stop_txt").text();
		var warning_txt = oXML.find("index_page").find("warning_txt").text();
		var btntxt_1 = oXML.find("index_page").find("btntxt_1").text();	
		var notes_txt;
		/*----- updation fo audio/nonaudio text -------------*/
		if(audioflg.toUpperCase() == "Y"){
			notes_txt = audionotes_txt;
		}else{
			notes_txt = nonaudionotes_txt;
		}
		fDynamicLaunch(coursetitle,notes_txt,stop_txt,warning_txt,btntxt_1,gMultiflg)	
		
	}
	
	
	function getLoadingText(){
		var splash = oXML.find("preloading_text").find("splash").text();
		var template = oXML.find("preloading_text").find("template").text();
		var audio = oXML.find("preloading_text").find("audio").text();
		var image = oXML.find("preloading_text").find("image").text();
		var simulations = oXML.find("preloading_text").find("simulations").text();
		fLoadingText(splash,template,audio,image,simulations)
	}
	
	
	function getMain(){	
		var gMultiflg = oXML.find("index_page").find("lang_txt").text();
		var txt_1 = oXML.find("index_page").find("btntxt_2").text();
		var bookmsg = oXML.find("index_page").find("bookmark_txt").text();
		var audioflg  = oXML.find("index_page").find("audio_flg").text();
		var CapQuestion = oXML.find("result_page").find("simulation_question").text();
		var QuestionTxt = oXML.find("index_page").find("QuestionTxt").text();	
		var PageNoTxt = oXML.find("index_page").find("PageNoTxt").text();	
		var PageNoOFTxt = oXML.find("index_page").find("PageNoOFTxt").text();	
		fDynamicMain(txt_1,bookmsg,gMultiflg,audioflg,CapQuestion, QuestionTxt, PageNoTxt, PageNoOFTxt)	
		getLoadingText()
	}
	
	function getResult(){		
		thisMovie('result_shell').SetVariable("gReadyState", readyflg);
		var head_txt=oXML.find("result_page").find("head_txt").text();
		var sucess_txt=oXML.find("result_page").find("sucess_txt").text();
		var score_txt=oXML.find("result_page").find("score_txt").text();
		var feedback_txt=oXML.find("result_page").find("feedback_txt").text();
		var completion_txt=oXML.find("result_page").find("completion_txt").text();
		var modulename_txt=oXML.find("result_page").find("modulename_txt").text();
		var btntxt_1=oXML.find("result_page").find("btntxt_1").text();				
		var btntxt_2=oXML.find("result_page").find("btntxt_2").text();				
		var surveylink = oXML.find("result_page").find("surveylink").text();	
		fDynamicResult(head_txt,sucess_txt,score_txt,feedback_txt,completion_txt,modulename_txt,btntxt_1,btntxt_2,surveylink)
		
	}
	
	function getCertificate(){
		thisMovie('activity_shell').SetVariable("gReadyState", readyflg);
		var head_txt=oXML.find("certificate_page").find("head_txt").text();
		var sucess_txt=oXML.find("certificate_page").find("sucess_txt").text();
		var name_txt=oXML.find("certificate_page").find("name_txt").text();	
		var on_txt=oXML.find("certificate_page").find("on_txt").text();	
		var btntxt_1=oXML.find("certificate_page").find("btntxt_1").text();				
		var btntxt_2=oXML.find("certificate_page").find("btntxt_2").text();
		var modulename_txt=oXML.find("result_page").find("modulename_txt").text();
		fDynamicCertificate(head_txt,sucess_txt,name_txt,on_txt,btntxt_1,btntxt_2,modulename_txt)
	}

	function getResourcePage(){
		thisMovie('resource_top').SetVariable("gReadyState", readyflg);
		thisMovie('resource_btm').SetVariable("gReadyState", readyflg);
		var reshead_txt=oXML.find("resource_page").find("head_txt").text();
		var exit_txt=oXML.find("resource_page").find("exit_txt").text();
		var gMultiflg = oXML.find("index_page").find("lang_txt").text();
		fDynamicResource(reshead_txt,exit_txt,gMultiflg)
	
	}
		
	function getFlashPlayerErrorMessage(){
		FlashPlayerErrorMessage1 = oXML.find("FlashPlayerErrorMessage").find("FlashPlayerErrorMessage1").text();
		FlashPlayerErrorMessage2 = oXML.find("FlashPlayerErrorMessage").find("FlashPlayerErrorMessage2").text();
		FlashPlayerErrorMessage3 = oXML.find("FlashPlayerErrorMessage").find("FlashPlayerErrorMessage3").text();
		FlashPlayerErrorMessage4 = oXML.find("FlashPlayerErrorMessage").find("FlashPlayerErrorMessage4").text();
		FlashPlayerErrorMessage5 = oXML.find("FlashPlayerErrorMessage").find("FlashPlayerErrorMessage5").text();
		FlashPlayerErrorMessage6 = oXML.find("FlashPlayerErrorMessage").find("FlashPlayerErrorMessage6").text();
		//alert(FlashPlayerErrorMessage1 + " -- "  + FlashPlayerErrorMessage2 + " -- "  + FlashPlayerErrorMessage3 + " -- "  + FlashPlayerErrorMessage4 )
	}
