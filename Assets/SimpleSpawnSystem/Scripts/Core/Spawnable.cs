using SimpleSpawnSystem.Data;
using UnityEngine;

namespace SimpleSpawnSystem.Core
{

    public class Spawnable : MonoBehaviour
    {

        #region Public Fields

        public System.Action<Spawnable> OnGotReleased;
        public SimpleSpawnData Data { get; set; }

        #endregion

        #region Serializable Fields

        #endregion

        #region Private Fields

        private Spawnable prefabReference = null;

        private RaycastHit[] terrainRaycastHitResults = new RaycastHit[10];

        private const float terrainDistanceCheck = 2f;

        #endregion

        #region Unity Methods

        #endregion

        #region Public Methods

        public void ApplySpawnModifiers(SimpleSpawnData data, Spawnable prefabReference) 
        {

            Data = data;

            if (data.UseUnityTerrain && data.UnityTerrain != null)
            {

                Vector3 position = transform.position;
                float terrainY = data.UnityTerrain.SampleHeight(position);
                position.y = terrainY + data.TerrainOffset;
                transform.position = position;

                if (data.AlignWithUnityTerrain) 
                {

                    Physics.RaycastNonAlloc(transform.position + Vector3.up, Vector3.down, terrainRaycastHitResults, terrainDistanceCheck, data.TerrainLayer);

                    if (terrainRaycastHitResults != null)
                    {
                        foreach (var raycastHit in terrainRaycastHitResults)
                        {
                            transform.up = raycastHit.normal;
                            break;
                        }
                    }

                }

            }

            if(data.UseUnitParentTransform && data.UnitParentTransform != null) 
            {
                transform.parent = data.UnitParentTransform;
            }

            this.prefabReference = prefabReference;

        }

        public void ReleaseObject() 
        {

            if (Data.UsePoolingSystem)
            {
                SimpleSpawnManager.instance.DestroySpawnable(Data.UsePoolingSystem, prefabReference, this);
            }
            else Destroy(gameObject);

            OnGotReleased?.Invoke(this);

        }


        #endregion

        #region Private Methods

        #endregion

    }

}
