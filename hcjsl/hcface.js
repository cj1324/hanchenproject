
//作者:寒晨
//时间: 2010年 11月 13日 星期六 11:15:30 CST
//作用:自动生成表情列表框让用户选择表情，添加指定的表情编码到文本框
//

HC.FacePnl=function(options)
{
if(!this.isObj())
{
alert("必须存在DOM对象才可以调用该插件,加载失败。。。。。。。");
return;
}
if(arguments.length!=1 ||  typeof(options) !="object" )
{
alert("参数不符合，加载失败。。。");
return;
}

//以下时默认选项
var setting={
'txtId':"txt_Conent", 
"facePnlId":"OnefacePnl",
"faceHeight":40,
"faceWidth":40,
"title":"标题",
"closetext":"关闭",
"faceArray":[{"src":"http://www.baidu.com/img/baidu_logo.gif","rel":"我点了百度。。","ftitle":"我是百度"}],
"facecss":"facecss",
"contcss":"contcss",
"headcss":"headcss",
"closecss":"closecss"
};

HC.setOptions(setting,options);
var location=this.getLocation();
var size=this.getSize();
var top=location.top+size.height+5;
var left=location.left


var cont=HC.create("div").setAttr("id",setting.facePnlId).setClass(setting.contcss);
cont.setCss("left",left+"px");
cont.setCss("top",top+"px");
cont.setCss("height","auto");
cont.setAttr("rel",setting.txtId);
cont.hide();
cont.appendTo(document.body);
var head=HC.create("div").setClass(setting.headcss).setHtml(setting.title);
var closebtn=HC.create("div").setClass(setting.closecss).setHtml(setting.closetext);
cont.append(head);
head.append(closebtn);

//点击关闭的时候处理隐藏
closebtn.setAttr("rel",setting.facePnlId);
closebtn.click(function(){
var e = arguments[0] || window.event;
var src = e.srcElement || e.target; 
var rel=  HC.getByDom(src).getAttr("rel");
HC.getById(rel).hide();
})

//缓存插入符号位置
var tar =HC.getById(setting.txtId);
tar.bind("blur",function(){ 
var e = arguments[0] || window.event;
var src = e.srcElement || e.target;

if(src.createTextRange)
{ 
    src.caretPos = document.selection.createRange().duplicate(); 
}

})


//点击表情按钮的事件
this.setAttr("rel",setting.facePnlId);
this.setAttr("tar",setting.txtId);
this.click(function(){
var e = arguments[0] || window.event;
var src = e.srcElement || e.target; 
var hcdom=HC.getByDom(src);
var rel=hcdom.getAttr("rel");
//var tar=hcdom.getAttr("tar");
var hctar=HC.getById(tar);
//function storeCaret (textEl) {
//if (textEl.createTextRange) 
//textEl.caretPos = document.selection.createRange().duplicate(); 
//}
//storeCaret(hctar.items[0]); 
HC.getById(rel).show(); 
});

function createFace(item,w,h,css)
{
var faceImg=HC.create("img").setAttr("alt",item.title).setAttr("src",item.src).setAttr("rel",item.rel).setAttr("width",w+"px").setAttr("height",h+"px").setAttr("border","1").setClass(setting.facecss).setCss("float","left");
faceImg.click(function(){
var e = arguments[0] || window.event;
var src = e.srcElement || e.target; 
var face=  HC.getByDom(src)
var txt=face.getAttr("rel");
var tarId=face.parent().getAttr("rel");
var tar=HC.getById(tarId);
//alert(txt+"==========="+tarId);
//焦点插入方法
function insertAtCaret (textEl, text) {
alert(textEl.nodeName);
if (textEl.createTextRange && textEl.caretPos) {
var caretPos = textEl.caretPos;
caretPos.text =caretPos.text.charAt(caretPos.text.length - 1) == ' ' ?text + ' ' : text; 
} 
}
insertAtCaret(tar.items[0],txt);
});

return faceImg;
}
for(i in setting.faceArray)
{
  createFace(setting.faceArray[i],setting.faceWidth,setting.faceHeight,setting.facecss).appendTo(cont);
}

}
