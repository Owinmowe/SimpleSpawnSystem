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

        public System.Action<SimpleSpawn> OnMonoDestroyed;

        public SimpleSpawnData Data 
        {
            private set 
            {

                data = value;

                gameObject.name = data.SpawnName;


                //Calls custom setter/getters

                Timer.CurrentTimerType = data.TimerType;
                Timer.FixedTime = data.FixedTime;
                Timer.SetRandomTimeUnsafe(data.MinRandomTime, data.MaxRandomTime);

                OrderType = data.OrderType;
                AreaType = data.AreaType;

                Spawning = data.AutoStartSpawning;

            }
            get 
            {
                return data;
            }
        }

        public SimpleSpawnTimer Timer { private set; get; }

        public SpawnOrderType OrderType 
        {
            set 
            {

                data.OrderType = value;

                switch (data.OrderType)
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
                        data.OrderType = SpawnOrderType.Sequential;
                        CurrentSequentialSpawn = 0;
                        currentSpawnAction = SpawnSequentialUnit;
                        break;
                }

            }
            get 
            {

                return data.OrderType;

            }
        }

        public SpawnAreaType AreaType 
        {

            set 
            {

                data.AreaType = value;

                switch (data.AreaType)
                {
                    
                    case SpawnAreaType.OnCenterPoint:
                        currentSpawnLocation = SpawnLocationOnCenter;
                        break;
                    
                    case SpawnAreaType.OnRandomAreaCircle:
                        currentSpawnLocation = SpawnLocationRandomCircle;
                        break;
                    
                    case SpawnAreaType.OnRandomAreaSphere:
                        currentSpawnLocation = SpawnLocationRandomSphere;
                        break;
                
                    default:
                        Debug.LogWarning("Area Type not implemented. Spawn area set to center point.");
                        data.AreaType = SpawnAreaType.OnCenterPoint;
                        currentSpawnLocation = SpawnLocationOnCenter;
                        break;

                }


            }

            get 
            {

                return data.AreaType;

            }

        }

        public bool Spawning 
        {
            set 
            {

                if (value == subscribedToSpawnEvent) return;

                if(value) Timer.OnTimerReached += SpawnUnit;
                else Timer.OnTimerReached -= SpawnUnit;

                subscribedToSpawnEvent = value;
                data.AutoStartSpawning = value;

            }
            get 
            {

                return data.AutoStartSpawning;

            }
        }

        public Spawnable[] PossibleSpawns 
        {
            private set 
            {
                data.PossibleSpawnPrefabs = value;
            }
            get 
            { 
                return data.PossibleSpawnPrefabs; 
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
                data.CircleRadius = value;
            }
            get 
            {
                return data.CircleRadius;
            }
        }

        public float CurrentRandomSphereSpawnSize 
        {
            set 
            {
                data.SphereRadius = value;
            }
            get 
            {
                return data.SphereRadius;
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

        public List<Spawnable> SpawnedUnits => spawnedUnits;

        #endregion

        #region Serializable Fields

        [SerializeField] [HideInInspector] private SimpleSpawnData data = default;

        [SerializeField] [HideInInspector] private int currentSequentialSpawn = 0;

        [SerializeField] [HideInInspector] private List<int> currentRandomNotRepeatedList = new List<int>();

        #endregion

        #region Private Fields

        private delegate Spawnable SpawnAction();

        private SpawnAction currentSpawnAction;

        private delegate Vector3 GetSpawnLocation();

        private GetSpawnLocation currentSpawnLocation;

        private bool subscribedToSpawnEvent = false;

        private List<Spawnable> spawnedUnits = new List<Spawnable>();

        private SimpleSpawnData previousData = default;

        private SimpleSpawnManager creatorManager = default;

        private bool dataChanged = false;

        #endregion

        #region Unity Methods

        private void Awake()
        {

            Timer = gameObject.AddComponent<SimpleSpawnTimer>();

        }

        private void Update()
        {

            Timer.UpdateTime();

            if (dataChanged)
            {
                dataChanged = false;
                SetSpawnData(data);
            }

        }

        private void OnValidate()
        {

            dataChanged = data != previousData;

        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {

            Gizmos.color = data.SpawnColor;
            Gizmos.DrawSphere(transform.position, 1.0f);

        }

        private void OnDrawGizmosSelected()
        {

            Handles.color = data.SpawnColor;
            Gizmos.color = data.SpawnColor;

            for (int i = 0; i < spawnedUnits.Count; i++)
            {

                if (spawnedUnits != null)
                {
                    Handles.DrawLine(transform.position, spawnedUnits[i].transform.position);
                }

            }

            switch (Data.AreaType)
            {

                case SpawnAreaType.OnCenterPoint:
                    break;

                case SpawnAreaType.OnRandomAreaCircle:
                    Handles.DrawWireDisc(transform.position, Vector3.up, Data.CircleRadius);
                    break;

                case SpawnAreaType.OnRandomAreaSphere:
                    Gizmos.DrawWireSphere(transform.position, Data.SphereRadius);
                    break;

                default:
                    break;
            }

        }

#endif

        private void OnDestroy() => OnMonoDestroyed?.Invoke(this);

        #endregion

        #region Public Methods

        public void SetManager(SimpleSpawnManager manager) => creatorManager = manager;

        public void SetSpawnData(SimpleSpawnData data)
        {

            Data = new SimpleSpawnData(data);
            previousData = new SimpleSpawnData(data);

        }

        public void SpawnUnit()
        {

            var spawnable = currentSpawnAction();
            spawnable.ApplySpawnModifiers(data);

        }

        public void DestroyAllUnits() 
        {
            for (int i = 0; i < spawnedUnits.Count; i++)
            {

                if (spawnedUnits != null)
                {
                    Destroy(spawnedUnits[i].gameObject);
                }

            }
            spawnedUnits.Clear();
        }

        #endregion

        #region Private Methods

        #region Spawn Methods

        private Spawnable SpawnRandomUnit() 
        {

            int unitIndex = Random.Range(0, PossibleSpawns.Length);

            var go = Instantiate(PossibleSpawns[unitIndex], currentSpawnLocation(), Quaternion.identity, transform);

            spawnedUnits.Add(go);

            return go;

        }

        private Spawnable SpawnSequentialUnit() 
        {

            if(CurrentSequentialSpawn >= PossibleSpawns.Length) CurrentSequentialSpawn = 0;

            var go = Instantiate(PossibleSpawns[CurrentSequentialSpawn], currentSpawnLocation(), Quaternion.identity, transform);

            spawnedUnits.Add(go);

            CurrentSequentialSpawn++;

            return go;

        }

        private Spawnable SpawnRandomNotRepeatedUnit() 
        {

            var go = Instantiate(PossibleSpawns[CurrentRandomNotRepeatedList[0]], currentSpawnLocation(), Quaternion.identity, transform);

            spawnedUnits.Add(go);

            CurrentRandomNotRepeatedList.RemoveAt(0);

            if (CurrentRandomNotRepeatedList.Count == 0) RecreateSpawnIndexList();

            return go;

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
