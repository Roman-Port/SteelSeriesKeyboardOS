using KeyboardOS.Apps;
using KeyboardOS.Framework;
using KeyboardOS.Framework.Tools;
using KeyboardOS.Framework.Views;
using KeyboardOS.Framework.Views.SystemViews;
using mrousavy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace KeyboardOS
{
    public class OSInstance
    {
        public DisplayFrame frame;
        public KeyboardConnection connection;
        public List<KeyboardApp> apps;
        public Random rand;

        public event KeyboardOSInputArgs KeyboardOSInputEvent;

        private InputKey? queuedKey;
        private int videoFrame = -1; //Used for recording. Set to 0 to begin, -1 to stop

        public OSInstance()
        {
            this.frame = new DisplayFrame(128, 40);
            this.connection = new KeyboardConnection();
            this.connection.Init();
            this.apps = new List<KeyboardApp>();
            this.rand = new Random();

            new Thread(() =>
            {
                Hooky.Init();
            }).Start();
            Hooky.listeners += Hooky_listeners;
        }

        private void Hooky_listeners(System.Windows.Forms.Keys key, bool down)
        {
            //Only act when pushed
            if (!down)
                return;
            
            //Get the key we want
            InputKey t;
            switch(key)
            {
                case System.Windows.Forms.Keys.Divide: t = InputKey.Down; break;
                case System.Windows.Forms.Keys.Multiply: t = InputKey.Up; break;
                case System.Windows.Forms.Keys.Subtract: t = InputKey.Enter; break;
                case System.Windows.Forms.Keys.Add: t = InputKey.Back; break;
                default: return;
            }

            //Send
            queuedKey = t;
        }

        public void LaunchApp(KeyboardApp app)
        {
            //Suspend active app
            if(apps.Count > 0)
            {
                apps[apps.Count - 1].OnAppSuspended();
                apps[apps.Count - 1].hidden = true;
                apps[apps.Count - 1].suspended = true;
            }

            //Start
            frame.children.Add(app);
            apps.Add(app);
            app.OnAppLaunched();
            app.OnAppResumed();
            app.suspended = false;
        }

        public void CloseApp(KeyboardApp app)
        {
            //Get the index
            int index = apps.IndexOf(app);

            //Close it
            if (!app.suspended)
                app.OnAppSuspended();
            app.OnAppClosed();
            frame.children.Remove(app);

            //If this is the active app and there are other apps, open the next one
            if(apps.Count > 1 && index == apps.Count-1)
            {
                apps[apps.Count - 2].OnAppResumed();
                apps[apps.Count - 2].hidden = false;
                apps[apps.Count - 2].suspended = false;
            }

            //Remove this app from the list
            apps.Remove(app);
        }

        public void Tick()
        {
            //Send key events
            if(queuedKey != null)
            {
                KeyboardOSInputEvent?.Invoke(queuedKey.Value);
                queuedKey = null;
            }

            //Update
            apps[apps.Count - 1].OnAppTicked();
            frame.DrawView();
            connection.UpdateDisplay(frame);

            //Write video
            if(videoFrame >= 0)
            {
                using (FileStream fs = new FileStream("D:\\test\\" + videoFrame + ".png", FileMode.Create))
                    frame.GetScreenshotData().Save(fs, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
                videoFrame++;
            }
        }

        public void Run()
        {
            while(true)
            {
                Tick();
                Thread.Sleep(50);
            }
        }

        public void SaveScreenshot()
        {
            using (FileStream fs = new FileStream("E:\\kb_os_screenshot.png", FileMode.Create))
                frame.GetScreenshotData().Save(fs, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
        }
    }

    public delegate void KeyboardOSInputArgs(InputKey key);
}
