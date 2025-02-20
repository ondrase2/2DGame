using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : IGridcell
{

    int value;
    int x;
    int y;
    float cellsize;
    Vector3 origin;
    public GridCell()
    {
      
        //this.cellsize = cellsize;
      //  this.origin = origin;
       // this.value = value;
    
    }



    public void IncreaseValue(int value)
    {


        this.value += value;
        
        
    }
    public int GetValue()
    {
        return value;



    }

    public void init(int x, int y)
    { 
    this.x = x;
        this.y = y;
    
    }
    

}