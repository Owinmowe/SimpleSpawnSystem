using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpawnSystem.Data
{

    public class SimpleSpawnConfiguration : ScriptableObject
    {

        #region Public Fields

        public List<SimpleSpawnData> SpawnsData = new List<SimpleSpawnData>();

        #endregion

        #region Serializable Fields

        #endregion

        #region Private Fields

        private void Awake()
        {
            SpawnsData.Add(new SimpleSpawnData());
        }

        #endregion

        #region Unity Methods

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion

    }

}
