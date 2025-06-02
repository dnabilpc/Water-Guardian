using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.iOS;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner main;

    [Header("Refrences")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Atributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPersecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private int maxWaves = 1;
    [SerializeField] private bool infiniteWaves = false;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    private bool allWavesCompleted = false; // Tambahkan flag untuk track completion

    private void Awake()
    {
        main = this;
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    public int GetEnemiesLeft()
    {
        return enemiesLeftToSpawn + enemiesAlive;
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPersecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            Debug.Log("Panggil EndWave() untuk memulai wave berikutnya");
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
        Debug.Log($"Enemy destroyed! Enemies alive: {enemiesAlive}, Left to spawn: {enemiesLeftToSpawn}, All waves completed: {allWavesCompleted}");

        // Pastikan enemiesAlive tidak negatif
        if (enemiesAlive < 0)
        {
            Debug.LogError("DOUBLE COUNTING DETECTED! Resetting enemiesAlive to 0");
            enemiesAlive = 0;
        }

        // Check jika semua wave sudah selesai DAN semua enemy sudah mati
        if (allWavesCompleted && enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            Debug.Log("Calling GameWon!");
            GameWon();
        }
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        Debug.Log($"maxWaves: {maxWaves}, currentWave: {currentWave}, infiniteWaves: {infiniteWaves}");
        // Cek apakah ini wave terakhir
        if (!infiniteWaves && currentWave > maxWaves)
        {
            allWavesCompleted = true; // Set flag bahwa semua wave sudah selesai
            // JANGAN panggil GameWon() di sini, tunggu sampai semua enemy mati
            Debug.Log("All waves spawned! Waiting for all enemies to be destroyed...");
            // Check jika semua wave sudah selesai DAN semua enemy sudah mati
            if (allWavesCompleted && enemiesAlive == 0 && enemiesLeftToSpawn == 0)
            {
                Debug.Log("Calling GameWon!");
                GameWon();
            }
        }

        StartCoroutine(StartWave());
    }

    private void GameWon()
    {
        Debug.Log("Congratulations! All waves completed and all enemies destroyed!");
        LevelManager.main.GameWon();
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[0];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}
