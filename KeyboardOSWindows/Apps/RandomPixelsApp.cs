using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyboardOS.Apps
{
    public class RandomPixelsApp : KeyboardApp
    {
        public RandomPixelsApp(OSInstance instance) : base(instance)
        {

        }

        public override void OnAppClosed()
        {
            
        }

        public override void OnAppLaunched()
        {
            
        }

        public override void OnAppTicked()
        {
            
        }

        public override void DrawView()
        {
            base.DrawView();
            Random rand = new Random();
            for (int x = 0; x<appFrame.width; x++)
            {
                for (int y = 0; y < appFrame.height; y++)
                {
                    appFrame.WritePixel(x, y, rand.Next(0, 2));
                }
            }
        }

        public override string GetAppName()
        {
            return "Pixels";
        }

        public override Image<Rgba32> GetAppIcon()
        {
            return LoadImageFromAsset("default_image", "png");
        }

        public override void OnAppSuspended()
        {
            context.KeyboardOSInputEvent -= Context_KeyboardOSInputEvent;
        }

        public override void OnAppResumed()
        {
            context.KeyboardOSInputEvent += Context_KeyboardOSInputEvent;
        }

        private void Context_KeyboardOSInputEvent(Framework.InputKey key)
        {
            if (key == Framework.InputKey.Back)
                CloseApp();
        }
    }
}
