using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using TMPro;
using UnityEngine;

namespace Management
{
    /// <summary>
    /// Class handling user interface logic.
    /// </summary>
    public class UIManager : MonoBehaviour, IUIManager
    {
        private const string PreyDeathsCountTextTemplate = "Prey Deaths: {0}";
        private const string PredatorDeathsCountTextTemplate = "Predator Deaths: {0}";
        
        [Header("UI References")]
        [SerializeField] 
        private TextMeshProUGUI preyDeathsCountText;
        [SerializeField] 
        private TextMeshProUGUI predatorDeathsCountText;
    
        private Dictionary<AnimalType, int> deathsCount;
    
        public void Initialize()
        {
            deathsCount = new Dictionary<AnimalType, int>();
            
            var allAnimalTypes = AnimalClassification.GetAllAnimalTypes();
            foreach (var animalType in allAnimalTypes)
            {
                deathsCount[animalType] = 0;
            }
        
            UpdateInterface();
            ZooActions.OnAnimalDied += OnAnimalDied;
        }

        private void OnDestroy()
        {
            ZooActions.OnAnimalDied -= OnAnimalDied;
        }
    
        private void OnAnimalDied(AnimalType type)
        {
            if (!deathsCount.ContainsKey(type))
            {
                deathsCount[type] = 0;
            }
        
            deathsCount[type]++;
            UpdateDeadCount(type, deathsCount[type]);
        }
    
        public void UpdateDeadCount(AnimalType type, int count)
        {
            UpdateInterface();
        }
    
        private void UpdateInterface()
        {
            var totalPreyDeaths = GetTotalDeathsByCategory(false); 
            var totalPredatorDeaths = GetTotalDeathsByCategory(true);
            
            if (preyDeathsCountText != null)
            {
                preyDeathsCountText.text = String.Format(PreyDeathsCountTextTemplate, totalPreyDeaths);
            }
        
            if (predatorDeathsCountText != null)
            {
                predatorDeathsCountText.text = String.Format(PredatorDeathsCountTextTemplate, totalPredatorDeaths);
            }
        }
        
        private int GetTotalDeathsByCategory(bool isPredator)
        {
            return (from kvp in deathsCount let animalType = kvp.Key let deaths = kvp.Value 
                where AnimalClassification.IsPredator(animalType) == isPredator select deaths).Sum();
        }
    }
}