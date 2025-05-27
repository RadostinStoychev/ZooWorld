using UnityEngine;

namespace Objects
{
    /// <summary>
    /// Definition of animal spawn settings.
    /// </summary>
    [System.Serializable]
    public struct SpawnSettings
    {
        public GameObject prefab;
        public float spawnWeight;
        public AnimalStats stats;
    }
}