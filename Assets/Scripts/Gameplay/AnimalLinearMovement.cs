using Interfaces;
using Objects;
using UnityEngine;

namespace Gameplay
{
    /// <summary>
    /// Class handling linear animal movement type.
    /// </summary>
    public class AnimalLinearMovement : IMovementBehavior
    {
        private const float LinearDirectionChangeChance = 0.001f;
        
        private Vector3 moveDirection;
        private float movementSpeed = 2f;
    
        public void Initialize(AnimalStats stats)
        {
            movementSpeed = stats.moveSpeed;
            ChangeDirection();
        }
    
        public void Move(Transform transform, Rigidbody rigidbody, float deltaTime)
        {
            if (Random.Range(0f, 1f) < LinearDirectionChangeChance)
            {
                ChangeDirection();
            }
        
            rigidbody.velocity = new Vector3(
                moveDirection.x * movementSpeed, 
                rigidbody.velocity.y, 
                moveDirection.z * movementSpeed
            );
        }
    
        private void ChangeDirection()
        {
            moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        }
    }
}