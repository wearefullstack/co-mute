// Add new Ride Opportunity via Controller to DB
; (function (root, $) {
    $('#ridesCreate').on('submit', function (ev) {
        ev.preventDefault();

        var departure = $('#departure').val();
        if (!departure) {
            return;
        }

        var arrival = $('#arrival').val();
        if (!arrival) {
            return;
        }

        var origin = $('#origin').val();
        if (!origin) {
            return;
        }

        var destination = $('#destination').val();
        if (!destination) {
            return;
        }

        var seats = $('#seats').val();
        if (!seats) {
            return;
        }

        var notes = $('#notes').val();
        if (!notes) {
            return;
        }

        $.post('/api/ride', { departureTime: departure, arrivalTime: arrival, Origin: origin, Destination: destination, AvailableSeats: seats, Notes: notes }, function (data) {

            // TODO: Navigate away...
            alert("Create ride successful"); // All details accepted

        }).fail(function (data) { // API fail
            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text('Create ride failed');
            $alert.removeClass('hidden');

            setTimeout(function () {
                $p.text('');
                $alert.addClass('hidden');
            }, 3000);
        });
    });
})(window, jQuery);