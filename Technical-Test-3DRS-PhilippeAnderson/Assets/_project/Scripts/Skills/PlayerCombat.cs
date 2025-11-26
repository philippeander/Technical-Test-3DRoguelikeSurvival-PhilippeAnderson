using System;
using UnityEngine;
using System.Collections.Generic;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private SkillData defaultSkillData;
    
    private List<ISkill> activeSkills = new List<ISkill>();

    private void Start()
    {
        AddSkill(defaultSkillData);
    }

    public void AddSkill(SkillData data)
    {
        ISkill skill = CreateSkillInstance(data.type);
        skill.Initialize(this, data);
        activeSkills.Add(skill);
        SkillUIManager.Instance.AddSkillIcon(data.icon);
    }

    private ISkill CreateSkillInstance(SkillType type)
    {
        return type switch
        {
            SkillType.Melee             => new SkillMelee(),
            SkillType.ProjectileForward => new SkillProjectileForward(),
            SkillType.Projectile4Dir    => new SkillProjectile4Directions(),
            SkillType.DamageField       => new SkillDamageField(),
            SkillType.Orbit             => new SkillOrbit(),
            SkillType.SpeedBuff         => new SkillSpeedBuff(),
            SkillType.Heal              => new SkillHeal(),
            _ => null
        };
    }

    private void Update()
    {
        foreach (ISkill skill in activeSkills)
            skill.Tick();
    }
}