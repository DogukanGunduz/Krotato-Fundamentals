using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int enemiesPerWave = 5; // Her dalga ba��na d��man say�s�
    public float spawnInterval = 10f; // D��man dalgalar� aras�ndaki s�re (saniye cinsinden)
    public float gameDuration = 60f; // Oyun s�resi (saniye cinsinden)

    private float currentTime = 0f;
    private float nextSpawnTime = 0f;
    private bool gameIsRunning = true;

    public GameObject enemyPrefab; // D��man prefab'�
    public Transform player; // Oyuncu karakteri
    public Collider2D mapCollider; // Harita zeminini temsil eden objenin Collider2D bile�eni
    public float playerSpawnDistance = 2f; // Oyuncudan en az ne kadar uzakta spawn edilece�i

    public GameObject spawnIndicatorPrefab; // D��man spawn yerini g�steren i�aret prefab'�

    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if (gameIsRunning)
        {
            currentTime += Time.deltaTime;

            // Yeni d��man spawn etme kontrol�
            if (Time.time >= nextSpawnTime)
            {
                SpawnEnemyWave(); // D��manlar� spawn et
                nextSpawnTime = Time.time + spawnInterval;
            }

            // Oyun s�resi kontrol�
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
            // D��man� spawn et
            Vector3 spawnPosition = GetRandomSpawnPosition();

            // Oyuncudan yeterince uzakta m� kontrol et
            if (Vector3.Distance(spawnPosition, player.position) >= playerSpawnDistance)
            {
                // Ger�ek d��man� spawn et
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                // EnemyAI script'ini al
                EnemyAI enemyAI = newEnemy.GetComponent<EnemyAI>();

                // E�er EnemyAI script'i varsa, player referans� varsa, player'� hedef olarak belirle
                if (enemyAI != null && player != null)
                {
                    enemyAI.target = player;
                }

                // D��man�n tam olarak do�aca�� yeri g�ster
                GameObject spawnIndicator = Instantiate(spawnIndicatorPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Collider s�n�rlar�n� al
        Bounds colliderBounds = mapCollider.bounds;

        // Rastgele bir spawn pozisyonu al (Collider s�n�rlar� i�inde)
        float randomX = Random.Range(colliderBounds.min.x, colliderBounds.max.x);
        float randomY = Random.Range(colliderBounds.min.y, colliderBounds.max.y);

        // Rastgele pozisyonu ve y�ksekli�i d�nd�r
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
