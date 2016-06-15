namespace Kore.IO.Util{
    public class KoreFolderInfo : IKoreFolderInfo
    {
        public KoreFolderInfo(string folder)
        {
            FullName = folder;
        }

        public string FullName { get; }

        public void EnsureExists()
        {
            throw new System.NotImplementedException();
        }
    }
}