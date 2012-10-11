define([], function () {

    var ViewModel = function (moduleContext) {

        var self = this;
        self.name = ko.observable("");
        self.photo = ko.observable("");
        self.reputation = ko.observable("");

        self.bind = function (data) {

            self.name(data.DisplayName);
            self.photo(data.PicLocation);
            self.reputation(data.Reputation);
        };

        self.getUser = function (id) {

            if (id >= 0) {
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: "/Home/Userprofile",
                    data: { id: id }

                }).done(function (data) {
                    self.bind(data);

                });
            }
        };


        this.testing = function() {
            alert("Hellooooo");
        };


    };

    return ViewModel;
});