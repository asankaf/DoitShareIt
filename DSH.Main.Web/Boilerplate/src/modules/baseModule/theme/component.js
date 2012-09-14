define(['Boiler', 'text!./view.html', './controller'], function (Boiler, template, Controller) {

    var Component = function (moduleContext) {

        var panel = null;

        return {
            activate: function (parent) {
                if (!panel) {
                    //create our controller that will handle user events
                    new Controller(moduleContext).init();
                }
            },

            deactivate: function () {
            }
        };
    };

    return Component;

});
