// JavaScript Document
/*********** Player level constants and attributes are initialized here ********************/

function fnCurrentPageObj(lPageId){
	var moduleCounter, lessonCounter, topicCounter, pageCounter, totalPageCounter;
	moduleCounter = 0;
	totalPageCounter = 0;
	var isPageAvailable = false;
	oXML.find("MODULE").each(function() {
		lessonCounter = 0;
		$(this).find("LESSON").each(function() {
			topicCounter = 0;
			$(this).find("TOPIC").each(function() {
				pageCounter = 0;
				$(this).find("PAGE").each(function() {
					if($(this).attr("ID") == lPageId){
						gCurrPage = pageCounter;
						gCurrTop = topicCounter;
						gCurrLess = lessonCounter;
						gCurrMod = moduleCounter;
						gCurrPageNum = (totalPageCounter+1); 
						isPageAvailable = true;
					}
					pageCounter++;
					totalPageCounter++;
					//alert('XML file loaded successfully >>> ' + $(this).find("PAGETITLE").text());
				});
				topicCounter++;
				//alert("Total number of pages in topic are >>> " + pageCounter);
			});
			lessonCounter++;
			//alert("Total number of topics in lesson are >>> " + topicCounter);
		});
		moduleCounter++;
		//alert("Total number of lessons in module are >>> " + lessonCounter);
	});
	//alert("Total number of modules in course are >>> " + moduleCounter);
	
	//alert("Total number of pages are >>> " + totalPageCounter);
	//gTotalPages = totalPageCounter;
	return isPageAvailable;
}

function fnJumpToPageByID(lPageId,lIsHideMenu){
	//
	if($("#clsHelpBtn").hasClass("highlight")){
		removeHighlightHelp();
	}
	fnSetCurrSndObj(null);
	//transcriptText.html("");
	if(lIsHideMenu == true){
		disablePopup();
	}
	fnShowPreLoader();
	var isPageAvailable = fnCurrentPageObj(lPageId);
	if(isPageAvailable){
		//$('#jquery_jplayer_1').jPlayer("clearMedia");
		fnLoadPage(gCurrMod,gCurrLess,gCurrTop,gCurrPage,true);
	}
}

