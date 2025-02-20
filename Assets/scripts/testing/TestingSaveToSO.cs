using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;



public class TestingSaveToSO : MonoBehaviour
{
    private string saveSeparator = "newitem";
    [SerializeField] private GameObject inv;
    [SerializeField] inventory invent;
    [SerializeField] TestingSO saveinv;


    public void Awake()
    {








    }

    public void Update()
    {




    }
    private void load()
    {
        if (saveinv.vrinventorysave != null && saveinv.vrinventorysave.Count > 0)
        {


            if (inventory.vrinventorylist != null && inventory.vrinventorylist.Count > 0)
            {
                inventory.vrinventorylist.Clear();
                foreach (vritem givenvritem in saveinv.vrinventorysave)
                {
                    inventory.vrinventorylist.Add(givenvritem);

                }






            }



        }

    }




    private void save()
    {


        if (inventory.vrinventorylist != null && inventory.vrinventorylist.Count > 0)
        {

            foreach (vritem givenvritem in inventory.vrinventorylist)
            {
                saveinv.vrinventorysave.Add(givenvritem);




            }





        }
    }


}


[CreateAssetMenu]
public class TestingSO : ScriptableObject
{
    public List<vritem> vrinventorysave = new List<vritem>();





}


