<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>SignIn</title>
    <!-- import jquery library-->
    <script src="../../Boilerplate/libs/jquery/jquery-min.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://platform.linkedin.com/in.js">
        api_key: dul1h8n5j6s2
        authorize: false
        onLoad: onLinkedInLoad
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
<body>
    <!-- 3. Displays a button to let the viewer authenticate -->
    <div style="position:relative; top:535px;left:592px">
        
            <script  type="IN/Login" ></script>
        
    </div>

    <img src="../../Content/login.jpg" />
    


<!-- 4. Placeholder for the greeting -->
<div id="profiles"></div>


</body>
</html>



