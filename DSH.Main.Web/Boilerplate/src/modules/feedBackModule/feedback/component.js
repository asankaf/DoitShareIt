define(['Boiler', 'text!./view.html', './postingPanel/component', './feedbackWall/component'], function (Boiler, template, PostingPanelComp, PublicWallComp) {

    /**
    * Parent Component class that will hold the clickme and lottery components
    * @class 
    * @param moduleContext {Boiler.Context} 
    */
    var feedbackComponent = function (moduleContext) {

        var parentPanel = null, publicWallComp = null;

        this.activate = function (parent) {
            if (!parentPanel) {
                parentPanel = new Boiler.ViewTemplate(parent, template, null);
                publicWallComp = new PublicWallComp(moduleContext);
                publicWallComp.initialize($('#feedbackPublicWall'));
            }
            parentPanel.show();
        };

        this.deactivate = function () {
            if (parentPanel) {
                parentPanel.hide();
            }

        };
    };

    return feedbackComponent;

});
