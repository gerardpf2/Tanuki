using System;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Infrastructure.System
{
    // TODO: Test
    public class Converter : IConverter
    {
        public T Convert<T>(object value)
        {
            T convertedValue = default;

            try
            {
                convertedValue = (T)global::System.Convert.ChangeType(value, typeof(T));
            }
            catch (Exception exception)
            {
                InvalidOperationException.Throw(
                    $"Cannot convert \"{value}\" to value of Type: {typeof(T)}. {exception.Message}"
                );
            }

            InvalidOperationException.ThrowIfNull(convertedValue);

            return convertedValue;
        }
    }
}