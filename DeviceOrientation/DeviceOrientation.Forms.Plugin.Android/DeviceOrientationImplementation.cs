using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Runtime;
using Android.Views;
using DeviceOrientation.Plugin.Abstractions;

namespace DeviceOrientation.Plugin.Droid
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
        /// <param name="newConfig">New configuration</param>
        public static void NotifyOrientationChange(Configuration newConfig)
        {
            bool isLandscape = newConfig.Orientation == Orientation.Landscape;
            var msg = new DeviceOrientationChangeMessage()
            {
                Orientation = isLandscape ? DeviceOrientations.Landscape : DeviceOrientations.Portrait
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
            IWindowManager windowManager = Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            var rotation = windowManager.DefaultDisplay.Rotation;
            bool isLandscape = rotation == SurfaceOrientation.Rotation90 || rotation == SurfaceOrientation.Rotation270;
            return isLandscape ? DeviceOrientations.Landscape : DeviceOrientations.Portrait;
        }

        /// <summary>
        /// An observable that fires a <code>DeviceOrientationChangeMessage</code> when the device orientation changes.
        /// </summary>
        public IObservable<DeviceOrientationChangeMessage> OrientationChanges { get { return _orientationChanges.AsObservable(); } }
        private static readonly Subject<DeviceOrientationChangeMessage> _orientationChanges = new Subject<DeviceOrientationChangeMessage>(); 

        #endregion
    }
}
