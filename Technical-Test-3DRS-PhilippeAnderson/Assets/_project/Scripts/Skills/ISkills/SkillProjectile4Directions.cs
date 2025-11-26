using UnityEngine;

public class SkillProjectile4Directions : ISkill
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
            var rotation = owner.transform.rotation;
            Quaternion[] dirs =
            {
                rotation,
                rotation * Quaternion.Euler(0, 90, 0),
                rotation * Quaternion.Euler(0,-90, 0),
                rotation * Quaternion.Euler(0,180, 0)
            };

            foreach (var dir in dirs)
            {
                GameObject proj = PoolManager.Instance.Get(data.prefab);
                proj.transform.position = owner.transform.position;
                proj.transform.rotation = dir;
                
                Projectile projectile = proj.GetComponent<Projectile>();
                projectile.Init(data.value, data.cooldown, data.prefab);
            }

            cd = data.cooldown;
        }
    }
}