//import "CheckTokenValidation.js"

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

var accessToken = "accessToken"
const Form = document.getElementById("Form")

async function sendDataToServer(data){
    //e.preventDefault()

    var response = await fetch( "../session/create",{
        method: "POST",
        headers: {"Accept": "application/json", "Content-Type":"application/json"},
        body: JSON.stringify({
            login:data[0].value,
            password:data[1].value
        })
    })

    if (response.ok){
        const data = await response.json()
        sessionStorage.setItem("AccessToken", data.AccessToken)
        sessionStorage.setItem("RefreshToken", data.RefreshToken)

        setCookie("jwtBearer", data.AccessToken, 1);
        
        window.location.pathname ='/profile'
    }
    else{
        window.location.pathname ='/error/' + response.status
    }


};

Form.onsubmit = async (e) => {
    e.preventDefault()
    await sendDataToServer(Form)
  }