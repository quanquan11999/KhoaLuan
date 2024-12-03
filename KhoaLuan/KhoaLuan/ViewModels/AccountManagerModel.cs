using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KhoaLuan.Models;

namespace KhoaLuan.ViewModels
{
	public class AccountManagerModel
	{
		public Account Account { get; set; }
		public AccStatu AccountStatus { get; set; }
		public Info AccountInfo { get; set; }
		public Province Province { get; set; }
		public District District { get; set; }
	}
}