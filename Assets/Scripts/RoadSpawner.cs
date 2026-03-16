using UnityEngine;
using System.Collections.Generic;

public class RoadSpawner : MonoBehaviour
{
    public GameObject roadTile;
    public int tilesAhead = 5;
    public float tileLength = 21f;

    private Transform player;
    private float spawnZ = 0f;
    private Queue<GameObject> activeTiles = new Queue<GameObject>();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < tilesAhead; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        if (player.position.z + (tilesAhead * tileLength) > spawnZ)
        {
            SpawnTile();
        }

        if (activeTiles.Count > 0)
        {
            GameObject firstTile = activeTiles.Peek();

            if (player.position.z - firstTile.transform.position.z > tileLength * 2f)
            {
                Destroy(activeTiles.Dequeue());
            }
        }
    }

    void SpawnTile()
    {
        GameObject newTile = Instantiate(
            roadTile,
            new Vector3(0, 0, spawnZ),
            Quaternion.Euler(0, 90, 0)
        );

        activeTiles.Enqueue(newTile);
        spawnZ += tileLength;
    }
}