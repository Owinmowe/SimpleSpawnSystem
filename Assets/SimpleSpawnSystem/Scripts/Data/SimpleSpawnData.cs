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

        public bool UseUnityTerrain;

        public Terrain UnityTerrain;

        public LayerMask TerrainLayer;

        public float TerrainOffset;

        public bool AlignWithUnityTerrain;

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

            UseUnityTerrain = false;

            UnityTerrain = default;

            TerrainLayer = default;

            TerrainOffset = .5f;

            AlignWithUnityTerrain = true;

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

            UseUnityTerrain = data.UseUnityTerrain;

            UnityTerrain = data.UnityTerrain;

            TerrainLayer = data.TerrainLayer;

            TerrainOffset = data.TerrainOffset;

            AlignWithUnityTerrain = data.AlignWithUnityTerrain;

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
