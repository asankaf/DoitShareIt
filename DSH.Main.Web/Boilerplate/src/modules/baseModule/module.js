define(['Boiler', './mainMenu/component', './language/component', './theme/component', './footer/component','./sidePane/component'], function(Boiler, MainMenuComponent, LanguageComponent, ThemeComponent, FooterComponent,SidePaneComponent) {

    var Module = function(globalContext) {
        var context = new Boiler.Context(globalContext);

        //scoped DomController that will be effective only on $('#page-content')
        var controller = new Boiler.DomController($('#page-content'));
        //add routes with DOM node selector queries and relavant components
        controller.addRoutes({
            ".main-menu": new MainMenuComponent(context),
            ".side-pane": new SidePaneComponent(context),
            ".language" : new LanguageComponent(context),
            ".theme" : new ThemeComponent(context),
            ".footer": new FooterComponent(context),
           // ".user" : new TopBar(context)
        });
        controller.start();
    };

    return Module;

});
