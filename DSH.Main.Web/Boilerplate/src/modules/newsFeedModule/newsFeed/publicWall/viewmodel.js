define(['../../../Models/Post', '../../../Models/Comment', '../../../Models/Wall/viewmodel'], function (Post, Comment, Wall) {

    var viewModel = function (context) {};
    var wall = new Wall();
    wall.loadPostsUrl = "/Post/GetPosts";
    wall.getMorePostsUrl = "/Post/GetMorePosts";
    wall.loadCommentUrl = "/Comment/Index";
    wall.removePostUrl = "/Post/Destroy";
    wall.postType = 0;

    viewModel.prototype = wall;

    return viewModel;
});
