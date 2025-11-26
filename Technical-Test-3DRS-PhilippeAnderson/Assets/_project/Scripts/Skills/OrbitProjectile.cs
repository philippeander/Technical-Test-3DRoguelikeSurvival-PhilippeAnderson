using UnityEngine;

public class OrbitProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float dist = 2f;
    
    private float lifetime = 3f;
    private float damage = 10;

    private GameObject prefabReference;
    private GameObject orbitedObject;
    
    float timer;
    
    void OnEnable()
    {
        timer = lifetime;
    }
    
    public void Init(float damageAmount, float lifetimeAmount, GameObject prefabRef, GameObject orbitedObject)
    {
        this.damage = damageAmount;
        this.lifetime = lifetimeAmount;
        this.prefabReference = prefabRef;
        this.orbitedObject = orbitedObject;
        
        transform.localPosition = Vector3.right * dist;
    }
    
    void Update()
    {
        if(orbitedObject)
            transform.RotateAround(orbitedObject.transform.position, Vector3.up, 180 * Time.deltaTime);

        // timer -= Time.deltaTime;
        // if (timer <= 0)
        //     PoolManager.Instance.Release(prefabReference, gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health hp = other.GetComponent<Health>();
            if (hp != null)
                hp.TakeDamage(damage);

            // retorna ao pool
            PoolManager.Instance.Release(prefabReference, gameObject);
        }
    }
}
