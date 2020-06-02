namespace Funbites.Patterns {
    using UnityEngine.SceneManagement;
    public class SceneLoader : SingletonScriptableObject<SceneLoader> {

        public void LoadScene(string sceneName) {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        public void LoadScene(Scene scene) {
            SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
        }

        public void LoadScene(int index) {
            SceneManager.LoadScene(index, LoadSceneMode.Single);
        }
    }
}