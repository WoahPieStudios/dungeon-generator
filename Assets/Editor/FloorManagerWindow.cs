using DungeonGenerator.Data;
using DungeonGenerator.Managers;
using UnityEditor;
using UnityEngine;

namespace DungeonGenerator.Editor
{
    using Editor = UnityEditor.Editor;

    [CustomEditor(typeof(FloorManager))]
    public class FloorManagerWindow : Editor
    {
        public override void OnInspectorGUI()
        {
            // Hide inspector elements thread: http://answers.unity.com/answers/1297701/view.html
            
            FloorManager floorManager = (FloorManager) target;

            base.OnInspectorGUI();            
            if (GUILayout.Button("Generate Floor")) floorManager.GenerateFloor();
            if (GUILayout.Button("Clear Floor")) floorManager.ClearFloor();
            //if (GUILayout.Button("Save to Scriptable Object")) CreateFloorData(floorManager);
        }

        private void CreateFloorData(FloorManager floorManager)
        {
            FloorData floorData = floorManager.FloorData == null ? CreateInstance<FloorData>() : floorManager.FloorData;

            if (string.IsNullOrEmpty(floorManager.Seed))
            {
                Debug.LogWarning("Please generate a floor first before saving to a scriptable object.");
                return;
            }
            
            floorData.seed = floorManager.Seed;
            floorData.floorSize = floorManager.FloorSize;

            string path = floorManager.FloorData == null ? AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/Data/Floor Data.asset") : $"Assets/Resources/Data/{floorData.name}.asset";

            if(floorManager.FloorData == null) AssetDatabase.CreateAsset(floorData, path);

            AssetDatabase.SaveAssets();
            
            EditorUtility.FocusProjectWindow();

            Selection.activeObject = floorData;
            
            Debug.Log($"Floor data saved at: {path}");
        }
    }
}