namespace Funbites.Patterns {
    [System.Serializable]
    public class ResourceAssetReference<T> where T : UnityEngine.Object {
        
        [UnityEngine.SerializeField, Sirenix.OdinInspector.ReadOnly]
        private string m_assetResourcesPath = "";
        
        private T loadedAsset;
        private bool hasLoaded = false;
        
        public string AssetResourcesPath => m_assetResourcesPath;
        [Sirenix.OdinInspector.ShowInInspector, 
            Sirenix.OdinInspector.AssetsOnly, 
            Sirenix.OdinInspector.Required]
        public T Asset
        {
            get
            {
                if (!hasLoaded) {
                    Funbites.Debugging.AssertionHelper.Assert(!string.IsNullOrEmpty(m_assetResourcesPath), $"Invalid resource path: {m_assetResourcesPath}");
                    loadedAsset = UnityEngine.Resources.Load<T>(m_assetResourcesPath);
                    Funbites.Debugging.AssertionHelper.AssertNotNull(loadedAsset, $"Asset with path '{m_assetResourcesPath}' was not found");
                    hasLoaded = true;
                }
                return loadedAsset;
            }
#if UNITY_EDITOR
            private set
            {
                Reset();
                m_assetResourcesPath = UnityEditor.AssetDatabase.GetAssetPath(value);
                int indexOfResources = m_assetResourcesPath.IndexOf(Funbites.UnityUtils.Constants.ResourcesFolderName);
                m_assetResourcesPath = "";
                Funbites.Debugging.AssertionHelper.Assert(indexOfResources > 0, "Asset must be in Resources Folder", value);
                indexOfResources += Funbites.UnityUtils.Constants.ResourcesFolderName.Length;
                m_assetResourcesPath = m_assetResourcesPath.Substring(indexOfResources, m_assetResourcesPath.LastIndexOf(".") - indexOfResources);
            }
#endif
        }

        public void Reset() {
            loadedAsset = null;
            hasLoaded = false;
        }

        
    }
}