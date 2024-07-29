using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pumpkin; // Reference to the pumpkin prefab
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
        // Define the outer area bounds (adjust as needed)
        float minX = -70f;
        float maxX = 70f;
        float minY = -40f;
        float maxY = 40f;

        // Randomly select a direction (0: north, 1: south, 2: east, 3: west)
        int direction = Random.Range(0, 4);

        float x = 0f;
        float y = 0f;

        switch (direction)
        {
            case 0: // North
                y = maxY;
                x = Random.Range(0,0);
                break;
            case 1: // South
                y = minY;
                x = Random.Range(0, 0);
                break;
            case 2: // East
                x = maxX;
                y = Random.Range(0, 0);
                break;
            case 3: // West
                x = minX;
                y = Random.Range(0, 0);
                break;
        }

        return new Vector3(x, y, 0f);
    }
}
