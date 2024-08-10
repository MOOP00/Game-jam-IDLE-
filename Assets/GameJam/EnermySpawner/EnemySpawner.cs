using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public int minEnemiesPerSpawn = 3;
    public int maxEnemiesPerSpawn = 8;
    public Button toggleSpawnButton;
    public Transform minSpawn, maxSpawn;
    private Transform player;
    private bool canSpawn = true;
    private int waveNumber = 1;
    private int enemiesRemainingToSpawn;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        toggleSpawnButton.onClick.AddListener(ToggleSpawn);
        enemiesRemainingToSpawn = 30; // Starting with 30 enemies in the first wave
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        transform.position = player.position;
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (canSpawn)
            {
                int enemiesToSpawn = Random.Range(minEnemiesPerSpawn, maxEnemiesPerSpawn);

                // Make sure we don't spawn more enemies than we need
                enemiesToSpawn = Mathf.Min(enemiesToSpawn, enemiesRemainingToSpawn);

                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    Instantiate(enemyPrefab, SelectSpawnPoint(), Quaternion.identity);
                }

                enemiesRemainingToSpawn -= enemiesToSpawn;

                if (enemiesRemainingToSpawn <= 0)
                {
                    // Prepare for the next wave
                    waveNumber++;
                    enemiesRemainingToSpawn = Mathf.RoundToInt(30 * Mathf.Pow(1.15f, waveNumber - 1));
                }

                yield return new WaitForSeconds(spawnInterval);
            }
            else
            {
                yield return null;
            }
        }
    }

    private void ToggleSpawn()
    {
        canSpawn = !canSpawn;
    }

    public Vector3 SelectSpawnPoint()
    {
        // Randomize the spawn point within the defined area
        float randomX = Random.Range(minSpawn.position.x, maxSpawn.position.x);
        float randomY = Random.Range(minSpawn.position.y, maxSpawn.position.y);

        return new Vector3(randomX, randomY, 0f);
    }
}
