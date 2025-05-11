using System.Windows.Markup;

namespace PresentationLayer.WPF.Helpers
{
    public class EnumBindingSourceExtension : MarkupExtension
    {
        public Type EnumType { get; private set; }

        public EnumBindingSourceExtension(Type enumType)
        {
            if (enumType is null || !enumType.IsEnum)
            {
                throw new ArgumentException("Type must be an enum");
            }
            EnumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CreateEnumValueList(EnumType);
        }

        private List<object> CreateEnumValueList(Type enumType)
        { 
            return Enum.GetNames(enumType)
                .Select(name => Enum.Parse(enumType, name))
                .ToList();
        }
    }
}
