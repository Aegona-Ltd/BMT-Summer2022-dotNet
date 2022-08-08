function submitUserForm() {
    let response = grecaptcha.getResponse();
    console.log(response.length);
    if (response.length === 0) {
        document.getElementById('g-recaptcha-error').innerHTML = '<span style="color: red ;">Chưa tích captcha</span>'
        return false;
    }
    return true;
}

function verifyCaptcha() {
    console.log('verified');
    document.getElementById('g-recaptcha-error').innerHTML = '';
}
var frm = $('#contactForm1');

frm.submit(function (e) {

    e.preventDefault();

    $.ajax({
        type: frm.attr('method'),
        url: frm.attr('action'),
        data: frm.serialize(),
        success: function (data) {
            console.log('Submission was successful.');
            console.log(data);
        },
        error: function (data) {
            console.log('An error occurred.');
            console.log(data);
        },
    });
});