using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyTargeting : MonoBehaviour
{



    float shootcooldown;
    [SerializeField] GameObject bullet;
    [SerializeField] float inrange;
    // Start is called before the first frame update
    void Awake()
    {


        shootcooldown = 0;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        player1 player = other.GetComponent<player1>();
        if (player != null)
        {
            
            if (shootcooldown <= 0)
            {
                
                if (Vector3.Distance(transform.position, player.transform.position) < inrange)
                {
                    
                    
                    

                        Vector3 direction = new Vector3();

                        direction = (player.transform.position - transform.position);
                        direction.Normalize();
                        EnemyProjectile projectilescript = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<EnemyProjectile>();
                        projectilescript.lauch(direction, 300);
                        shootcooldown = 100;

                    
                }

                

                    


            }
            if(shootcooldown>0) shootcooldown -= 1;
        }
    }
}
