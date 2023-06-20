//import "CheckTokenValidation.js"


var accessToken = "accessToken"

document.getElementById("submitLogin").addEventListener("click", async e =>{
    e.preventDefault()

    const response = await fetch( "session/create",{
        method: "POST",
        headers: {"Accept": "application/json", "Content-Type":"application/json"},
        body: JSON.stringify({
            login:document.getElementById("Email").value,
            password:document.getElementById("Password").value
        })
    })


    if (response.ok){
        const data = await response.JSON()
        sessionStorage.setItem(tokenKey, data.access_token)
    }

    else{
        console.log("Status: ", response.status)
    }
});