using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject[] bossPrefabs;
    public Transform player;
    public float minSpawnDistance = 20f; 
    public float maxSpawnDistance = 40f; 
    private int waveNumber = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SummonBoss();
        }
    }

    void SummonBoss()
    {
        waveNumber++;
        int randomIndex = Random.Range(0, bossPrefabs.Length);

        
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector2 spawnPosition = (Vector2)player.position + randomDirection * randomDistance;


        GameObject bossInstance = Instantiate(bossPrefabs[randomIndex], spawnPosition, Quaternion.identity);

        Boss boss = bossInstance.GetComponent<Boss>();
        boss.IncreaseDifficulty(waveNumber);
    }
}
