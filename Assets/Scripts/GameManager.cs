using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject miniBossPrefab; // Mini boss prefab
    public Transform[] spawnPoints; // Corners of the map
    public int waveNumber = 1; // Current wave number
    private int wavesUntilMiniBoss = 5; // Mini boss spawns every 5 waves

    void Start()
    {
        StartWave();
    }

    void StartWave()
    {
        if (waveNumber % wavesUntilMiniBoss == 0)
        {
            SpawnMiniBoss();
        }
        waveNumber++;
    }

    void SpawnMiniBoss()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject miniBoss = Instantiate(miniBossPrefab, spawnPoint.position, Quaternion.identity);
        MiniBoss miniBossScript = miniBoss.GetComponent<MiniBoss>();
        miniBossScript.IncreaseDifficulty(waveNumber / wavesUntilMiniBoss);
    }

    // Call this method when the wave is completed
    public void OnWaveCompleted()
    {
        StartWave();
    }
}
