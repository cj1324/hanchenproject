
//寒晨的JavaScript类库  UTF-8+编码
(function(w,d,n,_ver){
 w._ = w.HC = w.HC||{
	ver:_ver,  //库版本信息
    length:0,
    constructor:_ver.name,
    __typeObj:_ver.name+" Object",
    MyObj:function(o)
    {
		if(o.constructor === HC.__typeObj)
		{
			if(typeof(o.items) === "undefined") 
			{
			return false;
			}
			else
			{
			return true;
			}
		}
		return false;
    
    },
    isObj:function(){
   // alert("cont:"+this.constructor+"    items: "+typeof(this.items))
     if(this.constructor === HC.__typeObj)
    {
        if(typeof( this.items) === "undefined")
        {
        return false;
        }
        else
        {
        return true;
        }
    }
    return false;
    },
    /*  生成对象的方法 **/
  
    //根据ID获取默认对象 入参DOM对象ID 返回 默认对象
    getdom:function(){
    if(this.isObj()){
        return this.items[0];   
    }
    return d;
    
    },
    getByDom:function(dom)
    {
        return (new HC.__dom(dom));
    },
	getById:function(did){
     var con=this.getdom();
    return (new HC.__dom(con.getElementById(did)));},
    //根据DOM TagName 获取默认对象  
    getByTag:function(tag){
    var con=this.getdom();
    return (new HC.__dom(con.getElementsByTagName(tag)));},
    getByClass:function(cn){
    
    var doms=this.getByTag("*");var len=0;for(var i=0;i<doms.length;i++){if(doms.items[i].className===cn){doms.items[len]=doms.items[i];len++;}}doms.clearLen(len);return doms;},
	create:function(tag){return (new HC.__dom(d.createElement(tag)))},
    replace:function(s){/*待实现替换节点 **/ return undefined;},
    query:function(s,c){/*待实现的选择器**/ return undefined;},



    /* 一些通用方法 **/
    
    //对属性进行拷贝 模拟继承
    __copyPro:function(s,d){
    
    for(i in s){
    if(i.charAt(0) !=="_")
    {
    if(s.hasOwnProperty(i)) d[i]=s[i];
    }
    }
    
    },
	//获取光标位置! 入参文本框对象
	__CurPos:function(textBox){
		if(typeof(textBox.selectionStart) == "number"){
			start = textBox.selectionStart;
			end = textBox.selectionEnd;
		}
		else if(document.selection){
			var range = document.selection.createRange();
			if(range.parentElement().id == textBox.id){
				var range_all = document.body.createTextRange();
				range_all.moveToElementText(textBox);
				for (start=0; range_all.compareEndPoints("StartToStart", range) < 0; start++)
				range_all.moveStart('character', 1);
				for (var i = 0; i <= start; i ++){
					if (textBox.value.charAt(i) == '\n')
					start++;
				}
				var range_all = document.body.createTextRange();
				range_all.moveToElementText(textBox);
				for (end = 0; range_all.compareEndPoints('StartToEnd', range) < 0; end ++)
				range_all.moveStart('character', 1);
				for (var i = 0; i <= end; i ++){
					if (textBox.value.charAt(i) == '\n')
					end ++;
				}
			}
		}
		return {"start":start,"end":end};
	},
    toString:function(){return this.constructor;},

	//遍历成员 方法 
    each:function(fun){
		var i=0,tmp;
		for(var i=0;i<this.length;i++)
		{
			if(this.items[i].nodeName=="SCRIPT" || this.items[i].nodeName=="STYLE"){continue;} //遍历过程中排除 style和script标记
		tmp=new HC.__dom(this.items[i]);
		fun.call(tmp,i);
		delete tmp;
		}
		return this;
    },
    setTime:function(f,t){return w.setTimeout(f,t);},
    setInter:function(f,t){return w.setInterval(f,t);},  
    clearTime:function(t){w.clearTimeout(t);},
    clearInter:function(t){w.clearInterval(t)}, //清除定时器 
    setOptions:function(opt,set)
    {
        for(o in set)
        {
          if(!opt.hasOwnProperty(o))
          {
              opt[o]=set[o];  
          }
        }
    },
    /*  Ajax模块**/
    __ajax:function(options)
    {
        HC.setOptions(options,HC.__ajaxSetting);
        //HC.__initajax(options);
        var xhr=options.xhr();
        if(xhr) {options.error("xhr =null") return; }
        if(options.userName)
        {
            xhr.open(options.methon,options.url,options.aync);
        }
        else
        {
            xhr.open(options.methon,options.url,options.aync,options.userName,options.passWord);
        }
        try
        {
        if(options.cache)
        {
            //待处理缓存
        }
        if(options.ajaxHead) { xhr.setRequestHeader("X-Requested-With", "XMLHttpRequest"); }
        }catch(headError){}
        
        //ajax请求返回后的处理函数
        xhr.onreadystatechange=function(itime){
            if(xhr.readyState!=4)
            {
                return;
            }
            if(xhr.status==200)
            {
                XHRSuccess(xhr.Response.Text);
            }
            alert(xhr.status+"______?");
        };
        function XHRSuccess(RText)
        {

        }
       

//***************************************************************
        function requestTestRun(moduleName, className, methodName) {
            var methodSuffix = "";
            if (methodName) {
                methodSuffix = "." + methodName;
            }
            var xmlHttp = newXmlHttp();
            xmlHttp.open("GET", "%s/run?name=" + moduleName + "." + className + methodSuffix, true);
            xmlHttp.onreadystatechange = function() {
                if (xmlHttp.readyState != 4) {
                    return;
                }
                if (xmlHttp.status == 200) {
                    var result = eval("(" + xmlHttp.responseText + ")");
                    totalRuns += parseInt(result.runs);
                    totalErrors += result.errors.length;
                    totalFailures += result.failures.length;
                    document.getElementById("testran").innerHTML = totalRuns;
                    document.getElementById("testerror").innerHTML = totalErrors;
                    document.getElementById("testfailure").innerHTML = totalFailures;
                    if (totalErrors == 0 && totalFailures == 0) {
                        testSucceed();
                    } else {
                        testFailed();
                    }
                    var errors = result.errors;
                    var failures = result.failures;
                    var details = "";
                    for(var i=0; i<errors.length; i++) {
                        details += '<p><div class="error"><div class="errtitle">ERROR ' +
                                   errors[i].desc +
                                   '</div><div class="errdetail"><pre>'+errors[i].detail +
                                   '</pre></div></div></p>';
                    }
                    for(var i=0; i<failures.length; i++) {
                        details += '<p><div class="error"><div class="errtitle">FAILURE ' +
                                    failures[i].desc +
                                    '</div><div class="errdetail"><pre>' +
                                    failures[i].detail +
                                    '</pre></div></div></p>';
                    }
                    var errorArea = document.getElementById("errorarea");
                    errorArea.innerHTML += details;
                } else {
                    document.getElementById("errorarea").innerHTML = xmlHttp.responseText;
                    testFailed();
                }
            };
            xmlHttp.send(null);
        }
    },
    __ajaxSetting:
    {
        url:location.href,
        methon:"GET",
        contentType:"application/x-www-form-urlencoded",
        userName:false,
        passWord:"",
        date:"",
        async: true,
        ajaxHead:true,
        dateType:"json",
        cache:false,//未处理缓存
        xhr:function()
        {
        var A;
        try {
             A=new ActiveXObject("Msxml2.XMLHTTP");
        } catch (e) {
            try {
                A=new ActiveXObject("Microsoft.XMLHTTP");
            } catch (oc) { A=null; }
        }
        if(!A && typeof XMLHttpRequest != "undefined")
        A = new XMLHttpRequest(); 
        return A; 
        },
        success:function(o)
        {
            alert("未处理ajax成功事件!!Data:"+o);
        },
        error:function(e)
        {
            alert("ajax请求失败 Error:"+e);
        },
        accepts: {
        xml: "application/xml, text/xml",
        html: "text/html",
        script: "text/javascript, application/javascript",
        json: "application/json, text/javascript",
        text: "text/plain",
        _default: "*/*" 
        }
    },

    /* 初始对象后才可调用方法 可以进行链式操作  **/


    //事件系统
   __addEv:function(el,type,fun)
    {
        if (el.addEventListener){
			el.addEventListener(type, fun, false);
		} else if (el.attachEvent){
			el.attachEvent('on' + type,fun);
		}

    },
    __fixEv:function(e)
    {
        if (e.preventDefault) e.preventDefault();
		if (e.stopPropagation) e.stopPropagation();
		if (e.cancelBubble !== undefined) e.cancelBubble = true;
		if (e.returnValue !== undefined) e.returnValue = false;
    },
    elem:{
    bind:function(ev,fun){

      var el=this.getdom();
       HC.__addEv(el,ev,function(e)
       {
       fun(e);
       HC.__fixEv(e);
       return false;
       });
       return this;
    },
    unbind:function(ev){},
    click:function(fun){
       return this.bind("click",fun); 
    },
    mouseover:function(fun){
        return this.bind("mouseover",fun);
    },
    mouseout:function(fun){
        return this.bind("mouseout",fun);
    },
    keyup:function(fun)
    {
        return this.bind("keyup",fun);
    },
    keydown:function(fun)
    {
        return this.bind("keydown",fun);
    },
    ready:function(fun) { 
        if(d.addEventListener) 
        d.addEventListener("DOMContentLoaded",function() { 
        d.removeEventListener("DOMContentLoaded",arguments.callee,false); 
        fun(); 
        },false); 
        else if(d.attachEvent) {//for IE 
        if(d.documentElement.doScroll && w.self==w.top) { 
        (function() { 
        try { 
        d.documentElement.doScroll("left"); 
        }catch(ex) { 
        HC.setTime(arguments.callee,5); 
        return; 
        } 
        fun(); 
        })(); 
        }else {//maybe late but also for iframes 
        d.attachEvent("onreadystatechange",function() { 
        if(d.readyState==="complete") { 
        d.detachEvent("onreadystatechange", arguments.callee); 
        fun(); 
        } 
        }); 
        } 
        } 
    },


    parent:function(){ 
		var pdom;
		if(this.items[0].parentNode)
		{
		 pdom=(new HC.__dom(this.items[0].parentNode)) 
		}
		return pdom;
		},
    next:function(){
		var ndom=this.items[0];
		do{
		if(ndom.nextSibling)
		{
			ndom=ndom.nextSibling;
		}
		else{return;}
		}while(ndom.nodeType!=1)
		return (new HC.__dom(ndom));
		},
    prev:function(){
		var pdom=this.items[0];
		do{
		if(pdom.previousSibling)
		{
			pdom=pdom.previousSibling;
		}
		else{return undefined;}
		}while(pdom.nodeType!=1)
		return (new HC.__dom(pdom));
		},
    childs:function(){
			if(!this.items[0].hasChildNodes){return undefined;}
			if(arguments.length==1)
			{
				var i=arguments[0],j=0,k=0,ds=this.items[0].childNodes;
				for (;j<ds.length;j++)
				{
					if(ds[j].nodeType==1)
					{
						k++;
						if(k-1==i)
						{
							return (new HC.__dom(ds[j]));
						}
					}
				}
				return undefined;
			}
			if(arguments.length==0){
				return (new HC.dom(this.items[0].childNodes));
			};
			
			return undefined;},
    index:function(){return undefined;},
    __gdom:function(s){
       var dom; 
       var type=typeof(s);
       if(type==="string")
       {
           dom=d.getElementById(s); 
       }
       if(type==="object")
       {
         if(s.nodeType==1)
         {
            dom=s;
         }
        if(HC.MyObj(s))
        {
            dom=s.items[0];
        }
       }
       
       return dom;

    },
    append:function(d){ 
    this.items[0].appendChild(HC.__gdom(d));return this;},
    appendTo:function(d){ 
    HC.__gdom(d).appendChild(this.items[0]);return this;},
    show:function(){this.setDisplay("block");return this;},
    hide:function(){this.setDisplay("none");return this;},
    setDisplay:function(s){this.items[0].style.display=s;return this;},
    setZIndex:function(i){this.items[0].style.zIndex=i;;return this;},
    getLocation:function()
    {
        var el=this.items[0];
         var offsetTop = el.offsetTop; 
         var offsetLeft = el.offsetLeft; 
         while( el = el.offsetParent ) 
         { 
             offsetTop += el.offsetTop; 
             offsetLeft += el.offsetLeft; 
         } 
        return {'top':offsetTop,"left":offsetLeft};
    },
    getTop:function(){
        return this.getLocation().top; 
    },
    getLeft:function(){
        return this.getLocation().left;
    },
    getSize:function(){
        var el=this.items[0];
        var offsetWidth = el.offsetWidth; 
        var offsetHeight = el.offsetHeight;
        return {'width':offsetWidth,'height':offsetHeight};
    },
    getWidth:function(){return this.getSize().width;},
    getHeight:function(){return this.getSize().height;},
    
    setTop:function(w){return undefined;},
    setLeft:function(w){return undefined;},
    setWidth:function(w){return undefined;},
    setHeight:function(w){return undefined;},
	setClass:function(n){this.items[0].className=n;return this;},
    getClass:function(){return this.items[0].className;},
    setCss:function(k,v){this.items[0].style[k]=v;return this;},
    getCss:function(k){return this.items[0].style[k];},
    setAttr:function(k,v){this.items[0].setAttribute(k,""+v);return this;},
    getAttr:function(k){return this.items[0].getAttribute(k);},
	setHtml:function(t){this.items[0].innerHTML=t; return this;},
    getHtml:function(){return this.items[0].innerHTML;},
    //获取其中一个默认对象
    get:function(i){
        if(i>=0&& i<this.length)
        {
            this.items[0]=this.items[i];
            this.clearLen(1);
            return this;
        }
        else
        {
            return undefined;
        }
    },
    //设置实际DOM个数清除多余DOM
    clearLen:function(l){
        for(var i=this.length-1;i>l-1;i--)
        {
            delete this.items[i];
        }
        this.length=l;
        return this;
    
    },
    //压入一个默认对象
    push:function(m){this.items[this.length]=m;this.length++;return this;},
    pop:function(){
    this.length--;
    delete this.items[this.length];
    return this;
    },
    slice:function(b,e,i){/* 切片方法待实现 **/}
    
    }
    ,
    /* 初始化 对象时调用的方法 **/
	__dom:function(m){
    if(m==null) { return; }
    var len=0;
    this.items=[];
   if(!!m.length)
    {
       // len=m.length;
        for(var i=0;i<m.length;i++)
        {
			if(m[i].nodeType==1)
			{
            this.items.push(m[i]);
			len++;
			}
        }
    }
    else
    {
        this.items.push(m);
        len=1;
    }
    HC.__copyPro(HC.elem,this);
    this["constructor"]=HC.__typeObj;
    //this.prototype.constructor=HC.typeObj;
    this.length=len;
    }
};
})(window,document,navigator.userAgent,{name:'HanChen JavaScript Library',no:'1.0',date:'2010-10-06'})


