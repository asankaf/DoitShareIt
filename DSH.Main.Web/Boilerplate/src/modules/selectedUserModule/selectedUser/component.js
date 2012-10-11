define(['Boiler', 'text!./view.html', './selectedUserPostingPanel/component', './selectedUserWall/component', './viewmodel'], function (Boiler, template, SelectedUserPostingPanel, SelectedUserWall, ViewModel1) {

    /**
    * Parent Component class that will hold the clickme and lottery components
    * @class 
    * @param moduleContext {Boiler.Context} 
    */
    var ClickCounterComponent = function (moduleContext) {

        var parentPanel, vm1 = null;
        var selectedUserWall;
        var selectedUserPostingPanel;

        this.activate = function (parent, params) {
            if (!parentPanel) {
                //create the holding panel for selectedUserPostingPanel and selectedUserWall components
                parentPanel = new Boiler.ViewTemplate(parent, template, null);

                vm1 = new ViewModel1(moduleContext);
                ko.applyBindings(vm1, parentPanel.getDomElement());


                //create the postingPanelComp UI component and append to the parent
                selectedUserPostingPanel = new SelectedUserPostingPanel(moduleContext);
                selectedUserPostingPanel.initialize($('#selectedUserPostingPanel'));
                //create publicWallComp component and add to the parent
                selectedUserWall = new SelectedUserWall(moduleContext);
                selectedUserWall.initialize($('#selectedUserWall'), params.id);
                console.log(params.id);

            }

            vm1.getUser(params.id);
            selectedUserWall.loadPosts(params.id);
            console.log(params.id);
            parentPanel.show();
        };


        this.deactivate = function () {
            if (parentPanel) {
                parentPanel.hide();
            }

        };
    };

    return ClickCounterComponent;

});
