using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Infrastructure.System.Exceptions
{
    public static class InvalidOperationException
    {
        [ContractAnnotation("=> halt")]
        public static void Throw(string message = null)
        {
            throw new global::System.InvalidOperationException(message);
        }

        [ContractAnnotation("param:null => halt")]
        public static void ThrowIfNullWithMessage(object param, string message)
        {
            if (param == null) // "==" instead of "is" because of Unity's operator overloads
            {
                Throw(message);
            }
        }

        [ContractAnnotation("param:null => halt")]
        public static void ThrowIfNull(object param, [CallerArgumentExpression("param")] string paramName = null)
        {
            ThrowIfNullWithMessage(param, $"{paramName} cannot be null");
        }
    }
}