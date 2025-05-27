using Objects;
using UnityEngine;

namespace Interfaces
{
    /// <summary>
    /// Interface defining an animal movement type.
    /// </summary>
    public interface IMovementBehavior
    {
        void Move(Transform transform, Rigidbody rigidbody, float deltaTime);
        void Initialize(AnimalStats stats);
    }
}