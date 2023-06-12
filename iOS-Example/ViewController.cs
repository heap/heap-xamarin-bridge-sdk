using Foundation;
using System;
using UIKit;
using HeapInc.Xamarin.iOS;

namespace iOS_Example
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            updateLabel();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        void updateLabel()
        {
            label.Text = "UserId: " + Heap.SharedInstance.UserId + "\nSessionId: " + Heap.SharedInstance.SessionId;
        }

        partial void startRecordingTapped(UIButton sender)
        {
            Console.WriteLine("startRecordingTapped...");
            Heap.SharedInstance.StartRecording("833626842");
            updateLabel();
        }

        partial void stopRecordingTapped(UIButton sender)
        {
            Console.WriteLine("stopRecordingTapped...");
            Heap.SharedInstance.StopRecording();
            updateLabel();
        }

        partial void identifyTapped(UIButton sender)
        {
            Console.WriteLine("identifyTapped");
            Heap.SharedInstance.Identify("Xamarin user");
            updateLabel();
        }

        partial void resetIdentityTapped(UIButton sender)
        {
            Console.WriteLine("resetIdentityTapped");
            Heap.SharedInstance.ResetIdentity();
            updateLabel();
        }

        partial void trackTapped(UIButton sender)
        {
            Console.WriteLine("trackTapped");
            Heap.SharedInstance.Track("My event");
            updateLabel();
        }

        partial void trackWithPropertiesTapped(UIButton sender)
        {
            Console.WriteLine("Tracking with properties");
            var properties = new NSDictionary("key1-string", "my-value 1", "key2-number", 256);
            Heap.SharedInstance.Track("My event with properties", properties);
            updateLabel();
        }

        partial void addEventPropertiesTapped(UIButton sender)
        {
            Console.WriteLine("addEventPropertiesTapped");
            var properties = new NSDictionary("my-event-property-string", "my-value 1", "my-event-property-number", 512);
            Heap.SharedInstance.AddEventProperties(properties);
            updateLabel();
        }

        partial void addUserProperties(UIButton sender)
        {
            Console.WriteLine("addUserProperties");
            var properties = new NSDictionary("my-user-property-string", "my-value 1", "my-user-property-number", 1024);
            Heap.SharedInstance.AddUserProperties(properties);
            updateLabel();
        }

        partial void clearEventPropertiesTapped(NSObject sender)
        {
            Console.WriteLine("clearEventPropertiesTapped...");
            Heap.SharedInstance.ClearEventProperties();
            updateLabel();
        }

        partial void removeEventPropertyTapped(UIButton sender)
        {
            Console.WriteLine("removeEventPropertyTapped...");
            Heap.SharedInstance.RemoveEventProperty("my-event-property-number");
            updateLabel();
        }
    }
}
