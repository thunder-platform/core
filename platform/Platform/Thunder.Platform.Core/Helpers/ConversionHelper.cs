using System.ComponentModel;

namespace Thunder.Platform.Core.Helpers
{
    public static class ConversionHelper
    {
        public static TType ConvertFrom<TType>(object value)
        {
            var converter = TypeDescriptor.GetConverter(typeof(TType));
            return (TType)converter.ConvertFrom(value);
        }
    }
}
