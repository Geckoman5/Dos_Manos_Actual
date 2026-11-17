using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy List")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [Header("Spawn Configuration")]
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private int WaveNumber = 0;
    [SerializeField] private float spawnInterval = 60f;
    //in seconds
    [SerializeField] private int PerWave = 2;
    [SerializeField] private bool newWave = true;
    private float Timer;
    [SerializeField] private float height = 10f;
    //must be higher than the terrian or else they might clip thru and fall off

    void Start()
    {
        Timer = spawnInterval;
        Timer -= Time.deltaTime;
        newWave = true;
    }

    void Update()
    {
        Timer -= Time.deltaTime;

        if (newWave == true)  
        {
            WaveNumber++;
            SpawnWave(WaveNumber);
            Timer = spawnInterval;
            newWave = false;
        }

        if (Timer <= 0)
        {
            newWave = true;
        }

    }

    private Vector3 GetSpawnLocation()
    {
        Vector2 offset = Random.insideUnitCircle * spawnRadius;
        Vector3 position = transform.position + new Vector3(offset.x, 0f, offset.y);
        position.y = height;
        return position;
    }

    private void SpawnWave(int WaveNumber)
    {
        for (int i = 0; i < PerWave; i++)
        {
            if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("No enemy prefabs assigned to EnemySpawner.");
            return;
        }
            int index = Random.Range(0, enemyPrefabs.Length);

            Instantiate(enemyPrefabs[index], GetSpawnLocation(), Quaternion.identity);

            //waves have set ranges! Depending on the level/area, different enemies will spawn
            //always increase in enemies as time goes on.
            //every 5 waves, shop
            //every 10, boss

            if (i == 1) //WAVE 1
            {
                //spawn 2 orange and blue spheres
                for (int j = 0; j < 2; j++)
                {
                Instantiate(enemyPrefabs[0], GetSpawnLocation(), Quaternion.identity);
                Instantiate(enemyPrefabs[1], GetSpawnLocation(), Quaternion.identity);
                }
                Debug.Log("Wave: " + WaveNumber);
            }
            else if (i >= 2 && i < 5) //WAVE 2-4
            {
                //spawn 2 to 4 orange and blue
                for (int j = 0; j < Random.Range(2, 5); j++)
                {
                    Instantiate(enemyPrefabs[0], GetSpawnLocation(), Quaternion.identity);
                    Instantiate(enemyPrefabs[1], GetSpawnLocation(), Quaternion.identity);
                }
                Debug.Log("Wave: " + WaveNumber);
            }




        }
    }

 }

