; (function (root, $) {
    $('#login').on('submit', function (ev) {
        ev.preventDefault();
        var email = $('#inputEmail').val();
        if (!email) {
            return;
        }

        var pswd = $('#inputPassword').val();
        if (!pswd) {
            return;
        }

        $.post('/api/Authentication', { email: email, password: pswd }, function (data) {
            // TODO: Navigate away...
        }).fail(function (data) {
            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text('Incorrect email and password combination');
            $alert.removeClass('hidden');

            setTimeout(function () {
                $p.text('');
                $alert.addClass('hidden');
            }, 3000);
        });
    });
})(window, jQuery);