//*************************************************************************************************
//Amit :: Added to control Video 
//var isFLVVideo = false;
//--------------------------------------------------------------------------------------
/*function fVideoPlayingType(fileType){
	//alert("SHREE="+fileType);
	if(fileType == "FLV"){
		isFLVVideo = true
	}else{
		isFLVVideo = false;
		//fVideoPageInit()
	}
}*/
//--------------------------------------------------------------------------------------
function fnPauseVideo(){	
var ua = navigator.userAgent.toLowerCase();
var isiPAD = ua.indexOf("ipad") > -1; //&& ua.indexOf("mobile");

isiPAD = true;

var videoWidth = $("#flashVid").attr("width");
var videoHeight = $("#flashVid").attr("height");
	if(!Modernizr.video){
	
	if(document.getElementById("vid")!= null){
		try{ // HACk for IE8
			thisMovie("flashVid").jPauseVideo();
		}catch(e){}
		//*-*-----------------------------
		//Added for iPAD. iPAD keep video control & unable to clik on menu || exit yes no Button		
			/*$("#vid").hide();
			//$("#VideoContainer").css("background","url('"+document.getElementById("vid").poster+"') no-repeat");
$("#VideoContainer").css({"background":"url('"+document.getElementById("vid").poster+"') no-repeat",'width':''+videoWidth+'','height':''+videoHeight+''});					

			*/	
		//*-*-----------------------------
		}
	 }else{
		if(document.getElementById("vid")!= null){	
			if(isiPAD){
				document.getElementById("vid").pause();		
				//*-*-----------------------------
				//Added for iPAD. iPAD keep video control & unable to clik on menu || exit yes no Button		
				$("#vid").hide();												
			$("#VideoContainer").addClass("onExitVideoContainer");
			$("#VideoContainer").css({"background":"url('"+document.getElementById("vid").poster+"') no-repeat"});								
//$("#VideoContainer").css({"border":"dashed","float": "right","padding-right":"8%","background":"url('"+document.getElementById("vid").poster+"') no-repeat",'width':''+videoWidth+'','height':''+videoHeight+''});					
				//*-*-----------------------------
			}else{
				document.getElementById("vid").pause();				
			}			
		}
	}	
}
//--------------------------------------------------------------------------------------
function fnPlayVideo(){
	var ua = navigator.userAgent.toLowerCase();
	var isiPAD = ua.indexOf("ipad") > -1; //&& ua.indexOf("mobile");

	if(!Modernizr.video){				
		//if(thisMovie("flashVid")!= null && thisMovie("flashVid")!= undefined && thisMovie("flashVid")!= "undefined") {
			try{ // HACk for IE8
				thisMovie("flashVid").jPlayVideo();
			}catch(e){}
			
		//}		
		
		//*-*-----------------------------
		//Added for iPAD. iPAD keep video control & unable to clcik on menu || exit yes no Button
			/*$("#VideoContainer").css("background","none");
			$("#vid").show();*/
		//*-*-----------------------------
	}else{
		if(document.getElementById("vid")!= null){		
			if(isiPAD){
				document.getElementById("vid").play();
				//*-*-----------------------------
				//Added for iPAD. iPAD keep video control & unable to clcik on menu || exit yes no Button
					$("#VideoContainer").removeClass("onExitVideoContainer");
					$("#VideoContainer").css("background","none");
					$("#vid").show();
				//*-*-----------------------------
			}else{
				document.getElementById("vid").play();
			}
		}
		
	}
}
//--------------------------------------------------------------------------------------
function fOnVideoPageComplete(){ //Added to call from Flash Video player for IE8	
	//isFLVVideo = false;
	/* fnSetPageComp();
	if(gCurrPageNum<gTotalPages){		
		fnGetPage("NEXT");
	} */
}	
//--------------------------------------------------------------------------------------
function fOnVideoPageLoaded(){
	
}	
//--------------------------------------------------------------------------------------
function thisMovie(movieName) {
    if (navigator.appName.indexOf("Microsoft") != -1) {
        return window[movieName]
    }
    else {
        return document[movieName]
    }
}
//--------------------------------------------------------------------------------------
//*************************************************************************************************