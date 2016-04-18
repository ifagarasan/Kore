using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.Util
{
    public static class IO
    {
        public static string GetLeafIOName(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            string[] temp = path.Split('\\');

            return temp[temp.Length - 1];
        }
    }
}
