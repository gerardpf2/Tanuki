using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Infrastructure.System.Exceptions
{
    public static class ArgumentException
    {
        [ContractAnnotation("=> halt")]
        public static void Throw(string message, string paramName)
        {
            throw new global::System.ArgumentException(message, paramName);
        }

        public static void ThrowIfTypeIsNot<T>(object param, [CallerArgumentExpression("param")] string paramName = null)
        {
            if (param is not T)
            {
                Throw($"Type is not {typeof(T)}", paramName);
            }
        }
    }
}