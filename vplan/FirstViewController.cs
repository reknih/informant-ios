using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace vplan
{
	public partial class FirstViewController : UIViewController
	{
		List<Data> ti;
		Fetcher fetcher;
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public FirstViewController ()
			: base (UserInterfaceIdiomIsPhone ? "FirstViewController_iPhone" : "FirstViewController_iPad", null)
		{

			this.Title = "Vertretungen";
			this.TabBarItem.Image = UIImage.FromBundle ("first");
			fetcher = new Fetcher (this);
			ti = new List<Data> ();
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
		public override void ViewDidAppear(bool an) {
			base.ViewDidAppear (an);
			var store = NSUbiquitousKeyValueStore.DefaultStore;
			int group;
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
