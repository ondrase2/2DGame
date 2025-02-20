using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Laserray : MonoBehaviour
{

    float timer;
    
    // Start is called before the first frame update
    void Awake()
    {
       
        
        timer = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0)
        {
            Destroy(gameObject);

        }
        else
        {
            timer -= Time.deltaTime;


        }
    
    }
    public void setsize(float scale)
    {
        transform.localScale = new Vector3(transform.localScale.x, scale,transform.localScale.y);


    }

}
