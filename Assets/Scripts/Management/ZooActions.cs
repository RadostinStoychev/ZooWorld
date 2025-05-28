using System;
using Interfaces;
using UnityEngine;

namespace Management
{
    /// <summary>
    /// Game wide actions for loose coupling.
    /// </summary>
    public static class ZooActions
    {
        public static event Action<AnimalType> OnAnimalDied;
        public static event Action<Vector3, string> OnMessageSpawned;
        public static event Action<IAnimal> OnAnimalSpawned;
        public static event Action<GameObject, AnimalType> OnAnimalReadyForPool;
    
        public static void AnimalDied(AnimalType type) => OnAnimalDied?.Invoke(type);
        public static void MessageSpawned(Vector3 position, string message) => OnMessageSpawned?.Invoke(position, message);
        public static void AnimalSpawned(IAnimal animal) => OnAnimalSpawned?.Invoke(animal);
        public static void AnimalReadyForPool(GameObject animal, AnimalType type) => OnAnimalReadyForPool?.Invoke(animal, type);
    }
}