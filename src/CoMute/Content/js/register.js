(function (root, $) {
    $('#register').on('submit', function (ev) {
        ev.preventDefault();

        var name = $('#name').val();
        if (!name) {
            return;
        }

        var surname = $('#surname').val();
        if (!surname) {
            return;
        }

        var phone = $('#phone').val();
        //if (!phone) {
        //    return;
        //}

        var email = $('#email').val();
        if (!email) {
            return;
        }

        var pswd = $('#password').val();
        if (!pswd) {
            return;
        }

        var cpswd = $('#confirm-password').val();
        console.log(name);
        console.log(surname);
        console.log(email);
        console.log(pswd);
        console.log(cpswd);
        console.log(pswd != cpswd);

        if (pswd != cpswd) {
            return;
        }

        var data = { name: name, surname: surname, phoneNumber: phone, emailAddress: email, password: pswd };
        $.post('/api/user', data, function (op) {
            // TODO: Navigate away...
            window.location.href = "/home/login";
        }).fail(function (data) {
            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text('Registration failed');
            $alert.removeClass('hidden');

            setTimeout(function () {
                $p.text('');
                $alert.addClass('hidden');
            }, 3000);
        });
    });

    $('#name').val('Edima');
    $('#surname').val('Inyang');
    $('#email').val('wumi.inyang@gmail.com');
    $('#password').val('abcd1234');
    $('#confirm-password').val('abcd1234');


})(window, jQuery);