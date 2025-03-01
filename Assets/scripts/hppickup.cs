using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hppickup : MonoBehaviour
{
    [SerializeField] int healingvalue = 25;
    private void OnTriggerEnter2D(Collider2D other)
    {
        player1 player = other.GetComponent<player1>();
        Debug.Log("trigger");
        if(player != null)
        {
            player.Health += healingvalue;
            uihealth.instance.setvalue(player.health / (float)player.maxHP);
            Debug.Log(player.Health);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject playerobjct = collision.gameObject;
        player1 player2 = playerobjct.GetComponent<player1>();
        Debug.Log("trigger");
        if (player2 != null)
        {
            player2.Health += healingvalue;
            uihealth.instance.setvalue(player2.health / (float)player2.maxHP);
            Debug.Log(player2.Health);

        }

    }
}
