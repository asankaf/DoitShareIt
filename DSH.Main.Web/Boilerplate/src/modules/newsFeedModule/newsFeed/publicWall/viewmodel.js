define([], function () {

    function Comment(comment, votes) {
        var self = this;
        this.text = ko.observable(comment);
        this.votes = ko.observable(votes);
        this.voteUp = function () {
            this.votes(this.votes() + 1);
        };
        this.voteDown = function () {
            if (this.votes() == 0) {
                alert('you cannot down vote this post');
            } else {
                this.votes(this.votes() - 1);
            }
        };
    }


    function Post(postText) {
        var self = this;
        this.text = ko.observable(postText);
        this.comments = ko.observableArray();
        this.commentText = ko.observable();
        this.votes = ko.observable(0);
        this.voteUp = function () {
            this.votes(this.votes() + 1);
        };
        this.voteDown = function () {
            if (this.votes() == 0) {
                alert('you cannot down vote this post');
            } else {
                this.votes(this.votes() - 1);
            }
        };
        this.addComment = function () {
            if (this.commentText() == '') {
                alert('please enter a valid comment');
            } else {
                this.comments.push(new Comment(this.commentText(), 0));
                this.commentText('');
            }
        };
        this.removeComment = function (data, event) {
            self.comments.remove(data);
        };
    }

    var ViewModel = function (moduleContext) {
        var self = this;
        this.posts = ko.observableArray();

        // this.json = ko.observable();

        // this.save = function () {
        //     var js = ko.toJSON(this.posts());
        //     alert(js);
        //     this.json(js);
        // };

        moduleContext.listen("NEW_POST", function (post) {
            self.posts.unshift(new Post(post));
        });

        this.removePost = function (data, event) {
            self.posts.remove(data);
        };

        $.getJSON(moduleContext.getSettings().urls.feeds, function (result) {
            for (var i = 0; i < result.length; i++) {
                alert(result[i].text);
                var aPost = new Post(result[i].text);
                for (var j = 0; j < result[i].comments.length; j++) {

                }
            }
        });
    };

    return ViewModel;
});
