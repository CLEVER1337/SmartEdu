//import "CheckTokenValidation.js"



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
        window.location.pathname ='/profile'
    }
    else{
        window.location.pathname ='/error/' + response.status
    }


};

Form.onsubmit = async (e) => {
    e.preventDefault()
    var a = await sendDataToServer(Form)
    var text = await a.json()
    console.log(text)
  }