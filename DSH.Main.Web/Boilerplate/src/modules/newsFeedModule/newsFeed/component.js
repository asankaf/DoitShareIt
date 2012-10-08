define(['Boiler', 'text!./view.html', './postingPanel/component', './publicWall/component'], function (Boiler, template, PostingPanelComp, PublicWallComp) {

    /**
    * Parent Component class that will hold the clickme and lottery components
    * @class 
    * @param moduleContext {Boiler.Context} 
    */
    var ClickCounterComponent = function (moduleContext) {

        var parentPanel = null;

        this.activate = function (parent, params) {
            if (!parentPanel) {
                //create the holding panel for clickme and lottery components
                parentPanel = new Boiler.ViewTemplate(parent, template, null);
                //create the clickme component and append to the parent
                var postingPanelComp = new PostingPanelComp(moduleContext);
                postingPanelComp.initialize($('#postingPanel'));
                //create lottery component and add to the parent
                var publicWallComp = new PublicWallComp(moduleContext);
                publicWallComp.initialize($('#publicWall'));
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
