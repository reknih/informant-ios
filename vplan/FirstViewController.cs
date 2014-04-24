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
		Fetcher fetcher;
		NSUbiquitousKeyValueStore store = NSUbiquitousKeyValueStore.DefaultStore;
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
		public void Alert (string title, string text, string btn) {
			InvokeOnMainThread (new NSAction (delegate {
				new UIAlertView (title, text, null, btn, null).Show ();
			}));
		}
		public override void ViewDidAppear(bool an) {
			base.ViewDidAppear (an);
			int group;
			try {
				int igC = (int)store.GetDouble ("ignoredCount");
				try {
					for (int i = igC; i > 0; i--) {
						string igKey = "ignored"+ Convert.ToString(i);
						string value = store.GetString(igKey);
						ili.Add(new Igno(value));
					}
				} catch {}
			} catch {}
			try {
				group = (int)store.GetDouble ("group");
				if (group == 0) {
					throw new Exception();
				} else {
					fetcher.getTimes(group, false);
				}
			}
			catch {
				if (UserInterfaceIdiomIsPhone) {
					this.TabBarController.SelectedIndex = 1;
					this.TabBarController.SelectedViewController = TabBarController.ChildViewControllers [1];
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
			fetcher.getTimes(_group, false);
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

	}
}
