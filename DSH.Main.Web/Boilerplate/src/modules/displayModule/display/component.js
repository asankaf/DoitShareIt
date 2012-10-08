define(['Boiler', './viewmodel', 'text!./view.html'], function (Boiler, ViewModel, template) {

    var Component = function (moduleContext) {

        var vm, panel = null;

        this.initialize = function (parent, params, id) {
            if (!panel) {
                panel = new Boiler.ViewTemplate(parent, template, null);
                vm = new ViewModel(moduleContext);
                vm.display(params, id);
                ko.applyBindings(vm, panel.getDomElement());
            }
            panel.show();
        };
    };

    return Component;

}); 