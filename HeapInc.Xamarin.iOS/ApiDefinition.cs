using Foundation;
using ObjCRuntime;
using WebKit;
using System.Collections.Generic;

namespace HeapInc.Xamarin.iOS
{
    // @interface HeapLogger : NSObject
    [BaseType(typeof(NSObject), Name = "_TtC23HeapSwiftCoreInterfaces10HeapLogger")]
    [Internal]
    interface HeapLogger
    {
        // @property (readonly, nonatomic, strong, class) HeapLogger * _Nonnull sharedInstance;
        [Static]
        [Export("sharedInstance", ArgumentSemantic.Strong)]
        HeapLogger SharedInstance { get; }

        // @property (nonatomic) enum HeapLogLevel logLevel;
        [Export("logLevel", ArgumentSemantic.Assign)]
        HeapLogLevel LogLevel { get; set; }
    }

    // @interface HeapOption
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    [Internal]
    interface HeapOption : INativeObject
    {
        // -(id)isEqual:(id _Nullable)object __attribute__((warn_unused_result("")));
        [Export("isEqual:")]
        NSObject IsEqual([NullAllowed] NSObject @object);

        // @property (readonly, nonatomic) int hash;
        [Export("hash")]
        int Hash { get; }

        // +(HeapOption * _Nonnull)registerWithName:(NSString * _Nonnull)name type:(enum OptionType)type __attribute__((warn_unused_result("")));
        [Static]
        [Export("registerWithName:type:")]
        HeapOption RegisterWithName(string name, OptionType type);

        // +(HeapOption * _Nullable)named:(NSString * _Nonnull)name __attribute__((warn_unused_result("")));
        [Static]
        [Export("named:")]
        [return: NullAllowed]
        HeapOption Named(string name);

        // @property (readonly, nonatomic, strong, class) HeapOption * _Nonnull uploadInterval;
        [Static]
        [Export("uploadInterval", ArgumentSemantic.Strong)]
        HeapOption UploadInterval { get; }

        // @property (readonly, nonatomic, strong, class) HeapOption * _Nonnull baseUrl;
        [Static]
        [Export("baseUrl", ArgumentSemantic.Strong)]
        HeapOption BaseUrl { get; }

        // @property (readonly, nonatomic, strong, class) HeapOption * _Nonnull messageBatchByteLimit;
        [Static]
        [Export("messageBatchByteLimit", ArgumentSemantic.Strong)]
        HeapOption MessageBatchByteLimit { get; }

        // @property (readonly, nonatomic, strong, class) HeapOption * _Nonnull messageBatchMessageLimit;
        [Static]
        [Export("messageBatchMessageLimit", ArgumentSemantic.Strong)]
        HeapOption MessageBatchMessageLimit { get; }

        // @property (readonly, nonatomic, strong, class) HeapOption * _Nonnull captureAdvertiserId;
        [Static]
        [Export("captureAdvertiserId", ArgumentSemantic.Strong)]
        HeapOption CaptureAdvertiserId { get; }

        // @property (readonly, nonatomic, strong, class) HeapOption * _Nonnull startSessionImmediately;
        [Static]
        [Export("startSessionImmediately", ArgumentSemantic.Strong)]
        HeapOption StartSessionImmediately { get; }

        // @property (readonly, nonatomic, strong, class) HeapOption * _Nonnull disablePageviewAutocapture;
        [Static]
        [Export("disablePageviewAutocapture", ArgumentSemantic.Strong)]
        HeapOption DisablePageviewAutocapture { get; }

        // @property (readonly, nonatomic, strong, class) HeapOption * _Nonnull disablePageviewTitleCapture;
        [Static]
        [Export("disablePageviewTitleCapture", ArgumentSemantic.Strong)]
        HeapOption DisablePageviewTitleCapture { get; }

        // @property (readonly, nonatomic, strong, class) HeapOption * _Nonnull disableInteractionAutocapture;
        [Static]
        [Export("disableInteractionAutocapture", ArgumentSemantic.Strong)]
        HeapOption DisableInteractionAutocapture { get; }

        // @property (readonly, nonatomic, strong, class) HeapOption * _Nonnull disableInteractionTextCapture;
        [Static]
        [Export("disableInteractionTextCapture", ArgumentSemantic.Strong)]
        HeapOption DisableInteractionTextCapture { get; }

        // @property (readonly, nonatomic, strong, class) HeapOption * _Nonnull disableInteractionAccessibilityLabelCapture;
        [Static]
        [Export("disableInteractionAccessibilityLabelCapture", ArgumentSemantic.Strong)]
        HeapOption DisableInteractionAccessibilityLabelCapture { get; }

        // @property (readonly, nonatomic, strong, class) HeapOption * _Nonnull disableInteractionReferencingPropertyCapture;
        [Static]
        [Export("disableInteractionReferencingPropertyCapture", ArgumentSemantic.Strong)]
        HeapOption DisableInteractionReferencingPropertyCapture { get; }

        // @property (readonly, nonatomic, strong, class) HeapOption * _Nonnull interactionHierarchyCaptureLimit;
        [Static]
        [Export("interactionHierarchyCaptureLimit", ArgumentSemantic.Strong)]
        HeapOption InteractionHierarchyCaptureLimit { get; }
    }

    // @interface HeapSourceInfo
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject))]
    [Internal]
    interface HeapSourceInfo
    {
        // @property (readonly, copy, nonatomic) NSString * _Nonnull name;
        [Export ("name")]
        string Name { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nonnull version;
        [Export ("version")]
        string Version { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nonnull platform;
        [Export ("platform")]
        string Platform { get; }

        // +(HeapSourceInfo * _Nonnull)sourceInfoWithName:(NSString * _Nonnull)name version:(NSString * _Nonnull)version platform:(NSString * _Nonnull)platform __attribute__((warn_unused_result("")));
        [Static]
        [Export ("sourceInfoWithName:version:platform:")]
        HeapSourceInfo SourceInfo (string name, string version, string platform);
    }

    // @interface Heap
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject), Name = "_TtC13HeapSwiftCore4Heap")]
    [Internal]
    interface Heap
    {
        // @property (nonatomic, strong, class) Heap * _Nonnull sharedInstance;
        [Static]
        [Export("sharedInstance", ArgumentSemantic.Strong)]
        Heap SharedInstance { get; }

        // @property (nonatomic) enum HeapLogLevel logLevel;
        [Export("logLevel", ArgumentSemantic.Assign)]
        HeapLogLevel LogLevel { get; set; }

        // -(void)startRecording:(NSString * _Nonnull)environmentId;
        [Export("startRecording:")]
        void StartRecording(string environmentId);

        // -(void)startRecording:(NSString * _Nonnull)environmentId withOptions:(id)options;
        [Export("startRecording:withOptions:")]
        void StartRecording(string environmentId, NSDictionary options);

        // -(void)stopRecording;
        [Export("stopRecording")]
        void StopRecording();

        // -(void)track:(NSString * _Nonnull)event properties:(id)properties sourceInfo:(HeapSourceInfo * _Nullable)sourceInfo;
        [Export ("track:properties:sourceInfo:")]
        void Track (string @event, NSDictionary properties, [NullAllowed] HeapSourceInfo sourceInfo);

        // -(void)identify:(NSString * _Nonnull)identity;
        [Export("identify:")]
        void Identify(string identity);

        // -(void)resetIdentity;
        [Export("resetIdentity")]
        void ResetIdentity();

        // -(void)addUserProperties:(id)properties;
        [Export("addUserProperties:")]
        void AddUserProperties(NSDictionary properties);

        // -(void)addEventProperties:(id)properties;
        [Export("addEventProperties:")]
        void AddEventProperties(NSDictionary properties);

        // -(void)removeEventProperty:(NSString * _Nonnull)name;
        [Export("removeEventProperty:")]
        void RemoveEventProperty(string name);

        // -(void)clearEventProperties;
        [Export("clearEventProperties")]
        void ClearEventProperties();

        // @property (readonly, copy, nonatomic) NSString * _Nullable userId;
        [NullAllowed, Export("userId")]
        string UserId { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable identity;
        [NullAllowed, Export("identity")]
        string Identity { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable sessionId;
        [NullAllowed, Export("sessionId")]
        string SessionId { get; }

        // -(NSString * _Nullable)fetchSessionId __attribute__((warn_unused_result("")));
        [Export("fetchSessionId")]
        string FetchSessionId();

        // -(void)attachWebView:(WKWebView * _Nonnull)webView origins:(id)origins injectHeapJavaScript:(id)injectHeapJavaScript;
        [Export("attachWebView:origins:injectHeapJavaScript:")]
        void AttachWebView(WKWebView webView, NSObject origins, NSObject injectHeapJavaScript);

        // -(void)attachWebView:(WKWebView * _Nonnull)webView origins:(id)origins;
        [Export("attachWebView:origins:")]
        void AttachWebView(WKWebView webView, NSObject origins);
    }

    [Protocol(Name = "_TtP13HeapSwiftCore21HeapObjcPropertyValue_")]
    [Internal]
    interface HeapObjcPropertyValue
    {
        // @required @property (readonly, copy, nonatomic) NSString * _Nonnull heapValue;
        [Abstract]
        [Export("heapValue")]
        string HeapValue { get; }
    }

    // @interface HeapSwiftCore_Swift_302
    [Internal]
    interface HeapSwiftCore_Swift_302
    {
        // @property (readonly, copy, nonatomic) NSString * _Nonnull heapValue;
        [Export("heapValue")]
        string HeapValue { get; }
    }
}
