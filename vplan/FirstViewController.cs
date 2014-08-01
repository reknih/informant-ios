using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using UntisExp;

namespace vplan
{
	public partial class FirstViewController : UIViewController
	{
		List<Data> ti;
		List<Igno> ili;
		List<News> globNews;
		Fetcher fetcher;
		Press press;
		PrefManager pm = new PrefManager ();
		bool isNews;
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public FirstViewController ()
			: base (UserInterfaceIdiomIsPhone ? "FirstViewController_iPhone" : "FirstViewController_iPad", null)
		{

			this.Title = "Vertretungen";
			this.TabBarItem.Image = UIImage.FromBundle ("first");
			fetcher = new Fetcher (clear, Alert, refresh, add);
			ti = new List<Data> ();
			ili = new List<Igno> ();
			isNews = false;
		}
		public FirstViewController (bool _a)
			: base (UserInterfaceIdiomIsPhone ? "FirstViewController_iPhone" : "FirstViewController_iPad", null)
		{

			this.Title = "Nachrichten";
			this.TabBarItem.Image = UIImage.FromBundle ("third");
			press = new Press ();
			isNews = true;
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached Group, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public void Alert (string title, string text, string btn)
		{
			InvokeOnMainThread (new NSAction (delegate {
				spinnner.StopAnimating();
				new UIAlertView (title, text, null, btn, null).Show ();
			}));
		}

		public override void ViewDidAppear(bool an)
		{
			base.ViewDidAppear (an);
			spinnner.StartAnimating ();
			if (isNews) {
				InitNews ();
			} else {
				InitVplan ();
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
					fetcher = new Fetcher(addToNewsTable, group, 30);
				}
			}
			catch {
				fetcher = new Fetcher(addToNewsTable, 5, 30);
			}
			try {
				if (table == null) {
					table = new UITableView (View.Bounds);
					table.AutoresizingMask = UIViewAutoresizing.All;
					table.Source = new NewsTableSource (globNews);
					Add (table);
				} else {
					table.Source = new NewsTableSource (globNews);
					table.ReloadData();
				}
				spinnner.StopAnimating();
			} catch {
			}
		}
		protected void InitVplan()
		{
			int group;
			try {
				int igC = pm.getInt ("ignoredCount");
				try {
					for (int i = igC; i > 0; i--) {
						string igKey = "ignored"+ Convert.ToString(i);
						string value = pm.getString(igKey);
						ili.Add(new Igno(value));
					}
				} catch {}
			} catch {}
			try {
				group = pm.getInt ("group");
				if (group == 0) {
					throw new Exception();
				} else {
					fetcher.getTimes(group, Activity.ParseFirstSchedule, 30);
				}
			}
			catch {
				if (UserInterfaceIdiomIsPhone) {
					this.TabBarController.SelectedIndex = 2;
					this.TabBarController.SelectedViewController = TabBarController.ChildViewControllers [2];
				} else {
					spinnner.StopAnimating ();
					var li = new List<Data> ();
					li.Add (new Data("Hallo! Wähle eine Klasse, um zu starten."));
					table.Source = new TableSource (li);
					table.ReloadData ();
				}
			}
		}
		public void reload(int _group) {
			spinnner.StartAnimating ();
			fetcher.getTimes(_group, Activity.ParseFirstSchedule, 30);
		}
		public void add(Data v1) {
			InvokeOnMainThread (new NSAction (delegate {
				try {
					ili.ForEach (delegate (Igno curr) {
						if (curr.Fach == v1.AltFach && curr.Lehrer == v1.Lehrer)
							throw new Exception();
					});
					spinnner.StopAnimating ();
					ti.Add (v1);
					List<Data> _ti = ti;
					if (_ti.Count == 0) {
						_ti.Add(new Data());
					}
					if (table == null) {
						table = new UITableView (View.Bounds);
						table.AutoresizingMask = UIViewAutoresizing.All;
						table.Source = new TableSource (_ti);
						Add (table);
					} else {
						table.Source = new TableSource (_ti);
						table.ReloadData();
					}
				} catch {} 
			}));
		}
		public void clear() {
		
			try {
			ti.Clear ();
			table.Source = new TableSource (ti);
			} catch {
			}
		}
		public void refresh(List<Data> v1) {
			InvokeOnMainThread (new NSAction (delegate {
				for (int i = v1.Count - 1; i >= 0; i--)
				{
					ili.ForEach (delegate (Igno curr) {
						try{
						if (curr.Fach == v1[i].AltFach && curr.Lehrer == v1[i].Lehrer)
							v1.RemoveAt(i);
						} catch {}
					});
				}
				spinnner.StopAnimating ();
				List<Data> _ti = v1;
				if (_ti.Count == 0) {
					_ti.Add(new Data());
				}
				table.Source = new TableSource (_ti);
				table.ReloadData();
				ti = v1;
			}));
		}
		protected void CreateTableItems ()
		{
			List<Data> tableItems = new List<Data> ();
			tableItems.Add (new Data ());
			table.Source = new TableSource (tableItems);
		}
		protected void addToNewsTable (News n) {
			globNews.Insert (0, n);
			InvokeOnMainThread (new NSAction (delegate {
				if (table == null) {
					table = new UITableView (View.Bounds);
					table.AutoresizingMask = UIViewAutoresizing.All;
					table.Source = new NewsTableSource (globNews);
					Add (table);
				} else {
					table.Source = new NewsTableSource (globNews);
					table.ReloadData ();
				}
			}));
		}

	}
}
