using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using SimpleSpawnSystem.Data;

namespace SimpleSpawnSystem.Core
{

    public class SimpleSpawnManager : MonoBehaviour
    {

        #region Public Fields

        static public SimpleSpawnManager instance = default;

        public int GetSpawnsCount => currentSpawns.Count;

        public SimpleSpawnSaveFile SaveFile => saveFile;

        #endregion

        #region Serializable Fields

        [SerializeField] private List<SimpleSpawnData> startingSpawnData = new List<SimpleSpawnData>() { new SimpleSpawnData() };

        [SerializeField] private SimpleSpawnSaveFile saveFile = default;

        #endregion

        #region Private Fields

        private List<SimpleSpawn> currentSpawns = new List<SimpleSpawn>();

        private Dictionary<Spawnable, ObjectPool<Spawnable>> poolsDictionary = new Dictionary<Spawnable, ObjectPool<Spawnable>>();

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

            if(startingSpawnData == null) 
            {
                Debug.LogError("Spawn starting configuration not set in Simple Spawn Manager.");
                return;
            }

            for (int i = 0; i < startingSpawnData.Count; i++)
            {
                SimpleSpawnData spawnData = startingSpawnData[i];
                if (spawnData.PossibleSpawnPrefabs.Length < 1) 
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

        public Spawnable GetSpawnable(bool pooled, Spawnable prefab) 
        {
            if (pooled) 
            {
                ObjectPool<Spawnable> pool;
                if (poolsDictionary.TryGetValue(prefab, out pool))
                {
                    return pool.Get();
                }
                else 
                {
                    Debug.LogWarning("Spawnable not in pool. Instantiating normally.");
                }
                
            }
            return Instantiate(prefab);
        }

        public void DestroySpawnable(bool pooled, Spawnable prefab, Spawnable instance) 
        {
            if (pooled) 
            {
                ObjectPool<Spawnable> pool;
                if (poolsDictionary.TryGetValue(prefab, out pool))
                {
                    pool.Release(instance);
                    return;
                }
                else
                {
                    Debug.LogWarning("Instance not in pool. Destroying normally.");
                }
            }
            Destroy(instance.gameObject);
        }

        public int CreateSpawn(SimpleSpawnData data) 
        {

            GameObject spawnGO = new GameObject();

            var spawn = spawnGO.AddComponent<SimpleSpawn>();
            spawn.SetManager(this);
            spawn.SetSpawnData(data);

            spawn.OnMonoDestroyed += RemoveSpawnFromList;

            if (data.UsePool) 
            {

                foreach (var prefab in data.PossibleSpawnPrefabs)
                {

                    if (!poolsDictionary.ContainsKey(prefab)) 
                    {
                        var pool = new ObjectPool<Spawnable>(() => 
                        {
                            return Instantiate(prefab);
                        }, 
                        obj =>
                        {
                            obj.gameObject.SetActive(true);
                        }, 
                        obj =>
                        {
                            obj.gameObject.SetActive(false);
                        }, 
                        obj =>
                        {
                            Destroy(obj.gameObject);
                        }, 
                        true,
                        data.PoolDefaultCapacity, 
                        data.PoolMaxSize
                        );
                        poolsDictionary.Add(prefab, pool);
                    }

                }

            }

            currentSpawns.Add(spawn);

            return currentSpawns.Count - 1;

        }

        public void WriteDataToSaveFile() 
        {

            var dataToSave = new List<SimpleSpawnData>();
            foreach (var data in startingSpawnData)
            {
                dataToSave.Add(new SimpleSpawnData(data));
            }

            saveFile.SpawnsData.Clear();
            saveFile.SpawnsData = dataToSave;

        }

        public void ReadDataFromSaveFile() 
        {

            startingSpawnData.Clear();
            foreach (var data in saveFile.SpawnsData)
            {
                startingSpawnData.Add(new SimpleSpawnData(data));
            }

        }

        #endregion

        #region Private Methods

        private void RemoveSpawnFromList(SimpleSpawn simpleSpawn) => currentSpawns.Remove(simpleSpawn);

        #endregion

    }

}
