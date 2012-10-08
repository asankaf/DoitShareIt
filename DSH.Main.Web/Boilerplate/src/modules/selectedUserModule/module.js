define(['Boiler', './settings', './selectedUser/component'], function (Boiler, settings, SelectedUser) {

    var Module = function (globalContext) {

        var context = new Boiler.Context(globalContext);
        context.addSettings(settings);

        //the landing page should respond to the root URL, so let's use an URLController toop
        var controller = new Boiler.UrlController($(".appcontent"));
        controller.addRoutes({
            "/selected": new SelectedUser(context),
            '/user/{id}': new SelectedUser(context)
        });
        controller.start();
    };

    return Module;

});
