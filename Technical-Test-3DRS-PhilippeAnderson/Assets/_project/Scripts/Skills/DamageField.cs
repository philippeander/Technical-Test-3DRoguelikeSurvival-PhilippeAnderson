using System.Collections.Generic;
using UnityEngine;

public class DamageField : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damageInterval = 1f;

    private float damageAmount;
    
    public void Init(float damageAmount )
    {
        this.damageAmount = damageAmount;
    }
    
    private Dictionary<Health, float> damageTimers = new Dictionary<Health, float>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && other.TryGetComponent<Health>(out Health enemyHealth))
        {
            // registra o alvo; permite dano imediato (use Time.time + damageInterval se quiser esperar)
            if (!damageTimers.ContainsKey(enemyHealth))
                damageTimers.Add(enemyHealth, Time.time);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && other.TryGetComponent<Health>(out Health enemyHealth))
        {
            if (damageTimers.ContainsKey(enemyHealth))
                damageTimers.Remove(enemyHealth);
        }
    }

    private void Update()
    {
        if (damageTimers.Count == 0)
            return;

        List<Health> targets = new List<Health>(damageTimers.Keys);
        foreach (var target in targets)
        {
            if (target == null)
            {
                damageTimers.Remove(target);
                continue;
            }

            if (target.currentHP <= 0)
            {
                damageTimers.Remove(target);
                continue;
            }

            if (Time.time >= damageTimers[target])
            {
                target.TakeDamage(damageAmount);
                damageTimers[target] = Time.time + damageInterval;
            }
        }
    }

    private void OnDisable()
    {
        damageTimers.Clear();
    }
}