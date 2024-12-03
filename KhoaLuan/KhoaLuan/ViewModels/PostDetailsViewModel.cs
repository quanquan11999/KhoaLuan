using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KhoaLuan.Bo;
using KhoaLuan.Models;

namespace KhoaLuan.ViewModels
{
	public class PostDetailsViewModel
	{
		public SearchResult Post { get; set; }
		public List<string> ImageList { get; set; }
		public List<SearchResult> ViewedPost { get; set; }
		public List<SearchResult> SuggestPost { get; set; }

		public static SearchResult CreatePost(int postID)
		{
			SearchResult searchResult = new SearchResult();
			using (var db = new QLTroEntities())
			{
				searchResult = SearchResult.CreateSearchResult(postID);
				if (searchResult != null)
				{
					
				}
			}
			return null;
		}
	}
}