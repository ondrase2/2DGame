using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInWorld : MonoBehaviour
{

    [SerializeField] GameObject gridworldcell;
    
    Grid<GridCell> grid;
    //Gridint gridint;

    // Start is called before the first frame update
    void Start()
    {
       
        Vector3 origin = new Vector3(10, 5, 0);
        grid = new Grid<GridCell>(5, 4, 20,origin);
      //  gridint = new Gridint(5, 4, 20);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        
        {
            
           
            Vector3 worldpoz =   Camera.main.ScreenToWorldPoint(Input.mousePosition);


            //grid.SetValue(worldpoz,56);
            GridCell testingcell =grid?.getGridObject(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            testingcell?.IncreaseValue(15);
             Debug.Log(testingcell?.GetValue());


        }
    }
}
