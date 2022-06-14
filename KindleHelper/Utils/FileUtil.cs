using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindleHelper.Utils
{
    public class FileUtil
    {
        public FileUtil()
        {
        }

        public static bool CheckCookieFile()
        {
            if (!File.Exists("cookie.txt"))
            {
                Console.WriteLine("cookie.txt 文件不存在， 将自动创建，请将浏览器的cookie，复制到该文件");
                try
                {
                    File.Create("cookie.txt");
                }
                catch
                {
                    Console.WriteLine("cookie.txt创建失败，请手动创建, 并将浏览器的cookie 粘贴到 cookie.txt 文件");
                }
                return false;
            }

            return true;
        }

        public static string? ReadCookieFromFile()
        {
            try
            {
                var text = File.ReadAllText("cookie.txt");
                if (string.IsNullOrWhiteSpace(text))
                {
                    Console.WriteLine("cookie 为空， 请检查 cookie.txt 文件");
                }
                return text;
            }
            catch
            {
                Console.WriteLine("读取 cookie.txt 文件失败");
                return null;
            }
        }
    }
}
