using Interfaces;
using UnityEngine;

namespace Management
{
    /// <summary>
    /// Class handling level boundaries logic.
    /// </summary>
    public class BoundariesManager : IBoundariesManager
    {
        private readonly float xLevelBoundary;
        private readonly float zLevelBoundary;
    
        public BoundariesManager(float xLevelBoundary, float zLevelBoundary)
        {
            this.xLevelBoundary = xLevelBoundary;
            this.zLevelBoundary = zLevelBoundary;
        }
    
        /// <summary>
        /// Returns true if position is outside set level bounds.
        /// </summary>
        public bool IsOutOfBounds(Vector3 position)
        {
            return Mathf.Abs(position.x) > xLevelBoundary || Mathf.Abs(position.z) > zLevelBoundary;
        }
    
        /// <summary>
        /// Returns opposite direction of provided.
        /// </summary>
        public Vector3 GetRedirectDirection(Vector3 position)
        {
            return -position.normalized;
        }
    }
}