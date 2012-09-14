define(['Boiler', 'text!./view.html', './lottery/component'], function(Boiler, template, LotteryComp) {

	/**
	 * Parent Component class that will hold the clickme and lottery components
	 * @class 
	 * @param moduleContext {Boiler.Context} 
	 */
	var ClickCounterComponent = function(moduleContext) {

		var parentPanel = null;

		this.activate = function(parent, params) {
			if (!parentPanel) {
				//create the holding panel for clickme and lottery components
				parentPanel = new Boiler.ViewTemplate(parent, template, null);
				//create lottery component and add to the parent
				var lotteryComp = new LotteryComp(moduleContext);
				lotteryComp.initialize($('#lottery'));
			}
			parentPanel.show();
		}

		this.deactivate = function() {
			if (parentPanel) {
				parentPanel.hide();
			}

		}
	};

	return ClickCounterComponent;

});
