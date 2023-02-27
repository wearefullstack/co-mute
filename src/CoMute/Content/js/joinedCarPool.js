;
(function(root, $) {

    $(document).ready(function() {


        var userGuid = localStorage.getItem('userGuid');
        if (isLoggedIn == null) {
            window.location = '../home/index';
        }

        $.post('/api/carpool/GetJoinedCarpools/' + userGuid, null, function(data) {
            // TODO: Navigate away...
            var response = data

            var table = $('#tblCarpools').DataTable({
                "data": response,
                "responsive": true,
                "dom": 'frtip',
                "columnDefs": [{
                        "targets": -1,
                        "data": null,
                        "defaultContent": "<input id='btnDetails' class='btn btn-success' width='25px' value='Leave' />"
                    }

                ],
                "columns": [
                    /*{ "data": "UserGuid" },*/
                    {
                        "data": "Origin"
                    },
                    {
                        "data": "Destination"
                    },
                    {
                        "data": "DepartureTime"
                    },
                    {
                        "data": "ExpectedArrivalTime"
                    },
                    {
                        "data": "AvailableSeats"
                    },
                    {
                        "data": "Notes"
                    },
                    //{ "data": "Notes" },
                    {
                        "data": "Status"
                    },
                    {
                        "data": "JoindDate"
                    },
                    {
                        "data": "Action"
                    },

                ]

            });

            $('#tblCarpools tbody').on('click', '[id*=btnDetails]', function() {
                var data = table.row($(this).parents('tr')).data();
                var _carPoolGuid = data['CarPoolGuid'];
                //Join Pool
                $.post('/api/carpool/leave/' + _carPoolGuid + '/' + userGuid, null, function(data) {
                    // TODO: Navigate away...
                    var response = data
                }).fail(function(data) {
                    var $alert = $("#error");
                    var $p = $alert.find("p");
                    $p.text('Registration failed');
                    $alert.removeClass('hidden');

                    setTimeout(function() {
                        $p.text('');
                        $alert.addClass('hidden');
                    }, 3000);
                });
            });

        }).fail(function(data) {
            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text('Registration failed');
            $alert.removeClass('hidden');

            setTimeout(function() {
                $p.text('');
                $alert.addClass('hidden');
            }, 3000);
        });

        var userObject = localStorage.getItem('userObject');

    });


    $('#addcarpool').on('submit', function(ev) {
        ev.preventDefault();

        var origin = $('#origin').val();
        if (!origin) {
            return;
        }

        var destination = $('#destination').val();
        if (!destination) {
            return;
        }

        var departureTime = $('#departureTime').val();
        if (!departureTime) {
            return;
        }

        var expectedArrivaltime = $('#expectedArrivaltime').val();
        if (!expectedArrivaltime) {
            return;
        }

        var availableSeats = $('#availableSeats').val();
        if (!availableSeats) {
            return;
        }

        var notes = $('#notes').val();
        if (!notes) {
            return;
        }
        var userGuid = localStorage.getItem('userGuid').toString();
        var token = localStorage.getItem('token').toString();
        $.post('/api/addcarpool', {
            origin: origin,
            destination: destination,
            departureTime: departureTime,
            expectedArrivaltime: expectedArrivaltime,
            availableSeats: availableSeats,
            notes: notes,
            UserGuid: userGuid
        }, function(data) {

        }).fail(function(data) {
            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text('Registration failed');
            $alert.removeClass('hidden');

            setTimeout(function() {
                $p.text('');
                $alert.addClass('hidden');
            }, 3000);
        });
    });

    $('#availablecarpools').on('submit', function(ev) {
        ev.preventDefault();

        var origin = $('#origin').val();
        if (!origin) {
            return;
        }

        var destination = $('#destination').val();
        if (!destination) {
            return;
        }

        var departureTime = $('#departureTime').val();
        if (!departureTime) {
            return;
        }

        var expectedArrivaltime = $('#expectedArrivaltime').val();
        if (!expectedArrivaltime) {
            return;
        }

        var pswd = $('#password').val();
        if (!pswd) {
            return;
        }

        var cpswd = $('#confirm-password').val();
        if (!email) {
            return;
        }

        $.get('/api/addcarpool', {
            Destination: Destination,
            surname: surname,
            phoneNumber: phone,
            emailAddress: email,
            password: pswd
        }, function(data) {
            // TODO: Navigate away...
        }).fail(function(data) {
            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text('Registration failed');
            $alert.removeClass('hidden');

            setTimeout(function() {
                $p.text('');
                $alert.addClass('hidden');
            }, 3000);
        });
    });

    $('#searchcarpool').on('submit', function(ev) {
        ev.preventDefault();

        var origin = $('#origin').val();
        if (!origin) {
            return;
        }

        var destination = $('#destination').val();
        if (!destination) {
            return;
        }

        var userGuid = localStorage.getItem('userGuid').toString();
        var token = localStorage.getItem('token').toString();
        $.post('/api/searchPool', {
            origin: origin,
            destination: destination,
            UserGuid: userGuid
        }, function(data) {
            // TODO: Navigate away...

        }).fail(function(data) {
            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text('Registration failed');
            $alert.removeClass('hidden');

            setTimeout(function() {
                $p.text('');
                $alert.addClass('hidden');
            }, 3000);
        });
    });
})(window, jQuery);