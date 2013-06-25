define(['../../../Models/Post', '../../../Models/Comment'], function (Post, Comment) {

    var update = function () {
        var self = this;

        self.getNewPostsURI = "/Post/GetNewPostIds";
        self.getNewCommnetsURI = "/Comment/GetNewComments";
        self.loadPostsUrl = "/Post/GetPost";
        self.getCommentByIdURL = "/Comment/Show";

        var getPostIds = function (wall) {
            $.getJSON(self.getNewPostsURI, function (data) {
                //console.log("*getPostIds* data: " + data);
                if (data.Result.length > 0) {
                    var newPostIds = data.Result;
                    for (var i = 0; i < newPostIds.length; ++i) {
                        console.log("found new post => id: " + newPostIds[i]);
                        loadPosts(newPostIds[i], wall);
                    }


                } else {
                    console.log("No new posts to load ");
                }
                return data;
            });


        };

        // get new comments from the server and add them to respective posts
        // when a new comment is found we can use its parentID to find what field this comment belongs to
        // add that comment to correct post variable from the *wall* argument
        // wall it the actual wall object rended in the font page by knockoutjs
        var getNewComments = function (wall) {

            $.getJSON(self.getNewCommnetsURI, function (data) {

                if (data.Status == "SUCCESS" && data.Result.length > 0) {
                    // from wall find the post with appropriate ids (parent ids from comment and add those comments to those posts

                    for (var i = 0; i < data.Result.length; ++i) {
                        // find the post related to this comment and add this comment to that post

                        var perentPostById = data.Result[i].ParentId;

                        ko.utils.arrayForEach(wall.posts(), function (eachPost) {

                            if (eachPost.id == perentPostById) {

                                $.ajax({
                                    async: false,
                                    type: "GET",
                                    url: self.getCommentByIdURL,
                                    data: { commentId: data.Result[i].CommentId },
                                    success: function (commentInfo) {
                                        if (commentInfo.Status == "SUCCESS") {
                                            var cmtData = commentInfo.Result.Data;
                                            var comment = new Comment(self.moduleContext);
                                            comment.body(cmtData.Body);
                                            comment.score(cmtData.Score);
                                            comment.id = cmtData.Id;
                                            comment.ownerDisplayName = cmtData.OwnerDisplayName;
                                            comment.picUrl(cmtData.OwnerPicUrl);
                                            eachPost.comments.push(comment);
                                        }
                                    }
                                });


                            }
                            //                            console.log(eachPost);
                        });
                    }

                }
            });
        };


        // add both new comments and posts to the wall with this function call with the wall object as its argument
        self.changeWall = function (wall) {
            getPostIds(wall);
            getNewComments(wall);

        };


        //        self.removePost = function (data, event) {
        //            $.ajax({
        //                async: false,
        //                type: "DELETE",
        //                url: self.removePostUrl,
        //                data: { postId: data.id },
        //                success: function (result) {
        //                    if (result.Status == "SUCCESS") {
        //                        self.posts.remove(data);
        //                    }
        //                }
        //            });
        //        };


        // using previously written loadPost method with modifications to load new post into the wall
        var loadPosts = function (postId, wall) {
            console.log("loading post with id: " + postId);

            $.ajax({
                type: "GET",
                url: self.loadPostsUrl,
                data: { postId: postId },
                success: function (result) {

                    console.log("result : ");
                    console.log(result.Result);

                    if (result.Status == "SUCCESS") {
                        var post_data = result.Result.Data;
                        console.log("downloaded json posts");
                        console.log(post_data);

                        
                        var post = new Post(self.moduleContext);
                        post.id = post_data.Id;
                        post.body($('<div/>').html(post_data.Body).text());
                        post.score(post_data.Score);
                        post.ownerDisplayName(post_data.OwnerDisplayName);
                        post.picUrl(post_data.OwnerPicUrl);
                        post.isAnonymous(post_data.IsAnonymous);
                        post.title(post_data.Title);
                        if (!post_data.IsAnonymous) {
                            $.ajax({
                                async: false,
                                type: "GET",
                                url: self.getCommentByIdURL,
                                data: { commentId: post_data.Id },
                                
                                success: function(result2) {
                                    if (result2.Status == "SUCCESS") {
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
                        wall.posts.unshift(post);

                    }
                    //self.fetchingPosts(false);
                }
            });
        };





    };

    return update;

});