using UnityEngine;

public class PassengerSpawner : MonoBehaviour
{
    public GameObject passengerPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 3f; 
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnPassenger();
            timer = 0f;
        }
    }

    private void SpawnPassenger()
    {
        if (passengerPrefab != null && spawnPoint != null)
        {
            Instantiate(passengerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    public void SpawnSinglePassenger()
    {
        if (passengerPrefab != null && spawnPoint != null)
        {
            Instantiate(passengerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}