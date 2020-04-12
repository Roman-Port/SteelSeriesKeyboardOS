using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyboardOS.Framework.Api.SteelSeriesBind
{
    public class SteelSeriesBind
    {
        public string game { get; set; }
        public string @event { get; set; }
        public int icon_id { get; set; }
        public List<Handler> handlers { get; set; }
    }

    public class Handler
    {
        [JsonProperty("device-type")]
        public string deviceType { get; set; }
        public string mode { get; set; }
        public string zone { get; set; }
        public List<Data> datas { get; set; }
    }

    public class Data
    {
        [JsonProperty("has-text")]
        public bool hasText { get; set; }
        [JsonProperty("image-data")]
        public int[] imageData { get; set; }
    }
}
