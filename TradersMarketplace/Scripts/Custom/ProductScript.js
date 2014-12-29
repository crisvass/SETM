$("#btnAddToCart").click(function () {
    var params = {
        id: $('#productId').val(),
        qty: $('#qtyRequired').val()
    };

    $.ajax({
        type: "POST",
        data: JSON.stringify(params),
        url: '../../Product/Details',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            updateCartItems();
        },
        error: function (xhr) {
            if (xhr.responseText != "") {
                var error = JSON.parse(xhr.responseText).message;
                alert(error);
            }
            else {
                alert("An error occurred. Please contact administrator for help.");
            }
        }
    });
});

function updateCartItems() {
    $.ajax({
        type: "GET",
        url: '../../Product/UpdateCartTotal',
        dataType: "html",
        success: function (data) {
            if (data != null) {
                $("#noOfCartItems").html(data.trim());
            }
        },
        error: function (xhr) {
            var error = JSON.parse(xhr.responseText).message;
            alert(error);
        }
    });
};