using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using DeviceOrientation.Plugin.Abstractions;

#if __UNIFIED__
using UIKit;
using Foundation;

#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif

namespace DeviceOrientation.Plugin.iOS
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
            var notificationCenter = NSNotificationCenter.DefaultCenter;
            notificationCenter.AddObserver(UIApplication.DidChangeStatusBarOrientationNotification, DeviceOrientationDidChange);

            UIDevice.CurrentDevice.BeginGeneratingDeviceOrientationNotifications ();

        }

        /// <summary>
        /// Devices the orientation did change.
        /// </summary>
        /// <param name="notification">Notification.</param>
        public static void DeviceOrientationDidChange (NSNotification notification)
        {
            var orientation = UIDevice.CurrentDevice.Orientation;
            bool isPortrait = orientation == UIDeviceOrientation.Portrait 
                || orientation == UIDeviceOrientation.PortraitUpsideDown;
            SendOrientationMessage(isPortrait);
        }

        static void SendOrientationMessage(bool isPortrait)
        {
            var msg = new DeviceOrientationChangeMessage()
                {
                    Orientation = isPortrait ? DeviceOrientations.Portrait : DeviceOrientations.Landscape
                };
            _orientationChanges.OnNext(msg);
        }

        #region IDeviceOrientation implementation

        /// <summary>
        /// Gets the orientation.
        /// </summary>
        /// <returns>The orientation.</returns>
        public DeviceOrientations GetOrientation()
        {
            var currentOrientation = UIApplication.SharedApplication.StatusBarOrientation;
            bool isPortrait = currentOrientation == UIInterfaceOrientation.Portrait 
                || currentOrientation == UIInterfaceOrientation.PortraitUpsideDown;

            return isPortrait ? DeviceOrientations.Portrait: DeviceOrientations.Landscape;
        }

        /// <summary>
        /// An observable that fires a <code>DeviceOrientationChangeMessage</code> when the device orientation changes.
        /// </summary>
        public IObservable<DeviceOrientationChangeMessage> OrientationChanges { get { return _orientationChanges.AsObservable(); } }
        private static readonly Subject<DeviceOrientationChangeMessage> _orientationChanges = new Subject<DeviceOrientationChangeMessage>(); 


        #endregion
    }
}
