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

        if (cpswd !== pswd) {
            displayError($, 'Password and confirm password are not the same!');
            return;
        }

        $.post('/api/user/add', { name: name, surname: surname, phoneNumber: phone, emailAddress: email, password: pswd }, function (data) {
            var redirectUrl = '/';
            window.location.href = redirectUrl;
        }).fail(function (data) {
            console.log(data);
            displayError($, 'Registration failed, ' + data.responseJSON);
        });
    });
})(window, jQuery);

function displayError($, message) {
    var $alert = $("#error");
    var $p = $alert.find("p");
    $p.text();
    $alert.removeClass('hidden');

    setTimeout(function() {
        $p.text(message);
        $alert.addClass('hidden');
    }, 3000);
}
