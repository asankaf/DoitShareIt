define(['Boiler'], function (Boiler) {

    var ViewModel = function (moduleContext) {

        var self = this;
        this.id = "";
        this.flag;
        current = new Date();
        self.name = ko.observable("");
        self.photo = ko.observable("");
        self.reputation = ko.observable("");


        self.bind = function (data) {

            var splitName = data.DisplayName.split(" ");
            (self.flag) ? self.name(splitName[0]) : self.name("You");
            self.photo(data.PicLocation);
            self.reputation(data.Reputation);

        };


        self.getUser = function (id) {
            self.id = id;
            var current;
            if (id >= 0) {
                

                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: "/Home/Userprofile",
                    data: { id: id }

                }).done(function (data) {
                    self.bind(data);
                    current = data;

                });
            }

            return current;

        };

       
        this.link = function () {

            Boiler.UrlController.goTo("userinfo/" + self.id);
        };
    };

    return ViewModel;
});