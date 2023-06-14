using System;
using System.Collections.Generic;
namespace HeapInc.Xamarin
{
    public interface IHeap
    {
        string? SessionId { get; }
        string? UserId { get; }
        string? Identity { get; }

        void StopRecording();
        void Track(string eventName, Dictionary<string, string> properties);
        void Identify(string identity);
        void ResetIdentity();
        void AddUserProperties(Dictionary<string, string> properties);
        void AddEventProperties(Dictionary<string, string> properties);
        void RemoveEventProperty(string name);
        void ClearEventProperties();
        void SetLogLevel(HeapLogLevel logLevel);
        string? FetchSessionId();
    }
}

