using UnityEngine;

namespace Infrastructure.System
{
    // TODO: Test
    public class ProjectVersionGetter : IProjectVersionGetter
    {
        public string Get()
        {
            return Application.version;
        }
    }
}