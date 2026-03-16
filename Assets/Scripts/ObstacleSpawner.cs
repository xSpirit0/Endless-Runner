using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public GameObject fencePrefab;
    public GameObject coinPrefab;

    public float laneDistance = 2f;

    // object heights
    public float carY = 0.35f;
    public float fenceY = 0.25f;
    public float coinY = 1.2f;

    // spawn position inside the road tile
    public float spawnZ = 8.8f;

    // distance from player where obstacles are allowed
    public float safeStartDistance = 30f;

    // obstacle patterns
    // 0 = empty
    // 1 = fence
    // 2 = car
    private int[][] patterns = new int[][]
    {
        new int[] {2,0,2},
        new int[] {2,1,0},
        new int[] {1,2,0},
        new int[] {0,2,1},
        new int[] {1,0,2},

        new int[] {2,2,0},
        new int[] {0,2,2},

        new int[] {1,0,1},
        new int[] {2,0,1},
        new int[] {1,0,2}
    };

    private static int lastPatternIndex = -1;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
            return;

        float distance = transform.position.z - player.transform.position.z;

        // prevent spawning obstacles near the start
        if (distance < safeStartDistance)
            return;

        int patternIndex = GetPatternIndex();
        int[] pattern = patterns[patternIndex];

        SpawnObstacles(pattern);
        SpawnCoin(pattern);
    }

    void SpawnObstacles(int[] pattern)
    {
        for (int lane = 0; lane < 3; lane++)
        {
            int type = pattern[lane];

            if (type == 0)
                continue;

            Vector3 pos = transform.position;

            pos.x += (lane - 1) * laneDistance;
            pos.z += spawnZ;

            if (type == 2)
            {
                pos.y = carY;

                GameObject car = Instantiate(carPrefab, pos, Quaternion.identity);

                Destroy(car, 8f);
            }

            if (type == 1)
            {
                pos.y = fenceY;

                GameObject fence = Instantiate(fencePrefab, pos, Quaternion.identity);

                Destroy(fence, 8f);
            }
        }
    }

    void SpawnCoin(int[] pattern)
    {
        if (Random.value > 0.5f)
            return;

        int emptyLane = -1;

        for (int i = 0; i < 3; i++)
        {
            if (pattern[i] == 0)
            {
                emptyLane = i;
                break;
            }
        }

        if (emptyLane == -1)
            return;

        Vector3 pos = transform.position;

        pos.x += (emptyLane - 1) * laneDistance;
        pos.z += spawnZ - 2f;
        pos.y = coinY;

        GameObject coin = Instantiate(coinPrefab, pos, Quaternion.identity);

        Destroy(coin, 8f);
    }

    int GetPatternIndex()
    {
        int newIndex = Random.Range(0, patterns.Length);

        if (newIndex == lastPatternIndex)
            newIndex = Random.Range(0, patterns.Length);

        lastPatternIndex = newIndex;

        return newIndex;
    }
}