define(['Boiler', './settings', './singlePost/component'], function (Boiler, settings, SinglePost) {

    var Module = function (globalContext) {

        var context = new Boiler.Context(globalContext);
        context.addSettings(settings);

        //the landing page should respond to the root URL, so let's use an URLController toop
        var controller = new Boiler.UrlController($(".appcontent"));
        controller.addRoutes({
            "/singlepost/{id}" : new SinglePost(context)
        });
        controller.start();

    };

    return Module;

});