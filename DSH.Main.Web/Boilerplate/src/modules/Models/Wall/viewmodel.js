define(['../Post', '../Comment'], function (Post, Comment) {

    var wall = function () {
        var self = this;
        self.posts = ko.observableArray();
        self.loadPostsUrl = "";
        self.getMorePostsUrl = "";
        self.loadCommentUrl = "";
        self.removePostUrl = "";
        self.postType = '';

        self.allFetched = ko.observable(false);
        self.fetchingPosts = ko.observable(true);

        self.registerEvents = function () {
            self.moduleContext.listen("NEW_POST", function (p) {
                if (p.PostTypeId = self.postType) {
                    var post = new Post(self.moduleContext);
                    post.id = p.Id;
                    post.body($('<div/>').html(p.Body).text());
                    post.score(p.Score);
                    post.ownerDisplayName(p.OwnerDisplayName);
                    post.picUrl(p.OwnerPicUrl);
                    post.isAnonymous(p.IsAnonymous);
                    if (!p.IsAnonymous) {
                        $.ajax({
                            async: false,
                            type: "GET",
                            url: self.loadCommentUrl,
                            data: { postId: p.Id },
                            success: function (result2) {
                                if (result.Status == "SUCCESS") {
                                    var comments = result2.Result.Data;
                                    for (var j = 0; j < comments.length; j++) {
                                        var comment = new Comment(self.moduleContext);
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
                    }
                    self.posts.unshift(post);
                }
            });
        };

        self.removePost = function (data, event) {
            $.ajax({
                async: false,
                type: "DELETE",
                url: self.removePostUrl,
                data: { postId: data.id },
                success: function (result) {
                    if (result.Status == "SUCCESS") {
                        self.posts.remove(data);
                    }
                }
            });
        };

        self.loadPosts = function () {
            self.posts([]);
            $.ajax({
                type: "GET",
                url: self.loadPostsUrl,
                data: { postType: self.postType },
                success: function (result) {
                    if (result.Status == "SUCCESS") {
                        var posts = result.Result.Data;
                        for (var i = 0; i < posts.length; i++) {
                            var post = new Post(self.moduleContext);
                            post.id = posts[i].Id;
                            post.body($('<div/>').html(posts[i].Body).text());
                            post.score(posts[i].Score);
                            post.ownerDisplayName(posts[i].OwnerDisplayName);
                            post.picUrl(posts[i].OwnerPicUrl);
                            post.isAnonymous(posts[i].IsAnonymous);
                            if (!posts[i].IsAnonymous) {
                                $.ajax({
                                    async: false,
                                    type: "GET",
                                    url: self.loadCommentUrl,
                                    data: { postId: posts[i].Id },
                                    success: function (result2) {
                                        if (result.Status == "SUCCESS") {
                                            var comments = result2.Result.Data;
                                            for (var j = 0; j < comments.length; j++) {
                                                var comment = new Comment(self.moduleContext);
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
                            }
                            self.posts.push(post);
                        }
                    }
                    self.fetchingPosts(false);
                }
            });
        };

        self.getMorePosts = function () {
            self.fetchingPosts(true);
            $.ajax({
                type: "GET",
                url: self.getMorePostsUrl,
                data: { postType: self.postType },
                success: function (result) {
                    if (result.Status == "SUCCESS") {
                        var posts = result.Result.Data;
                        for (var i = 0; i < posts.length; i++) {
                            var post = new Post(self.moduleContext);
                            post.id = posts[i].Id;
                            post.body($('<div/>').html(posts[i].Body).text());
                            post.score(posts[i].Score);
                            post.ownerDisplayName(posts[i].OwnerDisplayName);
                            post.picUrl(posts[i].OwnerPicUrl);
                            post.isAnonymous(posts[i].IsAnonymous);
                            if (!posts[i].IsAnonymous) {
                                $.ajax({
                                    async: false,
                                    type: "GET",
                                    url: self.loadCommentUrl,
                                    data: { postId: posts[i].Id },
                                    success: function (result2) {
                                        if (result.Status == "SUCCESS") {
                                            var comments = result2.Result.Data;
                                            for (var j = 0; j < comments.length; j++) {
                                                var comment = new Comment(self.moduleContext);
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
                            }
                            self.posts.push(post);
                        }
                        if (posts.length == 0) {
                            self.allFetched(true);
                        }
                    }
                    self.fetchingPosts(false);
                }
            });
        };

        self.initialize = function (context) {
            self.moduleContext = context;
            $(window).scroll(function () {
                if (!self.allFetched()) {
                    if ($(window).height() + $(window).scrollTop() > $(document).height() - 25) {
                        self.getMorePosts();
                    }
                }
            });

            self.loadPosts();
            self.registerEvents();
        };


    };

    return wall;

});