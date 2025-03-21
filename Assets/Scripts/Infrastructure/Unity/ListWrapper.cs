using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Unity
{
    public abstract class ListWrapper { }

    [Serializable]
    public class ListWrapper<T> : ListWrapper
    {
        [NotNull, SerializeField] private List<T> _list = new();

        [NotNull]
        public List<T> List => _list;
    }
}