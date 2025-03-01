using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingUi : MonoBehaviour
{
    static BuildingUi buidingUiinstance;
    [SerializeField] private GameObject buttonPrefab;
    public static List<BuildingSellectButton> buttonlist = new List<BuildingSellectButton>();

    // Start is called before the first frame update
    void Awake()
    {
        buidingUiinstance = this;
        Instantiate(buttonPrefab,gameObject.transform).GetComponentInChildren<TextMeshProUGUI>().text ="railgun";
        Instantiate(buttonPrefab, gameObject.transform).GetComponentInChildren<TextMeshProUGUI>().text = "missile Turret";
        Instantiate(buttonPrefab, gameObject.transform);



        onActive();
    }

    // Update is called once per frame
    void Update()
    {
        




    }

    public void onActive()
    {



    }


}
