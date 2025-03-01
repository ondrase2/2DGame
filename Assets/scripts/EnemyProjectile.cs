using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    Rigidbody2D projrigidbody;


    // Start is called before the first frame update
    void Awake()
    {
        projrigidbody = GetComponent<Rigidbody2D>();



    }

    // Update is called once per frame
    void Update()
    {
        

    }

    float GetAngleFromVector(Vector3 dir) { 
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;

    
    
    }

    public void lauch(Vector3 direction, float force)
    {
        
        
        
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(direction));
        
        if ((direction.x != 0) || (direction.y != 0))
        {
            direction = direction.normalized;
        }
        else
        {
            direction.Set(1, 0, 0);
        }

       



        projrigidbody.AddForce(direction * force);

    
    
    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player1 player = collision.GetComponent<player1>();
        if (player != null)
        {
            player.takedamage(15);
            Destroy(gameObject);



        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        player1 player = collision.gameObject.GetComponent<player1>();
        if (player != null)
        {
            player.takedamage(15);
            Destroy(gameObject);



        }




    }
}



