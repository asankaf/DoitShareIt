define([], function () {

    function Comment() {
        var self = this;
        self.id = "";
        self.body = ko.observable();
        self.score = ko.observable();
        self.voteUpComment = function () {
            $.ajax({
                type: "GET",
                url: "http://localhost:59214/Vote/UpVote",
                data: { postId: self.id },
                success: function (result) {
                    if (result.Status == "SUCCESS") {

                    }
                    self.score(self.score() + 1);
                }
            });
        };
        self.voteDownComment = function () {
            if (self.score() == 0) {
                alert('you cannot down vote this post');
            } else {
                $.ajax({
                    type: "GET",
                    url: "http://localhost:59214/Vote/DownVote",
                    data: { postId: self.id },
                    success: function (result) {
                        if (result.Status == "SUCCESS") {

                        }
                        self.score(self.score() - 1);
                    }
                });
            }
        };
    }

    function Post() {
        var self = this;
        self.id = "";
        self.body = ko.observable();
        self.comments = ko.observableArray();
        self.score = ko.observable(0);

        self.commentText = ko.observable();

        self.voteUpPost = function () {
            $.ajax({
                type: "GET",
                url: "http://localhost:59214/Vote/UpVote",
                data: { postId: self.id },
                success: function (result) {
                    if (result.Status == "SUCCESS") {

                    }
                    self.score(self.score() + 1);
                }
            });
        };

        self.voteDownPost = function () {
            if (self.score() == 0) {
                alert('you cannot down vote this post');
            } else {
                $.ajax({
                    type: "GET",
                    url: "http://localhost:59214/Vote/DownVote",
                    data: { postId: self.id },
                    success: function (result) {
                        if (result.Status == "SUCCESS") {

                        }
                        self.score(self.score() - 1);
                    }
                });
            }
        };

        self.removeComment = function (data, event) {
            $.ajax({
                type: "DELETE",
                url: "http://localhost:59214/Comment/Destroy",
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
                type: "POST",
                url: "http://localhost:59214/Comment/Create",
                data: { Body: data.commentText(), ParentId: data.id, PostTypeId: 1 },
                success: function (result) {
                    if (result.Status == "SUCCESS") {
                        var c = result.Result.Data;
                        var c2 = new Comment();
                        c2.id = c.Id;
                        c2.body = c.Body;
                        c2.score = c.Score;
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
                type: "DELETE",
                url: "http://localhost:59214/Post/Destroy",
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
            post.body(p.Body);
            post.score(p.Score);
            self.posts.unshift(post);
        });

        // this.json = ko.observable();

        // this.save = function () {
        //     var js = ko.toJSON(this.posts());
        //     alert(js);
        //     this.json(js);
        // };

        //        moduleContext.listen("NEW_POST", function (post) {
        //            self.posts.unshift(new Post(post));
        //        });

        //        this.removePost = function (data, event) {
        //            self.posts.remove(data);
        //        };

        //        $.getJSON(moduleContext.getSettings().urls.feeds, function (result) {
        //            for (var i = 0; i < result.length; i++) {
        //                var aPost = new Post(result[i].text);
        //                for (var j = 0; j < result[i].comments.length; j++) {
        //                    var aComment = new Comment(result[i].comments[j].text, result[i].comments[j].votes);
        //                    aPost.comments.push(aComment);
        //                }
        //                aPost.votes(result[i].votes);
        //                self.posts.push(aPost);
        //            }
        //        });

        $.ajax({
            type: "GET",
            url: "http://localhost:59214/Post/Index",
            data: { postType: '0' },
            success: function (result) {
                if (result.Status == "SUCCESS") {
                    var posts = result.Result.Data;
                    for (var i = 0; i < posts.length; i++) {
                        var post = new Post();
                        post.id = posts[i].Id;
                        post.body(posts[i].Body);
                        post.score(posts[i].Score);
                        $.ajax({
                            type: "GET",
                            async: false,
                            url: "http://localhost:59214/Comment/Index",
                            data: { postId: posts[i].Id },
                            success: function (result2) {
                                if (result.Status == "SUCCESS") {
                                    var comments = result2.Result.Data;
                                    for (var j = 0; j < comments.length; j++) {
                                        var comment = new Comment();
                                        comment.body(comments[j].Body);
                                        comment.score(comments[j].Score);
                                        comment.id = comments[j].Id;
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

    return viewModel;
});
