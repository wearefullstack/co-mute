; (function (root, $) {
    $('#register').on('submit', function (ev) {
        ev.preventDefault();

        var name = $('#name').val();
        if (name==null) {
            return;
        }

        var surname = $('#surname').val();
        if (surname == null) {
            return;
        }

        var phone = $('#phone').val();
        if (phone == null) {
            return;
        }

        var email = $('#email').val();
        if (email == null) {
            return;
        }

        var pswd = $('#password').val();
        if (pswd == null) {
            return;
        }

        var cpswd = $('#confirm-password').val();
        if (cpswd == null && cpswd != pswd) {
            return;
        }

        $.post('/api/user', { name: name, surname: surname, phoneNumber: phone, emailAddress: email, password: pswd }, function (data) {
            var $alert = $('#success');
            var $p = $alert.find("p");
            $p.text('Registration Successful');
            $alert.removeClass('hidden');

            window.scrollTo(0, 0);            

            setTimeout(function () {
                $p.text('');
                $alert.addClass('hidden');
                window.location.href = '/home/index';
            }, 3000);
            
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
})(window, jQuery);