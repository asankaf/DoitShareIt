define(['../../Models/Post', '../../Models/Comment'], function (Post, Comment) {

    var viewModel = function (context) {
        var self = this;
        self.posts = ko.observableArray();
        self.loadPostUrl = "/Post/GetPost";
        self.loadCommentUrl = "/Comment/Index";

        self.display = function (id) {
            self.posts([]);
            $.ajax({
                type: "GET",
                url: self.loadPostUrl,
                data: { postId: id },
                success: function (result) {
                    if (result.Status == "SUCCESS") {
                        var post = result.Result.Data;
                        
                            var temp = new Post(self.moduleContext);
                            temp.id = post.Id;
                            temp.body($('<div/>').html(post.Body).text());
                            temp.score(post.Score);
                            temp.ownerDisplayName(post.OwnerDisplayName);
                            temp.picUrl(post.OwnerPicUrl);
                            temp.isAnonymous(post.IsAnonymous);
                            if (!post.IsAnonymous) {
                                $.ajax({
                                    async: false,
                                    type: "GET",
                                    url: self.loadCommentUrl,
                                    data: { postId: post.Id },
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
                                                temp.comments.push(comment);
                                            }
                                        }
                                    }
                                });
                            }
                            self.posts.push(temp);
                    }
                }
            });
        }
    };

    return viewModel;
});
