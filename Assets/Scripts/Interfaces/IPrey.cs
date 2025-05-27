using UnityEngine;

namespace Interfaces
{
    /// <summary>
    /// Interface defining a prey object.
    /// </summary>
    public interface IPrey : IAnimal, ICollidable
    {
        void Flee(Vector3 direction);
    }
}
