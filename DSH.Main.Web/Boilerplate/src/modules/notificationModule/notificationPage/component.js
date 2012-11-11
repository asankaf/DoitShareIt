define(['Boiler', './viewmodel', 'text!./view.html', 'path!./style.css'], function (Boiler, ViewModel, template,style) {

    var component = function (moduleContext) {

        var vm, panel = null;

        this.activate = function (parent) {
            if (!panel) {
                panel = new Boiler.ViewTemplate(parent, template, null);
                Boiler.ViewTemplate.setStyleLink(style);
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