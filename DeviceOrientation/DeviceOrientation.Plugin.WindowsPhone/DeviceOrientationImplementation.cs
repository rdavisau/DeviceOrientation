using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using DeviceOrientation.Plugin.Abstractions;
using Microsoft.Phone.Controls;

namespace DeviceOrientation.Plugin.WindowsPhone
{
    /// <summary>
    /// DeviceOrientation Implementation
    /// </summary>
    public class DeviceOrientationImplementation : IDeviceOrientation
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() 
        {
        }

        /// <summary>
        /// Send orientation change message through MessagingCenter
        /// </summary>
        /// <param name="e">Orientation changed event args</param>
        public static void NotifyOrientationChange(OrientationChangedEventArgs e)
        {
            bool isLandscape = (e.Orientation & PageOrientation.Landscape) == PageOrientation.Landscape;
            var msg = new DeviceOrientationChangeMessage()
            {
                Orientation = isLandscape ? DeviceOrientations.Landscape : DeviceOrientations.Portrait
            };
            _orientationChanges.OnNext(msg);
        }

        /// <summary>
        /// Gets the orientation.
        /// </summary>
        /// <returns>The orientation.</returns>
        public DeviceOrientations GetOrientation()
        {
            var rootFrame = (System.Windows.Application.Current.RootVisual as PhoneApplicationFrame);
            if (rootFrame == null)
                return DeviceOrientations.Undefined;

            PageOrientation currentOrientation = (System.Windows.Application.Current.RootVisual as PhoneApplicationFrame).Orientation;
            bool isLandscape = (currentOrientation & PageOrientation.Landscape) == PageOrientation.Landscape;
            return isLandscape ? DeviceOrientations.Landscape : DeviceOrientations.Portrait;
           
           
        }

        /// <summary>
        /// An observable that fires a <code>DeviceOrientationChangeMessage</code> when the device orientation changes.
        /// </summary>
        public IObservable<DeviceOrientationChangeMessage> OrientationChanges { get { return _orientationChanges.AsObservable(); } }
        private static readonly Subject<DeviceOrientationChangeMessage> _orientationChanges = new Subject<DeviceOrientationChangeMessage>(); 

    }
}
