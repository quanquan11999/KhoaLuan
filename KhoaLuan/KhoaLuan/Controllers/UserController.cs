using KhoaLuan.Bo;
using KhoaLuan.Helpers;
using KhoaLuan.Models;
using KhoaLuan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KhoaLuan.Controllers
{
	public class UserController : Controller
	{
		private QLTroEntities db = new QLTroEntities();
		private readonly int PageSize = 8;
		// GET: User
		public ActionResult AllPost(string pageNumber)
		{
			Account account = new Account();
			if (Session["account"] == null)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				account = Session["account"] as Account;
				if (account.Role == 0)
				{
					return RedirectToAction("AllPost", "Admin");
				}
				else if (account.Role == 2)
				{
					return RedirectToAction("ChangeInfo", "Account");
				}
			}

			int pageN = 1;
			if (pageNumber != null)
			{
				pageN = Int32.Parse(pageNumber);
			}
			List<PostManagerModel> list = new List<PostManagerModel>();
			var listSearchResult = SearchResult.CreateListSearchResult().Where(p => p.AccountID == account.AccountID).ToList();
			var postList = listSearchResult.Skip((pageN - 1) * PageSize).Take(PageSize).ToList();
			if (postList != null && postList.Count() > 0)
			{
				foreach (var item in postList)
				{
					list.Add(new PostManagerModel()
					{
						Post = item,
						ImageList = GetImage.getListImage(item.MotelID),
						motelID = item.MotelID
					});
				}
			}
			var subTotalPage = listSearchResult.Count / PageSize;
			var totalPage = (listSearchResult.Count % PageSize == 0) ? subTotalPage : subTotalPage + 1;
			ViewBag.PageNumber = pageN;
			ViewBag.TotalPage = totalPage;
			return View(list);
		}

		public ActionResult MotelRooms(string pageNumber)
		{
			var account = new Account();
			if (Session["account"] == null)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				account = Session["account"] as Account;
				if (account.Role == 0)
				{
					return RedirectToAction("AllPost", "Admin");
				}
				else if (account.Role == 2)
				{
					return RedirectToAction("ChangeInfo", "Account");
				}
			}
			account = Session["account"] as Account;
			int pageN = 1;
			if (pageNumber != null)
			{
				pageN = Int32.Parse(pageNumber);
			}
			List<RoomModel> listRoom = new List<RoomModel>();
			var listR = db.MotelRooms.Where(p => p.AccountID == account.AccountID).ToList();
			var list = listR.Skip((pageN - 1) * PageSize).Take(PageSize).ToList();
			foreach (var item in list)
			{
				RoomModel r = new RoomModel()
				{
					Motel = item,
					provinceName = db.Provinces.SingleOrDefault(p => p.ProvinceID == item.ProvinceID).ProvinceName,
					DistrictName = db.Districts.SingleOrDefault(p => p.DistrictID == item.DistrictID).DistrictName,
					WardName = db.Wards.SingleOrDefault(p => p.WardID == item.WardID).WardName
				};
				listRoom.Add(r);
			}
			var subTotalPage = db.MotelRooms.Count() / PageSize;
			var totalPage = (db.MotelRooms.Count() % PageSize == 0) ? subTotalPage : subTotalPage + 1;
			ViewBag.PageNumber = pageN;
			ViewBag.TotalPage = totalPage;
			return View(listRoom);
		}

		public ActionResult RoomDetails(string motelID)
		{
			var account = new Account();
			if (Session["account"] == null)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				account = Session["account"] as Account;
				if (account.Role == 0)
				{
					return RedirectToAction("AllPost", "Admin");
				}
				else if (account.Role == 2)
				{
					return RedirectToAction("ChangeInfo", "Account");
				}
			}
			var ID = 0;
			if (motelID != null)
			{
				ID = int.Parse(motelID);
			}
			var billExist = db.Bills.SingleOrDefault(p => p.MotelID == ID);
			if (billExist != null)
			{
				var dateCurrent = DateTime.Now;
				var listBillDetails = db.BillDetails.Where(p => p.BillID == billExist.BillID).ToList();
				var n = listBillDetails.Count();
				var toDayFinal = listBillDetails[n - 1].ToDay;
				var period = (dateCurrent- toDayFinal).Value.Days;
				if (period > 0)
				{
					var buildDetails = new BillDetail()
					{
						BillID = billExist.BillID,
						BillStatus = false,
						FromDay = toDayFinal.Value.AddDays(1),
						ToDay = toDayFinal.Value.AddDays(31),
						RoomRates = db.MotelRooms.SingleOrDefault(p=>p.MotelID == ID).Price
					};
					db.BillDetails.Add(buildDetails);
					db.SaveChanges();
				}
			}
			var x = db.MotelRooms.SingleOrDefault(p => p.MotelID == ID && p.AccountID == account.AccountID);
			ViewBag.totalPeople = db.Renters.Where(p => p.MotelID == ID).Count();
			ViewBag.maxPeople = db.MotelRooms.SingleOrDefault(p => p.MotelID == ID).MaxPeople;
			if (x != null)
			{
				ViewBag.motelID = ID;
				ViewBag.motelStatus = x.StatusID;
				account = Session["account"] as Account;
				var listRenter = db.Renters.Where(p => p.MotelID == ID).ToList();
				var bill = db.Bills.SingleOrDefault(p => p.MotelID == ID);
				var room = db.MotelRooms.SingleOrDefault(p => p.MotelID == ID);
				var listBillDetails = new List<BillDetail>();
				if (bill != null)
				{
					listBillDetails = db.BillDetails.Where(p => p.BillID == bill.BillID).ToList();
				}
				RoomDetailsModel roomDetailsModel = new RoomDetailsModel()
				{
					Bill = bill,
					ListRenter = listRenter,
					MotelRoom = room,
					ListBillDetail = listBillDetails
				};
				return View(roomDetailsModel);
			}
			else
			{
				return RedirectToAction("MotelRooms", "User");
			}

		}
		[HttpPost]
		public ActionResult RoomRegistration(FormCollection data)
		{
			var moID = int.Parse(data["motelID"]);
			var id = int.Parse(data["CMND"]);
			var name = data["renterName"];
			var temp = int.Parse(data["renterSex"]);
			var sex = false;
			if (temp == 1)
			{
				sex = true;
			}
			var birthday = DateTime.Parse(data["renterBirthDay"]);
			var phone = data["renterPhoneNumber"];
			var rentDate = DateTime.Parse(data["rentDate"]);
			var note = data["rentNote"];
			var r = db.MotelRooms.SingleOrDefault(p => p.MotelID == moID);
			r.StatusID = 2	;
			db.SaveChanges();
			var renter = new Renter()
			{
				ID = id,
				Birthday = birthday,
				MotelID = moID,
				Name = name,
				Note = note,
				Phone = phone,
				Sex = sex
			};
			var build = new Bill()
			{
				MotelID = r.MotelID,
				RentDay = rentDate,
				RentID = renter.RentID
			};
			db.Renters.Add(renter);
			db.Bills.Add(build);
			db.SaveChanges();
			var buildDetails = new BillDetail()
			{
				BillID = db.Bills.SingleOrDefault(p => p.MotelID == moID).BillID,
				BillStatus = false,
				FromDay = rentDate,
				ToDay = rentDate.AddDays(30),
				RoomRates = db.MotelRooms.SingleOrDefault(p => p.MotelID == moID).Price
			};
			db.BillDetails.Add(buildDetails);
			db.SaveChanges();
			return RedirectToAction("RoomDetails", "User", new { motelID = moID });
		}   
		[HttpPost]
		public ActionResult AddRentalPeriod(FormCollection data)
		{
			var motelID = int.Parse(data["motelID"]);
			var fromDayAdd = DateTime.Parse(data["fromDayAdd"]);
			var toDayAdd = DateTime.Parse(data["toDayAdd"]);
			var buildDetails = new BillDetail()
			{
				BillID = db.Bills.SingleOrDefault(p=>p.MotelID == motelID).BillID,
				BillStatus = false,
				FromDay = fromDayAdd,
				ToDay = toDayAdd,
				RoomRates = db.MotelRooms.SingleOrDefault(p => p.MotelID == motelID).Price
			};
			db.BillDetails.Add(buildDetails);
			db.SaveChanges();
			return RedirectToAction("RoomDetails", "User", new { motelID = motelID });
		}
		[HttpPost]
		public ActionResult CheckOut(int motelID)
		{
			var i = 0;
			var bill = db.Bills.SingleOrDefault(p => p.MotelID == motelID);
			var listBillDetails = db.BillDetails.Where(p => p.BillID == bill.BillID).ToList();
			foreach(var item in listBillDetails)
			{
				if(item.BillStatus == false)
				{
					i++;
				}
			}
			if (i == 0)
			{
				var listRent = db.Renters.Where(p => p.MotelID == motelID).ToList();
				var room = db.MotelRooms.SingleOrDefault(p => p.MotelID == motelID);
				room.StatusID = 1;
				db.BillDetails.RemoveRange(listBillDetails);
				db.Bills.Remove(bill);
				db.Renters.RemoveRange(listRent);
				db.SaveChanges();
				return Json(new { success = true }, JsonRequestBehavior.AllowGet);
			}
			return Json(new { success = false }, JsonRequestBehavior.AllowGet);
		}  
		[HttpPost]
		public ActionResult PayRoom(FormCollection data)
		{
			var motelID = int.Parse(data["motelID"]);
			var billDetailsID = int.Parse(data["billDetailsID"]);
			var electricityPrice = int.Parse(data["electricityPrice"]);
			var waterPrice = int.Parse(data["waterPrice"]);
			var internetPrice = int.Parse(data["internetPrice"]);
			var billDetail = db.BillDetails.SingleOrDefault(p => p.BillDetailsID == billDetailsID);
			billDetail.BillStatus = true;
			billDetail.ElectricityPrice = electricityPrice;
			billDetail.InternetPrice = internetPrice;
			billDetail.WaterPrice = waterPrice;
			db.SaveChanges();
			return RedirectToAction("RoomDetails", "User", new { motelID = motelID });
		}
		[HttpPost]
		public ActionResult AddRenter(FormCollection data)
		{
			var moID = int.Parse(data["motelID"]);
			var id = int.Parse(data["CMND"]);
			var name = data["renterName"];
			var temp = int.Parse(data["renterSex"]);
			var sex = false;
			if (temp == 1)
			{
				sex = true;
			}
			var birthday = DateTime.Parse(data["renterBirthDay"]);
			var phone = data["renterPhoneNumber"];
			var note = data["rentNote"];
			var x = db.Renters.Where(p => p.MotelID == moID).Count();
			var max = db.MotelRooms.SingleOrDefault(p => p.MotelID == moID).MaxPeople;
			if (x < max)
			{
				var r = db.MotelRooms.SingleOrDefault(p => p.MotelID == moID);
				r.StatusID = 2;
				db.SaveChanges();
				var renter = new Renter()
				{
					ID = id,
					Birthday = birthday,
					MotelID = moID,
					Name = name,
					Note = note,
					Phone = phone,
					Sex = sex
				};
				db.Renters.Add(renter);
				db.SaveChanges();
			}
			return RedirectToAction("RoomDetails", "User", new { motelID = moID });
		}
		[HttpPost]
		public ActionResult EditRenter(FormCollection data)
		{
			var moID = int.Parse(data["motelID"]);
			var rentID = int.Parse(data["rentID"]);
			var id = int.Parse(data["EditCMND"]);
			var name = data["renterEditName"];
			var temp = int.Parse(data["renterEditSex"]);
			var sex = false;
			if (temp == 1)
			{
				sex = true;
			}
			var birthday = DateTime.Parse(data["renterEditBirthDay"]);
			var phone = data["renterEditPhoneNumber"];
			var note = data["rentEditNote"];
			var renter = db.Renters.SingleOrDefault(p => p.RentID == rentID);
			renter.ID = id;
			renter.Name = name;
			renter.Sex = sex;
			renter.Birthday = birthday;
			renter.Phone = phone;
			renter.Note = note;
			db.SaveChanges();
			return RedirectToAction("RoomDetails", "User", new { motelID = moID });
		}
		[HttpPost]
		public ActionResult DelRenter(int rentID, int moID)
		{
			var rentIDInBill = db.Bills.SingleOrDefault(p => p.MotelID == moID).RentID;
			if(rentID != rentIDInBill)
			{
				var renter = db.Renters.SingleOrDefault(p => p.RentID == rentID);
				db.Renters.Remove(renter);
				db.SaveChanges();
				return Json(new { success = true }, JsonRequestBehavior.AllowGet);
			}
			//var x = db.Renters.Where(p => p.MotelID == moID).Count();
			//if (x == 0)
			//{
			//	var r = db.MotelRooms.SingleOrDefault(p => p.MotelID == moID);
			//	r.StatusID = 1;
			//}
			//db.SaveChanges();
			return Json(new { success = false }, JsonRequestBehavior.AllowGet);
		}
		[HttpGet]
		public ActionResult AddRoom()
		{
			if (Session["account"] == null)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				Account account = Session["account"] as Account;
				if (account.Role == 0)
				{
					return RedirectToAction("AllPost", "Admin");
				}
				else if (account.Role == 2)
				{
					return RedirectToAction("ChangeInfo", "Account");
				}
			}
			var cityList = db.Provinces.OrderBy(c => c.ProvinceName).ToList();
			var uploadViewModel = new UploadViewModel()
			{
				ListProvince = cityList,
				ListCriterion = db.Criteria.ToList()
			};
			return View(uploadViewModel);
		}
		[HttpPost]
		public ActionResult AddRoom(FormCollection data)
		{
			List<Criterion> list = db.Criteria.ToList();
			List<Criterion> listCriteria = new List<Criterion>();
			foreach (var item in list)
			{
				var x = "criteria" + item.CriteriaID;
				if (data[x] != null)
				{
					listCriteria.Add(item);
				}
				var t = data[x];
			}
			int? acreage = Convert.ToInt32(data["acreage"]);
			int? provinceID = Convert.ToInt32(data["province"]);
			int? districtID = Convert.ToInt32(data["district"]);
			int? maxPeople = Convert.ToInt32(data["maxPeople"]);
			int? wardID = Convert.ToInt32(data["ward"]);
			var motelName = data["motelName"];
			MotelRoom house = new MotelRoom();
			house.MotelName = motelName;
			house.Acreage = acreage;
			house.Price = CurrencyHelper.CurrencyToNumber(data["price"], '.');
			house.ProvinceID = provinceID;
			house.DistrictID = districtID;
			house.MaxPeople = maxPeople;
			house.StatusID = 1;
			house.AccountID = (Session["account"] as Account).AccountID;
			house.Address = (data["addressRoom"]).ToString();
			house.Criteria = listCriteria;
			house.WardID = wardID;
			db.MotelRooms.Add(house);
			db.SaveChanges();
			return RedirectToAction("AddRoom", "User");
		}
		[HttpPost]
		public ActionResult DelRoom(int motelID)
		{
			try
			{
				var x = db.Renters.Where(p => p.MotelID == motelID).Count();
				if (x == 0)
				{
					var room = db.MotelRooms.SingleOrDefault(p => p.MotelID == motelID);
					db.MotelRooms.Remove(room);
					db.SaveChanges();
					return Json(new { success = true }, JsonRequestBehavior.AllowGet);
				}
				else
				{
					return Json(new { success = false }, JsonRequestBehavior.AllowGet);
				}
			}
			catch (Exception e)
			{
				return Json(new { success = false }, JsonRequestBehavior.AllowGet);
			}
		}
		[HttpPost]
		public ActionResult EditRoom(FormCollection data)
		{
			var motelIDEidt = int.Parse(data["editMotelID"]);
			var acreageEidt = int.Parse(data["acreageEidt"]);
			var priceEdit = int.Parse(data["priceEdit"]);
			var maxPeopleEdit = int.Parse(data["maxPeopleEdit"]);
			var addressEdit = data["addressEdit"];
			List<Criterion> list = db.Criteria.ToList();
			List<Criterion> listCriteria = new List<Criterion>();
			foreach (var item in list)
			{
				var x = "criteria" + item.CriteriaID;
				if (data[x] != null)
				{
					listCriteria.Add(item);
				}
				var t = data[x];
			}
			var wardEdit = int.Parse(data["wardEdit"]);
			var districtEdit = int.Parse(data["districtEdit"]);
			var provinceEdit = int.Parse(data["provinceEdit"]);
			var room = db.MotelRooms.SingleOrDefault(p => p.MotelID == motelIDEidt);
			var currentList = room.Criteria.ToList();
			foreach (var item in currentList)
			{
				room.Criteria.Remove(item);
			}
			db.SaveChanges();
			room.Acreage = acreageEidt;
			room.Price = priceEdit;
			room.MaxPeople = maxPeopleEdit;
			room.Address = addressEdit;
			room.Criteria = listCriteria;
			room.ProvinceID = provinceEdit;
			room.DistrictID = districtEdit;
			room.WardID = wardEdit;
			db.SaveChanges();

			return RedirectToAction("MotelRooms", "User");
		}

		[HttpGet]
		public ActionResult UploadPost()
		{
			Account account = null;
			if (Session["account"] == null)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				account = Session["account"] as Account;
				if (account.Role == 0)
				{
					return RedirectToAction("AllPost", "Admin");
				}
				else if (account.Role == 2)
				{
					return RedirectToAction("ChangeInfo", "Account");
				}
			}
			//select roomId tu post cua account
			var list = db.Posts.ToList();
			List<int> list1 = new List<int>();
			foreach (var item in list)
			{
				int m = int.Parse(item.MotelID.ToString());
				list1.Add(m);
			}
			//select roomId tu motelroom cua account
			var list2 = new List<int>();
			var listRoom = db.MotelRooms.Where(p => p.AccountID == account.AccountID).ToList();
			foreach (var item in listRoom)
			{
				list2.Add(item.MotelID);
			}
			IEnumerable<int> differenceQuery = list2.Except(list1);
			var listResult = new List<MotelRoom>();
			foreach (var item in differenceQuery)
			{
				listResult.Add(db.MotelRooms.SingleOrDefault(p => p.MotelID == item));
			}
			return View(listResult);
		}

		[HttpPost]
		public ActionResult UploadPost(FormCollection data)
		{
			var motelID = int.Parse(data["selectMotelID"]);
			// Tai hinh anh len
			var urlList = UploadFile(Request.Files);
			if (urlList.Count > 0)
			{
				foreach (var url in urlList)
				{
					// Luu hinh anh
					SaveImage(url, motelID);
				}
				var post = new Post()
				{
					MotelID = motelID,
					PostTitle = data["postTitle"],
					Description = data["description"],
					PostDate = DateTime.Now,
					PostView = 0,
					PostStatus = false
				};
				db.Posts.Add(post);
				db.SaveChanges();
			}
			return RedirectToAction("UploadPost", "User");
		}
		[HttpPost]
		public ActionResult DelPost(string postID)
		{
			var pID = int.Parse(postID);
			var post = db.Posts.SingleOrDefault(p => p.PostID == pID);
			db.Posts.Remove(post);
			db.SaveChanges();
			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}
		private void SaveImage(string url, int motelID)
		{
			var image = new Image() { ImageID = 6, Url = url, MotelID = motelID };
			db.Images.Add(image);
			db.SaveChanges();
		}

		private List<string> UploadFile(HttpFileCollectionBase files)
		{
			var urlList = new List<string>();
			var maxImageId = 0;
			int totalImg = db.Images.ToList().Count;
			if (totalImg > 0)
			{
				maxImageId = db.Images.Max(i => i.ImageID);
			}
			try
			{
				for (int i = 0; i < files.Count; i++)
				{
					if (files[i].ContentLength > 0)
					{
						var fileName = System.IO.Path.GetFileNameWithoutExtension(files[i].FileName);
						var newFileName = files[i].FileName.Replace(fileName, (maxImageId + 1).ToString());
						var filePath = Server.MapPath("~/Content/Images/Houserent/" + newFileName);
						files[i].SaveAs(filePath);
						urlList.Add(newFileName);
						maxImageId++;
					}
				}
				return urlList;
			}
			catch (Exception) { return null; }
		}

		public ActionResult CheckID(int ID)
		{
			var renter = db.Renters.SingleOrDefault(p => p.ID == ID);
			if (renter == null)
			{
				return Json(new { success = true }, JsonRequestBehavior.AllowGet);
			}
			else
			{
				return Json(new { success = false }, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpPost]
		public ActionResult GetCriteriaForRoom(int motelID)
		{
			var listCriteria = db.Criteria.ToList();
			var list = db.MotelRooms.SingleOrDefault(p => p.MotelID == motelID).Criteria.ToList();
			var list2 = listCriteria.Except(list);
			string result = string.Empty;

			foreach (var item in list)
			{
				result += "<div class='col-sm-6'><div class='custom-control custom-checkbox'>" +
								"<input checked type='checkbox' class='custom-control-input' id='criteria" + item.CriteriaID + "' name='criteria" + item.CriteriaID + "'>" +
								"<label class='custom-control-label' for='criteria" + item.CriteriaID + "'>" + item.CriteriaName + "</label>" +
								"</div></div>";
			}
			foreach (var item in list2)
			{
				result += "<div class='col-sm-6'><div class='custom-control custom-checkbox'>" +
								"<input type='checkbox' class='custom-control-input' id='criteria" + item.CriteriaID + "' name='criteria" + item.CriteriaID + "'>" +
								"<label class='custom-control-label' for='criteria" + item.CriteriaID + "'>" + item.CriteriaName + "</label>" +
								"</div></div>";
			}
			return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult GetBillDetails(int billDetailsID)
		{
			var billDetail = db.BillDetails.SingleOrDefault(p => p.BillDetailsID == billDetailsID);
			if (billDetail != null)
			{
				var roomPrice = billDetail.RoomRates;
				var internetPrice = billDetail.InternetPrice;
				var electricityPrice = billDetail.ElectricityPrice;
				var waterPrice = billDetail.WaterPrice;
				var totalPrice = roomPrice + internetPrice + electricityPrice + waterPrice;
				return Json(new { success = true, roomPrice, internetPrice, electricityPrice, waterPrice, totalPrice }, JsonRequestBehavior.AllowGet);
			}
			return Json(new { success = false }, JsonRequestBehavior.AllowGet);
		}
		[HttpPost]
		public ActionResult GetRecentDays(int billID)
		{
			var listBillDetails = db.BillDetails.Where(p => p.BillID == billID).ToList();
			var n = listBillDetails.Count;
			if (n > 0)
			{
				var fromDay = listBillDetails[n - 1].ToDay.Value.AddDays(1);
				var day1 = fromDay.Day;
				var month1 = fromDay.Month;
				var year1 = fromDay.Year;
				var toDay = fromDay.AddDays(30);
				var day2 = toDay.Day;
				var month2 = toDay.Month;
				var year2 = toDay.Year;
				return Json(new { success = true, day1, month1, year1, day2, month2, year2 }, JsonRequestBehavior.AllowGet);
			}
			return Json(new { success = false }, JsonRequestBehavior.AllowGet);
		}
	}
}