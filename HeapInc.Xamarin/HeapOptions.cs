using System;
namespace HeapInc.Xamarin
{
    public class HeapOptions
    {
        public Uri? BaseUri { get; set; }
        public TimeSpan? UploadInterval { get; set; }
        public bool CaptureAdvertiserId { get; set; }
        public bool StartSessionImmediately { get; set; }
    }
}

