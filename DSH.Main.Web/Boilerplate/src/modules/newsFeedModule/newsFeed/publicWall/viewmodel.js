define(['../../../Models/Post', '../../../Models/Comment', './wall', './update'], function (Post, Comment, Wall, Update) {

    var viewModel = function (context) { };
    var wall = new Wall();
    wall.loadPostsUrl = "/Post/GetPosts";
    wall.getMorePostsUrl = "/Post/GetMorePosts";
    wall.loadCommentUrl = "/Comment/Index";
    wall.removePostUrl = "/Post/Destroy";
    wall.postType = 0;

    viewModel.prototype = wall;

    up = new Update();
//    up.changeWall(wall, "hello");

    __wall__ = wall;

    return viewModel;
});
