using UnityEngine;

namespace SimpleSpawnSystem.Utility
{

    public class SimpleSpawnTimer : MonoBehaviour
    {

        #region Public Fields

        public System.Action OnTimerReached;

        public TimerType CurrentTimerType 
        { 
            set 
            {

                currentTimerType = value;

                switch (currentTimerType)
                {
                    case TimerType.Fixed:
                        currentTimeCheckDelegate = CheckTimeFixed;
                        break;

                    case TimerType.RandomBetweenTwoFloats:
                        CurrentRandomSpawnTime = Random.Range(MinRandomTime, MaxRandomTime);
                        currentTimeCheckDelegate = CheckTimeRandomNumbers;
                        break;

                    default:
                        Debug.LogWarning("Timer type not implemented. Timer Type set to fixed.");
                        currentTimerType = TimerType.Fixed;
                        currentTimeCheckDelegate = CheckTimeFixed;
                        break;
                }

                if (ResetTimeOnTypeChange) currentTime = 0;

            } 
            get 
            {

                return currentTimerType;

            }
        }

        public float FixedTime 
        {
            set 
            {
                fixedTime = value;
            }
            get 
            {
                return fixedTime;
            }
        }

        public float MinRandomTime 
        {
            set 
            {
                if (MinRandomTime < 0)
                {
                    minRandomTime = 0;
                    Debug.LogWarning("Minimun random time can't be below 0. Time set to 0");
                }
                else if (MinRandomTime > MaxRandomTime) 
                {
                    minRandomTime = MaxRandomTime;
                    Debug.LogWarning("Minimun random time can't be over the maximun random time. Time set to the maximun random time.");
                }
                else 
                {
                    minRandomTime = value;
                }
            } 
            get 
            {
                return minRandomTime;
            }
        }

        public float MaxRandomTime
        {
            set
            {
                if (maxRandomTime < MinRandomTime)
                {
                    maxRandomTime = MinRandomTime;
                    Debug.LogWarning("Maximun random time can't be over the minimun random time. Time set to the minimun random time.");
                }
                else
                {
                    maxRandomTime = value;
                }
            }
            get
            {
                return maxRandomTime;
            }
        }

        public float CurrentRandomSpawnTime { get; private set; }

        public bool ResetTimeOnTypeChange { get; set; } = true;

        #endregion

        #region Serializable Fields

        [SerializeField] [HideInInspector] private float currentTime = 0;

        [SerializeField] [HideInInspector] private float minRandomTime = 1;

        [SerializeField] [HideInInspector] private float maxRandomTime = 2;

        [SerializeField] [HideInInspector] private float fixedTime = 1;

        #endregion

        #region Private Fields

        private TimerType currentTimerType = TimerType.Fixed;

        private delegate bool CurrentTimeCheckDelegate();

        private CurrentTimeCheckDelegate currentTimeCheckDelegate = default;

        #endregion

        #region Unity Methods

        private void Update()
        {

            UpdateTime();

        }

        #endregion

        #region Public Methods

        public void SetRandomTimeUnsafe(float min, float max) 
        {
            minRandomTime = min;
            maxRandomTime = max;
        }

        #endregion

        #region Private Methods

        private void UpdateTime()
        {

            currentTime += Time.deltaTime;

            while (currentTimeCheckDelegate()) OnTimerReached?.Invoke();

        }

        private bool CheckTimeFixed() 
        {

            if(currentTime > FixedTime) 
            {
                currentTime -= FixedTime;
                return true;
            }

            return false;

        }

        private bool CheckTimeRandomNumbers() 
        {

            if (currentTime > CurrentRandomSpawnTime)
            {
                currentTime -= CurrentRandomSpawnTime;
                CurrentRandomSpawnTime = Random.Range(MinRandomTime, MaxRandomTime);
                return true;
            }

            return false;

        }

        #endregion

    }

}
