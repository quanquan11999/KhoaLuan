using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KhoaLuan.Models;
using KhoaLuan.ViewModels;

namespace KhoaLuan.Controllers
{
	public class HomeController : Controller
	{
		QLTroEntities db = new QLTroEntities();
		public ActionResult Index()
		{
			HomeModel homeModel = new HomeModel();
			return View(homeModel.CreateHomeModel());
		}
	}
}