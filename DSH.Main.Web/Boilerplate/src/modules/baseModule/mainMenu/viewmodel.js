define(['Boiler'], function (Boiler) {
    var viewModel = function (moduleContext) {
        var self = this;
        self.noOfNotifications = ko.observable();
        self.noOfFeedbacks = ko.observable();
        self.context = moduleContext;

        moduleContext.listen("NEWSFEED_NOTIFICATIONS", function (count) {
            if (count == 0) { }
            else {
                self.noOfNotifications(count);
            }
        });

        moduleContext.listen("FEEDBACK_NOTIFICATIONS", function (count) {
            if (count == 0) { }
            else {
                self.noOfFeedbacks(count);
            }
        });

        moduleContext.listen("NOTIFICATIONS_READ", function () {
            self.noOfNotifications('');
            self.noOfFeedbacks('');
        });

    }
    return viewModel;
});

