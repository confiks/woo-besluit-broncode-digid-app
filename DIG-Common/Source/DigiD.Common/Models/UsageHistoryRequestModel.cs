using System;
using System.Collections.Generic;
using DigiD.Common.Http.Models;
using DigiD.Common.Models.RequestModels;
using Newtonsoft.Json;

namespace DigiD.Common.Models
{
    public class UsageHistoryRequestModel : BaseMijnDigiDRequest
    {
        [JsonProperty("page_id")]
        public int PageId { get; set; }
        
        [JsonProperty("language")]
        public string Language { get; set; }
    }

    public class UsageHistoryResponseModel : BaseResponse
    {
        [JsonProperty("total_items")]
        public int TotalItems { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("logs")]
        public List<UsageHistoryModel> Items { get; set; }
    }

    public class UsageHistoryModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime DateTime { get; set; }
        
        [JsonProperty("name")]
        public string Message { get; set; }
    }
}
