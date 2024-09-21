function SaveBanner() {
    alert(10)
    $("#bannersavebtn").hide();

    // Create a new FormData object
    var formData = new FormData();

    formData.append("BannerId", $("#BannerId").val());
    formData.append("BannerIsActive", $("#BannerIsActive").val());
    formData.append("StSortOrder", $("#StSortOrder").val());

    var fileInput = document.getElementById("BannerImg");
    if (fileInput.files.length > 0) {
        formData.append("BannerImg", fileInput.files[0]);
    }

    // Send the AJAX request
    $.ajax({
        type: "POST",
        url: getUrlPath() + "Banner/CreatePost",
        data: formData,
        contentType: false, // Necessary for sending file data
        processData: false, // Prevent jQuery from processing the data
        success: function (result) {
            if (result.success) {
                alert(result.message);
                window.location.href = getUrlPath() + "Stock/Index";
            } else {
                alert("Error: " + result.message);
            }
        },
        error: function (result) {
            alert("Cannot Save Data. Please try again.");
        }
    });
}