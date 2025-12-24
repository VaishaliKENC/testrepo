		$('document').ready(function(){
			var audioTag = '<audio id="aud1" preload="auto" ><source src="'+audioPath+'" type="audio/mpeg"></audio>'
			$("body").append(audioTag);
			aud1 = document.getElementById("aud1");
			document.getElementById("aud1").addEventListener('timeupdate', UpdateTheTime, false);
			//
			
			var butTag = '<div style="float: right"><button id="play" onclick="PlayNow();">Play</button><div>';
			$("body").append(butTag);
			
		})
		function hideDiv(str, showStr){
			var tempArray = [];
			if(str.indexOf(",")>-1){
				tempArray = str.split(",");
			}else{
				tempArray[0]= str
			}
			for(var i=0; i<tempArray.length; i++){
				$(tempArray[i]).animate({opacity:0},200,function(){
					$(tempArray[i]).css("visibility","hidden");
					if(showStr != "" && showStr != undefined){
						showDiv(showStr);
					}					
				});
			}
		}
		function showDiv(str){
			var tempArray = [];
			if(str.indexOf(",")>-1){
				tempArray = str.split(",");
			}else{
				tempArray[0]= str
			}
			for(var i=0; i<tempArray.length; i++){
				$(tempArray[i]).css("opacity","0");
				$(tempArray[i]).css("visibility","visible")
				$(tempArray[i]).animate({opacity:1},200)
			}
		}		
		function PlayNow() {
			if(aud1.ended){
				currCue = 0;
			}
			if (aud1.paused) {
				aud1.play();
			}else if(aud1.played){
				console.log("aud1.currentTime "+aud1.currentTime)
				aud1.pause();
			}
	    }
		function UpdateTheTime(event){
			//console.log("aud1.currentTime "+aud1.currentTime)
			if(currCue>= updateContentArray.length){
				return;
			}
			if(aud1.currentTime > (Number(updateContentArray[currCue].time))){
				
				if(updateContentArray[currCue].transcript != "" && updateContentArray[currCue].transcript!= undefined){
					showTranscript(updateContentArray[currCue].transcript);
				}
				if(updateContentArray[currCue].hide != "" && updateContentArray[currCue].hide!= undefined){
					hideDiv(updateContentArray[currCue].hide, updateContentArray[currCue].show);
				}else if(updateContentArray[currCue].show != "" && updateContentArray[currCue].show!= undefined){
					showDiv(updateContentArray[currCue].show);
				}
				currCue++;
			}
			$('#audTime').html(aud1.currentTime)
		}
		function showTranscript(str){
			console.log("currCue "+currCue+" "+str);
		}