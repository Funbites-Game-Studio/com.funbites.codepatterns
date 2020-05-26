namespace Funbites.Patterns.ObjectPooling {
    public class ParticleStopPoolNotifier : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField, Sirenix.OdinInspector.Required]
        private Poolable m_poolable = null;

        public void OnParticleSystemStopped()
        {
            m_poolable.ReturnToPool();
        }
    }
}