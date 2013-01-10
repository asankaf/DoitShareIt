define(['Boiler', 'text!./view.html', './publicWall/component'], function (Boiler, template, PublicWallComp) {

    /**
    * Parent Component class that will hold the clickme and lottery components
    * @class 
    * @param moduleContext {Boiler.Context} 
    */
    var publicWall = function (moduleContext) {

        var parentPanel = null;

        this.activate = function (parent, params) {
            if (!parentPanel) {
                //create the holding panel for clickme and lottery components
                parentPanel = new Boiler.ViewTemplate(parent, template, null);
                var publicWallComp = new PublicWallComp(moduleContext);
                publicWallComp.initialize($('#publicWall'));
            }
            parentPanel.show();
        };

        this.deactivate = function () {
            if (parentPanel) {
                parentPanel.hide();
            }

        };
    };

    return publicWall;

});
