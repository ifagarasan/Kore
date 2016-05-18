using System;

namespace Kore.Code.Util
{
    public static class Types
    {
        public static object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}