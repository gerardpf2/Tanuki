using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.Unity
{
    public class ProjectVersionGetter : IProjectVersionGetter
    {
        public string Get()
        {
            string version = Application.version;

            InvalidOperationException.ThrowIfNull(version);

            return version;
        }
    }
}