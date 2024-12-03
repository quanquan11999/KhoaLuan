using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KhoaLuan.Bo;
using KhoaLuan.Models;

namespace KhoaLuan.ViewModels
{
	public class DiscoverViewModel
	{
		public List<Province> ListProvince { get; set; }
		public List<PostViewModel> ListNewPosts { get; set; }
		public List<PostViewModel> ListPostsInMonth { get; set; }
		public List<PostViewModel> ListPostsCheapest { get; set; }
		public List<PostViewModel> ListMostView { get; set; }


		public DiscoverViewModel CreateDiscoverViewModel()
		{
			//load new post
			var q = from s in SearchResult.CreateListSearchResult()
					orderby s.PostDate descending
					select s;
			var listNewPosts = CreateListPostViewModel(q.ToList());

			//load most view
			q = from s in SearchResult.CreateListSearchResult()
				orderby s.PostView descending
				select s;
			var listMostView = CreateListPostViewModel(q.ToList());

			//load Cheapest
			q = from s in SearchResult.CreateListSearchResult()
				orderby s.Price ascending
				select s;
			var listPostsCheapest = CreateListPostViewModel(q.ToList());

			//load post in month
			var month = DateTime.Now.Month;
			var year = DateTime.Now.Year;
			var listResult = SearchResult.CreateListSearchResult().Where(p => p.PostDate.Value.Month == month && p.PostDate.Value.Year == year).OrderBy(p => p.PostDate).ToList();
			var listViewInMonth = CreateListPostViewModel(listResult).ToList();
			var db = new QLTroEntities();
			var discover = new DiscoverViewModel()
			{
				ListProvince = db.Provinces.ToList(),
				ListMostView = listMostView,
				ListNewPosts = listNewPosts,
				ListPostsCheapest = listPostsCheapest,
				ListPostsInMonth = listViewInMonth
			};
			return discover;
		}
		private List<PostViewModel> CreateListPostViewModel(List<SearchResult> listPosts)
		{
			List<PostViewModel> list = new List<PostViewModel>();
			using (var db = new QLTroEntities())
			{
				if (listPosts != null && listPosts.Count > 0)
				{
					foreach (var item in listPosts)
					{
						list.Add(new PostViewModel()
						{
							Post = item,
							ImageList = GetImage.getListImage(item.MotelID)
						});
					}
				}
			}
			return list;
		}
	}
}