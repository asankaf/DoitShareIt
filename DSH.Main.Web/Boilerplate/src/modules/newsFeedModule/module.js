define(['Boiler', './settings','./newsFeed/component'], function (Boiler, settings, NewsFeedComponent) {

    var Module = function (globalContext) {

        var context = new Boiler.Context(globalContext);
        context.addSettings(settings);

        //the landing page should respond to the root URL, so let's use an URLController toop
        var controller = new Boiler.UrlController($(".appcontent"));
        controller.addRoutes({
            "/" : new NewsFeedComponent(context)
        });
        controller.start();
    };

    return Module;

});
