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
            foreach (var spawnData in configuration.SpawnsData)
            {

                if(spawnData.PossibleSpawnPrefabs.Length < 1) 
                {
                    Debug.LogWarning("Prefab list for spawn named '" + spawnData.SpawnName + "' not found. Spawn will not be made");
                    continue;
                }

                GameObject spawnGO = new GameObject();

                if (spawnData.ChildOfManager) spawnGO.transform.parent = transform;
                spawnGO.transform.position = spawnData.Position;
                
                var spawn = spawnGO.AddComponent<SimpleSpawn>();
                spawn.SetSpawn(spawnData);

            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion

    }

}
