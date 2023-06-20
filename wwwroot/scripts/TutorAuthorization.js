import "CheckTokenValidation.js"



var accessToken = "accessToken"
const Form = document.getElementById("Form")

async function sendDataToServer(data){
    e.preventDefault()

    const response = await fetch( "вставить адрес",{
        method: "POST",
        headers: {"Accept": "application/json", "Content-Type":"application/json"},
        body: JSON.stringify({
            login:data[0],
            password:data[1]
        })
    })


    if (response.ok){
        const data = await response.JSON()
        sessionStorage.setItem(tokenKey, data.access_token)
    }

    else{
        console.log("Status: ", response.status)
    }


};

Form.onsubmit = async (e) => {
    e.preventDefault()
    sendDataToServer(Form)
  }