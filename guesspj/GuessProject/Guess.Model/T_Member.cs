using System;
namespace Guess.Model
{
	/// <summary>
	/// T_Member:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class T_Member
	{
		public T_Member()
		{}
		#region Model
		private int _f_id;
		private string _f_email;
		private string _f_password;
		private bool _f_sex;
		private string _f_nickname;
		private string _f_headpic;
		private string _f_securitypassword;
		private string _f_alipay;
		private string _f_issues;
		private string _f_answer;
		private string _f_initpassword;
		private string _f_mobile;
		private string _f_qq;
		private int _f_level=0;
		private int _f_gold=88;
		private int _f_diamond=0;
		private bool _f_vip=false;
		private string _f_key;
		private int? _f_status=0;
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
		public string F_Email
		{
			set{ _f_email=value;}
			get{return _f_email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string F_Password
		{
			set{ _f_password=value;}
			get{return _f_password;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool F_Sex
		{
			set{ _f_sex=value;}
			get{return _f_sex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string F_NickName
		{
			set{ _f_nickname=value;}
			get{return _f_nickname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string F_Headpic
		{
			set{ _f_headpic=value;}
			get{return _f_headpic;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string F_SecurityPassWord
		{
			set{ _f_securitypassword=value;}
			get{return _f_securitypassword;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string F_Alipay
		{
			set{ _f_alipay=value;}
			get{return _f_alipay;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string F_Issues
		{
			set{ _f_issues=value;}
			get{return _f_issues;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string F_Answer
		{
			set{ _f_answer=value;}
			get{return _f_answer;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string F_InitPassWord
		{
			set{ _f_initpassword=value;}
			get{return _f_initpassword;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string F_Mobile
		{
			set{ _f_mobile=value;}
			get{return _f_mobile;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string F_QQ
		{
			set{ _f_qq=value;}
			get{return _f_qq;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int F_Level
		{
			set{ _f_level=value;}
			get{return _f_level;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int F_Gold
		{
			set{ _f_gold=value;}
			get{return _f_gold;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int F_Diamond
		{
			set{ _f_diamond=value;}
			get{return _f_diamond;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool F_VIP
		{
			set{ _f_vip=value;}
			get{return _f_vip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string F_KEY
		{
			set{ _f_key=value;}
			get{return _f_key;}
		}
		/// <summary>
		/// 0.未进行邮箱认证  1.正常状态 2.锁定状态 3.其他状态
		/// </summary>
		public int? F_Status
		{
			set{ _f_status=value;}
			get{return _f_status;}
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

