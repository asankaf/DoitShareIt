﻿define(['Boiler', './viewmodel', 'text!./view.html'], function (Boiler, ViewModel, template) {

    var Component = function (moduleContext) {

        var vm, panel = null;

        this.activate = function (parent, params, id) {
            if (!panel) {
                panel = new Boiler.ViewTemplate(parent, template, null);
                vm = new ViewModel(moduleContext);
                ko.applyBindings(vm, panel.getDomElement());
            }
            vm.display(params, id);
            panel.show();
        };

        this.deactivate = function() {
            if (panel) {
                panel.hide();
            }
        };
    };

    return Component;

}); 