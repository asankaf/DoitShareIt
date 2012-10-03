define(['Boiler', './viewmodel', 'text!./view.html', '../../displayModule/display/component'], function (Boiler, ViewModel, template, Display) {

    var Component = function (moduleContext) {

        var vm, panel = null;


        this.activate = function (parent) {
            if (!panel) {
                panel = new Boiler.ViewTemplate(parent, template, null);
                vm = new ViewModel(moduleContext);
                ko.applyBindings(vm, panel.getDomElement());

                var display = new Display(moduleContext);
                display.initialize($('#body'), "large");


            }
            panel.show();

        }

        this.deactivate = function () {
            if (panel) {
                panel.hide();
            }

        }

    };

    return Component;

}); 