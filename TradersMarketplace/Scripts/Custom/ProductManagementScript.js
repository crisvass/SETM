$('body').on('change', '#CategoryId', function () {
    getProductSubCategories($(this).val(), null);
});

function getProductSubCategories(parentId, categoryId) {
    var params = { parentId: parentId };
    $.ajax({
        type: "POST",
        url: "../../ProductManagement/GetProductSubcategories",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            data = data.trim();
            if (data != null && data != "") {
                $("#SubcategoryId").prop("disabled", false);
                $("#SubcategoryId").html(data);
            }
            else {
                $("#SubcategoryId").prop("disabled", true);
                $("#SubcategoryId").html("<option value=''>Select Subcategory</option>");
            }            
        },
        error: function (xhr) {
            var error = JSON.parse(xhr.responseText).message;
            alert(error);
        }
    });
}

$('#productUploader').change(function (e) {
    $this = $(this);
    if ($this.val().length > 0) {
        var imageType = readURL(this, $("#productPreviewImage"));
        $("#ImagePath").val(convertImageToBase64(imageType, $("#productPreviewImage").prop("src"), 475, 400))
    }
});

//image upload
function readURL(input, img) {
    if (input.files && input.files[0]) {//Check if input has files.
        var reader = new FileReader(); //Initialize FileReader.

        reader.onload = function (e) {
            img.prop('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
        return input.files[0].type;
    }
}

function convertImageToBase64(type, imgSrc, width, height) {
    var canvas = document.createElement("canvas");
    var img = document.createElement("img");
    img.setAttribute('src', imgSrc);

    canvas.width = width;
    canvas.height = height;
    var ctx = canvas.getContext("2d");
    ctx.drawImage(img, 0, 0, width, height);
    var dataURL = canvas.toDataURL(type);
    return dataURL;
}