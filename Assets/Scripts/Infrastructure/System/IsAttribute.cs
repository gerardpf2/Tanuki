using System;

namespace Infrastructure.System
{
    // There is no behaviour behind this attribute for now, it is only used to decorate values to provide extra context

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
    public class IsAttribute : Attribute
    {
        public IsAttribute(ComparisonOperator comparisonOperator, int value) { }

        public IsAttribute(ComparisonOperator comparisonOperator, float value) { }
    }
}