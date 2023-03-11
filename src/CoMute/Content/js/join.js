$('.linkJoin').click(function (e) {
    e.preventDefault();
    
    var theLink = $(this).attr('href');
    var thePartsOfLink = theLink.split('/');
    var theId = thePartsOfLink[thePartsOfLink.length - 1];
    var id = parseInt(theId);

    $.post('/api/Join', { id : id }, function (data) {
        var $alert = $('#success');
        var $p = $alert.find("p");
        $p.text('Join successful');
        $alert.removeClass('hidden');

        window.scrollTo(0, 0);
        setTimeout(function () {
            $p.text('');
            $alert.addClass('hidden');
            window.location.href = '/search/viewcarpool';
        }, 3000);

    }).fail(function (data) {
        var $alert = $("#error");
        var $p = $alert.find("p");
        $p.text('Failed to Join');
        $alert.removeClass('hidden');

        setTimeout(function () {
            $p.text('');
            $alert.addClass('hidden');
        }, 3000);
    });
});