using UnityEngine;

public class XPOrb : MonoBehaviour {
    public int xpAmount = 10;
    public float attractionSpeed = 5f;
    public float pickupDistance = 1.2f;

    Transform player;
    
    GameObject XPOrbInstance;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public void Init(GameObject orbPrefab)
    {   
        XPOrbInstance = orbPrefab;
    }

    void Update() {
        if (player == null) return;
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= pickupDistance) {
            LevelSystem.Instance?.AddXP(xpAmount);
            PoolManager.Instance.Release(XPOrbInstance, gameObject);
            return;
        }
        // move towards player slowly
        transform.position = Vector3.MoveTowards(transform.position, player.position, attractionSpeed * Time.deltaTime);
    }
}