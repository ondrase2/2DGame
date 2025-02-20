using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class inventory : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteImporter = new Sprite[25];
    [SerializeField] private string[] spriteDescriptor = new string[25];
    public static List<item> inventorylist;
    public static List<vritem> vrinventorylist =new List<vritem>();
    public static int items = 0;
    
    [SerializeField] private GameObject uiinventoryobj;

    [SerializeField] private GameObject itemprefab;
    [SerializeField] private GameObject ItemInfoPopUponawakePrefab;
    public static GameObject ItemInfoPopUpPrefab;
    public static GameObject ItemInfoPopUpInstance;
    // Start is called before the first frame update
    void Awake()
    {
        if (ItemInfoPopUpPrefab == null) ItemInfoPopUpPrefab = ItemInfoPopUponawakePrefab;
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.inventorylist == null) inventory.inventorylist = new List<item>();
        
        
        
        
        
        if (Input.GetKeyDown(KeyCode.P))

        {
            foreach (item iteminloop in inventorylist)

            {
                Debug.Log("item present");
                Debug.Log(inventorylist.IndexOf(iteminloop));
                if (iteminloop == null) Debug.Log("item is null");
                Debug.Log(iteminloop.transform.localPosition);
            }

        }
        
        if (Input.GetKeyDown(KeyCode.I))
        { 
            if(uiinventory.instance == null) 
            { 
                uiinventoryobj.SetActive(true);
                drawui();


            
            
            }
            
            else if(uiinventory.instance.gameObject.active == true)
            uiinventory.instance.gameObject.SetActive(false);
            else {
                
                
                uiinventory.instance.gameObject.SetActive(true); 
                destroyui();
                drawui();
            
            
            }
        
        
        }
        if(Input.GetKeyDown(KeyCode.N)) {addtolist("LaserTurret",1); }
        if (Input.GetKeyDown(KeyCode.B))
        {
            


        }

    }


    public void additem(string type, int amount,int vrindex)
    {
        Sprite sprite= null;
        int i = 0;
        for(; i<spriteDescriptor.Length; i++)
        {
            if (spriteDescriptor[i] == type)
            {
                sprite = spriteImporter[i];
                break;
            }

        
        }
        if(sprite == null) {sprite = spriteImporter[0]; }
        Vector3 invslot = uiinventory.instance.transform.position;
        
       // Debug.Log(invslot);
        GameObject itemininventory = Instantiate(itemprefab, invslot,uiinventory.instance.transform.rotation,uiinventory.instance.transform);
        item itemscript = itemininventory.GetComponent<item>();
        itemscript.setproperties(vrindex, type, amount);
        Debug.Log("additem done");
        

    }

    public void addtolist(string type, int amount)
    {
        vritem thisvritem= new vritem();
        vrinventorylist.Add(thisvritem);
        Debug.Log(vrinventorylist.Count);
        thisvritem.setproperties(type, amount);
        if (uiinventory.instance != null)
        {
            if (uiinventory.instance.gameObject.activeSelf == true)
            {
                destroyui();
                drawui();
            }
        }

    }
    public void removeitem(item givenitem)
    {
        Destroy(givenitem.gameObject);



    }
    
    public void removefromlist(vritem itemtoremove)
    {
        vritem tihsvritem = itemtoremove;
        vrinventorylist.Remove(tihsvritem);
        if (uiinventory.instance != null)
        {
            if (uiinventory.instance.gameObject.activeSelf == true)
            {
                destroyui();
                drawui();
            }
        }



    }

    public vritem findinlist(string type)
    {

        vritem vritemtoreturn = null;
        for (int i=0;i< vrinventorylist.Count;i++)
        {
            if (vrinventorylist[i].itemtype == type)
            {


                 vritemtoreturn= vrinventorylist[i];
                break;
            }

            

            
        }


        return vritemtoreturn;


    }




    public void drawui()
    {
        if(inventory.vrinventorylist != null && inventory.vrinventorylist.Count > 0 )
        {
            foreach(vritem givenvritem in inventory.vrinventorylist)
            {
                Debug.Log(inventory.vrinventorylist.IndexOf(givenvritem));
                additem(givenvritem.itemtype, givenvritem.amount,inventory.vrinventorylist.IndexOf(givenvritem));




            }



        }



    }
    public void destroyui()
    {
        if (inventory.inventorylist != null && inventory.inventorylist.Count > 0)
        {

            foreach (item givenitem in inventory.inventorylist)
            {
                
                
                Destroy(givenitem.gameObject);

            }
            inventory.inventorylist.Clear();
        }
    }



}
