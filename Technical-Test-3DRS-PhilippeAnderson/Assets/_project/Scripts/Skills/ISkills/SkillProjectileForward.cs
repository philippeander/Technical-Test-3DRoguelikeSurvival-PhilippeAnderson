using UnityEngine;

public class SkillProjectileForward : ISkill
{
    PlayerCombat owner;
    SkillData data;
    float cd;

    public void Initialize(PlayerCombat owner, SkillData data)
    {
        this.owner = owner;
        this.data = data;
        cd = data.cooldown;
    }

    public void Tick()
    {
        cd -= Time.deltaTime;
        if (cd <= 0)
        {
            GameObject proj = PoolManager.Instance.Get(data.prefab);
            proj.transform.position = owner.transform.position;
            proj.transform.rotation = owner.transform.rotation;

            Projectile projectile = proj.GetComponent<Projectile>();
            projectile.Init(data.value, data.cooldown, data.prefab);
            
            cd = data.cooldown;
        }
    }
}