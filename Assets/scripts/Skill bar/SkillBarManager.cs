using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillBarManager : MonoBehaviour
{
    [SerializeField] private SkillBarUI skillBarUI;
    public static Action[] assignedHotkeys = new Action[20];
    public static Action<float>[] assignedSkillsCooldowns = new Action<float>[20];
    [SerializeField] private player1 playerInstance;
    public static ISkill[] skillArray = new ISkill[20];
    // Start is called before the first frame update
    void Awake()
    {
        assignedHotkeys[0] = playerInstance.newshoot;
        assignedHotkeys[1] = playerInstance.shootray;
        assignedHotkeys[2] = playerInstance.shootMissile;
        assignedHotkeys[3] = playerInstance.BoostSkill;
        
        
        
    }
    void Start()
    {
        skillArray[4] = playerInstance.teleportToMouseDelayedSkill;
        if (skillArray[4] == null) Debug.Log("player is too slow");
        skillBarUI.gameObject.SetActive(true);
        playerInstance.boostSkillCdNotifier += assignedSkillsCooldowns[3];

    }
    // Update is called once per frame
    void Update()
    {
        CheckKeyPresses();
        
    }
    private void CheckKeyPresses()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (assignedHotkeys[0] != null) assignedHotkeys[0]();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (assignedHotkeys[1] != null) assignedHotkeys[1]();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (assignedHotkeys[2] != null) assignedHotkeys[2]();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (assignedHotkeys[3] != null) assignedHotkeys[3]();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (skillArray[4] != null) skillArray[4].ActivateSkill();
        }
    }
   
    //skill serialization
    //public ISkill DeserealizeSkill(int? untId,int skillId)
    //{




        
    //}
    
    
    public interface ISkill
    {
        public void ActivateSkill();
        public Action<float> CooldownNotifier { get ; set; }
    }
    
    
    
    

}
