using Interfaces;
using Management;
using UnityEngine;

namespace Objects
{
    /// <summary>
    /// Class defining animal base functionality.
    /// </summary>
    public abstract class AnimalBase : MonoBehaviour, IAnimal
    {
        private const float LevelBoundaryX = 10f;
        private const float LevelBoundaryZ = 10f;
        
        protected AnimalType animalType;
        protected Rigidbody animalRigidbody;
        protected IMovementBehavior movementBehavior;
        protected IBoundariesManager boundariesManager;
        protected AnimalStats stats;
        protected bool isAlive = true;
    
        public AnimalType Type => animalType;
        public bool IsAlive => isAlive;
        public AnimalType AnimalType => animalType;
    
        protected virtual void Awake()
        {
            animalRigidbody = GetComponent<Rigidbody>();
            if (animalRigidbody == null)
            {
                animalRigidbody = gameObject.AddComponent<Rigidbody>();
                animalRigidbody.freezeRotation = true;
            }
            
            boundariesManager = new BoundariesManager(LevelBoundaryX, LevelBoundaryZ);
            InitializeMovementBehavior();
        }
    
        protected virtual void Start()
        {
            if (isAlive)
            {
                ZooActions.AnimalSpawned(this);
            }
        }
    
        public virtual void Initialize(AnimalStats animalStats)
        {
            stats = animalStats;
            movementBehavior?.Initialize(animalStats);
        }
        
        public virtual void ResetAnimal()
        {
            isAlive = true;
            
            if (animalRigidbody != null)
            {
                animalRigidbody.velocity = Vector3.zero;
                animalRigidbody.angularVelocity = Vector3.zero;
            }
            
            movementBehavior?.Initialize(stats);
            ZooActions.AnimalSpawned(this);
        }

        protected abstract void InitializeMovementBehavior();
        
        protected virtual void Update()
        {
            if (!isAlive)
            {
                return;
            }
            
            HandleBoundariesCheck();
            Move();
        }

        private void HandleBoundariesCheck()
        {
            if (boundariesManager.IsOutOfBounds(transform.position))
            {
                Vector3 redirectDirection = boundariesManager.GetRedirectDirection(transform.position);
                animalRigidbody.velocity = new Vector3(
                    redirectDirection.x * stats.moveSpeed, 
                    animalRigidbody.velocity.y, 
                    redirectDirection.z * stats.moveSpeed
                );
            }
        }

        public virtual void Move()
        {
            movementBehavior?.Move(transform, animalRigidbody, Time.deltaTime);
        }
    
        public virtual void Die()
        {
            if (!isAlive)
            {
                return;
            }
        
            isAlive = false;
            ZooActions.AnimalDied(animalType);
            ZooActions.AnimalReadyForPool(gameObject, animalType);
        }
    
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!isAlive)
            {
                return;
            }
        
            var otherAnimal = other.GetComponent<IAnimal>();
            if (otherAnimal == null || !otherAnimal.IsAlive)
            {
                return;
            }
        
            if (this is ICollidable collidable)
            {
                collidable.HandleCollision(otherAnimal);
            }
        }
    }
}