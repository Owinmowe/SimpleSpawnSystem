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

        public SimpleSpawnTimer Timer 
        {
            private set 
            {
                timer = value;
            } 
            get 
            {
                return timer;
            }
        }

        public SpawnOrderType OrderType 
        {
            set 
            {

                currentSpawnOrderType = value;

                switch (currentSpawnOrderType)
                {
                    case SpawnOrderType.Sequential:
                        CurrentSequentialSpawn = 0;
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
                        CurrentSequentialSpawn = 0;
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

        public Spawnable[] PossibleSpawns 
        {
            private set 
            {
                possibleSpawns = value;
            }
            get 
            { 
                return possibleSpawns; 
            }
        }

        public int CurrentSequentialSpawn
        {
            private set 
            {
                currentSequentialSpawn = value;
            }
            get 
            {
                return currentSequentialSpawn;
            }
        }

        public float CurrentRandomCircleSpawnSize 
        {
            set 
            {
                currentRandomCircleSpawnSize = value;
            }
            get 
            {
                return currentRandomCircleSpawnSize;
            }
        }

        public float CurrentRandomSphereSpawnSize 
        {
            set 
            {
                currentRandomSphereSpawnSize = value;
            }
            get 
            {
                return currentRandomSphereSpawnSize;
            }
        }

        public List<int> CurrentRandomNotRepeatedList 
        { 
            private set 
            {
                currentRandomNotRepeatedList = value;
            }
            get 
            {
                return currentRandomNotRepeatedList;
            }
        }

        public bool ShowSpawnsLines 
        {
            set 
            {
                showSpawnsLines = value;
            }
            get 
            {
                return showSpawnsLines;
            } 
        }

        #endregion

        #region Serializable Fields

        [SerializeField] [HideInInspector] private SimpleSpawnTimer timer = default;

        [SerializeField] [HideInInspector] private Spawnable[] possibleSpawns = default;
                       
        [SerializeField] [HideInInspector] private int currentSequentialSpawn = 0;
                       
        [SerializeField] [HideInInspector] private float currentRandomCircleSpawnSize = 1.0f;
                        
        [SerializeField] [HideInInspector] private float currentRandomSphereSpawnSize = 1.0f;
                       
        [SerializeField] [HideInInspector] private List<int> currentRandomNotRepeatedList = new List<int>();
                        
        [SerializeField] [HideInInspector] private bool showSpawnsLines = true;

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

        private List<Spawnable> spawnedUnits = new List<Spawnable>();


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
            CurrentRandomCircleSpawnSize = data.CircleRadius;
            CurrentRandomSphereSpawnSize = data.SphereRadius;

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

            if(CurrentSequentialSpawn >= PossibleSpawns.Length) CurrentSequentialSpawn = 0;

            var go = Instantiate(PossibleSpawns[CurrentSequentialSpawn], currentSpawnLocation(), Quaternion.identity, transform);

            spawnedUnits.Add(go);

            CurrentSequentialSpawn++;

        }

        private void SpawnRandomNotRepeatedUnit() 
        {

            var go = Instantiate(PossibleSpawns[CurrentRandomNotRepeatedList[0]], currentSpawnLocation(), Quaternion.identity, transform);

            spawnedUnits.Add(go);

            CurrentRandomNotRepeatedList.RemoveAt(0);

            if (CurrentRandomNotRepeatedList.Count == 0) RecreateSpawnIndexList();

        }

        private void RecreateSpawnIndexList() 
        {
            CurrentRandomNotRepeatedList.Clear();

            for (int i = 0; i < PossibleSpawns.Length; i++)
            {
                CurrentRandomNotRepeatedList.Add(i);
            }

            CurrentRandomNotRepeatedList = CurrentRandomNotRepeatedList.OrderBy(number => Random.Range(0, int.MaxValue)).ToList();
        }

        #endregion

        #region Spawn Location Methods

        private Vector3 SpawnLocationOnCenter() => transform.position;

        private Vector3 SpawnLocationRandomCircle()
        {
            Vector2 randomPos = Random.insideUnitCircle * CurrentRandomCircleSpawnSize;

            Vector3 finalPos = transform.position;
            finalPos.x += randomPos.x;
            finalPos.z += randomPos.y;

            return finalPos;
        }

        private Vector3 SpawnLocationRandomSphere() 
        {
            return transform.position + Random.insideUnitSphere * CurrentRandomSphereSpawnSize;
        }

        #endregion

        #endregion

    }

}
