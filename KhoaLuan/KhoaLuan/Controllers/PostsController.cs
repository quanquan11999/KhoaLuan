using KhoaLuan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KhoaLuan.Models;
using KhoaLuan.Bo;

namespace KhoaLuan.Controllers
{
    public class PostsController : Controller
    {
        QLTroEntities db = new QLTroEntities();
        private readonly int PageSize = 5;

        public ActionResult Discover()
        {
            DiscoverViewModel discoverViewModel = new DiscoverViewModel();
            return View(discoverViewModel.CreateDiscoverViewModel());
        }

        [HttpGet]
        public ActionResult SearchResult(string pageNumber, string order)
        {
            if (order != null)
            {
                return SearchResult(Session["searchData"] as FormCollection, pageNumber, order);
            }
            else
            {
                return SearchResult(Session["searchData"] as FormCollection, pageNumber, null);
            }
        }
        [HttpPost]
        public ActionResult SearchResult(FormCollection form, string pageNumber, string order)
        {
            var listCriteria = db.Criteria.ToList();
            List<PostViewModel> listPosts;
            var listResult = new List<PostViewModel>();
            var listSearchAdvange = new List<PostViewModel>();

            if (form == null) return View(listResult);
            var lastSearch = Session["searchform"] as FormCollection;

            int provinceID = Convert.ToInt32(form["province"]);
            if (provinceID == 0)
            {
                provinceID = Convert.ToInt32(lastSearch["province"]);
                form["province"] = lastSearch["province"];
            }
            int districtID = Convert.ToInt32(form["district"]);
            if (districtID == 0)
            {
                districtID = Convert.ToInt32(lastSearch["district"]);
                form["district"] = lastSearch["district"];
            }
            string searchKey = form["searchKey"] ?? "";
            Session["searchKey"] = searchKey;

            string isSearchAdvance = form["isSearchAdvance"];
            int maxPeople = Convert.ToInt32(form["maxPeople"]);

            Session["searchform"] = form;

            Response.Cookies["provinceId"].Value = provinceID.ToString();
            Response.Cookies["townshipId"].Value = districtID.ToString();

            if (provinceID == 0 || districtID == 0)
            {
                ViewBag.province = "Province";
                ViewBag.district = "District";
            }
            else
            {
                ViewBag.province = db.Provinces.Where(p => p.ProvinceID == provinceID).First().ProvinceName;
                ViewBag.district = db.Districts.Where(p => p.DistrictID == districtID).First().DistrictName;
            }
            // Get all post
            List<int> lstIdPostWithCriteria;
            if (!string.IsNullOrEmpty(form["criteria[]"]))
            {
                var criteriaList = new List<int>();
                criteriaList = form["criteria[]"].Split(',')
                                               .Select(int.Parse)
                                               .ToList();
                lstIdPostWithCriteria = db.MotelRooms
                .Where(x => criteriaList.All(cId => x.Criteria.Any(c => c.CriteriaID == cId)))
                .Select(x => x.MotelID)
                .Distinct()
                .ToList();
                Session["criteria"] = form["criteria[]"];
                listPosts = PostViewModel.CreateListPostViewModel(
                    provinceID, districtID, maxPeople, lstIdPostWithCriteria, searchKey);
            }
            else
            {
                listPosts = PostViewModel.CreateListPostViewModel(
                    provinceID, districtID, maxPeople, searchKey);
            }

            listResult = listPosts.Where(p => p.Post.PostStatus == true && p.Post.AccountStatus == 1).ToList();
            if (isSearchAdvance != null)
            {
                long price = long.Parse(form["price"]);
                long area = long.Parse(form["area"]);
                var listCriteriaID = new List<int>();
                listPosts = listResult.Where(p => p.Post.Price <= price && p.Post.Acreage <= area).ToList();
                listResult = listPosts;
                foreach (var item in listCriteria)
                {
                    if (form["criteria" + item.CriteriaID] != null)
                    {
                        var criID = Int32.Parse(form["criteria" + item.CriteriaID]);
                        listCriteriaID.Add(criID);
                    }
                }
                foreach (var item in listResult)
                {
                    var list = listCriteriaID.Except(item.Post.ListCriteriaID);
                    if (!list.Any())
                    {
                        listSearchAdvange.Add(item);
                    }
                }
                listResult = listSearchAdvange;
            }

            var subTotalPage = listResult.Count / PageSize;
            var totalPage = (listResult.Count % PageSize == 0) ? subTotalPage : subTotalPage + 1;

            var pageN = 1;
            if (!Int32.TryParse(pageNumber, out pageN)) pageN = 1;
            else if ((pageN < 1) || (pageN > totalPage)) pageN = 1;

            ViewBag.PageName = "SearchResult";
            ViewBag.PageSize = PageSize;
            ViewBag.PageNumber = pageN;
            ViewBag.TotalPage = totalPage;
            if (order != null)
            {
                ViewBag.Order = order;
                if (order.Equals("asc"))
                {
                    var q = from s in listResult
                            orderby s.Post.Price ascending
                            select s;
                    return View(q.ToList().Skip((pageN - 1) * PageSize).Take(PageSize));
                }
                else if (order.Equals("desc"))
                {
                    var q = from s in listResult
                            orderby s.Post.Price descending
                            select s;
                    return View(q.ToList().Skip((pageN - 1) * PageSize).Take(PageSize));
                }
                else
                {
                    return View(listResult.Skip((pageN - 1) * PageSize).Take(PageSize));
                }
            }
            else
            {
                return View(listResult.Skip((pageN - 1) * PageSize).Take(PageSize));
            }
        }

        [HttpGet]
        public ActionResult PostDetails(string postID)
        {
            PostDetailsViewModel post = new PostDetailsViewModel();
            var success = UpdatePostView(postID);
            if (success)
            {
                var temp = Int32.Parse(postID);
                post.Post = KhoaLuan.ViewModels.SearchResult.CreateListSearchResult().FirstOrDefault(p => p.PostID.Equals(temp));
                post.ImageList = GetImage.getListImage(post.Post.MotelID);
                if (Session["historyPost"] != null)
                {
                    var history = Session["historyPost"] as historyPost;
                    if (history.ListID.Count > 2)
                    {
                        var x = history.ListID[0];
                        history.ListID.Remove(x);
                    }
                    if (history.ListID.Count > 0)
                    {
                        post.ViewedPost = new List<SearchResult>();
                        var n = history.ListID.Count;
                        for (int i = n - 1; i >= 0; i--)
                        {
                            post.ViewedPost.Add(KhoaLuan.ViewModels.SearchResult.CreateListSearchResult().FirstOrDefault(p => p.PostID == history.ListID[i]));
                        }
                        var reslult = true;
                        foreach (var item in history.ListID)
                        {
                            if (item == temp)
                            {
                                reslult = false;
                            }
                        }
                        if (reslult)
                        {
                            history.ListID.Add(temp);
                        }
                    }
                    else
                    {
                        var h = new historyPost() { ListID = new List<int>() { temp } };
                        Session["historyPost"] = h;
                    }
                }
                else
                {
                    if (Session["historyPost"] == null)
                    {
                        var h = new historyPost() { ListID = new List<int>() { temp } };
                        Session["historyPost"] = h;
                        Session.Timeout = 10;
                    }
                }

                post.SuggestPost = new List<SearchResult>();
                var q = from s in KhoaLuan.ViewModels.SearchResult.CreateListSearchResult()
                        where s.ProvinceID == post.Post.ProvinceID && s.DistrictID == post.Post.DistrictID
                        orderby s.PostView descending
                        select s;
                int j = 0;
                foreach (var i in q)
                {
                    post.SuggestPost.Add(i);
                    j++;
                    if (j > 3)
                        break;
                }
                return View(post);
            }
            return View(post);
        }
        private bool UpdatePostView(string postID)
        {
            var result = false;
            if (!String.IsNullOrEmpty(postID))
            {
                var temp = 0;
                if (Int32.TryParse(postID, out temp))
                {
                    var current = db.Posts.FirstOrDefault(p => p.PostID.Equals(temp));
                    if (current != null)
                    {
                        current.PostView = current.PostView + 1;
                        db.SaveChanges();
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}