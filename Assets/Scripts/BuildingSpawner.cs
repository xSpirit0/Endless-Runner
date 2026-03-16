using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    public GameObject[] leftBuildings;
    public GameObject[] rightBuildings;

    public float sideDistance = 20f;
    public float spawnOffsetZ = 8f;

    void Start()
    {
        SpawnBuildings();
    }

    void SpawnBuildings()
    {
        int randomLeft = Random.Range(0, leftBuildings.Length);
        int randomRight = Random.Range(0, rightBuildings.Length);

        Vector3 leftPos = new Vector3(-sideDistance, 0, transform.position.z + spawnOffsetZ);
        Vector3 rightPos = new Vector3(sideDistance, 0, transform.position.z + spawnOffsetZ);

        Instantiate(leftBuildings[randomLeft], leftPos, leftBuildings[randomLeft].transform.rotation);
        Instantiate(rightBuildings[randomRight], rightPos, rightBuildings[randomRight].transform.rotation);
    }
}