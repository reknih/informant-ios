// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace vplan
{
	[Register ("NewsItemController")]
	partial class NewsItemController
	{
		[Outlet]
		UIKit.UIButton btnMore { get; set; }

		[Outlet]
		UIKit.UIImageView imgMain { get; set; }

		[Outlet]
		UIKit.UILabel lblSource { get; set; }

		[Outlet]
		UIKit.UILabel lblTitle { get; set; }

		[Outlet]
		UIKit.UITextView txtMain { get; set; }
		
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
