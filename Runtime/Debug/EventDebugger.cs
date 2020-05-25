using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Funbites.Utils.Debugging {
    [CreateAssetMenu(menuName = "Utils/Event Debugger")]
    public class EventDebugger : ScriptableObject {

        public void Log(string message) {
            Debug.Log(message);
        }
    }
}