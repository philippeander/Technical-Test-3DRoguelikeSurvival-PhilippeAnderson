using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float rotationSpeed = 180f;

    [Header("Combat Settings")]
    public float attackRange = 1.5f;
    public int damage = 10;
    public float attackCooldown = 1f;

    public bool alwaysFollowing = true;

    [Header("Perception")]
    public bool playerInRange = false;   // Ligado pelo SphereCollider Trigger

    [Header("References")]
    public Transform player;
    public Health health;

    private float attackTimer = 0f;
    private bool isFrontBlocked = false;

    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        if (health == null)
            health = GetComponent<Health>();
    }

    void Update()
    {
        if (health != null && health.IsDead) return;
        if (!playerInRange && !alwaysFollowing) return;  // Fora da zona → inimigo fica parado

        attackTimer -= Time.deltaTime;

        RotateTowardPlayer();
        MoveIfNotBlocked();
        TryAttack();
    }

    void RotateTowardPlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        Quaternion targetRot = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
    }

    void MoveIfNotBlocked()
    {
        if (isFrontBlocked) return;
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    void TryAttack()
    {
        if (!isFrontBlocked) return;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist > attackRange) return;

        if (attackTimer <= 0f)
        {
            attackTimer = attackCooldown;

            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
                playerHealth.TakeDamage(damage);
        }
    }

    // ------------------------------------------------------
    //          COLISÃO – Só para se for NA FRENTE
    // ------------------------------------------------------
    void OnCollisionEnter(Collision col)
    {
        if (!IsValidObstacle(col.collider)) return;

        foreach (ContactPoint c in col.contacts)
        {
            Vector3 dir = (c.point - transform.position).normalized;
            float dot = Vector3.Dot(transform.forward, dir);

            if (dot > 0.5f) // Obstáculo realmente à frente
            {
                isFrontBlocked = true;
                return;
            }
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (IsValidObstacle(col.collider))
            isFrontBlocked = false;
    }

    bool IsValidObstacle(Collider col)
    {
        return col.CompareTag("Player") || col.CompareTag("Enemy");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
