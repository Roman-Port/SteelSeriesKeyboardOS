using KeyboardOS.Apps;
using KeyboardOS.Apps.System;
using KeyboardOS.Framework;
using KeyboardOS.Framework.Views;
using KeyboardOS.Framework.Views.BasicViews;
using KeyboardOS.Framework.Views.SystemViews;
using System;
using System.IO;

namespace KeyboardOS
{
    class Program
    {
        public static Random rand = new Random();
        
        static void Main(string[] args)
        {
            OSInstance os = new OSInstance();
            os.LaunchApp(new LauncherApp(os));
            os.Run();
        }
    }
}
