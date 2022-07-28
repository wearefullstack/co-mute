async function leaveCarpool(carpool_id) {
    await fetch("/leave_carpool", {
        method: "POST",
        body: JSON.stringify({ carpool_id: carpool_id }),
    })
    window.location.href = "/joined_carpools";

}

async function joinCarpool(carpool_id, available_seats) {
    await fetch("/join_carpool", {
        method: "POST",
        body: JSON.stringify({ carpool_id: carpool_id, available_seats: available_seats }),
    })
    window.location.href = "/search_all";
}
