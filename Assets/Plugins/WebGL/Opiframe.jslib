var Opiframe = {

  GetDocumentTitle : function() {
  	var length = lengthBytesUTF8(document.title) + 1;
	var buffer = _malloc(length);
	stringToUTF8(document.title, buffer, length);
    console.log("JAU GetDocumentTitle");
	return buffer;
    },

  PostMuteMessage : function() {
      //document.getElementById("webgl_iframe").contentWindow.postMessage({action: "mute"},"https://connect.unity3dusercontent.com");
      //window.postMessage({action: "mute"}, "https://connect.unity3dusercontent.com");
      window.postMessage({action: "mute"}, "https://connect.unity.com");
      console.log("JAU PostMuteMessage");
    },

  UnmuteJS : function() {
    window.fakeDestinations.forEach(function(state) {state.gain.value = 1});
	window.parent.postMessage({action: "mute", value: 0}, "https://connect.unity.com");
    console.log("JAU Unmute");
  }

};

mergeInto(LibraryManager.library, Opiframe);
