using System;
using System.Collections.Generic;

namespace Infrastructure.System
{
    public abstract class ListWrapper { }

    [Serializable]
    public class ListWrapper<T> : ListWrapper
    {
        public readonly List<T> List = new();
    }
}