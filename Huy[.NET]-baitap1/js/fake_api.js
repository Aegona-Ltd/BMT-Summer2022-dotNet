
    function loginAccount(formLogin) {
        document.getElementById('validation-email').innerText = "";
        document.getElementById('validation-password').innerText = "";
        const validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    
        let check = false
        if (formLogin.email.value.length==0) {
            document.getElementById('validation-email').innerText = "Email is not null";
            document.getElementById('email').focus();
            check = true
        }else if (!formLogin.email.value.match(validRegex)) {
            document.getElementById('validation-email').innerText = "Invalid email";
            document.getElementById('email').focus();
            check = true
        }
        if (formLogin.password.value.length==0) {
            document.getElementById('validation-password').innerText = "Password is not null";
            document.getElementById('password').focus();
            check = true
        }
        return (check) ? "":authenticate()
    }


function authenticate() {
    var email = $('#email').val();
    var password = $('#password').val();
$.ajax({
type: "POST",
//the url where you want to sent the userName and password to
url: "https://private-3701d-loginformapi.apiary-mock.com/accounts",
//json object to sent to the authentication url
dataType: 'json',
//data: '{"email": "' + email + '", "password" : "' + password + '"}',
data: {
        'email': email, 
        'password': password
    },
success: function (data) {
            if(data.success){
                console.log(data);
                window.location.href = '../html/dashboard.html';
            }else{
                console.log('that bai')
            }         
        },
error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status);
            console.log(thrownError);
          }        
    })
};