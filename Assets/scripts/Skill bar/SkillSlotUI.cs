using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    public int SkillSlotId;
    public Action skillAction;
   
    // Start is called before the first frame update
    void Start()
    {
        //SkillBarUI.SkillSlotlist.Add(this);
        //SkillSlotId = BuildingUi.buttonlist.Count - 1;
        //RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        //rectTransform.anchoredPosition += new Vector2(SkillSlotId * 120, 0);
        //gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void init()
    {
        SkillBarUI.SkillSlotlist.Add(this);
        SkillSlotId = SkillBarUI.SkillSlotlist.Count - 1;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition += new Vector2(SkillSlotId * 120, 0);
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
        skillAction = SkillBarManager.assignedHotkeys[SkillSlotId];
        SkillBarManager.assignedSkillsCooldowns[SkillSlotId] = CoolldownChanged;
    }
    public void OnClick()
    {
        Debug.Log("event");
        if(skillAction != null) skillAction();

    }
    public void CoolldownChanged(float cd) {
        int cdTruncated = (int)(System.MathF.Truncate(cd * 10));
        
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = System.Convert.ToString(cdTruncated / 10) +"."+ System.Convert.ToString(cdTruncated % 10);
        


    }
}
