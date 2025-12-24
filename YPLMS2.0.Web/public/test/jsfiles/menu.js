// JavaScript Document
/*********** Player level constants and attributes are initialized here ********************/

function fnCreateMenu()
{
	var menuStr = "<ul>";
	for(var i=0;i<gModuleArr.length;i++)
	{
		if(fCheckCompletion(gModuleArr[i],"Module"))
		{
		//menuStr += '<li><a id="visited" class="toggle-link-main" href="javascript:void(0);"><span>'+gModuleArr[i].find('MODULETITLE').text() + '</span></a><ul class="sliding-section">'
		}else{
		//menuStr += '<li><a class="toggle-link-main" href="javascript:void(0);"><span>'+gModuleArr[i].find('MODULETITLE').text() + '</span></a><ul class="sliding-section">'
		}
		for(var j=0;j<gModuleArr[i].Lessons.length;j++)
		{
			if(fCheckCompletion(gModuleArr[i].Lessons[j],"Lesson"))
			{
				menuStr += '<li><a id = "visited" class="toggle-link-sub LessonGradient" href="javascript:void(0);"><span><div></div>'+gModuleArr[i].Lessons[j].find("LESSONTITLE").text()+'<div class="menu_close"></div></span></a><ul class="toggle-list">'
			}else{
				menuStr += '<li><a class="toggle-link-sub LessonGradient" href="javascript:void(0);"><span><div></div>'+gModuleArr[i].Lessons[j].find("LESSONTITLE").text()+'<div class="menu_close"></div></span></a><ul class="toggle-list">'
			}
			for(var k=0;k<gModuleArr[i].Lessons[j].Topics.length;k++)
			{
				if(fCheckCompletion(gModuleArr[i].Lessons[j].Topics[k],"Topic")){
				//menuStr += '<li><a id="visited" class="child-toggle-link-sub" href="#">'+gModuleArr[i].Lessons[j].Topics[k].find('TOPICTITLE').text()+'</a><ul>'
				}else{
				//menuStr += '<li><a class="child-toggle-link-sub" href="#">'+gModuleArr[i].Lessons[j].Topics[k].find('TOPICTITLE').text()+'</a><ul>'
				}
				for(var l=0;l<gModuleArr[i].Lessons[j].Topics[k].Pages.length;l++)
				{
					//alert(l + " Page ==  topic " + k + " == gCurrLess " + j + " == module " + i)
					var lPgId = gModuleArr[i].Lessons[j].Topics[k].Pages[l].attr("ID");
					
					if( gModuleArr[i].Lessons[j].Topics[k].Pages[l].attr("SHOWINMENU") == 'N' ) {
						continue;
					}
					
					if(gCurrPage == l && gCurrTop == k && gCurrLess == j &&	gCurrMod == i)
					{
						menuStr += '<li id="selected"><div> <span id="completion" class="current"></span></div><a href="#">'+gModuleArr[i].Lessons[j].Topics[k].Pages[l].find('PAGETITLE').text()+'</a></li>'
					} else {
						//alert(lPgId)
						if(fCheckCompletion(gModuleArr[i].Lessons[j].Topics[k].Pages[l],"Page")){
								menuStr += '<li class="pageSelect" onclick="javascript:fnJumpToPageByID(\''+lPgId+'\',true);playerToggleAciteClass();"><div><span id="completion" class="visible"></span> </div><a id="visited" style="cursor:pointer">'+gModuleArr[i].Lessons[j].Topics[k].Pages[l].find('PAGETITLE').text()+'</a></li>'
						}else{
							menuStr += '<li class="pageSelect" onclick="javascript:fnJumpToPageByID(\''+lPgId+'\',true);playerToggleAciteClass();"><div><span id="completion"></span></div><a style="cursor:pointer">'+gModuleArr[i].Lessons[j].Topics[k].Pages[l].find('PAGETITLE').text()+'</a></li>'
						}
					}
				}
				//menuStr += '</ul></li>'
			}
			menuStr += '</ul></li>';
		}
	}
	menuStr += '</ul>';
	$("#accordion-menu-wrapper").html(menuStr);
	$("#accordion-menu-wrapper").hide();
	fnAssignMenuEvents();
}


function fnAssignMenuEvents()
	{
	jQuery("div#accordion-menu-wrapper").ready(function(){
					var selected = jQuery("#selected");
					var parentTL = jQuery(selected).parents("li:first");
					//alert(selected.text())
					if(parentTL!=null){
						var innerUL = jQuery(parentTL).children("ul:first");
						if(innerUL.attr("class")!=null){
							jQuery(parentTL).children("ul:first").slideToggle(100);
							jQuery(parentTL).next("ul:visible").slideUp();
							jQuery(parentTL).children("a:first").toggleClass("active-toggle-link-sub");

							parentTL = jQuery(parentTL).parents("li:first")
							jQuery(parentTL).children("ul:first").slideToggle(100);
							jQuery(parentTL).children("a:first").toggleClass("active-child-toggle-link-sub");			
						} else {
							var mainPar = jQuery(parentTL).parents("li:first").parents("li:first");
							if(mainPar!=null){
								var mainInnerUL = jQuery(mainPar).children("ul:first");
								if(mainInnerUL.attr("class")!=null){
									jQuery(mainPar).children("ul:first").slideToggle(100);
									jQuery(mainPar).next("ul:visible").slideUp();
									jQuery(mainPar).children("a:first").toggleClass("active-toggle-link-main");
								} 
							}
							var mainPar = jQuery(parentTL).parents("li:first");
							if(mainPar!=null){
								var mainInnerUL = jQuery(mainPar).children("ul:first");
								if(mainInnerUL.attr("class")!=null){
									jQuery(mainPar).children("ul:first").slideToggle(100);
									jQuery(mainPar).next("ul:visible").slideUp();
									jQuery(mainPar).children("a:first").toggleClass("active-toggle-link-sub");
								} 
							}
							parentTL = jQuery(selected).parents("li:first");
							jQuery(parentTL).children("a:first").toggleClass("active-child-toggle-link-sub");			
						}
					}
				});
				
		 $("#popupClose").click(function(){  
			disablePopup();  
	   }); 
	   
		$("div#accordion-menu-wrapper a.main-link").click(function(){
				$("a.toggle-link-main").next("ul").slideUp();
				$("a.toggle-link-main").removeClass("active-toggle-link-main");
			});
			//==============================================//
			//		Function for Outer [ main ] slider 		//
			//==============================================//
			$("div#accordion-menu-wrapper a.toggle-link-main").click(function(){
				//$("a.toggle-link-main").next("ul:visible").slideUp();
				//$("a.toggle-link-main").removeClass("active-toggle-link-main");
				$(this).next("ul").slideToggle(300).parent().siblings().children("ul").slideUp();
				$(this).toggleClass("active-toggle-link-main").parent().siblings().children("a.toggle-link-main").removeClass("active-toggle-link-main");
				$("a.toggle-link-sub").next("ul:visible").slideUp();
				$(this).next("ul").children("li").children("a.toggle-link-sub").removeClass("active-toggle-link-sub");
			});
			//$("div#accordion-menu-wrapper a.child-toggle-link-sub").slideUp();
				//==============================================//
				//		Function for Inner slider 		 		//
				//==============================================//
				$("div#accordion-menu-wrapper a.child-toggle-link-sub").click(function(){
				//$(this).next("ul").slideDown();
					$(this).next("ul").slideToggle(300);
					$(this).toggleClass("active-child-toggle-link-sub");
				});

				$("div#accordion-menu-wrapper a.toggle-link-sub").click(function(event){
					
					if(!$(this).hasClass("active-toggle-link-sub")){
						$("div#accordion-menu-wrapper .active-toggle-link-sub").next("ul").slideUp();
						$("div#accordion-menu-wrapper .active-toggle-link-sub").toggleClass("active-toggle-link-sub");						
						$(this).next("ul").slideToggle(300);
						$(this).toggleClass("active-toggle-link-sub");
					}else{
						disablePopup();
					}
					event.stopPropagation();
				});
		
				$("#accordion-menu-wrapper").show();
	}