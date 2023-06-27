# Heap Xamarin Bridge Release Guide

## Updating HeapSwiftCore

When a new version of HeapSwiftCore is released, a new version of `HeapSwiftCoreInterfaces.xcframework` and
`HeapSwiftCore-Dynamic.xcframework` need to be added to this project.

1. Go to `https://buildkite.com/heap/repo-heap-swift-core-required/builds?branch={version}` to open the build for the version in question.
2. Open the "Build dynamic framework" build step.
3. Download the artifact and unzip.
4. Replace the xcframework directories in [HeapInc.Xamarin.iOS](HeapInc.Xamarin.iOS/) with those from the zip.
5. Update [HeapInc.Xamarin.iOS/HeapInc.Xamarin.iOS.nuspec](HeapInc.Xamarin.iOS/HeapInc.Xamarin.iOS.nuspec) and
   [HeapInc.Xamarin.iOS/Properties/AssemblyInfo.cs](HeapInc.Xamarin.iOS/Properties/AssemblyInfo.cs) with the new version. (See note below.)
6. Test that nothing has broken.
7. Create a new PR for the change.

## Updating HeapAndroidCore

When a new version of HeapAndroidCore is released, a new version of `heap-android-core-{version}.arr` need to be added to this project.

1. Go to `https://repo.maven.apache.org/maven2/io/heap/core/heap-android-core/{version}`.
2. Download `heap-android-core-{version}.arr`.
3. Delete the existing `arr` file from [HeapInc.Xamarin.Android/Jars/](HeapInc.Xamarin.Android/Jars/).
4. Add the new `arr` file.
5. Update [HeapInc.Xamarin.Android/HeapInc.Xamarin.Android.csproj](HeapInc.Xamarin.Android/HeapInc.Xamarin.Android.csproj) to reference the
   new `arr` file.
6. Update [HeapInc.Xamarin.Android/HeapInc.Xamarin.Android.nuspec](HeapInc.Xamarin.Android/HeapInc.Xamarin.Android.nuspec) and
   [HeapInc.Xamarin.Android/Properties/AssemblyInfo.cs](HeapInc.Xamarin.Android/Properties/AssemblyInfo.cs) with the new version. (See note
   below.)
7. Test that nothing has broken.
8. Create a new PR for the change.

## Adding a new feature to the bridge

If you ever need to add a new feature to the bridge (autocapture features, a new initialization setting, etc), you will want to take the
following steps.

1. Make sure relevant versions of HeapAndroidCore and HeapSwiftCore have been installed.
2. Add the feature to the bridge SDK, the `IHeap` protocol, and the corresponding implementations.  If working with HeapSwiftCore, you will
   need to manually configure the bindings for new features.
3. Bump the version of [HeapInc.Xamarin/HeapInc.Xamarin.nuspec](HeapInc.Xamarin/HeapInc.Xamarin.nuspec) to the version of the core SDKs that
   introduced the feature. (See note below.)
4. Update [HeapInc.Xamarin.iOS/HeapInc.Xamarin.iOS.nuspec](HeapInc.Xamarin.iOS/HeapInc.Xamarin.iOS.nuspec) and
   [HeapInc.Xamarin.Android/HeapInc.Xamarin.Android.nuspec](HeapInc.Xamarin.Android/HeapInc.Xamarin.Android.nuspec) to use the new version
   as a minimum dependency.
5. Bump the versions of HeapInc.Xamarin.iOS and HeapInc.Xamarin.Android if they were already released.
6. Test that changes.
7. Create a new PR for the change.

## Versioning

Generally speaking, the major and minor version of any project should match the version of the bridged SDK.  So if you're packaging
HeapAndroidCore 0.3.0, you should release HeapInc.Xamarin.Android 0.3.x.  In the absence of hot fixes, the patch version should also match,
so 0.3.1 should be for 0.3.1.  Situations will invariably happen where we need to push a hotfix for a binding library.  In this case, it is
perfectly fine to bump the patch version and have it differ from the core version.

## Creating a release

### Before you begin

In order to release to NuGet, you need an account.  If you do not currently have a Microsoft accounts, reach out in `#it-help` for one and
use that to create a NuGet account at https://www.nuget.org/users/account/LogOn. E.g., `bnickel.heap`.

Ask an existing owner to add you as an owner for the project.

Once added and logged in, you will need an API Key, which you can generate at https://www.nuget.org/account/apikeys.

### Release process

Creating a release is currently a manual process.

Once all the changes that are going into a release have been added and the version has been set, you need to check out main, tag the commit,
and push the tag to the repo.  The pattern is `bridge/{version}` for `HeapInc.Xamarin`, `ios/{version}` for `HeapInc.Xamarin.iOS`, and
`android/{version}` for `HeapInc.Xamarin.Android`.  For example, if I were releasing HeapInc.Xamarin.iOS 0.3.1, I would run the following:

```bash
git checkout main
git pull origin
git tag ios/0.3.1
git push origin ios/0.3.1
```

This will trigger a build on buildkite at `https://buildkite.com/heap/repo-heap-xamarin-bridge-required/builds?branch=foo%2F{version}` with
an artifact `artifacts/HeapInc.Xamarin.iOS.{version}.nupkg`.  Download that file.

Upload using `nuget push NUGET_PKG API_KEY -Source nuget.org`.

After releasing, it's a good idea to do one final test and then announce in `#product-announce` and share to `#eng-mobile-help`.
