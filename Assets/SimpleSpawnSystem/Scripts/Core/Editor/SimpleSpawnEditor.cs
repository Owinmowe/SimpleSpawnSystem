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

                var dataProperty = serializedObject.FindProperty("data");
                var colorProperty = dataProperty.FindPropertyRelative("SpawnColor");

                GUIStyle labelStyle = new GUIStyle();

                labelStyle.fontSize = 20;
                labelStyle.alignment = TextAnchor.MiddleCenter;
                labelStyle.normal.textColor = colorProperty.colorValue;

                GUILayout.Space(20);

                GUILayout.Label("Spawn Runtime Configuration", labelStyle);

                EditorGUILayout.PropertyField(dataProperty);

                GUILayout.Label("Current Spawns Amount: " + script.SpawnedUnits.Count);

                GUILayout.Space(20);

                if (GUILayout.Button("Spawn Unit"))
                {
                    script.SpawnUnitByMethod();
                }

                if (GUILayout.Button("Destroy All Units"))
                {
                    script.DestroyAllUnits();
                }

                serializedObject.ApplyModifiedProperties();


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
