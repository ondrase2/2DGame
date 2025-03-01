using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class target : MonoBehaviour
{

    public float health;

    public float maxhealth;
    public static List<target> targetlist;
    enemyshiphealthbar healthbar;
    enemyshieldscript shieldbar;
    public float maxshield = 100;
    public float shield;
    float regentimeout = 0;
    [SerializeField] bool shieldsenabled;
    [SerializeField] GameObject dropboxgameobject;
    
    public static target Getclosest(Vector3 position,float inrange)
    {
        target closest = null;
        
        foreach (target giventarget in targetlist)
        if(Vector3.Distance(position,giventarget.transform.position) <= inrange)
        {
            if (closest == null)
            {
                closest = giventarget;
            }

            else
            {
                if (Vector3.Distance(position, giventarget.transform.position) < Vector3.Distance(position, closest.transform.position))
                {
                    closest= giventarget;

                }

            }


        }
        return closest;
    
    }

     void Awake()
    {
        if(targetlist == null) targetlist= new List<target>();
        targetlist.Add(this);
        foreach (target debugtarget in targetlist)
        health = 100;
        shield= 100;
    }



    // Start is called before the first frame update
    void Start()
    {
        
       

    }

    // Update is called once per frame
    void Update()
    {
        if (shieldsenabled)
        {
            if (regentimeout <= 0)
            {
                if (shield < maxshield)
                {
                    shieldregen(Time.deltaTime);

                }



            }
            else if(regentimeout >0) regentimeout -= Time.deltaTime;

        }
        
    }
    public void takedamage(float damage)
    {
        if (damage > 0) regentimeout = 3;
        
        
        if (shield > 0 && shieldsenabled)
        {
            shield -= damage;

           // Debug.Log(shield);
            shieldbar = gameObject.GetComponentInChildren<enemyshieldscript>();
            shieldbar.changehealth(-damage);
            if (shield <= 0)
            {
                shield = 0;

            }
        }





        else
        {
            health -= damage;

            //Debug.Log(health);
            healthbar = gameObject.GetComponentInChildren<enemyshiphealthbar>();
            healthbar.changehealth(-damage);
            if (health <= 0)
            {
                if (dropboxgameobject != null)
                {
                    Instantiate(dropboxgameobject, transform.position, quaternion.identity).GetComponent<dropbox>().containedloot = "LaserTurret";//contained loot not case senstive and ignores spaces for now

                }


                targetlist.Remove(this);
                Destroy(gameObject);


            }



        }

    }

    public void shieldregen(float regen)
    {
        


        
            shield += regen;

            //Debug.Log(shield);
            shieldbar = gameObject.GetComponentInChildren<enemyshieldscript>();
            shieldbar.changehealth(regen);
            if (shield >= 100)
            {
                shield = maxshield;

            }
        

    }


}
