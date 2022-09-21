
$(function () {

    $.ajax({
        url: '/api/carpool',
        type: 'get',
        headers: headers,
        success: function (data) {
            $('#item_body').html('');
            if (data.length == 0) $('#item_body').append(`<h2><span class="text-danger"> No car pools created yet!</span></h2>`);
            else data.forEach(AddCarpoo);
        }
    });

});

function AddCarpoo(carpool) {
    console.log(carpool);
    $('#item_body').append(`            <hr />
            <div class="row">
                <div class="col-md-2">${carpool.Origin} </div>
                <div class="col-md-2">${carpool.Destination}</div>
                <div class="col-md-1">${getTime(carpool.DepartureTime)}</div >
                <div class="col-md-1">${getTime(carpool.ExpectArivalTime)}</div>
                <div class="col-md-1">${carpool.AvailableSeats}</div>
                <div class="col-md-1">${carpool.DaysAvailable}</div>
                <div class="col-md-2"><small>${carpool.Notes}</small></div>
                <div class="col-md-2">${carpool.PoolCreationDate}</div>
            </div>`);
   
}

function getTime(datetime) {
    let date = new Date(datetime);
    return date.toLocaleTimeString();
}