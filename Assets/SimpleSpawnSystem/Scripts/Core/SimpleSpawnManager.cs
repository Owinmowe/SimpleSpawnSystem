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
