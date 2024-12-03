using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoaLuan.ViewModels
{
	public class PostManagerModel
	{
		public SearchResult Post { get; set; }
		public List<string> ImageList { get; set; }
		public int? motelID { get; set; }
	}
}