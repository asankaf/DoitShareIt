
define(['Boiler'], function (Boiler) {

    var viewModel = function (moduleContext) {
        var self = this;
        self.reputationChange = ko.observableArray();
        self.reputation = ko.observable(0);
        self.feedbacks = ko.observableArray();
        self.comments = ko.observableArray();
        self.feedbacksSum = ko.observableArray();
        self.commentsSum = ko.observableArray();
        self.repSum = ko.observableArray();
        self.Visible = ko.observable(true);
        self.BottomVisible = ko.observable(true);
        self.UpvoteCount = ko.observable();
        self.DownvoteCount = ko.observable();
        self.VoteCount = ko.observable();
        self.FeedBackVoteCount = ko.observable();
        self.CommentvoteCount = ko.observable();

        var RepData = function () {
            var self = this;

            self.repCount = ko.observable(0);
            self.countDate = ko.observable();
            self.countTime = ko.observable();
            self.reason = ko.observable();
            self.title = ko.observable();
        };

        getReputation = function (id, repChanges) {
            $.ajax({
                type: "GET",
                url: "/user/reputationchange",
                data: { userId: id }
            }).done(function (result) {
                var repC = 0;
                var repArray = result.Result;
                repChanges([]);
                self.repSum([]);
                for (i = 0; i < repArray.length; i++) {
                    var rep = new RepData();
                    rep.repCount = repArray[i].ReputationCount;
                    repC = repC + 1;
                    rep.countDate = repArray[i].VotedDate;
                    rep.countTime = repArray[i].VotedTime;
                    rep.reason = repArray[i].VoteTypeForPost;
                    rep.title = repArray[i].PostDes;

                    repChanges.push(rep);

                    if (i < 5) {
                        self.repSum.push(rep);
                    }
                }
                self.reputation(repC);
            });

        };


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
                posts([]);
                for (i = 0; i < postsArray.length; i++) {
                    var post = new Post();
                    post.postId = postsArray[i].Id;

                    var date = new Date(parseInt(postsArray[i].CreationDate.slice(6, -2)));

                    post.createdDate = moment(date).fromNow();
                    post.title = postsArray[i].Body.slice(0, 35) + " ... ";
                    post.noOfVotes = postsArray[i].Score;
                    post.noOfComments = postsArray[i].CommentCount;
                    post.posterId = postsArray[i].OwnerUserId;

                    var splitName = postsArray[i].OwnerDisplayName.split(" ");

                    post.posterName = splitName[0];
                    posts.unshift(post);
                }

            });

        };


        getSummary = function (posts, postsSum) {

            postsSum([]);
            for (i = 0; i < posts.length && i < 5; i++) {
                var post = new Post();
                post.postId = posts[i].postId;
                post.createdDate = posts[i].createdDate;
                post.title = posts[i].title.slice(0, 20) + " ...";
                post.noOfVotes = posts[i].noOfVotes;
                post.noOfComments = posts[i].noOfComments;
                post.posterId = posts[i].posterId;
                post.posterName = posts[i].posterName;
                postsSum.push(post);
            }

        };

        getVotesInfo = function (id) {

            $.ajax({
                cache: false,
                async: false,
                type: "GET",
                url: "/User/UserVotesCount",
                data: { userId: id },

                success: function (result) {
                    if (result.Status == "SUCCESS") {
                        var data = result.Result;

                        self.UpvoteCount(data.TotalUpvoteCount);
                        self.DownvoteCount(data.TotalDownvoteCount);
                        self.VoteCount(data.TotalUpvoteCount + data.TotalDownvoteCount);
                        self.FeedBackVoteCount(data.FeedbackVoteCount);
                        self.CommentvoteCount(data.CommentVoteCount);

                    }

                }
            });

        };

        self.getPosts = function (id) {
            getReputation(id, self.reputationChange);
            getUserPosts(2, id, self.feedbacks);
            getUserPosts(1, id, self.comments);
            getSummary(self.feedbacks(), self.feedbacksSum);
            getSummary(self.comments(), self.commentsSum);
            getVotesInfo(id);

        };


        click = function (id) {
            //Boiler.UrlController.goTo("abcd/" + id.postId);
            moduleContext.notify("POST", id.postId);
            // console.log("hit userlink! " + id.postId);
        };

        userLink = function (id) {
            Boiler.UrlController.goTo("user/" + id.posterId);
        };


    };

    return viewModel;
});