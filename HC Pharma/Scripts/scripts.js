AOS.init();
function homeJs() {
    $('.banner').slick({
        prevArrow: "<button class='prev-btn'><i class='fas fa-chevron-left'></i></button>",
        nextArrow: "<button class='next-btn'><i class='fas fa-chevron-right'></i></button>",
        autoplay: true,
        autoplaySpeed: 3000,
        speed: 1000,
        infinite: true,
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
                    slidesToScroll: 3
                }
            }

        ]
    });
    $('.list-people').slick({
        infinite: true,
        slidesToShow: 4,
        slidesToScroll: 1,
        arrows: false,
        dots: true,
        autoplay: true,
        autoplaySpeed: 3000,
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 3
                }
            },
            {
                breakpoint: 600,
                settings: {
                    slidesToShow: 2
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1
                }
            }

        ]
    });

}

window.addEventListener("load", function () {
    $(".search-icon_mb").click(function () {
        $(".form-search__mb").addClass('active');
    });
    $(".close-form").click(function () {
        $(".form-search__mb").removeClass('active');
    });

    $(".bar").click(function () {
        $(".header-menu__mb").toggleClass('active');
    });

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

    $('.toggle').click(function () {
        $('.toggle').toggleClass('active');
        $(".overlay").toggleClass("active");
        $(".header-menu__mb").toggleClass("active");

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
        $(".form-model").addClass("active");
    });
    $(".close-model").click(function () {
        $(".form-model").removeClass("active");
    });

    $('ul.pagination li.PagedList-skipToNext a').html('<i class="fas fa-chevron-right"></i>');
    $('ul.pagination li.PagedList-skipToPrevious a').html('<i class="fas fa-chevron-left"></i>');
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
                $(".form-model").removeClass("active");
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
    $('.show-last').click(function () {
        $('.list-social').addClass('active');
        $('.show-last').hide();
    });
    $('.hiden-soci').click(function () {
        $('.list-social').removeClass('active');
        $('.show-last').show();

    });

    $(".remove-product").click(function () {
        const recordToDelete = $(this).attr("data-id");
        if (recordToDelete !== "") {
            Swal.fire({
                title: 'Bạn có muốn xóa sản phẩm?',
                text: "Sản phẩm sẽ bị xóa khỏi giỏ hàng",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Xóa'
            }).then((result) => {
                if (result.isConfirmed) {

                    $.post("/ShoppingCart/RemoveFromCart", { "id": recordToDelete }, function (data) {
                        if (data.ItemCount === 1) {
                            alert("Quá trình thực hiện không thành công");

                        } else {
                            $("tr[data-row='" + recordToDelete + "']").fadeOut();
                            function reload() {
                                location.reload();
                            }
                            setTimeout(reload, 500);
                            Swal.fire(
                                'Xóa thành công!',
                                'Sản phẩm đã bị xóa khỏi giỏ hàng',
                                'success'
                            );
                        }
                    });


                }
            });

        }
    });
    $("[data-item=city]").on("change", function (data) {
        const id = $(this).val();
        var items = [];
        items.push("<option value>Chọn Quận/Huyện</option>");

        if (id !== "") {
            $.getJSON("/Base/GetDistrict", { cityId: id }, function (data) {
                $.each(data, function (key, val) {
                    items.push("<option value='" + val.Id + "'>" + val.Name + "</option>");
                });
                $("[data-item=district]").html(items.join(""));
            });
        } else {
            $("[data-item=district]").html(items.join(""));
        }
    });
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

});

function landingPage() {
    $('.cause-disease-list').slick({
        infinite: true,
        slidesToShow: 4,
        slidesToScroll: 4,
        arrows: false,
        dots: true,
        autoplay: true,
        autoplaySpeed: 3000,
        prevArrow: "<button class='prev-btn'><i class='fas fa-chevron-left'></i></button>",
        nextArrow: "<button class='next-btn'><i class='fas fa-chevron-right'></i></button>",
        responsive: [
            {
                breakpoint: 992,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3,
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

    $('.feedback-landing-list').slick({
        infinite: true,
        slidesToShow: 3,
        slidesToScroll: 3,
        arrows: false,
        dots: true,
        autoplay: true,
        autoplaySpeed: 3000,
        responsive: [
            {
                breakpoint: 992,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3,
                }
            },
            {
                breakpoint: 769,
                settings: {
                    slidesToShow: 2,
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

    $(".support-form.landing-form").on("submit",
        function (e) {
            e.preventDefault();
            $.post("/Home/ContactProduct1",
                $(this).serialize(),
                function (data) {
                    if (data.status) {
                        $.toast({
                            heading: "Liên hệ thành công",
                            text: data.msg,
                            icon: "success"
                        });
                        $(".support-form.landing-form").trigger("reset");
                    } else {
                        $.toast({
                            heading: "Liên hệ không thành công",
                            text: data.msg,
                            icon: "error"
                        });
                    }
                });
        });

    $("#form-register-consultation").on("submit",
        function (e) {
            e.preventDefault();
            $.post("/Home/ContactProduct2",
                $(this).serialize(),
                function (data) {
                    if (data.status) {
                        $.toast({
                            heading: "Liên hệ thành công",
                            text: data.msg,
                            icon: "success"
                        });
                        $("#form-register-consultation").trigger("reset");
                    } else {
                        $.toast({
                            heading: "Liên hệ không thành công",
                            text: data.msg,
                            icon: "error"
                        });
                    }
                });
        });
}
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

function DetailJS() {
    $("#formBookProduct").on("submit", function (e) {
        e.preventDefault();
        $.post("/gio-hang/them-vao-gio-hang", $(this).serialize(), function (data) {
            if (data.result === 1) {
                $.toast({
                    text: "Thêm vào giỏ hàng thành công",
                    icon: "success",
                    position: "bottom-right"
                });
                $(".number-cart").text(data.count);
            } else {
                $.toast("Quá trình thực hiện không thành công");
            }
        });
    });
}
function addCart(n, m) {
    $.post("/gio-hang/them-vao-gio-hang", { productId: n }, function (n) {
        n.result === 1;
        console.log(n.CartTotal);
        $("#finalcart").text(n.CartTotal)
            ? ($.toast({
                text: "Thêm vào giỏ hàng thành công",
                icon: "success",
                position: "bottom-right",
            }),
                $(".number-cart").text(n.count))
            : $.toast({
                text: "Quá trình thực hiện không thành công",
                icon: "error",
                position: "bottom-right",
            });
    });
}
function UpdateToCard(id, changeValue) {
    $.ajax({
        type: "Post",
        url: "/ShoppingCart/UpdateCartV2", data: { productId: id, changeValue },
        success: function (res) {
            if (res) {
                $("[data-cart-item=" + id + "]").text(res.total);
                $("#finalTotal").html(res.totalMoneyString);
                $("#finalcart").html(res.totalMoneyString);
                $.toast({
                    heading: res.Msg,
                    position: "bottom-right",
                    icon: "success"
                });
            } else {
                $.toast({
                    heading: res.Msg,
                    position: "bottom-right",
                    icon: "error"
                });
            }
        }
    });
}