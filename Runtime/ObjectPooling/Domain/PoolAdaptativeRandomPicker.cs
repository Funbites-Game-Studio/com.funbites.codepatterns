namespace Funbites.Patterns.ObjectPooling {
    [UnityEngine.CreateAssetMenu(menuName = "Funbites/Object Pooling/Adaptative Random Pool Picker")]
    public class PoolAdaptativeRandomPicker : Probability.AdaptativeRandomPicker<Pool, WeightedPoolElement> {
        [UnityEngine.SerializeField]
        private System.Collections.Generic.List<WeightedPoolElement> m_pools = null;

        public override System.Collections.Generic.List<WeightedPoolElement> ElementsList => m_pools;
    }
}
