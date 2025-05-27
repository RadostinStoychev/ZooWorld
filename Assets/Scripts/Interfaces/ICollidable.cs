namespace Interfaces
{
    /// <summary>
    /// Interface defining an object that handles collision.
    /// </summary>
    public interface ICollidable
    {
        void HandleCollision(IAnimal collidingAnimal);
    }
}

