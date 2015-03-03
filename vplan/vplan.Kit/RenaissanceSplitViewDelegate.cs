using System;
using UIKit;
using Foundation;

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

