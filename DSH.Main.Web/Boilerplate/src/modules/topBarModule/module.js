define(['Boiler', './settings', './topBar/component'], function (Boiler, settings, TopBar) {

    var Module = function (globalContext) {

        var context = new Boiler.Context("topBar", globalContext);
        context.addSettings(settings);

        var controller = new Boiler.DomController($('#page-content'));
        controller.addRoutes({
            '.user': new TopBar(context)

        });
        controller.start();

    };

    return Module;

});
