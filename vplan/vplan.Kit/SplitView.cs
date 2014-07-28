using System;
using MonoTouch.UIKit;

namespace vplan
{
	public class SplitView : UISplitViewController
	{
		UIViewController masterView, detailView;
		FirstViewController fv;
		SplitDelegate sd;

		public SplitView () : base()
		{
			// create our master and detail views
			masterView = new SecondViewController ();
			detailView = new FirstViewController ();
			fv = (FirstViewController)detailView;
			sd = new SplitDelegate ();
			Delegate = sd;
			// create an array of controllers from them and then
			// assign it to the controllers property
			ViewControllers = new UIViewController[]
			{ masterView, detailView }; // order is important
		}
		public void updateDetails(int group) {
			fv.reload (group);
		}

		/*public override bool ShouldAutorotateToInterfaceOrientation
		(UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}*/
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

