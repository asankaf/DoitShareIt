define(['./Comment'], function (Comment) {

    var post = function (context) {
        var self = this;
        self.id = "";
        self.body = ko.observable();
        self.comments = ko.observableArray();
        self.score = ko.observable(0);
        self.picUrl = ko.observable("");
        self.ownerDisplayName = ko.observable("");
        self.isAnonymous = ko.observable("");
        self.title = ko.observable("");

        self.commentText = ko.observable("");

        self.registerEvents = function () {
            self.moduleContext.listen("NEW_COMMENT", function (c) {
                if (self.id == c.ParentId && !self.IsAnonymous) {
                    var comment = new Comment(self.moduleContext);
                    comment.body(c.Body);
                    comment.score(c.Score);
                    comment.id = c.Id;
                    comment.ownerDisplayName = c.OwnerDisplayName;
                    comment.picUrl(c.OwnerPicUrl);
                    self.comments.push(comment);
                }
            });
        };

        self.voteUpPost = function () {
            $.ajax({
                async: false,
                type: "GET",
                url: "/Vote/UpVotePost",
                data: { postId: self.id },
                success: function (result) {
                    if (result.Status == "SUCCESS") {
                        self.score(result.Result);
                    } else {
                        $.msgBox({
                            title: "Ooops",
                            content: "You cannot upvote this post",
                            type: "error",
                            buttons: [{ value: "Ok"}],
                            afterShow: function (result) { }
                        });
                        
                     }
                }
            });
        };

        self.voteDownPost = function () {
            if (self.score() == 0) {

                $.msgBox({
                    title: "Ooops",
                    content: "You cannot down vote this post",
                    type: "error",
                    buttons: [{ value: "Ok"}],
                    afterShow: function (result) { }
                });

            /*     $('#msgbox').html('You cannot down vote this post');
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

                }); */

            } else {
                $.ajax({
                    async: false,
                    type: "GET",
                    url: "/Vote/DownVotePost",
                    data: { postId: self.id },
                    success: function (result) {
                        if (result.Status == "SUCCESS") {
                            self.score(result.Result);
                        } else {

                            $.msgBox({
                                title: "Ooops",
                                content: result.Result,
                                type: "error",
                                buttons: [{ value: "Ok"}],
                                afterShow: function (result) { }
                            });



                            //=======================//
                          /*  $('#msgbox').html(result.Result);
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

                            }); */
                            //=======================//
                        }
                    }
                });
            }
        };

        self.removeComment = function (data, event) {
            $.ajax({
                async: false,
                type: "DELETE",
                url: "/Comment/Destroy",
                data: { commentId: data.id },
                success: function (result) {
                    if (result.Status == "SUCCESS") {
                        self.comments.remove(data);
                    }
                }
            });
        };

        self.addComment = function (data, event) {

            if (data.commentText().length < 1) {

                //=======================//
                $.msgBox({
                    title: "Ooops",
                    content: "You cannot post empty comments",
                    type: "error",
                    buttons: [{ value: "Ok"}],
                    afterShow: function (result) { }
                });




           /*     $('#msgbox').html('You cannot post empty comments');
                $('#msgbox').dialog({

                    open: function (event, ui) {

                        $(".ui-dialog-titlebar").hide();
                        setTimeout(function () {
                            $('#msgbox').dialog('close');
                        }, 3000);
                    },

                    show: "highlight",
                    hide: "highlight",
                    height: "75",
                    width: "500",
                    position: [$('#msgbox').offset().left + 400, $('#msgbox').offset().top]

                }); */
                //=======================//


            } else {

                $.ajax({
                    async: false,
                    type: "POST",
                    url: "/Comment/Create",
                    data: { Body: data.commentText(), ParentId: data.id, PostTypeId: 1 },
                    success: function (result) {

                        if (result.Status == "SUCCESS") {
                            var c = result.Result.Data;
                            var c2 = new Comment();
                            c2.id = c.Id;
                            c2.body = c.Body;
                            c2.score = c.Score;
                            c2.ownerDisplayName = c.OwnerDisplayName;
                            c2.picUrl = c.OwnerPicUrl;
                            data.comments.push(c2);
                        }
                        else {
                            var errMessageCmnt = new Comment();
                            errMessageCmnt.body = "Could not post that comment to server!";
                            data.comments.push(errMessageCmnt);

                            setInterval(function () { data.comments.remove(errMessageCmnt); }, 5000);

                        }
                    }
                });
                data.commentText('');
            }
        };
    };

    return post;

});