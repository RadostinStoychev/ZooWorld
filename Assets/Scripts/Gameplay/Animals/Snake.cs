using Interfaces;
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
        
        protected override void InitializeMovementBehavior()
        {
            movementBehavior = new AnimalLinearMovement();
        }
    
        public void HandleCollision(IAnimal collidingAnimal)
        {
            if (collidingAnimal is IPredator)
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
            else if (collidingAnimal is IPrey)
            {
                Consume(collidingAnimal);
            }
        }

        public void Consume(IAnimal prey)
        {
            prey.Die();
        }
    }
}