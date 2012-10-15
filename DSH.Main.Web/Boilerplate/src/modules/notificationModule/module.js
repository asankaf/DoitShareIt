define(['Boiler', './settings', './notification/component'], function (Boiler, settings, NotificationComponent) {

    var module = function (globalContext) {

        var context = new Boiler.Context(globalContext);
        context.addSettings(settings);

        var controller = new Boiler.DomController($('#page-content'));
        controller.addRoutes({
            ".notification": new NotificationComponent(context)
        });
        controller.start();
    };

    return module;

});
