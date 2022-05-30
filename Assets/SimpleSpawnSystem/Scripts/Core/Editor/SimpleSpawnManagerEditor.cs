using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

#if UNITY_EDITOR

namespace SimpleSpawnSystem.Core
{

    [CustomEditor(typeof(SimpleSpawnManager))]
    public class SimpleSpawnManagerEditor : Editor
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

                var script = (SimpleSpawnManager)target;

                GUIStyle titleStyle = new GUIStyle();
                titleStyle.fontSize = 20;
                titleStyle.normal.textColor = Color.white;

                GUIStyle spawnsStyle = new GUIStyle();
                spawnsStyle.fontSize = 10;

                GUILayout.Space(20);
                GUILayout.Label("Current Spawns", titleStyle);
                GUILayout.Space(20);
                for (int i = 0; i < script.GetSpawnsCount; i++)
                {
                    var spawn = script.GetSpawn(i);
                    spawnsStyle.normal.textColor = spawn.Data.SpawnColor;
                    GUILayout.Label("Name: " + spawn.name, spawnsStyle);
                    GUILayout.Label("Order Type: " + spawn.Data.OrderType.ToString(), spawnsStyle);
                    GUILayout.Label("Area Type: " + spawn.Data.AreaType.ToString(), spawnsStyle);
                    GUILayout.Label("Timer Type: " + spawn.Timer.CurrentTimerType.ToString(), spawnsStyle);

                    GUILayout.Space(20);
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