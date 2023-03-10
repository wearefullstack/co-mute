$(document).ready(function () {
    $('#theNameEditor').children().prop('disabled', true);
    $('#theSurnameEditor').children().prop('disabled', true);
    $('#thePhoneNumberEditor').children().prop('disabled', true);
    $('#theEmailAddressEditor').children().prop('disabled', true);
    $('#thePasswordEditor').children().prop('disabled', true);

    var isDisbaled = true;

    $('#allowEdit').click(function () {
        isDisbaled = $('#theNameEditor').children().prop('disabled');
        if (isDisbaled) {
            $('#theNameEditor').children().prop('disabled', false);
            $('#theSurnameEditor').children().prop('disabled', false);
            $('#thePhoneNumberEditor').children().prop('disabled', false);
            $('#theEmailAddressEditor').children().prop('disabled', false);
            $('#thePasswordEditor').children().prop('disabled', false);
        }
        else {
            $('#theNameEditor').children().prop('disabled', true);
            $('#theSurnameEditor').children().prop('disabled', true);
            $('#thePhoneNumberEditor').children().prop('disabled', true);
            $('#theEmailAddressEditor').children().prop('disabled', true);
            $('#thePasswordEditor').children().prop('disabled', true);
        }
    });

    $('#saveChanges').click(function () {
        isDisbaled = $('#theNameEditor').children().prop('disabled');
        if (!isDisbaled) {
            var name = $('#Name').val();
            var surname = $('#Surname').val();
            var phone = $('#PhoneNumber').val();
            var email = $('#EmailAddress').val();
            var pswd = $('#Password').val();

            $.post('/api/profile', { name: name, surname: surname, phoneNumber: phone, emailAddress: email, password: pswd }, function (data) {
                var $alert = $('#success');
                var $p = $alert.find("p");
                $p.text('Profile Update Successful');
                $alert.removeClass('hidden');

                window.scrollTo(0, 0);

                setTimeout(function () {
                    $p.text('');
                    $alert.addClass('hiddne');
                    location.reload();;
                }, 3000);
            }).fail(function (data) {
                var $alert = $("#error");
                var $p = $alert.find("p");
                $p.text('Update failed');
                $alert.removeClass('hidden');

                setTimeout(function () {
                    $p.text('');
                    $alert.addClass('hidden');
                }, 3000);
            });
        }
    });
});