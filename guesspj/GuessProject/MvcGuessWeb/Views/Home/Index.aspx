<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="google-site-verification" content="XWU-SOXkNOGvswP4YS-3LeCx5J0Yo8bmy6u0cwh9az4" />
<meta id="metaKeywords" name="keywords" content="人人翁,幸运28,开心16,玩游戏,赢奖品,赚Q币,在线网赚,免费网赚"/>
<meta name=description CONTENT="人人翁是一个集合广告体验、游戏娱乐、电子商务的深度网络效果营销平台，通过引进各种有奖游戏，使用户在休闲娱乐中完成知识、信息的传播，并获得游戏虚拟货币--柴币。通过柴币的积累，用户可得到丰厚的奖品，同时也为商家提供真实有效的广告受众。">

<title><%=ViewData["PageTitle"] %></title>
<!--[if lte IE 6]>
<style type="text/css">
body { behavior:url("css/csshover.htc"); }
</style>
<![endif]-->
<link href="Content/css/global.css" rel="stylesheet" type="text/css" />
<link href="Content/css/main.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../Scripts/jquery-1.4.1.js" ></script>
</head>

<body>
<% Html.RenderAction("VUC_TopHead", "CommControl"); %>

<div id="content1">
	<div class="bannerbar">
    	<ul class="banner left"><img src="Content/images/banner.jpg" alt="广告" /></ul>
        <ul class="bulletin right">
        	<h3>人人翁公告</h3>
            <div style="font-size:16px; margin-top:10px;color:#ee3333">
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;恭喜人人翁, 网站开<br/>
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;张了,希望大家多多支持,<br/>
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;我们会定期推出优惠活动<br/>
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;希望大家踊跃参加<br /><br />
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;---2010-12-14
            
            </div>
        </ul>
        <span class="blank10"></span>
    </div>
    
    <!--热门奖品-->
    <div class="hot-award">
    	<div class="tit1"><a href="#">更多</a></div>
        <div class="layout">
       	  <dl>
       		  <dt><a href="#"><img src="Content/home/pro1.gif" alt="" /></a></dt>
           	<dd>hello kitty 猫70CM <strong>4700000</strong></dd>
        	</dl>
            <dl>
        		<dt><a href="#"><img src="Content/home/pro2.gif" alt="" /></a></dt>
            	<dd>SONY 单反DSLR-A300+S.. <strong>4700000</strong></dd>
        	</dl>
            <dl>
        		<dt><a href="#"><img src="Content/home/pro3.gif" alt="" /></a></dt>
            	<dd>金立A300 电视购物 大.. <strong>4700000</strong></dd>
        	</dl>
          <dl>
       		  <dt><a href="#"><img src="Content/home/pro4.gif" alt="" /></a></dt>
            	<dd>30QQ币 <strong>4700000</strong></dd>
       	  </dl>
            <dl>
        		<dt><a href="#"><img src="Content/home/pro5.gif" alt="" /></a></dt>
            	<dd>奔腾 PYD50A电压力煲 <strong>4700000</strong></dd>
        	</dl>
        </div>
        <span class="blank10"></span>
    </div>
    
    <!--热门游戏-->
    <div class="hot-game">
    	<div class="tit1"><a href="#">更多</a></div>
        <div class="layout"><img  src="Content/home/pro6.gif" alt="" /></div>
        <span class="blank10"></span>
    </div>
    
    
    <!--盈利+兑奖+最新活动开始-->
    <div class="layout">
   	  <div class="home-content-left left">
       	  <h3>今日盈利总排行</h3>
    <div class="today">
              <dl>
                <dt><a href="#">亡命赌徒</a></dt>
                <dd>2830120</dd>
              </dl> 
              <dl> 
                <dt><a href="#">寒晨</a></dt>
                <dd>2530120</dd>
              </dl>   
              <dl>  
                <dt><a href="#">天下一绝</a></dt>
                <dd>2030120</dd>
              </dl> 
              <dl>  
                <dt><a href="#">新田地</a></dt>
                <dd>1700041</dd>
              </dl>
              <dl>  
                <dt><a href="#">暴发户</a></dt>
                <dd>1509861</dd>
              </dl>
              <dl>  
                <dt><a href="#">小天涯</a></dt>
                <dd>1201300</dd>
              </dl>
              <dl>  
                <dt><a href="#">绝地反击</a></dt>
                <dd>929254</dd>
              </dl>
              <dl>  
                <dt><a href="#">小富婆</a></dt>
                <dd>601045</dd>
              </dl>
              <dl>  
                <dt><a href="#">小青</a></dt>
                <dd>300353</dd>
              </dl>
              <dl>  
                <dt><a href="#">穷啊穷</a></dt>
                <dd>90754</dd>
              </dl>
          </div>
      	</div>
        
        <!--兑奖-->
   	  <div class="home-content-center left">
      	<h3><strong class="left">最新兑奖</strong><a href="#" class="more2">更多</a></h3>
      	<div class="award">
      		<dl>
            	<dt>兑换奖品</dt>
                <dd>兑换会员</dd>
            </dl>
            <div class="clear"></div>
            <ul>
            	<li><img src="Content/home/pro7.gif" />1秒发卡-1万广告卡x2  <a href="#" class="home-id">亡命赌徒</a></li>
            </ul>
            <ul>
            	<li><img src="Content/home/pro7.gif" />1秒发卡-1万广告卡x2  <a href="#" class="home-id">亡命赌徒</a></li>
            </ul>
            <ul>
            	<li><img src="Content/home/pro7.gif" />1秒发卡-1万广告卡x2  <a href="#" class="home-id">亡命赌徒</a></li>
            </ul>
            <ul style="border-bottom:none;">
            	<li><img src="Content/home/pro7.gif" />1秒发卡-1万广告卡x2  <a href="#" class="home-id">亡命赌徒</a></li>
            </ul>
      	</div>
      </div>
        
    	<div class="home-content-right right">
        	<!-- 最新活动-->
       	  <div class="home-content-right1">
            <ul class="bulletin right">
                <h3><strong class="left">最新活动</strong><a href="#" class="more2">更多</a></h3>
                <div class="clear"></div>
                    <dl>
                        <dd><a href="#">推广本本火爆进行中~~~~~~</a></dd>
                        <dd><a href="#">火柴棒之星 全民选秀</a></dd>
                        <dd><a href="#">账号注意事项与疑难问题回答</a></dd>
                        <dd><a href="#">官方500人超级群1红包天天发</a></dd>
                        <dd><a href="#">客服QQ与官方QQ500人超级QQ</a></dd>
                        <dd><a href="#">关于加强账号安全与防范问题!!!!</a></dd>
                    </dl>
            	</ul>
                <span class="blank10"></span>
            </div>
            <!--社区新帖-->
            <div class="home-content-right2">
            	<ul class="bulletin right">
                <h3><strong class="left">社区新贴</strong><a href="#" class="more2">更多</a></h3>
                <div class="clear"></div>
                    <dl>
                        <dd><a href="#">推广本本火爆进行中~~~~~~</a></dd>
                        <dd><a href="#">人人翁之星 全民选秀</a></dd>
                      <dd><a href="#">账号注意事项与疑难问题回答</a></dd>
                      <dd><a href="#">官方500人超级群1红包天天发</a></dd>
                  </dl>
            	</ul>
            </div>
        </div>
        <span class="blank10"></span>
    </div>
    <!--盈利+兑奖+最新活动结束-->
    
    <!--合作伙伴-->
    <div class="cop">
    	<img src="Content/images/cop.gif" />
    </div>
    
</div>







<div id="bottom">
	<span class="blank15"></span>
	<div class="links left">
    	<ul class="left">
    		<a href="#">关于人人翁</a>  |  
        	<a href="#">人人翁合作</a>  |  
        	<a href="#">诚征英才</a>  |  
        	<a href="#">联系我们</a>  |  
        	<a href="#">法律支持</a>  |  
        	<a href="#">站长内务</a> 
        </ul>
        <dl class="right">
        	<dd><img src="Content/images/pa.gif" alt="客服" /></dd>
            <dd><img src="Content/images/pa.gif" alt="客服" /></dd>
        </dl>
    </div>
    <div class="copyright left"><span class="left">版权所有2010  人人翁  蜀ICP10023384号</span><span class="left"><script type="text/javascript">

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
