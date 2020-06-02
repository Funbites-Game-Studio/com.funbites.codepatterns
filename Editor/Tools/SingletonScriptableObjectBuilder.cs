namespace Funbites.Patterns.Editor
{
    using Sirenix.OdinInspector.Editor;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    using Funbites.UnityUtils;
    using System;
    using Sirenix.OdinInspector;

    public class SingletonScriptableObjectBuilder : OdinEditorWindow
    {
        [ShowInInspector]
        private List<Type> uncreatedSingletons;

        [MenuItem("Tools/Funbites/Singleton ScriptableObjectBuilder")]
        private static void OpenWindow()
        {
            GetWindow<SingletonScriptableObjectBuilder>().Show();
        }

        protected override void Initialize()
        {
            Refresh();
        }

        private bool HasUncreatedSingletons => uncreatedSingletons != null && uncreatedSingletons.Count > 0;
        [Button]
        private void Refresh()
        {
            uncreatedSingletons = new List<Type>();
            var types = AppDomain.CurrentDomain.FindAllDerivedTypesOfGeneric(typeof(SingletonScriptableObject<>));
            foreach (var type in types)
            {
                var instance = Resources.Load(type.Name);
                if (instance == null)
                {
                    uncreatedSingletons.Add(type);
                }
            }
        }

        [Button]
        [EnableIf("HasUncreatedSingletons")]
        private void CreateAssets()
        {
            foreach (var type in uncreatedSingletons)
            {
                SingletonScriptableObjectFunctions.CreateAsset(type.Name, CreateInstance(type));
            }
            uncreatedSingletons.Clear();
        }
    }
}