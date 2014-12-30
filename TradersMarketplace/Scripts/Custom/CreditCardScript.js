$("body").on("click", "#btnAddCreditCard", function (e) {
    if ($("#creditCardsList table tbody tr").length <= 0) {
        $("#creditCardsList table thead").append("<tr><th>Card Holder Name</th><th>Credit Card Number</th><th>Credit Card</th><th>Expiry Date</th><th></th></tr>");
    }

    $("#creditCardsList table tbody").append("<tr><td class='cardHolderName'>" + $("#cardHolderName").val()
                                        + "</td><td class='creditCardNumber'>" + $("#creditCardNumber").val()
                                        + "</td><td class='creditCardType'>" + $("#creditCardType option:selected").text()
                                        + "<input type='hidden' value='" + $("#creditCardType").val() + "'>"
                                        + "</td><td class='expiryDate'>" + $("#expiryDateMonth").val() + "/" + $("#expiryDateYear").val()
                                        + "</td><td><a href='#' class='remove-table-item'>X</a></td></tr>");

    $("#addCreditCardContainer").empty();
});

$("#addNewCreditCard").click(function () {
    $.ajax({
        type: "GET",
        url: '../../UserManagement/CreateCreditCard',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            $("#addCreditCardContainer").html(data);
        },
        error: function (xhr) {
            var error = JSON.parse(xhr.responseText).message;
            alert(error);
        }
    });
});