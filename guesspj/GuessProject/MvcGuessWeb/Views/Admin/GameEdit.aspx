<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Guess.Model" %>
<%@ Import Namespace="Guess.Common" %>
<%
    T_Game tg=null;
    if (ViewData["GameInfo"] != null)
    {
        tg = ViewData["GameInfo"] as T_Game;
    }
    else
    {
        tg = new T_Game();
        tg.F_Id = 0;
        tg.F_Phases = 0;
        tg.F_NumOne = 0;
        tg.F_NumTwo = 0;
        tg.F_NumThree = 0;
        tg.F_InvolvedNum = 0;
        tg.F_WinningNum = 0;
        tg.F_LotteryDate = DateTime.Now;
        tg.F_CreateDate = DateTime.Now;
        tg.F_Bonus = 0;
        tg.F_Lottery = false;
        
    }
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=TempData["PageTitle"] %></title>
</head>
<body>
    <div style="text-align:center;" >
   <%Html.BeginForm("GameEditFun", "Admin"); %>
   <%=Html.Hidden("id",tg.F_Id) %>
    <table cellpadding="0" cellspacing="0" border="1"  width="40%" > 
    <tbody>
    <tr>
    <td>ID</td>
    <td><%=Html.TextBox("txt_id", tg.F_Id, new { ReadOnly = true })%></td>
    <td></td>
    </tr>

    <tr>
    <td>期数</td>
    <td><%=Html.TextBox("txt_phases", tg.F_Phases, new  {ReadOnly=true})%></td>
    <td></td>
    </tr>

    <tr>
    <td>是否已开奖</td>
    <td><span style="color:Red;"><%=tg.F_Lottery?"已开奖":"未开奖" %></span></td>
    </tr>

    <tr>
    <td>数字1</td>
    <td><%=Html.TextBox("txt_num1",tg.F_NumOne) %></td>
    <td>只能填入数字,否则默认为0</td>
    </tr>
    <tr>
    <td>数字2</td>
    <td><%=Html.TextBox("txt_num2",tg.F_NumTwo) %></td>
    <td>只能填入数字,否则默认为0</td>
    </tr>
    <tr>
    <td>数字3</td>
    <td><%=Html.TextBox("txt_num3",tg.F_NumTwo) %></td>
    <td>只能填入数字,否则默认为0</td>
    </tr>

    <tr>
    <td>参加人数</td>
    <td><%=Html.TextBox("txt_inv",tg.F_InvolvedNum) %></td>
    <td>只能填入数字,否则默认为0</td>
    </tr>
    <tr>
    <td>中奖人数</td>
    <td><%=Html.TextBox("txt_win",tg.F_WinningNum) %></td>
    <td>只能填入数字,否则默认为0</td>
    </tr>
    <tr>
    <td>奖池</td>
    <td><%=Html.TextBox("txt_boun",tg.F_Bonus) %></td>
    <td>只能填入数字,否则默认为0</td>
    </tr>
    <tr>
    <td>开奖时间</td>
    <td><%=Html.TextBox("txt_lotdate", tg.F_LotteryDate, new { ReadOnly = true })%></td>
    <td>只能填入数字,否则默认为0</td>
    </tr>

    <tr>
    <td>创建时间</td>
    <td><%=Html.TextBox("txt_createdate", tg.F_CreateDate, new { ReadOnly = true })%></td>
    <td>只能填入数字,否则默认为0</td>
    </tr>
    <tr>
    <td  colspan="3" > <input type="submit" value="修改" /> <input type="button" value="返回" /> </td>
    </tr>
    </tbody>
    </table>
    <%Html.EndForm(); %>
    </div>
</body>
</html>
