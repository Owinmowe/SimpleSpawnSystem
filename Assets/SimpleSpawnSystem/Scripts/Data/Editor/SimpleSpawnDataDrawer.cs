#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using SimpleSpawnSystem.Utility;

namespace SimpleSpawnSystem.Data
{

    [CustomPropertyDrawer(typeof(SimpleSpawnData))]
    public class SimpleSpawnDataDrawer : PropertyDrawer
    {

        #region Public Fields

        #endregion

        #region Serializable Fields

        #endregion

        #region Private Fields

        private float currentYPosition = 0;

        private float startingYPosition = 40;

        private float startingFullPropertyHeight = 375f;

        private float fullPropertyHeight = 375f;

        #endregion

        #region Unity Methods

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            EditorGUI.BeginProperty(position, label, property);

            Color bgColor = property.FindPropertyRelative("SpawnColor").colorValue;
            bgColor.a = .5f;
            GUI.backgroundColor = bgColor;
            GUI.contentColor = Color.white;

            property.isExpanded = EditorGUI.Foldout(new Rect(position.x, position.y, position.width, 18.5f), property.isExpanded, label);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;


            currentYPosition = startingYPosition;

            if (property.isExpanded) 
            {

                var spawnNameRect = new Rect(position.x, position.y + GetCurrentYPosition(30), position.width, GetStandardPropertyHeight());
                EditorGUI.PropertyField(spawnNameRect, property.FindPropertyRelative("SpawnName"));

                var spawnColorRect = new Rect(position.x, position.y + GetCurrentYPosition(30), position.width, GetStandardPropertyHeight());
                EditorGUI.PropertyField(spawnColorRect, property.FindPropertyRelative("SpawnColor"));

                var autoStartRect = new Rect(position.x, position.y + GetCurrentYPosition(30), position.width, GetStandardPropertyHeight());
                EditorGUI.PropertyField(autoStartRect, property.FindPropertyRelative("AutoStartSpawning"));

                var positionRect = new Rect(position.x, position.y + GetCurrentYPosition(30), position.width, GetStandardPropertyHeight());
                EditorGUI.PropertyField(positionRect, property.FindPropertyRelative("Position"));

                var possibleSpawnsProperty = property.FindPropertyRelative("PossibleSpawnPrefabs");
                if (possibleSpawnsProperty.isExpanded) 
                {
                    var spawnableListRect = new Rect(position.x, position.y + GetCurrentYPosition(20, 40, possibleSpawnsProperty.arraySize), position.width, GetStandardPropertyHeight());
                    EditorGUI.PropertyField(spawnableListRect, possibleSpawnsProperty);
                }
                else 
                {
                    var spawnableListRect = new Rect(position.x, position.y + GetCurrentYPosition(30), position.width, GetStandardPropertyHeight());
                    EditorGUI.PropertyField(spawnableListRect, possibleSpawnsProperty);
                }

                var spawnOrderRect = new Rect(position.x, position.y + GetCurrentYPosition(30), position.width, GetStandardPropertyHeight());
                EditorGUI.PropertyField(spawnOrderRect, property.FindPropertyRelative("OrderType"));

                var spawnAreaProperty = property.FindPropertyRelative("AreaType");
                var spawnAreaRect = new Rect(position.x, position.y + GetCurrentYPosition(30), position.width, GetStandardPropertyHeight());
                EditorGUI.PropertyField(spawnAreaRect, spawnAreaProperty);

                var spawnAreaType = GetCurrentSpawnAreaType(spawnAreaProperty.enumValueIndex);

                EditorGUI.indentLevel = 1;

                switch (spawnAreaType)
                {

                    case SpawnAreaType.OnCenterPoint:
                        break;

                    case SpawnAreaType.OnRandomAreaCircle:
                        var circleRect = new Rect(position.x, position.y + GetCurrentYPosition(30), position.width, GetStandardPropertyHeight());
                        EditorGUI.PropertyField(circleRect, property.FindPropertyRelative("CircleRadius"));
                        break;

                    case SpawnAreaType.OnRandomAreaSphere:
                        var sphereRect = new Rect(position.x, position.y + GetCurrentYPosition(30), position.width, GetStandardPropertyHeight());
                        EditorGUI.PropertyField(sphereRect, property.FindPropertyRelative("SphereRadius"));
                        break;

                    default:
                        break;
                }

                EditorGUI.indentLevel = 0;

                var timerProperty = property.FindPropertyRelative("TimerType");
                var timerRect = new Rect(position.x, position.y + GetCurrentYPosition(30), position.width, GetStandardPropertyHeight());
                EditorGUI.PropertyField(timerRect, timerProperty);

                var timerType = GetCurrentTimerType(timerProperty.enumValueIndex);

                EditorGUI.indentLevel = 1;

                switch (timerType)
                {

                    case TimerType.Fixed:
                        var fixedRect = new Rect(position.x, position.y + GetCurrentYPosition(30), position.width, GetStandardPropertyHeight());
                        EditorGUI.PropertyField(fixedRect, property.FindPropertyRelative("FixedTime"));
                        break;

                    case TimerType.RandomBetweenTwoFloats:
                        var minRect = new Rect(position.x, position.y + GetCurrentYPosition(30), position.width, GetStandardPropertyHeight());
                        EditorGUI.PropertyField(minRect, property.FindPropertyRelative("MinRandomTime"));
                        var maxRect = new Rect(position.x, position.y + GetCurrentYPosition(30), position.width, GetStandardPropertyHeight());
                        EditorGUI.PropertyField(maxRect, property.FindPropertyRelative("MaxRandomTime"));
                        break;

                    default:
                        break;
                }

            }

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();

        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded) return 17.5f;

            fullPropertyHeight = startingFullPropertyHeight;

            var possibleSpawnsProperty = property.FindPropertyRelative("PossibleSpawnPrefabs");
            if (possibleSpawnsProperty.isExpanded) 
            {
                int arraySize = possibleSpawnsProperty.arraySize;
                if(arraySize > 1) fullPropertyHeight += 20 * arraySize;
            }

            var spawnAreaProperty = property.FindPropertyRelative("AreaType");
            var spawnAreaType = GetCurrentSpawnAreaType(spawnAreaProperty.enumValueIndex);

            switch (spawnAreaType)
            {
                case SpawnAreaType.OnCenterPoint:
                    break;
                case SpawnAreaType.OnRandomAreaCircle:
                    fullPropertyHeight += 30;
                    break;
                case SpawnAreaType.OnRandomAreaSphere:
                    fullPropertyHeight += 30;
                    break;
                default:
                    break;
            }


            var timerProperty = property.FindPropertyRelative("TimerType");
            var timerType = GetCurrentTimerType(timerProperty.enumValueIndex);

            switch (timerType)
            {
                case TimerType.Fixed:
                    fullPropertyHeight += 30;
                    break;
                case TimerType.RandomBetweenTwoFloats:
                    fullPropertyHeight += 60;
                    break;
                default:
                    break;
            }

            return fullPropertyHeight;
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        private float GetCurrentYPosition(float propertySize) 
        {

            float y = currentYPosition;
            currentYPosition += propertySize;
            return y;

        }

        private float GetCurrentYPosition(float propertySize, float propertyStartingSize, int amount)
        {

            float extraY = propertySize;
            if(amount > 1) 
            {
                propertySize *= amount;
            }

            float y = currentYPosition;
            currentYPosition += propertySize + extraY + propertyStartingSize;
            return y;

        }

        private float GetStandardPropertyHeight() 
        {
            return 20.0f;
        }

        private TimerType GetCurrentTimerType(int enumIndex) 
        {
            return (TimerType)enumIndex;
        }

        private SpawnAreaType GetCurrentSpawnAreaType(int enumIndex) 
        {
            return (SpawnAreaType)enumIndex;
        }

        #endregion

    }

}

#endif