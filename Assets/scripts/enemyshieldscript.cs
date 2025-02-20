using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyshieldscript : MonoBehaviour
{
    target targetscript;
    Transform[] spritearray;
    Transform sprite;

    // Start is called before the first frame update


    private void Awake()
    {
        
        spritearray = GetComponentsInChildren<Transform>();
        sprite = spritearray[1];
        targetscript = GetComponentInParent<target>();

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changehealth(float change)
    {
        float spritex;
        if ((targetscript.shield / targetscript.maxshield < 0)) spritex = 0;
        else spritex = targetscript.shield / targetscript.maxshield;

        float spritey = sprite.localScale.y;
        float spritez = sprite.localScale.z;
        sprite.localScale = new Vector3(spritex, spritey, spritez);
       // Debug.Log(sprite.localScale.x);
       // Debug.Log(change);

    }

}
