## Device Orientation Plugin for Xamarin

This is a small fork of [aliozgur](https://github.com/aliozgur)'s device orientation plugin that replaces the dependency on Xamarin.Forms with Rx - suitable for non-forms projects.

`DeviceOrientation` includes a `OrientationChanges` property of type `IObservable<DeviceOrientationChangeMessage>` that fires on orientation changes.
