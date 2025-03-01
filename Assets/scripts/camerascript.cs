using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerascript : MonoBehaviour
{
    Vector3 playerposvec;
    [SerializeField] Transform playerpos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerpos != null)
        {
            playerposvec =playerpos.position;
            playerposvec.z = -10;
            transform.position = playerposvec;

        }
    }
}
