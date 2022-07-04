(function (root, $) {

    $('#createPool').on('submit', function (ev) {
        ev.preventDefault();
        var emailData = $('#emailData').val();

        var DepartureTime = $('#DepartureTime').val();
        if (!DepartureTime) {
            return;
        }

        var ArrivalTime = $('#ArrivalTime').val();
        if (!ArrivalTime) {
            return;
        }

        var Origin = $('#Origin').val();
        if (!Origin) {
            return;
        }

        var DaysAvailable = $('#DaysAvailable').val();
        if (!DaysAvailable) {
            return;
        }

        var Destination = $('#Destination').val();
        if (!Destination) {
            return;
        }

        var AvailableSeats = $('#AvailableSeats').val();
        if (!AvailableSeats) {
            return;
        }

        var Notes = $('#Notes').val();
        if (!Notes) {
            return;
        }



        $.post('/api/pool/post', { DepartureTime: DepartureTime, ArrivalTime: ArrivalTime, Origin: Origin, DaysAvailable: DaysAvailable, Destination: Destination, AvailableSeats: AvailableSeats, Notes: Notes, Owner: 1 }, function (data) {
            window.location.href = '/Home/MainPool?email=' + emailData;
            // TODO: Navigate away...
        }).fail(function (data) {
            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text('create pool failed');
            $alert.removeClass('hidden');

            setTimeout(function () {
                $p.text('');
                $alert.addClass('hidden');
            }, 3000);
        });
    });



})(window, jQuery);