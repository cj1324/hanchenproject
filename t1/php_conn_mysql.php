<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
	<meta http-equiv="Content-Type" content="text/html;charset=UTF-8">
	<title>测试mysql连接</title>
</head>
<body>
<?php
    $conn=mysql_connect("localhost","hanchen","654321") or die("Could not connect :".mysql_error());
    print "Connected successfully!!";
    mysql_select_db("studentdb") or die("Not chang database ".mysql_error());
    $sqlstr="select * from testtb ";
    $curse=mysql_query($sqlstr) or die("Query failed".mysql_error());

    print "<ul>";
        while($line=mysql_fetch_array($curse,MYSQL_ASSOC))
        {
            print "<li>$line[name]</li>";
        }
    print "</ul>";
    mysql_free_result($curse);
    mysql_close($conn);
    print "Closed  successfully!!";
?>
</body>
</html>
