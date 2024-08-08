using System.Collections;
using UnityEngine;
using UnityEngine.UI; 

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public float spawnInterval = 2f; 
    public int enemiesPerSpawn = 5; 
    public Button toggleSpawnButton; 

    public Transform minSpawn, maxSpawn;
    private Transform player; 
    private bool canSpawn = true; 

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        toggleSpawnButton.onClick.AddListener(ToggleSpawn); 
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
                for (int i = 0; i < enemiesPerSpawn; i++)
                {
                    Instantiate(enemyPrefab, SelectSpawnPoint(), Quaternion.identity);
                }
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void ToggleSpawn()
    {
        canSpawn = !canSpawn; 
    }
    public Vector3 SelectSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;
        bool spawnVerticleEdge = Random.Range(0f, 1f) > .5f;
        if(spawnVerticleEdge)
        {
            spawnPoint.y = Random.Range(minSpawn.position.y,maxSpawn.position.y);
            if(Random.Range(0f,1f) > .5f)
            {
                spawnPoint.x = maxSpawn.position.x;
            }else
            {
                spawnPoint.x = minSpawn.position.x;
            }
        }else
        {
            spawnPoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);
            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.y = maxSpawn.position.y;
            }
            else
            {
                spawnPoint.y = minSpawn.position.y;
            }
        }
        return spawnPoint;
    } 
}
