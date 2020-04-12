using KeyboardOS.Entities;
using KeyboardOS.Framework.Views.BasicViews;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace KeyboardOS.Apps.System
{
    public class NotificationPushedApp : KeyboardApp
    {
        public NotificationPushedApp(OSInstance instance) : base(instance)
        {
            data = new NotificationEvent
            {
                app_name = "Discord",
                title = "RomanPort",
                subtext = "No matter how many times I deactivate McAfee and refuse to subscribe, it still insists on emerging from the depths and scanning every download I make",
                icon_url = "https://cdn.discordapp.com/avatars/235392678920060928/f7f0075ee119e53fa6d03bb1e322cfbc.png"
            };
        }

        public NotificationEvent data;

        public VScrollView scroll;
        public TextBox title;
        public TextBox subtitle;

        public override Image<Rgba32> GetAppIcon()
        {
            return LoadImageFromAsset("default_image", "png");
        }

        public override string GetAppName()
        {
            return "Notification";
        }

        public override void OnAppClosed()
        {
            
        }

        public override void OnAppLaunched()
        {
            //Read and convert image
            byte[] iconRaw = new WebClient().DownloadData(data.icon_url);
            Image<Rgba32> img = Image.Load(iconRaw);

            //Downscale image
            img.Mutate(x => x.Resize(28, 28, new SixLabors.ImageSharp.Processing.Processors.Transforms.NearestNeighborResampler()));

            //Convert to 1-bit
            OutlinedImageBox box = new OutlinedImageBox(1, 1, 28, 28, this.appFrame);
            for(int x = 0; x<img.Width; x++)
            {
                for(int y = 0; y<img.Height; y++)
                {
                    float brightness = ((float)img[x, y].R + (float)img[x, y].G + (float)img[x, y].B) / 3f / byte.MaxValue;
                    if (brightness > 0.65f)
                        box.buffer[x, y] = 1;
                    else
                        box.buffer[x, y] = 0;
                }
            }

            //Add text
            scroll = new VScrollView(31, 0, appFrame.width - 31, appFrame.height, appFrame);
            title = new TextBox(0, 2, appFrame.width - 32, 8, scroll, data.title, TextBox.TextFormatting.Left);
            subtitle = new TextBox(0, 12, appFrame.width - 32, 8, scroll, data.subtext, TextBox.TextFormatting.Left);

            //Set title
            this.status.title = data.app_name;
        }

        public override void OnAppTicked()
        {
            
        }

        public override void DrawView()
        {
            base.DrawView();

            //Draw line below title
            for (int i = 1; i < scroll.width - 1; i += 1)
                scroll.WritePixel(i, 10, 1);
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
            if (key == Framework.InputKey.Up)
                scroll.ScrollBy(8);
            if (key == Framework.InputKey.Down)
                scroll.ScrollBy(-8);
            if (key == Framework.InputKey.Back)
                CloseApp();
        }
    }
}
