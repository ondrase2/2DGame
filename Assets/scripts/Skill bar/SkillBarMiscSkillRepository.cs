using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SkillBarManager;

public static class SkillBarMiscSkillRepository
{
    
    
    
    public class BuildingManagerSpawnturretSkill : ISkill
    {
        
        public Action<float> CooldownNotifier { get; set; }
        public void ActivateSkill()
        {
            SpawnTurret();

        }
        private void SpawnTurret() 
        {
            BuildingManager.Instance.SpawnTurret();

        
        }


    }


}
