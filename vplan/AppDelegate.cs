using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using UntisExp;

namespace vplan
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		Action<UIBackgroundFetchResult> completionHandler;
		PrefManager pm = new PrefManager ();
		NSUserDefaults nu = new NSUserDefaults();
		public static UIStoryboard Storyboard;
		List<Data> l;
		//
		// This method is invoked when the application has loaded and is ready to run. In this
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval (UIApplication.BackgroundFetchIntervalMinimum);
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
			var settings = UIUserNotificationSettings.GetSettingsForTypes (UIUserNotificationType.Alert
			               | UIUserNotificationType.Badge
			               | UIUserNotificationType.Sound, new NSSet ());
			UIApplication.SharedApplication.RegisterUserNotificationSettings (settings);
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			if (UserInterfaceIdiomIsPhone) {
				Storyboard = UIStoryboard.FromName ("MainStoryboard_iPhone", null);
			} else {
				Storyboard = UIStoryboard.FromName ("MainStoryboard_iPad", null);
			}

			var initialViewController = Storyboard.InstantiateInitialViewController () as UIViewController;
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
			window.RootViewController = initialViewController;
			window.MakeKeyAndVisible ();
			return true;
			

		}
		public override void PerformFetch (UIApplication application, Action<UIBackgroundFetchResult> _completionHandler)
		{
			int mode;
			nu = new NSUserDefaults();
			try {
				nu.Synchronize();
				if (nu.BoolForKey ("backgrounding") == false) {
					_completionHandler (UIBackgroundFetchResult.NoData);
					return;
				}
			} catch {
			}
			try {
				mode = (int)nu.IntForKey("bgMode");
			} catch {
				mode = 0;
			}
			completionHandler = _completionHandler;
			Fetcher fetcher = new Fetcher (Alert, Refresh, mode);
			int group;
			try {
				group = pm.getInt ("group");
				if (group == 0) {
					throw new Exception();
				} else {
					fetcher.getTimes(group);
				}
			} catch {
				_completionHandler (UIBackgroundFetchResult.Failed);
			}
		}
		protected void Clear () {
			l = new List<Data> ();
		}
		protected void Refresh (List<Data> _l) {
			l = _l;
			for (int i = l.Count - 1; i >= 0; i--)
			{
				if (l[i].Head) {
					l.RemoveAt (i);
				}
			}
			finish ();
		}
		public override UIWindow Window {
			get;
			set;
		}
		protected void finish () {
			InvokeOnMainThread (() => {
				var ili = new List<Igno>();
				try {
					int igC = pm.getInt ("ignoredCount");
					try {
						for (int i = igC; i > 0; i--) {
							string igKey = "ignored"+ Convert.ToString(i);
							string value = pm.getString(igKey);
							ili.Add(new Igno(value));
						}
					} catch {}
				} catch {}
				try {
					for (int i = l.Count - 1; i >= 0; i--)
					{
						ili.ForEach (delegate (Igno curr) {
							try{
								if (curr.Fach == l[i].AltFach && curr.Lehrer == l[i].Lehrer)
									l.RemoveAt(i);
							} catch {}
						});
					}
					if (l.Count == 0) {
						UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
						if (nu.IntForKey("oldVal")==l.Count) {
							completionHandler(UIBackgroundFetchResult.NoData);
						} else {
							nu.SetInt(l.Count, "oldVal");
							nu.Synchronize();
							completionHandler(UIBackgroundFetchResult.NewData);
						}
					}
					else if(nu.IntForKey("oldVal")==l.Count){
						completionHandler(UIBackgroundFetchResult.NoData);
					} else {
						throw new Exception();
					}
				} catch {
					var notification = new UILocalNotification();

					// set the fire date (the date time in which it will fire)
					DateTime now = DateTime.Now.AddSeconds(2);
					if (now.Kind == DateTimeKind.Unspecified)
						now = DateTime.SpecifyKind(now, DateTimeKind.Local);
					notification.FireDate = ((NSDate)now);

					// configure the alert stuff
					notification.AlertAction = "Anzeigen";
					if (l.Count == 1) {
						notification.AlertBody = "Es gibt eine neue Vertretung";
					} else {
						notification.AlertBody = "Es gibt "+ l.Count + " neue Vertretungen";
					}

					// modify the badge
					notification.ApplicationIconBadgeNumber = l.Count;

					// set the sound to be the default sound
					notification.SoundName = UILocalNotification.DefaultSoundName;
					nu.SetInt(l.Count, "oldVal");
					nu.Synchronize();
					// schedule it
					UIApplication.SharedApplication.ScheduleLocalNotification(notification);
					completionHandler (UIBackgroundFetchResult.NewData);
				}
			});
		}
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		protected void Alert () {
			completionHandler (UIBackgroundFetchResult.Failed);
		}
	}
}

