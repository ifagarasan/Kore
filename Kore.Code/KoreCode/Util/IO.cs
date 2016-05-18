namespace KoreCode.Util
{
    public static class IO
    {
        public static string GetLeafIOName(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            var temp = path.Split('\\');

            return temp[temp.Length - 1];
        }
    }
}