define([], function () {

    var comment = function(context) {
        var self = this;
        self.id = "";
        self.body = ko.observable();
        self.score = ko.observable(0);
        self.picUrl = ko.observable("");
        self.ownerDisplayName = ko.observable("");
        self.voteUpComment = function() {
            $.ajax({
                async: false,
                type: "GET",
                url: "/Vote/UpVoteComment",
                data: { commentId: self.id },
                success: function(result) {
                    if (result.Status == "SUCCESS") {
                        self.score(result.Result);
                    } else {
                      /*  //=======================//
                        $('#msgbox').html(result.Result);
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
                        //=======================// */

                        $.msgBox({
                            title: "DoitShareit",
                            content: result.Result,
                            type: "info"
                        });

                    }
                }
            });
        };
    };
    return comment;
});