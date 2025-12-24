/**
 * jQuery dragdrop(htm5) plugin
 * @name jquery.dragdrop.js
 * @author bhabani padhy - http://www.indecomm.net/
 * @version 0.1
 * @date May 22, 2012
 * @category jQuery plugin
 */
;(function($,document,window,undefined){
		// Default Configuration 
		var settings={
			gFeedBackType:"instant",
			AllowedDrops:3,
			arrDrags:[],
			arrDrops:[],
			gDropedArr:[],
		};
		// Default Configuration overriden by the options passed by developper	
	$.fn.dragdrop=function(options){
		gConfig = $.extend(settings,options);
		var jQueryMatchedObj = this;
		if(gConfig.gFeedBackType=="instant"){
				$('#evalBtn').hide()
		}	
	$('#evalBtn').attr('disabled','disabled');	
		return this.each(function(){
			//var gConfig = $.extend(settings,options),
			//$this= $(this);
			})			
		}
// Initializing the plugin 		
	$.fn.dragdrop.fInit=function(){
			fInitDrags();
			fInitDrops();			
		}	
    function fInitDrags(){
		//alert(gConfig.gFeedBackType+"_____"+gConfig.AllowedDrops+" ___ "+gConfig.arrDrags[1].ansDropID)
		var lTop=0;
		var lLeft=0;
		var ZInd=200;
		var lMid = Math.floor(gConfig.arrDrags.length/2)
		//alert(gConfig.arrDrags.length%2)
		$.each(gConfig.arrDrags,function(i){
			$('#'+gConfig.arrDrags[i].id).bind('dragstart',function(event){
					event.originalEvent.dataTransfer.setData("text/plain",event.target.getAttribute('id')+"##"+gConfig.arrDrags[i].ansDropID);	
				})
			$('#'+gConfig.arrDrags[i].id).bind('dragleave',function(event){	
				//alert('dr leave')
				})	
				$('#'+gConfig.arrDrags[i].id).parent().css({"z-index":ZInd,"top":lTop+"px","left":lLeft+'px'});
				lLeft+=110;
				if((gConfig.arrDrags.length%2)==0){
					if(i<=lMid-2){
						lTop+=30;
						ZInd+=1;
					}	
					if(i>=lMid){
						lTop-=30;
						ZInd-=4;					
					}									
				}
				else{
					if(i<lMid){
						lTop+=30;
						ZInd+=1;
					}
					if(i>=lMid){
						lTop-=30;
						ZInd-=4;					
					}										
				}
		})		
	}	
	function fInitDrops(){			
			$.each(gConfig.arrDrops,function(i){
				$('#'+gConfig.arrDrops[i].id).bind('dragover',function(event){
					event.preventDefault(event);
					return false;
				})
				$('#'+gConfig.arrDrops[i].id).bind('drop',function(event){
					if(fRestrictDrops($('#'+gConfig.arrDrops[i].id))){
						return;
					}					
					var gTransferdData = event.originalEvent.dataTransfer.getData("text/plain").split("##");					
					var item=gTransferdData[0];					
					var correctTarget=gTransferdData[1];						
					var mousey = $('#'+item);
					fCheckandAppendToArr(gConfig.gDropedArr,item);
					//mousey.parent().hide()
					//catHeading.html(mousey.attr('alt'));				
					$(this).append(mousey.parent());	
					//console.log(mousey.parent().parent().html()+" Drop node")
					mousey.attr('hspace','5');	
					mousey.parent().removeClass('clsBook');
					mousey.parent().addClass('clsBookDropped');
					
					if($(this).attr('id')==correctTarget){	
							mousey.attr('status','correct');
							if(gConfig.gFeedBackType=="instant"){
								if(mousey.siblings('img').attr('src')==undefined){
									mousey.parent().append('<img src="images/Tick_img.png"  align="top"/>')
								}
								else{
									mousey.siblings('img').attr('src','images/Tick_img.png');
									alert("b")
								}
							}
						}
					else{
							mousey.attr('status','inccorrect');
							if(gConfig.gFeedBackType=="instant"){
								   if(mousey.siblings('img').attr('src')==undefined){
										mousey.parent().append('<img src="images/wrong_img.png"  align="top"/>')
								   }else{
										mousey.siblings('img').attr('src','images/wrong_img.png');
										alert("bb")
									}
							}
						}	
					return false;												
				})					
			})		
		}
//check for cooect/incorrect moves		
function fEval(){
	$.each(gConfig.arrDrags,function(i){
			var mousey = $("#"+gConfig.arrDrags[i].id);
			if(mousey.attr('status')=='correct'){
				mousey.parent().append('<img src="images/Tick_img.png"  align="top" draggable="false" style="position:relative;display:inline;right:20px;top:0px"/>')				
			}
			if(mousey.attr('status')=='inccorrect'){
				mousey.parent().append('<img src="images/wrong_img.png"  align="top" draggable="false" style="position:relative;display:inline;right:20px;top:0px"/>')
			}
			mousey.attr('draggable','false');	
			mousey.css('cursor','none');
			mousey.parent().attr('draggable','false');
			mousey.parent().css('cursor','none');					
		})
		$('#evalBtn').attr('disabled',true);
		$('#evalBtn').removeClass('clsBtnEnable');
		$('#evalBtn').addClass('clsBtnDisable');
	}
//maximum number of drops allwoer to drop in a single target		
	function fRestrictDrops(lObj){
			if($('#evalBtn').attr('disabled')=='disabled'){
				fEnableBtn();
			}
			if((lObj.contents().length)>=gConfig.AllowedDrops){
				alert('you have exceeds the number of drops');
				return true;			
			}
		}	
	function fEnableBtn(){
		if(gConfig.gDropedArr.length>=gConfig.arrDrags.length-1){
			$('#evalBtn').removeAttr('disabled');
			$('#evalBtn').removeClass('clsBtnDisable');
			$('#evalBtn').addClass('clsBtnEnable');
			$('#evalBtn').bind('click',function(){
					fEval();					
				})			
		}
	}
	function fCheckandAppendToArr(lArr,lIdentifier)	{
		var exists=false
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
						
	})(jQuery,document,window)
	

	


