define(['Boiler', './viewmodel', 'text!./view.html'], function (Boiler, ViewModel, template) {

    var Component = function (moduleContext) {

        var vm, panel = null;

        this.initialize = function (parent, params) {
            if (!panel) {
                panel = new Boiler.ViewTemplate(parent, template, null);
                vm = new ViewModel(moduleContext);
                vm.display(params);
                ko.applyBindings(vm, panel.getDomElement());
            }
            
        }
    };

    return Component;

}); 