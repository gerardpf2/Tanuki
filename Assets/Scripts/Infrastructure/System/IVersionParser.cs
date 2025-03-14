using System;
using JetBrains.Annotations;

namespace Infrastructure.System
{
    public interface IVersionParser
    {
        [NotNull]
        Version Parse(string input);
    }
}