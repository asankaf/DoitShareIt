<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN"
"http://ww.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html id="Html1" xmlns="http://www.w3.org/1999/xhtml" lang="en" xml:lang="en" runat="server">
<head>
<%--<script src="jquery.js"></script>--%>
<script>
    $(document).ready(function () {
        $("#flip").hover(function () {
            $("#panel").slideToggle("fast");
            document.getElementById("panel");
        });
    });
</script>


 
<style type="text/css"> 
#flip
{
padding:5px;
height:58px;
text-align:center;

}
#panel
{
padding:5px;
display:none;
width:250px;
height:auto;
font-size:medium;
background-color:White;
border:5px solid Silver ;

}
</style>
</head>
<body>
 

<a id="flip"><img style="width:58px;height:58px" data-bind="attr:{src:photo}"/> <span data-bind="text:name"></span></a>
<div id="panel">
    <table style="border-collapse:collapse"><tr><td><img style="width:58px;height:58px" data-bind="attr:{src:photo}"/></td><td style="font-size:large"><p data-bind="text:name"></p></td></tr>
    <tr ><td style="font-size:x-large" ><p data-bind="text:reputation"></p> </td>
    <tr></tr>
    <tr><td><p>Reputation</p></td></tr>
    
    </tr>
    
    </table>
    
    
</div>

</body>
</html>