using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class npctraderv2 : MonoBehaviour
{
    public GameObject dialogboxobject;
    public TextMeshProUGUI dialogbox;
    [SerializeField] GameObject dialogboxyes;
    [SerializeField] GameObject dialogboxno;
    [SerializeField] string switchdialog;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void interact()
    {

        switch (switchdialog)

        {
            case "start":
            dialogboxno.SetActive(true);
        dialogboxyes.SetActive(true);
                break;
           
           
            case "yes":
                dialogboxno.SetActive(false);
                gameObject.SetActive(false);
                break;
                
            
            case "no":
                dialogboxyes.SetActive(false);
                gameObject.SetActive(false);
                break;
    
        }
    }






}

