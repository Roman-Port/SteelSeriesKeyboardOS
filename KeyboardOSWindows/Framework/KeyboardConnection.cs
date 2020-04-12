using HidLibrary;
using KeyboardOS.Framework.Api.SteelSeriesBind;
using KeyboardOS.Framework.Api.SteelSeriesSend;
using KeyboardOS.Framework.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace KeyboardOS.Framework
{
    public class KeyboardConnection
    {
        public HidDevice device;
        
        public KeyboardConnection()
        {
            
        }

        public void Init()
        {
            device = HidDevices.Enumerate(4152, 5648).FirstOrDefault();
            device.OpenDevice();
        }

        public void UpdateDisplay(DisplayFrame data)
        {
            //Render
            byte[] rendered = data.GetSerializedData();

            //Create data to send
            byte[] buffer = new byte[2 + rendered.Length];
            buffer[1] = 0x65;
            Array.Copy(rendered, 0, buffer, 2, rendered.Length);

            //Send
            device.WriteFeatureData(buffer);
        }
    }
}
