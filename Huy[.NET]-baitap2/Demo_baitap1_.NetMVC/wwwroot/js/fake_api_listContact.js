$(document).ready(function() {
$.ajax({
type: "GET",
//the url where you want to sent the userName and password to
url: "https://private-2a46e5-contactlistapi.apiary-mock.com/contacts",
//json object to sent to the authentication url
dataType: 'json',
success: function (datas) {
        console.log(datas.data);
            var result = "";
            datas.data?.forEach(item => {
                const {stt,data_time,fullname,email,phonenumber,subject} = item || [];
                result += 
                    `<tr>
                        <td>${item.stt}</td>
                        <td>${item.data_time}</td>
                        <td>${item.fullname}</td>
                        <td>${item.email}</td>
                        <td>${item.phonenumber}</td>
                        <td>${item.subject}</td>
                        <td><button type="button" class="btn btn-success">View</button> | <button type="button" class="btn btn-danger">Delete</button></td>
                    </tr>`
                ;
            });
            $('table').append(result);
        },
error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status);
            console.log(thrownError);
          }        
    })
});