$(document).ready(function () {
/*    console.log("token in cookie: " + getCookie("token"));*/
    LoadData();
    
});
function RefreshToken() {
    $.ajax({
        type: "POST",
        url: "http://localhost:5124/api/user/refresh-token",
        dataType: "json",
        success: function () {
            console.log(getCookie("token"));
        },
        error: function (xhr, exception) {
            console.log(xhr.status, exception);
        },
    })
}
function LogOut() {
    $.ajax({
        type: "POST",
        url: "http://localhost:5124/api/user/revoke-token",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify({
            "token": getCookie("refreshToken")
        }),
        success: function () {
            window.location.href = "http://localhost:5124/account/login"
        },
        error: function (xhr, exception) {
            console.log("rtoken: " + getCookie("refreshToken"))
            console.log("login that bai");
        },
    })
}
function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
var pageNum = 1;
function LoadData(pageNum = 1) {
    $.ajax({
        type: "GET",
        url: "http://localhost:5124/contactapi/findall/" + pageNum,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        headers: {
            Authorization: 'Bearer ' + getCookie("token"),
        },
        success: function (response) {
            console.log("token in cookie: " + getCookie("token"));
            //Read data from contact List
            var result = '';
            var data = response.data;
            $.each(data, function (i, item) {
                result += `<tr>
                        <td>${item.id}</td>
                        <td>${item.fullName}</td>
                        <td>${item.email}</td>
                        <td>${item.phone}</td>
                        <td>${item.subject}</td>
                        <td>${item.daySend}</td>
                        <td><button type="button" data-bs-toggle="modal" data-bs-target="#viewct" onclick="viewContact(${item.id})" class="btn btn-success">View</button>  | <button type="button" onclick="deleteContact(${item.id})" class="btn btn-danger">Delete</button></td>
                        </tr>`
            });
            $('#tblData').html(result);
            //Pagination Contact List
            var prevDisabled = (pageNum == 1) ? "disabled" : "";
            var nextDisabled = (pageNum == response.totalPage) ? "disabled" : "";
            let pagin = '';
            pagin += '<li class="page-item ' + prevDisabled + ' ">' +
                '<button type="button" class="page-link" onclick="LoadData(' + ((pageNum == 1) ? 1 : pageNum - 1) + ')" >' + 'Previous' + '</button>' +
                '</li>'
            for (let i = 1; i <= response.totalPage; i++) {
                var active = (pageNum == i) ? "active" : "";
                pagin += '<li class="page-item">' +
                    '<button type="button" class="page-link ' + active + '" active onclick="LoadData(' + i + ')" >' + i + '</button>' +
                    '</li>'
            }
            pagin += '<li class="page-item ' + nextDisabled + ' ">' +
                ' <button type="button" class="page-link" onclick="LoadData(' + ((pageNum == response.totalPage) ? response.totalPage : pageNum + 1) + ')" >' + 'Next' + '</button>' +
                '</li>'
            $('#pagination').html(pagin);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            if (xhr.status == '401') {
                let refreshToken = getCookie("refreshToken");
                console.log(refreshToken)
                if (refreshToken != "") {
                    RefreshToken();
                    LoadData();
                } else {
                    window.location.href = "http://localhost:5124/account/login"
                }
            }
        }
    });
};

function viewContact(id) {

    $.ajax({
        url: "http://localhost:5124/contactapi/findcontact/" + id,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (response) {

            var data = response.data;
            console.log(data.id);
            const dateTime = data.daySend.split("T");
            $('#date').html(dateTime[0])
            $('#time').html(dateTime[1])
            $('#fullName').html(data.fullName)
            $('#email').html(data.email)
            $('#phone').html(data.phone)
            $('#subject').html(data.subject)
            $('#message').html(data.message)
            var modalfooter = `
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                <button type="button" data-bs-toggle="modal" data-bs-target="#viewedit"
                    onclick="viewEditContact(${data.id})" class="btn btn-primary">Edit</button>
                              `
            $('#modal-footer').html(modalfooter)
            
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status);
            console.log(thrownError);
        }
    })
}
function viewEditContact(id) {
    
    $.ajax({
        url: "http://localhost:5124/contactapi/findcontact/" + id,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (response) {
            var data = response.data;
            $('#id1').val(data.id);
            $('#fullName').val(data.fullName);
            $('#email1').val(data.email);
            $('#phone1').val(data.phone);
            $('#subject1').val(data.subject);
            $('#mes').val(data.message);
            var modalfooter = `
               <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" onclick="LoadData(pageNum)">Close</button>
               <button type="button" onclick="contactFormEdit(document.contact)" class="btn btn-primary">Edit</button>
                                `
            $('#modal-footer1').html(modalfooter)
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status);
            console.log(thrownError);
        }
    })
}
function contactFormEdit(formContact) {
    document.getElementById('validation-fullname').innerText = "";
    document.getElementById('validation-email').innerText = "";
    document.getElementById('validation-phone').innerText = "";
    document.getElementById('validation-subject').innerText = "";
    document.getElementById('validation-mes').innerText = "";
    const validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    const validRegexPhone = /^[0-9]{10}$/;
    let check = false
    if (formContact.fullName.value.length == 0) {
        document.getElementById('validation-fullname').innerText = "Fullname is not null";
        document.getElementById('fullname').focus();
        check = true
    }
    if (formContact.email1.value.length == 0) {
        document.getElementById('validation-email').innerText = "Email is not null";
        document.getElementById('email').focus();
        check = true
    } else if (!formContact.email1.value.match(validRegex)) {
        document.getElementById('validation-email').innerText = "Invalid email";
        document.getElementById('email').focus();
        check = true
    }
    if (formContact.phone1.value.length == 0) {
        document.getElementById('validation-phone').innerText = "Phone is not null";
        document.getElementById('phone').focus();
        check = true
    } else if (!formContact.phone1.value.match(validRegexPhone)) {
        document.getElementById('validation-phone').innerText = "Phone number is not in the correct format";
        document.getElementById('phone').focus();
    }
    if (formContact.subject1.value.length == 0) {
        document.getElementById('validation-subject').innerText = "Subject is not null";
        document.getElementById('subject').focus();
        check = true
    }
    if (formContact.mes.value.length == 0) {
        document.getElementById('validation-mes').innerText = "Message is not null";
        document.getElementById('mes').focus();
        check = true
    }
    var id = $("#id1").val();
    var name = $("#fullName").val();
    var email = $("#email1").val();
    var phone = $("#phone1").val();
    var subject = $("#subject1").val();
    var mes = $("#mes").val();
    return (check) ? "" : EditContact(id)
}
function EditContact(id) {
    var id = $("#id1").val();
    var name = $("#fullName").val();
    var email = $("#email1").val();
    var phone = $("#phone1").val();
    var subject = $("#subject1").val();
    var mes = $("#mes").val();
    var grecaptchaResponse = window.grecaptcha.getResponse();
    var file = $('#file')[0].files[0];
    var formData = new FormData();
    formData.append("Id", id)
    formData.append("File", file)
    formData.append("FullName", name)
    formData.append("Email", email)
    formData.append("Phone", phone)
    formData.append("Subject", subject)
    formData.append("Message", mes)
    formData.append("g-recaptcha-response", grecaptchaResponse)
    if (grecaptchaResponse.length == 0) {
        $("#g-recaptcha-error").html("<span>Vui lòng xác nhận!</span>");
    }
    else {
        jQuery("#g-recaptcha-error").empty();
        $.ajax({
            type: "PUT",
            url: "http://localhost:5124/contactapi/update/" + id,
            data: formData,
            cache: false,
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            success: function (response) {
                console.log(response.status)
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
function deleteContact(id) {

    $.ajax({
        url: "http://localhost:5124/contactapi/deleteC/" + id,
        type: "DELETE",
        //contentType: "application/json; charset=utf-8",
        //dataType: 'json',
        success: function (response) {
            if (response.status) {
                notificationme();
                LoadData(pageNum = 1);
            } else {
                alert("that bai")
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status);
            console.log(thrownError);
        }
    });
}


function ExportExcel() {
    $.ajax({
        url: "http://localhost:5124/contactapi/ExporttoExcel",
        type: 'GET',
        xhrFields: {
            responseType: 'blob'
        },
        beforeSend: function () {
            StartLoader();
        },
        success: function (response) {
            console.log("ok")
            var blob = new Blob([response], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'});
            var downLoadUrl = URL.createObjectURL(blob);
            var a = document.createElement("a");
            a.href = downLoadUrl;
            a.download = "contactlist.xlsx";
            document.body.appendChild(a);
            a.click();
        },
        complete: function () {
            EndLoader();
        }
    });
}
function ImportFile() {
    var formData = new FormData();
    var file = $('#formFile')[0].files[0];
    formData.append("formFile", file)
    $.ajax({
        url: "http://localhost:5124/contactapi/importcsv",
        type: 'POST',
        data: formData,
        cache: false,
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        beforeSend: function () {
            StartLoader1();
        },
        success: function (response) {
            console.log("ok");
            notificationme();
            document.getElementsByName('contactList')[0].reset();

        },
        complete: function () {
            EndLoader1();
        }
        //error: function (xhr, ajaxOptions, thrownError) {
        //    console.log(xhr.status);
        //    console.log(thrownError);
        //}
    });
}
function StartLoader() {
    $("#btnExcel").text('Loading...');
    $("#btnExcel").attr('disabled', true);
    
}
function StartLoader1() {
    $("#btnIFile").text('Loading...');
    $("#btnIFile").attr('disabled', true);
}
function EndLoader() {
    $("#btnExcel").text('Export to Excel');
    $("#btnExcel").attr('disabled', false);
    
}
function EndLoader1() {
   
    $("#btnIFile").text('Import File');
    $("#btnIFile").attr('disabled', false);
}
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
        "timeOut": "2000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
    toastr["success"]("Successfully")
}