using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gridint
{


    int width;
    int height;
    int[,] gridArray;
    float cellsize;
    public Gridint(int width, int height, float cellsize)
    {
        this.width = width;
        this.height = height;
        this.cellsize = cellsize;
        gridArray = new int[width, height];


        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                // Debug.Log(x);
                // Debug.Log(y);
                //PSEUDO GameObject.Instantiate(gridcell,GetWorldPossition(x,y),Quaternion.identity);
                Debug.DrawLine(GetWorldPossition(x, y), GetWorldPossition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPossition(x, y), GetWorldPossition(x + 1, y), Color.white, 100f);

                Debug.Log(gridArray[x, y]);
                Debug.Log(x + " " + y);

            }
        }



    }


    private Vector3 GetWorldPossition(int x, int y)
    {
        return new Vector3(x, y) * cellsize;


    }


    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellsize);
        y = Mathf.FloorToInt(worldPosition.y / cellsize);


    }

    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height) gridArray[x, y] = value;




    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        GetXY(worldPosition, out int x, out int y);
        SetValue(x, y, value);


    }



    public int getValue(int x, int y)
    {

      
            return gridArray[x, y];
        



    }
    public int getValue(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / cellsize);
        int y = Mathf.FloorToInt(worldPosition.y / cellsize);
        Debug.Log(x + " " + y);
        return gridArray[x, y];


    }

}
