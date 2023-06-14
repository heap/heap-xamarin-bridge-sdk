using System;
using System.Collections.Generic;

namespace HeapInc.Xamarin
{
    public static class Heap
    {
        public static string? Identity => Implementation?.Identity;

        public static string? SessionId => Implementation?.SessionId;

        public static string? UserId => Implementation?.UserId;

        static IHeap? Implementation;


        public static void SetImplementation(IHeap implementation)
        {
            Implementation = implementation;
        }


        public static void StopRecording()
        {
            var implementation = Implementation;
            if (implementation is not null)
            {
                implementation.StopRecording();
            }
        }

        public static void Track(string eventName) => Track(eventName, new Dictionary<string, string>());

        public static void Track(string eventName, Dictionary<string, string> properties)
        {
            var implementation = Implementation;
            if (eventName is null)
            {
                Console.WriteLine("Track failed because the eventName was null");
                return;
            }

            if (properties is null)
            {
                properties = new Dictionary<string, string>();
            }

            if (implementation is not null)
            {
                implementation.Track(eventName, properties);
            }
            else
            {
                Console.WriteLine("Track failed because the implementation was not set");
            }
        }


        public static void Identify(string identity)
        {
            var implementation = Implementation;
            if (identity is null)
            {
                Console.WriteLine("Identify failed because the identity was null");
                return;
            }

            if (implementation is not null)
            {
                implementation.Identify(identity);
            }
            else
            {
                Console.WriteLine("Identify failed because the implementation was not set");
            }
        }

        public static void ResetIdentity()
        {
            var implementation = Implementation;
            if (implementation is not null)
            {
                implementation.ResetIdentity();
            }
            else
            {
                Console.WriteLine("ResetIdentity failed because the implementation was not set");
            }
        }

        public static void AddEventProperties(Dictionary<string, string> properties)
        {
            var implementation = Implementation;
            if (properties is null)
            {
                Console.WriteLine("AddEventProperties failed because the properties was null");
                return;
            }

            if (implementation is not null)
            {
                implementation.AddEventProperties(properties);
            }
            else
            {
                Console.WriteLine("AddEventProperties failed because the implementation was not set");
            }
        }

        public static void AddUserProperties(Dictionary<string, string> properties)
        {
            var implementation = Implementation;
            if (properties is null)
            {
                Console.WriteLine("AddUserProperties failed because the properties was null");
                return;
            }

            if (implementation is not null)
            {
                implementation.AddUserProperties(properties);
            }
            else
            {
                Console.WriteLine("AddUserProperties failed because the implementation was not set");
            }
        }

        public static void RemoveEventProperty(string name)
        {
            var implementation = Implementation;
            if (name is null)
            {
                Console.WriteLine("RemoveEventProperty failed because the name was null");
                return;
            }

            if (implementation is not null)
            {
                implementation.RemoveEventProperty(name);
            }
            else
            {
                Console.WriteLine("RemoveEventProperty failed because the implementation was not set");
            }
        }

        public static void ClearEventProperties()
        {
            var implementation = Implementation;
            if (implementation is not null)
            {
                implementation.ClearEventProperties();
            }
            else
            {
                Console.WriteLine("ClearEventProperties failed because the implementation was not set");
            }
        }

        public static string? FetchSessionId()
        {
            var implementation = Implementation;
            if (implementation is not null)
            {
                return implementation.FetchSessionId();
            }
            else
            {
                Console.WriteLine("FetchSessionId failed because the implementation was not set");
            }
            return null;
        }

    }
}


