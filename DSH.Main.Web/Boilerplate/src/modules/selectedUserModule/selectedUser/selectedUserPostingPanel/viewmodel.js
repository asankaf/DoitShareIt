define([], function () {
    var viewModel = function (moduleContext) {
        var self = this;
        var isAnonymous = false;

        this.postText = ko.observable("");

        this.picLocation = ko.observable("");

        // getting the profile picture url from the display module
        this.picUrl = moduleContext.retreiveObject("profilePicURL");

//        this.anonymousAvater = "../../Boilerplate/src/modules/baseModule/theme/gray/bullet1.png";
        this.anonymousAvater = "../../Content/anonymous-avatar.jpg";

        // the posting panel will show the pic of you as default: ( default is non anonymous posting) 
        this.picLocation(this.picUrl);

        this.makePost = function (formElement) {
            if (formElement.elements["post"].value == '') {
                alert('please enter a valid message');
            } else {
                $.ajax({
                    type: "POST",
                    url: "/Post/Create",
                    dataType: "json",
                    data: {
                        Body: $('<div/>').text(formElement.elements["post"].value).html(),
                        PostTypeId: 2,
                        IsAnonymous: isAnonymous
                    },
                    success: function (result) {
                        if (result.Status == "SUCCESS") {
                            moduleContext.notify('NEW_POST', result.Result.Data);
                            //                            alert("Success!!!!");
                        }
                    }
                });
            }
        };

        this.anonymousToggle = function (clickedElement) {
            if (isAnonymous) {
                isAnonymous = false;
                self.picLocation(self.picUrl);
            } else {
                isAnonymous = true;
                self.picLocation(self.anonymousAvater);
            }
        };
    };

    return viewModel;
});
