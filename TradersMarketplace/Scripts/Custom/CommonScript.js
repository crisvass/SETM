AddAntiForgeryToken = function (data) {
    data.__RequestVerificationToken = $('#__AjaxAntiForgeryForm input[name=__RequestVerificationToken]').val();
    return data;
};

$("body").on("click", ".remove-table-item", function () {
    if ($(this).parent().parent().parent().find("tr").length == 1) {
        $(this).parent().parent().parent().prev().find("tr").remove();
    }
    $(this).parent().parent().remove();
});