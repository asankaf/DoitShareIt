define([], function () {

    var ViewModel = function (moduleContext) {
        var self = this;
        this.postText = ko.observable("");
        this.makePost = function () {
            if (self.postText() == '') {
                alert('please enter a valid message');
            } else {
                $.ajax({
                    type: "POST",
                    url: "http://localhost:59214/Post/Create",
                    data: { Body: self.postText(), PostTypeId: 2 },
                    success: function (result) {
                        if (result.Status == "SUCCESS") {
                            moduleContext.notify('NEW_POST', result.Result.Data);                     
                        }
                    }
                });
                this.postText('');
            }
        };

    };

    return ViewModel;
});
