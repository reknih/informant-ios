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
	[Register ("NewsListController")]
	partial class NewsListController
	{
		[Outlet]
		MonoTouch.UIKit.UIActivityIndicatorView spinner { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView table { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (spinner != null) {
				spinner.Dispose ();
				spinner = null;
			}

			if (table != null) {
				table.Dispose ();
				table = null;
			}
		}
	}
}
