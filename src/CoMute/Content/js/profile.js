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

            }).fail(function (data) {

            });
        }
    });
});