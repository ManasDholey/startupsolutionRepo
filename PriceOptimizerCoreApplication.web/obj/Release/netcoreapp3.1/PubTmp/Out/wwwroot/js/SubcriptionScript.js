/// <reference path="notify.min.js" />
var pathname = window.location.pathname; // Returns path only (/path/example.html)
var url = window.location.href;
var origin = window.location.origin;
jQueryAjaxPostSubscribe = formSubscribe => {
    try {
        //if (form.valid()) {
        $.ajax({
            type: 'POST',
            url: formSubscribe.action,
            data: new FormData(formSubscribe),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.status) {
                    $.notify(res.message, "success");
                    //alert(res.message);
                }
                else
                    $.notify(res.message, "error");
                    //alert(res.message);
            },
            error: function (err) {
                console.log(err)
            }
        });
        //}
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}