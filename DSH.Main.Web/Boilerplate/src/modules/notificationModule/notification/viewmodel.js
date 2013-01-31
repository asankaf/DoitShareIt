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
                            self.notifications.push(temp);
                        }
                    }
                },
                error: function () {
                    //do nothing
                }
            });
        };

        self.seeNotifications = function () {
            Boiler.UrlController.goTo("#Notifications");
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
            setInterval(function () { self.checkNotifications() }, 60000);
        });
    };

    return viewModel;
});


 
