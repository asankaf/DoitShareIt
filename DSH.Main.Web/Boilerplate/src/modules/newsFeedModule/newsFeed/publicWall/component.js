define(['Boiler', './viewmodel', 'text!./view.html'], function (Boiler, ViewModel, template) {

    var Component = function (moduleContext) {

        var vm, panel = null;

        this.initialize = function (parent) {
            if (!panel) {
                panel = new Boiler.ViewTemplate(parent, template, null);
                vm = new ViewModel(moduleContext);
                ko.applyBindings(vm, panel.getDomElement());

                vm.loadPosts();

                //Auto refreshing every 120 seconds
                setInterval(function () {
                    vm.loadNewPosts();
                }, 120000);
            }
        };
    };

    return Component;

}); 