AddAntiForgeryToken = function (data) {
    data.__RequestVerificationToken = $('#__AjaxAntiForgeryForm input[name=__RequestVerificationToken]').val();
    return data;
};

$("body").on("click", ".remove-table-item", function () {
    $(this).parent().parent().remove();
});