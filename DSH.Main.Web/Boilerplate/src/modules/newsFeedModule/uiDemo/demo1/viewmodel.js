define([], function () {


    var ViewModel = function (moduleContext) {
        var self = this;


        self.val = ko.observable(50);
        self.disabled = ko.observable(false);
        

    };

    return ViewModel;
});
