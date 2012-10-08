define(['Boiler', './settings', './display/component'], function (Boiler, settings, Display) {

    var Module = function (globalContext) {

        var context = new Boiler.Context(globalContext);
        context.addSettings(settings);

        //the landing page should respond to the root URL, so let's use an URLController toop
        var controller = new Boiler.UrlController($(".appcontent"));
        controller.addRoutes({
            "display": new Display(context)
        });
        controller.start();
    };

    return Module;

});
