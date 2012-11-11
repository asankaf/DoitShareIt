define([], function () {
    var notification = function (context) {
        var self = this;
        self.id = ko.observable();
        self.senderId = ko.observable();
        self.recipientId = ko.observable();
        self.body = ko.observable();
        self.relevantPostId = ko.observable();
        self.isRead = ko.observable();
        self.dateOfOrigin = ko.observable();
        self.senderDisplayName = ko.observable();
        self.senderPicUrl = ko.observable();

        context.listen("NOTIFICATIONS_READ", function () {
            if (self.isRead() == false) {
                //$.ajax({
                //    type: "GET",
                //    url: "/Notification/MarkNotificationRead",
                //    data: { notificationId: self.id },
                //    success: function(result) {
                //    }
                //});
                //self.isRead(true);
            }
        });

        self.clicked = function () {
            alert('was clicked');
        };
    };

    return notification;

});