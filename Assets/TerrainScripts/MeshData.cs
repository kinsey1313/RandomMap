using UnityEngine;
using System.Collections;

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector3[] triVertices;
    public int[] newTriangles;
    public Color32[] colors;

    int triIndex;


    /// <summary>
    /// Creates the arrays necessary for a mesh
    /// </summary>
    /// <param name="size"></param>
    public MeshData(int size)
    {

        //Both vertices and triVertices defined. I wanted 
        //to have triangles colored seperately for a 'retro' look
        //to do this, each triangle had to have 3 of its own vertices
        triIndex = 0;
        vertices = new Vector3[size * size];
        triangles = new int[(size - 1) * (size - 1) * 6];
        triVertices = new Vector3[(size - 1) * (size - 1) * 6];
        newTriangles = new int[(size - 1) * (size - 1) * 6];
        colors = new Color32[triangles.Length];
    }


    /// <summary>
    /// Adds a triangle to the triangles array, each int is 
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    public void genTri(int a, int b, int c)
    {
        triangles[triIndex] = a;
        triangles[triIndex + 1] = b;
        triangles[triIndex + 2] = c;
        triIndex += 3;
    }


    /// <summary>
    /// Creates a mesh from the data in MeshData
    /// </summary>
    /// <returns></returns>
    public Mesh CreateMesh()
    {

        Mesh mesh = new Mesh();
        mesh.vertices = triVertices;
        mesh.triangles = newTriangles;
        mesh.colors32 = colors;
        mesh.RecalculateNormals();
        return mesh;
    }


}
