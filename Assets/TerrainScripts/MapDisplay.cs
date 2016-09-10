
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapDisplay : MonoBehaviour
{

    public MeshFilter meshFilter;
    public MeshCollider meshCollider;
    public MeshRenderer meshRenderer;
    public GameObject heightMap;
    public MeshData meshData;
    public Shader shader;
    public PointLight pointLight;

    DiamondSquare ds;
    Color32[] cols;
    
    //2^k number
    public int size;
    public float scalar;

    // + - Range of the random value 
    public float rRng;

    //divisor by which random range decreases
    public float dec;

    int mapSize;

    public bool autoUpdate;
    public int numColors;
    public float[,] dsArray;



    //use for selecting the colors
    public List<Color> MapColours = new List<Color>();

    

    // Use this for initialization
    void Start()
    {
        setup();
    }

    void Update()
    {
        meshRenderer.material.SetColor("_PointLightColor", this.pointLight.color);
        meshRenderer.material.SetVector("_PointLightPosition", this.pointLight.GetWorldPosition());
    }


    public void setup()
    {
        meshRenderer.material.shader = shader;
        DiamondSquare ds = new DiamondSquare(size, rRng, dec);
        
        mapSize = ds.dsArray.GetLength(0);
        cols = new Color32[mapSize * mapSize];
        dsArray = ds.dsArray;

        //Loop generates colors and inverseLerps all the values
        //so they are between 0 and 1
        for (int y = 0; y < ds.mapSize; y++) {
            for (int x = 0; x < ds.mapSize; x++) {

                dsArray[x,y] = Mathf.InverseLerp(ds.maxVal, ds.minVal, dsArray[x,y]);
                cols[x + y * mapSize] = getColor(dsArray[x, y], MapColours);

            }
        }
        meshData = MeshGenerator.GenMeshData(dsArray, scalar, MapColours);
        drawMesh(meshData);

    }




    /// <summary>
    /// Recalculates the colors for the height map
    /// </summary>
    public void recalColors()
    {

        for(int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                cols[x + y * size] = getColor(dsArray[x, y], MapColours);
            }
        }
    }





/// <summary>
/// Smooths a terrain by averaging 8 points around
/// every point
/// </summary>
    public void smoothTerrain()
    {
        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                
                smoothVertex(x, y);
            }
        }
    }





    /// <summary>
    /// Draws a mesh using the heightMap component, takes a meshData
    /// 
    /// </summary>
    /// <param name="meshData">Takes a meshData as defined in MeshGenerator</param>
    /// <param name="texture">Takes a Texture2D of colours (change this)</param>
    /// 

    public void drawMesh(MeshData meshData)
    {
        this.meshData = meshData;
        Mesh mesh = meshData.CreateMesh();
        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;
        
    }

    /// <summary>
    /// Smoothes the height map by averaging all values
    /// with their 8 surrounding values
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void smoothVertex(int x, int y)
    {
        float tl, t, tr, l, r, bl, b, br;

        tl = getValW(x - 1, y - 1);
        t = getValW(x, y - 1);
        tr = getValW(x + 1, y - 1);
        l = getValW(x - 1, y);
        r = getValW(x + 1, y);
        bl = getValW(x - 1, y + 1);
        b = getValW(x, y + 1);
        br = getValW(x + 1, y + 1);

        float avg = (tl + t + tr + l + r + bl + b + br) / 8.0f;
        setVal(x, y, avg);
    }


    /// <summary>
    /// Gets a color from the mapColors list, c is betweeen 0 and 1
    /// </summary>
    /// <param name="c"></param>
    /// <param name="MapColours"></param>
    /// <returns></returns>
    public static Color getColor (float c, List<Color> MapColours)
    {

        int idx = Mathf.RoundToInt(MapColours.Count * c);

        if(idx == MapColours.Count) {
            idx -= 1;
        }

        return MapColours[idx];

    }






    private void setVal(int x, int y, float val)
    {

        dsArray[x, y] = val; ;

    }



    private float getValW(int x, int y)
    {
        return dsArray[wrap(x), wrap(y)];
    }

    private int wrap(int a)
    {
        if (a > (mapSize - 1)) {
            return a - (mapSize - 1);
        }
        else if (a < 0) {
            return (mapSize - 1) + a;
        }

        return a;
    }


    

}


