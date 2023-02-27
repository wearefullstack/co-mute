; (function (root, $) {
    $(document).ready(function () {
        

        var userGuid = localStorage.getItem('userGuid');
        if (userGuid == null) {
            window.location = '../home/index';
        }

        $.get('/api/profile/' + userGuid, { userGuid: userGuid }, function (data) {
            // TODO: Navigate away...
            var response = data

            $("#name").val(data.FirstName);
            $("#surname").val(data.LastName);
            $("#email").val(data.Email);
            $("#phone").val(data.Phone);

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

        var userObject = localStorage.getItem('userObject');
        
    });

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

        if (cpwd !== pswd) {
            alert('Password and Confirm Password must match.');
            return
        }

        $.post('/api/user', { name: name, surname: surname, phoneNumber: phone, emailAddress: email, password: pswd }, function (data) {
            var url = 'CarPool/AvailableCarPools'; //'api/PresentationImpersonateTo/'  //+ id;
            window.location = url;
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

        
        var userGuid = localStorage.getItem('userGuid');

        $.post('/api/UpdateUser', { name: name, surname: surname, phoneNumber: phone, emailAddress: email, password: pswd, UserGuid: userGuid  }, function (data) {
            // TODO: Navigate away...

            var alertElement = $('<div class="alert alert-success">Update is Successful</div>');

            // Add the alert element to the DOM
            $('#userProfile').append(alertElement);
            setTimeout(function () {
                alertElement.remove();
            }, 000);

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

})(window, jQuery);