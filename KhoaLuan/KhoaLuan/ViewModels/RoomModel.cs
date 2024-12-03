using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KhoaLuan.Models;

namespace KhoaLuan.ViewModels
{
	public class RoomModel
	{
		public MotelRoom Motel { get; set; }
		public string provinceName { get; set; }
		public string DistrictName { get; set; }
		public string WardName { get; set; }
	}
}