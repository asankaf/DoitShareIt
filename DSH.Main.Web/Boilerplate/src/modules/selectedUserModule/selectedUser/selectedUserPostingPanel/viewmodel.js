define([], function () {
    var viewModel = function (moduleContext) {
        var self = this;
        this.postText = ko.observable("");
        this.makePost = function (formElement) {
            if (formElement.elements["post"].value == '') {
                alert('please enter a valid message');
            } else {
                $.ajax({
                    type: "POST",
                    url: "/Post/Create",
                    dataType: "json",
                    data: { Body: $('<div/>').text(formElement.elements["post"].value).html(), PostTypeId: 2 },
                    success: function (result) {
                        if (result.Status == "SUCCESS") {
                            moduleContext.notify('NEW_POST', result.Result.Data);
                        }
                    }
                });
            }
        };

    };

    return viewModel;
});
