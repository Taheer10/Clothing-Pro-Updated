//$("#txtSearch").keyup(function () {
//    var typeValue = $(this).val();
//    $('tbody tr').each(function () {
//        if ($(this).text().search(new RegExp(typeValue, "i")) < 0) {
//            $(this).fadeOut();
//        } else {
//            $(this).show();
//        }
//    })
//});



//$("#search-input-value").on("keyup", function (event) {
//    var len = $(this).val().length;
//    if ($(this).val().length == 0) {
//        $('#dv-quick-search').hide();
//    } else {
//        if ($(this).val().length > 1) {
//            searchQuickStockMenu();
//        }
//    }
//});

$(document).keydown(function (event) {
    if (event.key === "Enter") {
        //alert("Hello");
        var val = $("#search-input-value").val();
        window.location.href = getUrlPath() + "MenuHeader/Search?search=" + val;
    }
});


function searchQuickStockMenu() {
    var val = $("#search-input-value").val();
    if (val != "") {
        $.ajax({
            url: getUrlPath() + "Stock/Search?search=" + val,
            method: "GET",
            success: function (response) {
                var json = JSON.parse(response.model);
                $("#dv-quick-search").show();
                $("#tbl-quick-search>tbody").html('');
                var trHTML = '';
                $.each(json, function (i) {
                    trHTML += '<tr class="border-bottom" onclick=onclickstockmenusearchlist(' + json[i].StId + ') style="cursor:pointer" title="Click to select"><td>' + json[i].StName + '</td><td>' + json[i].StDes + '</td><td>' + json[i].StCode + '</td><td>' + json[i].MenuHeaderName + '</td></tr > ';
                    i++;
                });
                $("#tbl-quick-search>tbody").append(trHTML);
            },
            error: function () {
                alert("Customer Does Not Exist");
            }
        });
    } else {
        alert("Please enter Value.");
    }
}

function onclickstockmenusearchlist(StId) {
    $.ajax({
        type: "GET",
        url: getUrlPath() + "Stock/StockJsonReturn?StId=" + StId,
        data: {},
        dataType: "JSON",
        success: function (response) {
            var stock = JSON.parse(response.model);
            //var images = stock.StImage.split(';');

            $("#StId").val(stock.StId);
            //$("#stimagelist").val(stock.StImage);
            //$("#stimageindividual").val(images[0]);
            //$("#stockname").val(stock.StName);
            //$("#stockcode").val(stock.StCode);
            //$("#stockdes").val(stock.StDes);
            $('#dv-quick-search').hide();

        },
        error: function (response) {
        }
    }).done(function (response) {
        var stockdetail = JSON.parse(response.model);
        $('#dv-quick-search').hide();
        window.location.href = getUrlPath() + "Stock/StockDetail?StId=" + stockdetail.StId;
    });
}

$(document).ready(function () {
    $('#search-button').click(function () {
     
        var searchInput = $('#search-input-value');
        var searchResults = $('#dv-quick-search');
        searchResults.css({
            'top': searchInput.offset().top + searchInput.outerHeight(),
            'left': searchInput.offset().left,
            'width': searchInput.outerWidth()
        });
        searchResults.show();
    });
});


function DisplaySearchItemsList(search) {
    debugger
    var val = $("#search-input-value").val();
    if (search == "All Items") {
        window.location.href = getUrlPath() + "MenuHeader/Search?search=" + search;

    } else {
        window.location.href = getUrlPath() + "MenuHeader/Search?search=" + val;

    }
    //alert(val);
    //if (val != "") {
    //    $.ajax({
    //        url: getUrlPath() + "Stock/Search?search=" + val,
    //        method: "GET",
    //        success: function (response) {
    //            var json = JSON.parse(response.model);
                
    //        },
    //        error: function () {
    //            alert("Customer Does Not Exist");
    //        }
    //    });
    //} else {
    //    alert("Please enter Value.");
    //}

}