using UnityEngine;

public class SkillOrbit : ISkill
{
    PlayerCombat owner;
    SkillData data;
    float timer;
    GameObject orbit;

    public void Initialize(PlayerCombat owner, SkillData data)
    {
        this.owner = owner;
        this.data = data;
        timer = 0;
    }

    public void Tick()
    {
        timer -= Time.deltaTime;
        
        if (orbit == null && timer <= 0)
        {
            orbit = PoolManager.Instance.Get(data.prefab);
            orbit.transform.SetParent(owner.transform);
            //orbit.transform.localPosition = Vector3.right * 1.5f;

            OrbitProjectile orbitProj = orbit.GetComponent<OrbitProjectile>();
            orbitProj.Init(data.value, data.duration, data.prefab, owner.gameObject);
            
            timer = data.duration;
        }
        else if (orbit != null && timer <= 0)
        {
            PoolManager.Instance.Release(data.prefab, orbit);
            orbit = null;
            
            timer = data.cooldown;
        }
    }
}