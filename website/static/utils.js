async function leaveCarpool(carpoolId) {
    await fetch("/leave_carpool", {
        method: "POST",
        body: JSON.stringify({ carpoolId: carpoolId }),
    })
    window.location.href = "/joined_carpools";

}