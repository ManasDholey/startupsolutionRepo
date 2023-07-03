/// <reference path="notify.min.js" />
var btnSendMessage = $("#btnSendMessage");
(function ($) {
    // show loder
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
    jQueryAjaxPostContactUs = formContactUs => {
        try {
            showLoder();
            $.ajax({
                type: 'POST',
                url: formContactUs.action,
                data: new FormData(formContactUs),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.status) {
                        $.notify(res.message, "success");
                        //formContactUs.trigger("reset");
                        $('.contactForm').trigger("reset");
                    }
                    else {
                        $("#send-sms-div").html(res.Html);
                        $.notify(res.message, "error");
                    }
                    loaderHide();
                },
                error: function (err) {
                    console.log(err);
                    loaderHide();
                }
            });
            return false;
        } catch (ex) {
            console.log(ex);
            loaderHide();
        }
    }  

}(jQuery));


