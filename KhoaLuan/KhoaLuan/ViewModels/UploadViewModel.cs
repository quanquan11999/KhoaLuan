using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KhoaLuan.Models;

namespace KhoaLuan.ViewModels
{
	public class UploadViewModel
	{
		public List<Province> ListProvince { get; set; }
		public List<Criterion> ListCriterion { get; set; }
	}
}