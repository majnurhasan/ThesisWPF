using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ThesisApplication.Models
{
    public class channelTwoFeeds
    {
        [JsonProperty("feeds")]
        public List<ChannelTwo> feeds { get; set; }
        public int Count { get; set; }
    }
}
