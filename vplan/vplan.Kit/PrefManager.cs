using System;
using System.Threading;
using Foundation;
using UIKit;

namespace vplan
{
	public class PrefManager : UntisExp.ISettings
	{
		NSUserDefaults locstore = new NSUserDefaults();
		public PrefManager ()
		{
			refresh ();
		}
		protected void refresh () {
			locstore.Synchronize ();
		}
		public void write(string key, object value){
			if (value.GetType == typeof(string))
				locstore.SetString (key, value);
			else if (value.GetType == typeof(int))
				locstore.SetInt (key, value);
			else if (value.GetType == typeof(bool))
				locstore.SetBool (key, value);
			else if (value.GetType == typeof(float))
				locstore.SetFloat (key, value);
			else if (value.GetType == typeof(double))
				locstore.SetDouble (key, value);
			else
				locstore.SetNativeField (key, NSObject.FromObject(value));
			refresh ();
		}
		public object read (string key) {
			var val = locstore.ValueForKey (key);
			return locstore.ValueForKey (key);
		}
		public int getInt (string key) {
			int val;
			val = Convert.ToInt32(locstore.IntForKey (key));
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

