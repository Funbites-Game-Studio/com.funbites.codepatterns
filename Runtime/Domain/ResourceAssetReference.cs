using Sirenix.OdinInspector;
using UnityEngine;
using Funbites.Utils.Debugging;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace CommonReferenceables {
    [System.Serializable]
    public class ResourceAssetReference<T> where T : Object {
        private T loadedAsset;
        [SerializeField, ReadOnly]
        private string m_assetResourcesPath;
        private bool hasLoaded = false;
        public string AssetResourcesPath => m_assetResourcesPath;
        [ShowInInspector, AssetsOnly, Required]
        public T Asset
        {
            get
            {
                if (!hasLoaded) {
                    AssertionHelper.Assert(!string.IsNullOrEmpty(m_assetResourcesPath), $"Invalid resource path: {m_assetResourcesPath}");
                    loadedAsset = Resources.Load<T>(m_assetResourcesPath);
                    AssertionHelper.AssertNotNull(loadedAsset, $"Asset with path '{m_assetResourcesPath}' was not found");
                    hasLoaded = true;
                }
                return loadedAsset;
            }
#if UNITY_EDITOR
            private set
            {
                Reset();
                m_assetResourcesPath = AssetDatabase.GetAssetPath(value);
                int indexOfResources = m_assetResourcesPath.IndexOf(ResourcesFolderName);
                if (indexOfResources < 0) {
                    Debug.LogError("Asset must be in Resources Folder", value);
                    m_assetResourcesPath = "";
                    return;
                }
                indexOfResources += ResourcesFolderName.Length;
                m_assetResourcesPath = m_assetResourcesPath.Substring(indexOfResources, m_assetResourcesPath.LastIndexOf(".") - indexOfResources);
            }
#endif
        }

        public void Reset() {
            loadedAsset = null;
            hasLoaded = false;
        }

        private const string ResourcesFolderName = @"/Resources/";
    }
}