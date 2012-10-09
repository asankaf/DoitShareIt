define(['./Post', './Comment'], function (Post, Comment) {

    var Wall = function () {
        var self = this;
        self.posts = ko.observableArray();
        self.loadPostsUrl = "";
        self.getMorePostsUrl = "";
        self.loadCommentUrl = "";
        self.removePostUrl = "";
        self.postType = '';

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
                            var post = new Post();
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
                            }
                            self.posts.push(post);
                        }
                    }
                }
            });
        };

        self.getMorePosts = function () {
            $.ajax({
                type: "GET",
                url: self.getMorePostsUrl,
                data: { postType: self.postType },
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
                            }
                            self.posts.push(post);
                        }
                        if (posts.length == 0) {
                            alert('No more posts are available');
                        }
                    }
                }
            });
        };
    };

    return Wall;

});