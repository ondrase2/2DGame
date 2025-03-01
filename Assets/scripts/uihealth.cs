using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uihealth : MonoBehaviour
{
    public static uihealth instance
    { 
     get;
     private set ;
    }
    
    public Image healthbar;
    public float originalsize;
    // Start is called before the first frame update

    private void Awake()
    {
        instance= this;

    }



    void Start()
    {
        healthbar = GetComponent<Image>();
        originalsize =  healthbar.rectTransform.rect.width;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setvalue(float value)
    {
        
    }
}


