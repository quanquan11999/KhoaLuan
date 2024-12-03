using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KhoaLuan.Helpers;
using KhoaLuan.Models;
using KhoaLuan.ViewModels;

namespace KhoaLuan.Controllers
{
	public class AccountController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
		public ActionResult SignUp()
		{
			var listProvince = new List<Province>();
			using (var db = new QLTroEntities())
			{
				listProvince = db.Provinces.OrderBy(c => c.ProvinceName).ToList();
			}
			return View(listProvince);
		}

		[HttpPost]
		public ActionResult SignUp(FormCollection data)
		{
			var accountId = data["accountId"];
			var password = data["password"];
			var userName = data["userName"];
			DateTime birthDate = DateTime.Parse(data["birthDate"]);
			var address = data["address"];
			var phone = data["phone"];
			var email = data["email"];
			var city = data["city"];
			var district = data["district"];
			bool sex = true;
			if (data["sex"].Equals("1"))
			{
				sex = true;
			}
			else
			{
				sex = false;
			}
			int role = 0;
			if (data["role"].Equals("1"))
			{
				role = 1;
			}
			else
			{
				role = 2;
			}
			using (var db = new QLTroEntities())
			{
				db.Accounts.Add(new Account() { AccountID = accountId, AccountStatusID = 1, Password = password, Role = role });
				db.Infoes.Add(new Info() { AccountID = accountId, Name = userName, Sex = sex, Birthday = birthDate, Phone = phone, Email = email, ProvinceID = Int32.Parse(city), DistrictID = Int32.Parse(district) });
				db.SaveChanges();
			}
			return RedirectToAction("Index", "Home");
		}
		[HttpPost]
		public ActionResult Login(string userInput, string passwordInput)
		{
			//var StatusLogin = false;
			Account account = new Account();
			using (var db = new QLTroEntities())
			{
				account = db.Accounts.SingleOrDefault(p => p.AccountID.ToLower().Equals(userInput.ToLower()) && p.Password.Equals(passwordInput));
				if (account != null && account.AccountStatusID == 1)
				{
					account.Password = "";
					Session["account"] = account;
					var address = db.Infoes.SingleOrDefault(p => p.AccountID == account.AccountID);
					var districtName = db.Districts.SingleOrDefault(p => p.DistrictID == address.DistrictID).DistrictName;
					Session["address"] = address;
					Session["districtName"] = districtName;
				}
			}
			if (account.Role == 0)
			{
				return RedirectToAction("AllAccount", controllerName: "Admin");
			}
			else if (account.Role == 1)
			{
				return RedirectToAction("AllPost", "User");
			}
			else
			{
				return RedirectToAction("ChangeInfo", "Account");
			}
		}
		[HttpGet]
		public ActionResult Logout()
		{
			Session.Remove("account");
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public ActionResult Add(FormCollection data)
		{
			var account = new Account();
			account.AccountID = data["accountId"];
			account.Password = data["password"];
			account.Info = new Info();
			account.Info.Name = data["userName"];
			account.Info.Birthday = DateTime.Parse(data["birthDate"]);
			//account.HouseForRents
			return null;
		}

		[HttpGet]
		public ActionResult ChangePassWord()
		{
			if (Session["account"] == null)
			{
				return RedirectToAction("Index", "Home");
			}
			return View();
		}

		[HttpGet]
		public ActionResult ChangeInfo()
		{
			Account account = new Account();
			if (Session["account"] == null)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				account = Session["account"] as Account;
				ChangeInfoModel changeInfoModel = new ChangeInfoModel();
				using (var db = new QLTroEntities())
				{
					var info = db.Infoes.SingleOrDefault(p => p.AccountID.Equals(account.AccountID));
					changeInfoModel.InfoAccount = info;
					changeInfoModel.ProvinceAccount = db.Provinces.SingleOrDefault(p => p.ProvinceID == info.ProvinceID);
					changeInfoModel.DistrictAccount = db.Districts.SingleOrDefault(p => p.DistrictID == info.DistrictID);
				}
				return View(changeInfoModel);
			}
		}

		#region controller for ajax
		[HttpPost]
		public ActionResult CheckAccount(string userInput, string passwordInput)
		{
			using (var db = new QLTroEntities())
			{
				Account account = db.Accounts.SingleOrDefault(p => p.AccountID.Equals(userInput) && p.Password.Equals(passwordInput));
				if (account == null)
				{
					return Json(new { success = true, result = false, locked = false });
				}
				else
				{
					if (account.AccountStatusID == 2)
					{
						return Json(new { success = true, result = true, locked = true });
					}
					else
					{
						return Json(new { success = true, result = true, locked = false });
					}
				}
			}
		}
		[HttpPost]
		public ActionResult ChangeInfo(FormCollection data)
		{
			var accountID = data["accountID"];
			var name = data["nameUser"];
			var birthday = DateTime.Parse(data["birthday"]);
			var phone = data["phone"];
			var sex = Int32.Parse(data["sex"]);
			var province = Int32.Parse(data["province"]);
			var district = Int32.Parse(data["district"]);
			using (var db = new QLTroEntities())
			{
				Info accInfo = db.Infoes.SingleOrDefault(p => p.AccountID.Equals(accountID));
				if (accInfo != null)
				{
					accInfo.Name = name;
					accInfo.Birthday = birthday;
					accInfo.Phone = phone;
					if (sex == 1)
					{
						accInfo.Sex = true;
					}
					else
					{
						accInfo.Sex = false;
					}
					accInfo.ProvinceID = province;
					accInfo.DistrictID = district;
					db.SaveChanges();
					return Json(new { success = true }, JsonRequestBehavior.AllowGet);
				}
			}
			return Json(new { success = false }, JsonRequestBehavior.AllowGet);
		}
		[HttpPost]
		public ActionResult ChangePassWord(FormCollection data)
		{
			Account account = null;
			var acountID = data["accountID"];
			var oldPassword = data["currentPassword"];
			var newpassword = data["newPassword"];
			using (var db = new QLTroEntities())
			{
				account = db.Accounts.Single(p => p.AccountID.Equals(acountID) && p.Password.Equals(oldPassword));
				account.Password = newpassword;
				db.SaveChanges();
			}
			account.Password = "";
			Session["account"] = account;
			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}
		[HttpPost]
		public ActionResult CheckExist(string accountId)
		{
			using (var db = new QLTroEntities())
			{
				var account = db.Accounts.FirstOrDefault(p => p.AccountID.Equals(accountId));
				if (account == null)
				{
					return Json(new { success = true, result = false });
				}
				else
				{
					return Json(new { success = true, result = true });
				}
			}
		}
		[HttpPost]
		public ActionResult CheckCurrentPassword(string accountID, string currentPassword)
		{
			using (var db = new QLTroEntities())
			{
				Account account = db.Accounts.SingleOrDefault(p => p.AccountID.Equals(accountID) && p.Password.Equals(currentPassword));
				if (account != null)
				{
					return Json(new { success = true, result = true }, JsonRequestBehavior.AllowGet);
				}
				else
				{
					return Json(new { success = false, result = false }, JsonRequestBehavior.AllowGet);
				}
			}
		}

		[HttpPost]
		public ActionResult CheckEmailExist(string email)
		{
			using (var db = new QLTroEntities())
			{
				var emailConfirm = db.ConfirmEmails.SingleOrDefault(p => p.Email.Equals(email));
				if (emailConfirm != null)
				{
					return Json(new { success = true, exist = true }, JsonRequestBehavior.AllowGet);
				}
				else
				{
					return Json(new { success = true, exist = false }, JsonRequestBehavior.AllowGet);
				}
			}
		}
		[HttpPost]
		public ActionResult EmailConfirm(string email)
		{
			
			using (var db = new QLTroEntities())
			{
				// Create Verifition Code
				var verfiticationCode = Helpers.RandomHelper.RandomVerificationCode().ToString();
				ConfirmEmail emC = new ConfirmEmail()
				{
					Email = email,
					Time = DateTime.Now.TimeOfDay,
					VerificationCode = verfiticationCode
				};
				db.ConfirmEmails.Add(emC);
				db.SaveChanges();

				string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/Mail/templateEmail.html"));
				content = content.Replace("{{content}}", "Mã xác minh của bạn là: " + verfiticationCode + "<br><br>Mã xác minh có hiệu lực trong vòng 15 phút!");
				MailHelper.sendMail(email, "Xác minh tài khoản Email", content);
				return Json(new { success = true, exist = true }, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpPost]
		public ActionResult CheckVerfiticationCode(string email, string code)
		{
			var timeCurrent = DateTime.Now.TimeOfDay;
			using (var db = new QLTroEntities())
			{
				var ec = db.ConfirmEmails.SingleOrDefault(p => p.Email.Equals(email));
				var t = timeCurrent.TotalSeconds - ec.Time.Value.TotalSeconds;
				var verfiticationCode = ec.VerificationCode;
				if (t > 900)
				{
					return Json(new { success = false }, JsonRequestBehavior.AllowGet);
				}
				else
				{
					if (verfiticationCode.Equals(code))
					{
						return Json(new { success = true }, JsonRequestBehavior.AllowGet);
					}
					else
					{
						return Json(new { success = false }, JsonRequestBehavior.AllowGet);
					}
				}
			}
		}

		[HttpPost]
		public ActionResult ResetPassword(string accountID)
		{
			var newpass = RandomHelper.RandomNewPassword();
			var email = string.Empty;
			using (var db = new QLTroEntities())
			{
				var account = db.Accounts.Single(p => p.AccountID.Equals(accountID));
				var accountInfo = db.Infoes.Single(p => p.AccountID.Equals(accountID));
				email = accountInfo.Email;
				account.Password = newpass;
				db.SaveChanges();
				string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/Mail/templateEmail.html"));
				content = content.Replace("{{content}}", "Mật khẩu mới của bạn là: " + newpass);
				MailHelper.sendMail(email, "Lấy lại mật khẩu", content);
			}
			return Json(new { success = true, result = email }, JsonRequestBehavior.AllowGet);
		}
		#endregion
	}
}