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


 
