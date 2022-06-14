using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindleHelper.Models
{
	public class KindleUrl
	{
		public string BookAll { get; set; }
		public string Download { get; set; }
		public string Payload { get; set; }

		public static readonly KindleUrl CN = new KindleUrl()
		{
			BookAll = "https://www.amazon.cn/hz/mycd/myx#/home/content/booksAll/dateDsc/",
			Download = "https://cde-ta-g7g.amazon.com/FionaCDEServiceEngine/FSDownloadContent?type={0}&key={1}&fsn={2}&device_type={3}&customerId={4}&authPool=AmazonCN",
			Payload = "https://www.amazon.cn/hz/mycd/ajax"
		};

		public static readonly KindleUrl COM = new KindleUrl()
		{
			BookAll = "https://www.amazon.com/hz/mycd/myx#/home/content/booksAll/dateDsc/",
			Download = "https://cde-ta-g7g.amazon.com/FionaCDEServiceEngine/FSDownloadContent?type={0}&key={1}&fsn={2}&device_type={3}&customerId={4}",
			Payload = "https://www.amazon.com/hz/mycd/ajax"
		};

		public static readonly KindleUrl JP = new KindleUrl()
		{
			BookAll = "https://www.amazon.jp/hz/mycd/myx#/home/content/booksAll/dateDsc/",
			Download = "https://cde-ta-g7g.amazon.com/FionaCDEServiceEngine/FSDownloadContent?type={0}&key={1}&fsn={2}&device_type={3}&customerId={4}",
			Payload = "https://www.amazon.jp/hz/mycd/ajax"
		};

		public static KindleUrl GetKindleUrl(string name)
		{
			switch (name)
			{
				case "cn":
				default:
					return KindleUrl.CN;
				case "jp":
					return KindleUrl.JP;
				case "com":
					return KindleUrl.COM;
			}
		}
	}
}
