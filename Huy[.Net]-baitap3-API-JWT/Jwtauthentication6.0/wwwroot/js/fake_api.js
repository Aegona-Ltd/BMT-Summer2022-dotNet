function loginAccount(formLogin, event) {


    document.getElementById('validation-email').innerText = "";
    document.getElementById('validation-password').innerText = "";
    const validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

    let check = false

    if (formLogin.email.value.length == 0) {
        document.getElementById('validation-email').innerText = "Email is not null";
        document.getElementById('email').focus();
        check = true
    } else if (!formLogin.email.value.match(validRegex)) {
        document.getElementById('validation-email').innerText = "Invalid email";
        document.getElementById('email').focus();
        check = true
    }
    if (formLogin.password.value.length == 0) {
        document.getElementById('validation-password').innerText = "Password is not null";
        document.getElementById('password').focus();
        check = true
    }
    //if (check == false) {
    //    document.getElementById("loginForm").submit();
    //}
    return (check) ? "" : authenticate()
}



function authenticate() {
    var email = $('#email').val();
    var password = $('#password').val();
    $.ajax({
        type: "POST",
        url: "https://localhost:7107/api/user/token",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify({
            "email": email,
            "password": password
        }),
        success: function (result) {
            console.log(result.result.token);
            console.log(result.cookie);
            var token = result.result.token;
            CheckToken(token);
        },
        error: function (xhr) {
            console.log('that bai');
            console.log(xhr.status);

        }
    })
    function CheckToken(token) {
        $.ajax({
            type: "GET",
            url: "https://localhost:7107/api/user/check/" + token,
            headers: {
                Authorization: 'Bearer ' + token,
            },
            success: function (xhr) {
                if (xhr.status == '200') {
                    window.location.href = 'https://localhost:7107/account/index'
                } 
                
            },
            error: function (xhr) {
                if (xhr.status == '401') {
                    window.location.href = 'https://localhost:7107/account/login'
                }
            }
        });
    }
};