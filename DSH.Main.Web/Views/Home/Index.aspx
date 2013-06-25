<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<!DOCTYPE html>
<html>
<head>
<meta charset="utf-8">

<!-- Addding new items from our lold index.aspx to here -->


<title>Do It Share It! 99xTechnology</title>

<!--<link rel="icon" href="favicon.ico" type="image/x-icon">-->
<link href="../../Boilerplate/src/modules/baseModule/theme/gray/reset.css" rel="stylesheet" type="text/css" />
<link href="../../Boilerplate/src/modules/baseModule/theme/style/style.css" rel="stylesheet" type="text/css" />
<!--<link rel="stylesheet" type="text/css" id="themeStylesheet" href="../../Boilerplate/src/modules/baseModule/theme/gray/common.css" �>-->
<!--<script src="../../Boilerplate/libs/jquery/jquery-min.js" type="text/javascript"></script> -->
<link href="../../Boilerplate/libs/wijmo/jquery-wijmo.css" rel="stylesheet" type="text/css" title="rocket-jqueryui" />
<script src="../../Boilerplate/libs/xing-wysihtml5-fb0cfe4/parser_rules/advanced.js" type="text/javascript"></script>
<script src="../../Boilerplate/libs/xing-wysihtml5-fb0cfe4/dist/wysihtml5-0.3.0.min.js" type="text/javascript"></script>

<script type='text/javascript' src='http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.min.js'></script>
<script src="../../Scripts/jquery.msgBox.js" type="text/javascript"></script>    
<link href="../../Content/css/msgBoxLight.css" rel="stylesheet" type="text/css" /> 
   

  
<script type="text/javascript" src="http://platform.linkedin.com/in.js">
    
    //api_key: 0wa1x7ujouu0
    api_key: <%= ConfigurationManager.AppSettings["ApiKey"] %>;
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
            
    a:hover {
        color: #33348e;
        text-decoration: none;
        cursor: inherit
    }

    .ui-dialog-titlebar { visibility: hidden; }

    #msgbox {

        background-color: #FF6600;
        color: white;
        text-align: center;
    }
            
</style>

<!-- end of pasting old code for the header -->





<meta name="description" content="">
<meta name="viewport" content="width=device-width">
<link rel="stylesheet" type="text/css" href="../../Content/css/site-forms.css">
<link rel="stylesheet" type="text/css" href="../../Content/css/site-grids.css">
<link rel="stylesheet" type="text/css" href="../../Content/css/site-pages.css">
</head>

<body>
<div class="bodyWrapper">
    
    	
    <section id="page-content">
			
			<%--<section class="notification"></section>
            <section class="search" style="font-size:25px;position:relative;left:400px"></section>
              
              <section class="user" ></section>
             
              <a class="logout" >Logout</a>
              
              

              <div id="Div1" style="display: none;">
                    <p>You will also be logged out from your Linkedin account.</p>
                    <p>Do you want to continue ?</p>
              </div>--%>
            
            
                     

    
    
    
    
    
    
    
    
    
    
    <header>
		<div class="header row">
				<div class="col_2">
						<div class="logo"></div>
				</div>
		    <div class="col_10">
		        <ul class="horizontal">
		            <li>
		                <%--<div class="icoGlobe"></div>--%>
                        <section class="notification"></section>
		            </li>
		            <li>
		               <%-- <input type="text" class="txtSearch" />
		                <a href="#" class="btnSearch"></a></li>--%>
                        <section class="search"></section>
		            <li>
		                <%--<div class="userThumb"></div>--%>
                        <section class="user" ></section>
		            </li>
		            <%--<li><a href="#" class="userName">User Name</a> <a class="logout" >Logout</a></li>--%>
		        </ul>
		    </div>
            
            
            
            
            
            
              
              
             
             
              
              

              <div id="dialog_box" style="display: none;">
                    <p>You will also be logged out from your Linkedin account.</p>
                    <p>Do you want to continue ?</p>
              </div>
		</div>
        </header>
        <div class="content">
            <div class="col_2">
                <div class="leftBar">
                    <ul>
                        <li class="active"> <a href="#">Award Ceremonies</a> </li>
                        <li> <a href="#">link two</a> </li>
                        <li> <a href="#">link three</a> </li>
                        <li> <a href="#">link four</a> </li>
                        <aside>
                            <section class="main-menu"></section>
                        </aside>
                    </ul>
                </div>
            </div>
		    
		    <section class="main-content">
		        <div class="appcontent"></div>
		    </section>
		    <aside class="right_aside">
		        <section class="side-pane"></section>
		    </aside>   

				
				<div class="col_3">
						<div class="rightBar">
								<div class="redTitle">People you may interact with</div>
								<div class="ctrlHolder">
										<div class="col_2"><img src="../../Content/Images/d2.png" width="28" height="28"></div>
										<div class="col_10"><span class="boldText">Amila De Silva</span><br>
												3 Feedbacks,  20 Votes<br>
												<br>
										</div>
								</div>
								<div class="ctrlHolder">
										<div class="col_2"><img src="../../Content/Images/d2.png" width="28" height="28"></div>
										<div class="col_10"><span class="boldText">Amila De Silva</span><br>
												3 Feedbacks,  20 Votes<br>
												<br>
										</div>
								</div>
								<div class="ctrlHolder">
										<div class="col_2"><img src="../../Content/Images/d2.png" width="28" height="28"></div>
										<div class="col_10"><span class="boldText">Amila De Silva</span><br>
												3 Feedbacks,  20 Votes<br>
												<br>
										</div>
								</div>
								<div class="ctrlHolder">
										<div class="col_2"><img src="../../Content/Images/d2.png" width="28" height="28"></div>
										<div class="col_10"><span class="boldText">Amila De Silva</span><br>
												3 Feedbacks,  20 Votes<br>
												<br>
										</div>
								</div>
						</div>
				</div>
		</div>
</section>
</div>


		<script src="../../Boilerplate/libs/knockout/knockout-2.1.0pre.js" type="text/javascript" charset="utf-8"></script>
		<script src="../../Boilerplate/libs/underscore/underscore-1.3.3.js" type="text/javascript" charset="utf-8"></script>
		<script src="../../Boilerplate/libs/signals/signals.min.js" type="text/javascript" charset="utf-8"></script>
		<script src="../../Boilerplate/libs/crossroads/crossroads.min.js" type="text/javascript" charset="utf-8"></script>
		<script src="../../Boilerplate/libs/hasher/hasher.min.js" type="text/javascript" charset="utf-8"></script>
		<script src="../../Boilerplate/libs/pubsub/pubsub-20120708.js" type="text/javascript" charset="utf-8"></script>
	    <script src="../../Boilerplate/libs/flot/jquery.flot.min.js" type="text/javascript" charset="utf-8"></script>
		<script src="../../Boilerplate/libs/flot/jquery.flot.resize.js" type="text/javascript" charset="utf-8"></script>
		<script src="../../Boilerplate/libs/amplifystore/amplify.store.min.1.1.0.js" type="text/javascript" charset="utf-8"></script>
		<script src="../../Boilerplate/libs/boilerplate/groundwork.js" type="text/javascript" charset="utf-8"></script>
        <script src="../../Boilerplate/libs/moment/moment.js" type="text/javascript" charset="utf-8"></script>
        <script src="../../Boilerplate/libs/wijmo/jquery-ui.min.js" type="text/javascript"></script>
        <script type="text/javascript" data-main="../../Boilerplate/src/main" src="../../Boilerplate/libs/require/require.js"></script>


</body>
</html>
