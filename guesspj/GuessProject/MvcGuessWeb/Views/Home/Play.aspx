<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="System.Data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%=ViewData["PageTitle"] %></title>
<!--[if lte IE 6]>
<style type="text/css">
body { behavior:url("css/csshover.htc"); }
</style>
<![endif]-->
<link href="../Content/css/global.css" rel="stylesheet" type="text/css" />
<link href="../Content/css/main.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../Scripts/jquery-1.4.1.js" ></script>
</head>

<body>

<% Html.RenderAction("VUC_TopHead", "CommControl"); %>

<div id="content1">	
	<!-- 游戏列表开始 -->
  <div class="content1-left left">
      <!--切换导航-->
   	  <div class="menu2"><ul class="left"><li class="current"><a href="#">游戏列表</a></li> <li><a href="#">我的投注</a></li> <li><a href="#">投注模式</a></li></ul>
      <!--每期开奖时间-->
      <div class="hottime right" ><span style="float:left; font-weight:300;  color:Red; font-size:20px;" id="M_phases" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><div id="M_Span" style="float:right;" > 距离本期开奖<span id="M_timer" style="color:Red; font-size:14px" >5</span>秒</div> <div id="M_timeout" style="display:none; float:right;" > 本期已开奖 5秒后自动刷新</div> </div>
      </div>
        
        <div class="content1-left-centent">
        	<div class="clear"></div>
        	<div class="tit">
            	<ul>
                <li class="qs">期数</li>
                <li class="sj">开奖时间</li> 
                <li class="jg">开奖结果</li> 
                <li class="jc">奖池</li> 
                <li class="tze">投中额/中奖额</li>  
                <li class="wd">我的中奖/投注</li> 
                <li class="cy" style="background:none;">参与</li>
                </ul>
            </div>

            <ul id="ulcont">
                        <%
                DataTable dt = ViewData["ConentTable"] as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
            %>
                <li>
                    <ul>
                        <li class="qs" ><%=dr["F_Phases"]%></li>
                        <li class="sj" ><%=Convert.ToDateTime(dr["F_LotteryDate"]).ToString("MM-dd hh:mm")%></li>
                        <%
                        
                            string forstr = ViewData["LotteryShow"].ToString();
                            int  num1=Convert.ToInt32(dr["F_NumOne"].ToString());
                            int  num2=Convert.ToInt32(dr["F_NumTwo"].ToString());
                            int  num3=Convert.ToInt32(dr["F_NumThree"].ToString());
                            string Results = string.Format(forstr, num1.ToString(), num2.ToString(), num3.ToString(),( num1 + num2 + num3).ToString());
                        %>
                        <li class="jg" ><%=(!Convert.ToBoolean(dr["F_Lottery"])) ? "-" : Results%></li>
                        <li class="jc jcbg" ><%=Convert.ToInt32(dr["F_Bonus"]).ToString("00,000")%></li>
                        <li class="tze tzebg" >
                        <%=Convert.ToInt32(dr["F_InvolvedNum"])>Convert.ToInt32(dr["F_WinningNum"])?
                            dr["F_InvolvedNum"].ToString():dr["F_WinningNum"].ToString()  %>
                            /
                            <%= (!Convert.ToBoolean(dr["F_Lottery"]))? "0": Convert.ToInt32(dr["F_InvolvedNum"]) < Convert.ToInt32(dr["F_WinningNum"]) ?
    dr["F_InvolvedNum"].ToString() : dr["F_WinningNum"].ToString()%></li>
                        <li class="wd wdbg" >-</li>
                        <li class="cy" >
                        <% if (Convert.ToBoolean(dr["F_Lottery"]))
                           { %>
                           已开奖
                           
                           <%}
                           else
                           { %>
                           <a href="#" class="touzhu" >请投注</a>
                           
                           <%} %>
                        </li>

                    </ul>
                </li>
                <% }
                } %>
            </ul>
            
            <div class="page right">
            <% Html.RenderAction("VUC_HCPager", "CommControl", ViewData["PagerData"]??null); %>
            <!--
                	<dl>
                    	<dd class="start">1</dd>
                    	<dd><a href="#">2</a></dd>
                        <dd><a href="#">3</a></dd>
                        <dd><a href="#">4</a></dd>
                        <dd><a href="#">5</a></dd>
                        <dd><a href="#">6</a></dd>
                        <dd><a href="#">7</a></dd>
                        <dd><a href="#">8</a></dd>
                        <dd><a href="#">9</a></dd>
                        <dd><a href="#">></a></dd>
                        <dd class="all"><a href="#">>></a></dd>
                        <dd class="last"><a href="#">最后一页</a></dd>
                    </dl>
                     -->
                </div>
               
               
        </div>
	</div>
	<!-- 游戏列表结束 -->

    <script type="text/javascript" >
    //隔行变色效果
    function addrowcolor()
    {
    $("#ulcont>li:even").css("background-color","#FFFFFF");
     $("#ulcont>li:odd").css("background-color","#FFFFC3");

    }
    var timer=0;
    var timerid;
    function timer_span()
    {
        timer--;
        if(timer>0)
        {
        $("#M_timer").html(timer);
        }
        if(timer==0)
        {
                    $("#M_Span").hide();
                    $("#M_timeout").show();
        }
        if(timer<=-4)
        {
        window.location.href=window.location.href;
        clearInterval(timerid);

        }

    }
        $(document).ready(function () {
        addrowcolor();
        $("#ulcont>li").mouseover(function()
        {
        this.style.backgroundColor="#FFFF92";
        }).mouseout(addrowcolor)

            $.post("../Ajax/GameTimer", { "gtype":<%=ViewData["GameType"].ToString() %> }, function (data) {
            timer= parseInt( data.timer)-1;
            
                $("#M_phases").html("现在第"+data.id.toString()+"期");
                $("#M_timer").html(timer.toString());
                $("#M_Span").show();
               $("#M_timeout").hide();
                timeri=setInterval(timer_span,1000);

            }, "json");
        });

    </script>
    
	<!-- 右边信息辅助区开始-->
	<div class="content1-right right">
   	  <div class="layout">
       	  <div class="tit"><strong>社区新帖<span>社区新帖</span></strong></div>
          <ul>
           	<li><a href="#">兑换了真开心</a></li>
            <li><a href="#">1811</a></li>
            <li><a href="#">归零帖</a></li>
            <li><a href="#">大家早上好</a></li>
            <li><a href="#">大家都来玩</a></li>
            <li><a href="#">早上好</a></li>
            <li><a href="#">运气不行啊</a></li>
            <li><a href="#">报道来了，大家加油啊</a></li>
            <li class="more"><a href="#">更多+</a></li>
          </ul>
      </div>
      
      <span class="blank10"></span>
      
       <div class="layout">
       	  <div class="tit"><strong>最新加盟代理商<span>最新加盟代理商</span></strong></div>
          <ul>
           	<li>代理商图片</li>
            <li>代理商图片</li>
          </ul>
      </div>
      
	</div>
	<!-- 右边信息辅助区结束-->
</div>


<div id="bottom">
	<span class="blank15"></span>
	<div class="links left">
    	<ul class="left">
    		<a href="#">关于我们</a>  |  
        	<a href="#">商务合作</a>  |  
        	<a href="#">诚征英才</a>  |  
        	<a href="#">联系我们</a>  |  
        	<a href="#">法律支持</a>  |  
        	<a href="#">站长内务</a> 
        </ul>
        <dl class="right">
        	<dd><img src="../Content/images/pa.gif" alt="客服" /></dd>
            <dd><img src="../Content/images/pa.gif" alt="客服" /></dd>
        </dl>
    </div>
    <div class="copyright left"><span class="left">版权所有2010  www.hcbag.com  蜀ICP10023384号</span><span class="left"><script type="text/javascript">

  var _gaq = _gaq || [];
  _gaq.push(['_setAccount', 'UA-20258244-1']);
  _gaq.push(['_trackPageview']);

  (function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
  })();

</script></span></div>
</div>
</body>
</html>
