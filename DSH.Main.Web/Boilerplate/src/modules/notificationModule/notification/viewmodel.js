define(['Boiler', '../../Models/Notification'], function (Boiler, Notification) {

    var viewModel = function (moduleContext) {
        var self = this;
        self.noOfNotifications = ko.observable();
        self.context = moduleContext;
        self.notifications = ko.observableArray();
        self.checkNotifications = function () {
            $.ajax({
                type: "GET",
                url: "/NotificationChecking",
                success: function (result) {
                    if (result.Status == "SUCCESS") {
                        var n = result.Result.Data;
                        var feedbackCount = 0;
                        self.noOfNotifications(n.length);
                        self.notifications([]);
                        for (var i = 0; i < n.length; i++) {
                            var temp = new Notification(self.context);
                            temp.id(n[i].Id);
                            temp.senderId(n[i].SenderId);
                            temp.recipientId(n[i].RecipientId);
                            temp.body(n[i].Body);
                            temp.relevantPostId(n[i].RelevantPostId);
                            temp.relevantParentPostId(n[i].RelevantParentPostId);
                            temp.isRead(n[i].IsRead);
                            var date = new Date(parseInt(n[i].DateOfOrigin.slice(6, -2)));
                            temp.dateOfOrigin(moment(date).fromNow());
                            temp.senderDisplayName(n[i].SenderDisplayName);
                            temp.senderPicUrl(n[i].SenderPicUrl);
                            temp.notificationType(n[i].NotificationType);
                            self.notifications.push(temp);

                            if (temp.notificationType() == 'Feedback') {
                                feedbackCount = feedbackCount + 1;
                            }
                        }

                        self.context.notify("FEEDBACK_NOTIFICATIONS", feedbackCount);

                        self.context.notify("NEWSFEED_NOTIFICATIONS", self.noOfNotifications());
                    }
                },
                error: function () {
                    //do nothing
                }
            });
        };

        self.seeNotifications = function () {
            Boiler.UrlController.goTo("Notifications");
        };

        //self.checkNotifications();

        $(document).ready(function () {
            $(document).click(function () {
                $("#notificationList").hide('fast');
            });
            $("#notificationIcon").click(function (e) {
                if (self.noOfNotifications() > 0) {
                    $("#notificationList").slideToggle("fast");
                    self.noOfNotifications("");
                    e.stopPropagation();
                    self.context.notify('NOTIFICATIONS_READ');
                } else {
                    self.seeNotifications();
                }
            });
            self.checkNotifications();
            setInterval(function () { self.checkNotifications() }, 60000);
        });
    };

    return viewModel;
});


 
