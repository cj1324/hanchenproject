using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Guess.Common;

namespace Guess.DataBase
{
    /// <summary>
    /// 对分页存储过程进行封装的类
    /// </summary>
    public class RecordFromPage
    {
        #region 该类的构造函数(有重载)
        public RecordFromPage()
        {
            //函数本身为空
            this._procname = "sp_GetRecordFromPage";
        }

        public RecordFromPage(string tablename)
            : this()
        {
            this._tablename = tablename;

        }

        #endregion

        #region 分页所需的属性


        private string _procname;


        /// <summary>
        /// 存储过程名
        /// </summary>
        public string ProcName
        {
            get { return _procname; }
            set { _procname = value; }

        }


        private int _pageindex;

        /// <summary>
        /// 需要获取的当前页属性
        /// </summary>
        public int PageIndex
        {
            set { _pageindex = value; }
            get { return _pageindex; }
        }
        private int _pagesize;

        /// <summary>
        /// 需要获取每页的总条数
        /// </summary>
        public int PageSize
        {
            set { _pagesize = value; }
            get { return _pagesize; }
        }

        private string _tablename;

        /// <summary>
        /// 需要获取的表名
        /// </summary>
        public string TableName
        {
            set { _tablename = value; }
            get { return _tablename; }
        }


        private string _fields;


        /// <summary>
        /// 需要查询字段名
        /// </summary>
        public string Fields
        {
            set { _fields = value; }
            get { return _fields; }

        }



        private string _orderfields;



        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderFields
        {
            set { _orderfields = value; }
            get { return _orderfields; }

        }


        private string _where;



        /// <summary>
        /// where 条件
        /// </summary>
        public string Where
        {
            set { _where = value; }
            get { return _where; }

        }



        private int _totalcount;


        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount
        {
            set { _totalcount = value; }
            get { return _totalcount; }
        }


        private int _totalpage;


        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            set { _totalpage = value; }
            get { return _totalpage; }
        }


        private SqlConnection _conn;


        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public SqlConnection Conn
        {
            set { _conn = value; }
            get { return _conn; }
        }

        private string _connstr;


        /// <summary>
        /// 数据连接字符串
        /// </summary>
        public string ConnStr
        {
            get { return _connstr; }
            set { _connstr = value; }
        }

        #endregion

        #region 调用的分页方法(获取DataTable)
        public DataTable GetDt()
        {
            DataTable dt;
            SqlParameter[] parmenters ={
                new SqlParameter("@RETURN_VALUE",SqlDbType.Int),
                new SqlParameter("@TableName",SqlDbType.NVarChar,100),
                new SqlParameter("@Fields",SqlDbType.NVarChar,200),
                new SqlParameter("@OrderField",SqlDbType.NVarChar,100),
                new SqlParameter("@sqlWhere",SqlDbType.NVarChar,500),
                new SqlParameter("@pageSize",SqlDbType.Int),
                new SqlParameter("@pageIndex",SqlDbType.Int)
            };
            parmenters[0].Direction = ParameterDirection.ReturnValue;

            parmenters[1].Value = _tablename;
            if (String.IsNullOrEmpty(_fields))
            {
                parmenters[2].Value = null;
            }
            else
            {

                parmenters[2].Value = _fields;
            }

            if (String.IsNullOrEmpty(_orderfields))
            {
                parmenters[3].Value = null;
            }
            else
            {
                parmenters[3].Value = _orderfields;
            }
            if (String.IsNullOrEmpty(_where))
            {
                parmenters[4].Value = null;
            }
            else
            {
                parmenters[4].Value = _where;
            }

            if (_pagesize == 0)
            {
                parmenters[5].Value = null;
            }
            else
            {
                parmenters[5].Value = _pagesize;
            }

            parmenters[6].Direction = ParameterDirection.InputOutput;
            if (_pageindex == 0)
            {
                parmenters[6].Value = null;
            }
            else
            {
                parmenters[6].Value = _pageindex;
            }

            dt = DBoperationsGetDt(parmenters);
            return dt;
        }

        public DataTable GetDt(string tablename)
        {
            _tablename = tablename;
            return GetDt();

        }


        public DataTable GetDt(string tablename, string fields)
        {
            _tablename = tablename;
            _fields = fields;
            return GetDt();
        }


        public DataTable GetDt(string tablename, string fields, string where)
        {
            _tablename = tablename;
            _fields = fields;
            _where = where;
            return GetDt();
        }

        public DataTable GetDt(string tablename, string fields, string where, string order)
        {
            _tablename = tablename;
            _fields = fields;
            _where = where;
            _orderfields = order;
            return GetDt();
        }


        public DataTable GetDt(string tablename, string fields, string where, string order, int pageindex, int pagesize)
        {
            _tablename = tablename;
            _fields = fields;
            _where = where;
            _orderfields = order;
            _pageindex = pageindex;
            _pagesize = pagesize;
            return GetDt();
        }

        #endregion

        #region  对数据库进行操作


        public DataTable DBoperationsGetDt(SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            if (!String.IsNullOrEmpty(_connstr))
            {
                try
                {
                    _conn = new SqlConnection(_connstr);
                }
                catch (SqlException e)
                {
                    throw e;
                }

            }
            if (_conn == null)
            {
                throw new Exception("没有初始化数据库连接对象!!");
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.Parameters.AddRange(parameters);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = _procname;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            try
            {
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                Logo lg = new Logo();
                lg.WriteLogo(ex,"没有获取到分页数据!");
            }
            _totalcount = Convert.ToInt32(cmd.Parameters[0].Value);
            _pageindex = Convert.ToInt32(cmd.Parameters[6].Value);
            if (_totalcount == 0)
            {

                return null;
            }

            if (_pagesize != 0)
            {
                _totalpage = _totalcount / _pagesize;
                if (_totalcount % _pagesize != 0)
                {
                    _totalpage++;
                }
            }
            else
            {
                _totalpage = _totalcount / 20;
                if (_totalcount % 20 != 0)
                {
                    _totalpage++;
                }
            }


            return dt;
        }

        public SqlDataReader DBoperationsGetCursor(SqlParameter[] parameters)
        {
            SqlDataReader sdr=null;
            if (!String.IsNullOrEmpty(_connstr))
            {
                try
                {
                    _conn = new SqlConnection(_connstr);
                }
                catch (SqlException e)
                {
                    throw e;
                }

            }
            if (_conn == null)
            {
                throw new Exception("没有初始化数据库连接对象!!");
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.Parameters.AddRange(parameters);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = _procname;
            try
            {
                _conn.Open();
                sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (SqlException ex)
            {

                Logo lg = new Logo();
                lg.WriteLogo(ex, "分页存储过程获取游标失败!!!");
            }

            _totalcount = Convert.ToInt32(cmd.Parameters[0].Value);
            _pageindex = Convert.ToInt32(cmd.Parameters[6].Value);
            if (_totalcount == 0)
            {

                return null;
            }

            if (_pagesize != 0)
            {
                _totalpage = _totalcount / _pagesize;
                if (_totalcount % _pagesize != 0)
                {
                    _totalpage++;
                }
            }
            else
            {
                _totalpage = _totalcount / 20;
                if (_totalcount % 20 != 0)
                {
                    _totalpage++;
                }
            }
            return sdr;

        }

        #endregion

        #region 把存储过程插入数据库(对每个数据库最好只执行一次)  待实现

        public void ProcInsert()
        {
            if (!String.IsNullOrEmpty(_connstr))
            {
                try
                {
                    _conn = new SqlConnection(_connstr);
                }
                catch (SqlException e)
                {
                    throw e;
                }

            }
            if (_conn == null)
            {
                throw new Exception("没有初始化数据库连接对象!!");
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(""); //把存储过程拼接起来 待实现
            #region 存储过程SQL代码

            /*
            set ANSI_NULLS ON
            set QUOTED_IDENTIFIER ON
            go
            --create /alter
            alter   PROCEDURE [dbo].[sp_GetRecordFromPage]
            @TableName nvarchar(100),        --表名 (必须!)
            @Fields nvarchar(200) = N'*',    --字段名(全部字段为*)
            @OrderField nvarchar(100)=N'F_Id',        --排序字段(必须!支持多字段)   默认F_Id 通用性差  
            @sqlWhere nvarchar(500) = Null,--条件语句(不用加where)
            @pageSize int=20,                    --每页多少条记录(默认一页20条)
            @pageIndex int = 1 in output       --指定当前为第几页(无论传入多小,只要小于0 就显示第一页) (无论传入多大,只要超出界限就显示最后一页)
            --@return_value int output
            --@distinct VARCHAR(50)=NULL,   --去除重复值，注意只能是一个字段
            --@top INT=NULL                --查询TOP,不传为全部
            AS 
            BEGIN
                Declare @sql nvarchar(4000);
                Declare @totalRecord int;    
                DECLARE @totalPage INT;
                --计算总记录数
            --IF (@distinct IS NULL OR @distinct='')
            --BEGIN
            IF (@SqlWhere='' OR @sqlWhere IS NULL)
                SET @sql = 'select @totalRecord = count(1) from ' + @TableName
               ELSE
                SET @sql = 'select @totalRecord = count(1) from ' + @TableName + ' where ' + @sqlWhere
            --END
            --ELSE
            --BEGIN
            --IF (@SqlWhere='' OR @sqlWhere IS NULL)
            --    SET @sql = 'select @totalRecord = count(distinct ' + @distinct + ') from ' + @TableName
            --   ELSE
            --    SET @sql = 'select @totalRecord = count(distinct ' + @distinct + ') from ' + @TableName + ' where ' + @sqlWhere
            --END
                EXEC sp_executesql @sql,N'@totalRecord int OUTPUT',@totalRecord OUTPUT--计算总记录数       
                

            --IF(@top>0)
            --BEGIN
            --指定TOP 记录
            --SET @Fields= 'top ' + CAST(@top AS VARCHAR(20)) + ' ' + @Fields;
            --如果总记录数超过TOP数,设总记录数为TOP数
            --IF(@totalRecord>@top) 
            --   SET @totalRecord=@top
            --END

                --计算总页数
                SELECT @totalPage=CEILING((@totalRecord+0.0)/@PageSize)
                --SELECT @totalRecord AS 'fldtotalRecord',@totalPage AS 'fldTotalPage'

            --IF (@distinct IS NULL OR @distinct='')
            --BEGIN
            IF (@SqlWhere='' or @sqlWhere IS NULL)
               SET @sql = 'Select * FROM (select ' + @Fields + ',ROW_NUMBER() Over(order by ' + @OrderField + ') as rowId from ' + @TableName
            ELSE
               SET @sql = 'Select * FROM (select ' + @Fields + ',ROW_NUMBER() Over(order by ' + @OrderField + ') as rowId from ' + @TableName + ' where ' + @SqlWhere    
            --END
            --ELSE
            --BEGIN
            --IF (@SqlWhere='' or @sqlWhere IS NULL)
            --   SET @sql = 'Select * FROM (select ' + @Fields + ',ROW_NUMBER() Over(order by ' + @OrderField + ') as rowId from ' + @TableName
            --ELSE
            --   SET @sql = 'Select * FROM (select ' + @Fields + ',ROW_NUMBER() Over(order by ' + @OrderField + ') as rowId from ' + @TableName + ' where ' + @SqlWhere    
            --SET @sql=@sql + ' GROUP BY ' + @distinct;
            --END
                
                 
                --处理页数超出范围情况

                IF @PageIndex<=0 
                    SET @pageIndex = 1
	            IF @totalPage<=@pageIndex
		            SET @pageIndex=@totalPage
                
               

                 --处理开始点和结束点
                DECLARE @StartRecord INT 
                DECLARE @EndRecord int
                
                SET @StartRecord = (@pageIndex-1)*@PageSize + 1
                SET @EndRecord = @StartRecord + @pageSize - 1

                 --继续合成sql语句
                SET @sql = @sql + ') as tempTable where rowId >=' + CONVERT(VARCHAR(50),@StartRecord) + ' and rowid<= ' + CONVERT(VARCHAR(50),@EndRecord)
                Exec(@sql)
            --set @return_value=@totalPage
	            return @totalRecord
            end
             * */
            #endregion
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = sb.ToString();
            //cmd.ExecuteNonQuery();


        }

        #endregion

    }
}