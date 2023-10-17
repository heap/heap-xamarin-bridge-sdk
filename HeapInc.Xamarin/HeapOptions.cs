using System;
namespace HeapInc.Xamarin
{
    public class HeapOptions
    {
        public Uri? BaseUri { get; set; }
        public TimeSpan? UploadInterval { get; set; }
        public bool CaptureAdvertiserId { get; set; }
        public bool StartSessionImmediately { get; set; }
        public bool CaptureVendorId { get; set; } // CaptureVendorId Option is not yet implemented on Heap Android Core 0.4.0. Heap.Android will ignore this option. 
    }
}

