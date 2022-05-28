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

        public float FixedTime { get; set; } = 1;

        public float MinRandomTime 
        {
            set 
            {
                if (MinRandomTime < 0)
                {
                    minRandomSpawnTime = 0;
                    Debug.LogWarning("Minimun random time can't be below 0. Time set to 0");
                }
                else if (MinRandomTime > MaxRandomTime) 
                {
                    minRandomSpawnTime = MaxRandomTime;
                    Debug.LogWarning("Minimun random time can't be over the maximun random time. Time set to the maximun random time.");
                }
                else 
                {
                    minRandomSpawnTime = value;
                }
            } 
            get 
            {
                return minRandomSpawnTime;
            }
        }

        public float MaxRandomTime
        {
            set
            {
                if (maxRandomSpawnTime < MinRandomTime)
                {
                    maxRandomSpawnTime = MinRandomTime;
                    Debug.LogWarning("Maximun random time can't be over the minimun random time. Time set to the minimun random time.");
                }
                else
                {
                    maxRandomSpawnTime = value;
                }
            }
            get
            {
                return maxRandomSpawnTime;
            }
        }

        public float CurrentRandomSpawnTime { get; private set; }

        public bool ResetTimeOnTypeChange { get; set; } = true;

        #endregion

        #region Serializable Fields

        #endregion

        #region Private Fields

        private float currentTime = 0;

        private float minRandomSpawnTime = 1;

        private float maxRandomSpawnTime = 2;

        private TimerType currentTimerType = TimerType.Fixed;

        private delegate bool CurrentTimeCheckDelegate();

        private CurrentTimeCheckDelegate currentTimeCheckDelegate = default;

        #endregion

        #region Unity Methods

        #endregion

        #region Public Methods

        public void UpdateTime() 
        {

            currentTime += Time.deltaTime;

            if (currentTimeCheckDelegate()) OnTimerReached?.Invoke();

        }

        public void SetRandomTimeUnsafe(float min, float max) 
        {
            minRandomSpawnTime = min;
            maxRandomSpawnTime = max;
        }

        #endregion

        #region Private Methods

        private bool CheckTimeFixed() 
        {

            if(currentTime > FixedTime) 
            {
                currentTime = 0;
                return true;
            }

            return false;

        }

        private bool CheckTimeRandomNumbers() 
        {

            if (currentTime > CurrentRandomSpawnTime)
            {
                currentTime = 0;
                CurrentRandomSpawnTime = Random.Range(MinRandomTime, MaxRandomTime);
                return true;
            }

            return false;

        }

        #endregion

    }

}
