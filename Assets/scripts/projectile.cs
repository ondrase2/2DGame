using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class projectile : MonoBehaviour
{
    target hittarget;
    Rigidbody2D projrigidbody;
    private float timer = 2;
    // Start is called before the first frame update
    void Awake()
    {
      projrigidbody= GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer< 0) {
            Destroy(gameObject);
        }
        timer -= Time.deltaTime;
        float hitdetectsize = 0.5f;
        hittarget = target.Getclosest(transform.position, hitdetectsize);
        
        if(hittarget != null) 
        {

            
            hittarget.takedamage(15);
            Destroy(gameObject);

       }
    }
    public void lauch(Vector3 direction, float force)
    {

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        Debug.Log("colision");
        Destroy(gameObject);
    }

}
