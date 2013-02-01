define(['../../Models/Notification'], function (Notification) {

    var viewModel = function (moduleContext) {
        var self = this;
        self.context = moduleContext;
        self.notifications = ko.observableArray();
        self.loadNotificationsUrl = "/Notification/GetNotifications";
        self.getMoreNotificationsUrl = "/Notification/GetMoreNotifications";
        self.allFetched = ko.observable(false);
        self.fetchingNotifications = ko.observable(true);

        self.loadNotifications = function () {
            self.notifications([]);
            $.ajax({
                type: "GET",
                url: self.loadNotificationsUrl,
                success: function (result) {
                    if (result.Status == "SUCCESS") {
                        var n = result.Result.Data;
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
                            temp.url("#singlepost/" + n[i].RelevantParentPostId);
                            temp.details("click here to view the related post");
                            self.notifications.push(temp);
                        }
                    }
                    self.fetchingNotifications(false);
                }
            });
        };


        self.getMoreNotifications = function () {
            self.fetchingNotifications(true);
            $.ajax({
                type: "GET",
                url: self.getMoreNotificationsUrl,
                success: function (result) {
                    if (result.Status == "SUCCESS") {
                        var n = result.Result.Data;
                        for (var i = 0; i < n.length; i++) {
                            var temp = new Notification(self.context);
                            temp.id(n[i].Id);
                            temp.senderId(n[i].SenderId);
                            temp.recipientId(n[i].RecipientId);
                            temp.body(n[i].Body);
                            temp.relevantPostId(n[i].RelevantPostId);
                            temp.isRead(n[i].IsRead);
                            var date = new Date(parseInt(n[i].DateOfOrigin.slice(6, -2)));
                            temp.dateOfOrigin(moment(date).fromNow());
                            temp.senderDisplayName(n[i].SenderDisplayName);
                            temp.senderPicUrl(n[i].SenderPicUrl);
                            self.notifications.push(temp);
                        }
                        if (n.length == 0) {
                            self.allFetched(true);
                        }
                    }
                    self.fetchingNotifications(false);
                }
            });
        };

        self.loadNotifications();
        $(window).scroll(function () {
            if (!self.allFetched()) {
                if ($("#notification_wall").is(":visible") && ($(window).height() + $(window).scrollTop() > $(document).height() - 25)) {
                    self.getMoreNotifications();
                }
            }
        });

    };
    return viewModel;
});
