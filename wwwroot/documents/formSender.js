function serializeForm(formNode) {
    const { elements } = formNode
  
    const data = Array.from(elements)
      .map((element) => {
        const { name } = element
        const value = element.value
  
        return { name, value }
      })
      .filter((item) => !!item.name)
  
    return data;
}

function onError(error) {
    alert(error.message)
  }
  
async function handleFormSubmit(event) {
    event.preventDefault()
    const data = serializeForm(event.target)

    const response = await fetch("/registration/tutor", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
        login: data[0].value,
        password: data[1].value
    })
    })
}

const applicantForm = document.getElementById('aboba')
applicantForm.addEventListener('submit', handleFormSubmit)

