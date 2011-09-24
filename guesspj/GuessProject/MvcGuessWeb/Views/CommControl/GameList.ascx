<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script type="text/javascript" src="../../Scripts/jquery-1.4.1.js" ></script>
<table cellpadding="0" cellspacing="0" border="0" width="500">
<tbody>
<tr>
<th>游戏编号</th>
<th>游 戏 名</th>
<th>当前期数</th>
<th>开奖计时</th>
<th>管理结果</th>
<th>前台查看</th>
</tr>

<tr>
<th>1</th>
<th>幸运28</th>
<th id="gameonenum">&nbsp;</th>
<th id="gameonetimer">&nbsp;</th>
<th><a href="../../Admin/GameInfo">游戏管理</a></th>
<th><a href="../../Home/PlayGameOne">前台查看</a></th>
</tr>

<tr>
<th>2</th>
<th>投骰子</th>
<th id="gametwonum">&nbsp;</th>
<th id="gametwotimer">&nbsp;</th>
<th><a href="../../Admin/GameInfoTwo">游戏管理</a></th>
<th><a href="../../Home/PlayGameTwo">前台查看</a></th>
</tr>
</tbody>
</table>
<script type="text/javascript" >
function time_int()
{
            $.post("../Ajax/GameTimer", { "gtype":1 }, function (data) {
           
                $("#gameonenum").html(data.id.toString());
                $("#gameonetimer").html(data.timer.toString());
            }, "json");

            $.post("../Ajax/GameTimer", { "gtype":2 }, function (data) {
           
                $("#gametwonum").html(data.id.toString());
                $("#gametwotimer").html(data.timer.toString());
            }, "json");
}

$(document).ready(function () {
    setInterval(time_int, 1000);
});
</script> 
