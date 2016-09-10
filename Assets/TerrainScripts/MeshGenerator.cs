using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MeshGenerator : MonoBehaviour {


    
    /// <summary>
    /// Creates a MeshData object based on a height map array dsArray
    /// 
    /// </summary>
    /// <param name="dsArray"></param>
    /// <param name="scalar"></param>
    /// <param name="MapColors">Colors for the height map</param>
    /// <param name="Textured"></param>
    /// <returns></returns>
    public static MeshData GenMeshData(float[,] dsArray, float scalar, List<Color> MapColors)
    {

        int mapSize = dsArray.GetLength(0);


        //Centers the mesh in the middle of the screen
        float resetX = (mapSize - 1) / -2f;
        float resetY = (mapSize - 1) / 2f;

        MeshData meshData = new MeshData(mapSize);

        int idx = 0;


        for (int y = 0; y < mapSize; y++) {
            for (int x = 0; x < mapSize; x++) {

                //adds the triangles at the correct positions, square by square
                if (y < mapSize - 1 && x < mapSize - 1) {
                    meshData.genTri(idx, idx + mapSize + 1, idx + mapSize);
                    meshData.genTri(idx + mapSize + 1, idx, idx + 1);
                }
                meshData.vertices[idx] = new Vector3(resetX + x, dsArray[x, y], resetY - y);
                idx++;
            }
        }
        Color32 currentColor = new Color();

        idx = 0;


        while(idx < meshData.triVertices.Length) {
 
            meshData.triVertices[idx] = meshData.vertices[meshData.triangles[idx]];
            meshData.newTriangles[idx] = idx;

            if (idx % 3==0) {
                currentColor = MapDisplay.getColor(meshData.triVertices[idx].y, MapColors);
            }
    
            meshData.colors[idx] = currentColor;
            idx += 1;
        }
        return meshData;

    }

}


