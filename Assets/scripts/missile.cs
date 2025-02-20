using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class missile : MonoBehaviour
{
    [SerializeField] int seekRange;
    [SerializeField] int proximityRange;
    [SerializeField] float selfDestructTimer;
    [SerializeField] int enginePower;
    

    target seektarget;
    Rigidbody2D projrigidbody;
    target hittarget;




    // Start is called before the first frame update
    void Awake()
    {
        projrigidbody = GetComponent<Rigidbody2D>();
        projrigidbody.AddRelativeForce(Vector2.up * enginePower*0.5f);

       
       
    }

    // Update is called once per frame
    void Update()
    {
        if (selfDestructTimer < 0)
        {
            Destroy(gameObject);
        }
        selfDestructTimer -= Time.deltaTime;
        float seekdetectsize = seekRange;
        seektarget = target.Getclosest(transform.position, seekdetectsize);

        
        if (seektarget != null)
        {
            float hitdetetsize = proximityRange;
            hittarget = target.Getclosest(transform.position, hitdetetsize);

            if (hittarget != null)
            {

                seektarget.takedamage(15);
                Destroy(gameObject);


            }
        }







    }

    private void FixedUpdate()
    {
       
        if (seektarget!= null)
        {
            //Debug.Log(seektarget);
            Seek(seektarget);



        }
        else if(seektarget == null)
        {
            flyForward();




        }




    }




    private void Seek(target targettoseek)
    {
        Transform targetTransform = targettoseek.transform;
        float angleToTarget = GetAngleFromVectorcorectedforsprite(targetTransform.position - transform.position);
        
       
        projrigidbody.SetRotation(IncrementralRotation(transform.rotation.eulerAngles.z,angleToTarget));
        Vector3 vectortotarget = targetTransform.position - transform.position;
        vectortotarget.Normalize();
       // projrigidbody.AddForce(vectortotarget*Time.fixedDeltaTime* enginePower);
        flyForward();

    }




    private void flyForward()
    {
        projrigidbody.AddRelativeForce(Vector2.up*Time.fixedDeltaTime* enginePower);





    }

    float GetAngleFromVector(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        
        return n;



    }


    float GetAngleFromVectorcorectedforsprite(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
         n -= 90f;
        if (n < 0) n += 360;
        return n;



    }
    float IncrementralRotation(float rotation,float RotationToTarget)
    {
        float thisRotationtotarget = RotationToTarget;
        //if (thisRotationtotarget > 180) thisRotationtotarget -= 360;
        float rotateBy = 5;
        float finalRotation=0;
        if (thisRotationtotarget - rotation > -180f && thisRotationtotarget - rotation < 0 || thisRotationtotarget - rotation > 180f)
        {
            
           //  if (thisRotationtotarget < rotation - rotateBy)
           // {
                finalRotation = rotation - rotateBy;
           // }
       //    else {finalRotation= thisRotationtotarget; }
        }
        else
        {
           
         //   if(thisRotationtotarget -rotation >= 0) {
           //     if (thisRotationtotarget  > rotation + rotateBy) 
             //   {
                finalRotation=rotation +rotateBy;
                
               // }
           //     else {finalRotation= thisRotationtotarget; }
            
           // }

        }

        return finalRotation;
    }
}
