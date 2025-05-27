using System.ComponentModel;

namespace Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T enumValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description;
        }

        public static TEnum GetEnumFromDescription<TEnum>(this string description)
        {
            var enumType = typeof(TEnum);
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("O tipo fornecedio não é um Enum");
            }

            var enumValues = Enum.GetValues(enumType).Cast<TEnum>();
            foreach (var value in enumValues)
            {
                var fieldInfo = value.GetType().GetField(value.ToString());
                var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));
                if (attribute != null && attribute.Description.Equals(description, StringComparison.OrdinalIgnoreCase))
                {
                    return value;
                }
            }
            throw new ArgumentException($"Valor Enum não encontrado para descrição'{description}'.");
        }
    }
}