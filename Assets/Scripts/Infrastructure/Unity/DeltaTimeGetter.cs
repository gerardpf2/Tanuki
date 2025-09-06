using Infrastructure.System;
using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.Unity
{
    public class DeltaTimeGetter : IDeltaTimeGetter
    {
        public float Get()
        {
            float deltaTimeS = Time.deltaTime;

            InvalidOperationException.ThrowIfNot(deltaTimeS, ComparisonOperator.GreaterThanOrEqualTo, 0.0f);

            return deltaTimeS;
        }
    }
}