using System.Collections.Generic;
using UnityEngine;

namespace Management
{
    /// <summary>
    /// Static class containing animal type classification.
    /// </summary>
    public static class AnimalClassification
    {
        private static readonly Dictionary<AnimalType, bool> PredatorsMap = new Dictionary<AnimalType, bool>
        {
            { AnimalType.Frog, false }, 
            { AnimalType.Snake, true } 
        };
    
        public static bool IsPredator(AnimalType animalType)
        {
            return PredatorsMap.ContainsKey(animalType) && PredatorsMap[animalType];
        }
    
        public static bool IsPrey(AnimalType animalType)
        {
            return PredatorsMap.ContainsKey(animalType) && !PredatorsMap[animalType];
        }

        public static List<AnimalType> GetAllAnimalTypes()
        {
            return new List<AnimalType>(PredatorsMap.Keys);
        }
    }
}