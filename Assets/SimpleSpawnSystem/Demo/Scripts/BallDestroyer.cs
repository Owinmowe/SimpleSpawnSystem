using UnityEngine;
using System.Collections;
using SimpleSpawnSystem.Core;

namespace SimpleSpawnSystem.Demo
{

    public class BallDestroyer : MonoBehaviour
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
            yield return new WaitForSeconds(destroyTime);
            spawnable.DestroyObject();
        }

        #endregion

    }

}
