using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Objects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Management
{
    /// <summary>
    /// Class handling animal creation logic.
    /// </summary>
    [Serializable]
    public class AnimalFactory : IAnimalFactory
    {
        private const string AnimalPoolParentTemplateName = "{0}Pool";
        private const string AnimalTypePoolNoneError = "No pool or prefab registered for animal type: {0}";
        private const string AnimalPoolMaximumCountReachedError = "Maximum pool size reached for {0} type. Reusing oldest object.";
        
        [Header("General")]
        [SerializeField] 
        private List<SpawnSettings> animalPrefabs;
        
        [Header("Pool Settings")]
        [SerializeField] private int initialPoolSize = 5;
        [SerializeField] private int maxPoolSize = 10;
        
        private Dictionary<AnimalType, SpawnSettings> prefabMap;
        private Dictionary<AnimalType, Queue<GameObject>> objectPools;
        private Dictionary<AnimalType, Transform> poolParents;
        private Dictionary<GameObject, AnimalType> activeAnimals;
        
        public void Initialize()
        {
            prefabMap = new Dictionary<AnimalType, SpawnSettings>();
            objectPools = new Dictionary<AnimalType, Queue<GameObject>>();
            poolParents = new Dictionary<AnimalType, Transform>();
            activeAnimals = new Dictionary<GameObject, AnimalType>();
            
            foreach (var setting in animalPrefabs)
            {
                var animalComponent = setting.prefab.GetComponent<AnimalBase>();
                if (animalComponent != null)
                {
                    prefabMap[animalComponent.AnimalType] = setting;
                }
            }
            
            InitializePools();
            ZooActions.OnAnimalReadyForPool += ReturnAnimalToPool;
        }
        
        private void OnDestroy()
        {
            ZooActions.OnAnimalReadyForPool -= ReturnAnimalToPool;
        }
        
        /// <summary>
        /// Creating animal pools and preloading objects.
        /// </summary>
        public void InitializePools()
        {
            foreach (var kvp in prefabMap)
            {
                var animalType = kvp.Key;
                var poolParent = new GameObject(String.Format(AnimalPoolParentTemplateName, animalType));
                poolParents[animalType] = poolParent.transform;
                objectPools[animalType] = new Queue<GameObject>();
                
                for (int i = 0; i < initialPoolSize; i++)
                {
                    GetAnimalObjectFromPool(animalType);
                }
            }
        }

        /// <summary>
        /// Get animal from its pool, enable and position it.
        /// </summary>
        public GameObject CreateAnimal(AnimalType type, Vector3 position)
        {
            if (!objectPools.ContainsKey(type) || !prefabMap.ContainsKey(type))
            {
                Debug.LogError(String.Format(AnimalTypePoolNoneError, type));
                return null;
            }
            
            GameObject newAnimalObject;
            
            if (objectPools[type].Count > 0)
            {
                newAnimalObject = objectPools[type].Dequeue();
            }
            else
            {
                if (activeAnimals.Count >= maxPoolSize)
                {
                    Debug.LogWarning(String.Format(AnimalPoolMaximumCountReachedError, type));
                    return null;
                }
                
                newAnimalObject = GetAnimalObjectFromPool(type);
                objectPools[type].Dequeue();
            }

            // Return early if animal object is null.
            if (newAnimalObject == null)
            {
                return newAnimalObject;
            }
            
            newAnimalObject.transform.position = position;
            newAnimalObject.transform.rotation = Quaternion.identity;
            newAnimalObject.SetActive(true);
                
            activeAnimals[newAnimalObject] = type;
                
            var animal = newAnimalObject.GetComponent<AnimalBase>();
            if (animal != null)
            {
                animal.ResetAnimal();
            }

            return newAnimalObject;
        }
        
        /// <summary>
        /// Resets and returns animal back to its pool.
        /// </summary>
        public void ReturnAnimalToPool(GameObject targetAnimal, AnimalType type)
        {
            if (type == AnimalType.None)
            {
                return;
            }
            
            if (targetAnimal == null)
            {
                return;
            }
            
            if (activeAnimals.ContainsKey(targetAnimal))
            {
                activeAnimals.Remove(targetAnimal);
            }
            
            targetAnimal.SetActive(false);
            targetAnimal.transform.SetParent(poolParents[type]);
            
            var animalRigidbody = targetAnimal.GetComponent<Rigidbody>();
            if (animalRigidbody != null)
            {
                animalRigidbody.velocity = Vector3.zero;
                animalRigidbody.angularVelocity = Vector3.zero;
            }
            
            objectPools[type].Enqueue(targetAnimal);
        }
        
        public List<SpawnSettings> GetAnimalPrefabs() => animalPrefabs;
        
        /// <summary>
        /// Returns active animals count of specific type.
        /// </summary>
        public int GetActiveCount(AnimalType type)
        {
            return activeAnimals.Count(kvp => kvp.Value == type);
        }
        
        /// <summary>
        /// Returns object pools count of specific type.
        /// </summary>
        public int GetPooledCount(AnimalType type)
        {
            return objectPools.ContainsKey(type) ? objectPools[type].Count : 0;
        }
        
        private GameObject GetAnimalObjectFromPool(AnimalType type)
        {
            if (!prefabMap.ContainsKey(type))
            {
                return null;
            }
            
            var settings = prefabMap[type];
            var instance = Object.Instantiate(settings.prefab, poolParents[type], true);
            
            var animal = instance.GetComponent<AnimalBase>();
            if (animal != null)
            {
                animal.Initialize(settings.stats);
            }

            instance.SetActive(false);
            objectPools[type].Enqueue(instance);
            
            return instance;
        }
    }
}