using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Guess.Common;
using Guess.Model;

namespace Guess.DataBase
{
    public class Manage_T_Game
    {
        
        #region 创建一轮游戏
        /// <summary>
        /// 游戏1 创建一期
        /// </summary>
        public void CreateGameOne()
        {
            char[] numbers={'1','2','3','4','5','6','7','8','9','0'};
         
            SqlParameter[] paramters = {
                                           new SqlParameter("@NumOne",SqlDbType.Int,4),
                                           new SqlParameter("@NumTwo",SqlDbType.Int,4),
                                           new SqlParameter("@NumThree",SqlDbType.Int,4),
                                           new SqlParameter("@Bouns",SqlDbType.Int,4),
                                           new SqlParameter("@InvolvedNum",SqlDbType.Int,4),
                                           new SqlParameter("@WinningNum",SqlDbType.Int,4)
                                        };
            for (int i = 0; i < 3; i++)
            {
              string num=  RanHelper.RandomKey(numbers ,8,i);
              paramters[i].Value = Convert.ToInt32((num[7-(2*i)]).ToString());
            }
            Random rd = new Random();
            int total=rd.Next(0,3000);
            paramters[3].Value = total + 10000;
           
                string num2 = RanHelper.RandomKey(numbers, 9, 9);
                paramters[4].Value = Convert.ToInt32(num2.Substring(2,2));
                paramters[5].Value = Convert.ToInt32(num2.Substring(4,2));
            

            DBHelper.RunProc("sp_CreateGameOne", paramters);

        }


        /// <summary>
        /// 游戏2 创建一期
        /// </summary>
        public void CreateGameTwo()
        {
            char[] numbers = { '1', '2', '3', '4', '5', '6' };

            SqlParameter[] paramters = {
                                           new SqlParameter("@NumOne",SqlDbType.Int,4),
                                           new SqlParameter("@NumTwo",SqlDbType.Int,4),
                                           new SqlParameter("@NumThree",SqlDbType.Int,4),
                                           new SqlParameter("@Bouns",SqlDbType.Int,4),
                                           new SqlParameter("@InvolvedNum",SqlDbType.Int,4),
                                           new SqlParameter("@WinningNum",SqlDbType.Int,4)
                                        };
            for (int i = 0; i < 3; i++)
            {
                string num = RanHelper.RandomKey(numbers, 8, i);
                paramters[i].Value = Convert.ToInt32((num[7 - (2 * i)]).ToString());
            }
            Random rd = new Random();
            int total = rd.Next(0, 3000);
            paramters[3].Value = total + 10000;

            string num2 = RanHelper.RandomKey(numbers, 9, 9);
            paramters[4].Value = Convert.ToInt32(num2.Substring(2, 2));
            paramters[5].Value = Convert.ToInt32(num2.Substring(4, 2));


            DBHelper.RunProc("sp_CreateGameTwo", paramters);

        }



        #endregion



        #region 获取数据

        public int GetNewPhases(int gametype)
        {
            int ret = 0;
            string sql = "select max(F_Phases) from T_Game where  F_Type=" + gametype;
            Object obj = DBHelper.GetSingle(sql);

            if (obj != null)
            {
                ret = Convert.ToInt32(obj);
            }
            return ret;
        }


        private SqlDataReader GetSDRByWhere(string where, SqlParameter[] parameters)
        {
            string sql = "select  F_Id,F_Phases,F_Type,F_LotteryDate,F_NumOne,F_NumTwo,F_NumThree,F_NumFour,F_NumFive,F_Bonus,F_InvolvedNum,F_WinningNum,F_Lottery,F_CreateDate  from T_Game";

            if (!string.IsNullOrEmpty(where))
            {
                sql += "  where " + where;
            }
            return DBHelper.GetCursor(sql, parameters);

        }

        public T_Game GetModelByPro(SqlDbType sdt,int size,string fieldName,object value)
        {
            string where = string.Format(" {0}=@{0}", fieldName);
            SqlParameter[] parameters = { 
                                        new SqlParameter("@"+fieldName,sdt,size)
                                        };
            parameters[0].Value = value;
          SqlDataReader sdr=  GetSDRByWhere(where, parameters);
          if (sdr != null)
          {
              return GetModelBySDR(sdr);
          }
          return null;
        }

        public T_Game GetModelById(int id)
        {
            return GetModelByPro(SqlDbType.Int, 4, "F_Id",id);

        }

        #endregion

        #region 修改数据

        public int Update(Guess.Model.T_Game t_m)
        {
            string sql = string.Format("update T_Game set {0}=@{0},{1}=@{1},{2}=@{2},{3}=@{3},{4}=@{4},{5}=@{5},{6}=@{6},{7}=@{7},{8}=@{8},{9}=@{9},{10}=@{10} where {11}=@{11}",
                "F_Phases",
                "F_Type",
                "F_NumOne",
                "F_NumTwo",
                "F_NumThree",
                "F_NumFour",
                "F_NumFive",
                "F_Bonus",
                "F_InvolvedNum",
                "F_WinningNum",
                "F_Lottery",
                "F_Id"
                );

            return DBHelper.NonQuery(sql,SetParameter(t_m));


        }

        #endregion

        #region 辅助方法

        public T_Game GetModelBySDR(SqlDataReader sdr)
        {
            T_Game tg=null;
            if (sdr.Read())
            {
                tg = new T_Game();
                tg.F_Id = sdr.GetInt32(0);
                tg.F_Phases = sdr.GetInt32(1);
                tg.F_Type = sdr.GetInt32(2);
                tg.F_LotteryDate = sdr.GetDateTime(3);
                tg.F_NumOne = sdr.GetInt32(4);
                tg.F_NumTwo = sdr.GetInt32(5);
                tg.F_NumThree = sdr.GetInt32(6);
                tg.F_NumFour = sdr[7] ==DBNull.Value ? 0:sdr.GetInt32(7);
                tg.F_NumFive = sdr[8] == DBNull.Value ? 0 : sdr.GetInt32(8);
                tg.F_Bonus = sdr.GetInt32(9);
                tg.F_InvolvedNum = sdr.GetInt32(10);
                tg.F_WinningNum = sdr.GetInt32(11);
                tg.F_Lottery = sdr.GetBoolean(12);
                tg.F_CreateDate = sdr.GetDateTime(13);

            }


            return tg;
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public SqlParameter[] SetParameter(Guess.Model.T_Game model)
        {

            SqlParameter[] parameters = {
					
					new SqlParameter("@F_Phases", SqlDbType.Int,4),
					new SqlParameter("@F_Type", SqlDbType.Int,4),
					new SqlParameter("@F_NumOne", SqlDbType.Int,4),
					new SqlParameter("@F_NumTwo", SqlDbType.Int,4),
					new SqlParameter("@F_NumThree", SqlDbType.Int,4),
					new SqlParameter("@F_NumFour", SqlDbType.Int,4),
					new SqlParameter("@F_NumFive", SqlDbType.Int,4),
					new SqlParameter("@F_Bonus", SqlDbType.Int,4),
					new SqlParameter("@F_InvolvedNum", SqlDbType.Int,4),
					new SqlParameter("@F_WinningNum", SqlDbType.Int,4),
					new SqlParameter("@F_Lottery", SqlDbType.Bit,1),
                    new SqlParameter("@F_Id", SqlDbType.Int,4)};
            
            parameters[0].Value = model.F_Phases;
            parameters[1].Value = model.F_Type;
            parameters[2].Value = model.F_NumOne;
            parameters[3].Value = model.F_NumTwo;
            parameters[4].Value = model.F_NumThree;
            parameters[5].Value = model.F_NumFour;
            parameters[6].Value = model.F_NumFive;
            parameters[7].Value = model.F_Bonus;
            parameters[8].Value = model.F_InvolvedNum;
            parameters[9].Value = model.F_WinningNum;
            parameters[10].Value = model.F_Lottery;
            parameters[11].Value = model.F_Id;
            
            return parameters;

        }
        #endregion
    }
}
