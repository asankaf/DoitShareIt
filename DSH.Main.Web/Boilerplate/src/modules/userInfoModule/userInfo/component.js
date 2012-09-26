define(['Boiler', 'text!./view.html', './userinfo/component'], function (Boiler, template, userInfoComponent) {

    /**
    * Parent Component class that will hold the userInfo components
    * @class 
    * @param moduleContext {Boiler.Context} 
    */
    var ClickCounterComponent = function (moduleContext) {

        var parentPanel = null;

        this.activate = function (parent, params) {
            if (!parentPanel) {
                //create the holding panel for userInfo components
                parentPanel = new Boiler.ViewTemplate(parent, template, null);
                
                //create userInfo component and add to the parent
                var publicWallComp = new userInfoComponent(moduleContext);
                publicWallComp.initialize($('#userinfo'));
            }
            parentPanel.show();
        }

        this.deactivate = function () {
            if (parentPanel) {
                parentPanel.hide();
            }

        }
    };

    return ClickCounterComponent;

});
