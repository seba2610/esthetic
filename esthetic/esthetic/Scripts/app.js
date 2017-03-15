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
});