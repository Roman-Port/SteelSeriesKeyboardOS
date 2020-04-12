using System;
using System.Collections.Generic;
using System.Text;

namespace KeyboardOS.Framework.Api.SteelSeriesSend
{
    public class SteelSeriesSend
    {
        public string game;
        public string @event;
        public SendData data;
    }

    public class SendData
    {
        public int value;
    }
}
