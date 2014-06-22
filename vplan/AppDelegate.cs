using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
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
		UITabBarController tabBarController;
		UISplitViewController splitViewController;
		Action<UIBackgroundFetchResult> completionHandler;
		PrefManager pm = new PrefManager ();
		NSUserDefaults nu = new NSUserDefaults();
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
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			window.AutosizesSubviews = true;

			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) {
				tabBarController = new UITabBarController ();
				var viewController2 = new SecondViewController ();
				var viewController1 = new FirstViewController ();
				var viewController3 = new FirstViewController (true);
				tabBarController.ViewControllers = new UIViewController [] {
					viewController1,
					viewController3,
					viewController2,
				};
			
				window.RootViewController = tabBarController;
			} else {
				splitViewController = new SplitView ();
				window.RootViewController = splitViewController;
			}
			window.MakeKeyAndVisible ();
			
			return true;
		}
		public override void PerformFetch (UIApplication application, Action<UIBackgroundFetchResult> _completionHandler)
		{
			int mode;
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
		protected void finish () {
			InvokeOnMainThread (new NSAction (delegate {
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
					notification.FireDate = DateTime.Now;

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
			}));
		}
		protected void Alert () {
			completionHandler (UIBackgroundFetchResult.Failed);
		}
	}
}

