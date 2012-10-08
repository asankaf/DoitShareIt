﻿define([], function () {
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


            var d = date.getDate();
            var day = (d < 10) ? '0' + d : d;
            var m = date.getMonth() + 1;
            var month = (m < 10) ? '0' + m : m;
            var yy = date.getYear();
            var year = (yy < 1000) ? yy + 1900 : yy;


            self.createdDate(" " + year + " . " + month + " . " + day + " ");




            // Duration Calculations

            //            self.duration((current.getFullYear() - d.getFullYear()) + " years " + (current.getMonth() - d.getMonth()) + " months" + (current.getDate() - d.getDate()) + "Days" + (current.getMinutes()) + "minutes");

            //            years = current.getFullYear() - d.getFullYear();
            //            months = current.getMonth() - d.getMonth();
            //            days = current.getDate() - d.getDate();
            //            hours = current.getHours - d.getHours();
            //            minutes = current.getMinutes() - d.getMinutes();
            //            seconds = current.getSeconds() - d.getSeconds();

            //            var period;

            //            if (days > 1) {
            //                period = days + " days ";
            //            } else if (days == 1) {
            //                period = days + " day ";
            //            } else {
            //                if (days > 0) {
            //                    if (hours > 1) {
            //                        period += hours + " hours ";
            //                    } else if (hours == 1) {
            //                        period += hours + " hour ";
            //                    } else { 
            //                        
            //                    }
            //                }
            //            }

        };


        self.getUser = function (id) {

            if (id != null) {

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



        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "/Home/Userprofile"

        }).done(function (data) {
            self.bind(data);
        });


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


 
