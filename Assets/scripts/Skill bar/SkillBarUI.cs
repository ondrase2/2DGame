using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillBarUI : MonoBehaviour
{
    static SkillBarUI SkillBarInstance;
    [SerializeField] private GameObject SkillSlotPrefab;
    [SerializeField] private GameObject SkillSlotWithInterfacePrefab;
    public static List<SkillSlotUI> SkillSlotlist = new List<SkillSlotUI>();
    public static List<SkillSlotUiWithInterface> SkillSlotlistWithInterface = new List<SkillSlotUiWithInterface>();

    // Start is called before the first frame update
    void Awake()
    {

        SkillBarInstance = this;
        Instantiate(SkillSlotPrefab, gameObject.transform).GetComponent<SkillSlotUI>().init();
        Instantiate(SkillSlotPrefab, gameObject.transform).GetComponent<SkillSlotUI>().init();
        Instantiate(SkillSlotPrefab, gameObject.transform).GetComponent<SkillSlotUI>().init();
        Instantiate(SkillSlotPrefab, gameObject.transform).GetComponent<SkillSlotUI>().init();
        Instantiate(SkillSlotWithInterfacePrefab, gameObject.transform).GetComponent<SkillSlotUiWithInterface>().init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
