import * as tokenModule from "CheckTokenValidation.js"

function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}
function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}
function eraseCookie(name) {
    document.cookie = name + '=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}

window.onload = async function() {
    
    var accessToken = sessionStorage.getItem("AccessToken")
    var refreshToken = sessionStorage.getItem("RefreshToken")

    if(tokenModule.checkToken(accessToken)== false) {
        var updatedTokens = tokenModule.refreshTokens(refreshToken,accessToken)

        sessionStorage.setItem("AccessToken", updatedTokens.AccessToken)
        sessionStorage.setItem("RefreshToken", updatedTokens.RefreshToken)
    }

    await fetch("../user/get", {
        method: "GET",
        headers: {"Accept": "application/json", "Content-Type": "application/json", "Authorization": sessionStorage.getItem("AccessToken")},
    })
}

document.getElementById("courseButton").onclick = window.location.pathname