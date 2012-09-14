define(['Boiler', './settings', './clickCounter/component'], function(Boiler, settings, LotteryComponent) {

	var Module = function(globalContext) {

		var context = new Boiler.Context(globalContext);
		context.addSettings(settings);

		var controller = new Boiler.UrlController($(".appcontent"));
		controller.addRoutes( {
			'lotterynew' : new LotteryComponent(context)
		});
		controller.start();

	};

	return Module;

});
