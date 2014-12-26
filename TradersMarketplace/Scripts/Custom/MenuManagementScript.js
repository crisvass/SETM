$("#addNewSubmenu").click(function () {
    $.ajax({
        type: "GET",
        data: "{}",
        url: '../../MenuManagement/CreateSubmenu',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            $("#addSubmenuContainer").html(data);
        },
        error: function (xhr) {
            var error = JSON.parse(xhr.responseText).message;
            alert(error);
        }
    });
});

$("body").on("click", "#btnAddSubmenu", function (e) {
    if ($("#submenuList table tbody tr").length <= 0) {
        $("#submenuList table thead").append("<tr><th>Title</th><th>Action</th><th>URL</th><th>Position</th><th></th></tr>");
    }

    $("#submenuList table tbody").append("<tr><td class='title'>" + $("#submenuTitle").val()
                                        + "</td><td class='action'>" + $("#submenuAction").val()
                                        + "</td><td class='url'>" + $("#submenuUrl").val()
                                        + "</td><td class='position'>"
                                        + ($("#submenuList table tbody tr").length + 1)
                                        + "</td><td><a href='#' class='remove-table-item'>X</a></td></tr>");

    $("#addSubmenuContainer").empty();
});

$("body").on("click", "#btnAddAllMenuRoles", function () {
    $("#MenuRoles option").each(function (i) {
        $("#MenuRolesSelected").append($(this));
    });
    $("#MenuRoles option").remove();
    removeSelections();
});

$("body").on("click", "#btnAddMenuRole", function () {
    $("#MenuRolesSelected").append($("#MenuRoles option:selected"));
    $("#MenuRoles option:selected").remove();
    removeSelections();
});

$("body").on("click", "#btnRemoveMenuRole", function () {
    $("#MenuRoles").append($("#MenuRolesSelected option:selected"));
    $("#MenuRolesSelected option:selected").remove();
    $("#MenuRoles option:selected").removeAttr("selected");
});

$("body").on("click", "#btnRemoveAllMenuRoles", function () {
    $("#MenuRolesSelected option").each(function (i) {
        $("#MenuRoles").append($(this));
        $("#MenuRoles option:selected").removeAttr("selected");
    });
    $("#MenuRolesSelected option").remove();
});

function removeSelections() {
    $("#MenuRoles option:selected").removeAttr("selected");
    $("#MenuRolesSelected option:selected").removeAttr("selected");
}

$("body").on("click", "#btnCreateMenu", function () {
    var menuRoles = [];
    if ($("#MenuRolesSelected option").length > 0) {
        $("#MenuRolesSelected option").each(function (i) {
            menuRoles.push({
                RoleId: $(this).val(),
                RoleName: $(this).text()
            });
        });
    }

    var submenus = [];
    if ($("#submenuList table tbody tr").length > 0) {
        $("#submenuList table tbody tr").each(function (i) {
            submenus.push({
                Title: $(this).find(".title").text(),
                Action: $(this).find(".action").text(),
                Url: $(this).find(".url").text(),
                Position: $(this).find(".position").text()
            });
        });
    }

    var params = {
        title: $("#Title").val(),
        action: $("#Action").val(),
        url: $("#Url").val(),
        submenus: JSON.stringify(submenus),
        menuRoles: JSON.stringify(menuRoles)
    };
    //AddAntiForgeryToken(params);


    //var headers = {};
    //AddAntiForgeryToken(headers);

    $.ajax({
        type: "POST",
        //headers: headers,
        data: JSON.stringify(params),
        url: '../../MenuManagement/Create',
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

$("#btnSaveMenu").click(function () {
    var menuRoles = [];
    if ($("#MenuRolesSelected option").length > 0) {
        $("#MenuRolesSelected option").each(function (i) {
            menuRoles.push({
                RoleId: $(this).val(),
                RoleName: $(this).text()
            });
        });
    }

    var submenus = [];
    if ($("#submenuList table tbody tr").length > 0) {
        $("#submenuList table tbody tr").each(function (i) {
            submenus.push({
                Title: $(this).find(".title").text(),
                Action: $(this).find(".action").text(),
                Url: $(this).find(".url").text(),
                Position: $(this).find(".position").text()
            });
        });
    }

    var params = {
        id: $("#menuId").val(),
        title: $("#Title").val(),
        action: $("#Action").val(),
        url: $("#Url").val(),
        submenus: JSON.stringify(submenus),
        menuRoles: JSON.stringify(menuRoles)
    };

    $.ajax({
        type: "POST",
        data: JSON.stringify(params),
        url: '../../MenuManagement/Edit',
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