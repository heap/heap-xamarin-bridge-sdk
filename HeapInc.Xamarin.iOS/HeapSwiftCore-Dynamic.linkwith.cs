using ObjCRuntime;

[assembly: LinkWith("HeapSwiftCore-Dynamic.xcframework",
             ForceLoad = true,
             Frameworks = "Foundation",
             IsCxx = true,
             SmartLink = true)]

[assembly: LinkWith("HeapSwiftCoreInterfaces.xcframework",
             ForceLoad = true,
             Frameworks = "Foundation",
             IsCxx = true,
             SmartLink = true)]

