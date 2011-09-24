
//测试方法用来输出对象的属性
(function()
{
    var console= console||{};
    //{{{  console.writeobj(obj,title)
    console.writeobj=function(obj,title)
    {
    var contdiv=document.createElement("div");
    var odiv=document.createElement("div");
    if(!!title)
        title="empty";
    //odiv.id="odiv";
    odiv.style.border="2px solid blue";
    if(!!obj)
    {
        odiv.innerHTML+="<span onclick=\"this.parentNode.style.display=\'none\' \" style='background-color:red;float:right' >关闭</span><br/>";

        var ttt="&nbsp;&nbsp;&nbsp;&nbsp;||NULL||";
        try
        {
            if(obj.nodeType==1)
                ttt="&nbsp;&nbsp;&nbsp;&nbsp;||"+obj.nodeName+"||";
        odiv.innerHTML+="ObjectName:"+obj+ttt+"<br/>ObjectToString:"+obj.toString()+ttt+"&nbsp;&nbsp;&nbsp;&nbsp;<br/>";
        }
        catch (e)
        {
        odiv.innerHTML+="ObjectName: Error  "+ttt+" <br/> ObjectToString: "+obj+"   <br/>";
        }
        try
        {
            odiv.innerHTML+="ObjectConstructor:"+obj.constructor+"&nbsp;&nbsp;&nbsp;&nbsp;  <br/>  ObjectTypeOf:"+typeof obj+"<br/>";
        }
        catch (e)
        {
            odiv.innerHTML+="ObjectConstructor:undefined<br/>"
        }
        odiv.innerHTML+="<hr/>"
        for (var i in obj )
        {
            try
            {
                var tag="NULL";
                try{
                    if(obj[i].nodeType==1){
                        tag=obj[i]+"&nbsp;&nbsp;&nbsp;&nbsp;||"+obj[i].nodeName+"||";
                    }else
                    {
                        tag=obj[i];
                    }

                }catch(e){tag="GetError:"+obj[i];}
            odiv.innerHTML+=">> "+i.toString()+" >>>> "+tag+"      <br/>";

            }catch(e)
            {
            odiv.innerHTML+=">>"+i+" 异常："+e.toString()+"           <br/>";
            }
        }
        odiv.innerHTML+="<hr/>"
    }
    else
    {
        odiv.innerHTML=title;
    }
    contdiv.appendChild(odiv);
    //document.body.appendChild(contdiv);
    newwin=window.open("", "newwin", "height=250, width=250,toolbar=no,scrollbars=no,menubar=no");
    if(!!newwin)
        newwin.document.write(contdiv.innerHTML)
    else
        document.body.appendChild(contdiv);

    }
//}}}
})()

//HC={SB:33};
//玩对象
function TestObj()
{
var Person=function() {this.name="寒晨";this.age=23;};
Person.constructor="String";
Person.toString=function(){return "String"};
Person.prototype.toString=function(){return "String"};
Person.tt=5;
var p= new Person();
console.writeobj(p);

}


TestObj()
