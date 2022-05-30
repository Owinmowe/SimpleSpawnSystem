using UnityEngine;
using SimpleSpawnSystem.Data;

namespace SimpleSpawnSystem.Core
{

    public class SimpleSpawnManager : MonoBehaviour
    {

        #region Public Fields

        #endregion

        #region Serializable Fields

        [SerializeField] private SimpleSpawnConfiguration configuration = default;

        #endregion

        #region Private Fields

        #endregion

        #region Unity Methods

        private void Start()
        {

            if(configuration == null) 
            {
                Debug.LogWarning("Spawn configuration scriptable object not set in Simple Spawn Manager.");
                return;
            }

            foreach (var spawnData in configuration.SpawnsData)
            {

                if(spawnData.PossibleSpawnPrefabs.Length < 1) 
                {
                    Debug.LogWarning("Prefab list for spawn named '" + spawnData.SpawnName + "' not found. Spawn will not be made");
                    continue;
                }

                GameObject spawnGO = new GameObject();          
                
                var spawn = spawnGO.AddComponent<SimpleSpawn>();
                spawn.SetManager(this);
                spawn.SetSpawnData(spawnData);

            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion

    }

}
