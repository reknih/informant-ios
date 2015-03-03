// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace vplan
{
	public partial class RootController : UITabBarController
	{
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public RootController (IntPtr handle) : base (handle)
		{
			EdgesForExtendedLayout = UIRectEdge.None;
			ExtendedLayoutIncludesOpaqueBars = false;
			AutomaticallyAdjustsScrollViewInsets = false;
			//UINavigationBar.Appearance.BackgroundColor = UIColor.FromRGB(0,31,63);
			UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(0,31,63);
			UINavigationBar.Appearance.TintColor = UIColor.White;
			var attr = new UITextAttributes ();
			attr.TextColor = UIColor.White;
			UINavigationBar.Appearance.SetTitleTextAttributes(attr);
			UIApplication.SharedApplication.StatusBarHidden = false;
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		}
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			SetNeedsStatusBarAppearanceUpdate();
		}
		public override UIStatusBarStyle PreferredStatusBarStyle ()
		{
			return UIStatusBarStyle.LightContent;
		}
	}
}
