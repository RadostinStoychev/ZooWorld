using System.Collections.Generic;

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
    
        /// <summary>
        /// Returns true if provided type is Predator.
        /// </summary>
        public static bool IsPredator(AnimalType animalType)
        {
            return PredatorsMap.ContainsKey(animalType) && PredatorsMap[animalType];
        }
    
        /// <summary>
        /// Returns true if provided type is Prey.
        /// </summary>
        public static bool IsPrey(AnimalType animalType)
        {
            return PredatorsMap.ContainsKey(animalType) && !PredatorsMap[animalType];
        }

        /// <summary>
        /// Returns all present animal types.
        /// </summary>
        /// <returns></returns>
        public static List<AnimalType> GetAllAnimalTypes()
        {
            return new List<AnimalType>(PredatorsMap.Keys);
        }
    }
}