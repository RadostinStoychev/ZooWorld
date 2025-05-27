using UnityEngine;

namespace Interfaces
{
    /// <summary>
    /// Interface defining spawn handling manager.
    /// </summary>
    public interface ISpawnHandling
    {
        AnimalType GetNextAnimalType();
        Vector3 GetSpawnPosition();
        float GetSpawnInterval();
    }
}