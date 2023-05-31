// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace iOS_Example
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UILabel label { get; set; }

		[Action ("addEventPropertiesTapped:")]
		partial void addEventPropertiesTapped (UIKit.UIButton sender);

		[Action ("addUserProperties:")]
		partial void addUserProperties (UIKit.UIButton sender);

		[Action ("clearEventPropertiesTapped:")]
		partial void clearEventPropertiesTapped (Foundation.NSObject sender);

		[Action ("identifyTapped:")]
		partial void identifyTapped (UIKit.UIButton sender);

		[Action ("removeEventPropertyTapped:")]
		partial void removeEventPropertyTapped (UIKit.UIButton sender);

		[Action ("resetIdentityTapped:")]
		partial void resetIdentityTapped (UIKit.UIButton sender);

		[Action ("startRecordingTapped:")]
		partial void startRecordingTapped (UIKit.UIButton sender);

		[Action ("stopRecordingTapped:")]
		partial void stopRecordingTapped (UIKit.UIButton sender);

		[Action ("trackTapped:")]
		partial void trackTapped (UIKit.UIButton sender);

		[Action ("trackWithPropertiesTapped:")]
		partial void trackWithPropertiesTapped (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (label != null) {
				label.Dispose ();
				label = null;
			}
		}
	}
}
