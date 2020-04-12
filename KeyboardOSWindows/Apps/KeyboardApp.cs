using KeyboardOS.Framework.Views;
using KeyboardOS.Framework.Views.SystemViews;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KeyboardOS.Apps
{
    public abstract class KeyboardApp : View
    {
        public OSInstance context;

        public StatusBar status;
        public InnerView appFrame;
        public bool suspended;

        public KeyboardApp(OSInstance instance) : base(0, 0, 128, 40, instance.frame, false)
        {
            context = instance;
            status = new StatusBar(0, 0, context.frame.width, 10, this);
            status.title = "";
            appFrame = new InnerView(0, 10, 128, 30, this);
        }

        public void DisableStatusBar()
        {
            status.hidden = true;
            appFrame.height = 40;
            appFrame.y = 0;
        }

        public abstract void OnAppLaunched();
        public abstract void OnAppTicked();
        public abstract void OnAppClosed();
        public abstract void OnAppSuspended();
        public abstract void OnAppResumed();

        public abstract string GetAppName();
        public abstract Image<Rgba32> GetAppIcon();

        public override void DrawView()
        {
            if (y > 3)
                y -= 3;
            base.DrawView();
        }

        public override void WritePixel(int x, int y, int value)
        {
            base.WritePixel(x, y, value);
        }

        public Image<Rgba32> LoadImageFromAsset(string name, string type)
        {
            return Image.Load<Rgba32>(File.ReadAllBytes("Assets\\" + name + "." + type));
        }

        public void CloseApp()
        {
            context.CloseApp(this);
        }
    }
}
