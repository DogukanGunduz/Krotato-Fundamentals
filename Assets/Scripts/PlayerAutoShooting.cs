using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoShooting : MonoBehaviour
{
    public Transform weaponTransform; // Silah�n Transform bile�eni
    public float shootingRange = 10f; // Ate� etme menzili
    public float fireRate = 0.5f; // Ate� h�z� (saniye cinsinden)
    public GameObject bulletPrefab; // Mermi prefab'�
    public float bulletSpeed = 10f; // Mermi h�z�
    public float leadAmount = 0.5f; // D��man�n �n�ne ge�me miktar�

    private float nextFireTime = 0f;
    private GameObject nearestEnemy;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            nearestEnemy = FindNearestEnemy();

            if (nearestEnemy != null)
            {
                Vector3 targetPosition = nearestEnemy.transform.position; // Hedef d��man�n pozisyonu

                // D�nme a��s�n� hesapla
                Vector2 direction = targetPosition - transform.position;
                float distance = direction.magnitude;

                // D��man�n hareketine g�re hedeflenen konumu g�ncelle
                Vector3 leadPosition = targetPosition + (Vector3)(nearestEnemy.GetComponent<Rigidbody2D>().velocity * (distance / bulletSpeed) * leadAmount);

                // D�nme a��s�n� tekrar hesapla
                direction = leadPosition - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Mermiyi oyuncu objesinin konumunda ve d�nme a��s�nda olu�tur
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

                // Mermi h�z�n� ayarla
                bulletRb.velocity = direction.normalized * bulletSpeed;

                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
}
