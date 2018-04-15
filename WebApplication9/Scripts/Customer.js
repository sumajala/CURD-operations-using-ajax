
$(document).ready(function () {
});


$("#btnSubmit").on("click", function () {
    $('input[name=Age]').css('background', 'red');
});

$("#closeEditCustomer").on("click", function () {
    //window.location.replace(document.referrer);
});

$("#customer-delete").on("click", function () {
    var id = window.location.pathname.replace('/Customers/Delete/', '');
    console.log('id: ', id);
    $.ajax({
        type: 'POST',
        url: '/Customers/DeleteAjax/' + id,
        data: window.location.pathname.replace('/Customers/Delete/', ''),
        success: function (result) {
            if (result.status === 200) {
                alert("Delete the customer successfully!");
                window.location.href = "/Customers";
            } else {
                alert(result.msg);
            }

        }
    });
});

$("#customer-create").on("click", function () {

    if (!$('input[name=ID]').val().trim() || !$('input[name=Name]').val().trim()) {
        alert('Please check your input before you create a new customer!');
    }

    var data = {
        'CustomerID': $('input[name=CustomerID]').val().trim(),
        'Name': $('input[name=Name]').val().trim(),
        'Age': $('input[name=Age]').val().trim(),
        'Address': $('input[name=Address]').val().trim(),
    }


    console.warn('data: ', data);
    $.ajax({
        type: 'POST',
        url: '/Customers/CreateAjax',
        data: data,
        success: function (result) {

            if (result.status === 200) {
                //alert(result.msg + "");
                window.location.href = '/Customers';
            }
        }
    });
});

$("#form-submit").on("click", function () {
    var data = {
        'CustomerID': $('input[name=CustomerID]').val().trim(),
        'Name': $('input[name=Name]').val().trim(),
        'Age': $('input[name=Age]').val().trim(),
        'Address': $('input[name=Address]').val().trim(),
    };

    console.warn('data: ', data);

    $.ajax({
        type: 'POST',
        url: '/Customers/EditModal',
        data: data,
        success: function (result) {
            console.warn('result: ', result);

            if (result.status === 200) {
                console.log(result.msg + "!!!");
                //window.location.href = '/Customers';
            }
        }
    });
});