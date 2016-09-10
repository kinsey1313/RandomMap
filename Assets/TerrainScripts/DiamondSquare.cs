using UnityEngine;
using System.Collections;
/// <summary>
/// Class which creates a diamond square array
/// when initialized. This array is stored in the 
/// dsArray variable.
/// </summary>
public class DiamondSquare{
    

    //2D array containing the randomly seeded map
    public float[,] dsArray;
    //2^size + 1 size map
    public int mapSize;
    float rVal;
    float decMagnitude;

    public float maxVal;
    public float minVal;




    public DiamondSquare(int size, float rVal, float decMagnitude)
    {
        
        this.rVal = rVal;
        this.decMagnitude = decMagnitude;
        this.mapSize = (int)Mathf.Pow(2, size) + 1;
        dsArray = new float[mapSize, mapSize];
        diamondSquareGen(decMagnitude, rVal);
    }

    


    /// <summary>
    /// Generates the diamond sqauare array
    /// by alternating between the diamond step
    /// and the square step with a decreasing
    /// random range. 
    /// </summary>
    private void diamondSquareGen(float decMagnitude, float rVal)
    {


        int stepSize = mapSize - 1;

        float randRange = rVal;

        dsArray[0, 0] = Random.Range(0f, 1f);
        dsArray[0, stepSize] = Random.Range(0f, 1f);
        dsArray[stepSize, stepSize] = Random.Range(0f, 1f);
        dsArray[stepSize, 0] = Random.Range(0f, 1f);


        while (stepSize > 1) {


            for (int y = 0; y < mapSize; y += stepSize) {
                for (int x = 0; x < mapSize; x += stepSize) {
                    if (x == mapSize - 1 || y == mapSize - 1)
                        break;
                    
                    squareStep(x, y, stepSize, randRange);
                }
            }
            

            for (int y = 0; y < mapSize; y += (stepSize / 2)) {
                for (int x = 0; x < mapSize; x += (stepSize / 2)) {

                    if(dsArray[x,y]==0)
                        diamondStep(x, y, stepSize, randRange);
                }
            }

            stepSize = stepSize / 2;
            randRange /= decMagnitude;
        }

    }

    /// <summary>
    /// Performs the diamond step of the algorithm
    /// It takes the center point of a square, splitting it 
    /// into 'diamonds'
    /// as the x,y input.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="stepSize"></param>
    /// <param name="randRange"></param>
    private void diamondStep(int x, int y, int stepSize, float randRange)
    {
        //the midpoint of a 'diamond'
        int halfStep = stepSize / 2;

        float t, b, l, r;

        t = getValW(x, y + halfStep);
        b = getValW(x, y - halfStep);
        l = getValW(x - halfStep, y);
        r = getValW(x + halfStep, y);

        float rand = Random.Range(-randRange, randRange);

        float newVal;
        if(t == 0 || b == 0 || l == 0 || r == 0) {
            newVal = ((t + b + l + r) / 3.0f) + rand;
        }else {
            newVal = ((t + b + l + r) / 4.0f) + rand;
        }

        updateMinMax(newVal);
        setVal(x, y, newVal);
    }

    /// <summary>
    /// Square step of the Diamond Square algorithm.
    /// Takes the top left of the square as x,y input.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="stepSize"></param>
    /// <param name="randRange"></param>
    private void squareStep(int x, int y, int stepSize, float randRange)
    {

        //the middle point of a square
        int halfStep = stepSize / 2;
        float tl, tr, bl, br;

        //calculating each of the corners 
        tl = getVal(x, y);
        tr = getVal(x + stepSize, y);
        bl = getVal(x, y + stepSize);
        br = getVal(x + stepSize, y + stepSize);
        float rand = Random.Range(-randRange, randRange);

        float newVal = ((tl + tr + bl + br) / 4.0f)+rand;
        

        int newX = x + halfStep;
        int newY = x + halfStep;
        setVal(x + halfStep, y + halfStep, newVal);
        updateMinMax(newVal);
        
    }


    /// <summary>
    /// Sets the x,y index of dsArray as Value val
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="val"></param>
    private void setVal(int x, int y, float val)
    {

        dsArray[x, y] = val;;

    }


    /// <summary>
    /// Returns a value at [x,y] from the dsArray
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private float getVal(int x, int y)
    {

        return dsArray[x , y];

    }


    /// <summary>
    /// Gets a wrapped value around the map.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>Value from dsArray</returns>
    private float getValW(int x, int y)
    {
        if(x < 0 || x > (mapSize - 1)) {
            return 0;
        }
        if (y < 0 || y > (mapSize - 1)) {
            return 0;
        }
        return dsArray[x, y];
    }

    /// <summary>
    /// Prints the dsArray for debugging
    /// </summary>
    public void printDs()
    {
        for (int y = 0; y < mapSize; y++) {
            for (int x = 0; x < mapSize; x++) {
                Debug.Log(x + " " + y + " = " + dsArray[x, y]);
            }
        }
    }


    /// <summary>
    /// Updates the min, max of the dsArray.
    /// This is used for normalising values.
    /// </summary>
    /// <param name="v"></param>
    private void updateMinMax(float v) {
        if(v < minVal) {
            minVal = v;
        }
        if(v > maxVal) {
            maxVal = v;
        }

    }



    


}
