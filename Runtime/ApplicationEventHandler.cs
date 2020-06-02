namespace Funbites.Patterns
{
    public class ApplicationEventHandler : SingletonScriptableObject<ApplicationEventHandler>
    {
        [UnityEngine.SerializeField]
        private bool m_debug = true;

        public void Quit()
        {
            UnityEngine.Application.Quit();
        }

        public void HideMouseCursor()
        {
            if (m_debug) Debugging.Logger.Log("Hiding mouse cursor");
            UnityEngine.Cursor.visible = false;
        }

        public void ShowMouseCursor()
        {
            if (m_debug) Debugging.Logger.Log("Showing mouse cursor");
            UnityEngine.Cursor.visible = true;
        }
    }
}