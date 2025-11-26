using UnityEngine;

public class SkillMelee : ISkill
{
    PlayerCombat owner;
    SkillData data;
    float timer;

    MeleeAttack melee;

    public void Initialize(PlayerCombat owner, SkillData data)
    {
        this.owner = owner;
        this.data = data;

        // pega o componente no jogador
        melee = owner.GetComponentInChildren<MeleeAttack>();

        timer = 0f;
    }

    public void Tick()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = data.cooldown;
            melee.TryAttack(data.value, data.duration);
        }
    }
}