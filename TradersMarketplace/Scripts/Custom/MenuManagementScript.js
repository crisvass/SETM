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
        $("#submenuList table thead").append("<tr><th>Title</th><th>Action</th><th>URL</th><th>Position</th></tr>");
    }

    $("#submenuList table tbody").append("<tr><td class='title'>" + $("#submenuTitle").val()
                                        + "</td><td class='action'>" + $("#submenuAction").val()
                                        + "</td><td class='url'>" + $("#submenuUrl").val()
                                        + "</td><td class='position'>"
                                        + ($("#submenuList table tbody tr").length + 1) + "</td></tr>");

    $("#addSubmenuContainer").empty();
});

$("body").on("click", "#btnAddAllRoles", function () {
    $("#MenuRoles option").each(function (i) {
        $("#MenuRolesSelected").append($(this));
    });
    $("#MenuRoles option").remove();
    removeSelections();
});

$("body").on("click", "#btnAddRole", function () {
    $("#MenuRolesSelected").append($("#MenuRoles option:selected"));
    $("#MenuRoles option:selected").remove();
    removeSelections();
});

$("body").on("click", "#btnRemoveRole", function () {
    $("#MenuRoles").append($("#MenuRolesSelected option:selected"));
    $("#MenuRolesSelected option:selected").remove();
    $("#MenuRoles option:selected").removeAttr("selected");
});

$("body").on("click", "#btnRemoveAllRoles", function () {
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
    if ($("#MenuRolesSelected option").length > 0)
    {
        $("#MenuRolesSelected option").each(function(i){
            menuRoles.push({
                RoleId: $(this).val(),
                RoleName:$(this).text()
            });
        });    
    }

    var submenus = [];
    if ($("#submenuList table tbody tr").length <= 0) {
        $("#submenuList table tbody tr").each(function (i) {
            submenus.push({
                Title: $(this).find(".title").text(),
                Action:$(this).find(".action").text(),
                Url:$(this).find(".url").text(),
                Position: $(this).find(".position").text()
            });
        });
    }

    var params = "{'title' : '" + $("#Title").val()
                    "', 'action' : '" + $("#Action").val() +
                    "', 'url' : '" + $("#Url").val() +
                    "', 'submenus' : '" + JSON.stringify(submenus) +
                    "', 'menuRoles' : '" + JSON.stringify(menuRoles) +
                    "'}";

    $.ajax({
        type: "POST",
        data: params,
        url: '../../MenuManagement/Create',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {            
        },
        error: function (xhr) {
            var error = JSON.parse(xhr.responseText).message;
            alert(error);
        }
    });
});
