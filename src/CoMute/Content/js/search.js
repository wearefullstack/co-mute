$(document).ready(function () {
    $('#searchTerm').on('keyup', function () {
        var value = $(this).val();
        $('#viewOfCarpool tr').filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});