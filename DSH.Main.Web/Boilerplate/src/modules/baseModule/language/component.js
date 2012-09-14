define(['require', 'Boiler', 'text!./view.html'], function (require, Boiler, template) {

    var Component = function (moduleContext) {
        var panel = null;
        return {
            activate: function (parent) {
                if (this.id) {
                    moduleContext.setLanguage(this.id);
                }
            },

            deactivate: function () {
            }
        };
    };

    return Component;

});
