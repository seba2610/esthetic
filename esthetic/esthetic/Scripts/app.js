"use strict;"
var myDropzone = undefined;
var filesAdded = false;

$.fn.exists = function () {
    return this.length !== 0;
}

function setAddImagesToCategoryDropzone() {
    // Get the template HTML and remove it from the doumenthe template HTML and remove it from the doument
    var previewNode = document.querySelector("#template");
    previewNode.id = "";
    var previewTemplate = previewNode.parentNode.innerHTML;
    previewNode.parentNode.removeChild(previewNode);

    myDropzone = new Dropzone(document.body, { // Make the whole body a dropzone
        url: "AddImagesToCategory", // Set the url
        thumbnailWidth: 80,
        thumbnailHeight: 80,
        parallelUploads: 20,
        previewTemplate: previewTemplate,
        autoQueue: false, // Make sure the files aren't queued until manually added
        previewsContainer: "#previews", // Define the container to display the previews
        clickable: ".fileinput-button" // Define the element that should be used as click trigger to select files.
    });

    myDropzone.on("addedfile", function (file) {
        $("#actions .start").css("display", "inline-block");
        $("#actions .cancel").css("display", "inline-block");
    });

    myDropzone.on("removedfile", function (file) {
        if (myDropzone.files.length === 0) {
            $("#actions .start").css("display", "none");
            $("#actions .cancel").css("display", "none");
        }
    });

    myDropzone.on("complete", function () {
        if (this.getQueuedFiles().length == 0 &&
            this.getUploadingFiles().length == 0) {
            $("#actions .start").css("display", "none");
            $("#actions .cancel").css("display", "none");
        }
        filesAdded = true;
    });

    // Setup the buttons for all transfers
    // The "add files" button doesn't need to be setup because the config
    // `clickable` has already been specified.
    document.querySelector("#actions .start").onclick = function () {
        myDropzone.enqueueFiles(myDropzone.getFilesWithStatus(Dropzone.ADDED));
    };
    document.querySelector("#actions .cancel").onclick = function () {
        myDropzone.removeAllFiles(true);
    };
}

$(document).ready(function () {
    $('.edit-category-btn').on('click', function (event) {
        $(this).parent().parent().parent().children('.display-none').removeClass('display-none'); 
        $(this).parent().parent().parent().children().eq(0).addClass('display-none');
    });

    $('.cancel-edit-category').on('click', function (event) {
        $(this).parent().parent().parent().parent().children('.display-none').removeClass('display-none');
        $(this).parent().parent().parent().parent().children().eq(1).addClass('display-none');
    });

    $('.delete-category').on('click', function (event) {
        $('#delete-category-modal-link').attr("href", $('#delete-category-modal-input').attr("value") + '/' + $(this).attr("id"));
        $('#delete-category-modal').modal();
    });

    if ($('#jumbo-bottom')) {
        $('#jumbo-bottom').slick({
            dots: true,
            infinite: true,
            speed: 300,
            slidesToShow: 1,
            adaptiveHeight: true,
            arrows: false,
            autoplay: true,
            autoplaySpeed: 3000,
        });
    }

    if ($("#add-images-category-modal").exists()) {
        $('#add-images-category-modal').on('hidden.bs.modal', function (e) {
            myDropzone.removeAllFiles();
            if (filesAdded)
                location.reload();
        })
    }

    if ($(".add-image-btn").exists()) {
        $('.add-image-btn').on('click', function (event) {
            if (!myDropzone)
                setAddImagesToCategoryDropzone();

            myDropzone.options.url = "AddImagesToCategory?categoryId=" + $(event.target).attr("data-image-id");

            filesAdded = false;
            $('#add-images-category-modal').modal();
        });
    }

    if ($(".toggle-images-category").exists()) {
        $(".toggle-images-category").on('click', function (event) {
            var target = undefined;

            if ($(event.target).hasClass('glyphicon'))
                target = $(event.target);
            else if ($(event.target).hasClass('toggle-images-category'))
                target = $(event.target).children('.glyphicon');

            if (target) {
                if ($(target).hasClass("glyphicon-menu-down")) {
                    $(target).parent().parent().children(".row").addClass("zero-height");
                    $(target).parent().parent().children("span").addClass("zero-height");
                    $(target).addClass("glyphicon-menu-up").removeClass("glyphicon-menu-down");
                } else if ($(target).hasClass("glyphicon-menu-up")) {
                    $(target).parent().parent().children(".row").removeClass("zero-height");
                    $(target).parent().parent().children("span").removeClass("zero-height");
                    $(target).addClass("glyphicon-menu-down").removeClass("glyphicon-menu-up");
                }
            }
        });
    }
});