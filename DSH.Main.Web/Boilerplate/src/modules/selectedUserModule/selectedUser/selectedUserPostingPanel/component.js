define(['Boiler', './viewmodel', 'text!./view.html'], function (Boiler, ViewModel, template) {


    var Component = function (moduleContext) {

        var vm, panel = null;
        var flag = 0;



        this.initialize = function (parent) {
            if (!panel) {
                panel = new Boiler.ViewTemplate(parent, template, null);
                vm = new ViewModel(moduleContext);
                ko.applyBindings(vm, panel.getDomElement());
                flag = 1;
            }
        };

        this.close = function () {
            panel.hide();

        };

        this.open = function () {
            panel.show();

        };

        this.check = function () {
            if (flag == 1) {
                return true;
            } else {
                return false;
            }


        };
    };

    return Component;

}); 