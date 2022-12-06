// Write your JavaScript code.

showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');
        }
    })
};

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $("#view-all").html(res.html);
                    $("#form-modal .modal-body").html('');
                    $("#form-modal .modal-title").html('');
                    $("#form-modal").modal('hide');
                    $.notify("submitted successfully", { globalPosition: 'top-center', className: 'success' });
                }
                else
                    $("#form-modal .modal-body").html(res.html);
            },
            error: function (err) {
                console.log(err);
            }
        })
    } catch (e) {
        console.log(e)
    }
    //to prevent default form submit event
    return false;
}

jQueryAjaxDelete = form => {
    if (confirm("Are you sure you want to delete this record?")) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {
                        $("#view-all").html(res.html);
                        $.notify("deleted successfully", { globalPosition: 'top-center', className: 'success' });
                    }

                },
                error: function (err) {
                    console.log(err);
                }
            })

        } catch (e) {
            console.log(e);
        }
    }
    //to prevent default form submit event
    return false;
}