#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace SimpleSpawnSystem.Core
{

    [CustomEditor(typeof(SimpleSpawn))]
    public class SimpleSpawnEditor : Editor
    {

        #region Public Fields

        #endregion

        #region Serializable Fields

        #endregion

        #region Private Fields

        #endregion

        #region Unity Methods

        public override void OnInspectorGUI()
        {

            DrawDefaultInspector();

            if (Application.isPlaying) 
            {

                var script = (SimpleSpawn)target;

                if(GUILayout.Button("Spawn Unit")) 
                {
                    script.SpawnUnit();
                }


            }


        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion

    }

}

#endif
