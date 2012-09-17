define([], function () {


    var ViewModel = function (moduleContext) {
        var self = this;
        function Comment(aPost, comment, votes) {
            return {
                post: aPost,
                text: ko.observable(comment),
                votes: ko.observable(votes),
                removeComment: function () {
                    this.post.comments.remove(this);
                },
                voteUp: function () {
                    this.votes(this.votes() + 1);
                },
                voteDown: function () {
                    if (this.votes() == 0) {
                        alert('you cannot down vote this post');
                    } else {
                        this.votes(this.votes() - 1);
                    }
                }
            };
        }

        function Post(postText) {
            return {
                text: ko.observable(postText),
                comments: ko.observableArray(),
                commentText: ko.observable(),
                votes: ko.observable(0),
                removePost: function () {
                    self.posts.remove(this);
                },
                addComment: function () {
                    if (this.commentText() == '') {
                        alert('please enter a valid comment');
                    } else {
                        this.comments.push(Comment(this, this.commentText(), 0));
                        this.commentText('');
                    }
                },
                voteUp: function () {
                    this.votes(this.votes() + 1);
                },
                voteDown: function () {
                    if (this.votes() == 0) {
                        alert('you cannot down vote this post');
                    } else {
                        this.votes(this.votes() - 1);
                    }
                }
            };
        }

        this.posts = ko.observableArray();
        moduleContext.listen("NEW_POST", function (post) {
            self.posts.unshift(Post(post));
        });
    };

    return ViewModel;
});
