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
        if (!email) {
            return;
        }

        $.post('/api/User/CreateUser', { name: name, surname: surname, phoneNumber: phone, emailAddress: email, password: pswd }, function (data) {
            alert("User: " + data.name + " created!!!");
            window.location.replace('/home');
        })
        .fail(function (data) {
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
})(window, jQuery);