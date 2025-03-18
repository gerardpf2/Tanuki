using System.Runtime.CompilerServices;

namespace Infrastructure.System.Exceptions
{
    public static class ArgumentNullException
    {
        public static void ThrowIfNull(object param, [CallerArgumentExpression("param")] string paramName = null)
        {
            if (param is null)
            {
                throw new global::System.ArgumentNullException(paramName);
            }
        }
    }
}