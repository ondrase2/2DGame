using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagezone : MonoBehaviour
{
    private int damagecounter=0 ;
    [SerializeField] int damagevalue;
    private void OnTriggerStay2D(Collider2D other)
    {
        player1 player = other.GetComponent<player1>();
        if (player != null)
        {
            if (damagecounter == 25)
            {
                player.Health -= damagevalue;
                uihealth.instance.setvalue((player.health/(float)player.maxHP));
                Debug.Log(player.Health);

            damagecounter= 0 ;
            }
            else { damagecounter++; }
        }
    }

}
