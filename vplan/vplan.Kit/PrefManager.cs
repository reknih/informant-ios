using System;
using MonoTouch.Foundation;

namespace vplan
{
	public class PrefManager
	{
		NSUbiquitousKeyValueStore store = NSUbiquitousKeyValueStore.DefaultStore;
		NSUserDefaults locstore = new NSUserDefaults();
		public PrefManager ()
		{
			refresh ();
		}
		protected void refresh () {
			store.Synchronize ();
			locstore.Synchronize ();
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

