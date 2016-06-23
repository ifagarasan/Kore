using System;
using System.Windows.Markup;

namespace Kore.Wpf.Markup.Extensions
{
    public class EnumBinding: MarkupExtension
    {
        public EnumBinding(Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;
            if (!type.IsEnum)
                throw new InvalidOperationException("Expected type to be Enum");

            Type = type;
        }

        public Type Type { get; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(Type);
        }
    }
}
