using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpawnSystem.Data
{

    public class SimpleSpawnSaveFile : ScriptableObject
    {

        #region Public Fields

        public bool Empty 
        {
            set 
            {
                empty = value;
            } 
            get 
            {
                return empty;
            }
        }

        #endregion

        #region Serializable Fields

        #endregion

        #region Private Fields

        private string hash = "";

        private bool empty = true;

        #endregion

        #region Unity Methods

        #endregion

        #region Public Methods

        public void SaveFile(List<SimpleSpawnData> writeData) 
        {

            hash = "";

            foreach (var data in writeData)
            {
                hash += JsonUtility.ToJson(data);
                hash += 'ç';
            }

            empty = false;

            ForceSerialization();

        }

        public List<SimpleSpawnData> LoadFromFile() 
        {

            List<SimpleSpawnData> readData = new List<SimpleSpawnData>();

            string[] hashes = hash.Split('ç');

            for (int i = 0; i < hashes.Length - 1; i++)
            {
                readData.Add(JsonUtility.FromJson<SimpleSpawnData>(hashes[i]));
            }

            return readData;

        }

        #endregion

        #region Private Methods

        private void ForceSerialization()
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        #endregion

    }

}
