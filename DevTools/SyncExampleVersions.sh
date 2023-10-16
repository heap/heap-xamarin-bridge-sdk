#!/bin/bash
# DevTools/SyncExampleVersions.sh - Updates package references in the example projects to match the nuspec files.

set -o errexit

function realpath {
    [[ $1 = /* ]] && echo "$1" || echo "${PWD}/${1#./}"
}

SCRIPT_DIR="$(dirname "$(realpath "$0")")"
ROOT_DIR="${SCRIPT_DIR}/.."

WRAPPER_VERSION=$(xpath -q -e '/package/metadata/version/text()' "${ROOT_DIR}/HeapInc.Xamarin/HeapInc.Xamarin.nuspec")
ANDROID_VERSION=$(xpath -q -e '/package/metadata/version/text()' "${ROOT_DIR}/HeapInc.Xamarin.Android/HeapInc.Xamarin.Android.nuspec")
IOS_VERSION=$(xpath -q -e '/package/metadata/version/text()' "${ROOT_DIR}/HeapInc.Xamarin.iOS/HeapInc.Xamarin.iOS.nuspec")

ANDROID_CSPROJ=${ROOT_DIR}/Android-Example/Android-Example.csproj
IOS_CSPROJ=${ROOT_DIR}/iOS-Example/iOS-Example.csproj

"${SCRIPT_DIR}/SetPackageVersion.sh" HeapInc.Xamarin         "${WRAPPER_VERSION}" "${ANDROID_CSPROJ}"
"${SCRIPT_DIR}/SetPackageVersion.sh" HeapInc.Xamarin.Android "${ANDROID_VERSION}" "${ANDROID_CSPROJ}"
"${SCRIPT_DIR}/SetPackageVersion.sh" HeapInc.Xamarin         "${WRAPPER_VERSION}" "${IOS_CSPROJ}"
"${SCRIPT_DIR}/SetPackageVersion.sh" HeapInc.Xamarin.iOS     "${IOS_VERSION}"     "${IOS_CSPROJ}"
