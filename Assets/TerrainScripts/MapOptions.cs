using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MapDisplay))]
public class MapOptions : Editor
{

    public override void OnInspectorGUI()
    {
        MapDisplay display = (MapDisplay)target;

        if (DrawDefaultInspector())
        {
            if (display.autoUpdate)
            {
                display.drawMesh(MeshGenerator.GenMeshData(display.dsArray, display.scalar, display.MapColours));
            } 
        }


        if (GUILayout.Button("Smooth"))
        {
            display.smoothTerrain();
            display.drawMesh(MeshGenerator.GenMeshData(display.dsArray, display.scalar, display.MapColours));
        }

        GUILayout.Box(new GUIContent("Choose Colors"));

        if(GUILayout.Button("Generate Colors"))
        {
            display.recalColors();
            display.drawMesh(MeshGenerator.GenMeshData(display.dsArray, display.scalar, display.MapColours));
        }


    }
}
