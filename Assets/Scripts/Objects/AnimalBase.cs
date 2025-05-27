using Interfaces;
using Management;
using UnityEngine;

namespace Objects
{
    public abstract class AnimalBase : MonoBehaviour, IAnimal
    {
        [SerializeField] protected AnimalType animalType;
    
        protected Rigidbody animalRigidbody;

        //TODO: Add movement checks.
        
        protected IBoundariesManager boundariesManager;
        protected AnimalStats stats;
        protected bool isAlive = true;
    
        public AnimalType Type => animalType;
        public bool IsAlive => isAlive;
        public AnimalType AnimalType => animalType; // For factory
    
        protected virtual void Awake()
        {
            animalRigidbody = GetComponent<Rigidbody>();
            if (animalRigidbody == null)
            {
                animalRigidbody = gameObject.AddComponent<Rigidbody>();
                animalRigidbody.freezeRotation = true;
            }
            
            //TODO: Remove debug numbers.
            boundariesManager = new BoundariesManager(10f, 10f);
        }
    
        protected virtual void Start()
        {
            //TODO: Set actions.
        }
    
        public virtual void Initialize(AnimalStats stats)
        {
            this.stats = stats;
        }

        protected virtual void Update()
        {
            if (!isAlive) return;
            
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
            //TODO: Handle movement.
        }
    
        public virtual void Die()
        {
            if (!isAlive) return;
        
            isAlive = false;
            Destroy(gameObject, 0.1f);
        }
    
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!isAlive) return;
        
            var otherAnimal = other.GetComponent<IAnimal>();
            if (otherAnimal == null || !otherAnimal.IsAlive) return;
        
            if (this is ICollidable collidable)
            {
                collidable.HandleCollision(otherAnimal);
            }
        }
    }
}