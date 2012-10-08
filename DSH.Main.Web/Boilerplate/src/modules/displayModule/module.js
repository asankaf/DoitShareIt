define(['Boiler', './settings', './display/component'], function (Boiler, settings, Display) {

    var Module = function (globalContext) {

        var context = new Boiler.Context("display", globalContext);
        context.addSettings(settings);

        var controller = new Boiler.DomController($('#page-content'));
        controller.addRoutes({
            '.user': new Display(context)

        });
        controller.start();

    };

    return Module;

});
