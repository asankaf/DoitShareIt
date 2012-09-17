<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>SignIn</title>
    <!-- import jquery library-->
    <script src="../../Boilerplate/libs/jquery/jquery-min.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://platform.linkedin.com/in.js">
        api_key: dul1h8n5j6s2
        authorize: true
        onLoad: onLinkedInLoad
        </script>

        <script type="text/javascript">
            // 2. Runs when the JavaScript framework is loaded
            function onLinkedInLoad() {
                IN.Event.on(IN, "auth", onLinkedInAuth);
            }

            // 2. Runs when the viewer has authenticated
            function onLinkedInAuth() {
                IN.API.Profile("me").result(displayProfiles);
            }

            // 2. Runs when the Profile() API call returns successfully
            function displayProfiles(profiles) {
                member = profiles.values[0];
                document.getElementById("profiles").innerHTML =
                "<p id=\"" + member.id + "\">Hello " + member.firstName + " " + member.lastName + "</p>";

                


                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "Login/login",
                    data: { id: member.id, name: member.firstName },
                    
                }).done( function (success) {
                        document.location.href = 'Home/Index';
                    });
            }

        </script>

</head>
<body>
    <!-- 3. Displays a button to let the viewer authenticate -->
<script type="IN/Login"></script>

<!-- 4. Placeholder for the greeting -->
<div id="profiles"></div>

</body>
</html>



