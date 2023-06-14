using Foundation;
using System;
using System.Collections.Generic;
using UIKit;
using HeapInc.Xamarin;
using static HeapInc.Xamarin.iOS.Implementation;

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
            label.Text = "UserId: " + Heap.UserId + "\nSessionId: " + Heap.SessionId + "\nIdentity: " + Heap.Identity;
        }

        partial void startRecordingTapped(UIButton sender)
        {
            StartRecording("833626842", new HeapOptions
            {
                //BaseUri = new Uri("https://mybaseuri.com"),
                UploadInterval = TimeSpan.FromSeconds(5),
                CaptureAdvertiserId = true,
                StartSessionImmediately = true
            });
            updateLabel();
        }

        partial void stopRecordingTapped(UIButton sender)
        {
            Heap.StopRecording();
            updateLabel();
        }

        partial void identifyTapped(UIButton sender)
        {
            Heap.Identify("Xamarin user");
            updateLabel();
        }

        partial void resetIdentityTapped(UIButton sender)
        {
            Heap.ResetIdentity();
            updateLabel();
        }

        partial void trackTapped(UIButton sender)
        {
            Heap.Track("My event");
            updateLabel();
        }

        partial void trackWithPropertiesTapped(UIButton sender)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>
                {
                    {"key-1", "value-1"},
                    {"key-2", "value-2"},
                    {"key-3", "value-3"},
                    {"key-4", "value-4" },
                    {"key-5", "value-5"}
                };
            Heap.Track("My event with properties", properties);
            updateLabel();
        }

        partial void addEventPropertiesTapped(UIButton sender)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>
                {
                    {"event-key-1", "event-value-1"},
                    {"event-key-2", "event-value-2"},
                    {"event-key-3", "event-value-3"},
                    {"event-key-4", "event-value-4" },
                    {"event-key-5", "event-value-5"}
                };
            Heap.AddEventProperties(properties);
            updateLabel();
        }

        partial void addUserProperties(UIButton sender)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>
                {
                    {"user-key-1", "user-value-1"},
                    {"user-key-2", "user-value-2"},
                    {"user-key-3", "user-value-3"},
                    {"user-key-4", "user-value-4" },
                    {"user-key-5", "user-value-5"}
                };
            Heap.AddUserProperties(properties);
            updateLabel();
        }

        partial void clearEventPropertiesTapped(NSObject sender)
        {
            Heap.ClearEventProperties();
            updateLabel();
        }

        partial void removeEventPropertyTapped(UIButton sender)
        {
            Heap.RemoveEventProperty("event-key-5");
            updateLabel();
        }
    }
}
