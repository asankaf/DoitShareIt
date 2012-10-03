define(['Boiler', './settings', './search/component'], function (Boiler, settings, Search) {

    var Module = function (globalContext) {

        var context = new Boiler.Context("search", globalContext);
        context.addSettings(settings);

        var controller = new Boiler.DomController($('#page-content'));
        controller.addRoutes({
            '.search': new Search(context)

        });
        controller.start();

    };

    return Module;

});
