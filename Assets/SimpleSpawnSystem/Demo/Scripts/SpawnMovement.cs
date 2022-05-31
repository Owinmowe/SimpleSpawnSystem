using UnityEngine;

namespace SimpleSpawnSystem.Demo
{

    public class SpawnMovement : MonoBehaviour
    {

        #region Public Fields

        #endregion

        #region Serializable Fields

        [SerializeField] private float movementRange = 100f;

        [SerializeField] private float movementSpeed = 10f;

        [SerializeField] private float movementStartingOffset = -50f;

        #endregion

        #region Private Fields

        #endregion

        #region Unity Methods

        private void Update()
        {
            Vector3 position = transform.position;

            position.z = Mathf.PingPong(Time.time * movementSpeed, movementRange) + movementStartingOffset;

            transform.position = position;

        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion

    }

}
