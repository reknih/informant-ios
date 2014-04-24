using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using UntisExp;

namespace vplan
{
	public partial class SecondViewController : UIViewController
	{
		List<Group> ti;
		Fetcher fetcher;
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public SecondViewController ()
			: base (UserInterfaceIdiomIsPhone ? "SecondViewController_iPhone" : "SecondViewController_iPad", null)
		{
			fetcher = new Fetcher (Alert, refresh);
			if (UserInterfaceIdiomIsPhone) {
				this.Title = "Klasse";
				this.TabBarItem.Image = UIImage.FromBundle ("second");
			}
		}
		public void Alert (string title, string text, string btn) {
			InvokeOnMainThread (new NSAction (delegate {
				new UIAlertView (title, text, null, btn, null).Show ();
			}));
		}
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached Group, images, etc that aren't in use.
		}
		public void refresh (List<Data> s) {
			throw new NotImplementedException ();
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			fetcher.getClasses();
			// Perform any additional setup after loading the view, typically from a nib.
		}
		public void add(Group v1) {
			InvokeOnMainThread (new NSAction (delegate {
				spinner.StopAnimating ();
				ti.Add (v1);
				if (table == null) {
					table = new UITableView (View.Bounds);
					table.AutoresizingMask = UIViewAutoresizing.All;
					table.Source = new GroupTableSource (ti, this);
					Add (table);
				} else {
					table.Source = new GroupTableSource (ti, this);
					table.ReloadData();
				}
			}));
		}
		public void clear() {
			if (ti != null)
				ti.Clear ();
			InvokeOnMainThread (new NSAction (delegate {
				spinner.StartAnimating();
				if (table != null) {
					table = null;
				}
			}));
		}
		public void changeView () {
			if (UserInterfaceIdiomIsPhone) {
				this.TabBarController.SelectedIndex = 0;
				this.TabBarController.SelectedViewController = TabBarController.ChildViewControllers [0];
			}
		}
		public void goToUpdate (int group) {
			SplitView sv = (SplitView)SplitViewController;
			sv.updateDetails (group);
		}
		public void refresh(List<Group> v1) {
			InvokeOnMainThread (new NSAction (delegate {
				spinner.StopAnimating ();
				table.Source = new GroupTableSource (v1, this);
				table.ReloadData();
				ti = v1;
			}));
		}
		protected void CreateTableItems ()
		{
			List<Group> tableItems = new List<Group> ();
			tableItems.Add (new Group ());
			table.Source = new GroupTableSource (tableItems, this);
		}

	}
}

