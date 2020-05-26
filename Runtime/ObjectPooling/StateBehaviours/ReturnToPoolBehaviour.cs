namespace Funbites.Patterns.ObjectPooling {
    public class ReturnToPoolBehaviour : UnityEngine.StateMachineBehaviour
    {
        public bool OnExit;
        public Poolable Poolable { get; set; }

        public override void OnStateEnter(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!OnExit) {
                ReturnToPool(animator);
            }
        }

        private void ReturnToPool(UnityEngine.Animator animator)
        {
            if (Poolable != null) {
                Poolable.ReturnToPool();
            } else {
                Poolable = animator.GetComponentInParent<Poolable>();
                if (Poolable != null)
                    Poolable.ReturnToPool();
                else
                    Debugging.Logger.LogError("Trying to return a object to pool, but it does not have a pool: " + animator.gameObject.name, animator.gameObject);
            }
        }

        public override void OnStateExit(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (OnExit)
                ReturnToPool(animator);
        }
    }
}
