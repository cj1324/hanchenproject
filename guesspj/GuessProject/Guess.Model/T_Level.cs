using System;
namespace Guess.Model
{
	/// <summary>
	/// T_Level:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class T_Level
	{
		public T_Level()
		{}
		#region Model
		private int _f_id;
		private int _f_name;
		private int? _f_order;
		private int _f_minnum;
		private string _f_memo;
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
		public int F_Name
		{
			set{ _f_name=value;}
			get{return _f_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? F_Order
		{
			set{ _f_order=value;}
			get{return _f_order;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int F_MinNum
		{
			set{ _f_minnum=value;}
			get{return _f_minnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string F_Memo
		{
			set{ _f_memo=value;}
			get{return _f_memo;}
		}
		#endregion Model

	}
}

