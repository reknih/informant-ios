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
	[Register ("NewsItemController")]
	partial class NewsItemController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnMore { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imgMain { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblSource { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblTitle { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextView txtMain { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imgMain != null) {
				imgMain.Dispose ();
				imgMain = null;
			}

			if (lblSource != null) {
				lblSource.Dispose ();
				lblSource = null;
			}

			if (lblTitle != null) {
				lblTitle.Dispose ();
				lblTitle = null;
			}

			if (txtMain != null) {
				txtMain.Dispose ();
				txtMain = null;
			}

			if (btnMore != null) {
				btnMore.Dispose ();
				btnMore = null;
			}
		}
	}
}
