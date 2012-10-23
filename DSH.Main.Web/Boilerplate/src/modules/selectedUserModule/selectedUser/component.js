
define(['Boiler', './viewmodel', 'text!./view.html', './selectedUserPostingPanel/component', './selectedUserWall/component', '../../userInfoModule/userInfo/component'], function (Boiler, ViewModel, template, SelectedUserPostingPanel, SelectedUserWall, UserInfo) {


    /**
    * Parent Component class that will hold the clickme and lottery components
    * @class 
    * @param moduleContext {Boiler.Context} 
    */
    var ClickCounterComponent = function (moduleContext) {

        var parentPanel, vm = null;
        var selectedUserWall;
        var selectedUserPostingPanel;

        this.activate = function (parent, params) {
            if (!parentPanel) {


                //create the holding panel for selectedUserPostingPanel and selectedUserWall components
                parentPanel = new Boiler.ViewTemplate(parent, template, null);
                vm = new ViewModel(moduleContext);
                ko.applyBindings(vm, parentPanel.getDomElement());
                //create the postingPanelComp UI component and append to the parent
                selectedUserPostingPanel = new SelectedUserPostingPanel(moduleContext);

                //create publicWallComp component and add to the parent
                selectedUserWall = new SelectedUserWall(moduleContext);
                selectedUserWall.initialize($('#selectedUserWall'), params.id);
                selectedUserPostingPanel.initialize($('#selectedUserPostingPanel'), params.id);
                //console.log(params.id);

            }

            if (vm.getUser(0).Id != params.id) {
                vm.flag = true;
                selectedUserPostingPanel.close();
                selectedUserPostingPanel = new SelectedUserPostingPanel(moduleContext);
                selectedUserPostingPanel.initialize($('#selectedUserPostingPanel'), params.id);
                selectedUserPostingPanel.open();


            } else {
                if (selectedUserPostingPanel.check() == true) {

                    vm.flag = false;
                    selectedUserPostingPanel.close();

                }

            }

            vm.getUser(params.id);
            selectedUserWall.loadPosts(params.id);
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
