﻿using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

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
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			window.AutosizesSubviews = true;

			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) {
				tabBarController = new UITabBarController ();
				var viewController2 = new SecondViewController ();
				var viewController1 = new FirstViewController ();
				tabBarController.ViewControllers = new UIViewController [] {
					viewController1,
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
	}
}

