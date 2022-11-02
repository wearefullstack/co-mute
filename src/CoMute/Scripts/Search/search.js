$(document).ready(function () {
    $("#availableDays").select2();
    LoadAll();
});

$("#origin").keyup(function () {
    var origin = $("#origin").val();
    var destination = $("#destination").val();
    Filter(origin, destination);
});

$("#destination").keyup(function () {
    var origin = $("#origin").val();
    var destination = $("#destination").val();
    Filter(origin, destination);
})

function Filter(origin, destination) {
    $.ajax({
        type: 'GET',
        url: '/Search/Filter',
        data: {
            origin: origin,
            destination: destination
        },
        beforeSend: function (data) {
            $('#carpools').html(SpinnerLoadingBody);
        },
        success: function (data) {
            $('#carpools').html(data);
        },
        error: function (xhr, status, error) {
            alert("Error");
            console.log(error);
        }
    });
}

function LoadAll() {
    $.ajax({
        type: 'GET',
        url: '/Search/Carpools',
        data: {},
        beforeSend: function (data) {
            $('#carpools').html(SpinnerLoadingBody);
        },
        success: function (data) {
            $('#carpools').html(data);
        },
        error: function (xhr, status, error) {
            alert("Error");
            console.log(error);
        }
    });
}