function contactForm(formContact) {
    document.getElementById('validation-fullname').innerText = "";
    document.getElementById('validation-email').innerText = "";
    document.getElementById('validation-phone').innerText = "";
    document.getElementById('validation-subject').innerText = "";
    document.getElementById('validation-mes').innerText = "";
    const validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    const validRegexPhone = /^[0-9]{10}$/;
    let check = false
    if (formContact.fullname.value.length == 0) {
        document.getElementById('validation-fullname').innerText = "Fullname is not null";
        document.getElementById('fullname').focus();
        check = true
    }
    if (formContact.email.value.length == 0) {
        document.getElementById('validation-email').innerText = "Email is not null";
        document.getElementById('email').focus();
        check = true
    } else if (!formContact.email.value.match(validRegex)) {
        document.getElementById('validation-email').innerText = "Invalid email";
        document.getElementById('email').focus();
        check = true
    }
    if (formContact.phone.value.length == 0) {
        document.getElementById('validation-phone').innerText = "Phone is not null";
        document.getElementById('phone').focus();
        check = true
    } else if (!formContact.phone.value.match(validRegexPhone)) {
        document.getElementById('validation-phone').innerText = "Phone number is not in the correct format";
        document.getElementById('phone').focus();
    }
    if (formContact.subject.value.length == 0) {
        document.getElementById('validation-subject').innerText = "Subject is not null";
        document.getElementById('subject').focus();
        check = true
    }
    if (formContact.mes.value.length == 0) {
        document.getElementById('validation-mes').innerText = "Message is not null";
        document.getElementById('mes').focus();
        check = true
    }
    //if (check == false) {
    //    even.preventDefault
    //    document.getElementById("contactForm").submit();
    //}
    return (check) ? "" : CreateContact()
}
function CreateContact() {
    var name = $("#fullname").val();
    var email = $("#email").val();
    var phone = $("#phone").val();
    var subject = $("#subject").val();
    var mes = $("#mes").val();
    var formData = new FormData();
    var file = $('#file')[0].files[0];
    formData.append("File", file)
    formData.append("FullName",name)
    formData.append("Email", email)
    formData.append("Phone", phone)
    formData.append("Subject", subject)
    formData.append("Message", mes)
    var grecaptcha = window.grecaptcha.getResponse();
    formData.append($('g-recaptcha-response'), grecaptcha)
    if (grecaptcha.length == 0) {
        $("#g-recaptcha-error").html("<span>Vui lòng xác nhận!</span>");
    }
    else {
        jQuery("#g-recaptcha-error").empty();
        $.ajax({
            type: "POST",
            url: "http://localhost:5124/contactapi/create",
            data: formData,
            cache: false,
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            success: function (response) {
                if (response.status) {
                    notificationme();
                    document.getElementsByName('contact')[0].reset();
                    window.grecaptcha.reset();
                } else {
                    alert("that bai")
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log("that bai");
                console.log(xhr.status);
                console.log(thrownError);
            }
        })
    }
};


function notificationme() {
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
    toastr["success"]("Sent successfully")
}