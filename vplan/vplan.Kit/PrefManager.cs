using System;
using System.Threading;
using Foundation;
using UIKit;

namespace vplan
{
	public class PrefManager : UntisExp.Interfaces.ISettings
	{
		NSUserDefaults locstore = new NSUserDefaults();
		public PrefManager ()
		{
			refresh ();
		}
		protected void refresh () {
			locstore.Synchronize ();
		}
		public void Write (string key, object value){
			if (value.GetType() == typeof(string))
				locstore.SetString (key, value as string);
			else if (value.GetType() == typeof(int))
				locstore.SetInt ((int)value, key);
			else if (value.GetType() == typeof(bool))
				locstore.SetBool ((bool)value, key);
			else if (value.GetType() == typeof(float))
				locstore.SetFloat ((float)value, key);
			else if (value.GetType() == typeof(double))
				locstore.SetDouble ((double)value, key);
			else
				locstore.SetNativeField (key, NSObject.FromObject(value));
			refresh ();
		}
		public object Read (string key) {
			var nskey = new NSString (key);
			var val = locstore.ValueForKey (nskey);
			return val;
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

