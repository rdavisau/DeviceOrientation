using System;

namespace DeviceOrientation.Plugin.Abstractions
{
    /// <summary>
    /// DeviceOrientation Interface
    /// </summary>
    public interface IDeviceOrientation
    {
        /// <summary>
        /// Gets the orientation.
        /// </summary>
        /// <returns>The orientation.</returns>
        DeviceOrientations GetOrientation();

        /// <summary>
        /// An observable that fires a <code>DeviceOrientationChangeMessage</code> when the device orientation changes.
        /// </summary>
        IObservable<DeviceOrientationChangeMessage> OrientationChanges { get; }
    }
}
