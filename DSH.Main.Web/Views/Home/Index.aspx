﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN"
"http://ww.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html id="Html1" xmlns="http://www.w3.org/1999/xhtml" lang="en" xml:lang="en" runat="server">
	<head>
		<meta http-equiv="Content-Type" content="text/html">
	
		<meta charset="utf-8">
		<title>Do It Share It! 99xTechnology</title>
		<link rel="icon" href="../../Content/favicon.ico" type="image/x-icon">
        <link href="../../Boilerplate/src/modules/baseModule/theme/gray/reset.css" rel="stylesheet"
            type="text/css" />
        <link href="../../Boilerplate/src/modules/baseModule/theme/style/style.css" rel="stylesheet"
            type="text/css" />
		<!-- optional static style definition to render fast. 
			see 'src/modules/baseModule/theme/component.js' to see dynamic stylesheet setup on themes -->
		<link rel="stylesheet" type="text/css" id="themeStylesheet" href="../../Boilerplate/src/modules/baseModule/theme/gray/common.css"  >
	

        <script src="../../Boilerplate/libs/jquery/jquery-min.js" type="text/javascript"></script>
<%--       <link href="../../Boilerplate/libs/wijmo/jquery-wijmo.css" rel="stylesheet" type="text/css" title="rocket-jqueryui" />--%>
        <!--Wijmo Widgets CSS-->
<%--        <link href="../../Boilerplate/libs/wijmo/jquery.wijmo-complete.all.2.0.0.min.css" rel="stylesheet" type="text/css" />    --%>

        
        <!--wysihtml5-->
        <script src="../../Boilerplate/libs/xing-wysihtml5-fb0cfe4/parser_rules/advanced.js" type="text/javascript"></script>
        <script src="../../Boilerplate/libs/xing-wysihtml5-fb0cfe4/dist/wysihtml5-0.3.0.min.js" type="text/javascript"></script>
        
        <%--<script type="text/javascript">
            function logout() {
                window.location = "/Home/Logout";
            }
        </script>--%>

        <script type="text/javascript" src="http://platform.linkedin.com/in.js">
            api_key: dul1h8n5j6s2;
            authorize: true;
        </script>
        <script>
            IN.Event.on(IN, "logout", function () { onLinkedInLogout(); });

            function onLinkedInLogout() {
                window.location.href = '/Home/Logout';
            }
        </script>

        <script>
            $(document).ready(function () {
                $('.logout').click(function () {
                    $('#dialog_box').dialog({
                        title: 'Logout',
                        width: 400,
                        height: 200,
                        modal: true,
                        resizable: false,
                        draggable: false,
                        closeText: '',
                        buttons: [{
                                text: 'Yes',
                                click: function () {
                                    IN.User.logout();
                                }
                            },
                            {
                                text: 'No',
                                click: function () {
                                    $(this).dialog('close');
                                }
                            }]
                    });
                });
            });


        </script>

           
        <style type="text/css" media="screen">
            
            a:hover { color:#33348e; text-decoration: none; cursor:inherit }
            
        </style>

 </head>

	<body>

	    <section id="page-content">
	        <header>
	            
		        
	            <section class="user" ></section>

	            <section class="search" ></section>

                <div class="logout"><a>Logout</a></div>  

	            <div id="dialog_box" style="display: none;">
	                <p>You will also be logged out from your Linkedin account.</p>
	                <p>Do you want to continue ?</p>
	            </div>

                 
	        </header>
            
	        <aside>
	            <section class="main-menu"></section>
	        </aside>
	        <section class="main-content">
	            <div class="appcontent"></div>
	        </section>
	        <aside>
	            <section class="side-pane"></section>
	        </aside>                        
	    </section>

		<script src="../../Boilerplate/libs/jquery/jquery-min.js" type="text/javascript" charset="utf-8"></script>
		<script src="../../Boilerplate/libs/knockout/knockout-2.1.0pre.js" type="text/javascript" charset="utf-8"></script>
		<script src="../../Boilerplate/libs/underscore/underscore-1.3.3.js" type="text/javascript" charset="utf-8"></script>
		<script src="../../Boilerplate/libs/signals/signals.min.js" type="text/javascript" charset="utf-8"></script>
		<script src="../../Boilerplate/libs/crossroads/crossroads.min.js" type="text/javascript" charset="utf-8"></script>
		<script src="../../Boilerplate/libs/hasher/hasher.min.js" type="text/javascript" charset="utf-8"></script>
		<script src="../../Boilerplate/libs/pubsub/pubsub-20120708.js" type="text/javascript" charset="utf-8"></script>
		<script src="../../Boilerplate/libs/jquery/jstree/jstree-1.0-rc3.js" type="text/javascript" charset="utf-8"></script>
		<script src="../../Boilerplate/libs/flot/jquery.flot.min.js" type="text/javascript" charset="utf-8"></script>
		<script src="../../Boilerplate/libs/flot/jquery.flot.resize.js" type="text/javascript" charset="utf-8"></script>
		<script src="../../Boilerplate/libs/amplifystore/amplify.store.min.1.1.0.js" type="text/javascript" charset="utf-8"></script>
		<script src="../../Boilerplate/libs/boilerplate/groundwork.js" type="text/javascript" charset="utf-8"></script>

<%--                    <!--Wijmo Widgets JavaScript-->
        <script src="../../Boilerplate/libs/wijmo/jquery-ui.min.js" type="text/javascript"></script>
        <script src="../../Boilerplate/libs/wijmo/jquery.wijmo-complete.all.2.0.0.min.js" type="text/javascript"></script>
        <script src="../../Boilerplate/libs/wijmo/jquery.wijmo-open.all.2.0.0.min.js" type="text/javascript"></script>
        <script src="../../Boilerplate/libs/wijmo/knockout.wijmo.js" type="text/javascript"></script>--%>
  
        <script src="../../Boilerplate/libs/jquery/jquery-ui-custom.min.js" type="text/javascript"></script>

        <script type="text/javascript" data-main="../../Boilerplate/src/main" src="../../Boilerplate/libs/require/require.js"></script>
	</body>
</html>

