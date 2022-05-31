using UnityEngine;
using System.Collections;
using SimpleSpawnSystem.Core;

namespace SimpleSpawnSystem.Demo
{

    public class SpawnDestroyer : MonoBehaviour
    {

        #region Public Fields

        #endregion

        #region Serializable Fields

        [SerializeField] private float destroyTime = 5f;

        #endregion

        #region Private Fields

        private Spawnable spawnable = default;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            spawnable = GetComponent<Spawnable>();
        }

        private void OnEnable()
        {
            StartCoroutine(DestroyBallCoroutine());
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        private IEnumerator DestroyBallCoroutine() 
        {
            float t = 0;
            while(t < destroyTime) 
            {
                t += Time.deltaTime;
                yield return null;
            }
            spawnable.ReleaseObject();
        }

        #endregion

    }

}
