// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.getElementById("sendButton").disabled = true;
(function () {
    if (isStarted) {
        document.getElementById("messageInput").addEventListener("keyup", dActiveButton);
    }
})();

function dActiveButton() {
    var val = document.getElementById("messageInput").value;
    if (val.length != 0) {
        document.getElementById("sendButton").disabled = false;
    }
    else {
        document.getElementById("sendButton").disabled = true;
    }
}
document.getElementById("sendButton").addEventListener("click", function () {
    textInput.value = "";
    dActiveButton();
    document.getElementById("sendButton").blur();
})
