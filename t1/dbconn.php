<?php 
$db = new mysql("localhost","hanchen","654321","studentdb",null); 
   
class mysql{ 
    private    $db_host; 
    private    $db_user; 
    private    $db_password; 
    private    $db_table; 
    private    $db_conn;           //数据库连接标识; 
    private    $result;         //执行query命令的结果资源标识 
    private    $sql;      //sql执行语句 
       
    function __construct($db_host,$db_user,$db_password,$db_table,$db_conn){ 
        $this->db_host     = $db_host; 
        $this->db_user     = $db_user; 
        $this->db_password = $db_password; 
        $this->dbconn      = $db_conn;
        $this->db_table    = $db_table;
        $this->connect(); 
    } 
       
    function connect(){ 
        $this->db_conn = @mysql_connect($this->db_host,$this->db_user,$this->db_password) or die($this->show_error("数据库链接错误,请检查数据库链接配置！")); 
        if(!mysql_select_db($this->db_table,$this->db_conn)){ 
            echo "没有找到数据表：".$this->db_table; 
        } 
        mysql_select_db($this->db_table,$this->db_conn); 
    } 

       

    /*执行SQL语句的函数*/ 

    public function query($sql){ 
        if(trim($sql)==''){ 
            $this->show_error("你的sql语句不能为空！"); 
        }else{           
            $this->sql = $sql; 
        } 
        $result = mysql_query($this->sql,$this->db_conn); 
        return $this->result = $result; 

    } 


    // 根据select查询结果计算结果集条数  
    public function db_num_rows(){  
         if($this->result==null){ 
            if($this->show_error){ 
                $this->show_error("sql语句错误!"); 
            }            
         }else{ 
            return  mysql_num_rows($this->result);  
         } 
    } 
       

    /*

    mysql_fetch_row()    array  $row[0],$row[1],$row[2]

    mysql_fetch_array()  array  $row[0] 或 $row[id]

    mysql_fetch_assoc()  array  用$row->content 字段大小写敏感

    mysql_fetch_object() object 用$row[id],$row[content] 字段大小写敏感

    */ 

    /*取得记录集,获取数组-索引和关联,使用$row['content'] */ 
    public function fetch_array()   
    {        
        return @mysql_fetch_array($this->result);  
    }    

       

    //获取关联数组,使用$row['字段名'] 
    public function fetch_ass()  
    {  
        return @mysql_fetch_assoc($this->result);  
    } 

    //获取数字索引数组,使用$row[0],$row[1],$row[2] 
    public function fetch_row()  
    {  
        return @mysql_fetch_row($this->result);  
    } 

    //简化的delete 

    function delete($table,$where = ""){ 
        $table = $this->fulltablename($table); 
        if(emptyempty($where)){ 
            $this->show_error("条件不能为空!"); 
        }else{ 
            $where = " where ".$where; 
        } 
        $sql = "DELETE FROM $table ".$where; 
        return $this->query($sql); 

    } 

       

    //取得上一步 INSERT 操作产生的 ID 

    public function insert_id(){ 
        return mysql_insert_id(); 
    } 

       

    //取得 MySQL 服务器信息 

    public function mysql_server($num=''){ 

        switch ($num){ 
            case 1 : 
            return mysql_get_server_info(); //MySQL 服务器信息    
            break; 
            case 2 : 
            return mysql_get_host_info();   //取得 MySQL 主机信息 
            break; 
            case 3 : 
            return mysql_get_client_info(); //取得 MySQL 客户端信息 
            break; 
            case 4 : 
            return mysql_get_proto_info();  //取得 MySQL 协议信息 
            break; 
            default: 
            return mysql_get_client_info(); //默认取得mysql版本信息 
        } 

    } 

       

    //析构函数，自动关闭数据库,垃圾回收机制 
    public function close() 
    {
        if(!empty($this->result)){ 
            mysql_free_result($this->result);
        }
        mysql_close($this->db_conn);
    } 
       
    function show_error($str){       
        echo "<script language='Javascript'> alert('".$str."');history.back(-1);</script>"; 

    } 

       

} 

?>
