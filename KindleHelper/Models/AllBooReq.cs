using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KindleHelper.Models
{
    public class AllBooReq
    {
    }

    public class OwnershipDataEBook : OwnershipDataBase
    {
        [JsonPropertyName("originType")]
        public string OriginType { get; set; }
        [JsonPropertyName("showSharedContent")]
        public bool ShowSharedContent { get; set; }
    }

    public class OwnershipDataPDoc : OwnershipDataBase
    {
        [JsonPropertyName("isExtendedMYK")]
        public bool IsExtendedMYK { get; set; }
    }

    public class OwnershipDataBase
    {
        [JsonPropertyName("sortOrder")]
        public string SortOrder { get; set; }
        [JsonPropertyName("sortIndex")]
        public string SortIndex { get; set; }
        [JsonPropertyName("startIndex")]
        public int StartIndex { get; set; }
        [JsonPropertyName("batchSize")]
        public int BatchSize { get; set; }
        [JsonPropertyName("contentType")]
        public string ContentType { get; set; }
        [JsonPropertyName("itemStatus")]
        public string ItemStatus { get; set; }
    }
}
