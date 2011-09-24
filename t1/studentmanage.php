<?php
//{{{ 数据初始化
require_once('dbconn.php');  //导入数据库操作
$no='';
$name='';
$sex='';
$class='';
$birthdata='';
$schoolyear='';
$place='';
$remarks='';
$oper='';
$msg='';
$msgfmt='<div style="width:60%%;margin:10px auto 0px auto;;background-color:%s;font-size:18px;text-align:center;" >%s</div>';
//$msg2=sprintf($msgfmt,'red','学号已存在,无法插入!!');
//print $msg2;
//}}}

//{{{  预定义一些方法
    //进行安全的获取post参数方法
    function getparm($id)
    {
        if(trim($_POST[$id])!='')
        {
            return str_replace("\'","", trim($_POST[$id]));
        }
        return '';
    }
    //检查该编号学生是否存在
    function checkstudentno($no)
    {

        global $db;
        $sql='select * from student where no=\''.$no.'\' ';
        $ret=$db->query($sql);
       return  ($db->db_num_rows() > 0);
    }
    //查询表中所有的记录
    function selectall()
    {
        global $db;
        $sql='select * from student ';
        return $db->query($sql);
    }
    //查询某ID的数据
    function selectsingle($id)
    {
        global $db;
        $sql=sprintf('select * from student where no=\'%s\'',$id);
        $db->query($sql);
        return $db;
    }
    //删除某ID的数据
    function deletesingle($id)
    {
        global $db;
        $sql=sprintf('delete from student where no=\'%s\'',$id);
        //print $sql;
        $ret=$db->query($sql);
        return $ret;
    }
 //}}}

//{{{  插入数据部分
    if($_POST['hf_oper']=='insert')
    {
    //print "进行插入方法";

        $checkok=false;
        $no=getparm('txt_no');
        $name=getparm('txt_name');
        $sex=getparm('chk_sex');
        $class=getparm('txt_class');
        $birthdata=getparm('txt_birthdata');
        $schoolyear=getparm('txt_schoolyear');
        $place=getparm('txt_place');
        $remarks=getparm('txt_remarks');

        if(!checkstudentno($no))
        { 
            $checkok=true;
        }
        else
        {

           $msg=sprintf($msgfmt,'red','学号已存在,无法插入!!');
        }
        if($checkok)
        {
            $sql= sprintf('insert student(no,sex,name,class,birthdate,schoolyear,birthplace,remarks)
            values(\'%s\',\'%s\',\'%s\',\'%s\',\'%s\',%s,\'%s\',\'%s\')',
            $no,$sex,$name,$class,$birthdata,$schoolyear,$place,$remarks); 
            $db->query($sql);
            $msg=sprintf($msgfmt,'#FFA','插入成功');
        }
    }
//}}}

//{{{ 准备编辑某条数据预处理
    if($_POST['hf_oper']=='show')
    {
       // $msg=sprintf($msgfmt,'#FFA','编辑更新数据请输入');
        $tar_no=$_POST['hf_stno'];
        selectsingle($tar_no);
        $row=$db->fetch_ass();
        $no=$row['no'];
        $name=$row['name'];
        $sex=$row['sex'];
        $class=$row['class'];
        $birthdata=$row['birthdate'];
        $schoolyear=$row['schoolyear'];
        $place=$row['birthplace'];
        $remarks=$row['remarks'];
        $msg=sprintf($msgfmt,'#FFF','<script >fulledit(\''.$no.'\')</script>');
    }
//}}}

//{{{ 数据更新方法
    if($_POST['hf_oper']=='update')
    {

        $no=$_POST["hf_stno"];
        $name=getparm('txt_name');
        $sex=getparm('chk_sex');
        $class=getparm('txt_class');
        $birthdata=getparm('txt_birthdata');
        $schoolyear=getparm('txt_schoolyear');
        $place=getparm('txt_place');
        $remarks=getparm('txt_remarks');

        $sql= sprintf('update  student  set  sex=\'%s\',name=\'%s\',class=\'%s\',birthdate=\'%s\',schoolyear=%s,birthplace=\'%s\',remarks=\'%s\'   where no=\'%s\'',
        $sex,$name,$class,$birthdata,$schoolyear,$place,$remarks,$no); 
        if($db->query($sql))
        {
            $msg=sprintf($msgfmt,'#FFA','更新数据成功!');
        }
        else
       {
            $msg=sprintf($msgfmt,'red','更新数据失败!!! SQL:'.$sql);
       }
    }
///}}}
    
//{{{ 删除记录方法
    if($_POST['hf_oper']=='delete')
    {
        $st_no=$_POST["hf_stno"];
        if(deletesingle($st_no))
        {
        $msg=sprintf($msgfmt,'#FFA','删除数据成功!');
        }
        else{
        $msg=sprintf($msgfmt,'red','删除数据失败!!!');
        }
    }
///}}}
?>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
	<meta http-equiv="Content-Type" content="text/html;charset=UTF-8">
	<title>学籍管理系统 一.学生管理</title>
    <script type="text/javascript" src="./My97DatePicker/WdatePicker.js"></script>
</head>
<body>
<form method="post" id="form_oper" style="width:100%;text-align:center;" >

<!--{{{  操作数据的一些js方法-->
<script type="text/javascript">
    //js根据ID获取对象方法
    function $(id)
    {

        var type=typeof(id);
        var obj;
        if(type==="string")
        {
            obj=window.document.getElementById(id);
            if(!!!obj){alert('id:'+id+'对象不存在!!!!')}
        }
        else{alert('非法参数')}
        return obj;
    }
    //去字符串2边空格方法
    function trim(str)
    {
        var type=typeof(str);
        if(type==="string")
        {
            str=str.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
        }
        else{alert('非法参数')}
        return str;
    }
    
    //编辑按钮的js事件`
    function submitedit(id)
    {
        var frm=$("form_oper");
        var hf_op=$("hf_oper");
        var hf_no=$("hf_stno");
        hf_no.value=id;
        hf_op.value='show';
        frm.submit();
    }

    //编辑数据显示后调用 由php调用
    function fulledit(id)
    {
        var frm=$("form_oper");
        var hf_op=$("hf_oper");
        var hf_no=$("hf_stno");
        var btn=$("btn_enter");
        var txt=$("txt_no");
        txt.disabled="disabled";
        btn.value="修改";
        hf_no.value=id;
        hf_op.value='update';
    }

    //删除按钮的js事件
    function submitdelete(id)
    {
        var frm=$("form_oper");
        var hf_op=$("hf_oper");
        var hf_no=$("hf_stno");
        hf_no.value=id;
        hf_op.value='delete';
        frm.submit();
    }

    //表单提交的验证!通过后自动提交
    function submitfun()
    {
        var frm=$("form_oper");
        var hf_op=$("hf_oper");
        var hf_no=$("hf_stno");
        // 进行表单验证!!
        //alert(">>"+trim($("txt_no").value)+">>");
        if(!checkinput('txt_no','学生编号不能为空,必须唯一的,否则无法插入!!',false)){return false}
        if(!checkinput('txt_name','学生名称不能为空',false)){return false}
        if(!checkinput('txt_class','学生班级不能为空',false)){return false}
        if(!checkinput('txt_birthdata','学生出身日期不能为空,请用控件选择',false)){return false}
        if(!checkinput('txt_schoolyear','学生入学年份不能为空,必须是4位数字,否则无法插入!!',true)){return false}
        if(!checkinput('txt_place','学生籍贯不能为空',false)){return false}

        if(hf_op.value=="show")
        {
            if(trim(hf_no.value) !="")
            {
            hf_op.value="update";
            }
            else
            {
                alert("st_no ==string.empty error!!!!")
            }
        }
        if(hf_op.value=="")
        {
            hf_op.value="insert";
        }
       frm.submit(); 
        //alert('ok!');
    }
    
    //取消按钮的方法,清除框中数据
    function  formcancel()
    {
        var  list= window.document.getElementsByTagName("input");
        for(var i=0;i<list.length;i++)
        {
            if(list[i].type=="text")
            {
             list[i].value="";
             list[i].disabled="";
            }
        }

        $("txt_remarks").innerHTML="";
        $("hf_stno").value="";
        $("hf_oper").value="";
        $("btn_enter").value="添加";
        $("btn_cancel").value="取消"
    }

    //表单单个文本框验证方法
    function checkinput(id,msg,isnum)
    {
        var val=$(id).value;
        if(trim(val)!="")
        {
            if(isnum)
            {

                var re = /^[0-9]+.?[0-9]*$/;
                if( !re.test(val))
                {
                    alert(msg)
                    $(id).focus();
                    return false;
                }
            }
        } 
        else
        {
            alert(msg);
            $(id).focus();
            return false;
        }
        return true;

    }
</script>
<!--}}}-->

<!--{{{  HTML 显示数据表格 -->
<table cellspacing="0" cellpadding="0" border="1" style="width:60%;text-align:left;margin:20px auto 40px auto;" >
<tr>
<th colspan="11" style="text-align:center;">学员信息表</th>
</tr>
<tr>
    <td>&nbsp;&nbsp;</td>
    <td>学号</td>
    <td>姓名</td>
    <td>性别</td>
    <td>班级</td>
    <td>出生年月</td>
    <td>入学年份</td>
    <td>籍贯</td>
    <td>[编辑]</td>
    <td>[删除]</td>
    <td>&nbsp;&nbsp;</td>
</tr>
<?php
    selectall();
    while($line=$db->fetch_ass())
    {
?>
<tr>
    <td>&nbsp;&nbsp;</td>
    <td><?php echo($line['no']);?></td>
    <td><?php echo($line['name']);?></td>
    <td><?php echo($line['sex']=='M'?'男':'女');?></td>
    <td><?php echo($line['class']);?></td>
    <td><?php echo($line['birthdate']);?></td>
    <td><?php echo($line['schoolyear']);?></td>
    <td><?php echo($line['birthplace']);?></td>
    <td>[<a href="javascript:submitedit('<?php echo($line['no']) ?>')" >编辑</a>]</td>
    <td>[<a href="javascript:submitdelete('<?php echo($line['no']) ?>')" >删除</a>]</td>
    <td>&nbsp;&nbsp;</td>
</tr>
<?php } ?>
<tr>
<th colspan="11" style="tex-align:center;"></th>
</tr>
</table>
<!-- }}}-->

<!--{{{  HTML 修改和新增的table -->
<table border="1"  cellspacing="0" cellpadding="0" style="width:60%;margin:0px auto 0px auto;text-align:left;" >
<tr>
    <th colspan="5" style="text-align:center;"><span id="oper_title">学生管理操作</span></th>
</tr>
<tr>
    <td>&nbsp;&nbsp;</td>
    <td>学号:</td>
    <td><input type="text" id="txt_no" name="txt_no"    value="<?php echo($no); ?>" /></td>
    <td>新增必填且唯一 , 修改不可变</td>
    <td>&nbsp;&nbsp;</td>
</tr>
<tr>
    <td>&nbsp;&nbsp;</td>
    <td>姓名:</td>
    <td><input type="text" id="txt_name" name="txt_name" value="<?php echo($name); ?>" /></td>
    <td>必填</td>
    <td>&nbsp;&nbsp;</td>
</tr>
<tr>
    <td>&nbsp;&nbsp;</td>
    <td>性别:</td>
    <td>
        <input type="radio" name="chk_sex"  value="M" <?php if($sex!='F'){print "checked=\"checked\"";} ?> />男 
        &nbsp;&nbsp;
<input type="radio" value="F" <?php if($sex=='F'){print "checked=\"checked\"";} ?> name="chk_sex" />女  
    </td>
    <td>&nbsp;&nbsp;</td>
    <td>&nbsp;&nbsp;</td>
</tr>
<tr>
    <td>&nbsp;&nbsp;</td>
    <td>班级:</td>
    <td><input type="text" name="txt_class" value="<?php echo($class); ?>" id="txt_class" /> </td>
    <td>必填</td>
    <td>&nbsp;&nbsp;</td>
</tr>
<tr>
    <td>&nbsp;&nbsp;</td>
    <td>出生年月</td>
    <td><input type="text" name="txt_birthdata" id="txt_birthdata" onClick="WdatePicker()" value="<?php echo($birthdata); ?>" />  </td>
    <td>进行选择 必填</td>
    <td>&nbsp;&nbsp;</td>
</tr>
<tr>
    <td>&nbsp;&nbsp;</td>
    <td>入学年份:</td>
    <td><input type="text" name="txt_schoolyear" id="txt_schoolyear" value="<?php echo($schoolyear); ?>" /> </td>
    <td>只允许输入4位数字 必填</td>
    <td>&nbsp;&nbsp;</td>
</tr>
<tr>
    <td>&nbsp;&nbsp;</td>
    <td>籍贯:</td>
    <td><input type="text" name="txt_place" id="txt_place" value="<?php echo($place); ?>" /> </td>
    <td>必填</td>
    <td>&nbsp;&nbsp;</td>
</tr>
<tr>
    <td>&nbsp;&nbsp;</td>
    <td>备注:</td>
    <td><textarea id="txt_remarks" name="txt_remarks" ><?php echo($remarks); ?></textarea></td>
    <td>可选填</td>
    <td>&nbsp;&nbsp;</td>
</tr>
<tr>
    <th colspan="5" style="text-align:center;"> 
        <input type="hidden" value="" name="hf_stno" id="hf_stno" >
        <input type="hidden" value="" name="hf_oper" id="hf_oper" >   
        <input type="button" value="添加" id="btn_enter" name="btn_enter" onclick="submitfun()" >
        &nbsp;&nbsp;
        <input type="button" id="btn_cancel" name="btn_cancel" onclick="formcancel()" value="取消" />  
        
    </th>
</tr>
</table>
<!-- }}}-->
</form>
<?php  $db->close();/*关闭连接*/ echo($msg);/*输出消息)*/ ?>
</body>
</html>
