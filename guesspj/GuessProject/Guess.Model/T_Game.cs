using System;
namespace Guess.Model
{
	/// <summary>
	/// T_Game:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class T_Game
	{
		public T_Game()
		{}
		#region Model
		private int _f_id;
		private int _f_phases;
		private int _f_type;
		private DateTime _f_lotterydate;
		private int? _f_numone;
		private int? _f_numtwo;
		private int? _f_numthree;
		private int? _f_numfour;
		private int? _f_numfive;
		private int? _f_bonus;
		private int? _f_involvednum;
		private int? _f_winningnum;
		private bool _f_lottery;
        private DateTime _f_createdate = DateTime.Now;
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
		public int F_Phases
		{
			set{ _f_phases=value;}
			get{return _f_phases;}
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
		public DateTime F_LotteryDate
		{
			set{ _f_lotterydate=value;}
			get{return _f_lotterydate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? F_NumOne
		{
			set{ _f_numone=value;}
			get{return _f_numone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? F_NumTwo
		{
			set{ _f_numtwo=value;}
			get{return _f_numtwo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? F_NumThree
		{
			set{ _f_numthree=value;}
			get{return _f_numthree;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? F_NumFour
		{
			set{ _f_numfour=value;}
			get{return _f_numfour;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? F_NumFive
		{
			set{ _f_numfive=value;}
			get{return _f_numfive;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? F_Bonus
		{
			set{ _f_bonus=value;}
			get{return _f_bonus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? F_InvolvedNum
		{
			set{ _f_involvednum=value;}
			get{return _f_involvednum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? F_WinningNum
		{
			set{ _f_winningnum=value;}
			get{return _f_winningnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool F_Lottery
		{
			set{ _f_lottery=value;}
			get{return _f_lottery;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime F_CreateDate
		{
			set{ _f_createdate=value;}
			get{return _f_createdate;}
		}
		#endregion Model

	}
}

