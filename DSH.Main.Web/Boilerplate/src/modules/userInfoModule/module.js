define(['Boiler', './settings', './userInfo/component'], function (Boiler, settings, userInfoComponent) {

    var Module = function (globalContext) {

        var context = new Boiler.Context(globalContext);
        context.addSettings(settings);

        //the landing page should respond to the root URL, so let's use an URLController toop
        var controller = new Boiler.UrlController($(".appcontent"));
        controller.addRoutes({

            //"user": new userInfoComponent(context),
            "userinfo/{id}": new userInfoComponent(context)

        });
        
        controller.start();
    };

    return Module;

});
