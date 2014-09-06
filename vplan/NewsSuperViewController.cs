// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using UntisExp;

namespace vplan
{
	public partial class NewsSuperViewController : UISplitViewController
	{
		UIViewController[] controllers;
		public NewsSuperViewController (IntPtr handle) : base (handle)
		{
			this.Title = "Nachrichten";
			this.TabBarItem.Image = UIImage.FromBundle ("third");
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			controllers = ViewControllers;
			Delegate = new RenaissanceSplitViewDelegate ();
		}

		public void blackMesa(News incident)
		{
			((NewsItemController)controllers [1]).freshNews (incident);
		}
	}
}
