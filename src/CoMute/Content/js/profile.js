let state = {
    profile: {}
};

$(function () {

    $.ajax({
        url: '/api/user',
        type: 'get',
        headers: headers,
        success: function (data) {
            populateProfile(data);
            state.profile = data;
        }
    });

    $('#profile').on('submit', function (ev) {
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

        $.post('/api/user/updateprofile', { name: name, surname: surname, phoneNumber: phone, emailAddress: email, userId: $('#userId').val() }, function (data) {
        }).fail(function (data) {
            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text('profile update failed');
            $alert.removeClass('hidden');

            setTimeout(function () {
                $p.text('');
                $alert.addClass('hidden');
            }, 3000);
        });
    });

});

function populateProfile(profile) {
    $('#name').val(profile.Name);
    $('#surname').val(profile.Surname);
    $('#phone').val(profile.PhoneNumber);
    $('#userId').val(profile.UserId);
    $('#email').val(profile.EmailAddress);
}