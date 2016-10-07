<%@ Page Language="C#" %>
<%@ Import Namespace="System.Web.Security" %>
<html>
<body>
<form runat="server">
  <h3><font face="Verdana">Login Page</font></h3>
  <table>
    <tr>
      <td>Username:</td>
      <td><asp:TextBox id="Username" type="text" runat="server"/></td>
    </tr>
    <tr>
      <td>Password:</td>
      <td><asp:TextBox id="Password" type=password runat="server"/></td>
    </tr>
  </table>
</form>
</body>
</html>
