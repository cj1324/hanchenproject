<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<!doctype html public "-//w3c//dtd html 4.01 transitional//en" "http://www.w3c.org/tr/1999/rec-html401-19991224/loose.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>用户登录</title>
<link href="../Content/images/default.css" type="text/css" rel="stylesheet" />
<link  href="../Content/images/user_login.css" type="text/css" rel="stylesheet" />
<script type="text/javascript" language="javascript" >
    if (window.top != window) //如果在框架中 就跳出框架
    {
        window.top.location.href = window.location.href;
    }
</script>
</head>
<body id="userlogin_body">

<div id="user_login" >

<dl>
  <dd id="user_top">
  <ul>
    <li class="user_top_l"></li>
    <li class="user_top_c"></li>
    <li class="user_top_r"></li>
  </ul>
  </dd>
  <dd id="user_main">
   <%Html.BeginForm("CheckAdminInfo", "Admin"); %>
  <ul>
    <li class="user_main_l" ></li>
    <li class="user_main_c" >
   
        <div class="user_main_box" >
        <ul>
            <li class="user_main_text" >用户名：</li>
            <li class="user_main_input" ><input type="text" class="TxtUserNameCssClass" id="txt_username"    maxlength="20" name="txt_username" /> </li>
        </ul>
        <ul>
            <li class="user_main_text" >密 码：</li>
            <li class="user_main_input" ><input   type="password"  class="TxtPasswordCssClass" id="txt_password" maxlength="20"   name="txt_password" /> </li>
        </ul>
        <ul>
            <li class="user_main_text" >验证码：</li>
            <li class="user_main_input"><input  type="text" maxlength="4" class="TxtValidateCodeCssClass"   id="txt_validatecode" />
            <img src="../Content/images/checkcode.jpg" style="padding-top:2px;_padding-top:0px;" width="70"  onclick="this.src=this.src+'?'"   onmouseover="this.style.cursor='pointer';" height="20" alt="点击切换验证码" />
            </li>
        </ul>
        <ul>
           <li id="loginmsg" class="user_main_msg">
           <%= TempData["ErrorInfo"]==null?"":TempData["ErrorInfo"].ToString() %>
           </li>
        </ul>
        </div>
        
    </li>
    <li class="user_main_r">
        <input class="ibtnentercssclass" id="IbtnEnterCssClass"  style="border-top-width: 0px; border-left-width: 0px; background-image:url(../Content/images/user_botton.gif); width:96px;height:96px; border-bottom-width: 0px; border-right-width: 0px"   type="submit"  name="ibtnenter" />
    </li>

    </ul>
    <% Html.EndForm(); %>
    </dd>
  <dd id="user_bottom">
  <ul>
    <li class="user_bottom_l" ></li>
    <li class="user_bottom_c" ></li>
    <li class="user_bottom_r"></li>
  </ul>
  </dd>
</dl>

</div>
</body>

</html>
