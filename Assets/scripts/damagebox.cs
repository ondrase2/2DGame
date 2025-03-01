using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class damagebox : MonoBehaviour
{
    int damagecounter = 10;
    int damagevalue = 15;
    Vector3 push;
    float timer;
    player1 player;
    bool pushorno = false;
    private void OnTriggerStay2D(Collider2D other)
    {
        
        player = other.GetComponent<player1>();
        if (player != null)
        {
            
            if (damagecounter == 25)
            {
                player.Health -= damagevalue;
                uihealth.instance.setvalue(player.health /(float) player.maxHP);
                Debug.Log(player.Health);
                damagecounter = 0;


                Transform boxcentretransform  = GetComponentInParent(typeof(Transform)) as Transform;
                Vector3 boxcentre = boxcentretransform.position;
                
                push  = (player.transform.position -(boxcentre - player.transform.position));

                pushorno = true;
            }   
            else { damagecounter++; }
        }
    }
    private void FixedUpdate()
    {
        if (player != null) {
            if (pushorno)

            {

                timer += 1;
                player.transform.position = Vector3.Lerp(player.transform.position, push, Time.fixedDeltaTime);
                if (timer > 100)
                {
                    timer = 0;
                    pushorno = false;


                }
            }
        }

    }
}


