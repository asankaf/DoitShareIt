define([], function () {

    function Comment() {
        var self = this;
        self.id = "";
        self.body = ko.observable();
        self.score = ko.observable();
        self.picUrl = ko.observable("");
        self.ownerDisplayName = ko.observable("");
        self.voteUpComment = function () {
            $.ajax({
                async: false,
                type: "GET",
                url: "/Vote/UpVoteComment",
                data: { commentId: self.id },
                success: function (result) {
                    if (result.Status == "SUCCESS") {
                        self.score(result.Result);
                    } else {
                        alert(result.Result);
                    }
                }
            });
        };
    }

    function Post() {
        var self = this;
        self.id = "";
        self.body = ko.observable();
        self.comments = ko.observableArray();
        self.score = ko.observable(0);
        self.ownerDisplayName = ko.observable("");
        self.picUrl = ko.observable("");

        self.commentText = ko.observable();

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
    }

    var viewModel = function (moduleContext) {
        var self = this;
        self.posts = ko.observableArray();

        self.removePost = function (data, event) {
            $.ajax({
                async: false,
                type: "DELETE",
                url: "/Post/Destroy",
                data: { postId: data.id },
                success: function (result) {
                    if (result.Status == "SUCCESS") {
                        self.posts.remove(data);
                    }
                }
            });
        };

        moduleContext.listen("NEW_POST", function (p) {
            var post = new Post();
            post.id = p.Id;
            post.body($('<div/>').html(p.Body).text());
            post.score(p.Score);
            post.ownerDisplayName(p.OwnerDisplayName);
            $.ajax({
                async: false,
                type: "GET",
                url: "/User/GetUserPicUrl",
                data: { id: p.OwnerUserId },
                success: function (result2) {
                    if (result2.Status == "SUCCESS") {
                        post.picUrl(result2.Result);
                    }
                }
            });
            self.posts.unshift(post);
        });

        self.loadPosts = function () {
            $.ajax({
                type: "GET",
                url: "/Post/Index",
                data: { postType: '0' },
                success: function (result) {
                    if (result.Status == "SUCCESS") {
                        var posts = result.Result.Data;
                        for (var i = 0; i < posts.length; i++) {
                            var post = new Post();
                            post.id = posts[i].Id;
                            post.body($('<div/>').html(posts[i].Body).text());
                            post.score(posts[i].Score);
                            post.ownerDisplayName(posts[i].OwnerDisplayName);
                            post.picUrl(posts[i].OwnerPicUrl);
                            $.ajax({
                                type: "GET",
                                async: false,
                                url: "/Comment/Index",
                                data: { postId: posts[i].Id },
                                success: function (result3) {
                                    if (result.Status == "SUCCESS") {
                                        var comments = result3.Result.Data;
                                        for (var j = 0; j < comments.length; j++) {
                                            var comment = new Comment();
                                            comment.body(comments[j].Body);
                                            comment.score(comments[j].Score);
                                            comment.id = comments[j].Id;
                                            comment.ownerDisplayName = comments[j].OwnerDisplayName;
                                            comment.picUrl(comments[j].OwnerPicUrl);
                                            post.comments.push(comment);
                                        }
                                    }
                                }
                            });
                            self.posts.unshift(post);
                        }
                    }
                }
            });
        };
    };

    return viewModel;
});
