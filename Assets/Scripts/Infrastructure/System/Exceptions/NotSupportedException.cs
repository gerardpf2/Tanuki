using JetBrains.Annotations;

namespace Infrastructure.System.Exceptions
{
    public static class NotSupportedException
    {
        [ContractAnnotation("=> halt")]
        public static void Throw(string message = null)
        {
            throw new global::System.NotSupportedException(message);
        }
    }
}