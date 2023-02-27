; (function (root, $) {
    $(document).ready(function () {
        const isLoggedIn = localStorage.getItem('userGuid');

        if (isLoggedIn === null) {
          
            $('#username').text( localStorage.getItem('FirstName') + ' ' + localStorage.getItem('LastName'));

            $('.menu-item').each(function () {
                const menuItemValue = $(this).data('value'); // assuming you have a data attribute 'data-value' on each menu item
                if ((menuItemValue === 'item-profile' || menuItemValue === 'item-search' )) { // replace 'someMenuItemValue' with the value of the menu item you want to disable
                    $(this).hide();
                }

            });
        }
        else {
            $('.menu-item').each(function () {
                const menuItemValue = $(this).data('value');
            if ( (menuItemValue === 'item-register')) { // replace 'someMenuItemValue' with the value of the menu item you want to disable
                $(this).hide();
                }
            });
        }
        
    });



})(window, jQuery);