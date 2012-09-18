define(['Boiler', 'text!./view.html', './postingPanel/component', './demo1/component'],
    function(Boiler, template, PostingPanelComp, Demo1Component) {

        /**
    * Parent Component class that will hold the clickme and lottery components
    * @class 
    * @param moduleContext {Boiler.Context} 
    */
        var uiDemoComponent = function(moduleContext) {
            var self = this;


            var parentPanel = null;


            this.activate = function(parent, params) {
                if (!parentPanel) {
                    //create the holding panel for clickme and lottery components
                    parentPanel = new Boiler.ViewTemplate(parent, template, null);


                    /* 
                    //create the clickme component and append to the parent
                    var postingPanelComp = new PostingPanelComp(moduleContext);
                    postingPanelComp.initialize($('#postingPanel'));
                    */
                //create a demo1 component and add to the parent
                    var demo1Component = new Demo1Component(moduleContext);
                    demo1Component.initialize($('#demo1'));


                }
                parentPanel.show();
            };
            this.deactivate = function() {
                if (parentPanel) {
                    parentPanel.hide();
                }

            };
        };

        return uiDemoComponent;

    });
