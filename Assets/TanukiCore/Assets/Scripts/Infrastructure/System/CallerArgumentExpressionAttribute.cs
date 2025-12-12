// Namespace needs to be System.Runtime.CompilerServices, do not change it

namespace System.Runtime.CompilerServices
{
#if !NET6_0_OR_GREATER

    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class CallerArgumentExpressionAttribute : Attribute
    {
        public readonly string ParameterName;

        public CallerArgumentExpressionAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }
    }

#endif
}