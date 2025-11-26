using UnityEngine;

public class SkillHeal : ISkill
{
    PlayerCombat owner;
    SkillData data;
    bool applied = false;

    Health playerHealth;
    
    public void Initialize(PlayerCombat owner, SkillData data)
    {
        this.owner = owner;
        this.data = data;
        
        playerHealth = owner.GetComponent<Health>();
    }

    public void Tick()
    {
        if (!applied && playerHealth.currentHP < playerHealth.maxHP)
        {
            owner.GetComponent<Health>().Heal(data.value);
            applied = true;
        }
    }
}