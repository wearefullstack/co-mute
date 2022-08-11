; (function (root, $) {
    $('#carpool').on('submit', function (ev) {
        ev.preventDefault();

        var departureTime = $('#departure-time').val();
        if (!departureTime) {
            return;
        }

        var expectedArrivalTime = $('#expected-arrival-time').val();
        if (!expectedArrivalTime) {
            return;
        }

        var origin = $('#origin').val();
        if (!origin) {
            return;
        }

        var daysAvailable = $('#days-available').val();
        if (!daysAvailable) {
            return;
        }

        var destination = $('#destination').val();
        if (!destination) {
            return;
        }

        var availableSeats = $('#available-seats').val();
        if (!availableSeats) {
            return;
        }

        var notes = $('#notes').val();

        $.post('/api/carpoolopportunity/add', {
            departureTime: departureTime,
            expectedArrivalTime: expectedArrivalTime,
            origin: origin,
            daysAvailable: daysAvailable,
            destination: destination,
            availableSeats: availableSeats,
            notes: notes
        }, function (data) {
            var redirectUrl = '/';
            window.location.href = redirectUrl;
        }).fail(function (data) {
            displayError($, 'could not create a pool opportunity.');
        });
    });
})(window, jQuery);

function displayError($, message) {
    var $alert = $("#error");
    var $p = $alert.find("p");
    $p.text();
    $alert.removeClass('hidden');

    setTimeout(function () {
        $p.text(message);
        $alert.addClass('hidden');
    }, 3000);
}
