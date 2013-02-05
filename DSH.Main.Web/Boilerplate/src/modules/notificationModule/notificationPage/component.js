﻿define(['Boiler', './viewmodel', 'text!./view.html'], function (Boiler, ViewModel, template) {

    var component = function (moduleContext) {

        var vm, panel = null;

        this.activate = function (parent) {
            //if (!panel) {
                panel = new Boiler.ViewTemplate(parent, template, null);
                vm = new ViewModel(moduleContext,Boiler);
                ko.applyBindings(vm, panel.getDomElement());
            //}
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