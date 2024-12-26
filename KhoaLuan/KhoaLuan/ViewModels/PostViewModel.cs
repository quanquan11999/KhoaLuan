using KhoaLuan.Bo;
using KhoaLuan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoaLuan.ViewModels
{
    public class PostViewModel
    {
        public SearchResult Post { get; set; }
        public List<string> ImageList { get; set; }


        public static List<PostViewModel> CreateListPostViewModel(List<SearchResult> listPosts)
        {
            List<PostViewModel> list = new List<PostViewModel>();
            using (var db = new QLTroEntities())
            {
                var searchResult = SearchResult.CreateListSearchResult().Where(p => p.AccountStatus == 1 && p.PostStatus == true).ToList();
                if (searchResult != null && searchResult.Count > 0)
                {
                    foreach (var item in searchResult)
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

        public static List<PostViewModel> CreateListPostViewModel(int provinceID, int districtID, int maxPeople, string searchKey)
        {
            List<PostViewModel> list = new List<PostViewModel>();
            using (var db = new QLTroEntities())
            {
                var searchResult = SearchResult.CreateListSearchResult()
                    .Where(p => p.ProvinceID == provinceID && p.DistrictID == districtID
                    && p.AccountStatus == 1 && p.PostStatus == true
                    && (p.Name.Contains(searchKey) || p.PostTitle.Contains(searchKey) || p.DistrictName.Contains(searchKey) || 
                    p.Description.Contains(searchKey) || p.WardName.Contains(searchKey) || p.ProvinceName.Contains(searchKey))
                    )
                    .ToList();
                if (maxPeople > 0)
                {
                    searchResult = searchResult.Where(x => x.MaxPeople <= maxPeople).ToList();
                }
                if (searchResult != null && searchResult.Count > 0)
                {
                    foreach (var item in searchResult)
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

        public static List<PostViewModel> CreateListPostViewModel(int provinceID, int districtID, int maxPeople, List<int> lstIdPostWithCriteria, string searchKey)
        {
            List<PostViewModel> list = new List<PostViewModel>();
            using (var db = new QLTroEntities())
            {
                var searchResult = SearchResult.CreateListSearchResult()
                    .Where(p => p.ProvinceID == provinceID && p.DistrictID == districtID
                    && p.AccountStatus == 1 && p.PostStatus == true
                    && (p.Name.Contains(searchKey) ||  p.PostTitle.Contains(searchKey) || p.DistrictName.Contains(searchKey) ||
                    p.Description.Contains(searchKey) || p.WardName.Contains(searchKey) || p.ProvinceName.Contains(searchKey))
                    )
                    .ToList();
                if (maxPeople > 0)
                {
                    searchResult = searchResult.Where(x => x.MaxPeople <= maxPeople).ToList();
                }
                if (lstIdPostWithCriteria != null && lstIdPostWithCriteria.Count > 0)
                {
                    searchResult = searchResult.Where(x => lstIdPostWithCriteria.Contains((int)x.MotelID)).ToList();
                }
                else
                {
                    searchResult.Clear();
                }
                if (searchResult != null && searchResult.Count > 0)
                {
                    foreach (var item in searchResult)
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