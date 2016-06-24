using static Kore.Validation.ObjectValidation;

namespace Kore.IO.Util
{
    public static class IoNode
    {
        public static string RelativePath(IKoreIoNodeInfo source, IKoreIoNodeInfo parent)
        {
            IsNotNull(source, nameof(source));
            IsNotNull(parent, nameof(parent));

            var sourcePath = source.FullName;
            var parentPath = parent.FullName;

            return sourcePath.StartsWith(parentPath) ? sourcePath.Substring(parentPath.Length).TrimStart('\\') : string.Empty;
        }
    }
}