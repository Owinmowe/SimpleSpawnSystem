using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpawnSystem.Data
{

    public class SimpleSpawnSaveFile : ScriptableObject
    {

        #region Public Fields

        public List<SimpleSpawnData> SpawnsData = new List<SimpleSpawnData>() { new SimpleSpawnData() };

        #endregion

        #region Serializable Fields

        #endregion

        #region Private Fields

        #endregion

        #region Unity Methods

        #endregion

        #region Public Methods

        public void SaveFile(List<SimpleSpawnData> writeData) 
        {
            SpawnsData.Clear();
            foreach (var data in writeData)
            {
                var newData = new SimpleSpawnData(data);
                SpawnsData.Add(newData);
            }
        }

        public List<SimpleSpawnData> ReadFile() 
        {
            List<SimpleSpawnData> readData = new List<SimpleSpawnData>();
            foreach (var data in SpawnsData)
            {
                var newData = new SimpleSpawnData(data);
                readData.Add(newData);
            }
            return readData;
        }

        #endregion

        #region Private Methods

        #endregion

    }

}
