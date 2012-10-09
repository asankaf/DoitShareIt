define([], function () {

    var viewModel = function () {
        var self = this;
        self.frequentUser = ko.observableArray();

        

        $.ajax({
            type: "GET",
            url: "/user/frequentuser",
            success: function (result) {
                if (result.Status == "SUCCESS") {
                    self.frequentUser(result.Result);
                    Console.log(result.Result);
                }
            }

        });
    };
    return viewModel;
});
    

 
        



/*

define([], function () {
    var ViewModel = function (moduleContext) {
        var self = this;
        self.name = ko.observable("");
        self.photo = ko.observable("");
        self.reputation = ko.observable("");

        $.ajax({
            cache: false,
            type: "POST",
            url: "/Home/Userprofile"

        }).done(function (data) {



            self.name(data.DisplayName);
            self.photo(data.PicLocation);
            self.reputation(data.Reputation);

        });

    };


    return ViewModel;

});
  
*/
 

        // this.json = ko.observable();

        // this.save = function () {
        //     var js = ko.toJSON(this.posts());
        //     alert(js);
        //     this.json(js);
        // };

        //        moduleContext.listen("NEW_POST", function (post) {
        //            self.posts.unshift(new Post(post));
        //        });

        //        this.removePost = function (data, event) {
        //            self.posts.remove(data);
        //        };

        //        $.getJSON(moduleContext.getSettings().urls.feeds, function (result) {
        //            for (var i = 0; i < result.length; i++) {
        //                var aPost = new Post(result[i].text);
        //                for (var j = 0; j < result[i].comments.length; j++) {
        //                    var aComment = new Comment(result[i].comments[j].text, result[i].comments[j].votes);
        //                    aPost.comments.push(aComment);
        //                }
        //                aPost.votes(result[i].votes);
        //                self.posts.push(aPost);
        //            }
        //        });

    