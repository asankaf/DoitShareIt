define(['Boiler', 'text!./view.html', './selectedUserPostingPanel/component', './selectedUserWall/component', './selectedUserWall/viewmodel'], function (Boiler, template, SelectedUserPostingPanel, SelectedUserWall, SelectedUserViewModel) {

    /**
    * Parent Component class that will hold the clickme and lottery components
    * @class 
    * @param moduleContext {Boiler.Context} 
    */
    var ClickCounterComponent = function (moduleContext) {

        var parentPanel = null;

        this.activate = function (parent, params) {
            if (!parentPanel) {
                vm = new SelectedUserViewModel(moduleContext, params.id);
                //create the holding panel for selectedUserPostingPanel and selectedUserWall components
                parentPanel = new Boiler.ViewTemplate(parent, template, null);
                //create the postingPanelComp UI component and append to the parent
                var selectedUserPostingPanel = new SelectedUserPostingPanel(moduleContext);
                selectedUserPostingPanel.initialize($('#selectedUserPostingPanel'));
                //create publicWallComp component and add to the parent
                var selectedUserWall = new SelectedUserWall(moduleContext);
                selectedUserWall.initialize($('#selectedUserWall'));
            }

            vm.getId(params.id);
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
