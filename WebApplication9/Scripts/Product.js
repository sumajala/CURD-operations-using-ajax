
$(document).ready(function () {
});


$("#btnSubmit").on("click", function () {
    $('input[name=Age]').css('background', 'red');
});

$("#closeEditProduct").on("click", function () {
    //window.location.replace(document.referrer);
});

$("#Product-delete").on("click", function () {
    var id = window.location.pathname.replace('/Products/Delete/', '');
    console.log('id: ', id);
    $.ajax({
        type: 'POST',
        url: '/products/DeleteAjax/' + id,
        data: window.location.pathname.replace('/Products/Delete/', ''),
        success: function (result) {
            if (result.status === 200) {
                alert("Delete the Product successfully!");
                window.location.href = "/Products";
            } else {
                alert(result.msg);
            }

        }
    });
});

$("#Product-create").on("click", function () {

    if (!$('input[name=ProductID]').val().trim() || !$('input[name=Name]').val().trim()) {
        alert('Please check your input before you create a new Product!');
    }

    var data = {
        'ProductID': $('input[name=ProductID]').val().trim(),
        'Name': $('input[name=Name]').val().trim(),
        'Price': $('input[name=Price]').val().trim(),
       
    }


    console.warn('data: ', data);
    $.ajax({
        type: 'POST',
        url: '/Products/CreateAjax',
        data: data,
        success: function (result) {

            if (result.status === 200) {
                //alert(result.msg + "");
                window.location.href = '/Products';
            }
        }
    });
});

$("#form-submit").on("click", function () {
    var data = {
        'ProductID': $('input[name=ProductID]').val().trim(),
        'Name': $('input[name=Name]').val().trim(),
        'Price': $('input[name=Price]').val().trim(),
       
    };

    console.warn('data: ', data);

    $.ajax({
        type: 'POST',
        url: '/Products/EditModal',
        data: data,
        success: function (result) {
            console.warn('result: ', result);

            if (result.status === 200) {
                console.log(result.msg + "!!!");
                
            }
        }
    });
});c