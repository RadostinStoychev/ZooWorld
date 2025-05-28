using Interfaces;
using Management;
using Objects;
using UnityEngine;

namespace Gameplay.Animals
{
    /// <summary>
    /// Class defining the Snake animal type logic.
    /// </summary>
    public class Snake : AnimalBase, IPredator
    {
        private const float PredatorTypeCannibalismChance = 0.5f;
        private const string PredatorConsumeMessage = "Tasty!";
        
        protected override void InitializeMovementBehavior()
        {
            movementBehavior = new AnimalLinearMovement();
        }
    
        public void HandleCollision(IAnimal collidingAnimal)
        {
            if (AnimalClassification.IsPredator(collidingAnimal.Type))
            {
                if (Random.Range(0f, 1f) < PredatorTypeCannibalismChance)
                {
                    Consume(collidingAnimal);
                }
                else
                {
                    collidingAnimal.Die();
                }
            }
            else if (AnimalClassification.IsPrey(collidingAnimal.Type))
            {
                Consume(collidingAnimal);
            }
        }

        public void Consume(IAnimal prey)
        {
            prey.Die();
            ZooActions.MessageSpawned(transform.position, PredatorConsumeMessage);
        }
    }
}