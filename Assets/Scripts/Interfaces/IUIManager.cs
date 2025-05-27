namespace Interfaces
{
    /// <summary>
    /// Interface defining user interface manager.
    /// </summary>
    public interface IUIManager
    {
        void Initialize();
        void UpdateDeadCount(AnimalType type, int count);
    }
}