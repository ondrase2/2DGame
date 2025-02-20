using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSellectButton : MonoBehaviour
{
    //[SerializeField] BuildingUi buildingUi;
    public int buttonid;
     BuildingManager buildingManager;
   
    // Start is called before the first frame update
    void Start()
    {
        buildingManager = BuildingManager.Instance;
        BuildingUi.buttonlist.Add(this);
        buttonid = BuildingUi.buttonlist.Count;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition += new Vector2((buttonid-1) * 400, 0);


        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        


    }


    public void OnClick()
    {
        Debug.Log("event");
        switch (buttonid)

        {
            case 1:
                buildingManager.itemToSpawn = "LaserTurret";
                break;


            case 2:
                buildingManager.itemToSpawn = "MissileTurret";
                break;


            case 3:
                //buildingmanager.itemtospawn = case3
                break;

        }
    }
 
}
