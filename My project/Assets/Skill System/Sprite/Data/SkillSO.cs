using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UDEV.SkillSystem
{
    [CreateAssetMenu(menuName = "UDEV/Skill System/SkillSO")]
    public class SkillSO : ScriptableObject
    {
        [Header("Skill Properties")]
        public string skillName;
        public float timeTrigger;
        public float cooldownTime;
        
        [Header("Skill Visuals")]
        public Sprite skillIcon;
        
        [Header("Skill Sound Effects")]
        public AudioClip triggerSoundFx;
    }
}
