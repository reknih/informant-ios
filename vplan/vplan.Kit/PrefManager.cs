using System;
using System.Threading;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace vplan
{
	public class PrefManager
	{
		NSUbiquitousKeyValueStore store = NSUbiquitousKeyValueStore.DefaultStore;
		NSUserDefaults locstore = new NSUserDefaults();
		NSUrl iCloudUrl;
		public PrefManager ()
		{
			checkICloud ();
			refresh ();
		}
		protected void refresh () {
			store.Synchronize ();
			locstore.Synchronize ();
		}
		protected void checkICloud() {
			// GetUrlForUbiquityContainer is blocking, Apple recommends background thread
			new Thread(new ThreadStart(() => {
				var uburl = NSFileManager.DefaultManager.GetUrlForUbiquityContainer(null);
				// OR instead of null you can specify "TEAMID.com.xamarin.samples.icloud"
				if (uburl == null) {
					using(var pool = new NSAutoreleasePool())
						pool.InvokeOnMainThread(()=> {
						var alert = new UIAlertView("Keine i\uE049 verfügbar"
							, "Deine Klasse wird auf deinem Gerät gespeichert!"
							, null, "OK", null);
						alert.Show ();
					});
				} else { // iCloud enabled, store the NSURL for later use
					iCloudUrl = uburl;
				}
			})).Start();

		}
		public int getInt (string key) {
			int val;
			try {
				val = (int)store.GetDouble(key);
			} catch {
				val = locstore.IntForKey (key);
			}
			return val;
		}
		public string getString (string key) {
			string val;
			try {
				val = store.GetString(key);
			} catch {
				val = locstore.StringForKey (key);
			}
			return val;
		}
		public void setInt (string key, int val) {
			store.SetDouble (key, val);
			locstore.SetInt (val, key);
			refresh ();
		}
		public void setString (string key, string val) {
			store.SetString (key, val);
			locstore.SetString (val, key);
			refresh ();
		}
	}
}

