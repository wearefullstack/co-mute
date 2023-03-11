$('#create').click(function (e) {
    e.preventDefault();

    var origin = $('#Origin').val();
    var destination = $('#Destination').val();
    var daysAvailable = $('#DaysAvailable').val();
    var availableSeats = $('#AvailableSeats').val();
    var departureTime = $('#DepartureTime').val();
    var expectedTime = $('#ExpectedTime').val();
    var notes = $('#Notes').val();

    $.post('api/Create', { origin: origin, destination: destination, daysAvailable: daysAvailable, availableSeats: availableSeats, departureTime: departureTime, expectedTime: expectedTime, theNotes: notes }, function (data) {
        var $alert = $('#success');
        var $p = $alert.find("p");
        $p.text('Carpool creation Successful');
        $alert.removeClass('hidden');

        window.scrollTo(0, 0);

        setTimeout(function () {
            $p.text('');
            $alert.addClass('hidden');
            window.location.href = '/search/ViewCarpool';
        }, 3000);
    }).fail(function (data) {
        var $alert = $("#error");
        var $p = $alert.find("p");
        $p.text('Carpool creation Failed');
        $alert.removeClass('hidden');

        setTimeout(function () {
            $p.text('');
            $alert.addClass('hidden');
        }, 3000);
    });
});