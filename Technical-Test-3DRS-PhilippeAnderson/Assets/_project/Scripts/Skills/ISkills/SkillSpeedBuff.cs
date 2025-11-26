using UnityEngine;

public class SkillSpeedBuff : ISkill
{
    PlayerCombat owner;
    SkillData data;
    bool applied = false;

    public void Initialize(PlayerCombat owner, SkillData data)
    {
        this.owner = owner;
        this.data = data;
    }

    public void Tick()
    {
        if (!applied)
        {
            owner.GetComponent<PlayerController>().speed *= data.value; 
            applied = true;
        }
    }
}