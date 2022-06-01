#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

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

            var script = (SimpleSpawnManager)target;

            if (Application.isPlaying)
            {


                GUIStyle titleStyle = new GUIStyle();
                titleStyle.fontSize = 20;
                titleStyle.normal.textColor = Color.white;

                GUIStyle spawnsStyle = new GUIStyle();
                spawnsStyle.fontSize = 15;

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
            else if (script.SaveFile)
            {

                GUILayout.Space(20);

                if (GUILayout.Button("Save to file"))
                {
                    script.WriteDataToSaveFile();
                }

                GUILayout.Space(10);

                if (!script.SaveFile.Empty && GUILayout.Button("Load from file"))
                {
                    script.ReadDataFromSaveFile();
                    Repaint();
                    GUI.FocusControl(null);
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