define(['Boiler', './viewmodel', 'text!./view.html', 'path!./style.css', '../../displayModule/display/component'], function (Boiler, ViewModel, template, style, Display) {



    var Component = function (moduleContext) {

        var vm, panel = null;


        this.activate = function (parent, params) {

            if (!panel) {


                panel = new Boiler.ViewTemplate(parent, template, null);
                Boiler.ViewTemplate.setStyleLink(style);
                vm = new ViewModel(moduleContext);
                vm.id = params.id;
                console.log(params.id);
                vm.getPosts(vm.id);
                ko.applyBindings(vm, panel.getDomElement());


                var display = new Display(moduleContext);
                display.initialize($('#body'), "max", vm.id);





            }



        }

        this.deactivate = function () {
            if (panel) {
                panel.hide();
            }

        }

    };

    return Component;

}); 