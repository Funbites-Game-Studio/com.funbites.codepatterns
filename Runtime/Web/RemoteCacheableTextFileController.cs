namespace Funbites.Patterns.Web
{
    using Sirenix.OdinInspector;
    using UnityEngine;
    public class RemoteCacheableTextFileController : MonoBehaviour {
        [SerializeField, Required]
        private RemoteCacheableTextFile m_remoteCacheableTextFile = null;

        public void ClearLocalFile() {
            m_remoteCacheableTextFile.ClearLocalFile();
        }
    }
}