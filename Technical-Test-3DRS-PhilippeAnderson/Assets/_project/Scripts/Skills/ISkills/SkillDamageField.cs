using UnityEngine;

public class SkillDamageField : ISkill
{
    PlayerCombat owner;
    SkillData data;
    float timer;
    GameObject fieldInstance;

    public void Initialize(PlayerCombat owner, SkillData data)
    {
        this.owner = owner;
        this.data = data;
    }

    public void Tick()
    {
        timer -= Time.deltaTime;

        if (fieldInstance == null && timer <= 0)
        {
            fieldInstance = PoolManager.Instance.Get(data.prefab);
            fieldInstance.transform.SetParent(owner.transform);
            fieldInstance.transform.localPosition = Vector3.zero;
            DamageField damageField = fieldInstance.GetComponent<DamageField>();
            damageField.Init(data.value);
            
            timer = data.duration;
        }
        else if (fieldInstance != null && timer <= 0)
        {
            PoolManager.Instance.Release(data.prefab, fieldInstance);
            fieldInstance = null;

            timer = data.cooldown;
        }
    }
}