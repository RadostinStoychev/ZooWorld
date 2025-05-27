using System;
using System.Collections.Generic;
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
            deathsCount = new Dictionary<AnimalType, int>
            {
                { AnimalType.Frog, 0 },
                { AnimalType.Snake, 0 }
            };
        
            UpdateInterface();
            ZooActions.OnAnimalDied += OnAnimalDied;
        }
    
        private void OnDestroy()
        {
            ZooActions.OnAnimalDied -= OnAnimalDied;
        }
    
        private void OnAnimalDied(AnimalType type)
        {
            if (deathsCount.ContainsKey(type))
            {
                deathsCount[type]++;
                UpdateDeadCount(type, deathsCount[type]);
            }
        }
    
        public void UpdateDeadCount(AnimalType type, int count)
        {
            UpdateInterface();
        }
    
        private void UpdateInterface()
        {
            //TODO: Update to handle bigger amount of animals. For example more than 1 prey.
            
            if (preyDeathsCountText != null)
            {
                preyDeathsCountText.text = String.Format(PreyDeathsCountTextTemplate, deathsCount[AnimalType.Frog]);
            }

            if (predatorDeathsCountText != null)
            {
                predatorDeathsCountText.text = String.Format(PredatorDeathsCountTextTemplate, deathsCount[AnimalType.Snake]);
            }
        }
    }
}