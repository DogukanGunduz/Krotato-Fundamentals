using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int enemiesPerWave = 5; // Her dalga baþýna düþman sayýsý
    public float spawnInterval = 10f; // Düþman dalgalarý arasýndaki süre (saniye cinsinden)
    public float gameDuration = 60f; // Oyun süresi (saniye cinsinden)

    private float currentTime = 0f;
    private float nextSpawnTime = 0f;
    private bool gameIsRunning = true;

    public GameObject enemyPrefab; // Düþman prefab'ý
    public Transform player; // Oyuncu karakteri
    public Collider2D mapCollider; // Harita zeminini temsil eden objenin Collider2D bileþeni
    public float playerSpawnDistance = 2f; // Oyuncudan en az ne kadar uzakta spawn edileceði

    public GameObject spawnIndicatorPrefab; // Düþman spawn yerini gösteren iþaret prefab'ý

    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if (gameIsRunning)
        {
            currentTime += Time.deltaTime;

            // Yeni düþman spawn etme kontrolü
            if (Time.time >= nextSpawnTime)
            {
                SpawnEnemyWave(); // Düþmanlarý spawn et
                nextSpawnTime = Time.time + spawnInterval;
            }

            // Oyun süresi kontrolü
            if (currentTime >= gameDuration)
            {
                EndGame();
            }
        }
    }

    void SpawnEnemyWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            // Düþmaný spawn et
            Vector3 spawnPosition = GetRandomSpawnPosition();

            // Oyuncudan yeterince uzakta mý kontrol et
            if (Vector3.Distance(spawnPosition, player.position) >= playerSpawnDistance)
            {
                // Gerçek düþmaný spawn et
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                // EnemyAI script'ini al
                EnemyAI enemyAI = newEnemy.GetComponent<EnemyAI>();

                // Eðer EnemyAI script'i varsa, player referansý varsa, player'ý hedef olarak belirle
                if (enemyAI != null && player != null)
                {
                    enemyAI.target = player;
                }

                // Düþmanýn tam olarak doðacaðý yeri göster
                GameObject spawnIndicator = Instantiate(spawnIndicatorPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Collider sýnýrlarýný al
        Bounds colliderBounds = mapCollider.bounds;

        // Rastgele bir spawn pozisyonu al (Collider sýnýrlarý içinde)
        float randomX = Random.Range(colliderBounds.min.x, colliderBounds.max.x);
        float randomY = Random.Range(colliderBounds.min.y, colliderBounds.max.y);

        // Rastgele pozisyonu ve yüksekliði döndür
        return new Vector3(randomX, randomY, 0f);
    }

    void EndGame()
    {
        // Oyunu durdur
        Time.timeScale = 0f;
        gameIsRunning = false;
        Debug.Log("Oyun bitti!");
    }
}
