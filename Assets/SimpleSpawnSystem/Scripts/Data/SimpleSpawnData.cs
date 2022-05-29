using UnityEngine;
using SimpleSpawnSystem.Utility;
using SimpleSpawnSystem.Core;

namespace SimpleSpawnSystem.Data
{

    [System.Serializable]
    public class SimpleSpawnData
    {

        #region Public Fields

        public Color SpawnColor = Color.white;

        public string SpawnName = "Spawn Name";

        public bool AutoStartSpawning = false;

        public bool UseRuntimeTransformAsPosition = false;

        public Transform SpawnCenterTransform = default;

        public Vector3 PositionOffsetFromManager = Vector3.zero;

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

        public SimpleSpawnData(SimpleSpawnData data)
        {

            SpawnColor = data.SpawnColor;

            SpawnName = data.SpawnName;

            AutoStartSpawning = data.AutoStartSpawning;

            UseRuntimeTransformAsPosition = data.UseRuntimeTransformAsPosition;

            SpawnCenterTransform = data.SpawnCenterTransform;

            PositionOffsetFromManager = data.PositionOffsetFromManager;

            PossibleSpawnPrefabs = data.PossibleSpawnPrefabs;

            OrderType = data.OrderType;

            AreaType = data.AreaType;

            TimerType = data.TimerType;

            MinRandomTime = data.MinRandomTime;

            MaxRandomTime = data.MaxRandomTime;

            FixedTime = data.FixedTime;

            CircleRadius = data.CircleRadius;

            SphereRadius = data.SphereRadius;

        }

        #endregion

        #region Private Methods



        #endregion

    }

}
