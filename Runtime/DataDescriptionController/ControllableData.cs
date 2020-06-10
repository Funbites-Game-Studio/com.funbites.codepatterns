namespace Funbites.Patterns.DDC {
    using System;
    using UnityEngine;
    using Object = UnityEngine.Object;
    public abstract class ControllableData<T> where T : class {
        [NonSerialized]
        private bool isTaken = false;
        public T Controller { get; private set; }

        public abstract void SetupController(T newController);

        public void TakeBy(T newController) {
            if (!isTaken) {
                Controller = newController;
                isTaken = true;
                SetupController(newController);
            } else {
                ThrowException(newController, new Exception($"Controller for instance of {GetType().FullName} is already taken by: {Controller.ToString()}"));
            }    
        }

        private void ThrowException(T controller, Exception e) {
            if (controller is Object) {
                try {
                    Object o = (Object)Convert.ChangeType(controller, typeof(Object));
                    Debug.LogException(e, o);
                } catch (InvalidCastException) {
                    throw e;
                }
            } else {
                throw e;
            }
        }

        public void ReleaseFrom(T currentController) {
            if (isTaken) {
                if (currentController == Controller) {
                    isTaken = false;
                } else {
                    ThrowException(currentController, new Exception($"Controller for instance of { GetType().FullName } is trying to release from but it's not equal the current controller."));
                }
            } else {
                ThrowException(currentController, new Exception($"Controller for instance of { GetType().FullName } is trying to release but it's not taken by any controller."));
            }
        }

    }
}