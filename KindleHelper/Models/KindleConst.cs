using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindleHelper.Models
{
	public class KindleConst
	{
		public KindleConst()
		{
		}

		public static string COOKIE = string.Empty;

	}

	public class CONTENT_TYPES
	{
		public string Name { get; set; }

		public static readonly CONTENT_TYPES EBOK = new CONTENT_TYPES()
		{
			Name = "Ebook"
		};

		public static readonly CONTENT_TYPES PDOC = new CONTENT_TYPES()
		{
			Name = "KindlePDoc"
		};

		public static CONTENT_TYPES FromString(string fileType)
		{
			switch (fileType)
			{
				case "EBOK":
				default:
					return EBOK;
				case "PDOC":
					return PDOC;
			}
		}
	}
}
