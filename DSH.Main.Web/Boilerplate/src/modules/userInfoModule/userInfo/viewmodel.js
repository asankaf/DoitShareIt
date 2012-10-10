
define([], function () {

    var viewModel = function (moduleContext) {
        var self = this;
        self.feedbacks = ko.observableArray();
        self.comments = ko.observableArray();
        self.id;


        var Post = function () {
            var self = this;

            self.postId = ko.observable(0);
            self.createdDate = ko.observable();
            self.title = ko.observable();
            self.noOfVotes = ko.observable();
            self.noOfComments = ko.observable();
            self.posterId = ko.observable();
            self.posterName = ko.observable();

        };

        getUserPosts = function (postType, id, posts) {

            $.ajax({
                cache: false,
                async: false,
                type: "GET",
                url: "/Post/GetSelectedInfo",
                data: { postType: postType, selectedId: id }

            }).done(function (result) {
                var postsArray = result.Result.Data;

                for (i = 0; i < postsArray.length; i++) {
                    var post = new Post();
                    post.postId = postsArray[i].Id;

                    var date = new Date(parseInt(postsArray[i].CreationDate.slice(6, -2)));

                    post.createdDate = moment(date).fromNow();
                    post.title = postsArray[i].Body.slice(0, 35) + " ... ";
                    post.noOfVotes = postsArray[i].Score;
                    post.noOfComments = postsArray[i].CommentCount;
                    post.posterId = postsArray[i].OwnerUserId;

                    var splitName=postsArray[i].OwnerDisplayName.split(" ");

                    post.posterName =splitName[0] ;
                    posts.push(post);
                }


            });



        };




        self.getPosts = function (id) {
            getUserPosts(2, id, self.feedbacks);
            getUserPosts(1, id, self.comments);

        };





    };

    return viewModel;
});
