using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vritem
{


    public string itemtype;
    public int amount;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    

    public void setproperties(string type, int amount)
    {

        this.itemtype = type;
        this.amount = amount;


    }
}
