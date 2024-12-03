using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KhoaLuan.Models;

namespace KhoaLuan.ViewModels
{
	public class SearchResult
	{
		#region Properties
		public int PostID { get; set; }
		public string PostTitle { get; set; }
		public int? PostView { get; set; }
		public DateTime? PostDate { get; set; }
		public string Description { get; set; }
		public int? Acreage { get; set; }
		public long? Price { get; set; }
		public int? ProvinceID { get; set; }
		public int? DistrictID { get; set; }
		public int? WardID { get; set; }
		public int? MaxPeople { get; set; }
		public string Note { get; set; }
		public string Address { get; set; }
		public string Name { get; set; }
		public string ProvinceName { get; set; }
		public string DistrictName { get; set; }
		public string WardName { get; set; }
		public int? MotelID { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public DateTime? Birthday { get; set; }
		public bool? Sex { get; set; }
		public bool? PostStatus { get; set; }
		public int? StatusID { get; set; }
		public string AccountID { get; set; }
		public double? Longitude { get; set; }
		public double? Latitude { get; set; }
		public int? AccountStatus { get; set; }

		public List<int> ListCriteriaID { get; set; }
		#endregion

		#region Constructor
		public SearchResult()
		{

		}
		#endregion

		public static SearchResult CreateSearchResult(int postID)
		{
			var acc = new Account();
			var post = new Post();
			var motel = new MotelRoom();
			var info = new Info();
			var province = new Province();
			var district = new District();
			var ward = new Ward();
			List<int> listCriteria = new List<int>();

			using (var db = new QLTroEntities())
			{
				post = db.Posts.SingleOrDefault(p => p.PostID == postID);
				motel = db.MotelRooms.SingleOrDefault(p => p.MotelID == post.MotelID);
				info = db.Infoes.SingleOrDefault(p => p.AccountID.Equals(motel.AccountID));
				acc = db.Accounts.SingleOrDefault(p => p.AccountID.Equals(motel.AccountID));
				province = db.Provinces.SingleOrDefault(p => p.ProvinceID == motel.ProvinceID);
				district = db.Districts.SingleOrDefault(p => p.DistrictID == motel.DistrictID);
				ward = db.Wards.SingleOrDefault(p => p.WardID == motel.WardID);
				var list = db.MotelRooms.SingleOrDefault(p=>p.MotelID == motel.MotelID).Criteria.ToList();
				foreach(var item in list)
				{
					listCriteria.Add(item.CriteriaID);
				}
			}
			var searchResult = new SearchResult()
			{
				AccountID = motel.AccountID,
				AccountStatus = acc.AccountStatusID,
				PostID = post.PostID,
				PostTitle = post.PostTitle,
				PostDate = post.PostDate,
				PostView = post.PostView,
				MotelID = post.MotelID,
				Description = post.Description,
				PostStatus = post.PostStatus,

				Acreage = motel.Acreage,
				Price = motel.Price,
				ProvinceID = motel.ProvinceID,
				DistrictID = motel.DistrictID,
				WardID = motel.WardID,
				StatusID = motel.StatusID,
				Address = motel.Address,
				MaxPeople = motel.MaxPeople,
				ListCriteriaID = listCriteria,

				Name = info.Name,
				Sex = info.Sex,
				Birthday = info.Birthday,
				Phone = info.Phone,
				Email = info.Email,

				ProvinceName = province.ProvinceName,
				DistrictName = district.DistrictName,
				WardName = ward.WardName,
				Latitude = ward.Latitude,
				Longitude = ward.Longitude
			};
			return searchResult;
		}

		public static List<SearchResult> CreateListSearchResult()
		{
			var listPost = new List<Post>();
			var listSearchResult = new List<SearchResult>();
			using (var db = new QLTroEntities())
			{
				listPost = db.Posts.ToList();
			}
			foreach(var post in listPost)
			{
				listSearchResult.Add(CreateSearchResult(post.PostID));
			}
			return listSearchResult;
		}
	}
}