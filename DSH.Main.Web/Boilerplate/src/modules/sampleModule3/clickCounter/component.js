define(['Boiler', 'text!./view.html', './clickme/component'], function(Boiler, template, ClickmeComp) {

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
				//create the clickme component and append to the parent
				var clickmeComp = new ClickmeComp(moduleContext);
				clickmeComp.initialize($('#clickme'));
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
