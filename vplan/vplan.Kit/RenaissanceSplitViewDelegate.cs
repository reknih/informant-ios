using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace vplan
{
	public class RenaissanceSplitViewDelegate : UISplitViewControllerDelegate
	{
		public RenaissanceSplitViewDelegate ()
		{
		}
		public override bool ShouldHideViewController (UISplitViewController svc, UIViewController viewController, UIInterfaceOrientation inOrientation)
		{
			return false;
		}
	}
}

