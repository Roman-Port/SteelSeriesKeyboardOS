using KeyboardOS.Apps.Games.FlappyBird;
using KeyboardOS.Framework.Views;
using KeyboardOS.Framework.Views.BasicViews;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyboardOS.Apps.System
{
    public class LauncherApp : KeyboardApp
    {
        public LauncherApp(OSInstance instance) : base(instance)
        {
            status.title = "Launcher";
        }

        public KeyboardApp[] apps;
        public int index = 0;

        public override Image<Rgba32> GetAppIcon()
        {
            return LoadImageFromAsset("default_image", "png");
        }

        public override string GetAppName()
        {
            return "Keyboard OS Launcher";
        }

        public override void OnAppClosed()
        {
            
        }

        public override void OnAppLaunched()
        {
            //Set up app list
            apps = new KeyboardApp[]
            {
                new FlappyBirdApp(context),
                new RandomPixelsApp(context),
                new NotificationPushedApp(context),
                new LauncherApp(context),
                new NotificationPushedApp(context),
                new LauncherApp(context),
                new RandomPixelsApp(context)
            };

            //Create
            SetAppIndex(0);
        }

        private void Context_KeyboardOSInputEvent(Framework.InputKey key)
        {
            if (key == Framework.InputKey.Up && index < apps.Length - 1)
            {
                index++;
                SetAppIndex(index);
            }
            if (key == Framework.InputKey.Down && index >= 1)
            {
                index--;
                SetAppIndex(index);
            }

            if(key == Framework.InputKey.Enter)
            {
                //Launch
                context.LaunchApp(apps[index]);
            }
        }

        public override void OnAppTicked()
        {
            
        }

        public override void DrawView()
        {
            base.DrawView();
        }

        public void SetAppIndex(int index)
        {
            //Clear
            appFrame.RemoveAllChildren();
            
            //Set title
            status.title = $"Apps {index + 1}/{apps.Length}";
            
            //Draw previous app icon, if any
            if(index > 0)
                new ImageBox(4, 7, 16, 16, appFrame).SetImage(apps[index-1].GetAppIcon());

            //Draw current app frame
            var app = apps[index];
            string name = app.GetAppName();
            InnerView v = new InnerView(24, 0, (name.Length * 8) + 28, 30, appFrame);
            v.effect_invert = true;
            new ImageBox(4, 7, 16, 16, v).SetImage(app.GetAppIcon());
            new TextBox(24, 11, name.Length * 8, 8, v,name, TextBox.TextFormatting.Left);

            //Draw remaining icons
            int offset = (name.Length * 8) + 58;
            for(int i = index+1; i<apps.Length; i+=1)
            {
                new ImageBox(offset, 7, 16, 16, appFrame).SetImage(apps[i].GetAppIcon());
            }
        }

        public override void OnAppSuspended()
        {
            context.KeyboardOSInputEvent -= Context_KeyboardOSInputEvent;
        }

        public override void OnAppResumed()
        {
            context.KeyboardOSInputEvent += Context_KeyboardOSInputEvent;
        }
    }
}
