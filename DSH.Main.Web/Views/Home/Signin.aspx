﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>SignIn</title>
    <link href="../../Boilerplate/src/modules/baseModule/theme/gray/reset.css" rel="stylesheet"
        type="text/css" />
    <link href="../../Boilerplate/src/modules/baseModule/theme/style/style.css" rel="stylesheet"
        type="text/css" />
        
    

 <script src="../../Boilerplate/libs/jquery/jquery-min.js" type="text/javascript"></script>
 
<%--
    <script type="text/javascript">

        var width = $("html").width();
//        console.log(width);
        if (width > 1422) {
            // if width > 1422 then remove the backgroud image
            $("#login_page_body").css();
        }

    </script>--%>
 
 
 
 
 

    <script type="text/javascript" src="http://platform.linkedin.com/in.js">
        api_key: dul1h8n5j6s2;
        authorize: false;
        onLoad: onLinkedInLoad;
    </script>

        <script type="text/javascript">
            // 2. Runs when the JavaScript framework is loaded
            function onLinkedInLoad() {
                IN.Event.on(IN, "auth", onLinkedInAuth);
            }

            // 2. Runs when the viewer has authenticated
            function onLinkedInAuth() {
                IN.API.Profile("me")
                    .fields("id","firstName", "lastName","pictureUrl", "publicProfileUrl")
                    .result(displayProfiles);
            }

            // 2. Runs when the Profile() API call returns successfully
            function displayProfiles(profiles) {
                member = profiles.values[0];
                // document.getElementById("profiles").innerHTML =
                // "<p id=\"" + member.id + "\">Hello " + member.firstName + " " + member.lastName +" "+member.publicProfileUrl+ "</p>";

                


                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "Home/login",
                    data: { UserUniqueid:member.id, DisplayName: member.firstName +" "+ member.lastName, PublicProfileUrl:member.publicProfileUrl,PicLocation:member.pictureUrl },
                    
                }).done( function (data) {
                    document.location.href = 'Home/Index';
                      
                });
            }

         
            

        </script>

</head>
<body id="login_page_body">
    <div class="login_logo">

    </div>
    <!-- 3. Displays a button to let the viewer authenticate -->
    <div class="login_button_box rounded-corners">
    <div class="login_button">
        
            <script  type="IN/Login" ></script>
            

            <script type="text/javascript"> 
                            /* give me a fake log in from here */
                alert("Fake login");

            $.ajax({
                cache: false,
                type: "POST",
                url: "Home/login",
                data: {UserUniqueid:"szaF9K6odR", PublicProfileUrl:"http://www.linkedin.com/in/roshandhananajaya", 
                DisplayName:"Roshan Dhananajaya", PicLocation:"its getting it from the database coz im not  a new user"},                    
            }).done(function(data) {
                document.location.href = 'Home/Index';

            });
            /* end of fake login */
           
            
            </script>
        
    </div>
    </div>

<!-- 4. Placeholder for the greeting -->
<div id="profiles"></div>

<div class="login_footer">
    <p>99xTechnology.com</p> 
    <p>Some Message or something goes here: this is a mock message  Not all browsers support RGBa, so if the design permits, you should declare a "fallback" color. </p>   
    

</div>

</body>
</html>



