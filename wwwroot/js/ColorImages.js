

function editImage(Clrid) {
    debugger
    let row = $(`#tblColorImages tbody tr[id='${Clrid}']`);


    let colorName = row.find('td:nth-child(2)').text();
    // let colorImageFile = row.find('td:nth-child(3) img').attr('src');

    // Fill the form with the row's data
    $('#ColorImagesName').val(colorName);
    // $('#ColorImagesImg').val(colorImageFile);
    $('#ColorImagessavebtn').text('Update').off('click').on('click', AddImage);

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



$(document).ready(function () {
    let rowIndex = 1; // Counter for table index
    let isEdit = false; // Flag to determine if the form is in edit mode
    let currentRow; // Store the row that is being edited

    // Function to handle adding or updating image data in the table
    window.AddImage = function () {

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

        let row = $(`tr[data-row-index="${index}"]`);
        let colorName = row.find('td:nth-child(2)').text();
        // let colorImageFile = row.find('td:nth-child(3) img').attr('src');

        // Fill the form with the row's data
        $('#ColorImagesName').val(colorName);
        // $('#ColorImagesImg').val(colorImageFile);
        $('#ColorImagessavebtn').text('Update').off('click').on('click', AddImage);

        // Set the current row and switch to edit mode
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
        // debugger
        let row = $(this);
        let colorName = row.find('td:nth-child(2)').text();
        let colorImageName = row.find('td:nth-child(4)').text();
        let blobUrl = row.find('td:nth-child(3) img').attr('src'); // Get the image file reference
        let clrImgIds = row.find('td:nth-child(6)').text();

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
            debugger
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
    alert(arrClrImgsIds);
    alert(ColorNames);
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