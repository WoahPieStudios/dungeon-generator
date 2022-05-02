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
            base.OnInspectorGUI();
            
            FloorManager floorManager = (FloorManager) target;
            
            if (GUILayout.Button("Generate Floor")) floorManager.GenerateFloor();
            if (GUILayout.Button("Clear Floor")) floorManager.ClearFloor();
            
            using (new GUILayout.HorizontalScope()){
                
                if (GUILayout.Button("Save Seed")) floorManager.SaveSeed();
                if (GUILayout.Button("Clear Seed")) floorManager.ClearSeed();
            }
        }
    }
}