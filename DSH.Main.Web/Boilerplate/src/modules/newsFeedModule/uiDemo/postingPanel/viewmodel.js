define([], function () {

    var ViewModel = function (moduleContext) {
        var self = this;
        this.postText = ko.observable();
        this.makePost = function () {
            if (self.postText() == '') {
                alert('please enter a valid message');
            } else {
                moduleContext.notify('NEW_POST', self.postText());
                this.postText('');
            }
        };

    };

    return ViewModel;
});
