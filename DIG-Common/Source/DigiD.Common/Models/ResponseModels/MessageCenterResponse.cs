using System;
using System.Collections.Generic;
using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class MessageCenterResponse : BaseResponse
    {
        [JsonProperty("notifications")]
        public List<Message> Messages { get; set; } = new List<Message>();
    }

    public class Message
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("content")]
        public string Text { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset DateTime { get; set; }

        [JsonProperty("status_read")]
        public bool IsRead { get; set; }

        public bool IsNew => !IsRead;
    }
}
