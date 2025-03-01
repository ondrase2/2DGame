using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Grid<T> where T: IGridcell , new()
{ 
   
    int width;
    int height;
    public T[,] gridArray;
    float cellsize;
    Vector3 originPosition;
    public Grid(int width, int height, float cellsize,Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellsize = cellsize;
        gridArray = new T[width, height];
        this.originPosition = originPosition;


        for (int x = 0;  x < gridArray.GetLength(0); x++){
            for(int y = 0; y< gridArray.GetLength(1); y++)
            {
                
                Debug.Log(x);
                Debug.Log(y);
                //PSEUDO GameObject.Instantiate(gridcell,GetWorldPossition(x,y),Quaternion.identity);
                Debug.DrawLine(GetWorldPossition(x, y), GetWorldPossition(x, y + 1),Color.white,100f);
                Debug.DrawLine(GetWorldPossition(x, y), GetWorldPossition(x+1, y),Color.white, 100f);

                Debug.Log(gridArray[x, y]);
                Debug.Log(x +" " +y);
                gridArray[x, y] =  new  T();
                 gridArray[x, y].init(x, y);





            }
        }

        
        
    }


    private Vector3 GetWorldPossition(int x, int y) {
        return new Vector3(x,y)*cellsize+originPosition;
    
    
    }

   
    public void GetXY(Vector3 worldPosition,out int x , out int y)
    {
        x= Mathf.FloorToInt((worldPosition.x-originPosition.x)/cellsize);
        y = Mathf.FloorToInt((worldPosition.y-originPosition.y)/cellsize);


    }

    public void SetValue(int x, int y, T value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height) gridArray[x, y] = value;




    }

    public void SetValue(Vector3 worldPosition, T value)
    {
        GetXY(worldPosition, out int x, out int y);
        SetValue(x, y, value);


    }



    public T getGridObject(int x , int y)
    {
        
        if(gridArray[x, y] !=null)
            return gridArray[x, y];
        else return default(T);



    }
    public T getGridObject(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition.x - originPosition.x) / cellsize);
        int y = Mathf.FloorToInt((worldPosition.y - originPosition.y) / cellsize);
        Debug.Log(x +" "+ y);
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            if (gridArray[x, y] != null)
                return gridArray[x, y];
            else return default(T);
        }
        else return default(T);
    }

}
public interface IGridcell

{

    public abstract void IncreaseValue(int value);
   
    public abstract int GetValue();


    public abstract void init(int x, int y);
   




}