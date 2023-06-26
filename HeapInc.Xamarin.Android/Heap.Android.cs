using System;
using System.Collections;
using System.Collections.Generic;
using IO.Heap.Core;
using IO.Heap.Core.Logs;
using IO.Heap.Core.Api.Plugin.Model;

#nullable enable
namespace HeapInc.Xamarin.Android
{
    public static class Implementation
    {
        public static void StartRecording(global::Android.Content.Context context, string environmentId)
        {
            StartRecording(context, environmentId, new HeapOptions());
        }

        public static void StartRecording(global::Android.Content.Context context, string environmentId, HeapOptions options)
        {
            var implementation = HeapAndroid.Instance;
            implementation.StartRecording(context, environmentId, options);

            Heap.SetImplementation(implementation);
        }

        public static void SetLogLevel(HeapLogLevel logLevel)
        {
            CoreHeap.SetLogLevel(HeapAndroid.HeapLogLevelToAndroidLogLevel(logLevel));
        }
    }

    public class HeapAndroid : HeapInc.Xamarin.IHeap
    {
        private SourceInfo SourceInfo { get; }

        private static readonly HeapAndroid instance = new HeapAndroid();

        public static HeapAndroid Instance
        {
            get
            {
                return instance;
            }
        }

        protected HeapAndroid()
        {
            var version = typeof(HeapAndroid).Assembly.GetName().Version.ToString();
            SourceInfo = new SourceInfo("xamarin_bridge", version, "Xamarin.Android", new Dictionary<string, Java.Lang.Object>());
        }

        public string? SessionId => CoreHeap.SessionId;
        public string? UserId => CoreHeap.UserId;
        public string? Identity => CoreHeap.Identity;

        public void StartRecording(global::Android.Content.Context context, string environmentId)
        {
            CoreHeap.StartRecording(context, environmentId);
        }

        public void StartRecording(global::Android.Content.Context context, string environmentId, HeapOptions options)
        {
            Java.Net.URI uri = new Java.Net.URI("https://c.us.heap-api.com");
            double uploadInterval = Options.DefaultUploadInterval;
            bool captureAdvertiserId = Options.DefaultCaptureAdId;
            bool startSessionImmediately = Options.DefaultStartSessionImmediately;

            if (options.BaseUri is not null)
            {
                uri = new Java.Net.URI(options.BaseUri.AbsoluteUri);
            }

            if (options.UploadInterval is not null)
            {
                uploadInterval = (double)options.UploadInterval.Value.TotalSeconds;
            }

            if (options.CaptureAdvertiserId)
            {
                captureAdvertiserId = (bool)options.CaptureAdvertiserId;
            }

            if (options.StartSessionImmediately)
            {
                startSessionImmediately = (bool)options.StartSessionImmediately;
            }

            Options heapOptions = new Options(uri, uploadInterval, captureAdvertiserId, Options.DefaultDisableInteractionTextCapture, startSessionImmediately);
            CoreHeap.StartRecording(context, environmentId, heapOptions);
        }

        public void StopRecording()
        {
            CoreHeap.StopRecording();
        }

        public void Track(string trackEvent, Dictionary<string, string> properties)
        {
            CoreHeap.Track(trackEvent, TransformPropertyDictionary(properties), new Java.Util.Date(), SourceInfo);
        }

        public void Identify(string identity)
        {
            CoreHeap.Identify(identity);
        }

        public void ResetIdentity()
        {
            CoreHeap.ResetIdentity();
        }

        public void AddEventProperties(Dictionary<string, string> properties)
        {
            CoreHeap.AddEventProperties(TransformPropertyDictionary(properties));
        }

        public void AddUserProperties(Dictionary<string, string> properties)
        {
            CoreHeap.AddUserProperties(TransformPropertyDictionary(properties));
        }

        public void ClearEventProperties()
        {
            CoreHeap.ClearEventProperties();
        }

        public void RemoveEventProperty(string name)
        {
            CoreHeap.RemoveEventProperty(name);
        }

        public string? FetchSessionId()
        {
            return CoreHeap.FetchSessionId();
        }

        internal static CoreLogLevel HeapLogLevelToAndroidLogLevel(HeapLogLevel heapLogLevel)
        {
            switch (heapLogLevel)
            {
                case HeapLogLevel.None: return CoreLogLevel.None;
                case HeapLogLevel.Error: return CoreLogLevel.Error;
                case HeapLogLevel.Warn: return CoreLogLevel.Warn;
                case HeapLogLevel.Info: return CoreLogLevel.Info;
                case HeapLogLevel.Debug: return CoreLogLevel.Debug;
                case HeapLogLevel.Trace: return CoreLogLevel.Trace;
                default: return CoreLogLevel.Info;
            }
        }

        private static Dictionary<string, Java.Lang.Object> TransformPropertyDictionary(Dictionary<string, string> source)
        {
            var newDict = new Dictionary<string, Java.Lang.Object>();
            foreach (var pair in source) {
                var value = pair.Value;
                if (value is not null)
                {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                    var javaValue = (Java.Lang.Object)value;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                    if (javaValue is not null)
                    {
                        _ = newDict.TryAdd(pair.Key, javaValue);
                    }
                }
            }

            return newDict;
        }
    }
}
#nullable disable
