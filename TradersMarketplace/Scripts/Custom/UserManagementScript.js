$("body").on("click", "#btnAddAllUserRoles", function () {
    $("#UserRoles option").each(function (i) {
        $("#UserRolesSelected").append($(this));
    });
    $("#UserRoles option").remove();
    removeSelections();
});

$("body").on("click", "#btnAddUserRole", function () {
    $("#UserRolesSelected").append($("#UserRoles option:selected"));
    $("#UserRoles option:selected").remove();
    removeSelections();
});

$("body").on("click", "#btnRemoveUserRole", function () {
    $("#UserRoles").append($("#UserRolesSelected option:selected"));
    $("#UserRolesSelected option:selected").remove();
    $("#UserRoles option:selected").removeAttr("selected");
});

$("body").on("click", "#btnRemoveAllUserRoles", function () {
    $("#UserRolesSelected option").each(function (i) {
        $("#UserRoles").append($(this));
        $("#UserRoles option:selected").removeAttr("selected");
    });
    $("#UserRolesSelected option").remove();
});

function removeSelections() {
    $("#UserRoles option:selected").removeAttr("selected");
    $("#UserRolesSelected option:selected").removeAttr("selected");
}

$("body").on("click", "#btnCreateUser", function () {
    var userRoles = [];
    if ($("#UserRolesSelected option").length > 0) {
        $("#UserRolesSelected option").each(function (i) {
            userRoles.push({
                RoleId: $(this).val(),
                RoleName: $(this).text()
            });
        });
    }

    var creditCards = [];
    if ($("#creditCardsList table tbody tr").length > 0) {
        $("#creditCardsList table tbody tr").each(function (i) {
            creditCards.push({
                CardHolderName: $(this).find(".cardHolderName").text(),
                CreditCardNumber: $(this).find(".creditCardNumber").text(),
                CreditCardTypeId: $(this).find(".creditCardType").find("input[type=hidden]").val(),
                Month: $(this).find(".expiryDate").text().substring(0, $(this).find(".expiryDate").text().indexOf("/")),
                Year: $(this).find(".expiryDate").text().substring($(this).find(".expiryDate").text().indexOf("/") + 1)
            });
        });
    }

    var params = {
        username: $("#Username").val(),
        password: $("#Password").val(),
        email: $("#Email").val(),
        firstName: $("#FirstName").val(),
        lastName: $("#LastName").val(),
        contactNumber: $("#ContactNumber").val(),
        residence: $("#Residence").val(),
        street: $("#Street").val(),
        postCode: $("#PostCode").val(),
        town: $("#Town").val(),
        country: $("#Country").val(),
        creditCards: JSON.stringify(creditCards),
        requiresDelivery: $("#RequiresDelivery").val(),
        ibanNumber: $("#IbanNumber").val(),
        userRoles: JSON.stringify(userRoles)
    };

    $.ajax({
        type: "POST",
        data: JSON.stringify(params),
        url: '../../UserManagement/Create',
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

$("body").on("click", "#btnSaveUser", function () {
    var userRoles = [];
    if ($("#UserRolesSelected option").length > 0) {
        $("#UserRolesSelected option").each(function (i) {
            userRoles.push({
                RoleId: $(this).val(),
                RoleName: $(this).text()
            });
        });
    }

    var creditCards = [];
    if ($("#creditCardsList table tbody tr").length > 0) {
        $("#creditCardsList table tbody tr").each(function (i) {
            creditCards.push({
                Id: $(this).find("#ccd_Id").val(),
                CardHolderName: $(this).find(".cardHolderName").text(),
                CreditCardNumber: $(this).find(".creditCardNumber").text(),
                CreditCardTypeId: $(this).find(".creditCardType").find("input[type=hidden]").val(),
                Month: $(this).find(".expiryDate").text().substring(0, $(this).find(".expiryDate").text().indexOf("/")),
                Year: $(this).find(".expiryDate").text().substring($(this).find(".expiryDate").text().indexOf("/") + 1)
            });
        });
    }

    var params = {
        id: $("#Id").val(),
        email: $("#Email").val(),
        firstName: $("#FirstName").val(),
        lastName: $("#LastName").val(),
        contactNumber: $("#ContactNumber").val(),
        residence: $("#Residence").val(),
        street: $("#Street").val(),
        postCode: $("#PostCode").val(),
        town: $("#Town").val(),
        country: $("#Country").val(),
        creditCards: JSON.stringify(creditCards),
        requiresDelivery: $("#RequiresDelivery").is(':checked'),//$("#RequiresDelivery").val(),
        ibanNumber: $("#IbanNumber").val(),
        userRoles: JSON.stringify(userRoles)
    };

    $.ajax({
        type: "POST",
        data: JSON.stringify(params),
        url: '../../UserManagement/Edit',
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