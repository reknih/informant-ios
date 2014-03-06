using System;
using MonoTouch.UIKit;

namespace vplan
{
	public class SplitView : UISplitViewController
	{
		UIViewController masterView, detailView;
		SplitDelegate sd;

		public SplitView () : base()
		{
			// create our master and detail views
			masterView = new SecondViewController ();
			detailView = new FirstViewController ();
			sd = new SplitDelegate ();
			Delegate = sd;
			// create an array of controllers from them and then
			// assign it to the controllers property
			ViewControllers = new UIViewController[]
			{ masterView, detailView }; // order is important
		}

		public override bool ShouldAutorotateToInterfaceOrientation
		(UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
		class SplitDelegate : UISplitViewControllerDelegate
		{
			public override bool ShouldHideViewController (UISplitViewController svc,
				UIViewController viewController, UIInterfaceOrientation inOrientation)
			{
				return false;
			}
		}
	}
}

