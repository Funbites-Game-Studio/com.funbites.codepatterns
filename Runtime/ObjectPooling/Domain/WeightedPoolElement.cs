namespace Funbites.Patterns.ObjectPooling {
    [System.Serializable]
    public class WeightedPoolElement : Funbites.Probability.ProbabilityElement<Pool> {
        [UnityEngine.SerializeField]
        private Pool m_pool = null;

        public override Pool Element => m_pool;
    }
}