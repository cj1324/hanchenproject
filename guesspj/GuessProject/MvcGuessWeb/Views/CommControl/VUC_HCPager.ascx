<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<% String[]  btnlist=  ViewData["BtnList"] as String[]; %>
<% String nullmsg = ViewData["NullMsg"] as String;
     %>
<% if (btnlist != null)
   { %>
<dl>
    <% 
        
    foreach (String s in btnlist)
    {
        string[] arrs = s.Split('|');     
       %>
    <dd class='<%=arrs[2] %>' ><a href='<%=arrs[1] %>'><%=arrs[0]%></a></dd>
    <%} %>

</dl>
<%}else{ %><%=nullmsg??"&nbsp;" %><!-- 无数据 --><%} %>

