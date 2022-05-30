using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpawnSystem.Data
{

    [CreateAssetMenu(fileName = "Configuration", menuName = "Simple Spawn System/Configuration", order = 1)]
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
