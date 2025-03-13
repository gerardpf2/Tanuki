using System;

namespace Infrastructure.System
{
    // TODO: Test
    public class VersionParser : IVersionParser
    {
        public Version Parse(string input)
        {
            if (Version.TryParse(input, out Version version))
            {
                return version;
            }

            throw new InvalidOperationException($"Cannot parse version with Input: {input}");
        }
    }
}