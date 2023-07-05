async function sendDataToServer(data){
    return await fetch("../user/student/register", {
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
    sendDataToServer(Form)
  }
  
  function validate_password() {
    var pass = document.getElementById('pass').value;
    var confirm_pass = document.getElementById('pass_confirm').value;
  
    if (pass != confirm_pass) {
        document.getElementById('wrongPassAlert').innerHTML = 'Пароли должны быть одинаковыми';
        document.getElementById('register').disabled = true;
        document.getElementById('register').style.opacity = (0.4);
    } else {
      document.getElementById('wrongPassAlert').innerHTML = 'Пароли одинаковы';
      document.getElementById('register').disabled = false;
      document.getElementById('register').style.opacity = (1);
    }
  }