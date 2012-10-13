define(['../../../Models/Post', '../../../Models/Comment', '../../../Models/Wall/viewmodel'], function (Post, Comment, Wall) {

    var viewModel = function () { };

    var wall = new Wall();
    wall.loadPostsUrl = "/Post/GetPosts";
    wall.getMorePostsUrl = "/Post/GetMorePosts";
    wall.loadCommentUrl = "/Comment/Index";
    wall.removePostUrl = "/Post/Destroy";
    wall.postType = 0;

    viewModel.prototype = wall;

    viewModel.prototype.loadNewPosts = function () {
        $.ajax({
            type: "GET",
            url: "/Post/GetNewPosts",
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
                            async: false,
                            type: "GET",
                            url: "/Comment/Index",
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
                        self.posts.unshift(post);
                    }
                }
            }
        });
    };

    return viewModel;
});
