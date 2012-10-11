define(['Boiler'], function (Boiler) {

    var ViewModel = function (moduleContext) {

        var self = this;
        var id;
        current = new Date();
        self.name = ko.observable("");
        self.photo = ko.observable("");
        self.reputation = ko.observable("");


        self.bind = function (data) {

            var splitName = data.DisplayName.split(" ");

            self.name(splitName[0]);
            self.photo(data.PicLocation);
            self.reputation(data.Reputation);

        };


        self.getUser = function (id) {

            if (id >= 0) {
                this.id = id;

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





        this.link = function () {

            Boiler.UrlController.goTo("userinfo/" + id);
        }
    };

    return ViewModel;
});