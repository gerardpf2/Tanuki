using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Infrastructure.System.Exceptions
{
    public static class InvalidOperationException
    {
        [ContractAnnotation("=> halt")]
        public static void Throw(string message)
        {
            throw new global::System.InvalidOperationException(message);
        }

        [ContractAnnotation("param:null => halt")]
        public static void ThrowIfNull(object param, [CallerArgumentExpression("param")] string paramName = null)
        {
            if (param is null)
            {
                Throw($"Param {paramName} cannot be null");
            }
        }
    }
}