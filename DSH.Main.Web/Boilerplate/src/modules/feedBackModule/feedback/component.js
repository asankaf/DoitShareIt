define(['Boiler', 'text!./view.html', './postingPanel/component', './feedbackWall/component'], function (Boiler, template, PostingPanelComp, PublicWallComp) {

    /**
    * Parent Component class that will hold the clickme and lottery components
    * @class 
    * @param moduleContext {Boiler.Context} 
    */
    var FeedbackComponent = function (moduleContext) {

        var parentPanel = null, postingPanelComp = null, publicWallComp = null;

        this.activate = function (parent, params) {
            if (!parentPanel) {
                //create the holding panel for clickme and lottery components
                parentPanel = new Boiler.ViewTemplate(parent, template, null);
                //create the clickme component and append to the parent
                postingPanelComp = new PostingPanelComp(moduleContext);
                postingPanelComp.initialize($('#feedbackPostingPanel'));
                //create lottery component and add to the parent
                publicWallComp = new PublicWallComp(moduleContext);
                publicWallComp.initialize($('#feedbackPublicWall'));
            }
            publicWallComp.refresh();
            parentPanel.show();
        };

        this.deactivate = function () {
            if (parentPanel) {
                parentPanel.hide();
            }

        };
    };

    return FeedbackComponent;

});
