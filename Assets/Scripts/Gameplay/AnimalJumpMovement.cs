using Interfaces;
using Objects;
using UnityEngine;

namespace Gameplay
{
    /// <summary>
    /// Class handling jump animal movement type.
    /// </summary>
    public class AnimalJumpMovement : IMovementBehavior
    {
        private const float JumpInterval = 2f;
        private const float JumpDirectionChangeChance = 0.3f;
        
        private float jumpForce;
        private float timeForNextJump;
        private Vector3 jumpDirection;
    
        public void Initialize(AnimalStats stats)
        {
            jumpForce = stats.moveSpeed;
            ChangeJumpDirection();
            timeForNextJump = Time.time + JumpInterval;
        }
    
        public void Move(Transform transform, Rigidbody rigidbody, float deltaTime)
        {
            if (Time.time >= timeForNextJump)
            {
                Jump(rigidbody);
                timeForNextJump = Time.time + JumpInterval;
            }
        }
    
        private void Jump(Rigidbody rigidbody)
        {
            if (Random.Range(0f, 1f) < JumpDirectionChangeChance)
            {
                ChangeJumpDirection();
            }
        
            rigidbody.velocity = new Vector3(
                jumpDirection.x * jumpForce, 
                jumpForce * 0.5f, 
                jumpDirection.z * jumpForce
            );
        }
    
        private void ChangeJumpDirection()
        {
            jumpDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        }
    }
}