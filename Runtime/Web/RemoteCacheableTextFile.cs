namespace Funbites.Patterns.Web
{
    using MEC;
    using Sirenix.OdinInspector;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;
    using UnityEngine.Networking;

    [Serializable]
    public class RemoteCacheableTextFile 
    {
        [SerializeField, Required]
        private string m_url = "URL";
        [SerializeField, Required]
        private string m_localCacheRelativePath = "dataCache.json";

        public RemoteCacheableTextFile() { }

        public RemoteCacheableTextFile(string url, string localRelativeCachePath) {
            m_url = url;
            m_localCacheRelativePath = localRelativeCachePath;
        }
        [ShowInInspector, ReadOnly]
        public string FilePath { get; private set; }

        public bool IsLoading { get; private set; }
        private CoroutineHandle loadingCoroutine;

        public void Load(Action<bool> onComplete, Func<string, string> processRemoteData) {
            
            if (IsLoading) {
                throw new Exception("Already Loading");
            } else {
                IsLoading = true;
                loadingCoroutine = Timing.RunCoroutine(LoadCoroutine(onComplete, processRemoteData));
            }
        }

        public string ReadData() {
            if (string.IsNullOrEmpty(FilePath) || IsLoading || !File.Exists(FilePath)) throw new Exception("You must load the file first");
            return File.ReadAllText(FilePath);
        }

        private IEnumerator<float> LoadCoroutine(Action<bool> onComplete, Func<string, string> processRemoteData) {
            IsLoading = true;
            bool success = true;
            FilePath = $"{Application.persistentDataPath}{Path.DirectorySeparatorChar}{m_localCacheRelativePath}";
            if (!File.Exists(FilePath)) {
                Debug.Log($"Loading from web: {m_url}");
                UnityWebRequest www = UnityWebRequest.Get(m_url);
                yield return Timing.WaitUntilDone(www.SendWebRequest());
                if (www.result == UnityWebRequest.Result.Success) {
                    string Data;
                    if (processRemoteData != null)
                    {
                        Data = processRemoteData(www.downloadHandler.text);
                    }
                    else
                    {
                        Data = www.downloadHandler.text;
                    }
                    //Debug.Log($"Saving to local file: {FilePath}");
                    File.WriteAllText(FilePath, Data);
                } else {
                    //Debug.Log(www.error);
                    success = false;
                }
                if (!success)
                {
                    string errorMessage = www.error;
                    www.Dispose();
                    throw new Exception(errorMessage);
                }
                www.Dispose();
            }
            IsLoading = false;
            onComplete?.Invoke(success);
        }
        /* TODO: implement a version to run this on the editor
        private IEnumerator LoadFromWebEditor(Action<bool> onComplete, Func<string, string> processRemoteData)
        {

        }
        */
        [Button]
        public void ClearLocalFile() {
            FilePath = $"{Application.persistentDataPath}{Path.DirectorySeparatorChar}{m_localCacheRelativePath}";
            if (IsLoading)
            {
                IsLoading = false;
                Timing.KillCoroutines(loadingCoroutine);
            }
            if (File.Exists(FilePath)) {
                File.Delete(FilePath);
            }
        }
    }
}