using Interfaces;
using UnityEngine;

namespace Management
{
    /// <summary>
    /// Class handling general game logic.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        [Header("Spawn Configuration")]
        [SerializeField] 
        private AnimalFactory animalFactory;
        [SerializeField] 
        private float baseSpawnInterval = 1.5f;
        [SerializeField] 
        private float spawnRadius = 8f;
        
        [Header("Dependencies")]
        [SerializeField] 
        private UIManager uiManager;
        [SerializeField] 
        private FloatingMessagesManager floatingMessagesManager;

        private ISpawnHandling spawnManager;
        private float nextSpawnTime;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                InitializeGame();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    
        private void InitializeGame()
        {
            animalFactory.Initialize();
            spawnManager = new SpawnManager(animalFactory.GetAnimalPrefabs(), spawnRadius, baseSpawnInterval);
            uiManager?.Initialize();
            SetNextSpawnTime();
        }
    
        private void Update()
        {
            if (Time.time >= nextSpawnTime)
            {
                SpawnAnimal();
                SetNextSpawnTime();
            }
        }
    
        private void SpawnAnimal()
        {
            AnimalType animalTypeToSpawn = spawnManager.GetNextAnimalType();

            if (animalTypeToSpawn == AnimalType.None)
            {
                return;
            }
            
            Vector3 spawnPosition = spawnManager.GetSpawnPosition();
            animalFactory.CreateAnimal(animalTypeToSpawn, spawnPosition);
        }
    
        private void SetNextSpawnTime()
        {
            nextSpawnTime = Time.time + spawnManager.GetSpawnInterval();
        }
    }
}