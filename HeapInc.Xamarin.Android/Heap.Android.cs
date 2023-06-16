using System;
using System.Collections;
using System.Collections.Generic;

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
            HeapInc.Xamarin.Heap.SetImplementation(implementation);
        }

        public static void SetLogLevel(HeapLogLevel logLevel)
        {
            Heap.SetLogLevel(HeapAndroid.HeapLogLevelToAndroidLogLevel(logLevel));
        }
    }

    public class HeapAndroid : HeapInc.Xamarin.IHeap
    {
        private static readonly HeapAndroid instance = new HeapAndroid();

        public static HeapAndroid Instance
        {
            get
            {
                return instance;
            }
        }

        public string? SessionId => Heap.SessionId;
        public string? UserId => Heap.UserId;
        public string? Identity => Heap.Identity;

        public void StartRecording(global::Android.Content.Context context, string environmentId)
        {
            Heap.StartRecording(context, environmentId);
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
            Heap.StartRecording(context, environmentId, heapOptions);
        }

        public void StopRecording()
        {
            Heap.StopRecording();
        }

        public void Track(string trackEvent, Dictionary<string, string> properties)
        {
            Heap.Track(trackEvent, ObjectToJavaObject(properties));
        }

        public void Identify(string identity)
        {
            Heap.Identify(identity);
        }

        public void ResetIdentity()
        {
            Heap.ResetIdentity();
        }

        public void AddEventProperties(Dictionary<string, string> properties)
        {
            Heap.AddEventProperties(ObjectToJavaObject(properties));
        }

        public void AddUserProperties(Dictionary<string, string> properties)
        {
            Heap.AddUserProperties(ObjectToJavaObject(properties));
        }

        public void ClearEventProperties()
        {
            Heap.ClearEventProperties();
        }

        public void RemoveEventProperty(string name)
        {
            Heap.RemoveEventProperty(name);
        }

        public string? FetchSessionId()
        {
            return Heap.FetchSessionId();
        }

        public static IO.Heap.Core.Logs.LogLevel HeapLogLevelToAndroidLogLevel(HeapLogLevel heapLogLevel)
        {
            switch (heapLogLevel)
            {
                case HeapLogLevel.None: return IO.Heap.Core.Logs.LogLevel.None;
                case HeapLogLevel.Error: return IO.Heap.Core.Logs.LogLevel.Error;
                case HeapLogLevel.Warn: return IO.Heap.Core.Logs.LogLevel.Warn;
                case HeapLogLevel.Info: return IO.Heap.Core.Logs.LogLevel.Info;
                case HeapLogLevel.Debug: return IO.Heap.Core.Logs.LogLevel.Debug;
                case HeapLogLevel.Trace: return IO.Heap.Core.Logs.LogLevel.Trace;
                default: return IO.Heap.Core.Logs.LogLevel.Info;
            }
        }

        private static Dictionary<string, Java.Lang.Object> ObjectToJavaObject(object obj)
        {
            if (typeof(IDictionary).IsAssignableFrom(obj.GetType()))
            {
                IDictionary idict = (IDictionary)obj;
                Dictionary<string, Java.Lang.Object> newDict = new Dictionary<string, Java.Lang.Object>();

                foreach (object key in idict.Keys)
                {
                    newDict.Add(key.ToString(), idict[key].ToString());
                }
                return newDict;
            }
            else
            {
                // object is not a dictionary
                Console.WriteLine("The 'properties' object is not a Dictionary");
                return null;
            }
        }
    }
}
#nullable disable
