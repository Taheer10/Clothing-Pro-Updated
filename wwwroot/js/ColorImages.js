
var trackId;
function editImage(Clrid) {
    debugger
    let row = $(`#tblColorImages tbody tr[id='${Clrid}']`);


    let colorName = row.find('td:nth-child(2)').text();
    let colorImage = row.find('td:nth-child(3) img').attr('src');
    let imageName = row.find('td:nth-child(4)').text();
    // let colorImageFile = row.find('td:nth-child(3) img').attr('src');

    // Fill the form with the row's data
    $('#TrackUpdateAddId').val(Clrid);
    $('#TrackUpdateImgName').val(imageName);


    trackId = $('#TrackUpdateAddId').val();
    console.log(trackId);

    $('#ColorImagesName').val(colorName);
    if (colorImage != "" || colorImage != null || colorImage != undefined) {
        $("#ImageEditDiv").show();
        $('#OnEditImgSrc').attr('src', colorImage);
        $('#OnEditImgSrc').css({
            'height': '100px',
            'width': '100px',
            'margin-left': '47%',
            'margin-bottom': '3%'

        });
    }
    // $('#ColorImagesImg').val(colorImageFile);
    /*  $('#ColorImagessavebtn').text('Update').off('click').on('click', UpdateImage);*/
    $('#ColorImagessavebtn').text('Update').attr('onclick', 'UpdateImage()');

    // Set the current row and switch to edit mode
    currentRow = row;
    isEdit = true;
};

function deleteImage(Clrid) {

    if (Clrid === 0 || Clrid === null) {
        alert("Please select Item to Delete");
        return; // Exit function if no valid stId
    }

    var userConfirmed = confirm('Are you sure you want to delete?');
    if (!userConfirmed) {
        return false;
    }
    $(`#tblColorImages tbody tr[id='${Clrid}']`).remove();


    $.ajax({
        type: "POST",
        url: getUrlPath() + "ColorImages/Delete",
        data: { clrId: Clrid }, // Send the data as JSON
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
};



function UpdateImage(index) {
    debugger
    var trkid = $('#TrackUpdateAddId').val();
    var colorToUpdateName = $('#ColorImagesName').val();
    var ImgToUpdateSrc = $("#OnEditImgSrc").attr('src');
    var imageNameToUpdate = $('#TrackUpdateImgName').val();
    if (index == 0 || index == undefined && trkid != 0) {
        let CurrentRowData = $(`#tblColorImages tbody tr[id='${trackId}']`);
        //let colorName = CurrentRowData.find('td:nth-child(2)').text();
        //let colorImage = CurrentRowData.find('td:nth-child(3) img').attr('src');
        //let imageName = CurrentRowData.find('td:nth-child(4)').text();
        CurrentRowData.find('td:nth-child(2)').text(colorToUpdateName);
        CurrentRowData.find('td:nth-child(3) img').attr('src', ImgToUpdateSrc);
        CurrentRowData.find('td:nth-child(4)').text(imageNameToUpdate);
        $('#ColorImagesName').val("");
        $("#ImageEditDiv").hide();
        $("#OnEditImgSrc").attr('src', "");
        $('#ColorImagessavebtn').text('Add').attr('onclick', 'AddImage()');
    }

    if (index != 0 || index != "") {
        let CurrentRowData = $(`tr[data-row-index="${index}"]`);
        CurrentRowData.find('td:nth-child(2)').text(colorToUpdateName);
        CurrentRowData.find('td:nth-child(3) img').attr('src', ImgToUpdateSrc);
        CurrentRowData.find('td:nth-child(4)').text(imageNameToUpdate);
        $('#ColorImagesName').val("");
        $("#ImageEditDiv").hide();
        $("#OnEditImgSrc").attr('src', "");
        $('#ColorImagessavebtn').text('Add').attr('onclick', 'AddImage()');

    }

}


$(document).ready(function () {
    let rowIndex = 1; // Counter for table index
    let isEdit = false; // Flag to determine if the form is in edit mode
    let currentRow; // Store the row that is being edited

    // Function to handle adding or updating image data in the table



    window.AddImage = function () {
        debugger



        let colorName = $('#ColorImagesName').val();
        let colorImageFile = $('#ColorImagesImg')[0].files[0];
        let colorImageName = "";
        if (colorImageFile != undefined) {
            colorImageName = colorImageFile.name.toLowerCase();
        }
        let validExtensions = /\.(jpg|jpeg|png)$/i;

        var colorImageFileSrc = "";
        if (ColorImagesImg.files.length != 0) {
            colorImageFileSrc = ColorImagesImg.files[0].name;
        }

        if (colorImageFileSrc == "") {
            alert(`Please Select Image`);
            return false;
        }

        if (!validExtensions.test(colorImageName)) {
            alert(`Inserted image is not supported. File type: ${colorImageName}`);
            return false;
        }

        // Simple validation to check if fields are filled
        if (!colorName || !colorImageFile) {
            alert('Please fill in all fields!');
            return;
        }

        // Create an object URL for the selected image
        let imageUrl = URL.createObjectURL(colorImageFile);

        // If it's in edit mode, update the current row
        if (isEdit) {
            currentRow.find('td:nth-child(2)').text(colorName);
            currentRow.find('td:nth-child(3)').html(`<img src="${imageUrl}" alt="Color Image" class="img-thumbnail" style="max-width: 100px;">`);
            currentRow.find('td:nth-child(4)').text(colorImageName);
            // Reset the form and button state after updating
            $('#ColorImagesForm')[0].reset();
            $('#ColorImagessavebtn').text('Add').off('click').on('click', AddImage);
            isEdit = false;
        } else {
            debugger
            var last = $('#tblColorImages tbody tr:last-child');
            var lastRowIndex = last.find('td:nth-child(1)').text();

            if (lastRowIndex != 0) {
                rowIndex = parseInt(lastRowIndex) + parseInt(1);
            }
            // Append a new row if it's not in edit mode
            let row = `<tr data-row-index="${rowIndex}">
                                                              <td>${rowIndex}</td>
                                                              <td>${colorName}</td>
                                                              <td><img src="${imageUrl}" alt="Color Image" class="img-thumbnail" style="max-width: 100px;"></td>
                                                                  <td>${colorImageName}</td>
                                                              <td>
                                                                  <button class="btn btn-warning btn-sm" onclick="EditRow(${rowIndex})">Edit</button>
                                                                  <button class="btn btn-danger btn-sm" onclick="DeleteRow(${rowIndex})">Delete</button>
                                                              </td>
                                                          </tr>`;

            $('#tblColorImages tbody').append(row);

            // Reset form fields after adding a new row
            $('#ColorImagesForm')[0].reset();
            rowIndex++;
        }
    };

    // Function to handle editing a row
    window.EditRow = function (index) {
        debugger
        let row = $(`tr[data-row-index="${index}"]`);
        let colorName = row.find('td:nth-child(2)').text();
        let colorImageFile = row.find('td:nth-child(3) img').attr('src');
        let imageName = row.find('td:nth-child(4)').text();
        // Fill the form with the row's data
        $('#ColorImagesName').val(colorName);
        $("#ImageEditDiv").show();
        $('#OnEditImgSrc').css({
            'height': '100px',
            'width': '100px',
            'margin-left': '47%',
            'margin-bottom': '3%'

        });
        $('#OnEditImgSrc').attr('src', colorImageFile);
        $('#TrackUpdateImgName').val(imageName);
        var trkId = $('#TrackUpdateAddId').val();
        $('#TrackUpdateAddId').val(trkId);
        // $('#ColorImagesImg').val(colorImageFile);
        //$('#ColorImagessavebtn').text('Update').off('click').on('click', AddImage);
        $('#ColorImagessavebtn').text('Update').attr('onclick', `UpdateImage(${index})`);


        currentRow = row;
        isEdit = true;
    };

    // Function to delete a row
    window.DeleteRow = function (index) {

        $(`tr[data-row-index="${index}"]`).remove();
    };



});

function getUrlPath() {
    // Get the current URL
    const currentUrl = window.location.href;

    // Create a URL object
    const url = new URL(currentUrl);

    // Construct the base URL
    const baseUrl = `${url.protocol}//${url.hostname}${url.port ? ':' + url.port : ''}/`;

    return baseUrl;
}

window.SaveImage = async function () {
    // debugger
    let formData = new FormData(); // Use FormData to send files and other data
    let promises = []; // Store promises for all async tasks
    let arrClrImgsIds = [];
    let ColorNames = [];

    // Loop through each row of the table body
    $('#tblColorImages tbody tr').each(function () {
         //debugger
        let row = $(this);
        let colorName = row.find('td:nth-child(2)').text();
        let colorImageName = row.find('td:nth-child(4)').text();
        let blobUrl = row.find('td:nth-child(3) img').attr('src'); // Get the image file reference
        let clrImgIds = row.find('td:nth-child(6)').text();
        if (clrImgIds == "") {
            clrImgIds = 0;
        }

        // This function takes a blob URL and converts it back to a Blob object
        async function blobUrlToFile(blobUrl, colorImageName) {
            // Fetch the Blob from the blob URL
            const response = await fetch(blobUrl);
            const blob = await response.blob();

            // Convert the Blob back to a File
            const file = new File([blob], colorImageName, { type: blob.type });

            return file; // Now you have the file object
        }

        // Create a promise to handle the file conversion and formData append
        let promise = blobUrlToFile(blobUrl, colorImageName).then((file) => {
         //   debugger
            console.log(file); // This will log the file object
            formData.append('ColorImagesList[].ColorImagesImg', file); // Append the file to formData
        });
        // Append the color name to FormData
        formData.append('ColorImagesList[].ColorImagesName', colorName);
        formData.append('arrClrImgsIds[]', clrImgIds);
        formData.append('ColorNames[]', colorName);

        // Push the promise into the promises array
        promises.push(promise);
    });

    // Wait for all the promises to resolve
    await Promise.all(promises);

    // Check if any files have been added to FormData
    if (!formData.has('ColorImagesList[].ColorImagesImg')) {
        alert("Please add color images before saving.");
        return;
    }

    // Add stockId to the formData
    var stkid = $("#StockId").val();
    var colorImagesId = $("#ColorImagesId").val();
    console.log(arrClrImgsIds);
    formData.append('ColorImagesId', colorImagesId);
    formData.append('stockId', stkid);

    // Send the data via AJAX to the controller
    $.ajax({
        type: "POST",
        url: getUrlPath() + "ColorImages/CreatePost",
        data: formData,
        processData: false, // Important for sending FormData
        contentType: false, // Important for sending FormData
        success: function (result) {
            debugger
            if (result == true) {
                alert('Data Saved Successfully');
                // Optionally, refresh the page or update the UI
            } else {
                window.location.reload();
            }
        },
        error: function (result) {
            alert("Cannot Save Data. Please try again.");
        }
    });
};


function RedirectColorImages() {
    alert(10)
    window.location.href = getUrlPath() + "ColorImages/Create";
}