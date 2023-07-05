// function serializeForm(formNode) {
//     const { elements } = formNode
  
//     const data = Array.from(elements)
//       .map((element) => {
//         const { name } = element
//         const value = element.value
  
//         return { name, value }
//       })
//       .filter((item) => !!item.name)
  
//     console.log(data)
// }


// async function sendData(data) {
//     return await fetch('https://localhost:228/registration/tutor', {
//       method: 'POST',
//       headers: { 'Content-Type': 'multipart/form-data' },
//       body: data
//     })
// }


// async function handleFormSubmit(event) {
//     event.preventDefault()
//     const data = serializeForm(event.target)
  
  
//     const { status, error } = await sendData(data)
  
//     if (status === 200) {
//       onSuccess(event.target)
//     } else {
//       onError(error)
//     }
// }




async function sendDataToServer(data){
  return await fetch("../user/tutor/register", {
    method: "POST",
    headers: {"Accept": "application/json", "Content-Type": "application/json"},
    body: JSON.stringify({
      login: data[0].value,
      password: data[1].value
    })
  })

}


const Form = document.getElementById('Form')


Form.onsubmit = async (e) => {
  e.preventDefault()
    var a = sendDataToServer(Form)
  //console.log((await a).json())
}

// Form.addEventListener('submit', handleFormSubmit)

