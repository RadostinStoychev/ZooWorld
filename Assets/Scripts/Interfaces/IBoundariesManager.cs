using UnityEngine;

namespace Interfaces
{
    /// <summary>
    /// Interface defining level boundaries manager.
    /// </summary>
    public interface IBoundariesManager
    {
        bool IsOutOfBounds(Vector3 position);
        Vector3 GetRedirectDirection(Vector3 position);
    }
}