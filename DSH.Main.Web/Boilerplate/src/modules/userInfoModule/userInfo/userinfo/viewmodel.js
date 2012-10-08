define([], function () {

    var viewModel = function (moduleContext) {
        var self = this;
        self.posts = ko.observableArray();


        self.displayName = ko.observable();
        self.views = ko.observable();
        self.upVotes = ko.observable();
        self.downVotes = ko.observable();


        var url = '/user/'; // '/userInfo/' + '8654ae61-f19d-440f'; // server url (rest uri + user unique id)

        // geting userinfo from the server and display them in the view
        $.getJSON(url, function (data) {

            self.displayName(data.DisplayName);
            self.views(data.Views);
            self.upVotes(data.Upvotes);
            self.downVotes(data.Downvotes);

            //console.log(data);

        });


    };

    return viewModel;
});
