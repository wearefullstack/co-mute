
$(function () {
    console.log(JSON.stringify({ zweli: 'NKuna', Name: 'zweli' }));
    $('#createCarPool').on('submit', function (ev) {
        ev.preventDefault();


        var origin = $('#origin').val();
        if (!origin) {
            return;
        }

        var departureTime = $('#departureTime').val();
        if (!departureTime) {
            return;
        }
        var arivalTime = $('#arivalTime').val();
        if (!arivalTime) {
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

        var days_available = $('#days_available').val();
        if (!days_available) {
            return;
        }

       

        $.ajax({
            url: '/api/carpool',
            type: 'post',
            headers: headers,
            data: `{\"Notes\":\"${notes}\",\"DepartureTime\":\"${departureTime}\",\"ExpectArivalTime\":\"${arivalTime}\",\"Origin\":\"${origin}\",\"Destination\":\"${destination}\",\"DaysAvailable\":${days_available},\"AvailableSeats\":${seats}`,
            success: function (data) {
                window.location.replace('/');
            },
            fail: function (data) {
                var $alert = $("#error");
                var $p = $alert.find("p");
                $p.text('failed');
                $alert.removeClass('hidden');

                setTimeout(function () {
                    $p.text('');
                    $alert.addClass('hidden');
                }, 3000);
            }
        });
    });
});

