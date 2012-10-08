define(['Boiler', './viewmodel', 'text!./view.html', 'text!./style.css'], function (Boiler, ViewModel, template, cssPath) {

    var Component = function (moduleContext) {

        var vm, panel = null;

        this.initialize = function (parent) {
            if (!panel) {
                panel = new Boiler.ViewTemplate(parent, template, null, cssPath);
                vm = new ViewModel(moduleContext);
                ko.applyBindings(vm, panel.getDomElement());
            }
        };

        this.refresh = function () {
            vm.loadPosts();
        };
    };

    return Component;

}); 