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

        public bool UsePoolingSystem;

        public int PoolDefaultCapacity;

        public int PoolMaxSize;

        public bool UseUnitParentTransform;

        public Transform UnitParentTransform;

        public bool UseSpawnParentTransform;

        public Transform SpawnParentTransform;

        public bool UseUnityTerrain;

        public Terrain UnityTerrain;

        public LayerMask TerrainLayer;

        public float TerrainOffset;

        public bool AlignWithUnityTerrain;

        public Spawnable[] PossibleSpawnPrefabs;

        public SpawnOrderType OrderType;

        public int SelectedIndex;

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

            UsePoolingSystem = true;

            PoolDefaultCapacity = 10;

            PoolMaxSize = 1000;

            UseUnitParentTransform = false;

            UnitParentTransform = default;

            UseSpawnParentTransform = false;

            SpawnParentTransform = default;

            UseUnityTerrain = false;

            UnityTerrain = default;

            TerrainLayer = default;

            TerrainOffset = .5f;

            AlignWithUnityTerrain = true;

            PossibleSpawnPrefabs = default;

            OrderType = SpawnOrderType.Sequential;

            SelectedIndex = 0;

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

            UsePoolingSystem = data.UsePoolingSystem;

            PoolDefaultCapacity = data.PoolDefaultCapacity;

            PoolMaxSize = data.PoolMaxSize;

            UseUnitParentTransform = data.UseUnitParentTransform;

            UnitParentTransform = data.UnitParentTransform;

            UseSpawnParentTransform = data.UseSpawnParentTransform;

            SpawnParentTransform = data.SpawnParentTransform;

            UseUnityTerrain = data.UseUnityTerrain;

            UnityTerrain = data.UnityTerrain;

            TerrainLayer = data.TerrainLayer;

            TerrainOffset = data.TerrainOffset;

            AlignWithUnityTerrain = data.AlignWithUnityTerrain;

            PossibleSpawnPrefabs = data.PossibleSpawnPrefabs;

            OrderType = data.OrderType;

            SelectedIndex = data.SelectedIndex;

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
