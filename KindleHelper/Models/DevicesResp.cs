using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KindleHelper.Models
{
	public class DeviceResp
	{
		public GetDevice GetDevices { get; set; }


		public class GetDevice
		{
			[JsonProperty("count")]
			public int Count { get; set; }
			[JsonProperty("devices")]
			public List<Device> Devices;
		}

		public class Device
		{
			[JsonProperty("deviceSerialNumber")]
			public string DeviceSerialNumber { get; set; }
			[JsonProperty("deviceType")]
			public string DeviceType;
			[JsonProperty("customerId")]
			public string CustomerId { get; set; }
		}
	}
}
