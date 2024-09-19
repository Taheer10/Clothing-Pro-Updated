//const { Alert } = require("../lib/bootstrap/dist/js/bootstrap.bundle");

//$(document).ready(function () {
//    $("#StName").focus();

//    $("#StImage").change(function () {
//        const file = this.files[0];
//        if (file) {
//            const reader = new FileReader();
//            reader.onload = function (event) {
//                $("#StImage").data("base64", event.target.result);
//            };
//            reader.readAsDataURL(file);
//        }
//    });
//});


function getUrlPath() {
    // Get the current URL
    const currentUrl = window.location.href;

    // Create a URL object
    const url = new URL(currentUrl);

    // Construct the base URL
    const baseUrl = `${url.protocol}//${url.hostname}${url.port ? ':' + url.port : ''}/`;

    return baseUrl;
}

// Usage example
const basePath = getUrlPath();
console.log(basePath); // Will print the base URL with the correct port

//function ValidateSave() {
//    $("#SubmitButton").hide();
//    var stockInfo = {
//        StId: $("#StId").val(),
//        StName: $("#StName").val(),
//        StDes: $("#StDes").val(),
//        StInActive: $("#StInActive").val(),
//        StCode: $("#StCode").val(),
//        StImage: $("#StImage").data("base64"),
//        StIsPopular: $("#StIsPopular").val(),
//        StColour: $("#StColour").val(),
//        StSize: $("#StSize").val(),
//        StHSCode: $("#StHSCode").val(),
//        StIsShirt: $("#StIsShirt").val(),
//        StIsPant: $("#StIsPant").val(),
//        StIsOther: $("#StIsOther").val(),
//        StSortOrder: $("#StSortOrder").val(),
//        StMenuHeaderId: $("#StMenuHeaderId").val(),
//    };

//    $.ajax({
//        type: "POST",
//        url: getUrlPath() + "Stock/post-stock",
//        data: { model: stockInfo },
//        success: function (result) {
//            if (result == "success") {
//                alert('Data Saved Successfully');
//                window.location.reload();
//            } else {
//                //toastr.error(result);
//            }
//        },
//        error: function (result) {
//            alert("Cannot Save Data. Please try again.");
//        }
//    });
//}

//function ValidateSave() {
//    var stockInfo = {
//        StId: $("#StId").val(),
//        StName: $("#StName").val(),
//        StDes: $("#StDes").val(),
//        StInActive: $("#StInActive").val(),
//        StCode: $("#StCode").val(),
//        StIsPopular: $("#StIsPopular").val(),
//        StColour: $("#StColour").val(),
//        StSize: $("#StSize").val(),
//        StHSCode: $("#StHSCode").val(),
//        StIsShirt: $("#StIsShirt").val(),
//        StIsPant: $("#StIsPant").val(),
//        StIsOther: $("#StIsOther").val(),
//        StSortOrder: $("#StSortOrder").val(),
//        StMenuHeaderId: $("#StMenuHeaderId").val()
//    };

//    var formData = new FormData();
//    // Append stock info
//    formData.append("model", JSON.stringify(stockInfo));
//    // Append files
//    var files = $("#StImages").get(0).files;
//    for (var i = 0; i < files.length; i++) {
//        formData.append("StImages", files[i]);
//    }

//    $.ajax({
//        type: "POST",
//        url: getUrlPath() + "Stock/post-stock",
//        data: formData,
//        contentType: false,
//        processData: false,
//        success: function (result) {
//            if (result === "success") {
//                alert('Data Saved Successfully');
//                window.location.reload();
//            } else {
//                // Handle the error
//                alert("Error: " + result);
//            }
//        },
//        error: function (result) {
//            alert("Cannot Save Data. Please try again.");
//        }
//    });
//}

function addColorPicker() {
    var container = document.getElementById('color-picker-container');
    var newColorPicker = document.createElement('div');
    newColorPicker.className = 'color-picker';
    newColorPicker.innerHTML = `
                <input type="color" name="colors" />
                <button type="button" class="fa fa-trash text-danger" onclick="removeColor(this)"></button>
            `;
    container.appendChild(newColorPicker);
}

function removeColor(button) {
    var colorPicker = button.parentElement;
    colorPicker.remove();
}

function getColors() {
    var colors = [];
    var colorPickers = document.querySelectorAll('#color-picker-container input[name="colors"]');
    colorPickers.forEach(function (picker) {
        colors.push(picker.value);
    });
    return colors.join(';');
}

function ValidateSave() {
    var stockInfo = {
        StId: $("#StId").val(),
        StName: $("#StName").val(),
        StDes: $("#StDes").val(),
        StImageUpdate: $("#StImageUpdate").val(),
        StInActive: $("#StInActive").val(),
        StCode: $("#StCode").val(),
        StIsPopular: $("#StIsPopular").val(),
        //StColour: $("#StColour").val(),
        StSize: $("#StSize").val(),
        StHSCode: $("#StHSCode").val(),
        StIsShirt: $("#StIsShirt").val(),
        StIsPant: $("#StIsPant").val(),
        StIsOther: $("#StIsOther").val(),
        StSortOrder: $("#StSortOrder").val(),
        StMenuHeaderId: $("#StMenuHeaderId").val()
    };
    debugger

    var formData = new FormData();
    // Append model data as a JSON string
    formData.append("model", JSON.stringify(stockInfo));

    var colors = getColors(); // Function to get the selected colors
    formData.append("StColours", colors);

    // Append files
    var files = $("#StImages").get(0).files;
    for (var i = 0; i < files.length; i++) {
        formData.append("StImages", files[i]);
    }

    $.ajax({
        type: "POST",
        url: getUrlPath() + "Stock/post-stock",
        data: formData,
        contentType: false,
        processData: false,
        success: function (result) {
            if (result === "success") {
                alert('Data Saved Successfully');
                //window.location.reload();
                window.location.href = getUrlPath() + "Stock/Index";
            } else {
                // Handle the error
                alert("Error: " + result);
            }
        },
        error: function (result) {
            alert("Cannot Save Data. Please try again.");
        }
    });
}

//function displayLatestItems() {
//    var fromdate = $("#fromdate").val();
//    var todate = $("#todate").val();
//    window.location.href = getUrlPath() + "Home/StockLatetsItems?FromDate=" + fromdate + "&Todate=" + todate;
//}

//function displayLatestItems() {
//    alert(10)
//    var fromdate = $("#fromdate").val();
//    var todate = $("#todate").val();
//    alert(fromdate)
//    alert(todate)
//    window.location.href = getUrlPath() + "Home/StockLatetsItems?FromDate=" + fromdate + "&Todate=" + todate;
//}

function createstock(stId) {
    if (stId && stId !== 0) {
        //window.location.href = getUrlPath() + "stock/create-stock?StId=" + stId;
        window.location.href = getUrlPath() + "stock/stockdetail?StId=" + stId;
    } else {
        alert("Select Item");
    }
}
function Editstock(stId) {
    if (stId && stId !== 0) {
        window.location.href = getUrlPath() + "stock/create-stock?StId=" + stId;
    } else {
        alert("Select Item");
    }
}

function showMenuHeader() {
    $("#menuheadertitle").show();
    $("#navbarlist").show();
    $("#hidemenuheader").show();
    $("#showmenuheader").hide();

}

function hideMenuHeader() {
    $("#menuheadertitle").hide();
    $("#navbarlist").hide();
    $("#showmenuheader").show();
    $("#hidemenuheader").hide();

}

function deletestock(stId) {
    if (stId && stId !== 0) {
        window.location.href = getUrlPath() + "stock/Delete?StId=" + stId;
    } else {
        alert("Select Item");
    }
}

function deletestock(stId) {
    if (stId === 0 || stId === null) {
        alert("Please select Item to Delete");
        return; // Exit function if no valid stId
    }

    var userConfirmed = confirm('Are you sure you want to delete?');
    if (!userConfirmed) {
        return false;
    }

    $.ajax({
        type: "POST",
        url: getUrlPath() + "Stock/Delete",
        data: { StId: stId }, // Send the data as JSON
        success: function (result) {
            if (result == true) {
                alert('Data Deleted Successfully');
                // Optionally, refresh the page or update the UI
            } else {
                //alert("Error: " + result); // Better to show the error message if available
                window.location.reload();
                alert('Data Deleted Successfully');

            }
        },
        error: function (result) {
            alert("Cannot Delete Data. Please try again.");
        }
    });
}

function openDetail(stId) {
    window.location.href = getUrlPath() + "stock/create-stock?StId=" + stId;
}

function displayPopularItems() {
    window.location.href = getUrlPath() + "stock/StockPopularItems";
}

function displayLatestItems() {
    window.location.href = getUrlPath() + "Home/StockLatetsItems";
}

function saveMenuHeader() {
    $("#SubmitButton").hide();
    var menuInfo = {
        MenuHeaderId: $("#MenuHeaderId").val(),
        MenuHeaderName: $("#MenuHeaderName").val(),
        MenuHeaderIsActive: $("#MenuHeaderIsActive").val(),
        StSortOrder: $("#StSortOrder").val(),

    };

    $.ajax({
        type: "POST",
        url: getUrlPath() + "MenuHeader/CreatePost",
        data: { model: menuInfo },
        success: function (result) {
            if (result == "success") {
                alert('Data Saved Successfully');
                window.location.reload();
            } else {
                //toastr.error(result);
            }
        },
        error: function (result) {
            alert("Cannot Save Data. Please try again.");
        }
    });
}

function saveCompanyDetail() {
    $("#SubmitCompanyButton").hide();
    var CompanyInfo = {
        CompanyId: $("#CompanyId").val(),
        CompanyName: $("#CompanyName").val(),
        ContactNo: $("#ContactNo").val(),
        CompanyEmail: $("#CompanyEmail").val(),

    };

    $.ajax({
        type: "POST",
        url: getUrlPath() + "Company/CreatePost",
        data: { model: CompanyInfo },
        success: function (result) {
            if (result == "success") {
                alert('Data Saved Successfully');
                window.location.reload();
            } else {
                //toastr.error(result);
            }
        },
        error: function (result) {
            alert("Cannot Save Data. Please try again.");
        }
    });
}

function RedirectStockCreate() {
    window.location.href = getUrlPath() + "Stock/Create-Stock";

}

function createMenuHeader(id) {
    window.location.href = getUrlPath() + "MenuHeader/Create?mnId=" + id;
}
function getStockMenuList(id) {
    window.location.href = getUrlPath() + "MenuHeader/GetStockMenuDetail?mnId=" + id;
}

function RedirectMenuHeader() {
    window.location.href = getUrlPath() + "MenuHeader/Create";
}

function deleteMenuHeader(mnId) {
    if (mnId === 0 || mnId === null) {
        alert("Please select Item to Delete");
        return; // Exit function if no valid stId
    }

    var userConfirmed = confirm('Are you sure you want to delete?');
    if (!userConfirmed) {
        return false;
    }

    $.ajax({
        type: "POST",
        url: getUrlPath() + "MenuHeader/Delete",
        data: { mnId: mnId }, // Send the data as JSON
        success: function (result) {
            if (result == true) {
                alert('Data Deleted Successfully');
                // Optionally, refresh the page or update the UI
            } else {
                //alert("Error: " + result); // Better to show the error message if available
                window.location.reload();
                alert('Data Deleted Successfully');

            }
        },
        error: function (result) {
            alert("Cannot Delete Data. Please try again.");
        }
    });
}


function OpenImageDetail(imagePath) {
    window.open(imagePath, '_blank');
}

function ChangeImageDetail(imageUrl) {
    $(".stockimagecontained").attr("src", imageUrl); 
}