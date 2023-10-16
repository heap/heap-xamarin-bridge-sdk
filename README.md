# Heap Xamarin SDK

This repo contains the source for [`HeapInc.Xamarin.Android`](https://www.nuget.org/packages/HeapInc.Xamarin.Android) and
[`HeapInc.Xamarin.iOS`](https://www.nuget.org/packages/HeapInc.Xamarin.iOS), the official Xamarin SDKs for [Heap](https://heap.io).

## Installation and documentation

- [Installation instructions](https://developers.heap.io/docs/xamarin-quick-start)
- [API documentation](https://developers.heap.io/docs/xamarin-sdk-reference)

## Running the examples

The example in `examples.sln` depend on nuget packages rather than local projects.
Before running and after making changes in the SDKs, run `make restore_examples`
to build and install local nuget packages.
