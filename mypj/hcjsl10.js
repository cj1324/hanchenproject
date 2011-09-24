
//寒晨的JavaScript类库  UTF-8+编码
(function(w,d,n,_ver){
w.HC=w.HC||{
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
    /* 初始对象后才可调用方法 可以进行链式操作  **/


    //事件系统
   addEv:function(type,fun)
    {
         var el=this.getdom();
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
    bind:function(ev,fun){
       this.addEv(ev,function(e)
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
    show:function(){this.setDisplay("block");},
    hide:function(){this.setDisplay("none");},
    setDisplay:function(s){this.items[0].style.display=s;return this;},
    setZIndex:function(i){this.items[0].style.zIndex=i;;return this;},
    getTop:function(){return undefined;},
    setTop:function(w){return undefined;},
    setWidth:function(w){return undefined;},
    getWidth:function(){return undefined;},
    setHeight:function(w){return undefined;},
    getHeight:function(){return undefined;},
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
    slice:function(b,e,i){/* 切片方法待实现 **/},
    /* 初始化 对象时调用的方法 **/
	__dom:function(m){
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
    HC.__copyPro(HC,this);
    this["constructor"]=HC.__typeObj;
    //this.prototype.constructor=HC.typeObj;
    this.length=len;
    }
};
})(window,document,navigator.userAgent,{name:'HanChen JavaScript Library',no:'1.0',date:'2010-10-06'})


