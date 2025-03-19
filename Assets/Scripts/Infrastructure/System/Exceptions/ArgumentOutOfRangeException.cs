using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Infrastructure.System.Exceptions
{
    public static class ArgumentOutOfRangeException
    {
        [ContractAnnotation("=> halt")]
        public static void Throw(object param, [CallerArgumentExpression("param")] string paramName = null)
        {
            throw new global::System.ArgumentOutOfRangeException(paramName, param, null);
        }
    }
}