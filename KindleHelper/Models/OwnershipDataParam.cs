using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindleHelper.Models
{
	public class OwnershipDataParam
	{
		[JsonProperty("param")]
		public object param { get; set; }

		public class OwnershipDataBase
		{
			[JsonProperty("sortOrder")]
			public string SortOrder { get; set; }
			[JsonProperty("sortIndex")]
			public string SortIndex { get; set; }
			[JsonProperty("startIndex")]
			public int StartIndex { get; set; }
			[JsonProperty("batchSize")]
			public int BatchSize { get; set; }
			[JsonProperty("contentType")]
			public string ContentType;
			[JsonProperty("itemStatus")]
			public List<string> ItemStatus;

		}

		public class OwnershipDataEBok : OwnershipDataBase
		{
			[JsonProperty("originType")]
			public List<string> OriginType { get; set; }
		}

		public class OwnershipDataPDoc : OwnershipDataBase
		{
			[JsonProperty("isExtendedMYK")]
			public bool IsExtendedMYK { get; set; }
		}
	}
}
