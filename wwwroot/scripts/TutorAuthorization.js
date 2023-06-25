//import "CheckTokenValidation.js"



var accessToken = "accessToken"
const Form = document.getElementById("Form")

async function sendDataToServer(data){
    //e.preventDefault()

    return await fetch( "../session/create",{
        method: "POST",
        headers: {"Accept": "application/json", "Content-Type":"application/json"},
        body: JSON.stringify({
            login:data[0].value,
            password:data[1].value
        })
    })

    if (response.status == 404)
    alert("404")

    if (response.ok){
        const data = await response.json()
        sessionStorage.setItem("accessToken", data.access_token)
    }
    else{
        console.log("Status: ", response.status)
    }


};

Form.onsubmit = async (e) => {
    e.preventDefault()
    var a = await sendDataToServer(Form)
    var text = await a.json()
    console.log(text)
  }