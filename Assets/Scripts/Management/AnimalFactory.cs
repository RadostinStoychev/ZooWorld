using System.Collections.Generic;
using Interfaces;
using Objects;
using UnityEngine;

namespace Management
{
    /// <summary>
    /// Class handling animal creation logic.
    /// </summary>
    [System.Serializable]
    public class AnimalFactory : IAnimalFactory
    {
        [SerializeField] private List<SpawnSettings> animalPrefabs;
        private Dictionary<AnimalType, SpawnSettings> prefabMap;
    
        public void Initialize()
        {
            prefabMap = new Dictionary<AnimalType, SpawnSettings>();
            foreach (var setting in animalPrefabs)
            {
                var animalComponent = setting.prefab.GetComponent<AnimalBase>();
                if (animalComponent != null)
                {
                    prefabMap[animalComponent.AnimalType] = setting;
                }
            }
        }
    
        public GameObject CreateAnimal(AnimalType type, Vector3 position)
        {
            if (!prefabMap.ContainsKey(type))
            {
                Debug.LogError($"No prefab registered for animal type: {type}");
                return null;
            }
        
            var settings = prefabMap[type];
            var instance = Object.Instantiate(settings.prefab, position, Quaternion.identity);
            
            var animal = instance.GetComponent<AnimalBase>();
            if (animal != null)
            {
                animal.Initialize(settings.stats);
            }
        
            return instance;
        }
    
        public List<SpawnSettings> GetAnimalPrefabs() => animalPrefabs;
    }
}