using UnityEngine;
using SimpleSpawnSystem.Utility;
using SimpleSpawnSystem.Core;

namespace SimpleSpawnSystem.Data
{

    [System.Serializable]
    public class SimpleSpawnData
    {

        #region Public Fields

        public Color SpawnColor;

        public string SpawnName;

        public bool AutoStartSpawning;

        public bool UseCustomParent;

        public Transform CustomParentTransform;

        public Spawnable[] PossibleSpawnPrefabs;

        public SpawnOrderType OrderType;

        public SpawnAreaType AreaType;

        public TimerType TimerType;

        public float MinRandomTime;

        public float MaxRandomTime;

        public float FixedTime;

        public float CircleRadius;

        public float SphereRadius;

        #endregion

        #region Serializable Fields

        #endregion

        #region Private Fields

        #endregion

        #region Public Methods

        public SimpleSpawnData()
        {

            SpawnColor = Color.white;

            SpawnName = "Spawn Name";

            AutoStartSpawning = false;

            UseCustomParent = false;

            CustomParentTransform = default;

            PossibleSpawnPrefabs = default;

            OrderType = SpawnOrderType.Sequential;

            AreaType = SpawnAreaType.OnCenterPoint;

            TimerType = TimerType.Fixed;

            MinRandomTime = 1;

            MaxRandomTime = 2;

            FixedTime = 1;

            CircleRadius = 1.0f;

            SphereRadius = 1.0f;

        }

        public SimpleSpawnData(SimpleSpawnData data)
        {

            SpawnColor = data.SpawnColor;

            SpawnName = data.SpawnName;

            AutoStartSpawning = data.AutoStartSpawning;

            UseCustomParent = data.UseCustomParent;

            CustomParentTransform = data.CustomParentTransform;

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
