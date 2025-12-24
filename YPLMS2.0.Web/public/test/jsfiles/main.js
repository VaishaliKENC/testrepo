// JavaScript Document
/*********** Player level constants and attributes are initialized here ********************/

var gModLen, gLessonLen, gTopLen, gPageLen;
var gCurrMod=0, gCurrLess=0, gCurrTop=0, gCurrPage=0;
var gTotalPages, gCurrPageNum;
var gCurrSndObj, gCurrVdoObj;
var isAudioStreamPlaying = false;
var playPauseAudioStream = false;
var IsScrom = true;
var isBackBtnEnable = false;
var isNextBtnEnable = false;
var isTransEnable = false;
var isAudioEnable = true;
var pageObj="";
//var gModCompArr, gLessonCompArr, gTopCompArr, gPagesCompArr;
var gCompModArr = new Array(), gCompLessArr = new Array(), gCompTopArr = new Array(), gCompPageArr = new Array();
var arrVisited;
var iOS = (navigator.userAgent.match(/(iPad|iPhone|iPod)/g) ? true : false);
var gModuleArr = new Array();
var iPadAudioPlayed = false;
var isPreloadPlayed = false;
/* Nikhil A. */
var gPageCompCondArr = new Array();
var gPageTrackingArr = new Array();
var pageTracking; 
var gPageCompCondVal = 0;
var ua = navigator.userAgent.toLowerCase();
var isiPAD = ua.indexOf("ipad") > -1; //&& ua.indexOf("mobile");
var isInActivity = false;
var _currAudioPlayingPath;

var bAudioDisable = ""; //To hold the information about page audio
var bAudioMute = false; //To hold the state of volume control
var bAudioPlaying = false; //To hold current audio state (play / pause)

var bPageStateChange = true; //To hold any kind of change (e.g. page change, menu open, popup open etc.)
var bAudioCompleted = true;
var bFlashFallbackActive = false;
var isIntroPage = false;

/*********************** Assessment objects ********************/
var gIsAssessment = false;
var gIsPageSkillCheck = false;
var gIsAssessmentCompleted = false;
var gAssessmentPassScore = 80;
var gAssessmentAchievedScore = 0;
var gAssessmentCorrectAnswers = 0;
var gAssessmentTotalQuestions = -1;
var gAssessmentMaxAttempts = -1;
var gAssessmentCurrentAttempt = 1;
//ISSKILLCHECK
/***************************************************************/

function fnCreateXMLObjArr(){
	var moduleCounter, lessonCounter, topicCounter, pageCounter, totalPageCounter;
	moduleCounter = 0;
	totalPageCounter = 0;
	oXML.find("MODULE").each(function() {
		gModuleArr[moduleCounter] = $(this);
		gModuleArr[moduleCounter].Lessons = new Array();
		lessonCounter = 0;
		$(this).find("LESSON").each(function() {
			gModuleArr[moduleCounter].Lessons[lessonCounter] = $(this);
			gModuleArr[moduleCounter].Lessons[lessonCounter].Topics = new Array();
			topicCounter = 0;
			$(this).find("TOPIC").each(function() {
				gModuleArr[moduleCounter].Lessons[lessonCounter].Topics[topicCounter] = $(this)
				gModuleArr[moduleCounter].Lessons[lessonCounter].Topics[topicCounter].Pages = new Array();
				pageCounter = 0;
				$(this).find("PAGE").each(function() {
					gModuleArr[moduleCounter].Lessons[lessonCounter].Topics[topicCounter].Pages[pageCounter] = $(this)
					pageCounter++;
					totalPageCounter++;
				});
				topicCounter++;
			});
			lessonCounter++;
		});
		moduleCounter++;
	});
	gTotalPages = totalPageCounter;
		
}
function fnCreatePreCacheArr()
{
	var tempArr = new Array();
	var fileCounter = 0;
	oXML.find("PRELOADDATA").each(function() {
		$(this).find("PreloadFile").each(function(){
			tempArr[fileCounter] = $(this);
			fileCounter++;
		});
	});
	fnPreCacheData(tempArr);
}

function fnPreCacheData(tempArr)
{
	var idcnt = 0;
	for(var i=0;i<tempArr.length;i++)
		{
			var tempObj = new Object();
			if(tempArr[i].attr("type") == "Audio")
			{
				if(!iOS){
					if($.browser.mozilla)
					{
						tempObj.src = tempArr[i].attr("Path")+".wav";
					}else{
						tempObj.src = tempArr[i].attr("Path")+".mp3";
					}
				}
			}else{
				tempObj.src = tempArr[i].attr("Path");
			}
			if((iOS)&&(tempArr[i].attr("type") == "Audio")){
			}else{
				tempObj.id = idcnt;
				manifest[idcnt] = tempObj;
				idcnt++;
			}

		}
}
function isActive(obj){
  return $(obj).hasClass("activcls");
}

function fnAddPlayerEvents(){
	fnInitialize();
	playerToggleAciteClass();
	$("#clsNxtBtn").click(function(e) {
		if(isActive(this))
			  fnGetPage("NEXT");
	});
	$("#clsAudioOffBtn,#clsAudioOnBtn").click(function(e){
		if(isActive(this))
		  fnSoundOnOff();
	});
	$("#clsMnuBtn").click(function(e){
		
		if(isActive(this)){
			fnLoadMenu();
		}else{
			disablePopup();
		}
	});
	$("#clsExitBtn").click(function(e){
		if(isActive(this))
		  fnLoadExitPopup();
	});
	$("#clsHelpBtn").click(function(e){
		if(isActive(this))
		  fnLoadHelpPopup();
	});	
	$("#clsBackBtn").click(function(e) {
		if(isActive(this)){
			if( isInActivity ) {
	   			var lPageId = gModuleArr[gCurrMod].Lessons[gCurrLess].Topics[gCurrTop].Pages[gCurrPage].attr("ID");
				fnJumpToPageByID(lPageId)			
			}else{
				fnGetPage("PREV");
			}
		}
    });
	 $("#popupClose").click(function(){
		if(isActive(this))
        disablePopup();  
   });
   $("#clsReplayBtn").click(function(e){
		fnReplayPage();
	});
	$("#clsReplay").click(function(){
		if(isActive(this)){
			if( isInActivity ) {
	   			replayAudio();
	   		} else {
	   			var lPageId = gModuleArr[gCurrMod].Lessons[gCurrLess].Topics[gCurrTop].Pages[gCurrPage].attr("ID");
				fnJumpToPageByID(lPageId)
	   		}
			
		}
	});
	 $("#transcriptBtn").click(function(){ 
		if(isActive(this))
        fnTranscript();  
   });

	$("#clsReplayBtn").click(function (e){
		replayAudio();
	});

	$("#clsPlayPauseBtn").click(function (e) {
		if( !$('#clsPlayPauseBtn').hasClass("clsPlayDisable") ) {
			playPauseAudio();
		}
	})
}

//For Creating menu pop up
function loadPopup(){
	var options = {direction:"left"}
    //loads popup only if it is disabled  
    if($(".menuCover").data("state")==0){  
        $(".menuCover").css({  
           // "opacity": "0.7"  
        });  
        /*$(".bgPopup").fadeIn("medium");  */
		$( ".menuCover" ).effect( "slide", options, 500, showmenuCover );
        $( ".menuCover" ).click(function(){
			disablePopup();
		})
        $(".menuCover").data("state",1);  
    }  
}  

function showmenuCover()
{
	$(".MenuPopup").fadeIn("medium"); 
}
  
function disablePopup(){
	fnPlayAudio();
	playerToggleAciteClass();
	var options = {direction:"left",mode:"hide"}
    if ($(".menuCover").data("state")==1){  
       /* $(".bgPopup").fadeOut("medium");  */
        $(".MenuPopup").fadeOut("medium");  
        $(".menuCover").data("state",0); 
		/*$(".bgPopup").remove();*/
		$(".MenuPopup").remove();
		$( ".menuCover" ).effect( "slide", options, 500, hidemenuCover );		
    }  
}  

function hidemenuCover(){
 $(".menuCover").remove();
} 
 
function centerPopup(){
    var winw = $(window).width();  
    var winh = $(window).height();  
    var popw = $('.Popup').width();  
    var poph = $('.Popup').height();  
    $(".Popup").css({  
        "position" : "absolute",  
        "top" : 20,  
        "left" : winw/2-popw/2  
    });  
    //IE6  
    $(".bgPopup").css({  
        "height": winh    
    });  
}  

function fnLoadMenu()
{
	playerToggleAciteClass();
	fnPauseAudio();
	$("<div class='menuCover'><div class='MenuPopup'></div></div>").insertAfter('#contentFrame');
	//alert("In")
	$(".menuCover").data("state",0);
	//centerPopup(); 
	loadPopup(); 
	$('.MenuPopup').load('menu.html');
}
//Exit Pop

function loadExitPopup(){
var options={}
    //loads popup only if it is disabled  
    if($(".bgPopup").data("state")==0){  
        $(".bgPopup").css({  
            "opacity": "0.7"  
        });  
        /*$(".bgPopup").fadeIn("medium");*/
		$( ".bgPopup" ).effect( "slide", options, 500, showExitPopUp );
		
        $(".bgPopup").data("state",1);  
    }  
}  
 function showExitPopUp(){
 //alert('done')
 $(".ExitPopup").fadeIn("medium");
} 

function hideExitPopUp(){
 //alert('done')
 $(".bgPopup").remove();
}
function disableExitPopup(){
	fnPlayAudio();
	var options = {mode:"hide"}
    if ($(".bgPopup").data("state")==1){  
        /*$(".bgPopup").fadeOut("medium");  */
		
        $(".ExitPopup").fadeOut("medium");  
        $(".bgPopup").data("state",0); 
		
		$(".ExitPopup").remove();		
		$( ".bgPopup" ).effect( "slide", options, 500, hideExitPopUp);
    }  
}  
  
function centerExitPopup(){
    var winw = $(window).width();
    var winh = $(window).height();  
    var popw = $('.ExitPopup').width();
    var poph = $('.ExitPopup').height();
    $(".ExitPopup").css({  
        "position" : "absolute",  
        "top" : 20,  
        "left" : winw/2-popw/2  
    });  
    //IE6  
    $(".bgPopup").css({  
        "height": winh    
    });  
}  
function fnLoadExitPopup()
{
	fnSetCoreData();
	fnPauseAudio();
	BVScorm_adlOnunload();
	top.close();

}

function fnLoadHelpPopup() {
	fnPauseAudio();
	$("<div class='HelpPopup'></div><div class='bgHelpPopup'></div>").insertAfter('.gridContainer');
	$(".bgHelpPopup").data("state",0);
	centerHelpPopup(); 
	loadHelpPopup(); 
	$('.HelpPopup').load('help.html');	
}

//Exit Pop
function loadHelpPopup() {
var options={}
    //loads popup only if it is disabled  
    if($(".bgHelpPopup").data("state")==0){  
        $(".bgHelpPopup").css({  
            "opacity": "0.1"  
        });  
        /*$(".bgPopup").fadeIn("medium");*/
		$( ".bgHelpPopup" ).effect("slide", options, 300, showHelpPopUp);
		
        $(".bgHelpPopup").data("state",1);  
    }  
}  

function showHelpPopUp() {
	$(".HelpPopup").fadeIn();
} 

function hideHelpPopUp() {
	$(".bgHelpPopup").remove();
}
function disableHelpPopup() {
	fnPlayAudio();
	var options = {mode:"hide"}
    if ($(".bgHelpPopup").data("state")==1){  
        /*$(".bgPopup").fadeOut("medium");  */
        $(".HelpPopup").fadeOut("medium");  
        $(".bgHelpPopup").data("state",0); 
		
		$(".HelpPopup").remove();		
		$( ".bgHelpPopup" ).effect("slide", options, 300, hideHelpPopUp);
    }  
}

function centerHelpPopup() {
    var winw = $(window).width();  
    var winh = $(window).height();  
    var popw = $('.HelpPopup').width();  
    var poph = $('.HelpPopup').height();  
    $(".HelpPopup").css({  
        "position" : "absolute",  
        "top" : 0,  
        "left" : 0  
    });  
    //IE6  
    $(".bgHelpPopup").css({  
        "height": winh    
    });  
}  

function fnGetPage(lCase){
	//
	fnResetAudioProgressBar();
	if($("#jplayerPlay").hasClass("jp-play-deactive")){
		$("#jplayerPlay").addClass("jp-play");
		$("#jplayerPlay").removeClass("jp-play-deactive");
	}

	$('#contentFrame').html("");
	$('#contentFrame').empty();
	var lCurrPgNum, lCurrTopNum, lCurrLessNum, lCurrModNum;
	lCurrPgNum = gCurrPage;
	lCurrTopNum = gCurrTop;
	lCurrLessNum = gCurrLess;
	lCurrModNum = gCurrMod;
	switch(lCase)
	{
		case "SAME":
		break;
		case "NEXT":
			gCurrPageNum++;
			//alert((lCurrPgNum<gModuleArr[lCurrModNum].Lessons[lCurrLessNum].Topics[lCurrTopNum].Pages.length-1))
			if(lCurrPgNum<gModuleArr[lCurrModNum].Lessons[lCurrLessNum].Topics[lCurrTopNum].Pages.length-1)
			{
				lCurrPgNum++;
				//fnLoadPage(lCurrModNum,lCurrLessNum,lCurrTopNum,lCurrPgNum);
				var lPageId = gModuleArr[lCurrModNum].Lessons[lCurrLessNum].Topics[lCurrTopNum].Pages[lCurrPgNum].attr("ID");
		//fnLoadPage(gCurrMod, gCurrLess, gCurrTop, gCurrPage);
		fnJumpToPageByID(lPageId)
			}
			else
			{
				if(lCurrTopNum<gModuleArr[lCurrModNum].Lessons[lCurrLessNum].Topics.length-1)
				{
					lCurrTopNum++;
					lCurrPgNum = 0;
					//fnLoadPage(lCurrModNum,lCurrLessNum,lCurrTopNum,lCurrPgNum);
					var lPageId = gModuleArr[lCurrModNum].Lessons[lCurrLessNum].Topics[lCurrTopNum].Pages[lCurrPgNum].attr("ID");
		//fnLoadPage(gCurrMod, gCurrLess, gCurrTop, gCurrPage);
		fnJumpToPageByID(lPageId)
				}
				else if(lCurrLessNum<gModuleArr[lCurrModNum].Lessons.length-1)
				{
					lCurrLessNum++;
					lCurrTopNum=0;
					lCurrPgNum = 0;
					//fnLoadPage(lCurrModNum,lCurrLessNum,lCurrTopNum,lCurrPgNum);
					var lPageId = gModuleArr[lCurrModNum].Lessons[lCurrLessNum].Topics[lCurrTopNum].Pages[lCurrPgNum].attr("ID");
		//fnLoadPage(gCurrMod, gCurrLess, gCurrTop, gCurrPage);
		fnJumpToPageByID(lPageId)
				}
				else if(lCurrModNum<gModuleArr.length-1)
				{
					lCurrModNum++;
					lCurrLessNum=0;
					lCurrTopNum=0;
					lCurrPgNum = 0;
					//fnLoadPage(lCurrModNum,lCurrLessNum,lCurrTopNum,lCurrPgNum);
					var lPageId = gModuleArr[lCurrModNum].Lessons[lCurrLessNum].Topics[lCurrTopNum].Pages[lCurrPgNum].attr("ID");
		//fnLoadPage(gCurrMod, gCurrLess, gCurrTop, gCurrPage);
		fnJumpToPageByID(lPageId)
				}
			}
		break;
		case "PREV":
			if(lCurrPgNum > 0)
			{
				lCurrPgNum--;
			}
			else
			{
				if(lCurrTopNum>0)
				{
					lCurrTopNum--;
				}
				else
				{
					if(lCurrLessNum>0)
					{
						lCurrLessNum--;
					}
					else if(lCurrModNum>0)
					{
						lCurrModNum--;
						lCurrLessNum = gModuleArr[lCurrModNum].Lessons.length-1;
					}
					lCurrTopNum = gModuleArr[lCurrModNum].Lessons[lCurrLessNum].Topics.length-1;
				}
				lCurrPgNum = gModuleArr[lCurrModNum].Lessons[lCurrLessNum].Topics[lCurrTopNum].Pages.length-1;
			}
			gCurrPageNum--;
			//fnLoadPage(lCurrModNum,lCurrLessNum,lCurrTopNum,lCurrPgNum);
			var lPageId = gModuleArr[lCurrModNum].Lessons[lCurrLessNum].Topics[lCurrTopNum].Pages[lCurrPgNum].attr("ID");
		//fnLoadPage(gCurrMod, gCurrLess, gCurrTop, gCurrPage);
		fnJumpToPageByID(lPageId)
		break;
	}
}

function fnInitialize(){
	$("#jquery_jplayer_1").bind($.jPlayer.event.play, function(event) { // Add a listener to report the time play began
		isAudioStreamPlaying = true;
	});
	
	$("#jquery_jplayer_1").bind($.jPlayer.event.pause, function(event) { // Add a listener to report the time play began
		isAudioStreamPlaying = false;
		$("#jplayerPlay").removeClass("jp-play-deactive");
		$("#jplayerPlay").addClass("jp-play");		
	});
	
	$("#contentContainer").touchwipe({
		wipeLeft: function() {
			if(isNextBtnEnable && gCurrPageNum<gTotalPages && (!isInActivity)){
				fnGetPage("NEXT");
			}
		},
		wipeRight: function() {
			if(gCurrPageNum>1 && (!isInActivity)){
			fnGetPage("PREV");
			}
		}
	});
}

function fnGetTotalQuestions() {
	var lCalTot = 0;
	var lPagesObj = $(gModuleArr[gCurrMod].Lessons[gCurrLess].Topics[gCurrTop].Pages);
	lPagesObj.each(function(){
	    if($(this).attr('ISSKILLCHECK') == 'Y') {
	        lCalTot += 1;
	    }
	});
	/*try {
		($(gModuleArr[gCurrMod].Lessons[gCurrLess].Topics[gCurrTop].Pages)("[ISSKILLCHECK*='Y']").filter(function () {
			//return 1;
			lCalTot += 1;
		});
	} catch(e) {
		alert('Error:- '+e);
	}*/
	return lCalTot;
}

function fnLoadPage(lModNum,lLessNum,lTopNum,lPageNum){
	gIsAssessment = ( gModuleArr[lModNum].Lessons[lLessNum].Topics[lTopNum].attr("ISSKILLCHECK") == "Y" ) ? true : false;
	gIsPageSkillCheck = ( gModuleArr[lModNum].Lessons[lLessNum].Topics[lTopNum].Pages[lPageNum].attr("ISSKILLCHECK") == "Y" ) ? true : false;
	//To unbind previous page audio events
	isIntroPage = false;
	//Added to show preloader for first page
	fnShowPreLoader();
	fnResetAudioProgressBar();
	//---------------------------
	$('#contentFrame').html(""); //Added to remove last embedded Flash video from the page
	fnDesiableBack(true);
	fnDesiableNext(true);
	//Setting Location for SCORM
	//BVScorm_setlocation(gModuleArr[lModNum].Lessons[lLessNum].Topics[lTopNum].Pages[lPageNum].attr("ID")) 
	gIsPageSkillCheck ? BVScorm_setlocation(gModuleArr[lModNum].Lessons[lLessNum].Topics[lTopNum].Pages[0].attr("ID")) : BVScorm_setlocation(gModuleArr[lModNum].Lessons[lLessNum].Topics[lTopNum].Pages[lPageNum].attr("ID"));
	gCurrPage = lPageNum;
	gCurrTop = lTopNum;
	gCurrLess = lLessNum;
	gCurrMod = lModNum;
	if( gIsAssessment ) {
		gAssessmentTotalQuestions = fnGetTotalQuestions();
	}
	if( !gIsPageSkillCheck ) {
		if(gCurrPageNum==1)
		{
			fnDesiableBack(true);
			fnDesiableNext(false);
		} else if(gCurrPageNum==gTotalPages)
		{
			fnDesiableNext(true);
			gIsAssessment ? fnDesiableBack(true) : fnDesiableBack(false);
		} else {
			fnDesiableBack(false);
			fnDesiableNext(false);
		}
	} else {
		fnDesiableBack(true);
		fnDesiableNext(true);
		fnHideShowAudioPlayer(true);
	}
	var isNxtDis = gModuleArr[lModNum].Lessons[lLessNum].Topics[lTopNum].Pages[lPageNum].attr("NEXTDISABLE");
	var isCurrPgComp = fnCheckPageComplection(gModuleArr[lModNum].Lessons[lLessNum].Topics[lTopNum].Pages[lPageNum].attr("ID"))
	if(isNxtDis == "T" && !isCurrPgComp){
		fnDesiableNext(true);
	}
	/* Nikhil A. */
	gPageCompCondVal = 0;
	fnShowHidePlayerControls(true);
	//gPageCompCondArr = new Array();
	pageTracking = new Object();
	pageTracking.id = gModuleArr[lModNum].Lessons[lLessNum].Topics[lTopNum].Pages[lPageNum].attr("ID");
	if( !gIsAssessment ) {
		if(!chckPageVisited(pageTracking.id)){
			gPageCompCondArr = new Array(parseInt(gModuleArr[lModNum].Lessons[lLessNum].Topics[lTopNum].Pages[lPageNum].attr("MAXCONDITION")));
			pageTracking.trackingArr = gPageCompCondArr;
			gPageTrackingArr.push(pageTracking); 
		} else {
			gPageCompCondArr = pageTracking.trackingArr;
		}
	}
	var isVideoPage = gModuleArr[lModNum].Lessons[lLessNum].Topics[lTopNum].Pages[lPageNum].attr("ISVIDEO");
	if( isVideoPage == "Y" || gIsPageSkillCheck ) {
		fnHideShowAudioPlayer( true );
	} else {
		fnHideShowAudioPlayer( false );
	}
	fnDisableNavigation(gIsPageSkillCheck);
	var currPgTitle = gModuleArr[lModNum].Lessons[lLessNum].Topics[lTopNum].Pages[lPageNum].find("PAGETITLE").text();
	var lessonTitle = gModuleArr[lModNum].Lessons[lLessNum].find("LESSONTITLE").text();
	var currPgSrc = gModuleArr[lModNum].Lessons[lLessNum].Topics[lTopNum].Pages[lPageNum].attr("PAGESRC");
	
	//$("#pgTitleFrame").html('<span id="spnPgTitleTxt">'+lessonTitle+'<font color="#ffffff"> | </font>'+currPgTitle+'</span>');
	$("#pgTitleFrame").html('<span id="spnPgTitleTxt">'+currPgTitle+'</span>');
	$('#contentFrame').load(currPgSrc, function(){
		//fnHidePreloader();
		perCachePageImagesAudio();	// Added to solve CSS loading issue :: Page content gets dump on the screen & then the css get applied which is wrong.						
	});
	$('#clsPageNumber').html(fGetPageOfPages());
	setAudioButtonState(bAudioDisable);
}

function fnProgressBar(){
	var perDone = Math.round((gCompPageArr.length/gTotalPages)*100);
	$( "#progressbar" ).progressbar({
		value: perDone
	});
}


function fnDesiableNext(lcase)
{

	if(lcase)
	{
		$("#clsNxtBtn").removeClass("clsGlowNxtBtn");
		$("#clsNxtBtn").removeClass("clsEnaNxtBtn");
		$("#clsNxtBtn").addClass("clsDisNxtBtn");
		$("#clsNxtBtn").unbind("click");
		isNextBtnEnable = false;
	} else {
		$("#clsNxtBtn").removeClass("clsDisNxtBtn");
		$("#clsNxtBtn").addClass("clsEnaNxtBtn");		
		isNextBtnEnable = true;
		$("#clsNxtBtn").click(function(e) {
			fnGetPage("NEXT");
		});
	}
	if(!$("#clsNxtBtn").hasClass("clsGlowNxtBtn")) {
		$(".nextInst").hide();
	}
}

function handelEvent(e){
	fnGetPage(e.data);
}

function fnDesiableBack(lcase)
{
	if(lcase)
	{
		$("#clsBackBtn").removeClass("clsEnaBackBtn");
		$("#clsBackBtn").addClass("clsDisBackBtn");		
		$("#clsBackBtn").unbind("click");
		isBackBtnEnable = false;
	} else {
		$("#clsBackBtn").removeClass("clsDisBackBtn");
		$("#clsBackBtn").addClass("clsEnaBackBtn");			
		isBackBtnEnable = true;
		$("#clsBackBtn").click(function(e) {
			if( isInActivity ) {
	   			fnReturnToHome();
			}else{		
				fnGetPage("PREV");
			}
		});
	}
}

function fnHidePreloader(){
	$(".clsPreloader").hide();
	isPreloadPlayed	= false;
}
// This function is get called from Into.html which is genrated by HTML5 swiffy1.1.1 plugin in Adobe CS5 >> Commmand
function fnHideIntro(){	 
	fnSetCurrSndObj(null);
	$("body").css("background-color","#000");
	$(".courseIntro").hide();
	$(".courseIntro").remove();
	$('.gridContainer').css("visibility","visible");
	fnLoadPage(0, 0, 0, 0);	 // To Load the first page
}
function fnShowPreLoader(){
	isPreloadPlayed	= true;
	$(".clsPreloader").show();
}

function fnSetPageComp(){
	/* Nikhil A. */
	if( !fnCheckPageCompletion() ) {
		return;
	}
 	var lPageId = gModuleArr[gCurrMod].Lessons[gCurrLess].Topics[gCurrTop].Pages[gCurrPage].attr("ID")
	fCheckAndAddToList(lPageId,"Page");
	if(fCheckCompletion(gModuleArr[gCurrMod].Lessons[gCurrLess].Topics[gCurrTop],"Topic"))
	{
		var lTopicId = gModuleArr[gCurrMod].Lessons[gCurrLess].Topics[gCurrTop].attr("ID");
		fCheckAndAddToList(lTopicId,"Topic");
		if(fCheckCompletion(gModuleArr[gCurrMod].Lessons[gCurrLess],"Lesson"))
		{
			var lLessonId = gModuleArr[gCurrMod].Lessons[gCurrLess].attr("ID");
			fCheckAndAddToList(lLessonId,"Lesson");
			if(fCheckCompletion(gModuleArr[gCurrMod],"Module"))
			{
				var lModuleId = gModuleArr[gCurrMod].attr("ID");
				fCheckAndAddToList(lModuleId,"Module");
				if (fCheckCompletion(null,"Course"))
				{
					fSetComplete();
				}
			}
		}
	}
	fnSetCoreData();
	fnSetCompletion();
	//fnProgressBar();
	if(gCurrPageNum!=gTotalPages){
		fnGlowNextBtn();
	}
	if(!isNextBtnEnable && gCurrPageNum!=gTotalPages){
		fnDesiableNext(false);
		
	}
}

//Set Core Data ...
function fnSetCoreData()
{
	//gCompModArr, gCompLessArr, gCompTopArr
	//alert(gCompLessArr )
	var lessData="",topicData="",modData="";
	if(gCompLessArr!="" && gCompLessArr!=undefined)
	{
		lessData = gCompLessArr.join(",")
	}
	if(gCompModArr!="" && gCompModArr!=undefined)
	{
		modData = gCompModArr.join(",")
	}
	if(gCompTopArr!="" && gCompTopArr!=undefined)
	{
		topicData = gCompTopArr.join(",")
	}
	
	//Adding pages topic lessons module that are not completed.....
	lPagesArrDummy = fGetNonCompletedTopicPages();
	lTopicsArrDummy = fGetNonCompletedLessonTopics();
	lLessonsArrDummy = fGetNonCompletedModuleLessons();
	//+fCreateObjToArr(gPageTrackingArr)
	var _trackedData = new Array()
	for(var i=0; i<gPageTrackingArr.length; i++)
	{
		_trackedData[i] =gPageTrackingArr[i].id + "$" + gPageTrackingArr[i].trackingArr;
	}
	var dataSend = lPagesArrDummy.join(",") + "#" + lTopicsArrDummy.join(",") + "#" + lLessonsArrDummy.join(",") + "#" + modData + "#" + _trackedData.join("|"); 
	BVScorm_setcoredata(dataSend)
}

//Functions to calculate the non completed pages topics and lessons.....

//function for non complete topic pages 
function fGetNonCompletedTopicPages()
{
	// Block to add pages which does not belong to any completed topic
	var lPagesArrDummy = new Array();
	var lPagesArrForCompletedTopics = new Array();
	
	for (var i=0;i<gCompTopArr.length;i++){
		var lTopicObj = fGetObjFromID(gCompTopArr[i],"Topic");
		for (var k=0;k<lTopicObj.Pages.length;k++){
			lPagesArrForCompletedTopics[lPagesArrForCompletedTopics.length] = lTopicObj.Pages[k].attr("ID");
		}
	}	
	
	for (var i=0;i<gCompPageArr.length;i++){
		var bFound = false;
		for (var j=0;j<lPagesArrForCompletedTopics.length;j++){
			if (lPagesArrForCompletedTopics[j] == gCompPageArr[i]){
				bFound = true;
				break;
			}
		}
		if (!bFound){
			lPagesArrDummy[lPagesArrDummy.length] = gCompPageArr[i]
		}
	}
	return lPagesArrDummy;
	// Block ends here
} 

//Function for non completed topic in lessons...
function fGetNonCompletedLessonTopics()
{
	// Block to add topics which does not belong to any completed lesson
	var lTopicsArrDummy = new Array();
	var lTopicsArrForCompletedLessons = new Array();
	
	for (var i=0;i<gCompLessArr.length;i++){
		var lLessonObj = fGetObjFromID(gCompLessArr[i],"Lesson");
		for (var k=0;k<lLessonObj.Topics.length;k++){
			lTopicsArrForCompletedLessons[lTopicsArrForCompletedLessons.length] = lLessonObj.Topics[k].attr("ID");
		}
	}	
	
	for (var i=0;i<gCompTopArr.length;i++){
		var bFound = false;
		for (var j=0;j<lTopicsArrForCompletedLessons.length;j++){
			if (lTopicsArrForCompletedLessons[j] == gCompTopArr[i]){
				bFound = true;
				break;
			}
		}
		if (!bFound){
			lTopicsArrDummy[lTopicsArrDummy.length] = gCompTopArr[i]
		}
	}
	return lTopicsArrDummy;
	// Block ends here
}

//function for non completed lessons in modules...
function fGetNonCompletedModuleLessons()
{
// Block to add lessons which does not belong to any completed modules
	var lLessonsArrDummy = new Array();
	var lLessonsArrForCompletedModules = new Array();
	
	for (var i=0;i<gCompModArr.length;i++){
		var lModuleObj = fGetObjFromID(gCompModArr[i],"Module");
		for (var k=0;k<lModuleObj.Lessons.length;k++){
			lLessonsArrForCompletedModules[lLessonsArrForCompletedModules.length] = lModuleObj.Lessons[k].attr("ID");
		}
	}	

	for (var i=0;i<gCompLessArr.length;i++){
		var bFound = false;
		for (var j=0;j<lLessonsArrForCompletedModules.length;j++){
			if (lLessonsArrForCompletedModules[j] == gCompLessArr[i]){
				bFound = true;
				break;
			}
		}
		if (!bFound){
			lLessonsArrDummy[lLessonsArrDummy.length] = gCompLessArr[i]
		}
	}
	return lLessonsArrDummy;
	// Block ends here	
}


//Completion of module....
function fSetComplete()
{
	BVScorm_complete();
}

function fnCheckCourseCompletion(){
	
}

function fnCheckPageComplection(pageID)
{
	for(var i=0;i<gCompPageArr.length;i++)
	{
		if(gCompPageArr[i]==pageID)
		{
			return true;
		}
	}
	return false;
}

function fnSetCompletion(){
	var moduleCounter, lessonCounter, topicCounter, pageCounter, totalPageCounter;
	moduleCounter = 0;
	totalPageCounter = 0;
	oXML.find("MODULE").each(function() {
		lessonCounter = 0;
		$(this).find("LESSON").each(function() {
			topicCounter = 0;
			$(this).find("TOPIC").each(function() {
				pageCounter = 0;
				$(this).find("PAGE").each(function() {
					pageCounter++;
					totalPageCounter++;
				});
				topicCounter++;
			});
			lessonCounter++;
		});
		moduleCounter++;
	});
}

function fnPauseAudio(){
	bAudioPlaying = true;
	playPauseAudio();
	fnPauseVideo();
}

function fnPlayAudio(){
	bAudioPlaying = false;
	playPauseAudio();
	fnPlayVideo();
}
// -----------------------------
// -----------------------------
// For Video stop and play
// -----------------------------
var videoObjectId
var videoPausedBefore = false;
//setVideoObject called from page level to set video object
function setVideoObject(videoId){
	videoObjectId = videoId;
}
function fnPauseVideo(){
	//alert("fnPauseVideo()");	
	var videoObject = document.getElementById(""+videoObjectId);
	if(videoObject){
		  if(isiPAD){
			//*-*-----------------------------
			//Added for iPAD. iPAD keep video control & unable to clik on menu || exit yes no Button		
			$("#vid").removeAttr('controls');
		    }
		if(!videoObject.ended && !videoObject.paused){
			videoObject.pause()
		}else if(videoObject.paused){
			videoPausedBefore = true
		}
	}
}
function fnPlayVideo(){
	var videoObject = document.getElementById(""+videoObjectId);
	if(videoObject){
		if(isiPAD){
			//*-*-----------------------------
			//Added for iPAD. iPAD keep video control & unable to clcik on menu || exit yes no Button
			 $("video").attr("controls", true);
		}	
		if(!videoObject.ended && videoObject.paused && !videoPausedBefore){
			videoObject.play()
		}else if(videoPausedBefore){
			videoPausedBefore = false
		}
	}
}
// -----------------------------

function fGetPageOfPages()
{
	return fGet2DigitNo(gCurrPageNum) + " of " + fGet2DigitNo(gTotalPages);
}

function fGet2DigitNo(lNo)
{
	if(lNo<10)
	{
		return "0"+lNo;
	}
	else
	{
		return lNo;
	}
}

//checking for bookmark
function fnCheckForBookMark()
{
	
	//alert(lesson_location)
	if(IsScrom)
	{
		//For Visited Pages ... 
		if(suspend_data!="" && suspend_data!=undefined)
		{
			var lArrTemp = suspend_data.split("#");
			if(lArrTemp[0]!="" && lArrTemp[0]!=undefined)
			{
				gCompPageArr = new Array();
				gCompPageArr = lArrTemp[0].split(",");
			}
			if(lArrTemp[1]!="" && lArrTemp[1]!=undefined)
			{
				gCompTopArr = new Array();
				gCompTopArr = lArrTemp[1].split(",");
			}
			if(lArrTemp[2]!="" && lArrTemp[2]!=undefined)
			{
				gCompLessArr = new Array();
				gCompLessArr = lArrTemp[2].split(",");
			}
			
			if(lArrTemp[3]!="" && lArrTemp[3]!=undefined)
			{
				gCompModArr = new Array();
				gCompModArr = lArrTemp[3].split(",");
			}
			if(lArrTemp[4]!="" && lArrTemp[4]!=undefined)
			{
				var _trackedData = lArrTemp[4].split("|")
				for(var i=0;i<_trackedData.length;i++){
					var _temp = new Object();
					_temp.id = _trackedData[i].split("$")[0];
					_temp.trackingArr = _trackedData[i].split("$")[1].split(",");
					gPageTrackingArr.push(_temp);
				}
			}
			// Block to set all pages based on lesson, topic, module completion...
			for (var i=0;i<gCompModArr.length;i++){
				var lModuleObj = fGetObjFromID(gCompModArr[i],"Module");
				for (var k=0;k<lModuleObj.Lessons.length;k++){
					fCheckandAppendToArr(gCompLessArr,lModuleObj.Lessons[k].attr("ID"));
				}
			}
		
			for (var i=0;i<gCompLessArr.length;i++){
				var lLessonObj = fGetObjFromID(gCompLessArr[i],"Lesson");
				for (var k=0;k<lLessonObj.Topics.length;k++){
					fCheckandAppendToArr(gCompTopArr,lLessonObj.Topics[k].attr("ID"));
				}
			}
			
			for (var i=0;i<gCompTopArr.length;i++){
				var lTopicObj = fGetObjFromID(gCompTopArr[i],"Topic");
				for (var k=0;k<lTopicObj.Pages.length;k++){
					fCheckandAppendToArr(gCompPageArr,lTopicObj.Pages[k].attr("ID"));
				}
			}
			var lval;
			gAssessmentAchievedScore = ( (lval = BVScorm_getscore()) != -1 ) ? lval : 0;
			BVScorm_score(gAssessmentAchievedScore);
			gIsAssessmentCompleted = (gAssessmentAchievedScore>=gAssessmentPassScore);
			// Block ends
		} else {
			gCompPageArr = new Array();
			gCompModArr = new Array(); 
			gCompLessArr = new Array();
			gCompTopArr = new Array();
		}
		
		if(lesson_location!="" && lesson_location != undefined)
		{
			if (window.confirm("Do you want to go to the last visited page?") != true){
				lesson_location = "";
			}else{
				lesson_location = lesson_location;
			}
		} else {
			//fnLoadPage(gCurrMod, gCurrLess, gCurrTop, gCurrPage);
		}
	} else {
			gCompPageArr = new Array();
			gCompModArr = new Array(); 
			gCompLessArr = new Array();
			gCompTopArr = new Array();
	}
}
function fnMute(){
	$("#clsUnMuteBtn").show();
	$("#clsMuteBtn").hide();
	$('#jquery_jplayer_1').jPlayer("mute");
}
function fnSoundUnMute(){
	$("#clsUnMuteBtn").hide();
	$("#clsMuteBtn").show();
	$('#jquery_jplayer_1').jPlayer("unmute");
}

//Complition functions...
function fCheckAndAddToList(lIdentifier, lCase)
{
	if(lCase=="Page")
	{
		fCheckandAppendToArr(gCompPageArr,lIdentifier)
	}
	else if (lCase=="Topic")
	{
		fCheckandAppendToArr(gCompTopArr,lIdentifier)
	}
	else if (lCase=="Lesson")
	{
		fCheckandAppendToArr(gCompLessArr,lIdentifier)
	}
	else if (lCase=="Module")
	{
		fCheckandAppendToArr(gCompModArr,lIdentifier)
	}
}

function fCheckandAppendToArr(lArr,lIdentifier)
{
	var exists=false
	//alert(lArr);
	for (var i=0;i<lArr.length;i++)
	{
		if (lArr[i]==lIdentifier)
		{
			exists=true
			break;
		}
	}
	if (!exists){
		lArr[lArr.length]=lIdentifier
	}
}

function fCheckCompletion(lObj,lCase)
{
	switch(lCase)
	{
		case "Page":
			for(var i=0;i<gCompPageArr.length;i++)
			{
				if(gCompPageArr[i]==lObj.attr("ID"))
				{
					return true;
				}
			}
			return false;
			
		case "Topic":
			return (lObj.Pages.length<= (fGetCompleteElementCount(lObj.Pages, gCompPageArr)));

		case "Lesson":
			return (lObj.Topics.length <= (fGetCompleteElementCount(lObj.Topics, gCompTopArr)));
		case "Module":
			return (lObj.Lessons.length <= (fGetCompleteElementCount(lObj.Lessons, gCompLessArr)));
		case "Course":
			return (gModuleArr.length <= (fGetCompleteElementCount(gModuleArr, gCompModArr)));
	}
}

function fGetCompleteElementCount(lArr1, lArr2)
{
	var lCount=0
	for (var i=0;i<lArr1.length;i++)
	{
		for (var j=0;j<lArr2.length;j++)
		{
			if (lArr2[j]==lArr1[i].attr("ID"))
			{
				lCount++;
			}
		}
	}
	return lCount;
}

//function for getting objects from there IDS....
function fGetObjFromID(lId,lCase)
{
	var lArrLessons;
	var lArrTopics;
	var lArrPages;
	switch(lCase)
	{
		case "Page":
			for(var i=0;i<gModuleArr.length;i++)
			{
				lArrLessons = gModuleArr[i].Lessons;
				for(var j=0;j<lArrLessons.length;j++)
				{
					lArrTopics = lArrLessons[j].Topics;
					for(var k=0;k<lArrTopics.length;k++)
					{
						lArrPages=lArrTopics[k].Pages;
						for(var l=0;l<lArrPages.length;l++)
						{
							if(lArrPages[l].attr("ID")==lId)
							{
								return lArrPages[l];
							}
						}
					}
				}
			}
			return null;

		case "Topic":
			for(var i=0;i<gModuleArr.length;i++)
			{
				lArrLessons = gModuleArr[i].Lessons;
				for(var j=0;j<lArrLessons.length;j++)
				{
					lArrTopics = lArrLessons[j].Topics;
					for(var k=0;k<lArrTopics.length;k++)
					{
						if(lArrTopics[k].attr("ID")==lId)
						{
							return lArrTopics[k];
						}
					}
				}
			}
			return null;

		case "Lesson":
			for(var i=0;i<gModuleArr.length;i++)
			{
				lArrLessons = gModuleArr[i].Lessons;
				for(var j=0;j<lArrLessons.length;j++)
				{
					if(lArrLessons[j].attr("ID")==lId)
					{
						return lArrLessons[j];
					}
				}
			}
			return null;

		case "Module":
			for(var i=0;i<gModuleArr.length;i++)
			{
				if(gModuleArr[i].attr("ID")==lId)
				{
					return gModuleArr[i];
				}
			}
			return null;
	}
}
//For Page Pre Loading Audio images and all.....
function perCachePageImagesAudio(){
	if(manifest.length>0){
		loader.loadManifest(manifest);
		loader.onComplete = handlePageComplete;
	}else{
		if( _currAudioPlayingPath == null ) {
			fnHidePreloader();
		} else {
			initAudioPlayer(_currAudioPlayingPath);
		}
	}
	
}

function handlePageComplete()
{
	if( _currAudioPlayingPath == null ) {
		fnHidePreloader();
	} else {
		initAudioPlayer(_currAudioPlayingPath);
	}
}

function fnTranscript() {
	if(isTransEnable){
		$("#transcriptBtn").removeClass("clsTransOffBtn");
		$("#transcriptBtn").addClass("clsTransOnBtn");
		fnhideTranscript();
		isTransEnable = false;		
	}else{
		$("#transcriptBtn").removeClass("clsTransOnBtn");
		$("#transcriptBtn").addClass("clsTransOffBtn");	
		fnshowTranscript();
		isTransEnable = true;
	}
}

function fnshowTranscript() {
	$("#transcriptbar").show();
}

function fnhideTranscript() {
	$("#transcriptbar").hide();
}

function fnSoundOnOff() {
	if(!bAudioMute) {
		if(!bFlashFallbackActive) {
			audioObjRef.muted = true;
		} else {
			fallbackFlashRef.muteUnmuteAudio(0);
		}
		$("#clsAudioOnBtn").css("display","none");
		$("#clsAudioOffBtn").css("display","inline");
		isAudioEnable = false;
	} else {
		if(!bFlashFallbackActive) {
			audioObjRef.muted = false;
		} else {
			fallbackFlashRef.muteUnmuteAudio(1);
		}			
		$("#clsAudioOffBtn").css("display","none");
		$("#clsAudioOnBtn").css("display","inline");
		isAudioEnable = true;
	}
	
	try {
		var videoObj = document.getElementById(""+videoObjectId);
		if( videoObj ) {
			if(!bAudioMute) {
				videoObj.muted = true;
			} else {
				videoObj.muted = false;
			}
		}
	} catch(e) {
	}
	
	bAudioMute = !(bAudioMute);
}

function fnSoundOn() {
	//$("#jquery_jplayer_1").jPlayer("volume", 1);
}

function fnSoundOff() {
	//$("#jquery_jplayer_1").jPlayer("volume", 0);
}

function playerToggleAciteClass() {
	$("#clsNxtBtn").toggleClass("activcls");
	$("#clsAudioOffBtn").toggleClass("activcls");
	$("#clsAudioOnBtn").toggleClass("activcls");
	$("#clsExitBtn").toggleClass("activcls");
	$("#clsHelpBtn").toggleClass("activcls");
	$("#clsMnuBtn").toggleClass("activcls");
	$("#clsBackBtn").toggleClass("activcls");
	$("#popupClose").toggleClass("activcls");
	$("#clsReplay").toggleClass("activcls");
	$("#transcriptBtn").toggleClass("activcls");
}

//Audio Sync functionality
function UpdateTheTime(time) {
	//currCue
	if(currCue >= updateContentArray.length){
		return;
	}
	if(time > (Number(updateContentArray[currCue].time))) {
		//Update Transcript
		if(updateContentArray[currCue].transcript != "" && updateContentArray[currCue].transcript!= undefined){
			showTranscript(updateContentArray[currCue].transcript);
		}
		//Show/Hide Div for audio sync
		if(updateContentArray[currCue].hide != "" && updateContentArray[currCue].hide!= undefined){
			hideDiv(updateContentArray[currCue].hide, updateContentArray[currCue].show);
		}else if(updateContentArray[currCue].show != "" && updateContentArray[currCue].show!= undefined){
			showDiv(updateContentArray[currCue].show);
		}
		//Call function if any e.g. fnAudioFinishPageComp() function is called at end of audio
		if(updateContentArray[currCue].event !="" && updateContentArray[currCue].event!= undefined){
			eval(updateContentArray[currCue].event)
		}
		currCue++;
	}
}

function showTranscript(str) {
	//console.log("currCue "+currCue+" "+str);
	//transcriptText.html(str);
}

function hideDiv(str, showStr) {
	var tempArray = [];
	if(str.indexOf(",")>-1){
		tempArray = str.split(",");
	}else{
		tempArray[0]= str
	}
	for(var i=0; i<tempArray.length; i++){
		$(tempArray[i]).animate({opacity:0},200,function(){
			if(tempArray[i]==".helpHighlight"){
				$(tempArray[i]).css("display","none");
			}else{
				$(tempArray[i]).css("visibility","hidden");
			}
			if(showStr != "" && showStr != undefined){
				showDiv(showStr);
			}					
		});
	}
}

function showDiv(str) {
	var tempArray = [];
	if(str.indexOf(",")>-1){
		tempArray = str.split(",");
	} else {
		tempArray[0]= str
	}
	for(var i=0; i<tempArray.length; i++) {
		//fraud for help highligh
		if(tempArray[i]==".helpHighlight") {
			$(tempArray[i]).css("display","inline");
		} else {
			$(tempArray[i]).css("opacity","0");
			$(tempArray[i]).css("visibility","visible")
			$(tempArray[i]).animate({opacity:1},200)
		}
	}
}

function fnSetCurrSndObj(_mp3) {
	if(typeof(audioObjRef) != "string"){
	audioObjRef.setAttribute('src', null);
	audioObjRef.load();
	}
	_currAudioPlayingPath = null;
	bAudioCompleted = true;
	bAudioPlaying = false;
}

function checkIpadAudio() {
	if(iPadAudioPlayed) {
		return
	}
	if((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) 
	{
	var ver = iOSversion();
		if(ver[0]>5){
		return;
		}
		var $a = $('<div id="dummyBtn" style="" ><div id="dummyBtnBk"></div><a href="#" id="fakeClick"  style="position: absolute; top:40%;left:40%;"><img src="media/images/play_btn.png" /></a></div>');
		$a.bind("click", function(e) {
			e.preventDefault();
			sndInit()
		});
		$("body").append($a);
		function sndInit(){
			$('#jquery_jplayer_1').jPlayer("play")
			iPadAudioPlayed = true;
			$a.remove()
		}
	}
}

function initializeJplayer(audioPath) {
	_currAudioPlayingPath = audioPath;
	if( isIntroPage ) {
		initAudioPlayer(audioPath);
	}
}
function fnAudioFinishPageComp() {
	if(iOS) {
		isAudioStreamPlaying = false;
		$("#jplayerPlay").removeClass("jp-play");
		$("#jplayerPlay").addClass("jp-play-deactive");	
		fnSetCurrSndObj(null);
		fnSetPageComp();
	}
}
function fnGlowNextBtn() {
		$(".nextInst").show();
		$("#clsNxtBtn").removeClass("clsDisNxtBtn");
		$("#clsNxtBtn").removeClass("clsGlowNxtBtn");
		$("#clsNxtBtn").removeClass("clsEnaNxtBtn");
		$("#clsNxtBtn").addClass("clsGlowNxtBtn");
}
function iOSversion() {
  if (/iP(hone|od|ad)/.test(navigator.platform)) {
    // supports iOS 2.0 and later: <http://bit.ly/TJjs1V>
    var v = (navigator.appVersion).match(/OS (\d+)_(\d+)_?(\d+)?/);
    return [parseInt(v[1], 10), parseInt(v[2], 10), parseInt(v[3] || 0, 10)];
  }
}	
function highlightHelp() {
	$("#clsHelpBtn").addClass("highlight");
}
function removeHighlightHelp() {
	$("#clsHelpBtn").removeClass("highlight");
}


/* Nikhil A. */
/****************************************************************/
function fnResetCurrentCue() {
	currCue = 0;
}

function fnUpdatePageCompletionCounter( index ) {
	if( index >= 0 ) {
		gPageCompCondArr[index] = 1;
		pageTracking.trackingArr = gPageCompCondArr;
	}
	fnSetPageComp();
}

function fnCheckPageCompletion() {
	var isPageCompleted = true;
	for(var i = 0; i < gPageCompCondArr.length; i++ ) {
		if ( gPageCompCondArr[i] != 1 ) {
			isPageCompleted = false;
		} 
	}
	return isPageCompleted;
}

function fnAudioProgressBar(currTime, totalTime){
	$('.progressToolTip').attr('data', fnAudioProgressTime(currTime, totalTime));
	var perDone = Math.round((currTime/totalTime)*100);
	$( "#progressbar" ).progressbar({
		value: perDone
	});
}

function fnResetAudioProgressBar() {
	var perDone = 0;
	$( "#progressbar" ).progressbar({
		value: perDone
	});
}

function fnHideShowAudioPlayer( isToHide ) {
	if( isToHide ) {
		$("#jp_container_1").css("visibility","hidden");
		$("#progressbar").css("visibility","hidden");
		$(".progressToolTip").css("visibility","hidden");
		$('#clsPlayPauseBtn').css("visibility","hidden");
	} else {
		$("#jp_container_1").css("visibility","visible");
		$("#progressbar").css("visibility","visible");
		$(".progressToolTip").css("visibility","visible");
		$('#clsPlayPauseBtn').css("visibility","visible");
	}
}

function fnChangeTitleText( str ) {
	$("#pgTitleFrame").html('<span id="spnPgTitleTxt">'+str+'</span>');
}

function fnAudioProgressTime(currTime, totalTime) {
	return ( msToTime(currTime) + ' / ' + msToTime(totalTime) );
}

function msToTime(s) {
	var ms = s % 1000;
	s = (s - ms) / 1000;
	var secs = s % 60;
	s = (s - secs) / 60;
	var mins = s % 60;
	var hrs = (s - mins) / 60;
	var secss = secs.toPrecision(2);
	var mss = ms.toPrecision(2);
	if( parseInt(secss) < 10 ) {
		secss = Math.floor(secss);
		secss = '0' + secss;
	}
	if( parseInt(mss) < 10 ) {
		mss = Math.floor(mss);
		mss = '0' + mss;
	} 
	if( secss > 0 ) {
		return secss + ':' + mss;
	} else {
		return '00:' + mss;
	}
	
}

function fnReplayPage() {
	
}

function fnIsCourseCompleted() {
	for(var i = 0; i < gModuleArr.length; i++) {
		var lArrLessons = gModuleArr[i].Lessons;
		for(var j = 0; j < lArrLessons.length; j++) {
			var lArrTopics = lArrLessons[j].Topics;
			for(var k = 0; k < lArrTopics.length; k++) {
				var lArrPages = lArrTopics[k].Pages;
				for(var l = 0; l < lArrPages.length; l++) {
					if( lArrPages[l].attr("MARKCOURSECOMPLETION") == "T" ) {
						return ( fCheckCompletion(gModuleArr[i].Lessons[j].Topics[k].Pages[l], "Page") );
					}
				}
			}
		}
	}
	return false;
}

function fnJumptoAcknowledgmentPage() {
	var ackPage = undefined;
	for(var i = 0; i < gModuleArr.length; i++) {
		var lArrLessons = gModuleArr[i].Lessons;
		for(var j = 0; j < lArrLessons.length; j++) {
			var lArrTopics = lArrLessons[j].Topics;
			for(var k = 0; k < lArrTopics.length; k++) {
				var lArrPages = lArrTopics[k].Pages;
				for(var l = 0; l < lArrPages.length; l++) {
					if( lArrPages[l].attr("MARKCOURSECOMPLETION") == "T" ) {
						ackPage = gModuleArr[i].Lessons[j].Topics[k].Pages[l].attr("ID");
					}
				}
			}
		}
	}
	if( ackPage != undefined ) {
		fnJumpToPageByID(ackPage);
	}
}

function fnAudioFinished() {
	isAudioStreamPlaying = false;
	$("#jplayerPlay").removeClass("jp-play");
	$("#jplayerPlay").addClass("jp-play-deactive");	
	fnSetCurrSndObj(null);
	fnSetPageComp();
}

function fnGetUserName() {
	return ( BVScorm_GetUserName() );
}

function fnIsInActivity() {
	isInActivity = true;
}

function fnIsNotInActivity() {
	isInActivity = false;
}

function fnShowHidePlayerControls(val) {
	if( val ) {
		$('#clsBackBtn').css('display', 'block');
		$('#clsNxtBtn').css('display', 'block');
		$('#clsMnuBtn').css('display', 'block');
		$('#clsHelpBtn').css('display', 'block');
		$('#clsReplay').css('display', 'block');
		if( fnCheckPageComplection(gModuleArr[gCurrMod].Lessons[gCurrLess].Topics[gCurrTop].Pages[gCurrPage].attr("ID")) ){
			if($("#clsNxtBtn").hasClass("clsEnaNxtBtn")) {
				$(".nextInst").show();
			}
		}
	} else {
		/*$('#clsBackBtn').css('display', 'none');
		$('#clsNxtBtn').css('display', 'none');*/
		$('#clsMnuBtn').css('display', 'none');
		$('#clsHelpBtn').css('display', 'none');
		$('#clsReplay').css('display', 'none');
		/*if( fnCheckPageComplection(gModuleArr[gCurrMod].Lessons[gCurrLess].Topics[gCurrTop].Pages[gCurrPage].attr("ID")) ){
			$(".nextInst").hide();
		}*/
	}
}

/****************************************************************/
//tracking functions
function set(ID,value) {
	with (parent)
		{
		//set identifier if not already been set (now only set once)
		if (course.trackedObjects[course.currentSco.currentPage.trackingid] != 1||(course.trackedObjects[course.currentSco.currentPage.trackingid]==1&&course.currentSco.currentPage.parentItem.onePage!=null))
			{
			course.trackedObjects[ID] = value;
			//if the page is complete and progress hasnt already been incremented
			if (course.trackedObjects[course.currentSco.currentPage.trackingid] == 1 && course.currentSco.currentPage.updateProgress == true)
				{
				course.progressInd += 1; //increment the progress indicator
				course.currentSco.currentPage.updateProgress = false; //set the page so doesnt increment again
				// if object loaded (progress.js)
				if (course.currentSco.currentPage.objLoaded == true)
					{
					getProgressImage(); //call function in progress.js to update progress image
					}
				}
			}
		if (course.lockdown==MODE_LOCK && course.trackedObjects[course.currentSco.currentPage.trackingid]==1&&course.currentSco.currentPage.parentItem.onePage!=true)
			{
			unblock();
			}
		else if (course.lockdown==MODE_LOCK && course.trackedObjects[course.currentSco.currentPage.trackingid]==1&&course.currentSco.currentPage.parentItem.onePage==true&&course.trackedObjects[course.currentSco.currentPage.parentItem.trackingid]==1)
			{
			unblock();
			}
		}
	checkCompletion1();
	}
function checkCompletion1() {
	var totalKeys = 0, completedKeys = 0;
	for (var key in parent.course.trackedObjects) {
		if (typeof(parent.course.trackedObjects[key])=='number') {
			totalKeys++;
			if(parent.course.trackedObjects[key] == 1) {
				completedKeys++;
			}
		}
	}
	if(completedKeys == totalKeys) {
		parent.course.commit();
	}
}

function get(ID)
	{
	return parent.course.trackedObjects[ID]
	}

//status functions
function completion()
	{
	var sum = 0;
	for (var i=0; i<completion.aruments.length; i++)
		{
		sum += get(completion.arguments[i]);
		}
	var retval = (sum / completion.aruments.length);
	if (retval!=0 && retval!=2) {retval = 1;}
	return retval
	}
function chckPageVisited(pageId){
	for(var i=0; i<gPageTrackingArr.length;i++){
		if(gPageTrackingArr[i].id == pageId)
		{
			pageTracking = gPageTrackingArr[i]
			return true;
		}
	}
	return false;
}

function fCreateObjToArr(lObj) {
	if(lObj[0] == undefined && lObj != undefined) {
		lTempAObject = lObj;
		lObj = new Array();
		lObj[0] = lTempAObject;
	}
	return lObj;
}

////To resize page;
function resizeFrame() {
	var windowWidth = $(window).width();
	var windowHeight = $(window).height();
	var docWidth = $(document).width();
	var docHeight = $(document).height();
	  
	
	  var mainWidth = $('.player-container').width();
	  var mainHeight = $('.player-container').height();		  
	  var minWidth = 940;
	  var minHeight = 653;
	  
	  function resizeWidth() {
		var newWidth = windowWidth;
		$('.player-container').css('width',newWidth);
		var newHeight = newWidth / minWidth * minHeight;
		$('.player-container').css('height',newHeight);
		mainWidth = $('.player-container').width();
		mainHeight = $('.player-container').height();

		////To set font according to width;
		var preferredWidth = 940;
		var fontsize = 14;
		var displayWidth = $(window).width();
		var percentage = displayWidth / preferredWidth;
		var newFontSize = Math.floor(fontsize * percentage) - 1;				
		$("body").css("font-size", newFontSize);			
	  }
	  
	 function resizeHeight() {				  
		var newHeight = windowHeight;
		$('.player-container').css('height',newHeight);
		var newWidth = newHeight / minHeight * minWidth;
		$('.player-container').css('width',newWidth);
		mainWidth = $('.player-container').width();
		mainHeight = $('.player-container').height();		

		////To set font according to Height;
		var preferredHeight = 653;
		var fontsize = 14;
		var displayHeight = $(window).height();
		var percentage = displayHeight / preferredHeight;				
		var newFontSize = Math.floor(fontsize * percentage) - 1;				
		$("body").css("font-size", newFontSize);	
		
	  }			  
	   if (windowWidth >= docWidth) {
		resizeWidth();				
	  }
	  if (windowHeight < mainHeight) {
		resizeHeight();				
	  }
	  
	  ////To align conent into center of the page.
	    var space = $(document).width() - $(".player-container").width();
	  
	 if(isSTab()){
		//$(".player-container").css("left", String(	(space/4)+'px')	 );  
		$(".player-container").css("left", space/2 ); 
	  }else{		 
		$(".player-container").css("margin-left", space/2 );  
	  }
	  
}
////End Resize;

function isSTab(){	
	return navigator.platform.toLowerCase().indexOf("linux")==0;	
}

function onAudioStart()
{
	if( _currAudioPlayingPath != null ) {
		fnHidePreloader();
		fnEnablePlay();
	}
	
}
function onAudioTimeUpdate()
{
	if( _currAudioPlayingPath != null ) {
	
		if(audioObjRef != undefined)
		{
			UpdateTheTime( audioObjRef.currentTime );

			var currentTime = audioObjRef.currentTime;
			var duration = audioObjRef.duration;
			fnAudioProgressBar( currentTime, duration );

			if(bPageStateChange && (audioObjRef.currentTime*100) > 25)
			{
				bPageStateChange = false;
				bAudioPlaying = true;
				//playPauseAudio();
			}
			if ( (audioObjRef.currentTime >= (audioObjRef.duration - 0.3))&&(bAudioPlaying) )
			{
				//Stopping audio .3 ms before end to enable replay on devices, firing onEnd event from here itself...
				//Solved looping issue in android
				//audioObjRef.currentTime = 0.1;
				audioObjRef.currentTime = audioObjRef.duration - 0.15;
				audioObjRef.pause();
				//alert("call audio ended")
				onAudioEnd();
			}
		}
	}
}
function onFlashAudioTimeUpdate()
{	
	bPageStateChange = false;
	bAudioPlaying = true;
	$('#clsPlayPauseBtn').removeClass("clsPlay");
	$('#clsPlayPauseBtn').addClass("clsPause");
}

function onAudioEnd()
{
	bAudioCompleted = true;
	bAudioPlaying = false;
	if( !isIntroPage ) {
		fnDisablePlay();
		try {
			fnCheckAndMarkItemCompleted();
		} catch(e) {
		}
		fnSetPageComp();
	}
	fnSetCurrSndObj(null);
}

var audioObjRef = ""; //To hold reference to the audio tag in player html
var fallbackFlashRef = ""; //To hold reference of fallback flash object

function replayAudio()
{
	if(!bFlashFallbackActive)
	{
		//audioObjRef.currentTime = 0.1;
		initAudioPlayer(_currAudioPlayingPath);
		//audioObjRef.play(); - Playing in playPauseAudio method
	}
	else
	{
		fallbackFlashRef.replayAudio();
	}
}

var bCallBackAdded = false;
function initAudioPlayer(audioPathParam)
{
	fnShowPreLoader();
	if(audioObjRef == "") {
		audioObjRef = document.getElementById("audioplayer");
		audioObjRef.loop = false;
		/*audioObjRef.addEventListener("loadedmetadata", function()
		  {
			//alert('loadedmetadata');
		  }
		);
		audioObjRef.addEventListener("loadeddata", function()
		  {
			/*alert('loadeddata');
			if( !isIntroPage ) audioObjRef.pause();
		  }
		);*/
		/*audioObjRef.addEventListener("canplay", function()
		  {
			//alert('canplay::isIntroPage:-' + isIntroPage);
			/*if( !isIntroPage ) { 
				try {
					alert('audioObjRef = ' + audioObjRef);
					alert('audioObjRef.autoplay = ' + audioObjRef.autoplay);
					audioObjRef.pause();
				} catch (e) {
					alert('ERERIOEMRENRN');
				}
			}
		  }
		);*/
		/*audioObjRef.addEventListener("loadstart", function()
		  {
			//alert('loadstart');
		  }
		);
		audioObjRef.addEventListener("error", function(e)
		  {
			//alert( $('#audioplayer').attr('src') );
			//alert( "Error loading audio file:-" + e );
		  }
		);
		audioObjRef.addEventListener("ended", function(e)
		  {
			//alert( 'ended' );
			//onAudioEnd();
		  }
		);*/
	}
	/*}*/
	if(fallbackFlashRef == "")
	{
		//fallbackFlashRef = document.getElementById("flashAudio");
	}
	initAudioSource(audioPathParam);
}

function initAudioSource(audioPathParam)
{
 	if (audioObjRef.canPlayType)
 	{
 		bFlashFallbackActive = false;
		if ( "" != audioObjRef.canPlayType('audio/mpeg')) {
		 	audioObjRef.setAttribute('src', audioPathParam+'.mp3');
		 	audioObjRef.setAttribute('type', 'audio/mpeg');
		} else if ( "" != audioObjRef.canPlayType('audio/ogg; codecs="vorbis"')) {
		 	audioObjRef.setAttribute('src', audioPathParam+'.ogg');
		 	audioObjRef.setAttribute('type', 'audio/ogg');
		}
		audioObjRef.load();
		bAudioPlaying = true;

	} else {
		//Browser doesn't support audio tag, using fallback to load and play sound
		bFlashFallbackActive = true;
		//Using flash fallback to play audio (for non html5 compatible browsers)
		fallbackFlashRef.loadMp3(audioPathParam+'.mp3');
	}
}

function playPauseAudio()
{
	//alert('bAudioPlaying ' + bAudioPlaying +"_____"+ audioObjRef.currentTime );
	try {
		if(bAudioPlaying && bAudioDisable.toUpperCase() != "TRUE")
		{
			if(!bFlashFallbackActive)
			{
				audioObjRef.pause();
			} else {
				fallbackFlashRef.pauseAudio();
			}
			$('#clsPlayPauseBtn').removeClass("clsPause");
			$('#clsPlayPauseBtn').addClass("clsPlay");
		} else {
			if(!bFlashFallbackActive)
			{
				audioObjRef.play();
			} else {
				fallbackFlashRef.playAudio();
			}
			$('#clsPlayPauseBtn').removeClass("clsPlay");
			$('#clsPlayPauseBtn').addClass("clsPause");

		}
		bAudioPlaying = !(bAudioPlaying);
	} catch (e){
	}
}

function setAudioButtonState(audioStateParam) {
	if(audioStateParam.toUpperCase() == "TRUE") {
		$("#clsAudioBtn").attr("disabled", "disabled").css("cursor", "default");
		$("#clsReplayBtn").attr("disabled", "disabled").css("cursor", "default");
		$("#clsPlayPauseBtn").attr("disabled", "disabled").css("cursor", "default");

		$("#clsAudioBtn").fadeTo('slow', 0.5);
		$('#clsReplayBtn').fadeTo('slow', 0.5);
		$("#clsPlayPauseBtn").fadeTo('slow', 0.5);

		$('#clsAudioBtn').removeClass("clsAudioUnmute");
		$('#clsAudioBtn').removeClass("clsAudioMute");
		$('#clsReplayBtn').removeClass("clsReplay");
		
		$("#clsPlayPauseBtn").removeClass("clsPlay");
		$("#clsPlayPauseBtn").removeClass("clsPause");
		$('#clsPlayPauseBtn').addClass("clsPlayDisable");

		$('#clsAudioBtn').addClass("clsUnmuteDisable");
		$('#clsReplayBtn').addClass("clsReplayDisable");
	} else {
		$("#clsAudioBtn").removeAttr('disabled').css("cursor", "pointer");
		$("#clsReplayBtn").removeAttr('disabled').css("cursor", "pointer");
		$("#clsPlayPauseBtn").removeAttr('disabled').css("cursor", "pointer");

		$("#clsAudioBtn").fadeTo('slow', 1);
		$('#clsReplayBtn').fadeTo('slow', 1);
		$('#clsPlayPauseBtn').fadeTo('slow', 1);

		$('#clsAudioBtn').removeClass("clsUnmuteDisable");
		$('#clsAudioBtn').addClass("clsAudioUnmute");
		$('#clsPlayPauseBtn').removeClass("clsPlayDisable");
		$('#clsPlayPauseBtn').addClass("clsPlay");

		$('#clsReplayBtn').removeClass("clsReplayDisable");
		$('#clsReplayBtn').addClass("clsReplay");
	}
}

function fnEnablePlay() {
	$('#clsPlayPauseBtn').removeClass("clsPlayDisable");
	$('#clsPlayPauseBtn').removeClass("clsPlay");
	$('#clsPlayPauseBtn').addClass("clsPause");
	$("#clsPlayPauseBtn").css("cursor", "pointer");
}

function fnDisablePlay() {
	$("#clsPlayPauseBtn").removeClass("clsPlay");
	$("#clsPlayPauseBtn").removeClass("clsPause");
	$('#clsPlayPauseBtn').addClass("clsPlayDisable");
	$("#clsPlayPauseBtn").css("cursor", "default");
}

function fnRemoveAudio() {
	//$("#audioplayer").remove();
}

function fnIsIntroPage() {
	isIntroPage = true;
}

function fnUpdateAssessmentScore(val) {
	gAssessmentCorrectAnswers += val;
	fnCalculateAssessmentScore();
	fnGetPage("NEXT");
}

/*
* Called from result page
*/
function fnCalculateAssessmentResult() {
	gIsAssessmentCompleted = (gAssessmentAchievedScore>=gAssessmentPassScore);
	fnMarkAssessmentStatus();
}

/*
* Calculates assessment score
*/
function fnCalculateAssessmentScore() {
	gAssessmentAchievedScore = Math.ceil((gAssessmentCorrectAnswers/gAssessmentTotalQuestions)*100);
}

/*
* Re-Init Assessment
*/
function fnReInitAssessment() {
	gAssessmentAchievedScore = 0;
	gAssessmentCorrectAnswers = 0;
	gAssessmentTotalQuestions = -1;
	gAssessmentCurrentAttempt += 1;
}

/*
* Disable/Enable Shell buttons for skill check pages
*/
function fnDisableNavigation(val) {
	//fnHideShowAudioPlayer(val);
	fnHideShellBtn($("#clsMnuBtn"), val);
	fnHideShellBtn($("#clsHelpBtn"), val);
	fnHideShellBtn($("#clsAudioOffBtn"), val);
	fnHideShellBtn($("#clsAudioOnBtn"), val);
	fnHideShellBtn($("#clsReplay"), val);
	//(val) ? fnRemoveShellEvents() : fnAddShellEvents();
	/*fnDisableShellBtn($("#clsMnuBtn"), val);
	fnDisableShellBtn($("#clsHelpBtn"), val);
	fnDisableShellBtn($("#clsAudioOffBtn"), val);
	fnDisableShellBtn($("#clsAudioOnBtn"), val);*/
}

function fnHideShellBtn(instanceObj, lcase) {
	(lcase) ? (instanceObj.css("visibility","hidden")) : (instanceObj.css("visibility","visible"));
}

function fnDisableShellBtn(instanceObj, lcase) {
	if( (lcase) && (instanceObj.hasClass("activcls")) ) {
		instanceObj.toggleClass("activcls");
		
	} else if( (!lcase) && (!instanceObj.hasClass("activcls")) ) {
		instanceObj.toggleClass("activcls");
	}
}

function fnAddShellEvents() {
	$("#clsAudioOffBtn,#clsAudioOnBtn").click(function(e) {
		if(isActive(this))
		  fnSoundOnOff();
	});
	$("#clsMnuBtn").click(function(e) {
		if(isActive(this)) {
			fnLoadMenu();
		} else {
			disablePopup();
		}
	});
	$("#clsHelpBtn").click(function(e) {
		if(isActive(this))
		  fnLoadHelpPopup();
	});
}

function fnRemoveShellEvents() {
	$("#clsAudioOffBtn,#clsAudioOnBtn").unbind("click");
	$("#clsMnuBtn").unbind("click");
	$("#clsHelpBtn").unbind("click");
}

/*
* Called from result page
*/
function fnRetakeAssessment() {
	fnReInitAssessment();
	var lPageId = gModuleArr[gCurrMod].Lessons[gCurrLess].Topics[gCurrTop].Pages[0].attr("ID");
	fnJumpToPageByID(lPageId);
	//fnLoadPage(gCurrMod, gCurrLess, gCurrTop, 0);
}

/*
* Marks Assessment data to LMS
*/
function fnMarkAssessmentStatus() {
	//fnSendLMSData();
	BVScorm_score(gAssessmentAchievedScore);
	if(gIsAssessmentCompleted) { 
		BVScorm_Pass();
		BVScorm_complete() 
	} else {
		BVScorm_Fail()
		BVScorm_incomplete();
	}
	
}

/***************************************************
Flash Audio player methods:
--------------------------
	1) loadMp3(mp3Path)
	2) onSndLoadComplete
	3) playAudio
	4) pauseAudio
	5) replayAudio
	6) stopAudio
****************************************************/
