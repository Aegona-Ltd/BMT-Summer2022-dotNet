function contactForm(formContact) {
    document.getElementById('validation-fullname').innerText = "";
    document.getElementById('validation-email').innerText = "";
    document.getElementById('validation-phone').innerText = "";
    document.getElementById('validation-subject').innerText = "";
    document.getElementById('validation-mes').innerText = "";

    const validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

    let check = false
    if (formContact.fullname.value.length==0) {
        document.getElementById('validation-fullname').innerText = "Fullname is not null";
        document.getElementById('fullname').focus();
        check = true
    }
    if (formContact.email.value.length==0) {
        document.getElementById('validation-email').innerText = "Email is not null";
        document.getElementById('email').focus();
        check = true
    }else if (!formContact.email.value.match(validRegex)) {
        document.getElementById('validation-email').innerText = "Invalid email";
        document.getElementById('email').focus();
        check = true
    }
    if (formContact.phone.value.length==0) {
        document.getElementById('validation-phone').innerText = "Phone is not null";
        document.getElementById('password').focus();
        check = true
    }
    if (formContact.subject.value.length==0) {
        document.getElementById('validation-subject').innerText = "Subject is not null";
        document.getElementById('subject').focus();
        check = true
    }
    if (formContact.mes.value.length==0) {
        document.getElementById('validation-mes').innerText = "Message is not null";
        document.getElementById('mes').focus();
        check = true
    }
    return (check) ? "" : authenticateContact()
}
function authenticateContact() {
    var fullname = $('#fullname').val();
    var email = $('#email').val();
    var phone = $('#phone').val();
    var subject = $('#subject').val();
    var mes = $('#mes').val();

$.ajax({
type: "POST",
//the url where you want to sent the userName and password to
url: "https://private-3701d-loginformapi.apiary-mock.com/accounts",
//json object to sent to the authentication url
dataType: 'json',
//data: '{"email": "' + email + '", "password" : "' + password + '"}',
data: {
        'fullname': fullname, 
        'email': email, 
        'phone': phone,
        'subject': subject, 
        'mes': mes, 
    },
success: function (data) {
            if(data.success){
                alert("Thanh cong")
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