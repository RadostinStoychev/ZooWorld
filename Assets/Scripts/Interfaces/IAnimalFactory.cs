using UnityEngine;

namespace Interfaces
{
    /// <summary>
    /// Interface defining animal creation management.
    /// </summary>
    public interface IAnimalFactory
    {
        GameObject CreateAnimal(AnimalType type, Vector3 position);
    }
}
