using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Infrastructure.System.Exceptions
{
    public static class ArgumentNullException
    {
        [ContractAnnotation("param:null => halt")]
        public static void ThrowIfNull(object param, [CallerArgumentExpression("param")] string paramName = null)
        {
            if (param is null)
            {
                throw new global::System.ArgumentNullException(paramName);
            }
        }
    }
}