using UnityEngine;

namespace Infrastructure.Unity
{
    public class DeltaTimeGetter : IDeltaTimeGetter
    {
        public float Get()
        {
            return Time.deltaTime;
        }
    }
}