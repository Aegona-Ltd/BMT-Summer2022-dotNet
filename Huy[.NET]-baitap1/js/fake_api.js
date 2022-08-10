// var loginData = function(){
//     var xhttp = new XMLHttpRequest();
//     xhttp.onreadystatechange = function(){
//         if(this.readyState == 4 && this.status == 200)
//         addLoginData(this.response)
//     }
//     xhttp.open('GET','https://private-3701d-loginformapi.apiary-mock.com/accounts',true);
//     xhttp.send();
// }
// var addLoginData = function(){

// }
// var email = document.getElementById('email').val();
// var password = document.getElementById('password').val();
// $('#loginForm').submit(function(event){
//     event.preventDefault();
//     $.ajax({
//         type:"GET",
//         dataType: "json",
//         url:"https://private-3701d-loginformapi.apiary-mock.com/accounts",
//         data:{
//             'email': email,
//             'password': password
//         },
//         success: function(result){
//             if(result > 1){
//                 window.location.href = 'dashboard.html'
//             }else
//             {
//                 $('#result').empty().addClass('error')
//                     .append('Something is wrong.');
//             }
//         }
//     })
//     return false
// })
    $("#btnLogin").click(function (e) {
        e.preventDefault();
        //collect userName and password entered by users
        var email = $('#email').val();
        var password = $('#password').val();
        authenticate(email,password);

    });

function authenticate(email, password) {
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