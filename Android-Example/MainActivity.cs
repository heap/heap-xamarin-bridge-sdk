using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Button;
using Google.Android.Material.Snackbar;
using System.Collections.Generic;
using Android.Widget;
using HeapInc.Xamarin;
using static HeapInc.Xamarin.Android.Implementation;

namespace Android_Example
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            var startRecordingButton = FindViewById(Resource.Id.button_start_recording);
            startRecordingButton.Click += StartRecordingButton_Click;

            var stopRecordingButton = FindViewById(Resource.Id.button_stop_recording);
            stopRecordingButton.Click += StopRecordingButton_Click;

            var trackButton = FindViewById(Resource.Id.button_track);
            trackButton.Click += TrackButton_Click;

            var trackWithPropertiesButton = FindViewById(Resource.Id.button_track_with_properties);
            trackWithPropertiesButton.Click += TrackWithPropertiesButton_Click;

            var identifyButton = FindViewById(Resource.Id.button_identify);
            identifyButton.Click += IdentifyButton_Click;

            var resetIdentityButton = FindViewById(Resource.Id.button_reset_identity);
            resetIdentityButton.Click += ResetIdentityButton_Click;

            var addEventPropertiesButton = FindViewById(Resource.Id.button_add_event_properties);
            addEventPropertiesButton.Click += AddEventPropertiesButton_Click;

            var addUserPropertiesButton = FindViewById(Resource.Id.button_add_user_properties);
            addUserPropertiesButton.Click += AddUserPropertiesButton_Click;

            var removeEventPropertiesButton = FindViewById(Resource.Id.button_remove_event_properties);
            removeEventPropertiesButton.Click += RemoveEventPropertiesButton_Click;

            var clearEventPropertiesButton = FindViewById(Resource.Id.button_clear_event_properties);
            clearEventPropertiesButton.Click += ClearEventPropertiesButton_Click;

            SetLogLevel(HeapLogLevel.Trace);
            StartRecording(this, "833626842", new HeapOptions
            {
                //BaseUri = new Uri("https://mybaseuri.com"),
                UploadInterval = TimeSpan.FromSeconds(5),
                CaptureAdvertiserId = true,
                StartSessionImmediately = true
            });

            UpdateLabel();
        }

        private void UpdateLabel()
        {
            TextView infoLabel = FindViewById<TextView>(Resource.Id.heap_label);
            infoLabel.Text = "UserId: " + Heap.UserId + "\nSessionId: " + Heap.SessionId + "\nIdentity: " + Heap.Identity;
        }

        private void StartRecordingButton_Click(object sender, EventArgs e)
        {
            StartRecording(this, "833626842", new HeapOptions
            {
                //BaseUri = new Uri("https://mybaseuri.com"),
                UploadInterval = TimeSpan.FromSeconds(5),
                CaptureAdvertiserId = true,
                StartSessionImmediately = true
            });
            UpdateLabel();
        }

        private void StopRecordingButton_Click(object sender, EventArgs e)
        {
            Heap.StopRecording();
            UpdateLabel();
        }

        private void TrackButton_Click(object sender, EventArgs e)
        {
            Heap.Track("my xamarin event on android");
            UpdateLabel();
        }

        private void TrackWithPropertiesButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>
                {
                    {"key-1", "value-1"},
                    {"key-2", "value-2"},
                    {"key-3", "value-3"},
                    {"key-4", "value-4"},
                    {"key-5", "value-5"}
                };
            Heap.Track("My xamarin android event with properties", properties);
            UpdateLabel();
        }

        private void IdentifyButton_Click(object sender, EventArgs e)
        {
            Heap.Identify("Android Xamarin User");
            UpdateLabel();
        }

        private void ResetIdentityButton_Click(object sender, EventArgs e)
        {
            Heap.ResetIdentity();
            UpdateLabel();
        }

        private void AddEventPropertiesButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>
                {
                    {"event-key-1", "event-value-1"},
                    {"event-key-2", "event-value-2"},
                    {"event-key-3", "event-value-3"},
                    {"event-key-4", "event-value-4"},
                    {"event-key-5", "event-value-5"}
                };
            Heap.AddEventProperties(properties);
            UpdateLabel();
        }

        private void AddUserPropertiesButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>
                {
                    {"user-key-1", "user-value-1"},
                    {"user-key-2", "user-value-2"},
                    {"user-key-3", "user-value-3"},
                    {"user-key-4", "user-value-4"},
                    {"user-key-5", "user-value-5"}
                };
            Heap.AddUserProperties(properties);
            UpdateLabel();
        }

        private void RemoveEventPropertiesButton_Click(object sender, EventArgs e)
        {
            Heap.RemoveEventProperty("event-key-1");
            UpdateLabel();
        }

        private void ClearEventPropertiesButton_Click(object sender, EventArgs e)
        {
            Heap.ClearEventProperties();
            UpdateLabel();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();

            Heap.Track("xamarin_event");
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}

