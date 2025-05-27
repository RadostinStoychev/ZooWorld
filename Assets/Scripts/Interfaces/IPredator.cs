namespace Interfaces
{
    /// <summary>
    /// Interface defining a predator object.
    /// </summary>
    public interface IPredator : IAnimal, ICollidable
    {
        void Consume(IAnimal prey);
    }
}