using System;
namespace Guess.Model
{
	/// <summary>
	/// T_Operation:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class T_Operation
	{
		public T_Operation()
		{}
		#region Model
		private int _f_id;
		private int _f_uid;
		private string _f_content;
		private string _f_ip;
        private DateTime _f_time = DateTime.Now;
		/// <summary>
		/// 
		/// </summary>
		public int F_Id
		{
			set{ _f_id=value;}
			get{return _f_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int F_UId
		{
			set{ _f_uid=value;}
			get{return _f_uid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string F_Content
		{
			set{ _f_content=value;}
			get{return _f_content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string F_IP
		{
			set{ _f_ip=value;}
			get{return _f_ip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime F_Time
		{
			set{ _f_time=value;}
			get{return _f_time;}
		}
		#endregion Model

	}
}

