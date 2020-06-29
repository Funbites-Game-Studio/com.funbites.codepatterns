namespace Funbites.Patterns {
    using UnityEngine;
    using UnityEngine.SceneManagement;
    [CreateAssetMenu(menuName = "Funbites/Scene Management/Scene Loader")]
    public class SceneLoader : ScriptableObject { //SingletonScriptableObject<SceneLoader> {

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