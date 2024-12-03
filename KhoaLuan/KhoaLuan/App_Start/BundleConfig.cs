using System.Web;
using System.Web.Optimization;

namespace KhoaLuan
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			#region Styles

			bundles.Add(new StyleBundle("~/Content/style").Include(
					"~/Content/Custom/custom-scrollbar.css",
				   "~/Content/Theme/style.css"));

			/*##### Share #####*/
			bundles.Add(new StyleBundle("~/Content/share").Include(
				"~/Content/Custom/share.css",
				"~/Content/Custom/login-modal.css",
				"~/Content/Custom/my-style.css"));

			bundles.Add(new StyleBundle("~/Content/index").Include(
				"~/Content/Custom/index.css"));

			#endregion

			#region Scripts

			/*##### Jquery #####*/
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-3.3.1.min.js"));

				 /*##### Bootstrap #####*/
				 bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
						"~/Scripts/Bootstrap/popper.min.js",
						"~/Scripts/Bootstrap/bootstrap.min.js"));

			bundles.Add(new ScriptBundle("~/bundles/share").Include(
				"~/Scripts/plugins/plugins.js",
				"~/Scripts/plugins/active.js"));

			/*##### Custom #####*/
			bundles.Add(new ScriptBundle("~/bundles/custom").Include(
				"~/Scripts/Custom/sign-up-animate.js",
				"~/Scripts/Custom/login-modal-animate.js",
				"~/Scripts/Custom/validate-login.js",
				"~/Scripts/Custom/validate-sign-up.js",
				"~/Scripts/Custom/ask-modal.js",
				"~/Scripts/Custom/index.js",
				"~/Scripts/Custom/sign-up.js"));

			#endregion
		}
	}
}
