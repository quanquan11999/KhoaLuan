using KhoaLuan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KhoaLuan.Controllers
{
    public class AddressController : Controller
    {
		QLTroEntities db = new QLTroEntities();
		[HttpPost]
		public ActionResult LoadProvince()
		{
			string result = string.Empty;
			var listProvince = db.Provinces.ToList();
			foreach (Province p in listProvince)
			{
				result += "<option value='" + p.ProvinceID + "'>" +p.Kind+" "+ p.ProvinceName + "</option>" + Environment.NewLine;
			}
			return Json(new { success = true, result = result }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult LoadDistrict(string provinceID)
		{
			int citiIDInt = Int32.Parse(provinceID);
			List<District> LoadDistrict = (from m in db.Districts
										   where m.ProvinceID == citiIDInt
										   select m).OrderBy(t => t.DistrictName).ToList();
			string result = string.Empty;
			if (LoadDistrict.Count == 0)
			{
				result = "<option value=0>Chọn Quận/Huyện</option>";
				return Json(new { success = true, result });
			}
			else
			{
				var townshipId = Request.Cookies["townshipId"];
				foreach (var items in LoadDistrict)
				{
					if (townshipId != null && townshipId.Value == items.ProvinceID.ToString())
					{
						result += "<option value='" + items.DistrictID + "' selected> " + items.DistrictName + " </option>";
					}
					else
					{
						result += "<option value='" + items.DistrictID + "' > " + items.DistrictName + " </option>";
					}
				}
				return Json(new { success = true, result });
			}
		}

		[HttpPost]
		public ActionResult LoadWard(string districtID)
		{
			var result = string.Empty;
			var districtIDInt = Int32.Parse(districtID);
			var listWard = db.Wards.Where(p => p.DistrictID == districtIDInt).ToList();
			foreach (Ward w in listWard)
			{
				result += "<option value='" + w.WardID + "'>" + w.WardName + "</option>";
			}
			return Json(new { success = true, result },JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult LoadProvinceName(string provinceID)
		{
			int provinceIDInt = Int32.Parse(provinceID);
			var province = db.Provinces.SingleOrDefault(p => p.ProvinceID == provinceIDInt);
			return Json(new { success = true, result = province.ProvinceName }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult LoadDistrictName(string districtID)
		{
			int districtIDInt = Int32.Parse(districtID);
			var district = db.Districts.SingleOrDefault(p=>p.DistrictID == districtIDInt);
			return Json(new { success = true, result = district.DistrictName }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult GetProvinceForRoom(int motelID)
		{
			var x = db.MotelRooms.SingleOrDefault(p => p.MotelID == motelID).ProvinceID;
			string result = string.Empty;
			var listProvince = db.Provinces.ToList();
			foreach (Province p in listProvince)
			{
				if(x!=null && x== p.ProvinceID)
				{
					result += "<option selected value='" + p.ProvinceID + "'>" + p.Kind + " " + p.ProvinceName + "</option>" + Environment.NewLine;
				}
				else
				{
					result += "<option value='" + p.ProvinceID + "'>" + p.Kind + " " + p.ProvinceName + "</option>" + Environment.NewLine;
				}
			}
			return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
		}
		[HttpPost]
		public ActionResult GetDistrictForRoom(int motelID, int provinceID)
		{
			List<District> LoadDistrict = (from m in db.Districts
										   where m.ProvinceID == provinceID
										   select m).OrderBy(t => t.DistrictName).ToList();
			string result = string.Empty;
			if (LoadDistrict.Count == 0)
			{
				result = "<option value=0>Chọn Quận/Huyện</option>";
				return Json(new { success = true, result });
			}
			else
			{
				var districtID = db.MotelRooms.SingleOrDefault(p=>p.MotelID == motelID).DistrictID;
				foreach (var items in LoadDistrict)
				{
					if (districtID != null && districtID == items.DistrictID)
					{
						result += "<option selected value='" + items.DistrictID + "'> " + items.DistrictName + " </option>";
					}
					else
					{
						result += "<option value='" + items.DistrictID + "' > " + items.DistrictName + " </option>";
					}
				}
				return Json(new { success = true, result });
			}
		} 
		[HttpPost]
		public ActionResult GetWardForRoom(int motelID,int districtID)
		{
			var result = string.Empty;
			var x = db.MotelRooms.SingleOrDefault(p => p.MotelID == motelID).WardID;
			var listWard = db.Wards.Where(p => p.DistrictID == districtID).ToList();
			foreach (Ward w in listWard)
			{
				if(x!=null && x == w.WardID)
				{
					result += "<option selected value='" + w.WardID + "'>" + w.WardName + "</option>";
				}
				else
				{
					result += "<option value='" + w.WardID + "'>" + w.WardName + "</option>";
				}
			}
			return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
		}
	}
}