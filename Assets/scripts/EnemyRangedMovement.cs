using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRangedMovement : MonoBehaviour
{
    Transform[] parenttransformtransformarray;
    [SerializeField] float maxrange;
    [SerializeField] float minrange;
    [SerializeField] float tooclose;
    Transform parenttransform;

    // Start is called before the first frame update
    void Awake()
    {
        parenttransformtransformarray = GetComponentsInParent<Transform>();
        parenttransform = parenttransformtransformarray[1];
        
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
            float distancetoplayer =  Vector3.Distance(parenttransform.position, player.transform.position);
            Vector3 nvectortoplayer = Vector3.Normalize(player.transform.position - parenttransform.position);
            

            if (distancetoplayer < maxrange)
            {
                if(distancetoplayer > minrange) 
                {
                    parenttransform.position += nvectortoplayer*Time.fixedDeltaTime;
                
                
                
                
                
                
                
                
                }
            
                if(distancetoplayer < tooclose) 
                {
                    parenttransform.position -= nvectortoplayer*Time.fixedDeltaTime;
                    
                
                
                
                }




            }
        }
    
    
    }
}