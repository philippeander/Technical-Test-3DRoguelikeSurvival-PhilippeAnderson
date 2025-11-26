using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "Game/Enemy Type")]
public class EnemyType : ScriptableObject
{
    public GameObject prefab;
    public int level = 1; // nível mínimo para esse inimigo aparecer
    public GameObject RewardPrefab;
}

