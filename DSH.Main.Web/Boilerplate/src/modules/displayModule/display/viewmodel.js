define([], function () {
    var ViewModel = function (moduleContext) {
        var self = this;
        current = new Date();
        self.name = ko.observable("");
        self.photo = ko.observable("");
        self.reputation = ko.observable("");
        self.profile = ko.observable("");
        self.createdDate = ko.observable("");
        self.duration = ko.observable("");
        self.panelVisible = ko.observable(true);
        self.panel1Visible = ko.observable(true);
        self.flipVisible = ko.observable(true);


        self.bind = function (data) {

            self.name(data.DisplayName);
            self.photo(data.PicLocation);
            self.reputation(data.Reputation);
            self.createdDate(data.CreationDate);
            self.profile(data.PublicProfileUrl);

            var date = new Date(parseInt(data.CreationDate.slice(6, -2)));


            


            self.createdDate(moment(date).fromNow().slice(0,-3));




            
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






        self.display = function (size, id) {

            self.getUser(id);

            if (size == "min") {
               
                self.panelVisible(false);
                self.panel1Visible(false);
                $(document).ready(function () {

                    $("#flip").hover(function () {
                        $("#panel").slideToggle("fast");
                        document.getElementById("panel");
                    });
                });


            }

            if (size == "max") {

               
                self.flipVisible(false);
                self.panelVisible(false);
            }





        };




    };




    return ViewModel;

});


 
