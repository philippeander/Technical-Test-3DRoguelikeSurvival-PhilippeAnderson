using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Player Reference")]
    public Transform player;
    public LevelSystem playerLevel; // seu sistema atual de level

    [Header("Spawn Settings")]
    public float spawnRadius = 20f;
    public float spawnInterval = 2f;
    public int maxEnemies = 50;
    private float timer = 0f;
    
    [Header("Enemies")]
    public List<EnemyType> enemyTypes;

    [Header("Y Adjustment (optional)")]
    public float fixedY = 0f;

    [Header("Refs")]
    public List<GameObject> activeEnemies = new List<GameObject>();
    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f && activeEnemies.Count < maxEnemies)
        {
            timer = spawnInterval;
            SpawnEnemyBasedOnLevel();
        }
    }

    void SpawnEnemyBasedOnLevel()
    {
        int level = playerLevel.level;

        List<EnemyType> allowed = new List<EnemyType>();

        foreach (var e in enemyTypes)
        {
            if (e.level <= level)
                allowed.Add(e);
        }

        if (allowed.Count == 0) return;

        EnemyType chosen = allowed[Random.Range(0, allowed.Count)];

        Vector3 pos = GetSpawnPositionOnCircle();

        GameObject instEnemy = PoolManager.Instance.Get(chosen.prefab);
        instEnemy.transform.position = pos;
        Health enemyHealth = instEnemy.GetComponent<Health>();
        
        activeEnemies.Add(instEnemy);
        enemyHealth.OnDie = () =>
        {
            Debug.LogWarning(" Enemy Died, spawning XP Orb");
            KillCounter.Instance.AddKill();
            
            GameObject xpOrb = PoolManager.Instance.Get(chosen.RewardPrefab);
            xpOrb.transform.position = instEnemy.transform.position;
            xpOrb.GetComponent<XPOrb>().Init(chosen.RewardPrefab);
            
            activeEnemies.Remove(instEnemy);
            
            enemyHealth.Heal(enemyHealth.maxHP);
            PoolManager.Instance.Release(chosen.prefab, instEnemy);
            enemyHealth.OnDie = null;
        };
    }

    Vector3 GetSpawnPositionOnCircle()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);

        Vector3 dir = new Vector3(
            Mathf.Cos(angle),
            0,
            Mathf.Sin(angle)
        );

        Vector3 pos = player.position + dir * spawnRadius;

        pos.y = fixedY;

        return pos;
    }
}
