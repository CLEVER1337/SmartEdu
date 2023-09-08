import * as tokenModule from "CheckTokenValidation.js"

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