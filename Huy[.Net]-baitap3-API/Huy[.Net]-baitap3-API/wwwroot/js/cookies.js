
function CheckToken(token) {
    $.ajax({
        type: "GET",
        url: "http://localhost:5124/api/user/check/" + token,
        headers: {
            Authorization: 'Bearer ' + token,
        },
        success: function (xhr) {
            if (xhr.status == '200') {
                window.location.href = "http://localhost:5124/contact/contactlist";
            }
        },
        error: function (xhr) {
            if (xhr.status == '401') {
                console.log(token)
                console.log(xhr.status);
                /*window.location.href = 'http://localhost:5124/account/login'*/
            }
        }
    });
   
}


