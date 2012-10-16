define(['Boiler', './viewmodel', 'text!./view.html', 'text!./style.css'], function (Boiler, ViewModel, template,cssPath) {

    var component = function (moduleContext) {

        var vm, panel = null;
        this.activate = function (parent, params) {
            if (!panel) {
                panel = new Boiler.ViewTemplate(parent, template, null,cssPath);
                vm = new ViewModel(moduleContext);
                ko.applyBindings(vm, panel.getDomElement());
            }
            panel.show();

        };

        this.deactivate = function () {
            if (panel) {
                panel.hide();
            }

        };

    };

    return component;

}); 