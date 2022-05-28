using UnityEngine;
using SimpleSpawnSystem.Utility;
using SimpleSpawnSystem.Core;

namespace SimpleSpawnSystem.Data
{

    [System.Serializable]
    public class SimpleSpawnData
    {

        #region Public Fields

        public string SpawnName = "Spawn Name";

        public bool AutoStartSpawning = false;

        public bool ChildOfManager = false;

        public Vector3 Position = Vector3.zero;

        public Spawnable[] PossibleSpawnPrefabs = default;

        public SpawnOrderType OrderType = SpawnOrderType.Sequential;

        public SpawnAreaType AreaType = SpawnAreaType.OnCenterPoint;

        public TimerType TimerType = TimerType.Fixed;

        public float MinRandomTime = 1;

        public float MaxRandomTime = 2;

        public float FixedTime = 1;

        public float CircleRadius = 1.0f;

        public float SphereRadius = 1.0f;

        #endregion

        #region Serializable Fields

        #endregion

        #region Private Fields

        #endregion

        #region Unity Methods

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods



        #endregion

    }

}
