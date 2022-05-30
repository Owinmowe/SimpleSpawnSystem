#if UNITY_EDITOR

using UnityEngine;
using System.IO;
using UnityEditor;

namespace SimpleSpawnSystem.Utility
{

    public class SimpleSpawnSystemAssetManager : EditorWindow
    {

        #region Public Fields

        #endregion

        #region Serializable Fields

        #endregion

        #region Private Fields

        [MenuItem("Simple Spawn System/Create Simple Spawn Manager")]
        static void Init() 
        {

            var existingSpawnManager = FindObjectOfType<Core.SimpleSpawnManager>();

            if (existingSpawnManager) 
            {

                Debug.LogWarning("Simple Spawn Manager already exist in scene. Can't create new one.");

            }
            else 
            {
                var spawnManager = new GameObject();
                spawnManager.name = "Simple Spawn Manager";
                spawnManager.AddComponent<Core.SimpleSpawnManager>();
            }

        }

        [MenuItem("Simple Spawn System/Create Save File")]
        static void CreateConfiguration() 
        {
            Data.SimpleSpawnSaveFile asset = CreateInstance<Data.SimpleSpawnSaveFile>();

            AssetDatabase.CreateAsset(asset, "Assets/Save File.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }

        #endregion

        #region Unity Methods

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods



        #endregion

    }

}

#endif