$("#addSubcategory").click(function () {
    $.ajax({
        type: "GET",
        data: "{}",
        url: '../../ProductCategoryManagement/CreateSubCategory',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            $("#addSubcategoryContainer").html(data);
        },
        error: function (xhr) {
            var error = JSON.parse(xhr.responseText).message;
            alert(error);
        }
    });
});

$("body").on("click", "#addNewSubcategory", function (e) {
    if ($("#subcategoryList table tbody tr").length <= 0) {
        $("#subcategoryList table thead").append("<tr><th>Subcategory Name</th><th></th></tr>");
    }

    $("#subcategoryList table tbody").append("<tr><td class='name'>" + $("#addSubcategoryContainer #CategoryName").val()
                                        + "</td><td><a href='#' class='remove-table-item'>X</a></td></tr>");

    $("#addSubcategoryContainer").empty();
});

$("body").on("click", "#btnCreateCategory", function () {
    var subcategories = [];
    if ($("#subcategoryList table tbody tr").length > 0) {
        $("#subcategoryList table tbody tr").each(function (i) {
            subcategories.push({
                CategoryName: $(this).find(".name").text()
            });
        });
    }

    var params = {
        categoryName: $("#CategoryName").val(),
        subCategories: JSON.stringify(subcategories)
    };

    $.ajax({
        type: "POST",
        data: JSON.stringify(params),
        url: '../../ProductCategoryManagement/Create',
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

$("#btnSaveCategory").click(function () {
    var subcategories = [];
    if ($("#subcategoryList table tbody tr").length > 0) {
        $("#subcategoryList table tbody tr").each(function (i) {
            subcategories.push({
                CategoryName: $(this).find(".name").text()
            });
        });
    }

    var params = {
        id: $("#CategoryId").val(),
        categoryName: $("#CategoryName").val(),
        subCategories: JSON.stringify(subcategories)
    };

    $.ajax({
        type: "POST",
        data: JSON.stringify(params),
        url: '../../ProductCategoryManagement/Edit',
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