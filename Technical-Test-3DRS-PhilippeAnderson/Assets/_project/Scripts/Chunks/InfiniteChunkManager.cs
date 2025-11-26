using System.Collections.Generic;
using UnityEngine;

public class InfiniteChunkManager : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Chunk Settings")]
    public float chunkSize = 10f;
    public int activeRadius = 2; // quantos chunks de distância manter

    [Header("Chunk Types (Weighted)")]
    public List<ChunkType> chunkTypes;

    // Cache: coordenada → instância do chunk
    private Dictionary<Vector2Int, GameObject> chunkMap = new Dictionary<Vector2Int, GameObject>();

    private Vector2Int currentPlayerChunk;

    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        // Detect chunk inicial caso exista
        Vector2Int firstChunk = GetPlayerChunkCoord();

        foreach (Transform child in transform)
        {
            Vector2Int coord = new Vector2Int(
                Mathf.RoundToInt(child.position.x / chunkSize),
                Mathf.RoundToInt(child.position.z / chunkSize)
            );

            if (!chunkMap.ContainsKey(coord))
                chunkMap.Add(coord, child.gameObject);
        }

        currentPlayerChunk = firstChunk;
        UpdateChunksAroundPlayer();
    }
    
    void Update()
    {
        if (player == null) return;

        Vector2Int newChunk = GetPlayerChunkCoord();

        if (newChunk != currentPlayerChunk)
        {
            currentPlayerChunk = newChunk;
            UpdateChunksAroundPlayer();
        }
    }

    // -----------------------------------------------------------
    //  CALCULA O CHUNK ATUAL DO PLAYER
    // -----------------------------------------------------------
    Vector2Int GetPlayerChunkCoord()
    {
        int cx = Mathf.FloorToInt(player.position.x / chunkSize);
        int cz = Mathf.FloorToInt(player.position.z / chunkSize);

        return new Vector2Int(cx, cz);
    }

    // -----------------------------------------------------------
    //  ATUALIZAÇÃO DO MUNDO AO REDOR DO PLAYER
    // -----------------------------------------------------------
    void UpdateChunksAroundPlayer()
    {
        HashSet<Vector2Int> needed = new HashSet<Vector2Int>();

        for (int x = -activeRadius; x <= activeRadius; x++)
        {
            for (int z = -activeRadius; z <= activeRadius; z++)
            {
                Vector2Int coord = currentPlayerChunk + new Vector2Int(x, z);
                needed.Add(coord);

                if (!chunkMap.ContainsKey(coord))
                {
                    SpawnChunk(coord);
                }
                else
                {
                    chunkMap[coord].SetActive(true);
                }
            }
        }

        // Desativar chunks que estão longe demais
        List<Vector2Int> toDisable = new List<Vector2Int>();

        foreach (var kvp in chunkMap)
        {
            if (!needed.Contains(kvp.Key))
                toDisable.Add(kvp.Key);
        }

        foreach (var coord in toDisable)
            chunkMap[coord].SetActive(false);
    }

    // -----------------------------------------------------------
    //  SPAWN COM RARIDADE (WEIGHTED RANDOM)
    // -----------------------------------------------------------
    void SpawnChunk(Vector2Int coord)
    {
        GameObject prefab = SelectRandomChunkPrefab();

        Vector3 pos = new Vector3(
            coord.x * chunkSize,
            0,
            coord.y * chunkSize
        );

        GameObject instance = Instantiate(prefab, pos, Quaternion.identity, transform);
        chunkMap.Add(coord, instance);
    }

    GameObject SelectRandomChunkPrefab()
    {
        float total = 0f;
        foreach (var t in chunkTypes) total += t.weight;

        float r = Random.value * total;
        float c = 0f;

        foreach (var t in chunkTypes)
        {
            c += t.weight;
            if (r <= c)
                return t.prefab;
        }

        return chunkTypes[0].prefab; 
    }
}