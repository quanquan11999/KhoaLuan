using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KhoaLuan.Models;

namespace KhoaLuan.ViewModels
{
	public class RoomDetailsModel
	{
		public MotelRoom MotelRoom { get; set; }
		public List<Renter> ListRenter { get; set; }
		public Bill Bill { get; set; }
		public List<BillDetail> ListBillDetail { get; set; }
	}
}