using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObstacleData))]
public class Obstacle : Editor
{
    public override void OnInspectorGUI()
    {
        ObstacleData data = (ObstacleData)target;



        int width = 10;
        
        int height = 10;

        EditorGUILayout.LabelField("Click to toggle ");

        for (int y = height - 1; y >= 0; y--)



        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < width; x++)
            {


                int index = y * width + x;
                data.obstacles[index] = GUILayout.Toggle(data.obstacles[index], "");
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUI.changed)


        {
            EditorUtility.SetDirty(data);
        }
    }
}
