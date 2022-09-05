
$(document).ready(function () {
    LoadData();
});

function LoadData(pageNum = 1) {
    $.ajax({
        type: "GET",
        url: "http://localhost:5124/contactapi/findall/" + pageNum,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (response) {
            //console.log(response.data);
            //console.log(response.totalPage);
            //Read data from contact List
            var result = '';
            var data = response.data;
            $.each(data, function (i, item) {
                result += '<tr>' +
                    '<td>' + item.id + '</td>' +
                    '<td>' + item.fullName + '</td>' +
                    '<td>' + item.email + '</td>' +
                    '<td>' + item.phone + '</td>' +
                    '<td>' + item.subject + '</td>' +
                    '<td>' + item.daySend + '</td>' +
                    '<td>' + '<button type="button" class="btn btn-success">View</button>' + ' | ' + '<button type="button" class="btn btn-danger">Delete</button>' + '</td>'
                '</tr>';
            });
            $('#tblData').html(result);

            //Pagination Contact List
            var prevDisabled = (pageNum == 1) ? "disabled" : "";
            var nextDisabled = (pageNum == response.totalPage) ? "disabled" : "";
            let pagin = '';
            pagin += '<li class="page-item ' +prevDisabled+ ' ">' +
                '<button type="button" class="page-link" onclick="LoadData(' + ((pageNum == 1) ? 1 : pageNum-1) + ')" >' + 'Previous' + '</button>' +
                '</li>'
            for (let i = 1; i <= response.totalPage; i++) {
                var active = (pageNum == i) ? "active" : "";
                pagin += '<li class="page-item">' +
                    '<button type="button" class="page-link ' + active + '" active onclick="LoadData(' + i + ')" >' + i + '</button>' +
                          '</li>'
            }
            pagin += '<li class="page-item ' + nextDisabled + ' ">' +
                ' <button type="button" class="page-link" onclick="LoadData(' + ((pageNum == response.totalPage) ? response.totalPage : pageNum+1) + ')" >' + 'Next' +'</button>' +
                '</li>'
            $('#pagination').html(pagin);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status);
            console.log(thrownError);
        }
    });
};