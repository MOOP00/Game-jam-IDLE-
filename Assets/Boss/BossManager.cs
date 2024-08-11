using UnityEngine;
using System.Linq;

public class BossManager : MonoBehaviour
{
    public GameObject[] bossPrefabs;
    public Transform player;
    public float minSpawnDistance = 40f;
    public float maxSpawnDistance = 60f;
    private int waveNumber = 0;
    public GameObject healthBarPrefab; // Assign this in the inspector

    public void SetWaveNumber(int wave)
    {
        waveNumber = wave;
    }

    public void SummonBosses(int count)
    {
        GameObject[] selectedBossPrefabs = SelectRandomBossPrefabs(3);

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, selectedBossPrefabs.Length);

            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
            Vector2 spawnPosition = (Vector2)player.position + randomDirection * randomDistance;

            GameObject bossInstance = Instantiate(selectedBossPrefabs[randomIndex], spawnPosition, Quaternion.identity);

            Boss boss = bossInstance.GetComponent<Boss>();
            boss.IncreaseDifficulty(waveNumber);

            // Assign the health bar prefab to the boss instance
            boss.healthBarPrefab = healthBarPrefab;
        }
    }

    // Method to select 3 random unique boss prefabs from the bossPrefabs array
    private GameObject[] SelectRandomBossPrefabs(int selectionCount)
    {
        // Create a copy of the bossPrefabs array and shuffle it
        GameObject[] shuffledBossPrefabs = bossPrefabs.OrderBy(x => Random.value).ToArray();

        // Return the first 'selectionCount' prefabs from the shuffled array
        return shuffledBossPrefabs.Take(selectionCount).ToArray();
    }
}
