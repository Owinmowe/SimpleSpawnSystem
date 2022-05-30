using System.Collections.Generic;
using UnityEngine;
using SimpleSpawnSystem.Data;

namespace SimpleSpawnSystem.Core
{

    public class SimpleSpawnManager : MonoBehaviour
    {

        #region Public Fields

        static public SimpleSpawnManager instance = default;

        public int GetSpawnsCount => currentSpawns.Count;

        #endregion

        #region Serializable Fields

        [SerializeField] private SimpleSpawnConfiguration configuration = default;

        #endregion

        #region Private Fields

        private List<SimpleSpawn> currentSpawns = new List<SimpleSpawn>();

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if(instance != null) 
            {
                Debug.LogWarning("Only one spawn manager can exist at the same scene. Destroying the newest one.");
                Destroy(gameObject);
            }
            else 
            {
                instance = this;
            }
        }

        private void Start()
        {

            if(configuration == null) 
            {
                Debug.LogError("Spawn configuration scriptable object not set in Simple Spawn Manager.");
                return;
            }

            foreach (var spawnData in configuration.SpawnsData)
            {

                if(spawnData.PossibleSpawnPrefabs.Length < 1) 
                {
                    Debug.LogWarning("Prefab list for spawn named '" + spawnData.SpawnName + "' not found. Spawn will not be made.");
                    continue;
                }

                CreateSpawn(spawnData);

            }
        }

        #endregion

        #region Public Methods

        public SimpleSpawn GetSpawn(int index) 
        {

            if(index < 0 || index > currentSpawns.Count) 
            {
                Debug.LogWarning("Can't get spawn with index " + index + ". Current Spawn index go from 0 to " + (currentSpawns.Count - 1) + ".");
            }

            return currentSpawns[index];

        }

        public int CreateSpawn(SimpleSpawnData data) 
        {

            GameObject spawnGO = new GameObject();

            var spawn = spawnGO.AddComponent<SimpleSpawn>();
            spawn.SetManager(this);
            spawn.SetSpawnData(data);

            spawn.OnMonoDestroyed += RemoveSpawnFromList;

            currentSpawns.Add(spawn);

            return currentSpawns.Count - 1;

        }

        #endregion

        #region Private Methods

        private void RemoveSpawnFromList(SimpleSpawn simpleSpawn) 
        {
            currentSpawns.Remove(simpleSpawn);
        }

        #endregion

    }

}
