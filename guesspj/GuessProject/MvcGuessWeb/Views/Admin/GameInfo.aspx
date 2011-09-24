<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Guess.Model" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=ViewData["PageTitle"].ToString() %></title>
</head>
<body>
    <div>
    <% Html.RenderAction("GameList", "CommControl"); %>
    <div style=" color:Red; font-size:24px; text-align:center;"><%=ViewData["PageTitle"]%></div>
    <% 
        DataTable dt = ViewData["ConentTable"] as DataTable;
        if (dt != null && dt.Rows.Count > 0)
        {%>
        <table cellpadding="0" cellspacing="0" border="1" width="80%" style="text-align:center;" >
        <tbody>
        <tr>
        <th>&nbsp;</th>
        <th>ID</th>
        <th>期数</th>
        <th>开奖时间</th>
        <th>数字1</th>
        <th>数字2</th>
        <th>数字3</th>
        <th>结果</th>
        <th>奖池</th>
        <th>参加人数</th>
        <th>获奖人数</th>
        <th>是否已开奖</th>
        <th>创建时间</th>
        <th>修改</th>
        <th>删除</th>
        <th>&nbsp;</th>
        </tr>
        <% foreach(DataRow dr in dt.Rows){ 
               
        %>
        <tr>
        <td>&nbsp;</td>
        <td><%=dr["F_Id"] == null ? "NULL" : dr["F_Id"].ToString()%></td>
        <td><%=dr["F_Phases"] == null ? "NULL" : dr["F_Phases"].ToString()%></td>
        <td><%=dr["F_LotteryDate"] == null ? "NULL" : dr["F_LotteryDate"].ToString()%></td>
        <td><%=dr["F_NumOne"] == null ? "NULL" : dr["F_NumOne"].ToString()%></td>
        <td><%=dr["F_NumTwo"] == null ? "NULL" : dr["F_NumTwo"].ToString()%></td>
        <td><%=dr["F_NumThree"] == null ? "NULL" : dr["F_NumThree"].ToString()%></td>
        <td>Ret:&nbsp;</td>
        <td><%=dr["F_Bonus"] == null ? "NULL" : dr["F_Bonus"].ToString()%></td>
        <td><%=dr["F_InvolvedNum"] == null ? "NULL" : dr["F_InvolvedNum"].ToString()%></td>
        <td><%=dr["F_WinningNum"] == null ? "NULL" : "0"%></td>
        <td><%=Convert.ToBoolean(dr["F_Lottery"]) ? "已开奖" : "未开奖"%></td>
        <td><%=dr["F_CreateDate"] == null ? "NULL" : dr["F_CreateDate"].ToString()%></td>
        <th><a href='GameEdit/<%=dr["F_Id"].ToString() %>' >编辑</a></th>
        <td><a  >不可删除</a></td>
        <td>&nbsp;</td>
        </tr>
        <%} %>
        </tbody>
        </table>
    <% } %>



    </div>
</body>
</html>
