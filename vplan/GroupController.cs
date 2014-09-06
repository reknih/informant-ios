// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using UntisExp;

namespace vplan
{
	public partial class GroupController : UITableViewController
	{
		List<Group> ti;
		Fetcher fetcher;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public GroupController (IntPtr handle) : base (handle)
		{
			fetcher = new Fetcher (Alert, refresh);
			this.Title = "Klasse";
			this.TabBarItem.Image = UIImage.FromBundle ("second");
			EdgesForExtendedLayout = UIRectEdge.None;
			ExtendedLayoutIncludesOpaqueBars = false;
			AutomaticallyAdjustsScrollViewInsets = false;

		}

		public void Alert (string title, string text, string btn) {
			InvokeOnMainThread (new NSAction (delegate {
				new UIAlertView (title, text, null, btn, null).Show ();
			}));
		}

		public void refresh (List<Data> s) {
			throw new NotImplementedException ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			spinnner.StartAnimating ();
			fetcher.getClasses ();
			TableView.ContentInset = new UIEdgeInsets (20.0f, 0.0f, 0.0f, 0.0f);
		}
		public override void ViewDidAppear (bool animated)
		{

		}

		public void add(Group v1) {
			InvokeOnMainThread (new NSAction (delegate {
				spinnner.StopAnimating ();
				ti.Add (v1);

				TableView.Source = new GroupTableSource (ti, this);
				TableView.ReloadData();

			}));
		}

		public void clear() {
			if (ti != null)
				ti.Clear ();
			InvokeOnMainThread (new NSAction (delegate {
				spinnner.StartAnimating();

			}));
		}

		public void changeView () {
			if (UserInterfaceIdiomIsPhone) {
				this.TabBarController.SelectedIndex = 0;
				this.TabBarController.SelectedViewController = TabBarController.ChildViewControllers [0];
			}
		}
			

		public void refresh(List<Group> v1) {
			InvokeOnMainThread (new NSAction (delegate {
				spinnner.StopAnimating ();
				TableView.Source = new GroupTableSource (v1, this);
				ti = v1;
				table.ReloadData();
				TableView.ReloadInputViews();
			}));
		}

		protected void CreateTableItems ()
		{
			List<Group> tableItems = new List<Group> ();
			tableItems.Add (new Group ());
			TableView.Source = new GroupTableSource (tableItems, this);
		}

	}
}