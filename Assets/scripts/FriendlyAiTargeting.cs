using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FriendlyAiTargeting : MonoBehaviour
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

        target enemy = other.GetComponent<target>();
        if (enemy != null)
        {

            if (shootcooldown <= 0)
            {

                if (Vector3.Distance(transform.position, enemy.transform.position) < inrange)
                {




                    Vector3 direction = new Vector3();
                    direction = (enemy.transform.position - transform.position);
                    direction.Normalize();
                    Vector3 anglevector = new Vector3(0, 0, GetAngleFromVector(direction));
                    Quaternion angle = Quaternion.identity;
                    angle.eulerAngles= anglevector;
                    Debug.Log(anglevector);
                    // quater projrotation = 
                    projectile projectilescript = Instantiate(bullet, transform.position,angle).GetComponent<projectile>();
                    projectilescript.lauch(direction, 300);
                    shootcooldown = 100;


                }






            }
            if (shootcooldown > 0) shootcooldown -= 1;
        }
    }

    float GetAngleFromVector(Vector3 dir) {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        n -= 90;
        if (n < 0) n += 360;
        //rotate 90deg
        Debug.Log(n);
        return n;
        
    }
}
