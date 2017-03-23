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
        if (this.getQueuedFiles().length === 0 &&
            this.getUploadingFiles().length === 0) {
            $("#actions .start").css("display", "none");
            $("#actions .cancel").css("display", "none");
        }
        filesAdded = true;
    });

    document.querySelector("#actions .start").onclick = function () {
        myDropzone.enqueueFiles(myDropzone.getFilesWithStatus(Dropzone.ADDED));
    };
    document.querySelector("#actions .cancel").onclick = function () {
        myDropzone.removeAllFiles(true);
    };
}

$(document).ready(function () {

    setCategoryButtons();

    setThumbnailButtons();

    $('.delete-images-from-category').on('click', function (event) {
        $('#all-images-category-link').attr("href", $('#all-images-category-link-input').attr("value") + '?categoryId=' + $(this).attr("data-image-id"));
        $('#all-images-category-modal').modal();
    });

    $('.delete-img-from-category').on('click', function (event) {
        var categoryId = $(event.target).parent().parent().parent().parent().parent().parent().attr("data-category-id");
        $('#delete-image-category-link').attr("href", $('#delete-image-category-link-input').attr("value") + '?categoryId=' + categoryId + '&imageId=' + $(this).attr("data-image-id"));
        $('#delete-image-category-modal').modal();
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

            myDropzone.options.url = "AddImagesToCategory?categoryId=" + $(event.target).attr("data-category-id");

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

function setThumbnailButtons() {

    $('.edit-image-btn').each(function (elem) {
        if (!isOnClickBinded($(this))) {
            $(this).on('click', function (event) {
                var visibles = $(this).parent().parent().children('.display-block');
                var hiddens = $(this).parent().parent().children('.display-none');

                $(visibles).addClass('display-none').removeClass('display-block');
                $(hiddens).addClass('display-block').removeClass('display-none');
                $(this).parent().parent().parent().children('.img-wrapper').addClass('display-none');
            });
        }
    });

    $('.cancel-edit-image-btn').each(function (elem) {
        if (!isOnClickBinded($(this))) {
            $(this).on('click', function (event) {
                var visibles = $(this).parent().parent().children('.display-block');
                var hiddens = $(this).parent().parent().children('.display-none');
                $(visibles).addClass('display-none').removeClass('display-block');
                $(hiddens).addClass('display-block').removeClass('display-none');
                $(this).parent().parent().parent().children('.img-wrapper').removeClass('display-none');
            });
        }
    });

    $('.add-image-category-btn').each(function (elem) {
        if (!isOnClickBinded($(this))) {
            $(this).on('click', function (event) {
                var disable_categories = $(this).attr("data-categories-id").split(" ");
                var image_id = $(this).attr("data-image-id");
                $("#add-image-to-category-id").attr("value", image_id);
                $('.category-checkbox').attr('disabled', false);

                for (var i = 0; i < disable_categories.length; i++){
                    var selector = '#checkbox_' + disable_categories[i];
                    $(selector).attr('disabled', true);
                }

                $('.category-checkbox').on('change', function (event) {
                    if ($('.category-checkbox:checked').length > 0) {
                        $('#add-image-category-link').css("display", "inline-block");
                    }
                    else
                        $('#add-image-category-link').css("display", "none");
                });

                $('#add-category-image-modal').modal();
            });
        }
    });

}

function setCategoryButtons() {

    $('.edit-category-btn').each(function (elem) {
        if (!isOnClickBinded($(this))) {
            $(this).on('click', function (event) {
                $(this).parent().parent().children('.display-none').removeClass('display-none');
                $(this).parent().parent().children().eq(0).addClass('display-none');
            });
        }
    });

    $('.cancel-edit-category').each(function (elem) {
        if (!isOnClickBinded($(this))) {
            $(this).on('click', function (event) {
                $(this).parent().parent().children('.display-none').removeClass('display-none');
                $(this).parent().parent().children().eq(1).addClass('display-none');
            });
        }
    });

    $('.delete-category').each(function (elem) {
        if (!isOnClickBinded($(this))) {
            $(this).on('click', function (event) {
                $('#delete-category-modal-link').attr("href", $('#delete-category-modal-input').attr("value") + "/" + $(this).attr("id"));
                $('#delete-category-modal').modal();
            });
        }
    });
}

function isOnClickBinded(elem) {
    var result = false;
    var $events = jQuery._data(elem[0], "events");

    if (typeof $events != "undefined") {
        jQuery.each($events, function (i, event) {
            jQuery.each(event, function (i, event) {
                if (event.type && event.type === "click" && result === false) {
                    result = true;
                }
            });
        });
    }

    return result;

}