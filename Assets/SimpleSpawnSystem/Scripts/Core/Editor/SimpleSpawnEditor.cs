#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using SimpleSpawnSystem.Utility;

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

                bool value = script.Spawning;

                GUIStyle labelStyle = new GUIStyle();

                labelStyle.fontSize = 20;
                labelStyle.alignment = TextAnchor.MiddleCenter;

                GUILayout.Space(20);

                GUILayout.Label("Spawn Configurations", labelStyle);
                

                script.Spawning = GUILayout.Toggle(value, "Auto Spawning");

                GUILayout.Space(10);

                if (GUILayout.Button("Spawn Unit")) 
                {
                    script.SpawnUnit();
                }

                GUILayout.Space(20);

                script.OrderType = (SpawnOrderType)EditorGUILayout.EnumPopup(script.OrderType);

                GUILayout.Space(20);

                script.AreaType = (SpawnAreaType)EditorGUILayout.EnumPopup(script.AreaType);

                switch (script.AreaType)
                {
                    case SpawnAreaType.OnCenterPoint:
                        break;

                    case SpawnAreaType.OnRandomAreaCircle:
                        var circleProperty = serializedObject.FindProperty("currentRandomCircleSpawnSize");
                        EditorGUILayout.PropertyField(circleProperty);
                        break;

                    case SpawnAreaType.OnRandomAreaSphere:
                        var sphereProperty = serializedObject.FindProperty("currentRandomSphereSpawnSize");
                        EditorGUILayout.PropertyField(sphereProperty);
                        break;

                    default:
                        break;
                }

                GUILayout.Space(20);

                script.Timer.CurrentTimerType = (TimerType)EditorGUILayout.EnumPopup(script.Timer.CurrentTimerType);

                var timerSerializedObject = new SerializedObject(script.Timer);

                switch (script.Timer.CurrentTimerType)
                {
                    case TimerType.Fixed:
                        
                        EditorGUILayout.PropertyField(timerSerializedObject.FindProperty("fixedTime"));
                        break;

                    case TimerType.RandomBetweenTwoFloats:
                        EditorGUILayout.PropertyField(timerSerializedObject.FindProperty("minRandomTime"));
                        EditorGUILayout.PropertyField(timerSerializedObject.FindProperty("maxRandomTime"));
                        break;

                    default:
                        break;
                }

                timerSerializedObject.ApplyModifiedProperties();
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
