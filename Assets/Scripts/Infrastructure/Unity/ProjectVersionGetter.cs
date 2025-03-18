using System;
using UnityEngine;

namespace Infrastructure.Unity
{
    public class ProjectVersionGetter : IProjectVersionGetter
    {
        public string Get()
        {
            string version = Application.version;

            if (version == null)
            {
                throw new InvalidOperationException("Cannot get project version");
            }

            return version;
        }
    }
}