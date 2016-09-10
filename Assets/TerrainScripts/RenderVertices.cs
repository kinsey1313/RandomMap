using UnityEngine;
using System.Collections;

public class RenderVertices : MonoBehaviour {

    // Use this for initialization
    Mesh mesh;
    Texture2D texture;
	void Start () {



        Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;
        int[] triangles = mesh.triangles;
        Vector3[] vertices = mesh.vertices;
        Vector3[] verticesModified = new Vector3[triangles.Length];
        int[] trianglesModified = new int[triangles.Length];
        Color32 currentColor = new Color32();
        Color32[] colors = new Color32[triangles.Length];

        texture = new Texture2D(triangles.Length, triangles.Length);
        GetComponent<Renderer>().material.mainTexture = texture;

        ///Debug.Log(triangles.Length);
       // Debug.Log(vertices.Length);

        for (int i = 0; i < trianglesModified.Length; i++)
        {
            // Makes every vertex unique
            verticesModified[i] = vertices[triangles[i]];
            trianglesModified[i] = i;
            // Every third vertex randomly chooses new color
            if (i % 3 == 0)
            {
                currentColor = new Color(
                    Random.Range(0.0f, 1.0f),
                    Random.Range(0.0f, 1.0f),
                    Random.Range(0.0f, 1.0f),
                    1.0f
                );
            }
            colors[i] = currentColor;
         }
        
        for(int x = 0; x < 12; x++) {
            verticesModified[x].y = 1;
        }

        // Applyes changes to mesh
        mesh.vertices = verticesModified;
       mesh.triangles = trianglesModified;
        Debug.Log(mesh.colors32.Length);
       mesh.colors32 = colors;

        //Debug.Log(mesh.vertices[0]);
        //Debug.Log(mesh.vertices[1]);


    }




	
	// Update is called once per frame
	void Update () {
	
	}
}
