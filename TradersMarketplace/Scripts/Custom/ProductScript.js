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
        dataType: "html",
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

$("#btnUpdateCart").click(function () {
    var items = [];
    valid = true;
    $("#shoppingCartTable tbody tr").each(function (index) {
        if ($(this).find("td.qty").find("input[type=number]").val() >= 0) {
            items.push({
                ProductId: $(this).prop("id"),
                ProductQty: $(this).find("td.qty").find("input[type=number]").val()
            });
        }
        else {
            valid = false;
        }
    });

    if (valid) {
        var params = { carts: JSON.stringify(items) };

        $.ajax({
            type: "POST",
            data: JSON.stringify(params),
            url: '../../Product/ShoppingCart',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (data) {
                updateCartItems();
                updateShoppingCart();
                $("#updateResult").empty();
                $("#updateResult").append("Cart updated succesfully.");
            },
            error: function (xhr) {
                if (xhr.responseText != "") {
                    var error = JSON.parse(xhr.responseText).message;
                    $("#updateResult").empty();
                    $("#updateResult").append(error);
                }
                else {
                    $("#updateResult").empty();
                    $("#updateResult").append("An error occurred. Please contact administrator for help.");
                }
            }
        });
    }
    else {
        $("#updateResult").empty();
        $("#updateResult").append("Quantities must be bigger or equal to 0.");
    }
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

function updateShoppingCart() {
    $.ajax({
        type: "GET",
        url: '../../Product/GetShoppingCartList',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            if (data != null) {
                if (data != "") {
                    $("#shoppingCartTable tbody").empty();
                    $("#shoppingCartTable tbody").html(data);
                }
                else {
                    $("#shoppingCartContainer ").html("<p>There are no items in your shopping cart.</p>");
                }
            }
        },
        error: function (xhr) {
            var error = JSON.parse(xhr.responseText).message;
            alert(error);
        }
    });

    $.ajax({
        type: "GET",
        url: '../../Product/GetShoppingCartTotals',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            if (data != null) {
                if (data != "") {
                    $("#shoppingCartTotals").html(data);
                }
            }
        },
        error: function (xhr) {
            var error = JSON.parse(xhr.responseText).message;
            alert(error);
        }
    });
};

$("#btnPlaceOrder").click(function () {
    var creditCards = [];
    if ($("#creditCardsList table tbody tr").length > 0) {
        $("#creditCardsList table tbody tr").each(function (i) {
            creditCards.push({
                Id: $(this).find("#ccd_Id").val(),
                CardHolderName: $(this).find(".cardHolderName").text().trim(),
                CreditCardNumber: $(this).find(".creditCardNumber").text().trim(),
                CreditCardTypeId: $(this).find(".creditCardType").find("input[type=hidden]").val(),
                Month: $(this).find(".expiryDate").text().substring(0, $(this).find(".expiryDate").text().indexOf("/")).trim(),
                Year: $(this).find(".expiryDate").text().substring($(this).find(".expiryDate").text().indexOf("/") + 1).trim()
            });
        });
    }

    var params = {
        email: $("#Uv_Email").val(),
        contactNumber: $("#Uv_ContactNumber").val(),
        residence: $("#Uv_Residence").val(),
        street: $("#Uv_Street").val(),
        postCode: $("#Uv_PostCode").val(),
        town: $("#Uv_Town").val(),
        country: $("#Uv_Country").val(),
        creditCards: JSON.stringify(creditCards)        
    };

    $.ajax({
        type: "POST",
        data: JSON.stringify(params),
        url: '../../Product/Checkout',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            window.location.href = data;
        },
        error: function (xhr) {
            var error = JSON.parse(xhr.responseText).message;
            alert(error);
        }
    });
});