define(['Boiler', './viewmodel', 'text!./view.html', 'text!../../baseModule/theme/gray/jquery.ui.autocomplete.css'],
    function(Boiler, ViewModel, template, jqueryUiAutoCompleteCss) {

        var Component = function(moduleContext) {

            var vm, panel = null;

            this.activate = function(parent, params) {
                if (!panel) {
                    panel = new Boiler.ViewTemplate(parent, template, null, jqueryUiAutoCompleteCss);
                    vm = new ViewModel(moduleContext);
                    ko.applyBindings(vm, panel.getDomElement());
                }
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