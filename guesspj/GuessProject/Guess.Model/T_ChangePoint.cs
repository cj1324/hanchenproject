using System;
namespace Guess.Model
{
	/// <summary>
	/// T_ChangePoint:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class T_ChangePoint
	{
		public T_ChangePoint()
		{}
		#region Model
		private int _f_id;
		private int _f_uid;
		private int _f_type;
		private int? _f_num;
		private int? _f_last;
        private DateTime? _f_time = DateTime.Now;
		private string _f_reason;
		private string _f_executive;
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
		public int F_Type
		{
			set{ _f_type=value;}
			get{return _f_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? F_Num
		{
			set{ _f_num=value;}
			get{return _f_num;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? F_Last
		{
			set{ _f_last=value;}
			get{return _f_last;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? F_Time
		{
			set{ _f_time=value;}
			get{return _f_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string F_Reason
		{
			set{ _f_reason=value;}
			get{return _f_reason;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string F_Executive
		{
			set{ _f_executive=value;}
			get{return _f_executive;}
		}
		#endregion Model

	}
}

