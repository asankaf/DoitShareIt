define(['../../../Models/Post', '../../../Models/Comment'], function (Post, Comment) {

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
            post.title(p.Title);
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

        self.loadPosts = function (id) {

            self.posts.removeAll();
            console.log(id);
            $.ajax({
                type: "GET",
                url: "/Post/GetSelectedInfo",
                data: { postType: '0', selectedId: id },
                success: function (result) {
                    if (result.Status == "SUCCESS") {
                        var posts = result.Result.Data;
                        for (var i = 0; i < posts.length; i++) {
                            var post = new Post();
                            post.id = posts[i].Id;
                            post.body($('<div/>').html(posts[i].Body).text());
                            post.score(posts[i].Score);
                            post.ownerDisplayName(posts[i].OwnerDisplayName);
                            post.title(posts[i].Title);
                            $.ajax({
                                async: false,
                                type: "GET",
                                url: "/User/GetUserPicUrl",
                                data: { id: posts[i].OwnerUserId },
                                success: function (result2) {
                                    if (result2.Status == "SUCCESS") {
                                        post.picUrl(result2.Result);
                                    }
                                }
                            });
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
                                            $.ajax({
                                                async: false,
                                                type: "GET",
                                                url: "/User/GetUserPicUrl",
                                                data: { id: comments[j].OwnerUserId },
                                                success: function (result4) {
                                                    if (result4.Status == "SUCCESS") {
                                                        comment.picUrl(result4.Result);
                                                    }
                                                }
                                            });
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