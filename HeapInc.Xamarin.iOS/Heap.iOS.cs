using System;
using System.Collections.Generic;
using Foundation;

#nullable enable
namespace HeapInc.Xamarin.iOS
{
    public static class Implementation
    {
        public static void StartRecording(string environmentId)
        {
            StartRecording(environmentId, new HeapOptions());
        }

        public static void StartRecording(string environmentId, HeapOptions options)
        {
            var implementation = HeapIOS.Instance;
            HeapInc.Xamarin.Heap.SetImplementation(implementation);
            implementation.StartRecording(environmentId, options);
        }

        public static void SetLogLevel(HeapLogLevel logLevel)
        {
            Heap.SharedInstance.LogLevel = logLevel;
        }
    }

    class HeapIOS : HeapInc.Xamarin.IHeap
    {
        private static readonly HeapIOS instance = new HeapIOS();

        public static HeapIOS Instance
        {
            get
            {
                return instance;
            }
        }

        private HeapSourceInfo SourceInfo { get; }

        protected HeapIOS()
        {
            var version = typeof(HeapIOS).Assembly.GetName().Version.ToString();
            SourceInfo = HeapSourceInfo.SourceInfo("xamarin_bridge", version, "Xamarin.iOS");
        }

        public string? SessionId => Heap.SharedInstance.SessionId;
        public string? UserId => Heap.SharedInstance.UserId;
        public string? Identity => Heap.SharedInstance.Identity;

        public void StartRecording(string environmentId)
        {
            Heap.SharedInstance.StartRecording(environmentId);
        }

        public void StartRecording(string environmentId, HeapOptions options)
        {
            NSMutableDictionary heapOptions = new NSMutableDictionary();
            if (options.BaseUri is not null)
            {
                heapOptions.Add(HeapOption.BaseUrl, NSObject.FromObject(options.BaseUri.AbsoluteUri));
            }

            if (options.UploadInterval is not null)
            {
                
                heapOptions.Add(HeapOption.UploadInterval, NSObject.FromObject(options.UploadInterval.Value.TotalSeconds));
            }

            if (options.CaptureAdvertiserId)
            {
                heapOptions.Add(HeapOption.CaptureAdvertiserId, NSObject.FromObject(options.CaptureAdvertiserId));
            }

            if (options.StartSessionImmediately)
            {
                heapOptions.Add(HeapOption.StartSessionImmediately, NSObject.FromObject(options.StartSessionImmediately));
            }

            Heap.SharedInstance.StartRecording(environmentId, heapOptions);
        }

        public void StopRecording()
        {
            Heap.SharedInstance.StopRecording();
        }

        public void Track(string trackEvent, Dictionary<string, string> properties)
        {
            Heap.SharedInstance.Track(trackEvent, DictionaryToNSDictionary(properties), SourceInfo);
        }

        public void Identify(string identity)
        {
            Heap.SharedInstance.Identify(identity);
        }

        public void ResetIdentity()
        {
            Heap.SharedInstance.ResetIdentity();
        }

        public void AddEventProperties(Dictionary<string, string> properties)
        {
            Heap.SharedInstance.AddEventProperties(DictionaryToNSDictionary(properties));
        }

        public void AddUserProperties(Dictionary<string, string> properties)
        {
            Heap.SharedInstance.AddUserProperties(DictionaryToNSDictionary(properties));
        }

        public void ClearEventProperties()
        {
            Heap.SharedInstance.ClearEventProperties();
        }

        public void RemoveEventProperty(string name)
        {
            Heap.SharedInstance.RemoveEventProperty(name);
        }

        public string? FetchSessionId()
        {
            return Heap.SharedInstance.FetchSessionId();
        }

        private static NSDictionary DictionaryToNSDictionary(Dictionary<string, string> dictionary)
        {
            string[] objects = new string[dictionary.Count];
            dictionary.Values.CopyTo(objects, 0);

            string[] keys = new string[dictionary.Count];
            dictionary.Keys.CopyTo(keys, 0);

            return NSDictionary.FromObjectsAndKeys(objects, keys);
        }
    }
}
#nullable disable

