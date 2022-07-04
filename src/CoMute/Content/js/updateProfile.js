(function (root, $) {
    var emailData = $('#emailData').val();
    $.get('/api/user/get', { email: emailData }, function (data) {
        $("#Id").val(data.Id);
        $("#name").val(data.Name);
        $("#surname").val(data.Surname);
        $("#phone").val(data.PhoneNumber);
        $("#email").val(data.EmailAddress);
    }).fail(function (data) {
    });



    $('#updateProfile').on('submit', function (ev) {
        ev.preventDefault();

        var id = $('#Id').val();
        if (!id) {
            return;
        }

        var name = $('#name').val();
        if (!name) {
            return;
        }

        var surname = $('#surname').val();
        if (!surname) {
            return;
        }

        var phone = $('#phone').val();
        if (!phone) {
            return;
        }

        var email = $('#email').val();
        if (!email) {
            return;
        }

        var pswd = $('#password').val();
        if (!pswd) {
            return;
        }

        var cpswd = $('#confirm-password').val();
        if (!cpswd) {
            return;
        }

        $.post('/api/user/update', { id: id, name: name, surname: surname, phoneNumber: phone, emailAddress: email, password: pswd }, function (data) {
            window.location.href = '/Home/MainPool?email=' + email;
            // TODO: Navigate away...
        }).fail(function (data) {
            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text('update failed');
            $alert.removeClass('hidden');

            setTimeout(function () {
                $p.text('');
                $alert.addClass('hidden');
            }, 3000);
        });
    });
})(window, jQuery);