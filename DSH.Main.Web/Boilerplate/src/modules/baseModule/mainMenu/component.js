define(['require', 'Boiler', 'text!./view.html','./viewmodel','text!./style.css'], function(require, Boiler, template,ViewModel,cssPath) {

	var Component = function(moduleContext) {
		var panel = null;
		return {
			activate : function(parent) {
				if (!panel) {
					panel = new Boiler.ViewTemplate(parent, template, null);
					/* we use static method to attach the css as a separate link on head.
					 * If we pass CSS as a text parameter to above constructor, that goes as a inline
					 * CSS text on HTML, that makes the relative paths in CSS (images, etc) difficult to manage.
					 */
					//Boiler.ViewTemplate.setStyleLink(cssPath);
					vm = new ViewModel(moduleContext);
					ko.applyBindings(vm, panel.getDomElement());
				}
				panel.show();
			},

			deactivate : function() {
				if (panel) {
					panel.hide();
				}
			}
		};
	};

	return Component;

});
