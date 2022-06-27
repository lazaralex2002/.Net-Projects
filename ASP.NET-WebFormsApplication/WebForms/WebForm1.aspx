<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebForms.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"   
              EnablePageMethods="True">
        </asp:ScriptManager>
         <div>
             <asp:Button ID="Button1"
                 Text="AJAX METHOD CALL"
                 OnClientClick="BindTreeView()"
                 runat="server" />
             <asp:Button ID="Button2"
                 Text="PAGE METHOD JS CALL"
                 OnClientClick="fun()"
                 runat="server" />
             <asp:Button ID="Button3"
                 Text="XML FORMAT PAGE ELEMENTS"
                 OnClientClick="GetRss()"
                 runat="server" />
         </div>
        <div id="divOutput">

        </div>
    </form>
</body>
<script type="text/javascript">
    function BindTreeView()
    {
        $.ajax({
            type: "POST",
            url: "WebForm1.aspx/test",
            data: '{ querytype: "1" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessTree,
            failure: function (response) {
                alert("failure");
            }
        });
    }
    function OnSuccessTree(response) {
        console.log(response.d)
        alert("entered")
        if (response.d.length > 0) {
            alert("works")
        }
        return false;
    }

    function fun() {
        PageMethods.GetStatus(onSucceed, onError);
        return false;
    }

    function onSucceed(result)
    {
        alert(result);
    }

    //CallBack method when the page call fails due to interna, server error
    function onError(result) {
        alert(result.get_message());
    }

    function GetRss() {
        InterfaceTraining.DemoService.GetRssFeed(
            "https://blogs.interfacett.com/dan-wahlins-blog/rss.xml",
            OnWSRequestComplete);
    }

    function OnWSRequestComplete(result) {
        if (document.all) //Filter for IE DOM since other browsers are limited
        {
            var items = result.selectNodes("//item");
            for (var i = 0; i < items.length; i++) {
                var title = items[i].selectSingleNode("title").text;
                var href = items[i].selectSingleNode("link").text;
                $get("divOutput").innerHTML +=
                    "<a href='" + href + "'>" + title + "</a><br/>";
            }
        }
        else {
            $get("divOutput").innerHTML = "RSS only available in IE5+";
        }
    }
</script>
</html>
