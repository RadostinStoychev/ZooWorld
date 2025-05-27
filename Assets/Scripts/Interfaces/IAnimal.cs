namespace Interfaces
{
    /// <summary>
    /// Interface defining animal object.
    /// </summary>
    public interface IAnimal
    {
        AnimalType Type { get; }
        bool IsAlive { get; }
        void Move();
        void Die();
    }
}
