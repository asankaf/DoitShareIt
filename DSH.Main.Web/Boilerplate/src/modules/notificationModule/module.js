define(['Boiler', './settings', './notification/component','./notificationPage/component'], function (Boiler, settings, Notification,NotificationPage) {

    var module = function (globalContext) {

        var context = new Boiler.Context(globalContext);
        context.addSettings(settings);

        var controller = new Boiler.DomController($('#page-content'));
        controller.addRoutes({
            ".notification": new Notification(context),
        });
        controller.start();
        
        var urlController = new Boiler.UrlController($(".appcontent"));
        urlController.addRoutes({
            "Notifications": new NotificationPage(context)
        });
        urlController.start();
    };

    return module;

});
