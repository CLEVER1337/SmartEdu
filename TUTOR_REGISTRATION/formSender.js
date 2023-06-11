function serializeForm(formNode) {
    const { elements } = formNode
  
    const data = Array.from(elements)
      .map((element) => {
        const { name } = element
        const value = element.value
  
        return { name, value }
      })
      .filter((item) => !!item.name)
  
    console.log(data)
}

function onSuccess(formNode) {
    alert('Ваша заявка отправлена!')
    formNode.classList.toggle('hidden')
  }

async function sendData(data) {
    return await fetch('https://localhost:228/registration/tutor', {
      method: 'POST',
      headers: { 'Content-Type': 'multipart/form-data' },
      body: data,
    })
}

function onError(error) {
    alert(error.message)
  }

function toggleLoader() {
    const loader = document.getElementById('loader')
    loader.classList.toggle('hidden')
  }
  
async function handleFormSubmit(event) {
    event.preventDefault()
    const data = serializeForm(event.target)
  
    toggleLoader()
  
    const { status, error } = await sendData(data)
    toggleLoader()
  
    if (status === 200) {
      onSuccess(event.target)
    } else {
      onError(error)
    }
}



const applicantForm = document.getElementById('aboba')
applicantForm.addEventListener('submit', handleFormSubmit)

