using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Objects;
using UnityEngine;

namespace Management
{
    /// <summary>
    /// Class handling animal spawning.
    /// </summary>
    public class SpawnManager : ISpawnHandling
    {
        private readonly List<SpawnSettings> spawnSettings;
        private readonly float spawnRadius;
        private readonly float baseSpawnInterval;
        private float totalWeight;
    
        public SpawnManager(List<SpawnSettings> settings, float radius, float interval)
        {
            spawnSettings = settings;
            spawnRadius = radius;
            baseSpawnInterval = interval;
            totalWeight = settings.Sum(s => s.spawnWeight);
        }
    
        public AnimalType GetNextAnimalType()
        {
            var randomValue = Random.Range(0f, totalWeight);
            var currentWeight = 0f;
        
            foreach (var setting in spawnSettings)
            {
                currentWeight += setting.spawnWeight;
                if (randomValue <= currentWeight)
                {
                    var animal = setting.prefab.GetComponent<AnimalBase>();
                    return animal != null ? animal.AnimalType : AnimalType.None;
                }
            }
        
            return AnimalType.None;
        }
    
        public Vector3 GetSpawnPosition()
        {
            var x = Random.Range(-spawnRadius, spawnRadius);
            var z = Random.Range(-spawnRadius, spawnRadius);
            return new Vector3(x, 0.5f, z);
        }
    
        public float GetSpawnInterval()
        {
            return baseSpawnInterval + Random.Range(-0.5f, 0.5f);
        }
    }
}