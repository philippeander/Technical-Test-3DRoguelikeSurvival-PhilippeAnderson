using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private float activeTime = 0.05f; 
    private float damage = 10;
    Collider col;
    MeshRenderer rend;

    void Awake()
    {
        col = GetComponent<Collider>();
        rend = GetComponent<MeshRenderer>();
        col.enabled = false;
        rend.enabled = false; 
    }

    public void TryAttack(float damage, float activeTime)
    {
        this.damage = damage;
        this.activeTime = activeTime;
        StartCoroutine(HitboxRoutine());
    }

    private System.Collections.IEnumerator HitboxRoutine()
    {
        col.enabled = true;
        rend.enabled = true;
        yield return new WaitForSeconds(activeTime);
        col.enabled = false;
        rend.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health hp = other.GetComponent<Health>();
            if (hp != null)
                hp.TakeDamage(damage);
        }
    }
}