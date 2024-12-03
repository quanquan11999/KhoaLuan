using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KhoaLuan.Models;

namespace KhoaLuan.ViewModels
{
	public class ChangeInfoModel
	{
		public Info InfoAccount { get; set; }
		public Province ProvinceAccount { get; set; }
		public District DistrictAccount { get; set; }
	}
}