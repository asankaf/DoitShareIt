define(['Boiler'], function (Boiler) {

    var viewModel = function (moduleContext) {
        var self = this;
        self.checkNotifications = function () {
            $.ajax({
                type: "GET",
                url: "/NotificationChecking",
                success: function (result) {
                    self.checkNotifications();
                    //moduleContext.notify('NEW_POST', p);
                    //moduleContext.notify('NEW_COMMENT', c);
                }
            });
        };
        
        self.checkNotifications();
    };

    return viewModel;
});


 
