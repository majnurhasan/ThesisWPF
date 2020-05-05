using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ThesisApplication.Models
{
    public class channelOneFeeds
    {
        [JsonProperty("feeds")]
        public List<ChannelOne> feeds { get; set; }
        public int Count { get; set; }
    }
}
