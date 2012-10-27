define(['Boiler', './viewmodel', 'text!./view.html', 'path!./style.css', '../../displayModule/display/component'], function (Boiler, ViewModel, template, style, Display) {



    var Component = function (moduleContext) {

        var vm, panel, display = null;


        this.activate = function (parent, params) {

            if (!panel) {
                panel = new Boiler.ViewTemplate(parent, template, null);
                Boiler.ViewTemplate.setStyleLink(style);
                vm = new ViewModel(moduleContext);
                ko.applyBindings(vm, panel.getDomElement());
                display = new Display(moduleContext);

            }
            vm.getPosts(params.id);
            display.activate($('#body'), "max", params.id);
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