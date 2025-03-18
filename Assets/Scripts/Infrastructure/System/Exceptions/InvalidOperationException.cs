using System.Runtime.CompilerServices;

namespace Infrastructure.System.Exceptions
{
    public static class InvalidOperationException
    {
        public static void ThrowIfNull(object param, [CallerArgumentExpression("param")] string paramName = null)
        {
            if (param is null)
            {
                throw new global::System.InvalidOperationException($"Param {paramName} cannot be null");
            }
        }
    }
}