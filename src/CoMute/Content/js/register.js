; (function (root, $) {
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

        if (cpswd !== pswd) {
            alert('Password and Confirm Password must match.');
            return
        }

        $.post('/api/user', { name: name, surname: surname, phoneNumber: phone, emailAddress: email, password: pswd }, function (data) {
            // TODO: Navigate away...

            var url = '/'; //'api/PresentationImpersonateTo/'  //+ id;
            window.location = url;
           // window.location.href = url;
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


    $('#userProfile').on('submit', function (ev) {
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

        var userGuid = localStorage.getItem('userGuid').toString();

        $.post('/api/UpdateUser', { name: name, surname: surname, phoneNumber: phone, emailAddress: email, password: pswd, userGuid: userGuid }, function (data) {
            // TODO: Navigate away...

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

    $('#getUser').on('submit', function (ev) {
        ev.preventDefault();

        
        var userGuid = localStorage.getItem('userGuid').toString();

        $.get('/api/GetUserProfile', { userGuid: userGuid }, function (data) {

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