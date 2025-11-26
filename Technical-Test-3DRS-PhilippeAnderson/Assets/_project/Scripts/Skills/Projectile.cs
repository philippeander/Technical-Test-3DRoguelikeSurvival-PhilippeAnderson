using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    
    private float lifetime = 3f;
    private float damage = 10;

    private GameObject prefabReference;

    float timer;

    void OnEnable()
    {
        timer = lifetime;
    }
    
    public void Init(float damageAmount, float lifetimeAmount, GameObject prefabRef)
    {
        damage = damageAmount;
        lifetime = lifetimeAmount;
        prefabReference = prefabRef;
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        timer -= Time.deltaTime;
        if (timer <= 0)
            PoolManager.Instance.Release(prefabReference, gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health hp = other.GetComponent<Health>();
            if (hp != null)
                hp.TakeDamage(damage);

            PoolManager.Instance.Release(prefabReference, gameObject);
        }
    }
}