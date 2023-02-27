;
(function (root, $) {
    $(document).ready(function () {
        const isLoggedIn = localStorage.getItem('userGuid');

        if (isLoggedIn != null) {
            //Go to dashboard
            var url = 'CarPool/JoinedCarPools';
            window.location = url;
        }
    });
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

        $.post('/api/Authentication', {
            email: email,
            password: pswd
        }, function (data) {
            // TODO: Navigate away...
            var response = data;
            if (response.IsResult == false) {
                localStorage.clear;
                var alertElement = $('<div class="alert alert-danger">Incorrect email and password combination!</div>');

                $('#login').append(alertElement);
                setTimeout(function () {
                    alertElement.remove();
                }, 3000);
            } else {
                localStorage.setItem('token', data.JwtToken);
                localStorage.setItem('userGuid', data.UserGuid);
                localStorage.setItem('FirstName', data.FirstName);
                localStorage.setItem('LastName', data.LastName);
                sessionStorage.setItem('userGuid', data.UserGuid);

                var url = 'CarPool/JoinedCarPools'; //'CarPool/AvailableCarPools';
                window.location = url;
            }

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