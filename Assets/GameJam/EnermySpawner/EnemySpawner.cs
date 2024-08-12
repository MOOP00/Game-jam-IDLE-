using System.Collections;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 4f;
    public int minEnemiesPerSpawn = 3;
    public int maxEnemiesPerSpawn = 9;
    public Transform minSpawn, maxSpawn;
    public float waveDelay = 3f;
    private Transform player;
    private bool canSpawn = false;
    public int waveNumber = 0;
    private int enemiesRemainingToSpawn;
    private int enemiesRemainingInWave;
    private int bossesRemainingInWave;
    public static EnemySpawner Instance { get; private set; }
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI enemiesLeftText;

    // Reference to the BossManager script
    public BossManager bossManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("_player").transform;
    }

    private void Update()
    {
        transform.position = player.position;

        if (waveNumber == 0 && Input.GetKeyDown(KeyCode.T))
        {
            StartNextWave();
        }

        UpdateUI();
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (canSpawn)
            {
                int enemiesToSpawn = Random.Range(minEnemiesPerSpawn, maxEnemiesPerSpawn);
                enemiesToSpawn = Mathf.Min(enemiesToSpawn, enemiesRemainingToSpawn);

                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    Instantiate(enemyPrefab, SelectSpawnPoint(), Quaternion.identity);
                    enemiesRemainingInWave++;
                }

                enemiesRemainingToSpawn -= enemiesToSpawn;

                if (enemiesRemainingToSpawn <= 0)
                {
                    canSpawn = false;
                }

                yield return new WaitForSeconds(spawnInterval);
            }
            else
            {
                yield return null;
            }
        }
    }

    public void BossDefeated()
    {
        bossesRemainingInWave = Mathf.Max(bossesRemainingInWave - 1, 0);
        UpdateUI();

        if (enemiesRemainingInWave <= 0 && bossesRemainingInWave <= 0)
        {
            StartCoroutine(WaveDelayCoroutine());
        }
    }

    public void EnemyDefeated()
    {
        enemiesRemainingInWave = Mathf.Max(enemiesRemainingInWave - 1, 0);
        Debug.Log("Enemy defeated. Enemies left: " + enemiesRemainingInWave);
        UpdateUI();

        if (enemiesRemainingInWave <= 0 && bossesRemainingInWave <= 0)
        {
            StartCoroutine(WaveDelayCoroutine());
        }
    }

    private IEnumerator WaveDelayCoroutine()
    {
        yield return new WaitForSeconds(waveDelay);
        StartNextWave();
    }

    private void StartNextWave()
    {
        waveNumber++;
        Debug.Log($"Starting wave {waveNumber}");

        // Find the PlayerHealth component and increase health per wave
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.IncreaseHealthPerWave(waveNumber);
        }

        if (waveNumber % 10 == 0 && waveNumber != 0)
        {
            // Use BossManager to spawn a boss
            SpawnBoss();
        }
        else
        {
            // Regular wave
            enemiesRemainingToSpawn = Mathf.RoundToInt(30 * Mathf.Pow(1.15f, waveNumber - 1));
            enemiesRemainingInWave = 0;
            canSpawn = true;
            StartCoroutine(SpawnEnemies());
        }

        UpdateUI();
    }

    private void SpawnBoss()
    {
        if (bossManager != null)
        {
            bossManager.SetWaveNumber(waveNumber);
            bossManager.SummonBosses(3); // Spawn 3 bosses, selected from 3 random prefabs
            bossesRemainingInWave = 3; // Assuming three bosses per wave
        }
        else
        {
            Debug.LogWarning("BossManager not assigned!");
        }
    }

    public Vector3 SelectSpawnPoint()
    {
        float minDistanceFromPlayer = 20f;

        Vector3 spawnPoint;
        int attempts = 0;
        int maxAttempts = 30;

        do
        {
            float randomX = Random.Range(minSpawn.position.x, maxSpawn.position.x);
            float randomY = Random.Range(minSpawn.position.y, maxSpawn.position.y);
            spawnPoint = new Vector3(randomX, randomY, 0f);

            attempts++;
        } while (Vector3.Distance(spawnPoint, player.position) < minDistanceFromPlayer && attempts < maxAttempts);

        return spawnPoint;
    }

    private void UpdateUI()
    {
        if (waveText != null)
        {
            waveText.text = "Wave: " + waveNumber;
        }

        if (enemiesLeftText != null)
        {
            // This should reflect total enemies left
            enemiesLeftText.text = "Enemies Left: " + (enemiesRemainingInWave + bossesRemainingInWave);
        }
    }
}
