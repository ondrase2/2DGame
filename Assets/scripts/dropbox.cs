using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropbox : MonoBehaviour
{


    public string containedloot;
    [SerializeField] GameObject Inventory;
    
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        player1 player = other.GetComponent<player1>();

        if (player != null)
        {
           inventory inventoryscript = Inventory.GetComponent<inventory>();
            inventoryscript.addtolist(containedloot, 1);
            Destroy(gameObject);
                    


        }


    }
}