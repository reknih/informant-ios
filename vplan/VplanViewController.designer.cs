// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace vplan
{
	[Register ("VplanViewController")]
	partial class VplanViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIActivityIndicatorView spinnner { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView table { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (table != null) {
				table.Dispose ();
				table = null;
			}

			if (spinnner != null) {
				spinnner.Dispose ();
				spinnner = null;
			}
		}
	}
}
