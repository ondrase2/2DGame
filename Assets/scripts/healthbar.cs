using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class healthbar : MonoBehaviour
{

    player1 playerscript;
    float maxhealthpoints;
    float healthpoints;
    float healthpercentage;
    [SerializeField]  Transform spritetransform;
    public static healthbar instance;
    // Start is called before the first frame update
    void Awake()
    {
        healthbar.instance = this;
        playerscript = GetComponentInParent<player1>();

    }

    private void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void changehealth(float change)
    {
        spritetransform.localScale = new Vector3(((healthpoints-change)/maxhealthpoints),0.1f,1);
    
    }

    public void changehealthnew(float change)
    {
        float spritey = spritetransform.localScale.y;
        float spritez = spritetransform.localScale.z;
       // Debug.Log(playerscript.maxHP);
       // Debug.Log(playerscript.health);
        
        spritetransform.localScale = new Vector3((playerscript.health / playerscript.maxHP), spritey, spritez);
        //Debug.Log(spritetransform.localScale.x);
       // Debug.Log(change);


    }

}
