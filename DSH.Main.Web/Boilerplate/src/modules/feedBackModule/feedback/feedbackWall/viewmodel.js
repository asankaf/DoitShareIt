define(['../../../Models/Post', '../../../Models/Comment', '../../../Models/Wall'], function (Post, Comment, Wall) {

    var ViewModel = function () { };
    
    var wall = new Wall();
    wall.loadPostsUrl = "/Post/GetTaggedPosts";
    wall.getMorePostsUrl = "/Post/GetMoreTaggedPosts";
    wall.loadCommentUrl = "/Comment/Index";
    wall.removePostUrl = "/Post/Destroy";
    wall.postType = 2;

    ViewModel.prototype = wall;

    return ViewModel;
});
