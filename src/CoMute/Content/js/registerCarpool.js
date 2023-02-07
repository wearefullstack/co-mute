; (function (root, $) {
    $('#registerCarpool').on('submit', function (ev) {
        ev.preventDefault();

        var departureTime = $('#departureTime').val();
        if (!departureTime) {
            return;
        }

        var expectedArrivalTime = $('#expectedArrivalTime').val();
        if (!expectedArrivalTime) {
            return;
        }

        var origin = $('#origin').val();
        if (!origin) {
            return;
        }

        var daysAvailable = $('#daysAvailable').val();
        if (!daysAvailable) {
            return;
        }

        var destination = $('#destination').val();
        if (!destination) {
            return;
        }

        var availableSeats = $('#availableSeats').val();
        if (!availableSeats) {
            return;
        }

        var ownerLeader = $('#ownerLeader').val();
        if (!ownerLeader) {
            return;
        }

        var notes = $('#notes').val();
        if (!notes) {
            return;
        }

        $.post('/api/carpool', { departureTime: departureTime, expectedArrivalTime: expectedArrivalTime, origin: origin, daysAvailable: daysAvailable, destination: destination, availableSeats: availableSeats, ownerLeader: ownerLeader, notes: notes }, function (data) {
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