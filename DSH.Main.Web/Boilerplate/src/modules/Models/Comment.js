define([], function () {

    var comment = function(context) {
        var self = this;
        self.id = "";
        self.body = ko.observable();
        self.score = ko.observable(0);
        self.picUrl = ko.observable("");
        self.ownerDisplayName = ko.observable("");
        self.voteUpComment = function() {
            $.ajax({
                async: false,
                type: "GET",
                url: "/Vote/UpVoteComment",
                data: { commentId: self.id },
                success: function(result) {
                    if (result.Status == "SUCCESS") {
                        self.score(result.Result);
                    } else {
                        alert(result.Result);
                    }
                }
            });
        };
    };
    return comment;
});