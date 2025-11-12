using System;
using UnityEngine;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Infrastructure.Unity
{
    [CreateAssetMenu(fileName = nameof(PrefabContainer), menuName = "Tanuki/Infrastructure/Unity/" + nameof(PrefabContainer))]
    public class PrefabContainer : ScriptableObject, IPrefabGetter
    {
        [Serializable]
        private struct KeyPrefabPair
        {
            public string Key;
            public GameObject Prefab;
        }

        [SerializeField] private KeyPrefabPair[] _keyPrefabPairs;

        public GameObject Get(string key)
        {
            InvalidOperationException.ThrowIfNull(_keyPrefabPairs);

            GameObject prefab = null;

            foreach (KeyPrefabPair keyPrefabPair in _keyPrefabPairs)
            {
                if (keyPrefabPair.Key != key)
                {
                    continue;
                }

                prefab = keyPrefabPair.Prefab;

                break;
            }

            InvalidOperationException.ThrowIfNull(prefab);

            return prefab;
        }
    }
}