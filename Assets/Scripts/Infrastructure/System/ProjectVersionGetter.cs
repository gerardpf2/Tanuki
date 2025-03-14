using UnityEngine;

namespace Infrastructure.System
{
    public class ProjectVersionGetter : IProjectVersionGetter
    {
        public string Get()
        {
            return Application.version;
        }
    }
}