AOS.init();
$('.banner').slick({
    prevArrow: "<button class='prev-btn'><i class='fas fa-chevron-left'></i></button>",
    nextArrow: "<button class='next-btn'><i class='fas fa-chevron-right'></i></button>",
    autoplay: true,
    autoplaySpeed: 3000,
});

$('.list-feed-back').slick({
    infinite: true,
    slidesToShow: 3,
    slidesToScroll: 3,
    arrows: false,
    dots: true,
    autoplay: true,
    autoplaySpeed: 3000,
    responsive: [
        {
            breakpoint: 1024,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1,
            }
        },
        {
            breakpoint: 600,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1
            }
        },
        {
            breakpoint: 480,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }

    ]
});

$('.list-partner').slick({
    infinite: true,
    slidesToShow: 6,
    slidesToScroll: 2,
    arrows: false,
    dots: true,
    autoplay: true,
    autoplaySpeed: 3000,
    responsive: [
        {
            breakpoint: 1024,
            settings: {
                slidesToShow: 5,
                slidesToScroll: 3,
            }
        },
        {
            breakpoint: 600,
            settings: {
                slidesToShow: 5,
                slidesToScroll: 5
            }
        },
        {
            breakpoint: 480,
            settings: {
                slidesToShow: 3,
                slidesToScroll:3
            }
        }

    ]
});

$(".search-icon_mb").click(function () {
    $(".form-search__mb").addClass('active')
})
$(".close-form").click(function () {
    $(".form-search__mb").removeClass('active')
})

$(".bar").click(function () {
    $(".header-menu__mb").toggleClass('active')
})

$(window).scroll(function () {
    if ($(this).scrollTop()) {
        $(".back-top").fadeIn();
    } else {
        $(".back-top").fadeOut();
    }
});
$(".back-top").click(function () {
    $("html, body").animate({ scrollTop: 0 }, 1000);
});

$(".hamburger").click(function () {
    $(this).toggleClass("is-active");
    $(".header-menu__mb").toggleClass("active");
    $(".overlay").toggleClass("active");

});


$(".overlay").click(function () {
    $(this).toggleClass("is-active");
    $(".header-menu__mb").removeClass("active");
    $(".overlay").removeClass("active");
    $(".hamburger").toggleClass("is-active");
});


$('.product-thumnail').slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    arrows: true,
    fade: true,
    asNavFor: '.product-nav',
    prevArrow: "<button class='prev-btn'><i class='fas fa-chevron-left'></i></button>",
    nextArrow: "<button class='next-btn'><i class='fas fa-chevron-right'></i></button>",
});
$('.product-nav').slick({
    slidesToShow: 6,
    slidesToScroll: 1,
    asNavFor: '.product-thumnail',
    arrows: false,
    dots: false,
    focusOnSelect: true
});



$(".detail-btn span").click(function () {
    $(".form-model").addClass("active")
})
$(".close-model").click(function () {
    $(".form-model").removeClass("active")
})

$('ul.pagination li.PagedList-skipToNext a').html('<i class="fas fa-chevron-right"></i>');
$('ul.pagination li.PagedList-skipToPrevious a').html('<i class="fas fa-chevron-left"></i>');




function Sort(action) {
    $(document).on('click', '[data-filter]', function () {
        let sort = "";
        let url = $("input[name=currentUrl]").val();
        let keywords = $("input[name=keywords]").val();
        let catId = "";
        let title = $('.breadcrumb-item.active').text();

        $("[name=brandId]:checked").map(function () {
            brandId += $(this).val();
        });

        $("[name=sort]:checked").map(function () {
            sort += $(this).val();
        });

        $("[name=catId]:checked").map(function () {
            catId += $(this).val();
        });

        url = url.split('/').at(-1);

        window.history.pushState(title, '', url);
        $.get(action, { keywords: keywords, url, sort: sort, catId: catId }, function (data) {
            $('#list-item-sort').empty();
            $('#list-item-sort').html(data);

            var t = $('#list-item-sort').position().top; //lấy vị trí của phần trang web gán cho biến t
            $('html,body').stop().animate({ scrollTop: t }, 400); //cuộn trang đến phần với biến t
        });
    });
}
accordionNav = $(function () {
    $('.menu-toggle').click(function (e) {
        e.preventDefault();
        var toggleButton = $(this);
        if (toggleButton.next().hasClass('active')) {
            toggleButton.next().removeClass('active');
            toggleButton.next().slideUp(400);
            toggleButton.removeClass('rotate');
        } else {
            toggleButton.parent().parent().find('li .sub-menu').removeClass('active');
            toggleButton.parent().parent().find('li .sub-menu').slideUp(400);
            toggleButton.parent().parent().find('.menu-toggle').removeClass('rotate');
            toggleButton.next().toggleClass('active');
            toggleButton.next().slideToggle(400);
            toggleButton.toggleClass('rotate');
        }
    });
});

function DetailJS(){ 
}
$("#review-form").on("submit", function (e) {
    e.preventDefault();
    $.post("/Home/ReviewForm", $(this).serialize(), function (data) {
        if (data.status) {
            $.toast({
                heading: "Đánh giá thành công",
                text: data.msg,
                icon: "success",
                position: "bottom-right"
            });
            $('.review-form input').val("");
            $('.form-vote-input textarea').val("");
        } else {
            $.toast({
                heading: "Đánh giá không thành công",
                text: data.msg,
                icon: "error",
                position: "bottom-right"
            });
        }
    });
});


$("#orderForm").on("submit", function (e) {
    e.preventDefault();
    $.post("/Home/OrderForm", $(this).serialize(), function (data) {
        if (data.status) {
            $.toast({
                heading: "Gửi liên hệ thành công",
                text: data.msg,
                icon: "success",
                position: "bottom-right"
            });
            $(".contact-form").trigger("reset");
            $(".form-model").removeClass("active")
            $('.model-input input').val("");
            $('.model-input textarea').val("");
            $('.quantity').val("1");

        } else {
            $.toast({
                heading: "Liên hệ không thành công",
                text: data.msg,
                icon: "error",
                position: "bottom-right"
            });
        }
    });
});


$("#contact-form").on("submit", function (e) {
    e.preventDefault();
    $.post("/Home/ContactForm", $(this).serialize(), function (data) {
        if (data.status) {
            $.toast({
                heading: "Gửi liên hệ thành công",
                text: data.msg,
                icon: "success",
                position: "bottom-right"
            });
            $('.contact-item input').val("");
            $('.contact-item textarea').val("");
        } else {
            $.toast({
                heading: "Gửi liên hệ không thành công",
                text: data.msg,
                icon: "error",
                position: "bottom-right"
            });
        }
    });
});

$(".quantity").change(function () {
    const quantity = $(this).val();
    const price = $("input[name='Order.ProductPrice'][type=hidden]").val();
    const totalPrice = quantity * Number(price.replace(/[^0-9.-]+/g, ""));
    const output = totalPrice.toLocaleString();
    const number = Number(output.replace(/[^0-9.-]+/g, ""));

    $(".total-price").text(output);
    $("input[name='TotalPrice'][type=hidden]").val(number);
});