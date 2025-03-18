using UnityEngine;

namespace Infrastructure.Unity
{
    public class ProjectVersionGetter : IProjectVersionGetter
    {
        public string Get()
        {
            return Application.version;
        }
    }
}