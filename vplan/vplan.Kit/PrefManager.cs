using System;
using System.Threading;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace vplan
{
	public class PrefManager
	{
		NSUserDefaults locstore = new NSUserDefaults();
		bool notified = false;
		public PrefManager ()
		{
			refresh ();
		}
		protected void refresh () {
			locstore.Synchronize ();
		}
		public int getInt (string key) {
			int val;
			val = locstore.IntForKey (key);
			return val;
		}
		public string getString (string key) {
			string val;
			val = locstore.StringForKey (key);
			return val;
		}
		public void setInt (string key, int val) {
			locstore.SetInt (val, key);
			refresh ();
		}
		public void setString (string key, string val) {
			locstore.SetString (val, key);
			refresh ();
		}
	}
}

