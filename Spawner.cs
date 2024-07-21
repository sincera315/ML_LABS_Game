using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pumpkin; // Reference to the arrow prefab
    private float spawnInterval = 3f; // Spawn interval in seconds
    private float timeSinceLastSpawn = 0f; // Time since last spawn

    private void Update()
    {
        // Increment the timer
        timeSinceLastSpawn += Time.deltaTime;

        // Check if it's time to spawn
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnPumpkin();
            timeSinceLastSpawn = 0f; // Reset the timer
        }
    }

    private void SpawnPumpkin()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Instantiate(pumpkin, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // Define the inner area bounds (adjust as needed)
        float minX = -70f;
        float maxX = 70f;
        float minY = -40f;
        float maxY = 40f;

        // Generate random coordinates within the specified bounds
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        // Check if the generated position is too close to the box
        float minDistanceToBox = 10f; // Adjust this distance as needed
        Vector3 boxPosition = new Vector3(0f, 0f, 0f); // Get the position of your box GameObject 
        Vector3 spawnPosition = new Vector3(x, y, 0f);

        while (Vector3.Distance(spawnPosition, boxPosition) < minDistanceToBox)
        {
            x = Random.Range(minX, maxX);
            y = Random.Range(minY, maxY);
            spawnPosition = new Vector3(x, y, 0f);
        }

        return spawnPosition;
    }

}
