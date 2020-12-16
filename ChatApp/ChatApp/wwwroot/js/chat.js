"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;
var scroll = document.getElementById("scroll");
var textInput = document.getElementById("messageInput");

var audio = document.getElementById("sendAudio");
var isStarted = true;

connection.on("ReceiveMessage", function (user, message) {
    var isCurrentUserMessage = userName === user;
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var li = document.createElement("li");
    var div = document.createElement("div");
    var div2 = document.createElement("div");
    var h5 = document.createElement("h5");
    var p = document.createElement("p");
    //Odtworzenie dzwięku przychodzenia wiadomości//
    audio.play();
    //Dodanie odpowiednich składowych pod li
    div.appendChild(div2);
    div2.appendChild(h5);
    div2.appendChild(p);
    li.appendChild(div);
    //dodanie atrybutów w składowych htmla
    div.setAttribute("class", "chat-body");
    div2.setAttribute("class", "chat-message");
    li.className = isCurrentUserMessage ? "out" : "in";
    //dodanie kontentu do składowych
    p.textContent = msg;
    h5.textContent = user;
    //Dodanie składowego li do elementu messagesList
    document.getElementById("messagesList").appendChild(li);
    //Przeskrolowanie na sam dół i reset tekstu
    scroll.scrollTop = scroll.scrollHeight;
    
});

connection.start().then(function () {
    isStarted = true;
    //document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});
function ClearInputMessage() {
    textInput.value = "";
}
document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var messag = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, messag).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});