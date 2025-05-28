using Interfaces;
using Management;
using Objects;
using UnityEngine;

namespace Gameplay.Animals
{
    /// <summary>
    /// Class defining the Frog animal type logic.
    /// </summary>
    public class Frog : AnimalBase, IPrey
    {
        protected override void InitializeMovementBehavior()
        {
            movementBehavior = new AnimalJumpMovement();
        }

        public void HandleCollision(IAnimal collidingAnimal)
        {
            if (AnimalClassification.IsPredator(collidingAnimal.Type))
            {
                Die();
            }
            else if (AnimalClassification.IsPrey(collidingAnimal.Type))
            {
                Vector3 pushDirection = (transform.position - ((MonoBehaviour)collidingAnimal).transform.position).normalized;
                animalRigidbody.AddForce(pushDirection * 3f, ForceMode.Impulse);
            }
        }
    
        public void Flee(Vector3 direction)
        {
            animalRigidbody.AddForce(direction * stats.moveSpeed, ForceMode.Impulse);
        }
    }
}