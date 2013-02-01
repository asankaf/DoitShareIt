
define(function(require) {

	/* 
	 * Return an array containing all modules classes that needs to be initiated.
	 * We use 'require' function of requirejs to get the relevant module context classes.
	 * This could be of course done by passing those scripts as dependencies to 
	 * the 'define' function above. But following model is bit more clear to read.
	 */
	return [
        require('./baseModule/module'),
        require('./topBarModule/module'),     
        require('./newsFeedModule/module'),
        require('./feedBackModule/module'),
	    require('./userInfoModule/module'),
	    require('./selectedUserModule/module'),
        require('./searchModule/module'),
	    require('./notificationModule/module')
	];

});