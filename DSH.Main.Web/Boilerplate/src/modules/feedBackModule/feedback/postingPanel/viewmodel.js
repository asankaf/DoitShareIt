define([], function () {
    var ViewModel = function (moduleContext) {
        var self = this;
        self.postText = ko.observable("");
        self.isAnonymous = ko.observable(false);
        this.makePost = function (formElement) {
            if (formElement.elements["post"].value == '') {
                alert('please enter a valid message');
            } else {
                $.ajax({
                    type: "POST",
                    url: "/Post/Create",
                    dataType: "json",
                    data: { Body: $('<div/>').text(formElement.elements["post"].value).html(), PostTypeId: 2, IsAnonymous: self.isAnonymous },
                    success: function (result) {
                        if (result.Status == "SUCCESS") {
                            moduleContext.notify('NEW_POST', result.Result.Data);
                        }
                    }
                });
            }
        };

    };

    return ViewModel;
});
