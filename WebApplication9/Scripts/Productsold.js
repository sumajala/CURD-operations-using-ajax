
$(document).ready(function () {
});


$("#btnSubmit").on("click", function () {
    $('input[name=Age]').css('background', 'red');
});

$("#closeEditSales").on("click", function () {
    window.location.replace(document.referrer);
});

$("#sales-delete").on("click", function () {
    var id = window.location.pathname.replace('/ProductSolds/Delete/', '');
    console.log('id: ', id);
    $.ajax({
        type: 'POST',
        url: '/ProductSolds/DeleteAjax/' + id,
        data: window.location.pathname.replace('/ProductSolds/Delete/', ''),
        success: function (result) {
            if (result.status === 200) {
                alert("Delete the customer successfully!");
                window.location.href = "/ProductSolds";
            } else {
                alert(result.msg);
            }

        }
    });
});

$("#sales-create").on("click", function () {

    if (!$('input[name=ID]').val().trim() || !$('input[name=DateSold]').val().trim()) {
        alert('Please check your input before you create a new sale!');
    }

    //"ProductId, CustomerId, StoreId, DateSold, Id"
    var data = {
        'ID': $('input[name=ID]').val().trim(),
        'DateSold': $('input[name=DateSold]').val().trim(),
        'ProductID': $('#ProductID').val().trim(),
        'CustomerID': $('#CustomerID').val().trim(),
        'StoreID': $('#StoreID').val().trim(),
    }


    console.warn('data: ', data);
    $.ajax({
        type: 'POST',
        url: '/ProductSolds/CreateAjax',
        data: data,
        success: function (result) {
            console.warn('result: ', result);

            if (result.status === 200) {
                console.log(result.msg + "");
                window.location.href = '/ProductSolds';
            }
        }
    });
});

$("#sales-edit").on("click", function () {
    var data = {
        'ID': window.location.pathname.replace('/ProductSolds/Edit/', ''),
        'DateSold': $('input[name=DateSold]').val().trim(),
        'ProductID': $('#ProductID').val().trim(),
        'CustomerID': $('#CustomerID').val().trim(),
        'StoreID': $('#StoreID').val().trim(),
    };

    console.warn('data: ', data);

    $.ajax({
        type: 'POST',
        url: '/ProductSolds/EditAjax',
        data: data,
        success: function (result) {
            console.warn('result: ', result);

            if (result.status === 200) {
                alert(result.msg + "");
                window.location.href = '/ProductSolds';
            }
        }
    });
});