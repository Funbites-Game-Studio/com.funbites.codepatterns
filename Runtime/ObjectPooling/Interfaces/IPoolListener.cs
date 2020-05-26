namespace Funbites.Patterns.ObjectPooling {
    public interface IPoolListener {
        void OnLeavePool();
        void OnReturnToPool();
        bool enabled { get; }
    } 
}
