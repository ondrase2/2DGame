using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldBar : MonoBehaviour
{
    player1 playerscript;
    Transform[] spritearray;
    Transform sprite;

    // Start is called before the first frame update


    private void Awake()
    {

        spritearray = GetComponentsInChildren<Transform>();
        sprite = spritearray[1];
        playerscript = GetComponentInParent<player1>();

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
        if ((playerscript.shield / playerscript.maxshield < 0)) spritex = 0;
        else spritex = playerscript.shield / playerscript.maxshield;

        float spritey = sprite.localScale.y;
        float spritez = sprite.localScale.z;
        sprite.localScale = new Vector3(spritex, spritey, spritez);
        //Debug.Log(sprite.localScale.x);
        //Debug.Log(change);

    }

}
