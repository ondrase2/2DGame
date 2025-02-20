using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
public class BuildingManager : MonoBehaviour
{

    public static BuildingManager Instance;
    [SerializeField] Transform player;
    [SerializeField] GameObject laserturret;
    [SerializeField] GameObject missileturret;
    [SerializeField] GameObject inventorygo;
    Vector3 position;
    public string itemToSpawn = "LaserTurret";
    // Start is called before the first frame update
    void Awake()
    {
        Instance= this;


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            
            
            
            position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z= 0;
            if (Vector3.Distance(position, player.transform.position) < 2 && !EventSystem.current.IsPointerOverGameObject())
            {
                inventory inventoryscript = inventorygo.GetComponent<inventory>();
                vritem thisvritem =inventoryscript.findinlist(itemToSpawn);
                if (thisvritem != null)
                {
                    inventoryscript.removefromlist(thisvritem);
                    
                    
                    switch (itemToSpawn)

                    {
                        case "LaserTurret":
                            Instantiate(laserturret, position, Quaternion.identity);
                            break;


                        case "MissileTurret":
                            Instantiate(missileturret, position, Quaternion.identity); ;
                            break;


                        //case :
                            //buildingmanager.itemtospawn = case3
                          //  break;

                    }




                }
                else if (thisvritem == null) Debug.Log(itemToSpawn+"item not present in inv");


                
                


            }
            else Debug.Log("building too far or ever UI");
        
        }
        if (Input.GetKeyDown(KeyCode.N)) { 
            
        
        
        
        }
    }
    public void SpawnTurret() 
    {
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        if (Vector3.Distance(position, player.transform.position) < 2 && !EventSystem.current.IsPointerOverGameObject())
        {
            inventory inventoryscript = inventorygo.GetComponent<inventory>();
            vritem thisvritem = inventoryscript.findinlist(itemToSpawn);
            if (thisvritem != null)
            {
                inventoryscript.removefromlist(thisvritem);


                switch (itemToSpawn)

                {
                    case "LaserTurret":
                        Instantiate(laserturret, position, Quaternion.identity);
                        break;


                    case "MissileTurret":
                        Instantiate(missileturret, position, Quaternion.identity); ;
                        break;


                        //case :
                        //buildingmanager.itemtospawn = case3
                        //  break;

                }




            }
            else if (thisvritem == null) Debug.Log(itemToSpawn + "item not present in inv");






        }
        else Debug.Log("building too far or ever UI");
    }

}
