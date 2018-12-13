<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetFilesHash.aspx.cs" Inherits="QSDMS.Application.Web.HashSite.GetFilesHash" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../Content/scripts/jquery/jquery-1.10.2.min.js"></script>
    <script>
        function callJson() {
            $.ajax(
              {
                  url: '/HashSite/GetFilesHash256.ashx',
                  context: document.body,
                  success: function (str) {
                      $("#lt").html(str);
                  }
              });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <a href="javascript:callJson();" style="margin: 20px;">获取站点所有文件的哈希码</a><br />
        <span id="lt"></span>
    </form>
</body>
</html>
