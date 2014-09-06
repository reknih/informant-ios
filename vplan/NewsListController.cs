// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using UntisExp;

namespace vplan
{
	public partial class NewsListController : UITableViewController
	{
		Fetcher fetcher;
		PrefManager pm = new PrefManager ();
		Press press;
		List<News> globNews;


		public NewsListController (IntPtr handle) : base (handle)
		{

			this.Title = "Nachrichten";
			this.TabBarItem.Image = UIImage.FromBundle ("third");

			press = new Press ();
			this.NavigationItem.SetRightBarButtonItem (
				new UIBarButtonItem ("Zurück", UIBarButtonItemStyle.Done, delegate(object sender, EventArgs e) {
					var initialViewController = Storyboard.InstantiateInitialViewController () as UIViewController;
					NavigationController.NavigationBarHidden = true;
					NavigationController.PushViewController (initialViewController, true);
				}), true);
		}

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public void Alert (string title, string text, string btn)
		{
			InvokeOnMainThread (new NSAction (delegate {
				spinner.StopAnimating();
				new UIAlertView (title, text, null, btn, null).Show ();
			}));
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			spinner.StartAnimating ();
			if (!UserInterfaceIdiomIsPhone) {
				TableView.ContentInset = new UIEdgeInsets (20.0f, 0.0f, 20.0f, 0.0f);
			} else {
				SetNeedsStatusBarAppearanceUpdate();
				UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;
				UIApplication.SharedApplication.SetStatusBarStyle (UIStatusBarStyle.LightContent, false);
				try {
					((NewsSuperViewController)ParentViewController).blackMesa(globNews[0]);
				} catch {}
			}
			InitNews ();

		}
		public override UIStatusBarStyle PreferredStatusBarStyle ()
		{
			return UIStatusBarStyle.LightContent;
		}
			
		public override void PrepareForSegue (UIStoryboardSegue segue, 
			NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			var newsItemController = segue.DestinationViewController 
				as NewsItemController;
			try {
				if (newsItemController != null) {
					newsItemController.theNews = ((NewsTableSource)sender).selected;
				}
			} catch {
			}
		}


		protected async void InitNews() 
		{
			var news = await press.getNews ();
			globNews = news;
			int group;
			try {
				group = pm.getInt ("group");
				if (group == 0) {
					throw new Exception();
				} else {
					fetcher = new Fetcher(addToNewsTable, group);
				}
			}
			catch {
				fetcher = new Fetcher(addToNewsTable, 5, 30);
			}
			try {
				if (table == null) {
					table = new UITableView (View.Bounds);
					table.AutoresizingMask = UIViewAutoresizing.All;
					table.Source = new NewsTableSource (globNews, this);
					Add (table);
				} else {
					table.Source = new NewsTableSource (globNews, this);
					table.ReloadData();
				}
				spinner.StopAnimating();
			} catch {
			}
		}

		protected void addToNewsTable (News n) {
			globNews.Insert (0, n);
			InvokeOnMainThread (new NSAction (delegate {
				if (table == null) {
					table = new UITableView (View.Bounds);
					table.AutoresizingMask = UIViewAutoresizing.All;
					table.Source = new NewsTableSource (globNews, this);
					Add (table);
				} else {
					table.Source = new NewsTableSource (globNews, this);
					table.ReloadData ();
				}
				try {
				((NewsSuperViewController)ParentViewController).blackMesa(globNews[0]);
				} catch {}
			}));
		}
	}
}
