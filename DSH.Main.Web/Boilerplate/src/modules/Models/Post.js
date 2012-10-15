define([], function () {

    var post = function (context) {
        var self = this;
        self.id = "";
        self.body = ko.observable();
        self.comments = ko.observableArray();
        self.score = ko.observable(0);
        self.picUrl = ko.observable("");
        self.ownerDisplayName = ko.observable("");
        self.isAnonymous = ko.observable("");

        self.commentText = ko.observable();

        self.registerEvents = function() {
            self.moduleContext.listen("NEW_COMMENT", function (c) {
                if (self.id = c.ParentId && !self.IsAnonymous) {
                    var comment = new Comment(self.moduleContext);
                    comment.body(c.Body);
                    comment.score(c.Score);
                    comment.id = c.Id;
                    comment.ownerDisplayName = c.OwnerDisplayName;
                    comment.picUrl(c.OwnerPicUrl);
                    self.comments.push(comment);
                }
            });
        };

        self.voteUpPost = function () {
            $.ajax({
                async: false,
                type: "GET",
                url: "/Vote/UpVotePost",
                data: { postId: self.id },
                success: function (result) {
                    if (result.Status == "SUCCESS") {
                        self.score(result.Result);
                    } else {
                        alert(result.Result);
                    }
                }
            });
        };

        self.voteDownPost = function () {
            if (self.score() == 0) {
                alert('you cannot down vote this post');
            } else {
                $.ajax({
                    async: false,
                    type: "GET",
                    url: "/Vote/DownVotePost",
                    data: { postId: self.id },
                    success: function (result) {
                        if (result.Status == "SUCCESS") {
                            self.score(result.Result);
                        } else {
                            alert(result.Result);
                        }
                    }
                });
            }
        };

        self.removeComment = function (data, event) {
            $.ajax({
                async: false,
                type: "DELETE",
                url: "/Comment/Destroy",
                data: { commentId: data.id },
                success: function (result) {
                    if (result.Status == "SUCCESS") {
                        self.comments.remove(data);
                    }
                }
            });
        };

        self.addComment = function (data, event) {
            $.ajax({
                async: false,
                type: "POST",
                url: "/Comment/Create",
                data: { Body: data.commentText(), ParentId: data.id, PostTypeId: 1 },
                success: function (result) {
                    if (result.Status == "SUCCESS") {
                        var c = result.Result.Data;
                        var c2 = new Comment();
                        c2.id = c.Id;
                        c2.body = c.Body;
                        c2.score = c.Score;
                        c2.ownerDisplayName = c.OwnerDisplayName;
                        $.ajax({
                            async: false,
                            type: "GET",
                            url: "/User/GetUserPicUrl",
                            data: { id: c.OwnerUserId },
                            success: function (result2) {
                                if (result2.Status == "SUCCESS") {
                                    c2.picUrl(result2.Result);
                                }
                            }
                        });
                        data.comments.push(c2);
                    }
                }
            });
            data.commentText('');
        };
    };

    return post;

});