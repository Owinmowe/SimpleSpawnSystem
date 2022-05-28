using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using SimpleSpawnSystem.Utility;
using SimpleSpawnSystem.Data;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SimpleSpawnSystem.Core
{

    public class SimpleSpawn : MonoBehaviour
    {

        #region Public Fields

        public SimpleSpawnTimer Timer { get; private set; }

        public SpawnOrderType OrderType 
        {
            set 
            {

                currentSpawnOrderType = value;

                switch (currentSpawnOrderType)
                {
                    case SpawnOrderType.Sequential:
                        currentSequentialSpawn = 0;
                        currentSpawnAction = SpawnSequentialUnit;
                        break;

                    case SpawnOrderType.Randomized:
                        currentSpawnAction = SpawnRandomUnit;
                        break;

                    case SpawnOrderType.RandomNotRepeated:
                        RecreateSpawnIndexList();
                        currentSpawnAction = SpawnRandomNotRepeatedUnit;
                        break;

                    default:
                        Debug.LogWarning("Spawn order type not implemented. Spawn order set to sequential.");
                        currentSpawnOrderType = SpawnOrderType.Sequential;
                        currentSequentialSpawn = 0;
                        currentSpawnAction = SpawnSequentialUnit;
                        break;
                }

            }
            get 
            {

                return currentSpawnOrderType;

            }
        }

        public SpawnAreaType AreaType 
        {

            set 
            {

                currentSpawnAreaType = value;

                switch (currentSpawnAreaType)
                {
                    
                    case SpawnAreaType.OnCenterPoint:
                        currentSpawnAreaType = SpawnAreaType.OnCenterPoint;
                        currentSpawnLocation = SpawnLocationOnCenter;
                        break;
                    
                    case SpawnAreaType.OnRandomAreaCircle:
                        currentSpawnAreaType = SpawnAreaType.OnRandomAreaCircle;
                        currentSpawnLocation = SpawnLocationRandomCircle;
                        break;
                    
                    case SpawnAreaType.OnRandomAreaSphere:
                        currentSpawnAreaType = SpawnAreaType.OnRandomAreaSphere;
                        currentSpawnLocation = SpawnLocationRandomSphere;
                        break;
                
                    default:
                        Debug.LogWarning("Area Type not implemented. Spawn area set to center point.");
                        currentSpawnAreaType = SpawnAreaType.OnCenterPoint;
                        currentSpawnLocation = SpawnLocationOnCenter;
                        break;

                }


            }

            get 
            {

                return currentSpawnAreaType;

            }

        }

        public bool Spawning 
        {
            set 
            {

                if (spawning == value) return;

                if(value) Timer.OnTimerReached += SpawnUnit;
                else Timer.OnTimerReached -= SpawnUnit;

                spawning = value;

            }
            get 
            {

                return spawning;

            }
        }

        public Spawnable[] PossibleSpawns { set; get; }

        public bool ShowSpawnsLines { set; get; } = true;

        #endregion

        #region Serializable Fields

        #endregion

        #region Private Fields

        private delegate void SpawnAction();

        private SpawnAction currentSpawnAction;

        private delegate Vector3 GetSpawnLocation();

        private GetSpawnLocation currentSpawnLocation;

        private Color spawnColor = Color.white;

        private bool spawning = false;

        private SpawnOrderType currentSpawnOrderType = SpawnOrderType.Randomized;

        private SpawnAreaType currentSpawnAreaType = SpawnAreaType.OnCenterPoint;

        private int currentSequentialSpawn = 0;

        private List<int> currentRandomNotRepeatedList = new List<int>();

        private List<Spawnable> spawnedUnits = new List<Spawnable>();

        private float currentRandomCircleSpawnLocation = 1.0f;

        private float currentRandomSphereSpawnLocation = 1.0f;

        #endregion

        #region Unity Methods

        private void Awake()
        {

            Timer = gameObject.AddComponent<SimpleSpawnTimer>();

        }

        private void Update()
        {

            Timer.UpdateTime();

        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            


        }

        private void OnDrawGizmosSelected()
        {

            if (!ShowSpawnsLines) return;

            for (int i = 0; i < spawnedUnits.Count; i++)
            {

                if (spawnedUnits[i] != null)
                {
                    Handles.color = spawnColor;
                    Handles.DrawLine(transform.position, spawnedUnits[i].transform.position);
                }
                else spawnedUnits.RemoveAt(i);

            }


        }

#endif

        #endregion

        #region Public Methods

        public void SetSpawn(SimpleSpawnData data) 
        {

            gameObject.name = data.SpawnName;
            spawnColor = data.SpawnColor;
            Spawning = data.AutoStartSpawning;
            PossibleSpawns = data.PossibleSpawnPrefabs;
            OrderType = data.OrderType;

            AreaType = data.AreaType;
            currentRandomCircleSpawnLocation = data.CircleRadius;
            currentRandomSphereSpawnLocation = data.SphereRadius;

            Timer.CurrentTimerType = data.TimerType;
            Timer.FixedTime = data.FixedTime;
            Timer.SetRandomTimeUnsafe(data.MinRandomTime, data.MaxRandomTime);

        }

        public void SpawnUnit() => currentSpawnAction();

        #endregion

        #region Private Methods

        #region Spawn Methods

        private void SpawnRandomUnit() 
        {

            int unitIndex = Random.Range(0, PossibleSpawns.Length);

            var go = Instantiate(PossibleSpawns[unitIndex], currentSpawnLocation(), Quaternion.identity, transform);

            spawnedUnits.Add(go);

        }

        private void SpawnSequentialUnit() 
        {

            if(currentSequentialSpawn >= PossibleSpawns.Length) currentSequentialSpawn = 0;

            var go = Instantiate(PossibleSpawns[currentSequentialSpawn], currentSpawnLocation(), Quaternion.identity, transform);

            spawnedUnits.Add(go);

            currentSequentialSpawn++;

        }

        private void SpawnRandomNotRepeatedUnit() 
        {

            var go = Instantiate(PossibleSpawns[currentRandomNotRepeatedList[0]], currentSpawnLocation(), Quaternion.identity, transform);

            spawnedUnits.Add(go);

            currentRandomNotRepeatedList.RemoveAt(0);

            if (currentRandomNotRepeatedList.Count == 0) RecreateSpawnIndexList();

        }

        private void RecreateSpawnIndexList() 
        {
            currentRandomNotRepeatedList.Clear();

            for (int i = 0; i < PossibleSpawns.Length; i++)
            {
                currentRandomNotRepeatedList.Add(i);
            }

            currentRandomNotRepeatedList = currentRandomNotRepeatedList.OrderBy(number => Random.Range(0, int.MaxValue)).ToList();
        }

        #endregion

        #region Spawn Location Methods

        private Vector3 SpawnLocationOnCenter() => transform.position;

        private Vector3 SpawnLocationRandomCircle()
        {
            Vector2 randomPos = Random.insideUnitCircle * currentRandomCircleSpawnLocation;

            Vector3 finalPos = transform.position;
            finalPos.x += randomPos.x;
            finalPos.z += randomPos.y;

            return finalPos;
        }

        private Vector3 SpawnLocationRandomSphere() 
        {
            return transform.position + Random.insideUnitSphere * currentRandomSphereSpawnLocation;
        }

        #endregion

        #endregion

    }

}
