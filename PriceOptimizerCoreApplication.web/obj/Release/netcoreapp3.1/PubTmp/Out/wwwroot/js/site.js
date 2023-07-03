///<reference path="notify.min.js" />
///<reference path="js.cookie.min.js" />
var pathname = window.location.pathname; // Returns path only (/path/example.html)
var url = window.location.href;     
var origin = window.location.origin;
(function ($) {
    var showLoder = function () {
        setTimeout(function () {
            if ($('#ftco-loader').length > 0) {
                $('#ftco-loader').addClass('show');
            }
        }, 1);
    };
    //Hide loder 
    var loaderHide = function () {
        setTimeout(function () {
            if ($('#ftco-loader').length > 0) {
                $('#ftco-loader').removeClass('show');
            }
        }, 1000);
    };
    jQueryAjaxPost = form => {
        try {
            //if (form.valid()) {
            showLoder();
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.status) {
                        //alert(res.message);
                        $.notify(res.message, "success");
                       // form.trigger("reset");
                        $("#myForm").trigger("reset");
                        //console.log(form);
                    }
                    else {
                        $("#htmlFooter").html(res.Html);
                        $.notify(res.message, "error");
                    }
                    loaderHide();        //alert(res.message);
                },
                error: function (err) {
                    console.log(err)
                    loaderHide();
                }
            });
            // loaderHide();
            //}
            //to prevent default form submit event
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }
    jQueryAjaxPostService = form => {
        var body = $(".modal-body");
        var exampleModal = $("#exampleModal");
        var exampleModalLabel = $("#exampleModalLabel");
        $('.submit-form').prop('disabled', true);
        try {
            //if (form.valid()) {
            showLoder();
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    exampleModal.modal({
                        backdrop: 'static',
                        keyboard: false
                    });
                    if (res.status) {
                        //alert(res.message);
                        $.notify(res.message, "success");
                    }
                    else {
                        
                        body.html("");
                        body.html(res.html);
                        $.notify(res.message, "error");
                    }
                    $('.submit-form').prop('disabled', false);
                    loaderHide();        //alert(res.message);
                },
                error: function (err) {
                    console.log(err)
                    loaderHide();
                }
            });
            // loaderHide();
            //}
            //to prevent default form submit event
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }

}(jQuery));
// show loder

////.AspNetCore.Antiforgery.7QB6-caK5CU
//function jQueryAjaxPost(that) {
//    var form = $('#myForm');
//    var dataFrom = form.serialize();
//    var postUrl = "//";
//    var cookie_email = Cookies.get('.$.cookieAspNetCore.Antiforgery.7QB6-caK5CU');
//    console.log(cookie_email);
//    //$.post(url, dataFrom, function (result) {
//    //    var report = JSON.parse(JSON.stringify(result));

//    //    if (report.status) {
//    //        swal("<span class=fa fa-thumbs-up", report.message, "success");
//    //        setInterval(function () { window.location.reload(true); }, 5000);
//    //    }
//    //    else {
//    //        swal("<span class=fa fa-thumbs-down", report.message, "error");
//    //    }
//    //});
//    console.log(cookie_email);
//    console.log(JSON.stringify(dataFrom));
//    $.ajax({
//        type: 'POST',
//        url: '/api/SendEmailApi/Send',
//        data: JSON.stringify(dataFrom),
//        contentType: false,
//        processData: false,
//        headers: {
//            'Authorization': 'RequestVerificationToken' + cookie_email
//        },
//        success: function (res) {
//            if (res.status) {
//                alert(res.message);
//            }
//            else
//                alert(res.message);
//            // var pathnamearr = pathname.split('/');
//            // var controlar = form.action.split('/');
//            // alert(res.message);
//            //if (pathnamearr[1] == controlar[1] ) {
//            //    if (res.status) {
//            //        alert(res.message);
//            //    }
//            //    else
//            //        alert(res.message);
//            //}
//            //else {
//            //    window.location.href = url;
//            //}

//        },
//        error: function (data) {
//            console.log(data.responseText);
//            console.log(data);
//        } //End of AJAX error function  


//    });
//}
$('#carouselExampleCaptions').on('slide.bs.carousel', function onSlide(ev) {
    var id = ev.relatedTarget.id;
    console.log(id);
    switch (id) {
        case "1":
            // do something the id is 1
            break;
        case "2":
            // do something the id is 2
            break;
        case "3":
            // do something the id is 3
            break;
        default:
        //the id is none of the above
    }
});
$('.carousel').carousel({
    interval: 12000
});


