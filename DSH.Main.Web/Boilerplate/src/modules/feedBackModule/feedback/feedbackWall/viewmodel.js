define(['../../../Models/Post', '../../../Models/Comment', './wall'], function (Post, Comment, Wall) {

    var viewModel = function () { };

    var wall = new Wall();
    wall.loadPostsUrl = "/Post/GetTaggedPosts";
    wall.getMorePostsUrl = "/Post/GetMoreTaggedPosts";
    wall.loadCommentUrl = "/Comment/Index";
    wall.removePostUrl = "/Post/Destroy";
    wall.postType = 2;

    viewModel.prototype = wall;

    return viewModel;
});
