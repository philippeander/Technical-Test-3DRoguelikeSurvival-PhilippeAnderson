using UnityEngine;

[CreateAssetMenu(fileName = "ChunkType", menuName = "World/Chunk Type")]
public class ChunkType : ScriptableObject
{
    public GameObject prefab;
    public float weight = 1f; // raridade
}