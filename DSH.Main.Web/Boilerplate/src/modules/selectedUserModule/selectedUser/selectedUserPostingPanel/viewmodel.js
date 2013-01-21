define([], function () {
    var viewModel = function (moduleContext, id) {
        var self = this;
        var isAnonymous = false;
        //  var selected_id = 0;

        this.postText = ko.observable("");

        this.picLocation = ko.observable("");

        // getting the profile picture url from the display module
        this.picUrl = moduleContext.retreiveObject("profilePicURL");

        //        this.anonymousAvater = "../../Boilerplate/src/modules/baseModule/theme/gray/bullet1.png";
        this.anonymousAvater = "../../Content/anonymous-avatar.jpg";

        // the posting panel will show the pic of you as default: ( default is non anonymous posting) 
        this.picLocation(this.picUrl);

        this.makePost = function (formElement) {
            console.log(id);
            if (formElement.elements["post"].value == '') {
                //=======================//
                $('#msgbox').html('Please enter a valid message');
                $('#msgbox').dialog({

                    open: function (event, ui) {

                        $(".ui-dialog-titlebar").hide();
                        setTimeout(function () {
                            $('#msgbox').dialog('close');
                        }, 3000);
                    },

                    show: "highlight",
                    hide: "highlight",
                    height: "70",
                    width: "500",
                    position: [$('#msgbox').offset().left + 400, $('#msgbox').offset().top]

                });
                //=======================//

            } else {
                $.ajax({
                    type: "POST",
                    url: "/Post/Create",
                    dataType: "json",
                    data: {
                        Body: $('<div/>').text(formElement.elements["post"].value).html(),
                        PostTypeId: 2,
                        TaggedUserId: id,
                        IsAnonymous: isAnonymous
                    },
                    success: function (result) {
                        if (result.Status == "SUCCESS") {
                            self.postText("");
                            moduleContext.notify('NEW_POST', result.Result.Data);
                            //alert("Success!!!!");

                        }

                        if (result.Status == "ANONYMOUS_SUCCESS") {
                            // moduleContext.notify('NEW_POST', result.Result.Data);
                            self.postText("");
                          //=======================//
                        $('#msgbox').html('Successfully sent your anonymous feedback');
                        $('#msgbox').dialog({
                         
                        open: function(event, ui) {

                            $(".ui-dialog-titlebar").hide();
                                 setTimeout(function(){
                                 $('#msgbox').dialog('close');}, 3000);},
                        
                        show: "highlight",
                        hide: "highlight",
                        height: "70",
                        width: "500",
                        position: [ $('#msgbox').offset().left+ 400, $('#msgbox').offset().top]
                         
                        });
                        //=======================//



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
